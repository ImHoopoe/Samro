using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using WinWin.DataLayer.Entities.EventModels;
using WinWin.DataLayer.Entities.TournamentMatch;

namespace WinWin.DataLayer.DTOS
{
    public class CreateTournamentViewModel
    {
        [Required(ErrorMessage = "عنوان رویداد الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان رویداد نباید بیشتر از 100 کاراکتر باشد.")]
        [DisplayName("عنوان رویداد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "آدرس الزامی است.")]
        [StringLength(200, ErrorMessage = "آدرس نباید بیشتر از 200 کاراکتر باشد.")]
        [DisplayName("آدرس رویداد")]
        public string Address { get; set; }

        [Required(ErrorMessage = "محل مسابقه الزامی است.")]
        [DisplayName("محل برگزاری مسابقات")]
        public string MatchLocation { get; set; }

        [Required(ErrorMessage = "محل وزن‌کشی الزامی است.")]
        [DisplayName("محل برگزاری وزن‌کشی")]
        public string WeighInLocation { get; set; }

        [Required(ErrorMessage = "محل ملاقات رودررو الزامی است.")]
        [DisplayName("محل ملاقات رودررو")]
        public string FaceToFaceLocation { get; set; }

        [Required(ErrorMessage = "محل خوابگاه الزامی است.")]
        [DisplayName("محل اقامت و خوابگاه")]
        public string HostelLocation { get; set; }

        [Required(ErrorMessage = "تاریخ مسابقه الزامی است.")]
        [DisplayName("تاریخ برگزاری مسابقه")]
        public string MatchDate { get; set; }

        [Required(ErrorMessage = "تاریخ وزن‌کشی الزامی است.")]
        [DisplayName("تاریخ برگزاری وزن‌کشی")]
        public string WeighInDate { get; set; }

        [Required(ErrorMessage = "تاریخ ملاقات رودررو الزامی است.")]
        [DisplayName("تاریخ ملاقات رودررو")]
        public string FaceToFaceDate { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات نباید بیشتر از 500 کاراکتر باشد.")]
        [DisplayName("توضیحات رویداد")]
        [Required(ErrorMessage = "توضیحات نمی تواند خالی باشد")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "نوع رویداد الزامی است.")]
        [DisplayName("نوع رویداد")]
        public TournamentType TournamentType { get; set; }

        [DisplayName("تاریخ شروع ثبت‌نام")]
        [Required(ErrorMessage = "تاریخ ملاقات رودررو الزامی است.")]
        public string? RegsiterStartsAt { get; set; }

        [Required(ErrorMessage = "تاریخ پایان ثبت‌نام الزامی است.")]
        [DisplayName("تاریخ پایان ثبت‌نام")]
        public string RegsiterEndsAt { get; set; }

        [DisplayName("حداکثر تعداد شرکت‌کنندگان")]
        [Range(1, int.MaxValue, ErrorMessage = "حداقل باید 1 نفر بتواند شرکت کند.")]
        public int MaximnumPlayers { get; set; }

        [DisplayName("تصویر پوستر رویداد")]
        public IFormFile? Thumbnail { get; set; }

        [DisplayName("ورزش")]
        [Required(ErrorMessage = " ورزش الزامی است.")]
        public int SportId { get; set; }

        //public static ValidationResult? ValidateDate(object value, ValidationContext context)
        //{
        //    if (value is not string input || string.IsNullOrWhiteSpace(input))
        //        return ValidationResult.Success; // عدم بررسی برای فیلدهای Optional

        //    try
        //    {
        //        var parts = input.Split('T');
        //        if (parts.Length != 2)
        //            return new ValidationResult("فرمت تاریخ باید به صورت YYYY-MM-DDTHH:mm:ss باشد.");

        //        var dateParts = parts[0].Split('-');
        //        var timeParts = parts[1].Split(':');

        //        if (dateParts.Length != 3 || timeParts.Length < 2)
        //            return new ValidationResult("فرمت تاریخ یا ساعت نادرست است.");

        //        // استخراج قسمت‌های تاریخ و ساعت
        //        int year = int.Parse(dateParts[0]);
        //        int month = int.Parse(dateParts[1]);
        //        int day = int.Parse(dateParts[2]);

        //        int hour = int.Parse(timeParts[0]);
        //        int minute = int.Parse(timeParts[1]);

        //        // تبدیل تاریخ شمسی به میلادی
        //        var pc = new PersianCalendar();
        //        DateTime persianDateTime = new DateTime(year, month, day, hour, minute, 0, pc);

        //        // بررسی اینکه تاریخ باید در آینده باشد
        //        if (persianDateTime < DateTime.Now)
        //            return new ValidationResult("تاریخ باید در آینده باشد.");
        //    }
        //    catch
        //    {
        //        return new ValidationResult("تاریخ وارد شده معتبر نیست.");
        //    }

        //    return ValidationResult.Success;
        //}

        public class DeleteTournamentViewModel
        {
            public int TournamentId { get; set; }
            public string Title { get; set; }
        }

    }

    public class ShowTournamentForAdminViewModel
    {
        public int TournamentId { get; set; }
        public string? Title { get; set; }
        public string Address { get; set; }
        public string MatchLocation { get; set; }
        public string WeighInLocation { get; set; }
        public string FaceToFaceLocation { get; set; }
        public string HostelLocation { get; set; }
        public DateTimeOffset WeighInDate { get; set; }
        public DateTimeOffset FaceToFaceDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string TournamentType { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTimeOffset? RegsiterStartsAt { get; set; }
        public DateTimeOffset RegsiterEndsAt { get; set; }
        public bool IsTimeEnds { get; set; } = false;
        public bool IsFull { get; set; } = false;
        public int? MaximnumPlayers { get; set; }
        public int? RegisteredUsersCount { get; set; }
        public string SportName { get; set; }
        public string Thumbnail { get; set; }
        public bool IsAccepted { get; set; } = false;
    }

    public class EditTournamentViewModel
    {
        public int TournamentId { get; set; }
        [Required(ErrorMessage = "عنوان رویداد الزامی است.")]
        [StringLength(100, ErrorMessage = "عنوان رویداد نباید بیشتر از 100 کاراکتر باشد.")]
        [DisplayName("عنوان رویداد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "آدرس الزامی است.")]
        [StringLength(200, ErrorMessage = "آدرس نباید بیشتر از 200 کاراکتر باشد.")]
        [DisplayName("آدرس رویداد")]
        public string Address { get; set; }

        [Required(ErrorMessage = "محل مسابقه الزامی است.")]
        [DisplayName("محل برگزاری مسابقات")]
        public string MatchLocation { get; set; }

        [Required(ErrorMessage = "محل وزن‌کشی الزامی است.")]
        [DisplayName("محل برگزاری وزن‌کشی")]
        public string WeighInLocation { get; set; }

        [Required(ErrorMessage = "محل ملاقات رودررو الزامی است.")]
        [DisplayName("محل ملاقات رودررو")]
        public string FaceToFaceLocation { get; set; }

        [Required(ErrorMessage = "محل خوابگاه الزامی است.")]
        [DisplayName("محل اقامت و خوابگاه")]
        public string HostelLocation { get; set; }

        [Required(ErrorMessage = "تاریخ مسابقه الزامی است.")]
        [DisplayName("تاریخ برگزاری مسابقه")]
        public string MatchDate { get; set; }

        [Required(ErrorMessage = "تاریخ وزن‌کشی الزامی است.")]
        [DisplayName("تاریخ برگزاری وزن‌کشی")]
        public string WeighInDate { get; set; }

        [Required(ErrorMessage = "تاریخ ملاقات رودررو الزامی است.")]

        [DisplayName("تاریخ ملاقات رودررو")]
        public string FaceToFaceDate { get; set; }

        [StringLength(500, ErrorMessage = "توضیحات نباید بیشتر از 500 کاراکتر باشد.")]
        [DisplayName("توضیحات رویداد")]
        [Required(ErrorMessage = "توضیحات نمی تواند خالی باشد")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "نوع رویداد الزامی است.")]
        [DisplayName("نوع رویداد")]
        public TournamentType TournamentType { get; set; }

        [DisplayName("تاریخ شروع ثبت‌نام")]
        [Required(ErrorMessage = "تاریخ ملاقات رودررو الزامی است.")]
        public string? RegsiterStartsAt { get; set; }

        [Required(ErrorMessage = "تاریخ پایان ثبت‌نام الزامی است.")]
        [DisplayName("تاریخ پایان ثبت‌نام")]
        public string RegsiterEndsAt { get; set; }

        [DisplayName("حداکثر تعداد شرکت‌کنندگان")]
        [Range(1, int.MaxValue, ErrorMessage = "حداقل باید 1 نفر بتواند شرکت کند.")]
        public int MaximnumPlayers { get; set; }

        [DisplayName("تصویر پوستر رویداد")]
        public IFormFile? Thumbnail { get; set; }

        [DisplayName("ورزش")]
        [Required(ErrorMessage = " ورزش الزامی است.")]
        public int SportId { get; set; }

        public static ValidationResult? ValidateDate(object value, ValidationContext context)
        {
            if (value is not string input || string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success;

            try
            {
                var parts = input.Split('T');
                if (parts.Length != 2)
                    return new ValidationResult("فرمت تاریخ باید به صورت YYYY-MM-DDTHH:mm:ss باشد.");

                var dateParts = parts[0].Split('-');
                var timeParts = parts[1].Split(':');

                if (dateParts.Length != 3 || timeParts.Length < 2)
                    return new ValidationResult("فرمت تاریخ یا ساعت نادرست است.");

                // استخراج قسمت‌های تاریخ و ساعت
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);

                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);

                var pc = new PersianCalendar();
                DateTime persianDateTime = new DateTime(year, month, day, hour, minute, 0, pc);

                if (persianDateTime < DateTime.Now)
                    return new ValidationResult("تاریخ باید در آینده باشد.");
            }
            catch
            {
                return new ValidationResult("تاریخ وارد شده معتبر نیست.");
            }

            return ValidationResult.Success;
        }




    }

    public class TournumentRequest
    {
        public string? Title { get; set; }
        public string Address { get; set; }
        public string MatchLocation { get; set; }
        public string WeighInLocation { get; set; }
        public string FaceToFaceLocation { get; set; }
        public string HostelLocation { get; set; }
        public DateTimeOffset WeighInDate { get; set; }
        public DateTimeOffset FaceToFaceDate { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAccepted { get; set; } = false;
        public TournamentType TournamentType { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset RegsiterStartsAt { get; set; }
        public DateTimeOffset RegsiterEndsAt { get; set; }
        public bool IsTimeEnds { get; set; } = false;
        public bool IsFull { get; set; } = false;
        public int MaximnumPlayers { get; set; }
        public int? RegisteredUsersCount
        {
            get;

            //return RegisteredUsers.Count;


        }
        public string Thumbnail { get; set; } = "No.png";
        public int? SportId { get; set; }
    }

    public class UpdateTournamentStatusModel
    {
        public int TournamentId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
