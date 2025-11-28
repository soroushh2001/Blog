using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.Blog
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; } = null!;

        public string Text { get; set; } = null!;

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")] 
        public ICollection<Comment>? Replies { get; set; }
    }
}
