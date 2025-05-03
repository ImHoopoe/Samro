using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities.Sport;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class Match
    {
        public int MatchId { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset MatchDate { get; set; }
        public bool IsDeleted { get; set; }
        public int TournamentId { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Date { get; set; }
        public int? SportId  { get; set; }
        #region Relations
        public Tournament Tournament { get; set; }
        public IEnumerable<User> Players { get; set; }
        public List<MatchRole> ParticipantsWithRoles { get; set; } = new();
        public List<MatchRound> Rounds { get; set; } = new();
        public List<MatchWarning> Warnings { get; set; } = new();
        public MatchScore Score { get; set; }
        public ICollection<SportToMatch> SportToMatches { get; set; }
        #endregion
    }
}
