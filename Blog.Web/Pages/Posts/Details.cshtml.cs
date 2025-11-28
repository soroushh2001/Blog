using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Comments;
using Blog.Application.ViewModels.Posts;
using CarPartsShop.Mvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        #region Constructor

        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public DetailsModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        #endregion

        public PostViewModel Post { get; set; }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            var post = await _postService.GetPostBySlugAsync(slug);
           
            if (post == null) return NotFound();
         
            Post = post;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CreateCommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                await _commentService.AddCommentAsync(HttpContext.User.GetCurrentUserId(),comment);
                return Redirect($"/posts/details/{comment.PostSlug}");
            }
            Post = await _postService.GetPostBySlugAsync(comment.PostSlug);
            return Page();
        }

        public async Task<IActionResult> OnGetLoadCommentsAsync(int postId, int skip = 0, int take = 5)
        {
            var comments = await _commentService.GetCommentsForPostDetailsAsync(postId, skip, take);
            return Partial("_Comments", comments);
        }

    }
}
