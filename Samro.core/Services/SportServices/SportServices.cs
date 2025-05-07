using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces.Sports;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.Sport;

namespace WinWin.Core.Services.SportServices
{
    public class SportServices : ISport
    {
        private readonly SamroContext _context;

        public SportServices(SamroContext context)
        {
            _context = context;
        }

       

        public async Task<Sport> GetSportsAsync(int id)
        {
            return await _context.Sports
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SportId == id);
        }

        public async Task<bool> CreateAsync(Sport sport)
        {
            _context.Sports.Add(sport);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Sport sport)
        {
            var existingSport = await _context.Sports.FindAsync(sport.SportId);
            if (existingSport == null)
                return false;

            _context.Entry(existingSport).CurrentValues.SetValues(sport);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
                return false;

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddSubSportAsync(int parentId, Sport subSport)
        {
            var parentSport = await _context.Sports
                .Include(s => s.SubGroups)
                .FirstOrDefaultAsync(s => s.SportId == parentId);

            if (parentSport == null)
                return false;

            if (parentSport.SubGroups == null)
                parentSport.SubGroups = new List<Sport>();

            parentSport.SubGroups.Add(subSport);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Sport>> GetSportsAsync()
        {
            return await _context.Sports
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Sport> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetSportsCounts()
        {
            return await _context.Sports.CountAsync();
        }
    }
}
