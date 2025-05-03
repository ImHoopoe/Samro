using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.Entities.Sport
{
    public class SportToMatch
    {
        public int SportToMatchId { get; set; }
        public int SportId { get; set; }
        public int MatchId { get; set; }


        #region Relations
        public Sport Sport { get; set; }
        public Match Match { get; set; }
        #endregion
    }
}
