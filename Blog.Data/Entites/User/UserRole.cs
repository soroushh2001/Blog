using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.User
{
    public class UserRole : BaseEntity
    {
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
