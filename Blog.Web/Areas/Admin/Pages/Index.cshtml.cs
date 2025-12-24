using Blog.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Areas.Admin.Pages
{
    [Authorize(Roles = PermissionConstants.Dashboard)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
