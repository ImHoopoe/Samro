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
    public class MatchWarningServices : IMatchWarning
    {
        private readonly SamroContext _context;

        public MatchWarningServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMatchWarning(MatchWarning matchWarning)
        {
            try
            {
                await _context.AddAsync(matchWarning);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditMatchWarning(MatchWarning matchWarning)
        {
            try
            {
                _context.Update(matchWarning);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteMatchWarning(int id)
        {
            try
            {
                MatchWarning matchWarning = await GetMatchWarningById(id);
                matchWarning.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MatchWarning> GetMatchWarnings()
        {
            return _context.MatchWarnings;
        }

        public async Task<MatchWarning> GetMatchWarningById(int id)
        {
            return await _context.MatchWarnings.FindAsync(id);
        }
    }

}
