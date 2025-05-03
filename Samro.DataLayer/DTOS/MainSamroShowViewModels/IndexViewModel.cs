using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.EventModels;

namespace WinWin.DataLayer.DTOS.MainSamroShowViewModels
{
    public class IndexViewModel
    {
        public List<Blog> LastBlogs { get; set; }
        public List<Tournament> LasTournaments { get; set; }
        public int UsersCounts { get; set; }
        public int TournamentsCounts { get; set; }
        public int AwardsCounts { get; set; }
    }
}
