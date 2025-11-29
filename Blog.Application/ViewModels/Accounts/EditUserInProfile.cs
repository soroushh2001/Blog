using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class EditUserInProfile 
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string? UserName { get; set; }

        public string CurrentAvatar { get; set; } = null!; 

        [Display(Name = "آواتار")]
        public IFormFile? NewAvatar { get; set; }
    }
    public enum EditUserInProfileResult
    {
        Success,
        UserNotFound,
        UserNameExists,
        InvalidImage
    }
}
