using System;
using Tamarack.Pipeline;

namespace Tamarack.Test.Pipeline
{
	public class ShortCircuit : IFilter<int, string>
	{
		public string Execute(int context, Func<int, string> executeNext)
		{
			return context.ToString();
		}
	}
}