using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.BlogBlogGroup;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.DataLayer.DTOS
{
    public class CreateBlogViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, ErrorMessage = " مقدار {0} باید حداکثر {1} کاراکتر باشد.")]
        public required string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(160, MinimumLength = 50, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string ShortDescription { get; set; }

        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [MinLength(300, ErrorMessage = " مقدار {0} باید حداقل {1} کاراکتر باشد.")]
        public required string Description { get; set; }

        [Display(Name = "تصویر مطلب")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        public required IFormFile Thumbnail { get; set; }

        [Display(Name = "گروه بلاگ")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        public required int BlogGroupId { get; set; }


        [Display(Name = "برچسب‌ها")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(200, ErrorMessage = " مقدار {0} باید حداکثر {1} کاراکتر باشد.")]
        public required string Tags { get; set; }
    }

    public class EditBlogViewModel
    {
        public int BlogId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, ErrorMessage = " مقدار {0} باید حداکثر {1} کاراکتر باشد.")]
        public required string Title { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(160, MinimumLength = 50, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public required string ShortDescription { get; set; }

        [Display(Name = "توضیح کامل")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [MinLength(300, ErrorMessage = " مقدار {0} باید حداقل {1} کاراکتر باشد.")]
        public required string Description { get; set; }



        [Display(Name = "تصویر مطلب")]
        public IFormFile? Thumbnail { get; set; }

        public string? ThumbnailName { get; set; }

        [Display(Name = "گروه بلاگ")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        public required int BlogGroupId { get; set; }

        [Display(Name = "برچسب‌ها")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(200, ErrorMessage = " مقدار {0} باید حداکثر {1} کاراکتر باشد.")]
        public required string Tags { get; set; }
    }

    public class ShowBlogsViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / Count);
    }



    public class CreateBlogGroupViewModel
    {
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public string BlogGroupName { get; set; }

        [Display(Name = "گروه والد")]
        public int? ParentId { get; set; }
    }

    public class EditBlogGroupViewModel
    {
        public int BlogGroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public string BlogGroupName { get; set; }

        [Display(Name = "گروه والد")]
        public int? ParentId { get; set; }
    }

    public class ShowBlogGroupsViewModel
    {
        public IEnumerable<BlogGroup> BlogGroups { get; set; }
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / Count);
    }

    public class DeleteBlogGroupViewModel
    {
        public int BlogGroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public string BlogGroupName { get; set; }

    }


    public class CreateUserViewModel()
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر و شامل حداقل یک حرف بزرگ، یک حرف کوچک، یک عدد و یک کاراکتر خاص باشد.")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند.")]
        public string RePassword { get; set; }
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
        [Display(Name = "کد ملی")]
        public string? NationalId { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile Avatar { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        [Display(Name = "سن")]
        public int? Age { get; set; }
        [Display(Name = "قد")]
        public double? Height { get; set; }
        [Display(Name = "وزن")]
        public double? Weight { get; set; }

    }
    public class UpdateUserViewModel()
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = " مقدار {0} الزامی است.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = " مقدار {0} باید بین {2} تا {1} کاراکتر باشد.")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} الزامی است.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "رمز عبور باید حداقل {2} کاراکتر باشد.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر و شامل حداقل یک حرف بزرگ، یک حرف کوچک، یک عدد و یک کاراکتر خاص باشد.")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند.")]
        public string RePassword { get; set; }
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }
        [Display(Name = "کد ملی")]
        public string? NationalId { get; set; }
        [Display(Name = "تصویر")]
        public string Avatar { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        [Display(Name = "سن")]
        public int? Age { get; set; }
        [Display(Name = "قد")]
        public double? Height { get; set; }
        [Display(Name = "وزن")]
        public double? Weight { get; set; }
    }
    public class ShowUsersViewModel()
    {
        public IEnumerable<User> Users { get; set; }
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / Count);
    }
    public class CreateMatchViewModel()
    {
        public int MatchId { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset MatchDate { get; set; }
        public int TournamentId { get; set; }
    }

}
