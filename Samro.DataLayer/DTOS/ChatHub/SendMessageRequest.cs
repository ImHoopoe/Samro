using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.DTOS.ChatHub
{
    public class SendMessageRequest
    {
        public Guid RoomId { get; set; }
        public Guid FromUserId { get; set; }
        public string Content { get; set; }
    }
}
