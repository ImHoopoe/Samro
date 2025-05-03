using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public enum TournamentType
    {
        [Display(Name = "آماتور")]
        Amatour = 1,

        [Display(Name = "لیگ")]
        League = 2,

        [Display(Name = "سوپرفایت")]
         SuperFight= 3,

        [Display(Name = "حرفه ای")]
        Professional = 4,

        [Display(Name = "آماتور")]
        Eliminated = 5        
    }

}



