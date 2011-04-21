using System;
using Tamarack.Example.Pipeline.BlogEngine.Filters;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.BlogEngine
{
	public class BlogEngine
	{
		private readonly IServiceProvider serviceProvider;
		private readonly IBlogRepository repository;

		public BlogEngine(IServiceProvider serviceProvider, IBlogRepository repository)
		{
			this.serviceProvider = serviceProvider;
			this.repository = repository;
		}

		public int Submit(Post post)
		{
			var pipeline = new Pipeline<Post, int>(serviceProvider)
				.Add<CanoncalizeHtml>()
				.Add<StripMaliciousTags>()
				.Add<RemoveJavascript>()
				.Add<RewriteProfanity>()
				.Add<GuardAgainstDoublePost>()
				.Finally(p => repository.Save(p));

			var newId = pipeline.Execute(post);

			return newId;
		}
	}
}
