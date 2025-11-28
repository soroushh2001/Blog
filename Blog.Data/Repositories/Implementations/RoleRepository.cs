using Blog.Data.Context;
using Blog.Data.Entites.User;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        #region Constructor

        private readonly BlogDbContext _context;
        public RoleRepository(BlogDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public IQueryable<Role> GetAllQueryable()
        {
            return _context.Roles.Include(x=> x.RolePermissions)
                .ThenInclude(x=> x.Permission).AsQueryable();
        }

        public async Task<bool> CheckPersianTitleExistedAsync(string persainTitle, int? id = null)
        {
            return await _context.Roles.AnyAsync(x => x.PersianTitle == persainTitle && x.Id != id);
        }

        public async Task<bool> CheckEnglishTitleExistedAsync(string englishTitle, int? id = null)
        {
            return await _context.Roles.AnyAsync(x => x.EnglishTitle == englishTitle && x.Id != id);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task AddPermissionsToRoleAsync(List<RolePermission> rolePermissions)
        {
            await _context.RolePermissions.AddRangeAsync(rolePermissions);
        }

        public async Task RemovePermissionsFromRoleAsync(int roleId)
        {
           _context.RemoveRange(await _context.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync());
        }

        public async Task<List<int>> GetAllSelectedPermissionIdsAsync(int roleId)
        {
            return await _context.RolePermissions.Where(x => x.RoleId == roleId).Select(x => x.PermissionId).ToListAsync();
        }

       
    }
}
