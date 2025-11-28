using Blog.Data.Entites.User;

namespace Blog.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CheckUserNameExistedAsync(string userName);
        Task<bool> CheckEmailExistedAsync(string email);
        Task AddAsync(User user);
        void Update(User user);
        Task SaveChangesAsync();
        Task AddRolesToUserAsync(List<UserRole> userRoles);
        Task DeleteRolesFromUserAsync(int userId);
        Task AddRoleToUserAsync(UserRole userRole);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(int userId);
        Task<User?> GetByEmailAciveCodeAsync(string code);
        Task<List<string>> GetUserPermissionTitlesAsync(int userId);
        IQueryable<User> GetQueryable();
        Task<List<int>> GetAllSelectedRoleIdsAsync(int userId);
    }
}
