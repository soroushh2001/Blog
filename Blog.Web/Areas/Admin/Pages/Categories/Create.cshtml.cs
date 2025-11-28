using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Categories;
using Blog.Data.Entites.Blog;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Pages.Categories
{
    public class CreateModel : PageModel
    {
        #region Constructor

        private readonly ICategoryService _categoryService;
        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        [BindProperty]
        public CreateCategoryViewModel Create { get; set; } 

        public List<SelectListItem> ColorsSelectListItem { get; set; } 

        public async Task OnGetAsync()
        {
            var colors = await _categoryService.GetAllCategoriesColorAsync();
            ColorsSelectListItem = colors.Select(c => new SelectListItem
            {
                Text = c.Color,
                Value = c.Id.ToString()
            }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateCategoryAsync(Create);
                switch (result)
                {
                    case CreateCategoryResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case CreateCategoryResult.InvalidSlug:
                        ModelState.AddModelError(string.Empty, "اسلاگ وجود دارد!");
                        break;
                    case CreateCategoryResult.InvalidTitle:
                        ModelState.AddModelError(string.Empty, "عنوان وجود دارد!");
                        break;
                    case CreateCategoryResult.InvalidImage:
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
