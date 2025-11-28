using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;

namespace Blog.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRolesAsync(bool? isDeleted = null);
        Task<CreateRoleResult> CreateRoleAsync(CreateRoleViewModel role);
        Task<EditRoleViewModel?> GetRoleForEditAsync(int id);
        Task<EditRoleResult> EditRoleAsync(EditRoleViewModel edit);
        Task AddPermissionsToRoleAsync(List<int> permissionIds, int roleId);
        Task<List<Permission>> GetAllPermissionsAsync();
        Task<bool> RemoveRoleAsync(int roleId);
    }
}
