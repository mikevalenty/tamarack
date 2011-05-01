using System;

namespace Tamarack.Pipeline
{
	public static class PipelineExtensions
	{
		/// <summary>
		/// Convenience method to add a short-circuiting filter to the end of the chain
		/// </summary>
		public static Pipeline<T, TOut> Finally<T, TOut>(this Pipeline<T, TOut> pipeline, Func<T, TOut> func)
		{
			pipeline.Add(new ShortCircuit<T, TOut>(func));
			return pipeline;
		}

		private class ShortCircuit<T, TOut> : IFilter<T, TOut>
		{
			private readonly Func<T, TOut> func;

			public ShortCircuit(Func<T, TOut> func)
			{
				this.func = func;
			}

			public TOut Execute(T context, Func<T, TOut> executeNext)
			{
				return func.Invoke(context);
			}
		}
	}
}