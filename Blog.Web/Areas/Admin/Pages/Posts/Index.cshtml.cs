using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Posts;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Posts
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        public IndexModel(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        #endregion

        [BindProperty(SupportsGet = true)]
        public FilterPostsViewModel Filter { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }

        public async Task OnGetAsync()
        {
            Filter.TakeEntity = 1;
            Filter = await _postService.FilterPostsAsync(Filter);
            var categories = await _categoryService.GetAllCategoriesAsync(IsDeleted: false);
            CategoriesSelectList = categories.Select(c => new SelectListItem 
            {
                Text = c.Title,
                Value = c.Slug,
            }).ToList();
        }

        public async Task<IActionResult> OnGetTagSearchAsync(string term)
        {
            var tags = await _postService.GetTagTitlesAsync(term);
            return new JsonResult(tags);
        }

        public async Task<IActionResult> OnGetToggleDeleteAsync(int id)
        {
            var result = await _postService.ToggleDeletePostAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد") 
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

    }
}
