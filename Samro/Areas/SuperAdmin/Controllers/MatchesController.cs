using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.Core.Services;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize]
    public class MatchesController : Controller
    {

        private readonly IMatch _matchServices;
        private readonly ILogger<HomeController> _logger;
        public MatchesController(IMatch matchServices, ILogger<HomeController> logger)
        {
            _matchServices = matchServices;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateMatch()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchViewModel createMatch)
        {
            Match match = new Match()
            {
                Title = createMatch.Title,
                Description = createMatch.Description,
                MatchDate = createMatch.MatchDate,
                TournamentId = createMatch.TournamentId
            };
            if (await _matchServices.CreateMatch(match))
            {
                TempData["SuccessMessage"] = $"مسابقه {match.Title} با موفقیت اضافه شد";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "عملیات ناموفق بود";
            return View(match);
        }
        public IActionResult EditMatch()
        {
            
            return View();
        }
        public IActionResult DeleteMatch()
        {
            return View();
        }
        public IActionResult CreateMatchRole()
        {
            return View();
        }
        public IActionResult EditMatchRole()
        {
            return View();
        }
        public IActionResult CreateMatchRound()
        {
            return View();
        }
        public IActionResult EditMatchRound()
        {
            return View();
        }
        public IActionResult DeleteMatchRound()
        {
            return View();
        }
        public IActionResult CreateMatchWarning()
        {
            return View();
        }
        public IActionResult EditMatchWarning()
        {
            return View();
        }
        public IActionResult DeleteMatchWarning()
        {
            return View();
        }
        public IActionResult CreateMatchScore()
        {
            return View();
        }
        public IActionResult EditMatchScore()
        {
            return View();
        }
        public IActionResult DeleteMatchScore()
        {
            return View();
        }

    }
}
