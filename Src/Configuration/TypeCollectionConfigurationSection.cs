using System.Configuration;

namespace Tamarack.Configuration
{
	public class TypeCollectionConfigurationSection : ConfigurationSection
	{
		private const string CollectionPropertyName = "types";

		[ConfigurationProperty(CollectionPropertyName)]
		public TypeConfigurationElementCollection TypeCollection
		{
			get { return (TypeConfigurationElementCollection)this[CollectionPropertyName]; }
		}
	}
}