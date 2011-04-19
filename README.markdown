Tamarack is micro framework for implementing the Chain of Responsibility pattern in .NET
=================================================================================================

The Chain of Responsibility is a key building block of extensible software.

>Avoid coupling the sender of a request to its receiver by giving more than one object a 
>chance to handle the request. Chain the receiving objects and pass the request along the 
>chain until an object handles it. -- Gang of Four

Variations of this pattern are the basis for Servlet Filters, IIS Modules and Handlers and several open source 
projects that I've used including Sync4J, JAMES, Log4Net and Unity. It's an essential tool in OO toolbox and it's key in transforming rigid procedural code into a composable Domain Specific Language.

Show me examples!
-----------
Consider a block of code to process a blog comment coming from a web-based rich text editor. There are
probably several things you'll want to do before letting the text into your database. 
    
	public class BlogEngine
	{
		...

		public int Submit(Post post)
		{
			var pipeline = new Pipeline<Post, int>(serviceProvider)
				.Add<CanoncalizeHtml>()
				.Add<StripMaliciousTags>()
				.Add<RemoveJavascript>()
				.Add<RewriteProfanity>()
				.Add<GuardAgainstDoublePost>()
				.Finally(b => repository.Save(b));

			var blogId = pipeline.Execute(post);

			return blogId;
		}
	}

How about user login? There are all kinds of things you might need to do there:

	public class LoginService
	{
		...
	
		public bool Login(string username, string password)
		{
			var pipeline = new Pipeline<LoginContext, bool>(serviceProvider)
				.Add<WriteLoginAttemptToAuditLog>()
				.Add<LockoutOnConsecutiveFailures>()
				.Add<AuthenticateAgainstLocalStore>()
				.Add<AuthenticateAgainstLdap>()
				.Finally(c => false);

			return pipeline.Execute(new LoginContext(username, password));
		}
	}

Calculating a spam score in a random block of text:

	public class SpamScorer
	{
		...
		
		public double CalculateSpamScore(string text)
		{
			var pipeline = new Pipeline<string, double>(serviceProvider)
				.Add<SpamCopBlacklistFilter>()
				.Add<PerspcriptionDrugFilter>()
				.Add<PornographyFilter>()
				.Finally(score => 0);

			return pipeline.Execute(text);
		}
	}

How does it work?
-----------

It's pretty simple, there is just one interface to implement and it looks like this:

	public interface IFilter<T, TOut>
	{
		TOut Execute(T context, Func<T, TOut> executeNext);
	}

Basically, you get an input to operate on and you return a value. The executeNext delegate 
is the next filter in the chain using it in this fashion allows you several options:

 * Modify the input before the next filter gets it
 * Modify the output of the next filter before returning
 * Short circuit out of the chain by not calling the executeNext delegate

I learn by example, so let's look at this interface in action. In the spam score calculator 
example, each filter looks for markers in the text and adds to the overall spam score by
modifying the _result_ of the next filter before returning.

	public class PerspcriptionDrugFilter : IFilter<string, double>
	{
		public double Execute(string text, Func<string, double> executeNext)
		{
			var score = executeNext(text);

			if (text.Contains("viagra"))
				score += .25;

			return score;
		}
	}
	
In this login example, we're look for the user in our local user store and if it exists 
we'll short-circuit the chain and authenticate the request. Otherwise we'll let the request 
continue to the next filter which looks for the user in an Ldap respository.

	public class AuthenticateAgainstLocalStore : IFilter<LoginContext, bool>
	{
		...
		
		public bool Execute(LoginContext context, Func<LoginContext, bool> executeNext)
		{
			var user = repository.FindByUsername(context.Username);

			if (user != null)
				return user.IsValid(context.Password); // short circuit
			
			return executeNext(context);
		}
	}
