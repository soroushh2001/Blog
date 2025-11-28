using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Blog.Web.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        [BindProperty]
        public LoginUserViewModel Login { get; set; } = new();

        public void OnGet(string? returnUrl = null)
        {
            if (returnUrl != null)
                Login.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CheckUserForLoginAsync(Login);
                switch (result)
                {
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmailOrUserNameAsync(Login.UserNameOrEmail);

                        var permissions = await _userService.GetUserPermissionTitlesAsync(user.Id);

                        var permissionClaims = permissions.Select(p => new Claim(ClaimTypes.Role, p)).ToList();

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                        };

                        claims.AddRange(permissionClaims);

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = Login.RememberMe
                        };
                        await HttpContext.SignInAsync(principal, properties);

                        TempData[ToastrMessages.SuccessMessage] = "خوش آمدید";
                        return !string.IsNullOrEmpty(Login.ReturnUrl)
                            ? Redirect(Login.ReturnUrl)
                            : Redirect("/");

                    case LoginUserResult.NotFound:
                        TempData[ToastrMessages.ErrorMessage] = "کاربری با این مشخصات یافت نشد.";
                        break;
                    case LoginUserResult.Banned:
                        TempData[ToastrMessages.WarningMessage] = "حساب کاربری شما مسدود شده است.";
                        break;
                    case LoginUserResult.NotActived:
                        TempData[ToastrMessages.InfoMessage] = "لطفا حساب کاربری خود را فعال نمایید.";
                        break;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData[ToastrMessages.SuccessMessage] = "خروج با موفقیت انجام شد";
            return Redirect("/");
        }

    }
}
