using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entites.User
{
    public class Role : BaseEntity
    {
        [MaxLength(250)]
        public string PersianTitle { get; set; } = null!;

        [MaxLength(250)]
        public string EnglishTitle { get; set; } = null!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
