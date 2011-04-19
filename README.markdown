Tamarack is micro framework for implementing the Chain of Responsibility pattern in .NET
=================================================================================================

Why should I care?
------------------
The Chain of Responsibility is a key building block of extensible software. The Gang of Four describe
it as

>Avoid coupling the sender of a request to its receiver by giving more than one object a 
>chance to handle the request. Chain the receiving objects and pass the request along the 
>chain until an object handles it.

Variations of this pattern are the basis for Servlet Filters, IIS modules and handlers and several open source 
projects that I've used including Sync4J, JAMES, Log4Net and Unity. It's an essential tool in OO toolbox and it's key in transforming rigid procedural code into a composable Domain Specific Language.

Why do need a micro framework?
------------------
You don't. However after using variations of this pattern over and over again in enterprise applications, I settled 
into mildly a opinionated implementation that has helped me usher code into the pit of success in a team environment. 

Show me examples
-----------
Consider a block of code to submit a comment from a rich text editor. There are
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

How about user login? There all kinds of things you might be doing there:

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
