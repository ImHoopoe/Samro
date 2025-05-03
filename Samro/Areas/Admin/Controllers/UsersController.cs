using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Services.BlogandBlogGroupServices;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Areas.Admin.Controllers
{
    // [Authorize]
    //  [PermissionChecker(1)]
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUser _UserServices;
        private readonly ILogger<HomeController> _logger;
        public UsersController(IUser UserServices, ILogger<HomeController> logger)
        {
            _UserServices = UserServices;
            _logger = logger;
        }
        #region User
        public async Task<IActionResult> Index(int? pageNumber = 1, int? count = 10)
        {
            var users = _UserServices.GetAllUser(pageNumber ?? 1, count ?? 10);
            int totalRecords = await _UserServices.GetTotalUsersCount();

            var viewModel = new ShowUsersViewModel
            {
                Users = users,
                PageNumber = pageNumber ?? 1,
                Count = count ?? 10,
                TotalRecords = totalRecords
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUser)
        {
            User User = new User
            {
                UserName = createUser.UserName,
                Password = createUser.Password,
                Email = createUser.Email,
                PhoneNumber = createUser.PhoneNumber,
                NationalId = createUser.NationalId,
                Avatar = "Nopng",
                Address = createUser.Address,
                Age = createUser.Age,
                Height = createUser.Height,
                Weight = createUser.Weight,
                IsActivated = true

            };
            if (await _UserServices.CreateUser(User))
            {
                TempData["SuccessMessage"] = $"کاربر {User.UserName} با موفقیت اضافه شد";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "عملیات ناموفق بود";
            return View(User);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(Guid id)
        {
            User updateuser = await _UserServices.GetUserById(id);
            if (updateuser == null) return NotFound();

            var user = new UpdateUserViewModel
            {
                UserName = updateuser.UserName,
                Email = updateuser.Email,
                PhoneNumber = updateuser.PhoneNumber,
                NationalId = updateuser.NationalId,
                Address = updateuser.Address,
                Age = updateuser.Age,
                Height = updateuser.Height,
                Weight = updateuser.Weight,
                Password = updateuser.Password

            };

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel updateuser)
        {
            if (!ModelState.IsValid)
                return View(updateuser);

            var user = new User
            {
                UserName = updateuser.UserName,
                Email = updateuser.Email,
                PhoneNumber = updateuser.PhoneNumber,
                NationalId = updateuser.NationalId,
                Address = updateuser.Address,
                Age = updateuser.Age,
                Height = updateuser.Height,
                Weight = updateuser.Weight,
                Password = updateuser.Password
            };

            if (await _UserServices.EditUser(user))
            {
                TempData["SuccessMessage"] = $"گروه {user.UserName} با موفقیت ویرایش شد";
                return RedirectToAction("index");
            }

            TempData["ErrorMessage"] = "ویرایش ناموفق بود";
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _UserServices.GetUserById(id);
            if (user == null)
            {
                return Json(new { success = false, message = "کاربر یافت نشد." });
            }

            var result = await _UserServices.DeleteUser(id);
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "حذف کاربر ناموفق بود." });
            }
        }



        #endregion
    }
}
