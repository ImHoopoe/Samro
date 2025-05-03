using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.TournamentMatch
{
    public class MatchRole
    {
        public int MatchRoleId { get; set; }
        public string MatchRoleName { get; set; }           
        public string? MatchRoleDisplayName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
