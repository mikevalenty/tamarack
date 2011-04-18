using System;

namespace Tamarack
{
	public class ActivatorServiceProvider : IServiceProvider
	{
		public object GetService(Type serviceType)
		{
			return Activator.CreateInstance(serviceType);
		}
	}
}