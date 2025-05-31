using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.Sports;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.Core.Services.BlogandBlogGroupServices;
using WinWin.Core.Tools.Account;
using WinWin.Core.Tools.PublicTools;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.EventModels;
using static WinWin.DataLayer.DTOS.CreateTournamentViewModel;

namespace WinWin.Areas.SuperAdmin.Controllers
{
    [Area("SuperAdmin")]
    [Authorize]
    [PermissionChecker(5)]
    public class TournamentsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITournament _tournamentServices;
        private readonly ISport _SportServices;
        public TournamentsController(ILogger<HomeController> logger, ITournament tournamentServices, ISport SportServices)
        {
            _logger = logger;
            _tournamentServices = tournamentServices;
            _SportServices = SportServices;
        }
        public async Task<IActionResult> Index()
        {
            var tournaments = await _tournamentServices.GetTournaments();

            var viewModelList = new List<ShowTournamentForAdminViewModel>();

            foreach (var t in tournaments)
            {
                viewModelList.Add(new ShowTournamentForAdminViewModel
                {
                    Title = t.Title,
                    IsDeleted = t.IsDeleted,
                    IsTimeEnds = t.IsTimeEnds,
                    IsFull = t.IsFull,
                    Description = t.Description,
                    TournamentType = await t.TournamentType.GetDisplayNameAsync(),
                    MatchLocation = t.MatchLocation,
                    FaceToFaceDate = t.FaceToFaceDate,
                    Address = t.Address,
                    WeighInLocation = t.WeighInLocation,
                    FaceToFaceLocation = t.FaceToFaceLocation,
                    HostelLocation = t.HostelLocation,
                    WeighInDate = t.WeighInDate,
                    RegsiterStartsAt = t.RegsiterStartsAt,
                    RegsiterEndsAt = t.RegsiterEndsAt,
                    MaximnumPlayers = t.MaximnumPlayers,
                    CreatedByUserName = t.CreatedByUser.UserName,
                    RegisteredUsersCount = t.RegisteredUsersCount,
                    TournamentId = t.TournamentId,
                    Thumbnail = t.Thumbnail,
                    IsAccepted = t.IsAccepted
                });
            }

            return View(viewModelList);
        }



        [HttpGet]
        [PermissionChecker(6)]
        public async Task<IActionResult> CreateTournament()
        {
            ViewData["Sports"] = await _tournamentServices.GetSportGroups();
            return View();
        }

        [HttpPost]
        [PermissionChecker(6)]
        public async Task<IActionResult> CreateTournament(CreateTournamentViewModel createTournamentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Sports"] = await _tournamentServices.GetSportGroups();
                TempData["ErrorMessage"] = "عملیات با شکست مواجه شد";
                return View(createTournamentViewModel);
            }

