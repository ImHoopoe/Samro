using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using WinWin.Core.Interfaces.ChatHub;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Components
{
    public class ChatsViewComponent : ViewComponent
    {
        private readonly IRoom _roomServices;
        public ChatsViewComponent(IRoom roomServices)
        {
            _roomServices = roomServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userId,Guid roomId)
        {
            var rooms = await _roomServices.GetUserRooms(userId);
            ViewBag.roomId = roomId;
            return View("Chats",rooms);
        }

    }
}
