using Blog.Application.ViewModels.Posts;
using Blog.Data.Entites.Blog;

namespace Blog.Application.Services.Interfaces
{
    public interface IPostService 
    {
        Task<FilterPostsViewModel> FilterPostsAsync(FilterPostsViewModel filter);
        Task<CreatePostResult> CreatePostAsync(CreatePostViewModel create);
        Task<List<string>> GetTagTitlesAsync(string term);
        Task AddTagsToPostAsync(List<string> tags,int postId);
        Task<EditPostViewModel?> GetPostForEditAsync(int postId);
        Task<EditPostResult> EditPostAsync(EditPostViewModel edit);
        Task<bool> ToggleDeletePostAsync(int postId);
        Task<List<LatestPostViewModel>> GetLatestPostAsync(int take);
        Task<PostViewModel?> GetPostBySlugAsync(string slug);
    }
}
