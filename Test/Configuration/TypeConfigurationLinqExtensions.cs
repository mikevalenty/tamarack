using System.Collections.Generic;
using Tamarack.Configuration;

namespace Tamarack.Test.Configuration
{
	public static class TypeConfigurationLinqExtensions
	{
		public static IEnumerable<TypeConfigurationElement> AsEnumerable(this TypeConfigurationElementCollection collection)
		{
			foreach (TypeConfigurationElement element in collection)
			{
				yield return element;
			}
		}
	}
}