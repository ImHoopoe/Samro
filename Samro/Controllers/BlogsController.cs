using Microsoft.AspNetCore.Mvc;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.DataLayer.DTOS;

namespace Samro.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlog _blogServices;
        public BlogsController(IBlog blogServices)
        {
            _blogServices = blogServices;
        }

        public async Task<IActionResult> Blogs(int page = 1, int pageSize = 9)
        {
            var result = await _blogServices.GetBlogs(page, pageSize);

            var totalCount = await _blogServices.GetTotalBlogsCount();

            var viewModel = new ShowBlogsViewModel()
            {
                Blogs = result,
                PageNumber = page,
                Count = pageSize,           
                TotalRecords = totalCount   
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ShowBlog(int id)
        {
            var blog =await _blogServices.GetBlogById(id);
            if (blog==null)
            {
                return View("Error");
            }

            return View(blog);
        }

    }
}
