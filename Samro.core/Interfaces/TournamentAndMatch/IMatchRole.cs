using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface IMatchRole
    {
        Task<bool> CreateMatchRole(MatchRole matchRole);
        Task<bool> EditMatchRole(MatchRole matchRole);
        Task<bool> DeleteMatchRole(int id);
        IEnumerable<MatchRole> GetMatchRoles();
        Task<MatchRole> GetMatchRoleById(int id);
    }
}
