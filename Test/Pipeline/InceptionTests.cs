using NUnit.Framework;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	[TestFixture]
	public class InceptionTests
	{
		[Test]
		public void Pipeline_func_can_be_a_filter()
		{
			var numbers = new Pipeline<int, string>()
				.Add(new AppendToOutput("1"))
				.Add(new AppendToOutput("2"))
				.Add(new AppendToOutput("3"));

			var result = new Pipeline<int, string>()
				.Add(numbers)
				.Add(new AppendToOutput("4"))
				.Finally(x => x.ToString())
				.Execute(5);

			Assert.That(result, Is.EqualTo("54321"));
		}

		[Test]
		public void Pipeline_can_be_a_filter()
		{
			var context = new MyContext();

			var numbers = new Pipeline<MyContext>()
				.Add(new AppendToValue("1"))
				.Add(new AppendToValue("2"))
				.Add(new AppendToValue("3"));

			new Pipeline<MyContext>()
				.Add(numbers)
				.Add(new AppendToValue("4"))
				.Execute(context);

			Assert.That(context.Value, Is.EqualTo("1234"));
		}
	}
}