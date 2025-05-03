using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.BlogBlogGroup;

namespace WinWin.Core.Services.BlogandBlogGroupServices
{
    public class BlogServices : IBlog
    {
        private readonly SamroContext _context;
        public BlogServices(SamroContext context)
        {
            _context = context;
        }
        public async Task<bool>  CreateBlog(Blog blog)
        {
            try
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<bool> DeleteBlog(int id)
        {
            try
            {
                var blog = await GetBlogById(id);
                _context.Remove(blog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<bool> EditBlog(Blog blog)
        {
            try
            {
                _context.Update(blog);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;

            }
        }

        public async Task<Blog> GetBlogById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<IEnumerable<Blog>> GetBlogs(int? pageNumber = 1, int? count = 10)
        {
            pageNumber = pageNumber ?? 1;
            count = count ?? 10;

            return await _context.Blogs
                .Skip((pageNumber.Value - 1) * count.Value)
                .Take(count.Value)
                .ToListAsync();
        }

        public Task<List<Blog>> GetLastBlogs(int counts = 6)
        {
            return _context.Blogs.OrderByDescending(b => b.PublishDate).Take(counts).ToListAsync();
        }

        public async Task<int> GetTotalBlogsCount()
        {
            return await _context.Blogs.CountAsync();
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return Path.Combine("images", folderName, uniqueFileName).Replace("\\", "/");
        }
    }
}
