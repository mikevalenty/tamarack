using System;
using System.Linq;
using System.Configuration;

namespace Tamarack.Configuration
{
	public class TypeConfigurationElementCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new TypeConfigurationElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			var typeConfigurationElement = (TypeConfigurationElement)element;

			var key = typeConfigurationElement.TypeName;

			if (!string.IsNullOrEmpty(typeConfigurationElement.Name))
			{
				key += "." + typeConfigurationElement.Name;
			}

			return key;
		}
	}
}