using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Interfaces.ChatHub;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Components
{
    public class MainNavbarComponent : ViewComponent
    {
        private readonly IBlog _blogServices;
        //private readonly  _blogServices;
        public MainNavbarComponent(IBlog blogServices)
        {
            _blogServices = blogServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userId,Guid roomId)
        {
            //var rooms = await _roomServices.GetUserRooms(userId);
            ViewBag.roomId = roomId;
            return View();
        }

    }
}
