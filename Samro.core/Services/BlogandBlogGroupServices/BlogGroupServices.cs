using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WinWin.Core.Services.BlogandBlogGroupServices
{
    public class BlogGroupServices : IBlogGroup
    {
        private readonly SamroContext _context;
        public BlogGroupServices(SamroContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateBlogGroup(BlogGroup blogGroup)
        {
            try
            {
                _context.Add(blogGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<bool> DeleteBlogGroup(int id)
        {
            try
            {
                var blogGroups = _context.BlogGroups
                    .Where(bg => bg.BlogGroupId == id || bg.ParentId == id)
                    .ToList();

                foreach (var bg in blogGroups)
                {
                    bg.IsDeleted = true;
                }

                _context.SaveChanges();

                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<bool> RestoreBlogGroup(int id)
        {
            try
            {
                var blogGroups = _context.BlogGroups.IgnoreQueryFilters()
                    .Where(bg => bg.BlogGroupId == id || bg.ParentId == id)
                    .ToList();

                foreach (var bg in blogGroups)
                {
                    bg.IsDeleted = false;
                }

                _context.SaveChanges();

                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<bool> EditBlogGroup(BlogGroup blogGroup)
        {
            try
            {
                _context.Update(blogGroup);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<BlogGroup> GetBlogGroupById(int id)
        {
            return _context.BlogGroups.Find(id);
        }

        public async Task<IEnumerable<BlogGroup>> GetBlogGroups(int? pageNumber = 1, int? count = 10)
        {
            pageNumber = pageNumber ?? 1;
            count = count ?? 10;

            return await _context.BlogGroups
                .Skip((pageNumber.Value - 1) * count.Value)
                .Take(count.Value)
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetBlogGroupsSelectList()
        {
            return await _context.BlogGroups.Where(bg => bg.ParentId == null).Select(bg => new SelectListItem()
            {
                Value = bg.BlogGroupId.ToString(),
                Text = bg.BlogGroupName
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSubBlogGroupSelectList(int parentId)
        {
            return await _context.BlogGroups.Where(bg => bg.ParentId == parentId).Select(bg => new SelectListItem()
            {
                Text = bg.BlogGroupName,
                Value = bg.BlogGroupId.ToString()
            }).ToListAsync();
        }



        public async Task<int> GetTotalBlogGroupsCount()
        {
            return await _context.BlogGroups.CountAsync();
        }
        public async Task<int> GetTotalDeletedBlogGroupsCount()
        {
            return await _context.BlogGroups.IgnoreQueryFilters().Where(bg => bg.IsDeleted == true).CountAsync();
        }

        public async Task<IEnumerable<BlogGroup>> GetDeletedBlogGroups(int? pageNumber = 1, int? count = 10)
        {
            pageNumber = pageNumber ?? 1;
            count = count ?? 10;

            return await _context.BlogGroups.IgnoreQueryFilters().Where(bg => bg.IsDeleted == true)
                .Skip((pageNumber.Value - 1) * count.Value)
                .Take(count.Value)
                .ToListAsync();
        }

        public async Task<List<BlogGroup>> GetBlogGroupsForNavbar()
        {
            return await _context.BlogGroups.ToListAsync();
        }
    }
}
