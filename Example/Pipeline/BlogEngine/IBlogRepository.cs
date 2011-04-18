namespace Tamarack.Example.Pipeline.BlogEngine
{
	public interface IBlogRepository
	{
		int Save(Post post);
	}
}