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
			if (IsValid(context))
				return true;

			return executeNext(context);
		}

		private bool IsValid(LoginContext context)
		{
			var user = repository.FindByUsername(context.Username);

			if (user == null)
				return false;

			return user.IsValid(context.Password);
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