using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Posts;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Posts
{
    [Authorize(Roles = PermissionConstants.PostList)]
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

        

    }
}
