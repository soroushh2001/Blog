using Blog.Data.Entites.Blog;

namespace Blog.Data.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> GetQueryable();
        Task AddAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
