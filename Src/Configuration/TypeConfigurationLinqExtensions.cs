using System.Collections.Generic;

namespace Tamarack.Configuration
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