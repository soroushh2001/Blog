using Blog.Data.Context;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;

namespace Blog.Data.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        #region Constructor

        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context)
        {
            _context = context;
        }

        #endregion

        public IQueryable<Comment> GetQueryable()
        {
            return _context.Comments.AsQueryable();
        }

        public async Task AddAsync(Comment comment)
        {
           await _context.Comments.AddAsync(comment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
