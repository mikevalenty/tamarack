using System;

namespace Tamarack.Pipeline
{
	public interface IFilter<T>
	{
		void Execute(T context, Action<T> executeNext);
	}

	public interface IFilter<T, TOut>
	{
		TOut Execute(T context, Func<T, TOut> executeNext);
	}
}