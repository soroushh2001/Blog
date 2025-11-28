using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Categories
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Title { get; set; } = null!;

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Slug { get; set; } = null!;

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryColorId { get; set; }

        public string Image { get; set; } = null!;

        [Display(Name = "عکس")]

        public IFormFile? NewImage { get; set; }
    }

    public enum EditCategoryResult
    {
        Success,
        InvalidSlug,
        InvalidTitle,
        NotFound,
        InvalidImage
    }
}
