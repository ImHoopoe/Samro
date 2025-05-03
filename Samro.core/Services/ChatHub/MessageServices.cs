using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces.ChatAnd_Message;
using WinWin.Core.Tools.PublicTools;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.DTOS.ChatHub;
using WinWin.DataLayer.Entities.ChatHub;

namespace Samro.Core.Services.ChatHub
{
    public class MessageServices : IMessage
    {
        private readonly SamroContext _context;
        public MessageServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMassage(Message massage)
        {
            try
            {
                await _context.Messages.AddAsync(massage);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> DeleteMassage(Guid id)
        {
            Message message = await GetMessageById(id);
            if (message == null)
            {
                return false;
            }
            try
            {
                //message.IsDelete = true;
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<ChatMessageViewModel>> GetChatMessages(Guid roomId, string userId)
        {
            var messages2 = await _context.Messages
                .Where(m => m.RoomId == roomId)
                .Include(m => m.FromUser)
                .OrderBy(m => m.Timestamp)
                .
                ToListAsync();

            var messages = await _context.Messages
                .Where(m => m.RoomId == roomId)
                .Include(m => m.FromUser)
                .OrderBy(m => m.Timestamp)
                .Select(message => new ChatMessageViewModel()
                {
                    Timestamp = message.Timestamp.GetTimeAgo(),
                    Content = message.Content,
                    FromUserName = message.FromUser.UserName,
                    IsMine = string.Equals(message.FromUserId.ToString(), userId),
                }).
                ToListAsync();
            return messages;
        }

        public async Task<Message> GetMessageById(Guid id)
        {
           return await _context.Messages.FindAsync(id);
        }

        public async Task<bool> UpdateMassage(Message massage)
        {
            try
            {
                _context.Update(massage);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
