using Microsoft.AspNetCore.Mvc;

namespace WinWin.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class PaymentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
    
}
