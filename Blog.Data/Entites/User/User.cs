using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entites.User
{
    public class User : BaseEntity
    {
        [MaxLength(250)]
        public string UserName { get; set; } = null!;

        [MaxLength(250)]
        public string Email { get; set; } = null!;

        [MaxLength(50)]
        public string EmailActivationCode { get; set; } = null!;

        public bool IsEmailConfirmed { get; set; }

        [MaxLength(250)]
        public string Password { get; set; } = null!;
       
        [MaxLength(50)]
        public string? AvatarName { get; set; }
        
        public bool IsBanned { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
