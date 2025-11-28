using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Entites.Blog
{
    public class CategoryColor : BaseEntity
    {
        [MaxLength(50)]
        public string Color { get; set; } = null!;

        [MaxLength(50)]
        public string BootstrapClassName { get; set; } = null!;

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
