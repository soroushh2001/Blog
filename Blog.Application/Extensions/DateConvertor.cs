using System.Globalization;

namespace CarPartsShop.Application.Extensions
{
    public static class DateConvertor
    {
        private static readonly PersianCalendar _pc = new PersianCalendar();
        
        public static string ToShamsiDate(this DateTime dateTime)
        {
            string year = _pc.GetYear(dateTime).ToString();
            string month = _pc.GetMonth(dateTime).ToString();
            string day = _pc.GetDayOfMonth(dateTime).ToString();

            return $"{year}/{month}/{day} {dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}";
        }

        public static string ToShamsiDateWithMonthName(this DateTime date)
        {
            try
            {
                int year = _pc.GetYear(date);
                int month = _pc.GetMonth(date);
                int day = _pc.GetDayOfMonth(date);

                string monthName = GetPersianMonthName(month);

                return $"{day} {monthName} {year}";
            }
            catch (ArgumentOutOfRangeException)
            {
                return "تاریخ نامعتبر";
            }
        }

        private static string GetPersianMonthName(int month)
        {
            return month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",
                _ => "نامشخص"
            };
        }
    }
}
