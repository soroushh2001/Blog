using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Categories;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.Admin.Pages.Categories
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public List<CategoryViewModel> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategoriesAsync();
        }

        public async Task<IActionResult> OnGetToggleDeleteAsync(int id)
        {
            var result = await _categoryService.ToggleDeleteCategoryAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد") 
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }


    }
}
