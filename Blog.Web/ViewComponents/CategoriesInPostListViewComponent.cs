using Blog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class CategoriesInPostListViewComponent : ViewComponent
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public CategoriesInPostListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryService.GetAllCategoriesAsync(false));
        }
    }
}
