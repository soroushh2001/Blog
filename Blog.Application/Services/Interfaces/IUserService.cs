using Blog.Application.ViewModels.Accounts;
using Blog.Data.Entites.User;

namespace Blog.Application.Services.Interfaces
{
    public interface IUserService 
    {
        Task<RegisterUserResult> RegisterUserAsync(RegisterUserViewModel register);
        Task<LoginUserResult> CheckUserForLoginAsync(LoginUserViewModel login);
        Task<User?> GetUserByEmailOrUserNameAsync(string userNameOrEmail);
        Task<List<string>> GetUserPermissionTitlesAsync(int userId);
        Task<bool> ActiveUserAsync(string code);
        Task<bool> ForgotPasswordAsync(string emailOrUsername);
        Task<bool> ResetPasswordAsync(string code);
        Task<bool> CheckUserForResetPasswordAsync(string code);
        Task<FilterUsersViewModel> FilterUsersAsync(FilterUsersViewModel filter);
        Task<bool> ChangeUserPasswordInAdminAsync(ChangeUserPasswordinAdminViewModel change);
        Task<ManageUserRolesViewModel?> GetSelectedUserRolesForManageAsync(int userId);
        Task ManageUserSelectedRolesAsync(ManageUserRolesViewModel manage);
        Task<bool> ToggleUserActivationStatusAsync(int userId);
        Task<bool> ToggleUserBanStatusAsync(int userId);
    }
}
