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
    public class MatchServices : IMatch
    {
        private readonly SamroContext _context;
        public MatchServices(SamroContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateMatch(Match match)
        {
            try
            {
                await _context.AddAsync(match);
                await _context.SaveChangesAsync();
                return true;
            }
            catch //Exception
            {
                return false;

            }
        }

        public async Task<bool> DeleteMatch(int id)
        {
            try
            {
                Match match = await GetMatchById(id);
                match.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch //Exception
            {
                return false;

            }
        }

        public async Task<bool> EditMatch(Match match)
        {
            try
            {
                _context.Update(match);
                await _context.SaveChangesAsync();
                return true;
            }
            catch //Exception
            {
                return false;

            }
        }

        public async Task<Match> GetMatchById(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public IEnumerable<Match> GetMatches()
        {
            return _context.Matches;
        }
    }
}
