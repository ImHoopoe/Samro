using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Areas.Admin.Controllers
{
    //[Authorize]
    //[PermissionChecker(1)]
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBlogGroup _blogGroupServices;
        private readonly IBlog _blogServices;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const long MaxFileSize = 5 * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        public HomeController(IBlogGroup blogGroupServices, IBlog blogServices, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _blogGroupServices = blogGroupServices;
            _blogServices = blogServices;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

      

      

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
                return BadRequest(new { error = "فایلی ارسال نشده است!" });

            if (upload.Length > MaxFileSize)
                return BadRequest(new { error = "حجم فایل نباید بیشتر از ۵ مگابایت باشد!" });

            string fileExtension = Path.GetExtension(upload.FileName).ToLower();
            if (Array.IndexOf(AllowedExtensions, fileExtension) == -1)
                return BadRequest(new { error = "فرمت فایل مجاز نیست! فرمت‌های مجاز: jpg, jpeg, png, gif, webp" });

            try
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Blogs","BlogBodies");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }
                string fileUrl = $"/Blogs/BlogBodies/{uniqueFileName}";

                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "خطا در ذخیره‌سازی فایل", details = ex.Message });
            }
        }
    }
}