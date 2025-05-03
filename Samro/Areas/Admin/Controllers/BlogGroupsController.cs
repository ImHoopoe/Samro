using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Services.BlogandBlogGroupServices;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Areas.Admin.Controllers
{
   // [Authorize]
  //  [PermissionChecker(1)]
    [Area("Admin")]
    [Authorize]
    public class BlogGroupsController : Controller
    {
        private readonly IBlogGroup _blogGroupServices;
        private readonly IBlog _blogServices;
        private readonly ILogger<HomeController> _logger;
        public BlogGroupsController(IBlogGroup blogGroupServices, IBlog blogServices, ILogger<HomeController> logger)
        {
            _blogGroupServices = blogGroupServices;
            _blogServices = blogServices;
            _logger = logger;
        }

        #region BlogGroup

        public async Task<IActionResult> index(int? pageNumber = 1, int? count = 50)
        {
            var blogGroups = await _blogGroupServices.GetBlogGroups(pageNumber, count);
            int totalRecords = await _blogGroupServices.GetTotalBlogGroupsCount();

            var viewModel = new ShowBlogGroupsViewModel
            {
                BlogGroups = blogGroups,
                PageNumber = pageNumber ?? 1,
                Count = count ?? 10,
                TotalRecords = totalRecords
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateBlogGroup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogGroup(CreateBlogGroupViewModel createBlog)
        {
            if (!ModelState.IsValid)
                return View(createBlog);

            var blogGroup = new BlogGroup { BlogGroupName = createBlog.BlogGroupName };

            if (await _blogGroupServices.CreateBlogGroup(blogGroup))
            {
                TempData["SuccessMessage"] = $"گروه {blogGroup.BlogGroupName} با موفقیت اضافه شد";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "عملیات ناموفق بود";
            return View(createBlog);
        }

        [HttpGet]
        public async Task<IActionResult> EditBlogGroup(int id)
        {
            var blogGroup = await _blogGroupServices.GetBlogGroupById(id);
            if (blogGroup == null) return NotFound();

            var editBlog = new EditBlogGroupViewModel
            {
                BlogGroupId = blogGroup.BlogGroupId,
                BlogGroupName = blogGroup.BlogGroupName
            };

            return View(editBlog);
        }

        [HttpPost]
        public async Task<IActionResult> EditBlogGroup(EditBlogGroupViewModel editBlogGroup)
        {
            if (!ModelState.IsValid)
                return View(editBlogGroup);

            var blogGroup = new BlogGroup
            {
                BlogGroupId = editBlogGroup.BlogGroupId,
                ParentId = editBlogGroup.ParentId,
                BlogGroupName = editBlogGroup.BlogGroupName
            };

            if (await _blogGroupServices.EditBlogGroup(blogGroup))
            {
                TempData["SuccessMessage"] = $"گروه {blogGroup.BlogGroupName} با موفقیت ویرایش شد";
                return RedirectToAction("index");
            }

            TempData["ErrorMessage"] = "ویرایش ناموفق بود";
            return View(editBlogGroup);
        }

        [HttpGet("GetSubGroups")]
        public async Task<IActionResult> GetSubGroups(int id)
        {
            var list = await _blogGroupServices.GetSubBlogGroupSelectList(id);
            return Json(new SelectList(list, "Value", "Text"));
        }

        [HttpGet]
        public IActionResult CreateSubBlogGroup(int id)
        {
            var BlogGroup = _blogGroupServices.GetBlogGroupById(id);
            if (BlogGroup==null)
            {
                return NotFound();
            }

            ViewBag.BlogGroupId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubBlogGroup(CreateBlogGroupViewModel createBlog)
        {
            if (!ModelState.IsValid)
                return View(createBlog);

            var blogGroup = new BlogGroup { BlogGroupName = createBlog.BlogGroupName,ParentId = createBlog.ParentId};

            if (await _blogGroupServices.CreateBlogGroup(blogGroup))
            {
                TempData["SuccessMessage"] = $"گروه {blogGroup.BlogGroupName} با موفقیت اضافه شد";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "عملیات ناموفق بود";
            return View(createBlog);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBlogGroupMessage(int id)
        {
            var blogGroup = await _blogGroupServices.GetBlogGroupById(id);
            if (blogGroup == null)
            {
                return NotFound("لطفا url را تغییر ندهید");
            }

            DeleteBlogGroupViewModel deleteBlogGroup = new DeleteBlogGroupViewModel()
            {
                BlogGroupId = blogGroup.BlogGroupId,
                BlogGroupName = blogGroup.BlogGroupName
            };
            return View(deleteBlogGroup);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlogGroup(int id)
        {
            try
            {
                var success = await _blogGroupServices.DeleteBlogGroup(id);
                if (success)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "عملیات حذف گروه با شکست مواجه شد." });
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error deleting blog group: {ex.Message}");
                return Json(new { success = false, message = $"خطا در حذف گروه: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletedBlogGroups(int? pageNumber = 1, int? count = 50)
        {
            var blogGroups = await _blogGroupServices.GetDeletedBlogGroups(pageNumber, count);
            int total = await _blogGroupServices.GetTotalDeletedBlogGroupsCount();
            var deletedBlogGroups = new ShowBlogGroupsViewModel()
            {
                BlogGroups = blogGroups,
                Count = count.Value,
                PageNumber = pageNumber.Value,
                TotalRecords = total
            };
            return View(deletedBlogGroups);
        }


        [HttpPost]
        public async Task<IActionResult> RestoreBlogGroup(int id)
        {
            try
            {
                var success = await _blogGroupServices.RestoreBlogGroup(id);
                if (success)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "عملیات بازگردانی گروه با شکست مواجه شد." });
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error deleting blog group: {ex.Message}");
                return Json(new { success = false, message = $"خطا در بازگردانی گروه: {ex.Message}" });
            }
        }
        #endregion
    }
}
