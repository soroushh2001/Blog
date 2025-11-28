using Blog.Data.Entites.Blog;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IPostRepository 
    {
        IQueryable<Post> GetAllQueryable();
        Task<Post?> GetByIdAsync(int id);
        Task AddAsync(Post post);
        void Update(Post post);
        Task SaveChangesAsync();
        Task<Tag?> CheckTagExisted(string tag);
        Task AddTagAsync(Tag tag);
        Task AddPostTagAsync(PostTag postTag);
        Task<List<string>> GetTagTitlesAsync(string term);
        void RemoveTagsFromPostByPostId(int postId);
        Task<List<string>> GetSelectedTagTitlesAsync(int postId);
        Task<List<Post>> GetLatestAsync(int take);
        Task<Post?> GetBySlygAsync(string slug);
    }
}
