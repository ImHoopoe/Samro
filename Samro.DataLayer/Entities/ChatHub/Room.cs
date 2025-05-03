using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.Entities.ChatHub
{
    public class Room
    {
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public Guid User1Id { get; set; }
        [ForeignKey("User1Id")]
        public User User1 { get; set; }
        public Guid User2Id { get; set; }
        [ForeignKey("User2Id")]
        public User User2 { get; set; }
        public  bool IsDelete { get; set; }


        public ICollection<Message> Messages { get; set; }
    }
}
