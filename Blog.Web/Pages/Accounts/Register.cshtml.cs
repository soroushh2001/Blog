using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;
        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [BindProperty]
        public RegisterUserViewModel Register { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(Register);
                switch (result)
                {
                    case RegisterUserResult.Success:
                        TempData[ToastrMessages.SuccessMessage] = "ثبت نام با موفقیت انجام شد";
                        TempData[ToastrMessages.InfoMessage] = "ایمیلی حاوی لینک فعالسازی برای شما ارسال شد";
                        break;
                    case RegisterUserResult.UserNameExisted:
                        break;
                    case RegisterUserResult.EmailExisted:
                        break;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnGetConfirmEmailAsync(string code)
        {
            var result = await _userService.ActiveUserAsync(code);
            if (!result)
                return NotFound();
            TempData[ToastrMessages.SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد";
            return RedirectToPage("Login");
        }

    }
}
