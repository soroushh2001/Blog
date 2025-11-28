namespace Blog.Application.ViewModels.Categories
{
    public class CategoryViewModel 
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;

        public string BootstrapClassName { get; set; } = null!;

        public int Id { get; set; }

        public string Image { get; set; } = null!; 
        public bool IsDeleted { get; set;  }
    }
}
