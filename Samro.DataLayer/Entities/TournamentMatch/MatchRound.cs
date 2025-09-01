using Samro.DataLayer.Entities.TournamentMatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.TournamentMatch
{

    public class Round
    {
        public int RoundId { get; set; }
        public int MatchId { get; set; }
        public int RoundNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public RoundStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }

    public enum RoundStatus
    {
        Ongoing,
        Completed,
        Cancelled
    }
}
