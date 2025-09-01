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
    public class RoundServices : IMatchRound
    {
        private readonly SamroContext _context;
        public RoundServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMatchRound(Round matchRound)
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
                Round matchRound = await GetMatchRoundById(id);
                matchRound.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditMatchRound(Round matchRound)
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

        public async Task<Round> GetMatchRoundById(int id)
        {
            return await _context.Rounds.FindAsync(id);
        }

        public IEnumerable<Round> GetMatchRounds()
        {
            return _context.Rounds;
        }

    }
}
