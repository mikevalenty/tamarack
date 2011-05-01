using System;
using System.Collections.Generic;

namespace Tamarack.Pipeline
{
	public class Pipeline<T, TOut>
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
			GetNext = () => current < filters.Count
				? x => filters[current++].Execute(x, GetNext())
				: new Func<T, TOut>(c => { throw new EndOfChainException(); });

			return GetNext().Invoke(input);
		}

		private Func<Func<T, TOut>> GetNext { get; set; }
	}
}