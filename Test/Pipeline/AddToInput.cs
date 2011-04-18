using System;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	public class AddToInput : IFilter<int, string>
	{
		private readonly int value;

		public AddToInput(int value)
		{
			this.value = value;
		}

		public string Execute(int context, Func<int, string> executeNext)
		{
			context += value;

			var result = executeNext(context);

			return result;
		}
	}
}