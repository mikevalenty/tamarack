using System;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.LoginService.Filters
{
	public class AuthenticateAgainstLocalStore : IFilter<LoginContext, bool>
	{
		private readonly IUserRepository repository;

		public AuthenticateAgainstLocalStore(IUserRepository repository)
		{
			this.repository = repository;
		}

		public bool Execute(LoginContext context, Func<LoginContext, bool> executeNext)
		{
			var user = repository.FindByUsername(context.Username);

			if (user != null)
				return user.IsValid(context.Password);

			return executeNext(context);
		}
	}

	public interface IUserRepository
	{
		User FindByUsername(string username);
	}

	public class User
	{
		public bool IsValid(string password)
		{
			return password == "local";
		}
	}
}