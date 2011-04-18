using System;
using System.Collections.Generic;

namespace Tamarack.Pipeline
{
	public class Pipeline<T, TOut>
	{
		private readonly IServiceProvider serviceProvider;
		private readonly IList<IFilter<T, TOut>> filters;
		private Func<T, TOut> tail;
		private int current;

		public Pipeline()
			: this(new ActivatorServiceProvider())
		{
		}

		public Pipeline(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
			filters = new List<IFilter<T, TOut>>();
		}

		public Pipeline<T, TOut> Add(IFilter<T, TOut> filter)
		{
			filters.Add(filter);
			return this;
		}

		public Pipeline<T, TOut> Add<TFilter>() where TFilter : IFilter<T, TOut>
		{
			Add((IFilter<T, TOut>)serviceProvider.GetService(typeof(TFilter)));
			return this;
		}

		public Pipeline<T, TOut> Finally(Func<T, TOut> func)
		{
			tail = func;
			return this;
		}

		public TOut Execute(T input)
		{
			GuardAgainstNullTailFunc();

			GetNext = () => current < filters.Count
					? x => filters[current++].Execute(x, GetNext())
					: tail;

			return GetNext().Invoke(input);
		}

		private void GuardAgainstNullTailFunc()
		{
			if (tail == null)
				throw new InvalidOperationException("Finally function must be set");
		}

		private Func<Func<T, TOut>> GetNext { get; set; }
	}
}