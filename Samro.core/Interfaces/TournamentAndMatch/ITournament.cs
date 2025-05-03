using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.EventModels;

namespace WinWin.Core.Interfaces.TournamentAndMatch
{
    public interface ITournament
    {
        #region CRUD
        Task<bool> CreateTournament(Tournament tournament);
        Task<bool> EditTournament(Tournament tournament);
        Task<bool> DeleteTournament(int id);
        Task<List<Tournament>> GetTournaments();
        Task<Tournament> GetTournamentById(int id);
        Task<List<Tournament>> GetLastTournaments(int counts = 10);
        Task<List<SelectListItem>> GetSportGroups();
        Task<List<SelectListItem>> GetSubSportsSelectList(int parentId);

        #endregion

    }
}
