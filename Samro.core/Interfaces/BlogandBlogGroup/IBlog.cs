using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Core.Interfaces.BlogandBlogGroup
{
    public interface IBlog
    {
        #region CRUDservices
        Task<bool>  CreateBlog(Blog blog);
        Task<Blog> GetBlogById(int id);
        Task<IEnumerable<Blog>> GetBlogs(int? pageNumber = 1, int? count = 10);
        Task<bool> EditBlog(Blog blog);
        Task<bool> DeleteBlog(int id);
        Task<List<Blog>> GetLastBlogs(int counts = 6);
        #endregion

        #region AdminServices

        Task<string> SaveImageAsync(IFormFile imageFile, string folderName);
        Task<int> GetTotalBlogsCount();

        #endregion
    }
}
