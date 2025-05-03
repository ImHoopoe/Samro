using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Core.Services.TournamentAndMatch
{
    public class MatchRoundServices : IMatchRound
    {
        private readonly SamroContext _context;
        public MatchRoundServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMatchRound(MatchRound matchRound)
        {
            try
            {
                await _context.AddAsync(matchRound);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMatchRound(int id)
        {
            try
            {
                MatchRound matchRound = await GetMatchRoundById(id);
                matchRound.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditMatchRound(MatchRound matchRound)
        {
            try
            {
                _context.Update(matchRound);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<MatchRound> GetMatchRoundById(int id)
        {
            return await _context.MatchRounds.FindAsync(id);
        }

        public IEnumerable<MatchRound> GetMatchRounds()
        {
            return _context.MatchRounds;
        }

    }
}
