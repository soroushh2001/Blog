using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Roles
{
    public class CreateModel : PageModel
    {
        #region Constructor

        private readonly IRoleService _roleService;
        public CreateModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        [BindProperty]
        public CreateRoleViewModel Create { get; set; } = null!;

        public List<Permission> Permissions { get; set; } = null!;

        public async Task OnGetAsync()
        {
            Permissions = await _roleService.GetAllPermissionsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.CreateRoleAsync(Create);
                switch (result)
                {
                    case CreateRoleResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case CreateRoleResult.InvalidPersianTitle:
                        ModelState.AddModelError(string.Empty, "عنوان فارسی وارد شده ثبت شده است!");
                        break;
                    case CreateRoleResult.InvalidEnglishTitle:
                        ModelState.AddModelError(string.Empty, "عنوان انگلیسی وارد شده ثبت شده است!");
                        break;
                }
            }

            Permissions = await _roleService.GetAllPermissionsAsync();


            return Page();
        }
    }
}