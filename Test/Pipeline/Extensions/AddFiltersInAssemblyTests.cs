using System;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Tamarack.Pipeline;
using Tamarack.Pipeline.Extensions;

namespace Tamarack.Test.Pipeline.Extensions
{
	[TestFixture]
	public class AddFiltersInAssemblyTests
	{
		private IServiceProvider serviceProvider;

		[SetUp]
		public void SetUp()
		{
			var container = new UnityContainer()
				.RegisterType<AddToInput>(new InjectionConstructor(3))
				.RegisterType<AppendToOutput>(new InjectionConstructor("#"));

			serviceProvider = new UnityServiceProvider(container);
		}

		[Test]
		public void Should_load_filters_in_namespace_in_alphabetical_order()
		{
			var pipeline = new Pipeline<int, string>()
				.AddNamespace("Tamarack.Test.Pipeline.Extensions")
				.Finally(x => x.ToString());

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("2*^"));
		}

		[Test]
		public void Should_load_all_filters_in_assembly()
		{
			var pipeline = new Pipeline<int, string>(serviceProvider)
				.AddAssembly(typeof(AddFiltersInAssemblyTests).Assembly);

			Assert.That(pipeline.Count, Is.EqualTo(5));
		}
	}
}
