using Microsoft.AspNetCore.Mvc;

namespace WinWin.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class AchievementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
