using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entites.Blog
{
    public class Tag : BaseEntity
    {
        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}
