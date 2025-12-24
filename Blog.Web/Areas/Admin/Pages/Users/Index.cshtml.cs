using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Areas.Admin.Pages.Users
{
    [Authorize(Roles = PermissionConstants.UsersList)]
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

    }
}
