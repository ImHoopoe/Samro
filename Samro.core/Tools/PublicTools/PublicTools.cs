using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace WinWin.Core.Tools.PublicTools
{
    public static class DateTimeExtensions
    {
        #region TimeServices

        public static string ToShamsi(this DateTimeOffset dateTime)
        {
            var persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(dateTime.DateTime);
            int month = persianCalendar.GetMonth(dateTime.DateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime.DateTime);
            return $"{year}/{month:D2}/{day:D2} ساعت {dateTime.Hour:D2}:{dateTime.Minute:D2}";
        }

        public static string ToShamsi(this DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.ToShamsi() ?? string.Empty;
        }

        public static string GetTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            return timeSpan.TotalSeconds switch
            {
                < 60 => "چند لحظه پیش",
                < 3600 => $"{(int)timeSpan.TotalMinutes} دقیقه پیش",
                < 86400 => $"{(int)timeSpan.TotalHours} ساعت پیش",
                < 2592000 => $"{(int)timeSpan.TotalDays} روز پیش",
                < 31536000 => $"{(int)(timeSpan.TotalDays / 30)} ماه پیش",
                _ => $"{(int)(timeSpan.TotalDays / 365)} سال پیش"
            };
        }

        public static string GetTimeAgo(this DateTimeOffset dateTimeOffset)
        {
            var timeSpan = DateTimeOffset.UtcNow - dateTimeOffset;

            return timeSpan.TotalSeconds switch
            {
                < 60 => "چند لحظه پیش",
                < 3600 => $"{(int)timeSpan.TotalMinutes} دقیقه پیش",
                < 86400 => $"{(int)timeSpan.TotalHours} ساعت پیش",
                < 2592000 => $"{(int)timeSpan.TotalDays} روز پیش",
                < 31536000 => $"{(int)(timeSpan.TotalDays / 30)} ماه پیش",
                _ => $"{(int)(timeSpan.TotalDays / 365)} سال پیش"
            };
        }


        public static DateTime ToGregorian(this string persianDateTime)
        {
            if (string.IsNullOrWhiteSpace(persianDateTime))
                throw new ArgumentException("تاریخ نمی‌تواند خالی باشد.", nameof(persianDateTime));

            persianDateTime = ReplacePersianDigits(persianDateTime.Trim());
            var parts = persianDateTime.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var dateParts = parts[0].Split('/');
            if (dateParts.Length != 3)
                throw new FormatException("فرمت تاریخ باید به صورت yyyy/MM/dd باشد.");

            if (!int.TryParse(dateParts[0], out int year) ||
                !int.TryParse(dateParts[1], out int month) ||
                !int.TryParse(dateParts[2], out int day))
            {
                throw new FormatException("تاریخ نامعتبر است.");
            }

            var timeParts = parts.Length > 1 ? parts[1].Split(':') : Array.Empty<string>();
            var (hour, minute, second) = ParseTimeParts(timeParts);

            var pc = new PersianCalendar();
            return pc.ToDateTime(year, month, day, hour, minute, second, 0);
        }

        private static (int hour, int minute, int second) ParseTimeParts(string[] timeParts)
        {
            if (timeParts.Length == 0) return (0, 0, 0);
            if (timeParts.Length < 2 || !int.TryParse(timeParts[0], out int h) || !int.TryParse(timeParts[1], out int m))
                throw new FormatException("فرمت زمان نامعتبر است.");

            int s = timeParts.Length > 2 && int.TryParse(timeParts[2], out int sec) ? sec : 0;
            return (h, m, s);
        }

        private static string ReplacePersianDigits(string input)
        {
            var persianDigits = new[] { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' };
            return new string(input.Select(c =>
                Array.IndexOf(persianDigits, c) is int index && index >= 0 ?
                char.Parse(index.ToString()) :
                c).ToArray());
        }

        #endregion

        #region ImageServices

        public static bool IsImage(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) &&
                   permittedExtensions.Contains(extension) &&
                   file.ContentType.StartsWith("image/");
        }

        #endregion
    }

    public static class PublicTools
    {
        public static async Task<bool> SaveOriginalImageAsync(
            IFormFile imageFile,
            string folderName,
            string fileNameWithoutExtension)
        {
            try
            {
                var targetDir = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    folderName,
                    "org");

                Console.WriteLine($"Attempting to save original image to: {targetDir}");
                Directory.CreateDirectory(targetDir);

                var extension = Path.GetExtension(imageFile.FileName)?.ToLowerInvariant();
                var fullPath = Path.Combine(targetDir, $"{fileNameWithoutExtension}{extension}");

                await using var fileStream = new FileStream(fullPath, FileMode.Create);
                await imageFile.CopyToAsync(fileStream);

                Console.WriteLine($"Successfully saved original image: {fullPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving original image: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static async Task<bool> SaveThumbnailImageAsync(
            IFormFile imageFile,
            string folderName,
            string fileNameWithoutExtension,
            int thumbWidth = 150,
            int thumbHeight = 150,
            int jpegQuality = 75)
        {
            try
            {
                var targetDir = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    folderName,
                    "thumb");

                Console.WriteLine($"Attempting to save thumbnail to: {targetDir}");
                Directory.CreateDirectory(targetDir);

                var thumbPath = Path.Combine(targetDir, $"{fileNameWithoutExtension}.jpg");

                await using var stream = imageFile.OpenReadStream();
                using var image = await Image.LoadAsync(stream);

                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(thumbWidth, thumbHeight),
                    Mode = ResizeMode.Crop,
                    Position = AnchorPositionMode.Center
                }));

                var encoder = new JpegEncoder { Quality = jpegQuality };
                await image.SaveAsync(thumbPath, encoder);

                Console.WriteLine($"Successfully saved thumbnail: {thumbPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving thumbnail: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}