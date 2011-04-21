using System;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline.Extensions
{
	public class AppendCaretToOutput : IFilter<int, string>
	{
		public string Execute(int context, Func<int, string> executeNext)
		{
			var result = executeNext(context);

			return result + "^";
		}
	}
}
