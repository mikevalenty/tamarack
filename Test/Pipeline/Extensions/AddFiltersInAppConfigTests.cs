using System;
using NUnit.Framework;
using Tamarack.Pipeline;
using Tamarack.Pipeline.Extensions;

namespace Tamarack.Test.Pipeline.Extensions
{
	[TestFixture]
	public class AddFiltersInAppConfigTests
	{
		[Test]
		public void Should_add_types_to_pipeline()
		{
			var pipeline = new Pipeline<int, string>().AddSection("filters");

			Assert.That(pipeline.Count, Is.EqualTo(2));
		}

		[Test]
		public void Should_throw_exception_when_config_section_is_not_found()
		{
			var pipeline = new Pipeline<int, string>();

			Assert.Throws<ArgumentException>(() => pipeline.AddSection("bogus"));
		}
	}
}
