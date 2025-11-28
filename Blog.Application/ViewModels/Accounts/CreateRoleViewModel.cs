using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class CreateRoleViewModel
    {
        [Display(Name = "عنوان فارسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string PersianTitle { get; set; } = null!;

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string EnglishTitle { get; set; } = null!;

        [Display(Name = "مجوزها")]
        public List<int>? PermissionIds { get; set; } 
    }
    public enum CreateRoleResult
    {
        Success,
        InvalidPersianTitle,
        InvalidEnglishTitle
    }
}
