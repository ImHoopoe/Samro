using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface IMatchWarning
    {
        Task<bool> CreateMatchWarning(MatchWarning matchWarning);
        Task<bool> EditMatchWarning(MatchWarning matchWarning);
        Task<bool> DeleteMatchWarning(int id);
        IEnumerable<MatchWarning> GetMatchWarnings();
        Task<MatchWarning> GetMatchWarningById(int id);
    }
}
