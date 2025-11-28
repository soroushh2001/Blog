using Blog.Data.Entites.User;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
        IQueryable<Role> GetAllQueryable();
        Task<bool> CheckPersianTitleExistedAsync(string persainTitle,int? id = null);
        Task<bool> CheckEnglishTitleExistedAsync(string englishTitle,int? id = null);
        Task AddAsync(Role role);
        void Update(Role role);
        void Delete(Role role);
        Task SaveChangesAsync();
        Task<List<Permission>> GetAllPermissionsAsync();
        Task AddPermissionsToRoleAsync(List<RolePermission> rolePermissions);
        Task RemovePermissionsFromRoleAsync(int roleId);
        Task<List<int>> GetAllSelectedPermissionIdsAsync(int roleId);
    }
}
