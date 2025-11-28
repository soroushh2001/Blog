using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.Admin.Pages.Users
{
    public class ChangeUserPasswordModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;

        public ChangeUserPasswordModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public ChangeUserPasswordinAdminViewModel Change { get; set; } = new();

        public void OnGet(int userId)
        {
            Change.UserId = userId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangeUserPasswordInAdminAsync(Change);
                if (result)
                {
                    TempData[ToastrMessages.ErrorMessage] = "عملیات با موفقیت انجام شد";
                    return RedirectToPage("Index");
                }
                TempData[ToastrMessages.ErrorMessage] = "عملیا با شکست مواجه شد";

            }
            return Page();
        }
    }
}
