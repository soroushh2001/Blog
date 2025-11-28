using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.Blog
{
    public class PostTag : BaseEntity
    {
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; } = null!;

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; } = null!;
    }
}
