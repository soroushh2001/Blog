using Blog.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
    #region Slider

    public class SliderViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IPostService _postService;

        public SliderViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _postService.GetLatestSliderAsync(5));
        }
    }

    #endregion

    #region NewestPosts

    public class NewestPostsViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IPostService _postService;

        public NewestPostsViewComponent(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _postService.GetLatestPostAsync(9);
            return View(model);
        }
    }

    #endregion

    #region CategoryList

    public class CategoryListViewComponent : ViewComponent
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _categoryService.GetAllCategoriesAsync(false));
        }
    }

    #endregion
}
