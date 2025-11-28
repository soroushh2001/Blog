using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Categories;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Categories
{
    public class EditModel : PageModel
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        [BindProperty]
        public EditCategoryViewModel Edit { get; set;  }
        public List<SelectListItem> ColorsSelectListItem { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Edit = await _categoryService.GetCategoryForEditAsync(id);
            if(Edit == null) 
                return NotFound();

            var colors = await _categoryService.GetAllCategoriesColorAsync();
            ColorsSelectListItem = colors.Select(c => new SelectListItem
            {
                Text = c.Color,
                Value = c.Id.ToString()
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.EditCategoryAsync(Edit);
                switch (result)
                {
                    case EditCategoryResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case EditCategoryResult.InvalidSlug:
                        ModelState.AddModelError(string.Empty, "اسلاگ وجود دارد!");
                        break;
                    case EditCategoryResult.InvalidTitle:
                        ModelState.AddModelError(string.Empty, "عنوان وجود دارد!");
                        break;
                    case EditCategoryResult.NotFound:
                        return NotFound();
                    case EditCategoryResult.InvalidImage:
                        ModelState.AddModelError(string.Empty, "فرمت عکس وارد شده صحیح نمی باشد");
                        break;
                }
            }

            var colors = await _categoryService.GetAllCategoriesColorAsync();
            ColorsSelectListItem = colors.Select(c => new SelectListItem
            {
                Text = c.Color,
                Value = c.Id.ToString()
            }).ToList();

            return Page();
        }

    }
}
