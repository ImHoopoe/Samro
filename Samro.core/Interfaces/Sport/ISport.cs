using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Sport;

namespace WinWin.Core.Interfaces.Sports
{
    public interface ISport
    {
        Task<List<Sport>> GetSportsAsync();
        Task<Sport> GetByIdAsync(int id);
        Task<bool> CreateAsync(Sport sport);
        Task<bool> UpdateAsync(Sport sport);
        Task<bool> DeleteAsync(int id);
        Task<int> GetSportsCounts();

        Task<bool> AddSubSportAsync(int parentId, Sport sport );
    }
}
