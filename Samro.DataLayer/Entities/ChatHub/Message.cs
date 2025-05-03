using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.ChatHub
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
       
        public DateTime Timestamp { get; set; }

        public bool IsDelete { get; set; }
        public Guid FromUserId { get; set; }
        public User FromUser { get; set; }

        public Guid RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
