using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Entites.User
{
    public class Permission : BaseEntity    
    {
        [MaxLength(250)]
        public string PersianTitle { get; set; } = null!;

        [MaxLength(250)]
        public string EnglishTitle { get; set; } = null!;

        public int? ParentId { get; set;  }
        [ForeignKey(nameof(ParentId))]
        public ICollection<Permission> Parnets { get; set; } = new List<Permission>();

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();  
    }
}
