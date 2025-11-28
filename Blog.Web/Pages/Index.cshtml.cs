using Blog.Application.Extensions;
using Blog.Application.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }


        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            var fileName = file.FileNameGenerator();

            await file.UploadFile(allowedExtensions, fileName, PathTools.RichTextEditorContentServerPath);
            var imgUrl = PathTools.RichTextEditorContentPath + fileName;
            return new JsonResult(new { location = imgUrl });
        }


    }
}
