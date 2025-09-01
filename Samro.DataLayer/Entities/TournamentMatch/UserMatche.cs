using System;

namespace Samro.DataLayer.Entities.TournamentMatch
{
    public class UserMatche
    {
        public int UserMatheId { get; set; } // شناسه مسابقه
        public int TournamentId { get; set; } // شناسه تورنمنت
        public Guid UserId { get; set; } // شناسه کاربر (بازیکن)
        public int RoundId { get; set; } // شناسه دور از مسابقه
        public DateTime MatchDate { get; set; } // تاریخ و زمان برگزاری مسابقه
        public bool IsCompleted { get; set; } // وضعیت مسابقه (آیا تمام شده است یا نه)
        public bool IsBye { get; set; } // آیا این بازیکن به دلیل بای از مسابقه کنار رفته است؟
        public string Opponent { get; set; } // حریف این بازیکن در مسابقه
        public int? Score { get; set; } // امتیاز (می‌تواند خالی باشد اگر هنوز مسابقه تمام نشده باشد)
        public int? OpponentScore { get; set; } // امتیاز حریف
        public bool IsWinner { get; set; } // آیا این بازیکن برنده مسابقه است؟
        public string MatchType { get; set; } // نوع مسابقه (مثلاً "نیمه‌نهایی"، "فینال" یا "مرحله گروهی")
        public string Status { get; set; } // وضعیت مسابقه (مثلاً "در حال انجام"، "پایان‌یافته")

        // متد برای دریافت خلاصه مسابقه
        public string GetMatchSummary()
        {
            return $"مسابقه {UserId} در مقابل {Opponent} در دور {RoundId} برگزار شد. مرحله: , وضعیت: {Status}, امتیاز: {Score}-{OpponentScore}";
        }
    }

    // Enum برای مراحل مختلف مسابقه
    public enum MatchStage
    {
        Round1,      // دور اول
        Round2,      // دور دوم
        QuarterFinal, // مرحله یک چهارم نهایی
        SemiFinal,   // نیمه‌نهایی
        Final        // فینال
    }
}