using Blog.Data.Entites.Blog;

namespace Blog.Application.ViewModels.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Slug { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Category Category { get; set; } = null!;

        public List<string> Tags { get; set; } = null!;

        public DateTime DatePublished { get; set; }
        public string MainImage { get; set; }
    }
}
