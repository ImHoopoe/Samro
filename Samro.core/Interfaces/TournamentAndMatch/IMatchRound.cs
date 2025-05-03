using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface IMatchRound
    {
        Task<bool> CreateMatchRound(MatchRound matchRound);
        Task<bool> EditMatchRound(MatchRound matchRound);
        Task<bool> DeleteMatchRound(int id);
        IEnumerable<MatchRound> GetMatchRounds();
        Task<MatchRound> GetMatchRoundById(int id);
    }
}
