using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace Samro.DataLayer.Entities.TournamentMatch
{
    public class TournamentParticipant
    {
        public int TournamentParticipantId { get; set; }
        public Guid? UserId { get; set; }
        public int? TournamentId { get; set; }
        public int? RoleId { get; set; }
        public int? MatchRoleId { get; set; }

        public Tournament Tournament { get; set; }
        public User User { get; set; }
        public Role? Role { get; set; }
        public MatchRole? MatchRole { get; set; }


    }
}
