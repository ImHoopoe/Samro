using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface IMatch
    {
        #region CRUD
        Task<bool> CreateMatch(Match match);
        Task<bool> EditMatch(Match match);
        Task<bool> DeleteMatch(int id);
        IEnumerable<Match> GetMatches();
        Task<Match> GetMatchById(int id);

        #endregion

    }
}