            try
            {

            
            Tournament tournament = new Tournament()
            {
                Title = createTournamentViewModel.Title,
                Address = createTournamentViewModel.Address,
                Description = createTournamentViewModel.Description,
                IsDeleted = false,
                FaceToFaceDate = DateTimeExtensions.ToGregorian(createTournamentViewModel.FaceToFaceDate),
                WeighInDate = DateTimeExtensions.ToGregorian(createTournamentViewModel.WeighInDate),
                MatchLocation = createTournamentViewModel.MatchLocation,
                HostelLocation = createTournamentViewModel.HostelLocation,
                TournamentType = createTournamentViewModel.TournamentType,
                WeighInLocation = createTournamentViewModel.WeighInLocation,
                CreatedByUserId =Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                FaceToFaceLocation = createTournamentViewModel.FaceToFaceLocation,
                IsFull = false,
                IsTimeEnds = false,
                MaximnumPlayers = createTournamentViewModel.MaximnumPlayers,
                RegsiterEndsAt = DateTimeExtensions.ToGregorian(createTournamentViewModel.RegsiterEndsAt),
                RegsiterStartsAt = DateTimeExtensions.ToGregorian(createTournamentViewModel.RegsiterStartsAt),
                SportId = createTournamentViewModel.SportId
            };
            if (createTournamentViewModel.Thumbnail!=null&&createTournamentViewModel.Thumbnail.Length>0)
            {
                string ImageName = Guid.NewGuid().ToString();
               await PublicTools.SaveOriginalImageAsync(createTournamentViewModel.Thumbnail, "Tournament", ImageName);
               await PublicTools.SaveThumbnailImageAsync(createTournamentViewModel.Thumbnail, "Tournament", ImageName);
                tournament.Thumbnail = ImageName + ".jpg";
            }

            if (!await _tournamentServices.CreateTournament(tournament))
            {
                TempData["ErrorMessage"] = "عملیات با شکست مواجه شد";
                return Redirect($"/Admin/Home/Index/");
            }

            TempData["SuccessMessage"] = $"رویداد {tournament.Title} با موفقیت اضافه گردید";
            return Redirect($"/Admin/Home/Index/");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditTournament(int id)
        {
            Tournament tournament = await _tournamentServices.GetTournamentById(id);

            if (tournament == null)
            {
                TempData["ErrorMessage"] = "رویداد مورد نظر یافت نشد";
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            EditTournamentViewModel tournamentViewModel = new EditTournamentViewModel
            {
                TournamentId = tournament.TournamentId,
                Title = tournament.Title,
                Address = tournament.Address,
                Description = tournament.Description,
                FaceToFaceDate = DateTimeExtensions.ToShamsi(tournament.FaceToFaceDate),
                FaceToFaceLocation = tournament.FaceToFaceLocation,
                MatchLocation = tournament.MatchLocation,
                HostelLocation = tournament.HostelLocation,
                WeighInDate = DateTimeExtensions.ToShamsi(tournament.WeighInDate),
                WeighInLocation = tournament.WeighInLocation,
                MaximnumPlayers = tournament.MaximnumPlayers,
                RegsiterEndsAt = DateTimeExtensions.ToShamsi(tournament.RegsiterEndsAt),
                RegsiterStartsAt = DateTimeExtensions.ToShamsi(tournament.RegsiterStartsAt),
                TournamentType = tournament.TournamentType,
                SportId = tournament.SportId.Value
                
            };

            ViewData["Sports"] = await _tournamentServices.GetSportGroups();

            return View(tournamentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTournament(EditTournamentViewModel editTournament)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Sports"] = await _tournamentServices.GetSportGroups();
                TempData["ErrorMessage"] = "تمام فیلد ها را پر کنید";
                return View(editTournament);
            }
            Tournament tournament = await _tournamentServices.GetTournamentById(editTournament.TournamentId);
            if (tournament == null) {
                TempData["ErrorMessage"] = "رویداد مورد نظر یافت نشد";
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
           
                tournament.Title = editTournament.Title;
                tournament.Address = editTournament.Address;
                tournament.Description = editTournament.Description;
                tournament.FaceToFaceDate = DateTimeExtensions.ToGregorian(editTournament.FaceToFaceDate);
                tournament.FaceToFaceLocation = editTournament.FaceToFaceLocation;
                tournament.MatchLocation = editTournament.MatchLocation;
                tournament.HostelLocation = editTournament.HostelLocation;
                tournament.WeighInDate = DateTimeExtensions.ToGregorian(editTournament.WeighInDate);
                tournament.WeighInLocation = editTournament.WeighInLocation;
                tournament.MaximnumPlayers = editTournament.MaximnumPlayers;
                tournament.RegsiterEndsAt = DateTimeExtensions.ToGregorian(editTournament.RegsiterEndsAt);
                tournament.RegsiterStartsAt = DateTimeExtensions.ToGregorian(editTournament.RegsiterStartsAt);
                tournament.SportId = editTournament.SportId;
            
            if (!await _tournamentServices.EditTournament(tournament))
            {
                TempData["ErrorMessage"] = "عملیات با شکست مواجه شد";
                return Redirect($"/Admin/Home/Index/");
            }
            TempData["SuccessMessage"] = $"رویداد {tournament.Title} با موفقیت ویرایش گردید";
            return Redirect($"/Admin/Tournaments/Index/"); 
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTournamentMessage(int id)
        {
            var tournament = await _tournamentServices.GetTournamentById(id);
            if (tournament == null)
            {
                return NotFound("رویداد مورد نظر یافت نشد. لطفاً از تغییر URL خودداری نمایید.");
            }

            DeleteTournamentViewModel deleteTournament = new DeleteTournamentViewModel()
            {
                TournamentId = tournament.TournamentId,
                Title = tournament.Title
            };

            return View(deleteTournament);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTournament([FromBody] int id)
        {
            try
            {
                var success = await _tournamentServices.DeleteTournament(id);
                if (success)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "عملیات حذف رویداد با شکست مواجه شد." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting tournament: {ex.Message}");
                return Json(new { success = false, message = $"خطا در حذف رویداد: {ex.Message}" });
            }
        }

        [HttpGet("/GetSubSportGroups")]
        public async Task<IActionResult> GetSubGroups(int id)
        {
            var list = await _tournamentServices.GetSubSportsSelectList(id);
            return Json(new SelectList(list, "Value", "Text"));
        }


        [HttpGet]
        [Route("admin/tournaments/UpdateTournamentStatus")]
        public async Task<IActionResult> UpdateTournamentStatus()
        {
            var tournaments = await _tournamentServices.GetStepOneTournaments();

            var viewModelList = new List<ShowTournamentForAdminViewModel>();

            foreach (var t in tournaments)
            {
                viewModelList.Add(new ShowTournamentForAdminViewModel
                {
                    Title = t.Title,
                    IsDeleted = t.IsDeleted,
                    IsTimeEnds = t.IsTimeEnds,
                    IsFull = t.IsFull,
                    Description = t.Description,
                    TournamentType = await t.TournamentType.GetDisplayNameAsync(),
                    MatchLocation = t.MatchLocation,
                    FaceToFaceDate = t.FaceToFaceDate,
                    Address = t.Address,
                    WeighInLocation = t.WeighInLocation,
                    FaceToFaceLocation = t.FaceToFaceLocation,
                    HostelLocation = t.HostelLocation,
                    WeighInDate = t.WeighInDate,
                    RegsiterStartsAt = t.RegsiterStartsAt,
                    RegsiterEndsAt = t.RegsiterEndsAt,
                    MaximnumPlayers = t.MaximnumPlayers,
                    CreatedByUserName = t.CreatedByUser.UserName,
                    RegisteredUsersCount = t.RegisteredUsersCount,
                    TournamentId = t.TournamentId,
                    Thumbnail = t.Thumbnail,
                    IsAccepted = t.IsAccepted
                });
            }

            return View(viewModelList);

        }

        [HttpPost]
        [Route("admin/tournaments/updateTournamentStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTournamentStatus([FromBody] UpdateTournamentStatusViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "مدل ارسال شده null است" });
            }

            var tournament = await _tournamentServices.GetTournamentById(model.TournamentId);
            if (tournament == null)
            {
                return Json(new { success = false, message = "رویداد پیدا نشد" });
            }

            tournament.IsAccepted = model.IsAccepted;

            var success = await _tournamentServices.EditTournament(tournament);
            if (success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "خطا در بروزرسانی وضعیت رویداد" });
            }
        }


        [HttpGet]
        [Route("admin/tournaments/UpdateTournamentFinalStatus")]
        public async Task<IActionResult> UpdateTournamentFinalStatus()
        {
            var tournaments = await _tournamentServices.GetStepStepTwoTournaments();

            var viewModelList = new List<ShowTournamentForAdminViewModel>();

            foreach (var t in tournaments)
            {
                viewModelList.Add(new ShowTournamentForAdminViewModel
                {
                    Title = t.Title,
                    IsDeleted = t.IsDeleted,
                    IsTimeEnds = t.IsTimeEnds,
                    IsFull = t.IsFull,
                    Description = t.Description,
                    TournamentType = await t.TournamentType.GetDisplayNameAsync(),
                    MatchLocation = t.MatchLocation,
                    FaceToFaceDate = t.FaceToFaceDate,
                    Address = t.Address,
                    WeighInLocation = t.WeighInLocation,
                    FaceToFaceLocation = t.FaceToFaceLocation,
                    HostelLocation = t.HostelLocation,
                    WeighInDate = t.WeighInDate,
                    RegsiterStartsAt = t.RegsiterStartsAt,
                    RegsiterEndsAt = t.RegsiterEndsAt,
                    MaximnumPlayers = t.MaximnumPlayers,
                    CreatedByUserName = t.CreatedByUser.UserName,
                    RegisteredUsersCount = t.RegisteredUsersCount,
                    TournamentId = t.TournamentId,
                    Thumbnail = t.Thumbnail,
                    IsAccepted = t.IsAccepted
                });
            }

            return View(viewModelList);
        }
        [HttpPost]
        [Route("admin/tournaments/UpdateTournamentFinalStatus")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTournamentFinalStatus([FromBody] UpdateTournamentFinalStatusModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "مدل ارسال شده null است" });
            }

            var tournament = await _tournamentServices.GetTournamentById(model.TournamentId);
            if (tournament == null)
            {
                return Json(new { success = false, message = "رویداد پیدا نشد" });
            }

            tournament.IsAccepted = model.IsAccepted;

            var success = await _tournamentServices.EditTournament(tournament);
            if (success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "خطا در بروزرسانی وضعیت رویداد" });
            }
        }

    }
}
