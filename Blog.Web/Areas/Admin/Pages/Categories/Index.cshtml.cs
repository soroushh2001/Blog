using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Categories;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.Admin.Pages.Categories
{
    [Authorize(Roles = PermissionConstants.CategoriesList)]
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

    }
}
