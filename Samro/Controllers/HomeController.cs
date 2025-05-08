using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WinWin.Core.Interfaces;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.DataLayer.DTOS.MainSamroShowViewModels;
using WinWin.Models;

namespace WinWin.Controllers;

public class HomeController : Controller
{
    public IUser _userServices { get; set; }
    public IBlog _blogServices { get; set; }
    public ITournament _tournamentServices { get; set; }

    public HomeController(IUser userServices, IBlog blogServices, ITournament tournamentServices)
    {
        _userServices = userServices;
        _blogServices = blogServices;
        _tournamentServices = tournamentServices;
    }

    public async Task<IActionResult> Index()
    {
        IndexViewModel indexViewModel = new IndexViewModel()
        {
            LastBlogs = await _blogServices.GetLastBlogs(),
            LasTournaments = await _tournamentServices.GetLastTournaments(),
            UsersCounts = await _userServices.GetTotalUsersCount()

        };
        return View(indexViewModel);
    }

    [Route("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }


    

    [HttpGet]
    [Route("/check-token")]
    public IActionResult CheckToken()
    {
        var token = Request.Cookies["JwtToken"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized("No token found in cookies");
        }
        return Ok($"Token: {token}");
    }
}
