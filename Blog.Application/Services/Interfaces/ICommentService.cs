using Blog.Application.ViewModels.Comments;

namespace Blog.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(int userId, CreateCommentViewModel create);
        Task<List<CommentsListViewModel>> GetCommentsForPostDetailsAsync(int postId, int skip, int take);
    }
}
