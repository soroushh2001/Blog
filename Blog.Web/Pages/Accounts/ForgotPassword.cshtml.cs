using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Accounts
{
    public class ForgotPasswordModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public ForgotPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [BindProperty]
        public ForgotPasswordViewModel Forgot { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ForgotPasswordAsync(Forgot.Email);
                if (result)
                {
                    TempData[ToastrMessages.SuccessMessage] = "ایمیلی حاوی لینک تغییر کلمه عبور برای شما ارسال شد!";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "کاربری با این ایمیل یافت نشد!");

                }
            }
            return Page();
        }
    }
}
