using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.DTOS.ChatHub;
using WinWin.DataLayer.Entities.ChatHub;

namespace WinWin.Core.Interfaces.ChatHub
{
    public interface IRoom
    {
        #region CRUD
        Task<List<Room>> GetUserRooms(Guid userId);
        Task<bool> CreateRoom(Guid userId, Guid userId2);
        Task<bool> DeleteRoom(Guid userId);
        

        #endregion

    }
}
