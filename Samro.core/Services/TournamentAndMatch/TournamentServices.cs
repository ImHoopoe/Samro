using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.EventModels;

namespace WinWin.Core.Services.TournamentAndMatch
{
    public class TournamentServices : ITournament
    {
        private readonly SamroContext _context;
        public TournamentServices(SamroContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateTournament(Tournament tournament)
        {
            try
            {
                await _context.AddAsync(tournament);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return false;

            }
        }

        public async Task<bool> DeleteTournament(int id)
        {
            try
            {
                Tournament tournament = await GetTournamentById(id);
                tournament.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return false;

            }
        }

        public async Task<bool> EditTournament(Tournament tournament)
        {
            try
            {
                _context.Update(tournament);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return false;

            }
        }

        public async Task<List<Tournament>> GetLastTournaments(int counts = 10)
        {
            return await _context.Tournaments.Take(counts).ToListAsync();
        }

        public async Task<Tournament> GetTournamentById(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }


        public async Task<List<Tournament>> GetTournaments()
        {
            return await _context.Tournaments
                .Include(t => t.CreatedByUser) 
                .Include(t => t.Sport)          
                .ToListAsync();
        }



        public async Task<List<SelectListItem>> GetSubSportsSelectList(int parentId)
        {
            return await _context.Sports.Where(bg => bg.ParentId == parentId).Select(s => new SelectListItem()
            {
                Text = s.SportName,
                Value = s.SportId.ToString()
            }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetSportGroups()
        {
            return await _context.Sports
                .Where(sport => sport.ParentId == null)
                .Select(sport => new SelectListItem
                {
                    Value = sport.SportId.ToString(),
                    Text = sport.SportName
                })
                .ToListAsync();
        }

        public async Task<int> GetTournamentsCounts()
        {
            return await _context.Tournaments.CountAsync();
        }

        // متد اول: GetStepOneTournaments
        public async Task<List<Tournament>> GetStepOneTournaments()
        {
            return await _context.Tournaments
                .Where(t => !t.IsAccepted)
                .AsNoTracking()  // جلوگیری از ردیابی تغییرات
                .Include(t => t.CreatedByUser)  // بارگذاری موجودیت مربوط به CreatedByUser
                .Include(t => t.Sport)  // بارگذاری موجودیت مربوط به Sport
                .ToListAsync();
        }

        // متد دوم: GetStepTwoTournaments
        public async Task<List<Tournament>> GetStepStepTwoTournaments()
        {
            return await _context.Tournaments
                .Where(t => !t.IsFinal && t.IsAccepted)
                .AsNoTracking()  // جلوگیری از ردیابی تغییرات
                .Include(t => t.CreatedByUser)  // بارگذاری موجودیت مربوط به CreatedByUser
                .Include(t => t.Sport)  // بارگذاری موجودیت مربوط به Sport
                .ToListAsync();
        }

 
    }
}
