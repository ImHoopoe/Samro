using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface IMatchScore
    {
        Task<bool> CreateMatchScore(MatchScore matchScore);
        Task<bool> EditMatchScore(MatchScore matchScore);
        Task<bool> DeleteMatchScore(int id);
        IEnumerable<MatchScore> GetMatchScores();
        Task<MatchScore> GetMatchScoreById(int id);
    }
}
