using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.DTOS
{
    public class PlayerTournumentsViewModel
    {
        public string? Title { get; set; }
        public string MatchLocation { get; set; }
        public DateTimeOffset MatchDate { get; set; }
        public bool IsDeleted { get; set; }
        public TournamentType TournamentType { get; set; }
        public Guid CreatedByUserId { get; set; }

    }
}
