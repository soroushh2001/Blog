using Blog.Application.Extensions;
using Blog.Application.Services.Interfaces;
using Blog.Application.Statics;
using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;
using Blog.Data.Repositories.Implementations;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using Resume.Utilities;

namespace Blog.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        #endregion

        public async Task<RegisterUserResult> RegisterUserAsync(RegisterUserViewModel register)
        {
            var checkUserName = await _userRepository.CheckUserNameExistedAsync(register.UserName);
            if (checkUserName)
                return RegisterUserResult.UserNameExisted;
            var checkUserEmail = await _userRepository.CheckEmailExistedAsync(register.Email.Trim().ToLower());
            if (checkUserEmail)
                return RegisterUserResult.EmailExisted;

            var newUser = new User
            {
                AvatarName = "default.png",
                Email = register.Email.Trim().ToLower(),
                EmailActivationCode = Guid.NewGuid().ToString("N"),
                UserName = register.UserName,
                Password = PasswordHelper.HashPassword(register.Password)
            };

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            await _userRepository.AddRoleToUserAsync(new UserRole
            {
                RoleId = 2,
                UserId = newUser.Id
            });
            await _userRepository.SaveChangesAsync();

            var domain = _configuration.GetValue<string>("SiteSettings:Domain");
            var callBackUrl = $"{domain}register?handler=ConfirmEmail&code={newUser.EmailActivationCode}";
            register.Email.SendEmail("ایمیل فعال سازی", $"<a href='{callBackUrl}'>فعالسازی</a>");

            return RegisterUserResult.Success;
        }

        public async Task<LoginUserResult> CheckUserForLoginAsync(LoginUserViewModel login)
        {
            var user = login.UserNameOrEmail.Contains("@")
                ? await _userRepository.GetByEmailAsync(login.UserNameOrEmail)
                : await _userRepository.GetByUserNameAsync(login.UserNameOrEmail);
            if (user == null)
                return LoginUserResult.NotFound;
            if (!PasswordHelper.VerifyPassword(login.Password, user.Password))
                return LoginUserResult.NotFound;
            if (user.IsBanned)
                return LoginUserResult.Banned;
            if (!user.IsEmailConfirmed)
                return LoginUserResult.NotActived;
            return LoginUserResult.Success;
        }

        public async Task<User?> GetUserByEmailOrUserNameAsync(string userNameOrEmail)
        {
            return userNameOrEmail.Contains("@")
                ? await _userRepository.GetByEmailAsync(userNameOrEmail.Trim().ToLower())
                : await _userRepository.GetByUserNameAsync(userNameOrEmail);
        }

        public async Task<List<string>> GetUserPermissionTitlesAsync(int userId)
        {
            return await _userRepository.GetUserPermissionTitlesAsync(userId);
        }

        public async Task<bool> ActiveUserAsync(string code)
        {
            var user = await _userRepository.GetByEmailAciveCodeAsync(code);
            if (user == null || user.IsBanned) return false;
            user.IsEmailConfirmed = true;
            user.EmailActivationCode = Guid.NewGuid().ToString("N");
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string emailOrUsername)
        {
            var user = await GetUserByEmailOrUserNameAsync(emailOrUsername);
            if (user == null || user.IsBanned) return false;
            var domain = _configuration.GetValue<string>("SiteSettings:Domain");
            var callBackUrl = $"{domain}reset-pass?code={user.EmailActivationCode}";
            user.Email.SendEmail("فراموشی کلمه عبور", $"<a href='{callBackUrl}'>فراموشی کلمه عبور</a>");
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string code)
        {
            var user = await _userRepository.GetByEmailAciveCodeAsync(code);
            if (user == null || user.IsBanned)
                return false;

            user.Password = PasswordHelper.HashPassword(user.Password);
            user.IsEmailConfirmed = true;
            user.EmailActivationCode = Guid.NewGuid().ToString("N");
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckUserForResetPasswordAsync(string code)
        {
            var user = await _userRepository.GetByEmailAciveCodeAsync(code);
            if (user.IsBanned || user.IsDeleted || user == null)
                return false;
            return true;
        }

        public async Task<FilterUsersViewModel> FilterUsersAsync(FilterUsersViewModel filter)
        {
            var query = _userRepository.GetQueryable();
            if (filter.Search != null)
            {
                query = filter.Search.Contains("@")
                    ? query.Where(q => q.Email.Contains(filter.Search))
                    : query.Where(q => q.UserName == filter.Search);
            }
            if(filter.RoleId != null)
            {
                query = query.Where(q => q.UserRoles.Any(q => q.RoleId == filter.RoleId));
            }

            switch (filter.Status)
            {
                case UserStatus.All:
                    break;
                case UserStatus.Active:
                    query = query.Where(q => q.IsEmailConfirmed);
                    break;
                case UserStatus.Banned:
                    query = query.Where(q => q.IsBanned);
                    break;
                case UserStatus.NotActive:
                    query = query.Where(q => !q.IsEmailConfirmed);
                    break;
            }

            switch (filter.OrderBy)
            {
                case UserOrderBy.Date_Descending:
                    query = query.OrderByDescending(q => q.CreatedAt);
                    break;
                case UserOrderBy.Date_Ascending:
                    query = query.OrderBy(q => q.CreatedAt);
                    break;
            }

            var items = query.
                Include(q=> q.UserRoles)
                .ThenInclude(q=> q.Role).Select(q => new UserViewModel
            {
                Id = q.Id,
                IsActive = q.IsEmailConfirmed,
                RegisterDate = q.CreatedAt,
                IsBanned = q.IsBanned,
                Roles = q.UserRoles.Where(ur => ur.UserId == q.Id).Select(r => r.Role.PersianTitle).ToList(),
                Username = q.UserName,
                Email = q.Email
            });

            await filter.SetPagingAsync(items);
            return filter;
        }

        public async Task<bool> ChangeUserPasswordInAdminAsync(ChangeUserPasswordinAdminViewModel change)
        {
            var user = await _userRepository.GetByIdAsync(change.UserId);
            if (user == null)
                return false;

            user.Password = PasswordHelper.HashPassword(change.Password);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<ManageUserRolesViewModel?> GetSelectedUserRolesForManageAsync(int userId)
        {
            var roles = await _userRepository.GetAllSelectedRoleIdsAsync(userId);
            if (roles == null)
                return null;
            return new()
            {
                SelectedRoles = roles,
                UserId = userId
            };
        }

        public async Task ManageUserSelectedRolesAsync(ManageUserRolesViewModel manage)
        {
            await _userRepository.DeleteRolesFromUserAsync(manage.UserId);
            await _userRepository.SaveChangesAsync();
            await _userRepository.AddRolesToUserAsync(
                manage.SelectedRoles.Select(x => new UserRole
                {
                    RoleId = x,
                    UserId = manage.UserId
                }).ToList());
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> ToggleUserActivationStatusAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null) return false;
            user.IsEmailConfirmed = !user.IsEmailConfirmed;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleUserBanStatusAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null) return false;
            user.IsBanned = !user.IsBanned;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<EditUserInProfile?> GetUserForEditInProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;
            return new EditUserInProfile
            {
                CurrentAvatar = user.AvatarName!,
                UserName = user.UserName
            };
        }

        public async Task<EditUserInProfileResult> EditUserInProfileAsync(int userId, EditUserInProfile edit)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return EditUserInProfileResult.UserNotFound;
            if (edit.UserName != null)
            {
                var checkUserName = await _userRepository.CheckUserNameExistedAsync(edit.UserName,user.Id);
                if (checkUserName)
                    return EditUserInProfileResult.UserNameExists;
                user.UserName = edit.UserName;
            }
            if(edit.NewAvatar != null)
            {
                var imageName = edit.NewAvatar.FileNameGenerator();
                var upResult = await edit.NewAvatar.UploadImage(imageName,PathTools.UserAvatarOrgServerPath,PathTools.UserAvatarThumbServerPath);
                if(!upResult)
                    return EditUserInProfileResult.InvalidImage;
                user.AvatarName.DeleteImage(PathTools.UserAvatarOrgServerPath,PathTools.UserAvatarThumbServerPath);
                user.AvatarName = imageName;
            }
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return EditUserInProfileResult.Success;
        }

        public async Task<bool> ChangePasswordInUserPanelAsync(int userId, ChangePasswordInUserPanelViewModel change)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var checkPassword = PasswordHelper.VerifyPassword(change.OldPassword, user!.Password);
            if (checkPassword)
            {
                user.Password = PasswordHelper.HashPassword(change.Password);
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
