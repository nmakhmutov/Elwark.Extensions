using System;

namespace Elwark.Extensions
{
    public static class DateExtensions
    {
        public static bool IsWorkingDay(this DateTimeOffset date) =>
            date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

        public static bool IsWorkingDay(this DateTime date) =>
            date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

        public static bool IsWeekend(this DateTimeOffset date) =>
            date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static bool IsWeekend(this DateTime date) =>
            date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        public static DateTimeOffset NextWorkday(this DateTimeOffset date)
        {
            var result = date.AddDays(1);
            while (!result.IsWorkingDay())
                result = result.AddDays(1);

            return result;
        }
        
        public static DateTime NextWorkday(this DateTime date)
        {
            var result = date.AddDays(1);
            while (!result.IsWorkingDay())
                result = result.AddDays(1);

            return result;
        }

        public static DateTimeOffset NextWeekend(this DateTimeOffset date)
        {
            var result = date.AddDays(1);
            while (!result.IsWeekend())
                result = result.AddDays(1);

            return result;
        }
        
        public static DateTime NextWeekend(this DateTime date)
        {
            var result = date.AddDays(1);
            while (!result.IsWeekend())
                result = result.AddDays(1);

            return result;
        }
    }
}