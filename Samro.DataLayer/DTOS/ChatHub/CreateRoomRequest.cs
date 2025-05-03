using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.DTOS.ChatHub
{
    public class CreateRoomRequest
    {
        public Guid User1Id { get; set; }
        public Guid User2Id { get; set; }
    }
}
