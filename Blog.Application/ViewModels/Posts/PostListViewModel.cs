using Blog.Data.Entites.Blog;
using Blog.Data.Entites.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Posts
{
    public class PostListViewModel 
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Slug { get; set; } = null!;
        public DateTime LastUpdateDate { get; set; } 

        public PostStatus Status { get; set; }

        public int? Visit { get; set;  }

        public bool IsDeleted { get; set; }

        public string CategoryTitle { get; set; } = null!;

        public string MainImage { get; set; }

        public string CategoryBootstarpClass { get; set; }

        public string ShortDescription { get; set;  }
    }
}
