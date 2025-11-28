using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Application.Services.Implementation
{
    public class RoleService : IRoleService
    {
        #region Constructor

        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        #endregion

        public async Task<List<RoleViewModel>> GetAllRolesAsync(bool? isDeleted = null)
        {
            var query = _roleRepository.GetAllQueryable();

            if (isDeleted != null)
            {
                query = query.Where(r => r.IsDeleted == isDeleted);
            }

            return await query.Select(r => new RoleViewModel
            {
                EnglishTitle = r.EnglishTitle,
                Id = r.Id,
                Permissions = r.RolePermissions.Select(rp => rp.Permission).ToList(),
                PersianTitle = r.PersianTitle

            }).ToListAsync();
        }
        public async Task<CreateRoleResult> CreateRoleAsync(CreateRoleViewModel role)
        {
            var checkPersianTitle = await _roleRepository.CheckPersianTitleExistedAsync(role.PersianTitle);
            if (checkPersianTitle)
                return CreateRoleResult.InvalidPersianTitle;

            var checkEnglishTitle = await _roleRepository.CheckEnglishTitleExistedAsync(role.EnglishTitle);
            if (checkEnglishTitle)
                return CreateRoleResult.InvalidEnglishTitle;

            var newRole = new Role
            {
                PersianTitle = role.PersianTitle,
                EnglishTitle = role.EnglishTitle,
            };

            await _roleRepository.AddAsync(newRole);
            await _roleRepository.SaveChangesAsync();

            if (role.PermissionIds != null)
            {
                await AddPermissionsToRoleAsync(role.PermissionIds, newRole.Id);
            }

            return CreateRoleResult.Success;

        }

        public async Task<EditRoleViewModel?> GetRoleForEditAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                return null;
            return new EditRoleViewModel
            {
                Id = role.Id,
                PermissionIds = await _roleRepository.GetAllSelectedPermissionIdsAsync(role.Id),
                EnglishTitle = role.EnglishTitle,
                PersianTitle = role.PersianTitle
            };
        }

        public async Task<EditRoleResult> EditRoleAsync(EditRoleViewModel edit)
        {
            var role = await _roleRepository.GetByIdAsync(edit.Id);
            if (role == null)
                return EditRoleResult.NotFound;

            var checkPersianTitle = await _roleRepository.CheckPersianTitleExistedAsync(edit.PersianTitle, edit.Id);
            if (checkPersianTitle)
                return EditRoleResult.InvalidPersianTitle;

            var checkEnglishTitle = await _roleRepository.CheckEnglishTitleExistedAsync(edit.EnglishTitle, edit.Id);
            if (checkEnglishTitle)
                return EditRoleResult.InvalidEnglishTitle;

            role.PersianTitle = edit.PersianTitle;
            role.EnglishTitle = edit.EnglishTitle;
            _roleRepository.Update(role);
            await _roleRepository.SaveChangesAsync();
            if (edit.PermissionIds != null)
            {
                await _roleRepository.RemovePermissionsFromRoleAsync(role.Id);
                await _roleRepository.SaveChangesAsync();
                await AddPermissionsToRoleAsync(edit.PermissionIds, role.Id);
            }
            return EditRoleResult.Success;
        }

        public async Task AddPermissionsToRoleAsync(List<int> permissionIds, int roleId)
        {
            var rolePermissions = permissionIds.Select(x => new RolePermission
            {
                RoleId = roleId,
                PermissionId = x,
            }).ToList();
            await _roleRepository.AddPermissionsToRoleAsync(rolePermissions);
            await _roleRepository.SaveChangesAsync();
        }

        public async Task<List<Permission>> GetAllPermissionsAsync()
        {
            return await _roleRepository.GetAllPermissionsAsync();
        }

        public async Task<bool> RemoveRoleAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null) return false;
            _roleRepository.Delete(role);
            await _roleRepository.SaveChangesAsync();
            return true;
        }
    }
}
