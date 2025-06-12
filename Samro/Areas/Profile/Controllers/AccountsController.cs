



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using WinWin.Core.Interfaces;
using WinWin.DataLayer.DTOS;

namespace Samro.Areas.User.Profile.Controllers
{
    [Area("Profile")]
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly IUser _UserServices;

        public AccountsController(IUser UserServices)
        {
            _UserServices = UserServices;
        }

        private Guid GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || !Guid.TryParse(claim.Value, out var userId))
                throw new Exception("شناسه کاربر یافت نشد");

            return userId;
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);

            ShowUserViewModel model = new ShowUserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                LastName = user.LastName,
                Avatar = user.Avatar
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);
            if (user == null)
            {
                TempData["Error"] = "کاربر یافت نشد";
                return Redirect("/Profile/Accounts/index");
            }

            EditProfileViewModel model = new EditProfileViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                LastName = user.LastName,
                AvatarName = user.Avatar,
                Address = user.Address,
                Age = user.Age
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "فیلدهای خالی را پر کنید";
                return View(model);
            }

            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);

            if (user == null)
            {
                TempData["Error"] = "کاربر یافت نشد";
                return View(model);
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.Age = model.Age;
            user.Avatar = model.AvatarName;

            if (!await _UserServices.EditUser(user))
            {
                TempData["Error"] = "عملیات با موفقیت شکست خورد";
                return View(model);
            }

            TempData["Sucsses"] = "عملیات با موفقیت انجام شد";
            return Redirect("/Profile/Accounts/index");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "لطفاً فیلدها را به درستی پر کنید";
                return View(model);
            }

            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);
            if (user == null)
            {
                TempData["Error"] = "کاربر یافت نشد";
                return Redirect("/Profile/Accounts/index");
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["Error"] = "رمز عبور و تکرار آن مطابقت ندارد";
            }
            user.Password = model.NewPassword;
            var result = await _UserServices.EditUser(user);
            if (!result)
            {
                TempData["Error"] = "تغییر رمز عبور انجام نشد. رمز فعلی نادرست است یا مشکلی رخ داده است.";
                return View(model);
            }

            TempData["Sucsses"] = "رمز عبور با موفقیت تغییر کرد";
            return Redirect("/Profile/Accounts/index");
        }
        [HttpGet]
        public async Task<IActionResult> CompleteInfo()
        {
            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);
            if (user == null)
            {
                TempData["Error"] = "کاربر یافت نشد";
                return Redirect("/Profile/Accounts/index");
            }

            CompleteInfoViewModel model = new CompleteInfoViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Address = user.Address,
                Age = user.Age,
                NationalId = user.NationalId,
                Height = user.Height,
                Weight = user.Weight,
                IsActivated = user.IsActivated
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteInfo(CompleteInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "فیلدهای خالی را پر کنید";
                return View(model);
            }

            var userId = GetCurrentUserId();
            var user = await _UserServices.GetUserById(userId);

            if (user == null)
            {
                TempData["Error"] = "کاربر یافت نشد";
                return View(model);
            }

            user.UserName = model.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.Name = user.Name;
            model.LastName = user.LastName;
            model.Avatar = user.Avatar;
            model.Address = user.Address;
            model.Age = user.Age;
            model.NationalId = user.NationalId;
            model.Height = user.Height;
            model.Weight = user.Weight;
            model.IsActivated = user.IsActivated;

            if (!await _UserServices.EditUser(user))
            {
                TempData["Error"] = "عملیات با موفقیت شکست خورد";
                return View(model);
            }

            TempData["Sucsses"] = "عملیات با موفقیت انجام شد";
            return Redirect("/Profile/Accounts/index");
        }
    }
}
