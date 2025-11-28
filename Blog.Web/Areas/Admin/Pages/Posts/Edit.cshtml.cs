using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Posts
{
    public class EditModel : PageModel
    {
        #region Constructor

        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public EditModel(IPostService postService,ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        #endregion

        [BindProperty]
        public EditPostViewModel Edit { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Edit = await _postService.GetPostForEditAsync(id);

            if (Edit == null)
                return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();
            CategoriesSelectList = categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _postService.EditPostAsync(Edit);
                switch (result)
                {
                    case EditPostResult.Success:
                        TempData["SuccessMessage"] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case EditPostResult.InvalidImage:
                        TempData["ErrorMessage"] = "فرمت تصویر انتخاب شده معتبر نمی باشد";
                        break;
                    case EditPostResult.NotFound:
                        return NotFound();
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
