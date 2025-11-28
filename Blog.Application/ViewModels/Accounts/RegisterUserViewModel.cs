using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class RegisterUserViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        [RegularExpression(@"^[^@]+$", ErrorMessage = "نام کاربری نباید شامل @ باشد.")]
        public string UserName { get; set; } = null!;

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Email { get; set; } = null!;

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        public string Password { get; set; } = null!;

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد.")]
        [Compare(nameof(Password),ErrorMessage = "کلمه عبور و تکرار آن با یکدیگر مغایرت دارند.")]
        public string ConfirmPassword { get; set; } = null!;
    }

    public enum RegisterUserResult
    {
        Success,
        UserNameExisted,
        EmailExisted
    }
}
