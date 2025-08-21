using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace WinWin.DataLayer.DTOS
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8,ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        [MaxLength(50, ErrorMessage = "حداکثر مقدار {0} 50 کاراکتر می باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Compare("Password",ErrorMessage = "مقدار رمز عبور و تکرار آن مطابقت ندارد")]
        public string RePassword { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [EmailAddress(ErrorMessage = "ایمیل صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string PhoneNumber { get; set; }
        
    }
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        [MaxLength(50, ErrorMessage = "حداکثر مقدار {0} 50 کاراکتر می باشد")]
        public required string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        public required string Password { get; set; }
    }
    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        [MaxLength(50, ErrorMessage = "حداکثر مقدار {0} 50 کاراکتر می باشد")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [EmailAddress(ErrorMessage = "ایمیل صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string PhoneNumber { get; set; }
        [Display(Name = "آواتار")]
        public IFormFile Avatar { get; set; }
        [Display(Name = "آواتار")]
        public string? AvatarName { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string LastName { get; set; }
        [Display(Name = "سن")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public int? Age { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Address { get; set; }



    }
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز فعلی را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "رمز جدید را وارد کنید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "تکرار رمز را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "رمزهای عبور مطابقت ندارند")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
    public class DeleteProfile 
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        [MaxLength(50, ErrorMessage = "حداکثر مقدار {0} 50 کاراکتر می باشد")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        public string Password { get; set; }
    }
    public class  ForgetPasswordViewModel
    {
        [Display(Name = "ایمیل یا نام کاربری یا شماره تلفن")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Identifier { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Compare("Password", ErrorMessage = "مقدار رمز عبور و تکرار آن مطابقت ندارد")]
        public string RePassword { get; set; }

        [Display(Name = "ایمیل یا نام کاربری یا شماره تلفن")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Identifier { get; set; }

        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [MinLength(8, ErrorMessage = "حداقل مقدار {0} 8 کاراکتر می باشد")]
        public string NewPassword { get; set; }

    }
    public class ShowUserViewModel 
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string PhoneNumber { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Avatar { get; set; }
        [Display(Name = "آواتار")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        public string LastName { get; set; }

        public bool IsActivated { get; set; }
    }
    public class CompleteInfoViewModel
    {

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Display(Name = "جنسیت")]
        public bool IsMan { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [StringLength(50)]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [StringLength(50)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Display(Name = "کد ملی")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Display(Name = "آواتار")]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [StringLength(200)]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Range(0, 120)]
        [Display(Name = "سن")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Range(0.0, 3.0)]
        [Display(Name = "قد (متر)")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Range(0.0, 500.0)]
        [Display(Name = "وزن (کیلوگرم)")]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "مقدار {0} الزامی است !")]
        [Display(Name = "فعال شده")]
        public bool IsActivated { get; set; }

    }


}
