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
            var query = _commentRepository.GetQueryable();

            var parentIds = await query
                .Where(c => c.PostId == postId && c.ParentId == null)
                .OrderByDescending(c => c.Id)
                .Skip(skip)
                .Take(take)
                .Select(c => c.Id)
                .ToListAsync();

            if (parentIds.Count == 0)
                return new();

            return await query
                .Where(c => parentIds.Contains(c.Id) || parentIds.Contains(c.ParentId ?? 0))
                .OrderBy(c => c.CreatedAt)
                .Select(c => new CommentsListViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    ParentId = c.ParentId,
                    CreatedAt = c.CreatedAt,
                    UserName = c.User.UserName,
                    UserAvatar = c.User.AvatarName
                })
                .ToListAsync();
        }
    }
}
