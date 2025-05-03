using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class MatchRound
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int RoundNumber { get; set; }
        public bool IsCompleted { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsDeleted { get; set; }
    }
}
