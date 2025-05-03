
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.ChatHub;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.ChatHub;

namespace WinWin.Core.Services.ChatHub
{
    public class RoomServices : IRoom
    {
        private readonly SamroContext _context;

        public RoomServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRoom(Guid user1Id, Guid user2Id)
        {
            try
            {
                Room room = new Room()
                {
                    RoomId = Guid.NewGuid(),
                    RoomName = $"{user1Id}{user2Id}",
                    User1Id = user1Id,
                    User2Id = user2Id,

                };
                await _context.AddAsync(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
                
            }
        }

        public async Task<bool> DeleteRoom(Guid roomId)
        {
            try
            {
                Room room = await _context.Rooms.FindAsync(roomId); 
                //room.IsDelete = true;
                await _context.SaveChangesAsync();
                return  true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public Task<List<Room>> GetUserRooms(Guid userId)
        {
            return _context.Rooms
                .Where(r => r.User1Id == userId || r.User2Id == userId)
                .Include(r => r.User1)
                .Include(r => r.User2)
                .ToListAsync();
        }

    }
}
