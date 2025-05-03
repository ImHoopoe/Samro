using Microsoft.AspNetCore.Mvc;

namespace WinWin.Areas.Profile.Controllers
{
    public class TournamentsController : Controller
    {
        [Area("Profile")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
