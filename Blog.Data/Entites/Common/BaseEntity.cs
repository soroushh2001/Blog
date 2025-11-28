using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entites.Common
{
    public abstract class BaseEntity 
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now; 
        public bool IsDeleted { get; set; } = false;
    }
}
