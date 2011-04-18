namespace Tamarack.Example.Pipeline.LoginService
{
	public class LoginContext
	{
		public LoginContext(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Username { get; private set; }

		public string Password { get; private set; }
	}
}