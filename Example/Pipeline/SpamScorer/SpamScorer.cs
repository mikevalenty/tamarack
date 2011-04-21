using System;
using Tamarack.Example.Pipeline.SpamScorer.Filters;
using Tamarack.Pipeline;
using Tamarack.Pipeline.Extensions;

namespace Tamarack.Example.Pipeline.SpamScorer
{
	public class SpamScorer
	{
		public double CalculateSpamScore(string text)
		{
			var pipeline = new Pipeline<string, double>()
				.Add<SpamCopBlacklistFilter>()
				.Add<PerspcriptionDrugFilter>()
				.Add<PornographyFilter>()
				.Finally(score => 0);

			return pipeline.Execute(text);
		}

		public double CalculateSpamScore2(string text)
		{
			var pipeline = new Pipeline<string, double>()
				.AddFiltersIn("Tamarack.Example.Pipeline.SpamScorer.Filters")
				.Finally(score => 0);

			return pipeline.Execute(text);
		}
	}
}