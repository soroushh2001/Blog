using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.Blog
{
    public class Category : BaseEntity
    {
        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(50)]
        public string Image { get; set; } = null!;

        public int CategoryColorId { get; set; }
        [ForeignKey("CategoryColorId")]
        public CategoryColor CategoryColor { get; set; } = null!;

        public ICollection<Post> Post { get; set; } = new List<Post>();
    }
}
