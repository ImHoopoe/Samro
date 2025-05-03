using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class MatchWarning
    {
        public int Id { get; set; }

        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int PlayerNumber { get; set; } // 1 یا 2

        public WarningType Type { get; set; }
        public string? Reason { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
