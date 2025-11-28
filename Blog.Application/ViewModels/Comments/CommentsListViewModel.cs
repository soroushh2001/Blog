using Blog.Data.Entites.Blog;
using Blog.Data.Entites.User;

namespace Blog.Application.ViewModels.Comments
{
    public class CommentsListViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserAvatar { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? ParentId { get; set; }

    }
}
