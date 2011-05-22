using System;
using System.Linq;
using System.Reflection;

namespace Tamarack.Pipeline.Extensions
{
	public static class AssemblyPipelineExtensions
	{
		public static Pipeline<T, TOut> AddFiltersIn<T, TOut>(this Pipeline<T, TOut> pipeline, Assembly assembly)
		{
			return AddFiltersIn(pipeline, Assembly.GetCallingAssembly(), t => true);
		}

		public static Pipeline<T, TOut> AddFiltersIn<T, TOut>(this Pipeline<T, TOut> pipeline, string filterNamespace)
		{
			return AddFiltersIn(pipeline, Assembly.GetCallingAssembly(), filterNamespace);
		}

		public static Pipeline<T, TOut> AddFiltersIn<T, TOut>(
			this Pipeline<T, TOut> pipeline,
			Assembly assembly,
			string filterNamespace)
		{
			return AddFiltersIn(pipeline, assembly, t => t.Namespace == filterNamespace);
		}

		public static Pipeline<T, TOut> AddFiltersIn<T, TOut>(
			this Pipeline<T, TOut> pipeline,
			Assembly assembly,
			Func<Type, bool> predicate)
		{
			var filterTypes = assembly
				.GetTypes()
				.Where(t => typeof(IFilter<T, TOut>).IsAssignableFrom(t));

			foreach (var filterType in filterTypes.Where(predicate))
			{
				pipeline.Add(filterType);
			}

			return pipeline;
		}
	}
}