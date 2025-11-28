using Blog.Application.Extensions;
using Blog.Application.Services.Interfaces;
using Blog.Application.Statics;
using Blog.Application.ViewModels.Categories;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        #region Constructor

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync(bool? IsDeleted)
        {
            var query = _categoryRepository.GetAllQueryable();
            if (IsDeleted != null)
                query = query.Where(x => x.IsDeleted == IsDeleted);

            return await query.Select(q => new CategoryViewModel
            {
                BootstrapClassName = q.CategoryColor.BootstrapClassName,
                Title = q.Title,
                Slug = q.Slug,
                Id = q.Id,
                IsDeleted = q.IsDeleted,
                Image = q.Image
            }).ToListAsync();
        }

        public async Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel category)
        {
            category.Slug = category.Slug.GenerateSlug();

            var checkTitle = await _categoryRepository.CheckTitleExistedAsync(category.Title);
            if (checkTitle)
            {
                return CreateCategoryResult.InvalidTitle;
            }
            var checkSlug = await _categoryRepository.CheckSlugExistedAsync(category.Slug);
            if (checkSlug)
            {
                return CreateCategoryResult.InvalidSlug;
            }

            var newCategory = new Category
            {
                CategoryColorId = category.CategoryColorId,
                Slug = category.Slug,
                Title = category.Title,
            };

            var imageName = NameGenerator.FileNameGenerator(category.Image);
            var upResult = await category.Image.UploadImage(imageName, PathTools.CategoryOrgImageServerPath, PathTools.CategoryThumbImageServerPath);
            if (!upResult)
                return CreateCategoryResult.InvalidImage;

            newCategory.Image = imageName;

            await _categoryRepository.AddAsync(newCategory);
            await _categoryRepository.SaveChangesAsync();

            return CreateCategoryResult.Success;
        }

        public async Task<EditCategoryViewModel?> GetCategoryForEditAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return null;
            return new EditCategoryViewModel
            {
                CategoryColorId = category.CategoryColorId,
                Id = id,
                Title = category.Title,
                Image = category.Image,
                Slug = category.Slug,
            };
        }

        public async Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel edit)
        {
            edit.Slug = edit.Slug.GenerateSlug();

            var categoryToEdit = await _categoryRepository.GetByIdAsync(edit.Id);
            if (categoryToEdit == null)
                return EditCategoryResult.NotFound;

            var checkTitle = await _categoryRepository.CheckTitleExistedAsync(edit.Title, edit.Id);
            if (checkTitle)
            {
                return EditCategoryResult.InvalidTitle;
            }
            var checkSlug = await _categoryRepository.CheckSlugExistedAsync(edit.Slug, edit.Id);
            if (checkSlug)
            {
                return EditCategoryResult.InvalidSlug;
            }


            categoryToEdit.Title = edit.Title;
            categoryToEdit.Slug = edit.Slug;
            categoryToEdit.UpdatedAt = DateTime.Now;
            categoryToEdit.CategoryColorId = edit.CategoryColorId;

            if (edit.NewImage != null)
            {
                edit.Image.DeleteImage(PathTools.CategoryOrgImageServerPath, PathTools.CategoryThumbImageServerPath);
                var imageName = NameGenerator.FileNameGenerator(edit.NewImage);
                var upResult = await edit.NewImage.UploadImage(imageName, PathTools.CategoryOrgImageServerPath, PathTools.CategoryThumbImageServerPath);
                if (!upResult)
                {
                    return EditCategoryResult.InvalidImage;
                }

                categoryToEdit.Image = imageName;
            }

            _categoryRepository.Update(categoryToEdit);
            await _categoryRepository.SaveChangesAsync();

            return EditCategoryResult.Success;
        }

        public async Task<List<CategoryColor>> GetAllCategoriesColorAsync()
        {
            return await _categoryRepository.GetAllCategoryColorsAsync();
        }

        public async Task<bool> ToggleDeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) { return false; }
            category.IsDeleted = !category.IsDeleted;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            return true;
        }
    }
}
