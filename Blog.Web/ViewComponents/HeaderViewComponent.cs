using Blog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public HeaderViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync(IsDeleted: false);
            return View();
        }
    }
}
