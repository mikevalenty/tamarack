using System;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.SpamScorer.Filters
{
	public class PerspcriptionDrugFilter : IFilter<string, double>
	{
		public double Execute(string text, Func<string, double> executeNext)
		{
			var score = executeNext(text);

			if (text.Contains("viagra"))
				score += .25;

			return score;
		}
	}
}