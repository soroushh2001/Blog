using Blog.Application.Services.Interfaces;
using Blog.Application.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Blog.Web.Pages.Posts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IPostService _postService;
        public IndexModel(IPostService postService)
        {
            _postService = postService;
        }

        #endregion

        [BindProperty(SupportsGet = true)]
        public FilterPostsViewModel Filter { get; set; }

        public async Task OnGet()
        {
            Filter = await _postService.FilterPostsAsync(Filter);
        }
    }
}
