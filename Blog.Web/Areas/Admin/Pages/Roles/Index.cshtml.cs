using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Pages.Roles
{
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

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var result = await _roleService.RemoveRoleAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }
    }
}
