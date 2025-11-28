using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.Blog
{
    public class Post : BaseEntity
    {
        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(50)]
        public string MainImage { get; set; } = null!;

        [MaxLength(250)]
        public string ShortDescription { get; set; } = null!;

        
        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public PostStatus Status { get; set; }

        public int? Visit { get; set; }
    }

    public enum PostStatus
    {
        [Display(Name = "پیش نویس")]
        Draft,
        [Display(Name = "منتشر شده")]
        Published,
        [Display(Name = "بایگانی شده")]
        Archived,
        [Display(Name = "حذف شده")]
        Deleted
    }
}
