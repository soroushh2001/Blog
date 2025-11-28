using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Accounts
{
    public class ResetPasswordModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public ResetPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [BindProperty]
        public ResetPasswordViewModel Reset { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string code)
        {
            if (code == null)
                return NotFound();

            var result = await _userService.CheckUserForResetPasswordAsync(code);
            if (!result)
                return NotFound();

            Reset.EmailActiveCode = code;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(Reset.EmailActiveCode);
                if (!result)
                    return NotFound();
                TempData[ToastrMessages.SuccessMessage] = "کلمه عبور با موقیت تغییر کرد.";
                return RedirectToPage("Login");
            }
            return Page();
        }
    }
}
