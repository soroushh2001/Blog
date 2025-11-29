using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.UserPanel.Pages
{
    [Authorize]
    public class EditProfileModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public EditProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [BindProperty]
        public EditUserInProfile? Edit { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Edit = await _userService.GetUserForEditInProfileAsync(User.GetCurrentUserId());
            if (Edit == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUserInProfileAsync(User.GetCurrentUserId(), Edit!);
                switch (result)
                {
                    case EditUserInProfileResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "عملیات با موفقیت انجام شد";
                        await HttpContext.SignOutAsync();
                        return Redirect("/login");
                    case EditUserInProfileResult.UserNotFound:
                        return NotFound();
                    case EditUserInProfileResult.UserNameExists:
                        ModelState.AddModelError(string.Empty, "این نام کاربری قبلا انتخاب شده است");
                        break;
                    case EditUserInProfileResult.InvalidImage:
                        ModelState.AddModelError(string.Empty, "تصویر انتخاب شده معتبر نمی باشد");
                        break;
                }
            }

            return Page();
        }
    }
}
