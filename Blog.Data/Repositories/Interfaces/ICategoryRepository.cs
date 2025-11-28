using Blog.Data.Entites.Blog;

namespace Blog.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        IQueryable<Category> GetAllQueryable();
        Task<bool> CheckTitleExistedAsync(string title, int? id = null);
        Task<bool> CheckSlugExistedAsync(string slug, int? id = null);
        Task AddAsync(Category category);
        void Update(Category category);
        Task SaveChangesAsync();
        Task<List<CategoryColor>> GetAllCategoryColorsAsync();
    }
}
