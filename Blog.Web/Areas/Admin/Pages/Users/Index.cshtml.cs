using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public IndexModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        #endregion

        [BindProperty(SupportsGet = true)]
        public FilterUsersViewModel Filter { get; set; } = new();
        public List<SelectListItem> RolesSelectList { get; set; } = new();

        public async Task OnGetAsync()
        {
            Filter.TakeEntity = 10;
            Filter = await _userService.FilterUsersAsync(Filter);
            var roles = await _roleService.GetAllRolesAsync(false);
            RolesSelectList = roles.Select(x => new SelectListItem { Text = x.PersianTitle,Value = x.Id.ToString()}).ToList();
        }

        public async Task<IActionResult> OnGetToggleUserActivationStatusAsync(int id)
        {
            var result = await _userService.ToggleUserActivationStatusAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        public async Task<IActionResult> OnGetToggleUserBanStatusAsync(int id)
        {
            var result = await _userService.ToggleUserBanStatusAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

    }
}
