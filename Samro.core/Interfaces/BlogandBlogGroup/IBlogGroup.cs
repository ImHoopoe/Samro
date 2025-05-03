using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Core.Interfaces.BlogandBlogGroup
{
    public interface IBlogGroup
    {
        #region CRUDservices
        Task<bool>  CreateBlogGroup(BlogGroup blog);
        Task<BlogGroup> GetBlogGroupById(int id);
        Task<IEnumerable<BlogGroup>> GetBlogGroups(int? pageNumber = 1, int? count = 10);
        Task<List<BlogGroup>> GetBlogGroupsForNavbar();
        Task<IEnumerable<BlogGroup>> GetDeletedBlogGroups(int? pageNumber = 1, int? count = 10);
        Task<bool> EditBlogGroup(BlogGroup blog);
        Task<bool> DeleteBlogGroup(int id);
        Task<bool> RestoreBlogGroup(int id);
        Task<int> GetTotalBlogGroupsCount();
        Task<int> GetTotalDeletedBlogGroupsCount();
        Task<List<SelectListItem>> GetSubBlogGroupSelectList(int parentId);
        Task<List<SelectListItem>> GetBlogGroupsSelectList();
        #endregion
    }
}
