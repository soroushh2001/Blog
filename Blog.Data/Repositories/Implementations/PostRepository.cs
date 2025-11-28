using Blog.Data.Context;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        #region Constructor

        private readonly BlogDbContext _context;
        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        #endregion

        public IQueryable<Post> GetAllQueryable()
        {
            return _context.Posts
                .Include(x => x.Category)
                .ThenInclude(x => x.CategoryColor)
                .AsQueryable();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public void Update(Post post)
        {
            _context.Update(post);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Tag?> CheckTagExisted(string tag)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Title == tag);
        }

        public async Task AddTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }

        public async Task AddPostTagAsync(PostTag postTag)
        {
            await _context.PostTags.AddAsync(postTag);
        }

        public async Task<List<string>> GetTagTitlesAsync(string term)
        {
            return await _context.Tags
                   .Where(t => t.Title.Contains(term))
                   .Select(t => t.Title)
                   .ToListAsync();
        }

        public void RemoveTagsFromPostByPostId(int postId)
        {
            _context.PostTags
                .RemoveRange(_context.PostTags.Where(pt => pt.PostId == postId));
        }

        public async Task<List<string>> GetSelectedTagTitlesAsync(int postId)
        {
            return await _context.PostTags
                .Where(pt => pt.PostId == postId)
                .Select(pt => pt.Tag.Title)
                .ToListAsync();
        }

        public async Task<List<Post>> GetLatestAsync(int take)
        {
            return await _context.Posts.Where(p => p.Status == PostStatus.Published).
                           Include(p => p.Category).
                           ThenInclude(pc => pc.CategoryColor).
                             OrderByDescending(p => p.UpdatedAt).Take(take).ToListAsync();
        }

        public async Task<Post?> GetBySlygAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .ThenInclude(p => p.CategoryColor)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
