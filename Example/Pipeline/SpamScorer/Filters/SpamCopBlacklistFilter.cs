using System;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.SpamScorer.Filters
{
	public class SpamCopBlacklistFilter : IFilter<string, double>
	{
		public double Execute(string text, Func<string, double> executeNext)
		{
			throw new NotImplementedException();
		}
	}
}
