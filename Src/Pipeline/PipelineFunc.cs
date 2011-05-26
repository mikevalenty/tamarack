using System;
using System.Collections.Generic;

namespace Tamarack.Pipeline
{
	public class Pipeline<T, TOut> : IFilter<T, TOut>
	{
		private readonly IServiceProvider serviceProvider;
		private readonly IList<IFilter<T, TOut>> filters;
		private int current;

		public Pipeline()
			: this(new ActivatorServiceProvider())
		{ }

		public Pipeline(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
			filters = new List<IFilter<T, TOut>>();
		}

		public int Count
		{
			get { return filters.Count; }
		}

		public Pipeline<T, TOut> Add(IFilter<T, TOut> filter)
		{
			filters.Add(filter);
			return this;
		}

		public Pipeline<T, TOut> Add(Type filterType)
		{
			Add((IFilter<T, TOut>)serviceProvider.GetService(filterType));
			return this;
		}

		public Pipeline<T, TOut> Add<TFilter>() where TFilter : IFilter<T, TOut>
		{
			Add(typeof(TFilter));
			return this;
		}

		public TOut Execute(T input)
		{
			var tail = new Func<T, TOut>(x => { throw new EndOfChainException(); });

			return ((IFilter<T, TOut>)this).Execute(input, tail);
		}

		TOut IFilter<T, TOut>.Execute(T input, Func<T, TOut> executeNext)
		{
			GetNext = () => current < filters.Count
				? x => filters[current++].Execute(x, GetNext())
				: executeNext;

			return GetNext().Invoke(input);
		}

		private Func<Func<T, TOut>> GetNext { get; set; }
	}
}