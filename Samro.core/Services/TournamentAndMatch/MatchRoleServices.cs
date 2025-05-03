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
    public class MatchRoleServices : IMatchRole
    {

            private readonly SamroContext _context;
            public MatchRoleServices(SamroContext context)
            {
                _context = context;
            }

            public async Task<bool> CreateMatchRole(MatchRole matchRole)
            {
                try
                {
                    await _context.AddAsync(matchRole);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> DeleteMatchRole(int id)
            {
                try
                {
                    MatchRole matchRole = await GetMatchRoleById(id);
                    matchRole.IsDeleted = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<bool> EditMatchRole(MatchRole matchRole)
            {
                try
                {
                    _context.Update(matchRole);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public async Task<MatchRole> GetMatchRoleById(int id)
            {
                return await _context.MatchRoles.FindAsync(id);
            }

            public IEnumerable<MatchRole> GetMatchRoles()
            {
                return _context.MatchRoles;
            }
        }
    }

