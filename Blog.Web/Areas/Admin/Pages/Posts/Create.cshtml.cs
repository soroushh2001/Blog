using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Categories;
using Blog.Application.ViewModels.Posts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Posts
{
    public class CreateModel : PageModel
    {
        #region Constructor

        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public CreateModel(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        #endregion

        [BindProperty]
        public CreatePostViewModel Create { get; set; }

        public List<SelectListItem> CategoriesSelectList { get; set; }

        public async Task OnGetAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync(); 
            CategoriesSelectList = categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _postService.CreatePostAsync(Create);
                switch (result)
                {
                    case CreatePostResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case CreatePostResult.InvalidImage:
                        TempData[ToastrMessages.ErrorMessage] = "فرمت تصویر انتخاب شده معتبر نمی باشد";
                        break;
                }
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            CategoriesSelectList = categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList(); 
            return Page();
        }
    }
}
