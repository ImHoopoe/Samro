using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Interfaces.ChatHub;
using WinWin.Core.Interfaces.Sports;
using WinWin.DataLayer.DTOS.MainSamroShowViewModels;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Components
{
    public class MainNavbarViewComponent : ViewComponent
    {
        private readonly IBlogGroup _blogGroupServices;
        private readonly ISport _sportServices;
        public MainNavbarViewComponent(IBlogGroup blogGroupServices, ISport sportServices)
        {
            _blogGroupServices = blogGroupServices;
            _sportServices = sportServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userId,Guid roomId)
        {
            NavbarViewModel navbar = new NavbarViewModel();
            navbar.BlogGroups = await _blogGroupServices.GetBlogGroupsForNavbar();
            navbar.Sports = await _sportServices.GetSportsAsync();

           return View("MainNavbar", navbar);
        }

    }
}
