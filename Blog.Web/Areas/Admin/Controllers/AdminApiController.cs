using Blog.Application.Services.Implementation;
using Blog.Application.Services.Interfaces;
using Blog.Web.Helpers;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/admin")]
    public class AdminApiController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public AdminApiController(ICategoryService categoryService, IPostService postService, IRoleService roleService, IUserService userService)
        {
            _categoryService = categoryService;
            _postService = postService;
            _roleService = roleService;
            _userService = userService;
        }

        #endregion

        #region Categories

        [Authorize(Roles = PermissionConstants.DeleteCategory)]
        [HttpPost("[action]")]
        public async Task<IActionResult> ToggleDeleteCategoryAsync(int id)
        {
            var result = await _categoryService.ToggleDeleteCategoryAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        #endregion

        #region Posts

        [HttpGet("[action]")]
        public async Task<IActionResult> TagSearchAsync(string term)
        {
            var tags = await _postService.GetTagTitlesAsync(term);
            return new JsonResult(tags);
        }

        [Authorize(Roles = PermissionConstants.DeletePost)]
        [HttpPost("[action]")]
        public async Task<IActionResult> ToggleDeletePostAsync(int id)
        {
            var result = await _postService.ToggleDeletePostAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        #endregion

        #region Roles

        [Authorize(Roles = PermissionConstants.DeleteRole)]
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var result = await _roleService.RemoveRoleAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        #endregion

        #region Users

        [Authorize(Roles = PermissionConstants.EditUser)]
        [HttpPost("[action]")]
        public async Task<IActionResult> ToggleUserActivationStatusAsync(int id)
        {
            var result = await _userService.ToggleUserActivationStatusAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        [Authorize(Roles = PermissionConstants.EditUser)]
        [HttpPost("[action]")]
        public async Task<IActionResult> ToggleUserBanStatusAsync(int id)
        {
            var result = await _userService.ToggleUserBanStatusAsync(id);
            return result ? JsonHelper.JsonResponse(200, "عملیات با موفقیت انجام شد")
                : JsonHelper.JsonResponse(404, "عملیات با شکست مواجه شد");
        }

        #endregion
    }
}
