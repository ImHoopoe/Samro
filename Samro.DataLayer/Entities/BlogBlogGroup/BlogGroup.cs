
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.BlogBlogGroup
{
    public class BlogGroup
    {
        public int BlogGroupId { get; set; }
        public string BlogGroupName { get; set; }
        public int? ParentId { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ParentId")]
        public BlogGroup? ParentGroup { get; set; }
        public ICollection<Blog> Blogs { get; set; } = new List<Blog>();


    }
}
