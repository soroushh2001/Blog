using Blog.Data.Entites.Blog;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Posts
{
    public class EditPostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Title { get; set; } = null!;

        public string MainImage { get; set; } = null!;

        [Display(Name = "عکس اصلی")]
        public IFormFile? NewMainImage { get; set; } = null!;

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]

        public string ShortDescription { get; set; } = null!;

        [Display(Name = "توضیحات اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر داشته باشد")]
        public string Description { get; set; } = null!;

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Display(Name = "برچسب ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public List<string> Tags { get; set; } = null!;

        public PostStatus Status { get; set; }
    }
    public enum EditPostResult
    {
        Success,
        InvalidImage,
        NotFound
    }
}
