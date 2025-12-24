using Blog.Application.Extensions;
using Blog.Application.Statics;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/UploadImage")]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif",".webp" };

            var fileName = file.FileNameGenerator();

            await file.UploadFile(allowedExtensions, fileName, PathTools.RichTextEditorContentServerPath);
            var imgUrl = PathTools.RichTextEditorContentPath + fileName;
            return new JsonResult(new { location = imgUrl });
        }
    }
}
