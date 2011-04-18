using System;
using Microsoft.Practices.Unity;

namespace Tamarack.Test
{
	public class UnityServiceProvider : IServiceProvider
	{
		private readonly IUnityContainer container;

		public UnityServiceProvider(IUnityContainer container)
		{
			this.container = container;
		}

		public object GetService(Type serviceType)
		{
			return container.Resolve(serviceType);
		}
	}
}