using Blog.Data.Context;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Constructor

        private readonly BlogDbContext _context;
        public CategoryRepository(BlogDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public IQueryable<Category> GetAllQueryable()
        {
            return _context.Categories.Include(c => c.CategoryColor).AsQueryable();
        }

        public async Task<bool> CheckTitleExistedAsync(string title, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Title == title && c.Id != id);
        }

        public async Task<bool> CheckSlugExistedAsync(string slug, int? id = null)
        {
            return await _context.Categories.AnyAsync(c => c.Slug == slug && c.Id != id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryColor>> GetAllCategoryColorsAsync()
        {
            return await _context.CategoryColors.ToListAsync();
        }
    }
}
