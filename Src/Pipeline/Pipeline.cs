using System;
using System.Collections.Generic;

namespace Tamarack.Pipeline
{
	public class Pipeline<T>
	{
		private readonly IServiceProvider serviceProvider;
		private readonly IList<IFilter<T>> filters;
		private int current;

		public Pipeline()
			: this(new ActivatorServiceProvider())
		{ }

		public Pipeline(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
			filters = new List<IFilter<T>>();
		}

		public int Count
		{
			get { return filters.Count; }
		}

		public Pipeline<T> Add(IFilter<T> filter)
		{
			filters.Add(filter);
			return this;
		}

		public Pipeline<T> Add(Type filterType)
		{
			Add((IFilter<T>)serviceProvider.GetService(filterType));
			return this;
		}

		public Pipeline<T> Add<TFilter>() where TFilter : IFilter<T>
		{
			Add(typeof(TFilter));
			return this;
		}

		public void Execute(T input)
		{
			GetNext = () => current < filters.Count
				? x => filters[current++].Execute(x, GetNext())
				: new Action<T>(c => { });

			GetNext().Invoke(input);
		}

		private Func<Action<T>> GetNext { get; set; }
	}
}