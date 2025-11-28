using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Application.ViewModels.Accounts
{
    public class EditRoleViewModel
    {
        public int Id { get; set; }

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

    public enum EditRoleResult
    {
        Success,
        InvalidPersianTitle,
        InvalidEnglishTitle,
        NotFound
    }
}
