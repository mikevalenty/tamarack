using System;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.SpamScorer.Filters
{
	public class PornographyFilter : IFilter<string, double>
	{
		public double Execute(string text, Func<string, double> executeNext)
		{
			var score = executeNext(text);

			if (text.Contains("sex"))
				score += .1;

			return score;
		}
	}
}