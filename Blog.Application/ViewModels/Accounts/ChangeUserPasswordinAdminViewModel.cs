using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class ChangeUserPasswordinAdminViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Password { get; set; } = null!;
    }
}
