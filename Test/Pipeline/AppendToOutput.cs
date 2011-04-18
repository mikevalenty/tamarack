using System;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	public class AppendToOutput : IFilter<int, string>
	{
		private readonly string value;

		public AppendToOutput(string value)
		{
			this.value = value;
		}

		public string Execute(int context, Func<int, string> executeNext)
		{
			var result = executeNext(context);

			result += value;

			return result;
		}
	}
}