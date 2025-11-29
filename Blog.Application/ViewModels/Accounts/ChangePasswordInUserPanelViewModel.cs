using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class ChangePasswordInUserPanelViewModel
    {
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string OldPassword { get; set; } = null!;

        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Password { get; set; } = null!;

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        [Compare(nameof(Password), ErrorMessage = "کلمه عبور و تکرار آن با یکدیگر مغایرت دارند.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
