using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Pages.Users
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public ManageUserRolesModel(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public List<RoleViewModel> Roles { get; set; } = null!;

        [BindProperty]
        public ManageUserRolesViewModel Manage { get; set; } = new();

        public async Task<IActionResult> OnGet(int id)
        {
            Manage = await _userService.GetSelectedUserRolesForManageAsync(id);
            if(Manage == null)
            {
                return NotFound();
            }
            Roles = await _roleService.GetAllRolesAsync(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _userService.ManageUserSelectedRolesAsync(Manage);
                TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";

                return RedirectToPage("Index");
            }
            Roles = await _roleService.GetAllRolesAsync(false);

            return Page();
        }
    }
}
