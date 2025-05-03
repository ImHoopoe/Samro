using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.BlogBlogGroup
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public bool IsDeleted { get; set; }
        public int BlogGroupId { get; set; }
        public string Tags { get; set; }

        #region Relations

        public BlogGroup BlogGroup { get; set; }
        

        #endregion
    }
}
