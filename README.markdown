Tamarack is light-weight framework for implementing the Chain of Responsibility pattern in .NET
=================================================================================================

Why should I care?
------------------
The Chain of Responsibility is probably the best pattern evar.

Examples
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

Or how about login? There's all kinds of fun to be had:

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
