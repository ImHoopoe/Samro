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
        Task<bool> CreateMatchRound(Round matchRound);
        Task<bool> EditMatchRound(Round matchRound);
        Task<bool> DeleteMatchRound(int id);
        IEnumerable<Round> GetMatchRounds();
        Task<Round> GetMatchRoundById(int id);
    }
}
