using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace Blog.Web.Areas.UserPanel.Pages
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;
        public ChangePasswordModel(IUserService userService)
        {

            _userService = userService;
        }

        #endregion

        [BindProperty]
        public ChangePasswordInUserPanelViewModel Change { get; set; } = null!;

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePasswordInUserPanelAsync(User.GetCurrentUserId(), Change);
                if (result)
                {
                    TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                    await HttpContext.SignOutAsync();
                    return Redirect("/login");
                }
                ModelState.AddModelError(string.Empty, "کلمه عبور فعلی نادرست است");

            }

            return Page();
        }
    }
}
