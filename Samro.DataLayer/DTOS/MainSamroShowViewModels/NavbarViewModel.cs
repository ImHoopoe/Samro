using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.Sport;

namespace WinWin.DataLayer.DTOS.MainSamroShowViewModels
{
    public class NavbarViewModel
    {
        public List<BlogGroup> BlogGroups { get; set; }
        public List<Sport> Sports { get; set; }
     
    }
}
