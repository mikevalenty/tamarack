using System;
using System.Configuration;
using System.Reflection;
using Tamarack.Configuration;

namespace Tamarack.Pipeline.Extensions
{
	public static class TypeConfigurationPipelineExtensions
	{
		public static Pipeline<T, TOut> AddSection<T, TOut>(this Pipeline<T, TOut> pipeline, string section)
		{
			ForEachTypeIn(section, t => pipeline.Add(t));

			return pipeline;
		}

		public static Pipeline<T> AddSection<T>(this Pipeline<T> pipeline, string section)
		{
			ForEachTypeIn(section, t => pipeline.Add(t));

			return pipeline;
		}

		private static void ForEachTypeIn(string section, Action<Type> action)
		{
			var config = (TypeCollectionConfigurationSection)ConfigurationManager.GetSection(section);

			if (config == null)
				throw new ArgumentException("Configuration section '" + section + "' required");

			foreach (TypeConfigurationElement element in config.TypeCollection)
			{
				action(element.Type);
			}
		}
	}
}