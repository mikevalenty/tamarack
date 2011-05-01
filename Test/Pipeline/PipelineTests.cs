using Microsoft.Practices.Unity;
using NUnit.Framework;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	[TestFixture]
	public class PipelineTests
	{
		[Test]
		public void Filter_can_modify_input()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Add(new AddToInput(3));
			pipeline.Finally(x => x + "!");

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("5!"));
		}

		[Test]
		public void Filter_can_modify_output()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Add(new AppendToOutput("#"));
			pipeline.Finally(x => x + "!");

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("2!#"));
		}

		[Test]
		public void Should_apply_each_filter_in_order_added()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Add(new AddToInput(2));
			pipeline.Add(new AppendToOutput("@"));
			pipeline.Finally(x => x + "!");

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("4!@"));
		}

		[Test]
		public void Should_have_fluent_interface()
		{
			var output = new Pipeline<int, string>()
				.Add(new AddToInput(3))
				.Add(new AppendToOutput("#"))
				.Finally(x => x + "!")
				.Execute(2);

			Assert.That(output, Is.EqualTo("5!#"));
		}

		[Test]
		public void Should_build_filters_with_service_provider()
		{
			var container = new UnityContainer()
				.RegisterType<AppendToOutput>(new InjectionConstructor("*"));

			var output = new Pipeline<int, string>(new UnityServiceProvider(container))
				.Add<AppendToOutput>()
				.Finally(x => x + "!")
				.Execute(2);

			Assert.That(output, Is.EqualTo("2!*"));
		}
	}
}