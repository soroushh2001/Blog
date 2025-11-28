using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Roles
{
    public class EditModel : PageModel
    {
        #region Constructor

        private readonly IRoleService _roleService;
        public EditModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        [BindProperty]
        public EditRoleViewModel Edit { get; set; } = null!;
        public List<Permission> Permissions { get; set; } = null!;


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Edit = await _roleService.GetRoleForEditAsync(id);
            
            if(Edit == null)
            {
                return NotFound();
            }

            Permissions = await _roleService.GetAllPermissionsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.EditRoleAsync(Edit);
                switch (result)
                {
                    case EditRoleResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        return RedirectToPage("Index");
                    case EditRoleResult.InvalidPersianTitle:
                        ModelState.AddModelError(string.Empty, "عنوان فارسی وارد شده ثبت شده است!");
                        break;
                    case EditRoleResult.InvalidEnglishTitle:
                        ModelState.AddModelError(string.Empty, "عنوان انگلیسی وارد شده ثبت شده است!");
                        break;
                }
              
            }

            Permissions = await _roleService.GetAllPermissionsAsync();
            
            return Page();
        }
    }
}
