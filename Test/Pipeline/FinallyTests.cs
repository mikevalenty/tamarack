using NUnit.Framework;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	[TestFixture]
	public class FinallyTests
	{
		[Test]
		public void Should_execute_final_function()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Finally(x => x + "!");

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("2!"));
		}

		[Test]
		public void Should_throw_exception_when_no_filters_exist()
		{
			var pipeline = new Pipeline<int, string>();

			Assert.Throws<EndOfChainException>(() => pipeline.Execute(2));
		}

		[Test]
		public void Should_throw_exception_when_chain_goes_too_far_without_finally()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Add(new AddToInput(3));

			Assert.Throws<EndOfChainException>(() => pipeline.Execute(2));
		}

		[Test]
		public void Should_not_require_final_function_when_chain_short_circuits()
		{
			var pipeline = new Pipeline<int, string>();
			pipeline.Add(new ShortCircuit());

			var output = pipeline.Execute(2);

			Assert.That(output, Is.EqualTo("2"));
		}
	}
}