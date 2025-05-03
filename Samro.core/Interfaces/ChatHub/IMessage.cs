using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.DTOS.ChatHub;
using WinWin.DataLayer.Entities.ChatHub;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Core.Interfaces.ChatAnd_Message
{
    public interface IMessage
    {
        #region CRUD
        Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(Guid roomId, string userId);
        Task<Message> GetMessageById(Guid id);
        Task<bool> CreateMassage(Message massage);
        Task<bool> UpdateMassage(Message massage);
        Task<bool> DeleteMassage(Guid id);
        #endregion
    }
}
