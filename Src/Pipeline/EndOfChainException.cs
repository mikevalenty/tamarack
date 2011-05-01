using System;

namespace Tamarack.Pipeline
{
	public class EndOfChainException : Exception
	{
		public EndOfChainException()
			: base("Next filter does not exist."
			+ " Use 'Finally' or a filter that short-circuits before reaching the end of the chain")
		{ }
	}
}