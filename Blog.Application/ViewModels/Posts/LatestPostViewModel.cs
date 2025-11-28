using Blog.Data.Entites.Blog;

namespace Blog.Application.ViewModels.Posts
{
    public class LatestPostViewModel
    {
        public string Title { get; set;  }
        public string Slug { get; set; }
        public Category Category { get; set; }  
        public DateTime PublishedDate { get; set; }
        public string MainImage { get; set; }
    }
}
