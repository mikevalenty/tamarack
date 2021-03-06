﻿using System;
using Tamarack.Example.Pipeline.BlogEngine.Filters;
using Tamarack.Pipeline;

namespace Tamarack.Example.Pipeline.BlogEngine
{
	public class BlogEngine
	{
		private readonly IBlogRepository repository;

		public BlogEngine(IBlogRepository repository)
		{
			this.repository = repository;
		}

		public int Submit(Post post)
		{
			var pipeline = new Pipeline<Post, int>()
				.Add(new CanoncalizeHtml())
				.Add(new StripMaliciousTags())
				.Add(new RemoveJavascript())
				.Add(new RewriteProfanity())
				.Add(new GuardAgainstDoublePost())
				.Finally(p => repository.Save(p));

			var newId = pipeline.Execute(post);

			return newId;
		}
	}
}
