using Microsoft.AspNetCore.Mvc;
using WinWin.Core.Interfaces.Sports;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Sport;
using Microsoft.AspNetCore.Authorization;

namespace WinWin.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize]
    public class SportsController : Controller
    {
        private readonly ISport _sportServices;

        public SportsController(ISport sportServices)
        {
            _sportServices = sportServices;
        }

        public async Task<IActionResult> Index()
        {
            var sports = await _sportServices.GetSportsAsync();
            return View(sports);
        }

        public IActionResult CreateSport()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSport(Sport model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _sportServices.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateSubSport(int id)
        {
            var parentSport = await _sportServices.GetByIdAsync(id);
            if (parentSport == null)
                return NotFound();

            ViewBag.ParentId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubSport(int id, Sport model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentId = id;
                return View(model);
            }

            await _sportServices.AddSubSportAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Sports/Edit/5
        public async Task<IActionResult> EditSport(int id)
        {
            var sport = await _sportServices.GetByIdAsync(id);
            if (sport == null)
                return NotFound();

            return View(sport);
        }

        // POST: Admin/Sports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSport(int id, Sport model)
        {
            if (id != model.SportId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);
            var result = await _sportServices.UpdateAsync(model);
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSport(int id)
        {
            var result = await _sportServices.DeleteAsync(id);
            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
