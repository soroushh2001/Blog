using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Comments;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services.Implementation
{
    public class CommentService : ICommentService
    {
        #region Constructor

        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        #endregion

        public async Task AddCommentAsync(int userId, CreateCommentViewModel create)
        {
            var newComment = new Comment
            {
                PostId = create.PostId,
                Text = create.Text,
                ParentId = create.ParentId,
                UserId = userId,
            };
            await _commentRepository.AddAsync(newComment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<List<CommentsListViewModel>> GetCommentsForPostDetailsAsync(int postId, int skip, int take)
        {
            var parents = await _commentRepository.GetQueryable()
        .Where(c => c.PostId == postId && c.ParentId == null)
        .OrderByDescending(c => c.Id)
        .Skip(skip)
        .Take(take)
        .Include(c => c.User)
        .Include(c => c.Replies)
            .ThenInclude(r => r.User)
        .ToListAsync();

            var result = new List<CommentsListViewModel>();

            foreach (var parent in parents)
            {
                result.Add(new CommentsListViewModel
                {
                    Id = parent.Id,
                    Text = parent.Text,
                    ParentId = null,
                    UserName = parent.User?.UserName ?? "",
                    UserAvatar = parent.User?.AvatarName ?? ""
                });

                if (parent.Replies != null)
                {
                    foreach (var reply in parent.Replies)
                    {
                        result.Add(new CommentsListViewModel
                        {
                            Id = reply.Id,
                            Text = reply.Text,
                            ParentId = parent.Id,
                            UserName = reply.User?.UserName ?? "",
                            UserAvatar = reply.User?.AvatarName ?? ""
                        });
                    }
                }
            }

            return result;
        }
    }
}
