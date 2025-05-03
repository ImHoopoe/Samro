using HopLearn.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Threading.Tasks;
using WinWin.Core.Interfaces;
using WinWin.Core.Interfaces.RoleInterfaces;
using WinWin.Core.Services;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.DTOS;
using WinWin.DataLayer.Entities.Roles;
using IUser = WinWin.Core.Interfaces.IUser;

namespace WinWin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _userServices;
        private readonly AccountTools _accountTools;
        private readonly JwtService _jwtServices;
        private readonly EncryptionService _encryptionService;
        private readonly IRole _RoleServices;

        public AccountController(IUser userServices, AccountTools accountTools, JwtService jwtServices, EncryptionService encryptionService, IRole roleServices)
        {
            _userServices = userServices;
            _accountTools = accountTools;
            _jwtServices = jwtServices;
            _encryptionService = encryptionService;
            _RoleServices = roleServices;
        }
        [HttpGet]
        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            var stopwatch = Stopwatch.StartNew();

            if (!ModelState.IsValid)
            {
                ViewData["ValidationErrors"] = "خطاهای زیر را رفع کنید";
                return View(registerModel);
            }

            #region RegisterProcess

            var user = await _userServices.GetUserByIdentifier(registerModel.Email)
                       ?? await _userServices.GetUserByIdentifier(registerModel.PhoneNumber);

            if (user == null)
            {
                User userRegister = new User
                {
                    UserId = AccountTools.GenerateNewId(),
                    UserName = registerModel.UserName,
                    PhoneNumber = registerModel.PhoneNumber,
                    Email = registerModel.Email,
                    IsActivated = false,
                    ActivationCode = AccountTools.GenerateNewId()
                };
                userRegister.Password = _accountTools.HashPassword(userRegister, registerModel.Password);
                if (await _userServices.CreateUser(userRegister))
                {
                    stopwatch.Stop();
                    TempData["SuccessMessage"] = $"ثبت نام با موفقیت انجام شد! لطفاً ایمیل یا پیامک خود را برای فعال‌سازی بررسی کنید. ⏱ زمان اجرا: {stopwatch.ElapsedMilliseconds} میلی‌ثانیه";
                    return Redirect("/");
                }

                stopwatch.Stop();
                ViewData["ValidationErrors"] = $"ثبت نام شکست خورد! لطفاً دوباره تلاش کنید. ⏱ زمان اجرا: {stopwatch.ElapsedMilliseconds} میلی‌ثانیه";
                return View(registerModel);
            }

            if (!user.IsActivated)
            {
                ViewData["ValidationErrors"] = "حساب کاربری شما ثبت شده اما فعال‌سازی نشده است. لطفاً ایمیل یا پیامک خود را بررسی کنید.";
            }
            else
            {
                ViewData["ValidationErrors"] = "کاربری با این ایمیل یا شماره تلفن قبلاً ثبت‌نام کرده است.";
            }

            stopwatch.Stop();
            ViewData["ExecutionTime"] = $"⏱ زمان اجرا: {stopwatch.ElapsedMilliseconds} میلی‌ثانیه";
            return View(registerModel);

            #endregion
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()

        {
            return View();
        }
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel, [FromForm] string returnUrl = "/")
        {
            if (string.IsNullOrWhiteSpace(loginModel.UserName) || string.IsNullOrWhiteSpace(loginModel.Password))
                return BadRequest("نام کاربری و رمز عبور الزامی هستند.");

            var user = await _userServices.GetUserByIdentifier(loginModel.UserName);
            if (user == null || !await _userServices.IsPasswordCorrect(loginModel.UserName, loginModel.Password))
                return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");

            var token = _jwtServices.GenerateToken(user);
            var encryptionService = HttpContext.RequestServices.GetRequiredService<EncryptionService>();
            var encryptedToken = encryptionService.Encrypt(token);

            Response.Cookies.Append("JwtToken", encryptedToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(24)
            });

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return Redirect("/");
        }


        [Route("/LogOut")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("JwtToken");
            return Redirect("/");
        }

        [Route("/AccessDenied")]
        public IActionResult AccessDenied(string? Message)
        {
            TempData["ErrorMessage"] = Message;
            return View();
        }
        [HttpGet]
        [Route("/ForgetPassWord")]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("/ForgetPassWord")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPassword)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ValidationErrors"] = "خطاهای زیر را رفع کنید";
                return View(forgetPassword);
            }
            User user = await _userServices.GetUserByIdentifier(forgetPassword.Identifier);
            if (user == null)
            {
                ViewData["ValidationErrors"] = "کاربری با این مشخصات وجود ندارد";
                return View(forgetPassword);
            }
            if (user.IsActivated == false)
            {
                ViewData["ValidationErrors"] = "حساب کاربری شما فعال نشده است";
                return View(forgetPassword);
            }
            //unimplemented



            return View();
        }
        [HttpGet]
        [Route("/ResetPassword")]
        public async Task<IActionResult> ResetPassword(Guid activationcode)
        {
            if (activationcode == Guid.Empty) 
            {
                TempData["ErrorMessage"] = "خطا در دریافت اطلاعات";
                return Redirect("/Login");
            }
            User user = await _userServices.GetUserByActivationCode(activationcode);
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربری با این مشخصات وجود ندارد";
                return Redirect("/Login");
            }
            return View();
        }
        [HttpPost]
        [Route("/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {

            if (!ModelState.IsValid)
            {
                TempData["ValidationErrors"] = "خطاهای زیر را رفع کنید";
                return View(resetPassword);
            }


            var user = await _userServices.GetUserByIdentifier(resetPassword.Identifier);
            if (user == null)
            {
                TempData["ValidationErrors"] = "کاربری با این مشخصات وجود ندارد";
                return View(resetPassword);
            }


            if (!user.IsActivated)
            {
                TempData["ValidationErrors"] = "حساب کاربری شما فعال نشده است";
                return View(resetPassword);
            }


            var hashedPassword = _accountTools.HashPassword(user, resetPassword.NewPassword);
            user.Password = hashedPassword;


            var updateResult = await _userServices.EditUser(user);
            if (updateResult)
            {
                TempData["SuccessMessage"] = "رمز عبور با موفقیت تغییر کرد";
                return Redirect("/Login");
            }

            TempData["ErrorMessage"] = "عملیات ناموفق بود";
            return View(resetPassword);
        }


    }
}

