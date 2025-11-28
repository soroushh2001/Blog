using Blog.Application.ViewModels.Common;
using Blog.Data.Entites.Blog;

namespace Blog.Application.ViewModels.Comments
{
    public class FilterCommentsViewModel : Paging<CommentsListViewModel>
    {
        public int? PostId { get; set; }
    }
}
