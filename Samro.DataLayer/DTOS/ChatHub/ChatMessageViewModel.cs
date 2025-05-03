using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.ChatHub;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.DTOS.ChatHub
{
    public class ChatMessageViewModel
    {
        public string Content { get; set; }
        public string Timestamp { get; set; }
        public string FromUserName { get; set; }
        public bool IsMine { get; set; }

    }
}
