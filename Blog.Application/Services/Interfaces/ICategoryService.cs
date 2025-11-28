using Blog.Application.ViewModels.Categories;
using Blog.Data.Entites.Blog;

namespace Blog.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync(bool? IsDeleted = null);
        Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel category);
        Task<EditCategoryViewModel?> GetCategoryForEditAsync(int id);
        Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel edit);
        Task<List<CategoryColor>> GetAllCategoriesColorAsync();
        Task<bool> ToggleDeleteCategoryAsync(int id);
    }
}
