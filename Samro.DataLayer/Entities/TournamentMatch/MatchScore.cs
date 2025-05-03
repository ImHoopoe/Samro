using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class MatchScore
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int PlayerRedScore { get; set; }
        public int PlayerBlueScore { get; set; }
        public int ScoreDifference => Math.Abs(PlayerRedScore - PlayerBlueScore);

        public bool IsDeleted { get; set; }
    }
}
