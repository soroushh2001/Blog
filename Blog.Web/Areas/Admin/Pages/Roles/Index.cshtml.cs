using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Pages.Roles
{
    [Authorize(Roles = PermissionConstants.RolesList)]
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IRoleService _roleService;

        public IndexModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        public List<RoleViewModel> Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _roleService.GetAllRolesAsync();
        }

    }
}
