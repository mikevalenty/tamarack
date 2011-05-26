using System;
using NUnit.Framework;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	[TestFixture]
	public class PipelineTests
	{
		[Test]
		public void Should_not_require_any_filters()
		{
			var context = new MyContext { Value = "hi" };

			var pipeline = new Pipeline<MyContext>();
			pipeline.Execute(context);

			Assert.That(context.Value, Is.EqualTo("hi"));
		}

		[Test]
		public void Should_apply_each_filter_in_order_added()
		{
			var context = new MyContext { Value = "one" };

			var pipeline = new Pipeline<MyContext>();
			pipeline.Add(new AppendToValue(", two"));
			pipeline.Add(new AppendToValue(", three"));
			pipeline.Execute(context);

			Assert.That(context.Value, Is.EqualTo("one, two, three"));
		}

	}

	public class MyContext
	{
		public string Value { get; set; }
	}

	public class AppendToValue : IFilter<MyContext>
	{
		private readonly string text;

		public AppendToValue(string text)
		{
			this.text = text;
		}

		public void Execute(MyContext context, Action<MyContext> executeNext)
		{
			context.Value += text;

			executeNext(context);
		}
	}
}