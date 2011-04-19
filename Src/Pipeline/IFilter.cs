using System;

namespace Tamarack.Pipeline
{
	public interface IFilter<T, TOut>
	{
		TOut Execute(T context, Func<T, TOut> executeNext);
	}
}