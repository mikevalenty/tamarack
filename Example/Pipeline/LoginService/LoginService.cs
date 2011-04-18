using System;
using Tamarack.Example.Pipeline.LoginService.Filters;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.LoginService
{
	public class LoginService
	{
		private readonly IServiceProvider serviceProvider;

		public LoginService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

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
}