using System;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.LoginService.Filters
{
	public class WriteLoginAttemptToAuditLog : IFilter<LoginContext, bool>
	{
		public bool Execute(LoginContext context, Func<LoginContext, bool> executeNext)
		{
			return executeNext(context);
		}
	}
}