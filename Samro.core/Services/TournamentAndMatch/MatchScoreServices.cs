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

        public class MatchScoreServices : IMatchScore
        {
            private readonly SamroContext _context;

            public MatchScoreServices(SamroContext context)
            {
                _context = context;
            }

            public async Task<bool> CreateMatchScore(MatchScore matchScore)
            {
                try
                {
                    await _context.AddAsync(matchScore);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> EditMatchScore(MatchScore matchScore)
            {
                try
                {
                    _context.Update(matchScore);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> DeleteMatchScore(int id)
            {
                try
                {
                    MatchScore matchScore = await GetMatchScoreById(id);
                    matchScore.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IEnumerable<MatchScore> GetMatchScores()
            {
                return _context.MatchScores;
            }

            public async Task<MatchScore> GetMatchScoreById(int id)
            {
                return await _context.MatchScores.FindAsync(id);
            }
        }

    }

