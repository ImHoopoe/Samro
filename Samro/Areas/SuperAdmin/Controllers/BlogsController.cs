using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Services.BlogandBlogGroupServices;
using WinWin.Core.Tools.Account;
using WinWin.Core.Tools.PublicTools;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Areas.SuperAdmin.Controllers
{
    // [Authorize]
    //  [PermissionChecker(1)]
    [Area("SuperAdmin")]
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly IBlog _blogServices;
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogGroup _blogGroupServices;
        public BlogsController(IBlog blogServices, ILogger<HomeController> logger, IBlogGroup blogGroupServices)
        {
            _blogServices = blogServices;
            _logger = logger;
            _blogGroupServices = blogGroupServices;
        }

        #region Blog

        public async Task<IActionResult> Index(int? pageNumber = 1, int? count = 10)
        {
            var blogs = await _blogServices.GetBlogs(pageNumber, count);
            int totalRecords = await _blogServices.GetTotalBlogsCount();

            var viewModel = new ShowBlogsViewModel()
            {
                PageNumber = pageNumber ?? 1,
                Count = count ?? 10,
                TotalRecords = totalRecords,
                Blogs = blogs
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {
            ViewData["BlogGroups"] = await _blogGroupServices.GetBlogGroupsSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var blog = new Blog
                    {
                        Title = model.Title,
                        ShortDescription = model.ShortDescription,
                        Description = model.Description,
                        BlogGroupId = model.BlogGroupId,
                        Tags = model.Tags,
                        PublishDate = DateTime.Now,
                    };
                    if (model.Thumbnail!=null)
                    {
                        string imageName = Guid.NewGuid().ToString();
                       await PublicTools.SaveOriginalImageAsync(model.Thumbnail, "BlogThumbs", imageName);
                       await PublicTools.SaveThumbnailImageAsync(model.Thumbnail, "BlogThumbs", imageName);
                       blog.Thumbnail = imageName + ".jpg";
                    }
                    var result = await _blogServices.CreateBlog(blog);
                    if (result)
                    {
                        TempData["SuccessMessage"] = $"مقاله <{blog.Title}> با موفقیت اضافه شد";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "خطا در ذخیره مقاله.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating blog.");
                    ModelState.AddModelError("", "خطا در ذخیره مقاله.");
                }
            }
            ViewData["BlogGroups"] = await _blogGroupServices.GetBlogGroupsSelectList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            Blog blog = await _blogServices.GetBlogById(id);
            if (blog==null)
            {
                TempData["ErrorMessage"] = "این مقاله وجود ندارد";
                return RedirectToAction("Index");

            }
            EditBlogViewModel editModel = new EditBlogViewModel()
            {
                BlogGroupId = blog.BlogGroupId,
                Title = blog.Title,
                Tags = blog.Tags,
                Description = blog.Description,
                ShortDescription = blog.ShortDescription,
                BlogId = blog.BlogId,
                ThumbnailName = blog.Thumbnail
            };
            ViewData["BlogGroups"] = await _blogGroupServices.GetBlogGroupsSelectList();
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlog(EditBlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var blog = new Blog
                    {
                        BlogId = model.BlogId,
                        Title = model.Title,
                        ShortDescription = model.ShortDescription,
                        Description = model.Description,
                        BlogGroupId = model.BlogGroupId,
                        Tags = model.Tags,
                        PublishDate = DateTime.UtcNow,
                        Thumbnail = await _blogServices.SaveImageAsync(model.Thumbnail, "thumbnails")
                    };

                    var result = await _blogServices.CreateBlog(blog);
                    if (result)
                    {
                        TempData["SuccessMessage"] = $"مقاله <{blog.Title}> با موفقیت ویرایش شد";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "خطا در ذخیره مقاله.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating blog.");
                    ModelState.AddModelError("", "خطا در ذخیره مقاله.");
                }
            }
            ViewData["BlogGroups"] = await _blogGroupServices.GetBlogGroupsSelectList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MassageDeleteBlog(int id) 
        {
            Blog blog = await _blogServices.GetBlogById(id);
            if (blog == null)
            {
                BadRequest("url را تغییر ندهید ");
            }
            if (await _blogServices.DeleteBlog(id))
            {
                TempData["SuccessMessage"] = $"مقاله {blog.Title} با موفقیت حدف شد";
                return RedirectToAction("index");

            }
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                var success = await _blogServices.DeleteBlog(id);
                if (success)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "عملیات حذف مقاله با شکست مواجه شد." });
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error deleting blog ..." +
                    $": {ex.Message}");
                return Json(new { success = false, message = $"خطا در حذف مقاله: {ex.Message}" });
            }
        }
        #endregion
    }
}
