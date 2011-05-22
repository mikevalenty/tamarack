using System;
using System.Linq;
using System.Reflection;

namespace Tamarack.Pipeline.Extensions
{
	public static class AssemblyPipelineExtensions
	{
		public static Pipeline<T, TOut> AddAssembly<T, TOut>(this Pipeline<T, TOut> pipeline)
		{
			return AddAssembly(pipeline, Assembly.GetCallingAssembly(), t => true);
		}

		public static Pipeline<T, TOut> AddAssembly<T, TOut>(this Pipeline<T, TOut> pipeline, Func<Type, bool> predicate)
		{
			return AddAssembly(pipeline, Assembly.GetCallingAssembly(), predicate);
		}

		public static Pipeline<T, TOut> AddAssembly<T, TOut>(this Pipeline<T, TOut> pipeline, Assembly assembly)
		{
			return AddAssembly(pipeline, assembly, t => true);
		}

		public static void AddAssemblyOf<T, TOut, TOf>(this Pipeline<T, TOut> pipeline)
		{
			AddAssembly(pipeline, typeof(TOf).Assembly, t => true);
		}

		public static Pipeline<T, TOut> AddAssembly<T, TOut>(
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

		public static Pipeline<T, TOut> AddNamespace<T, TOut>(this Pipeline<T, TOut> pipeline, string filterNamespace)
		{
			return AddAssembly(pipeline, Assembly.GetCallingAssembly(), t => t.Namespace == filterNamespace);
		}

		public static Pipeline<T, TOut> AddNamespace<T, TOut>(
			this Pipeline<T, TOut> pipeline,
			Assembly assembly,
			string filterNamespace)
		{
			return AddAssembly(pipeline, assembly, t => t.Namespace == filterNamespace);
		}
	}
}