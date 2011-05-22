using System;
using System.Linq;
using System.Configuration;
using NUnit.Framework;
using Tamarack.Configuration;

namespace Tamarack.Test.Configuration
{
	[TestFixture]
	public class TypeConfigurationSectionTests
	{
		[Test]
		public void Should_read_list_of_types_from_system_configuration()
		{
			var config = (TypeCollectionConfigurationSection)ConfigurationManager.GetSection("filters");

			Assert.That(config.TypeCollection, Has.Count.EqualTo(2));
		}

		[Test]
		public void Can_specify_name_when_types_are_the_same()
		{
			var config = (TypeCollectionConfigurationSection)ConfigurationManager.GetSection("named");

			var names = config.TypeCollection.AsEnumerable().Select(t => t.Name);

			Assert.That(names, Has.Member("first"));
			Assert.That(names, Has.Member("second"));
		}
	}
}