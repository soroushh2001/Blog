using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.User
{
    public class RolePermission : BaseEntity
    {
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;

        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; } = null!;
    }
}
