using Blog.Data.Context;
using Blog.Data.Entites.User;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        #region Constructor

        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> CheckUserNameExistedAsync(string userName, int? userId = null)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName && u.Id != userId);
        }

        public async Task<bool> CheckEmailExistedAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Trim().ToLower() == email);
        }

        public async Task AddAsync(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddRolesToUserAsync(List<UserRole> userRoles)
        {
            await _context.UserRoles.AddRangeAsync(userRoles);
        }

        public async Task DeleteRolesFromUserAsync(int userId)
        {
            _context.UserRoles.RemoveRange(await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync());
        }

        public async Task AddRoleToUserAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetByEmailAciveCodeAsync(string code)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmailActivationCode == code);
        }

        public async Task<List<string>> GetUserPermissionTitlesAsync(int userId)
        {
            return await _context.Users.Where(u => u.Id == userId)
                .SelectMany(u => u.UserRoles)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.EnglishTitle)
                .Distinct()
                .ToListAsync();
        }

        public IQueryable<User> GetQueryable()
        {
            return _context.Users.AsQueryable();
        }

        public async Task<List<int>> GetAllSelectedRoleIdsAsync(int userId)
        {
            return await _context.UserRoles.Where(u => u.UserId == userId).Select
                (ur => ur.RoleId).ToListAsync();
        }
    }
}
