using System;

namespace cd.Domain.Infrastructure
{
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        /// Gets the date for the nth Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, or Saturday for the month.
        /// </summary>
        /// <param name="nth">The occurrence to find</param>
        /// <param name="dayOfWeek">The week day to find</param>
        /// <returns>DateTime</returns>
        public static DateTime NthDayOfWeekForMonth(this DateTime date, int nth, DayOfWeek dayOfWeek)
        {
            nth.ValidateRange(1, 4, $"nth is out of range: {nth}.");

            DateTime next = date.FirstDayOfMonth().Next(dayOfWeek);

            if (nth == 1)
            {
                return next;
            }
            return next.AddWeeks(nth - 1);
        }

        /// <summary>
        /// Sets the time component for a date.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime date, int hour, int minutes, int seconds)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minutes, seconds);
        }

        /// <summary>
        /// Gets the date for the 1st Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, or Saturday for the month.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week to find</param>
        /// <returns>DateTime</returns>
        public static DateTime FirstDayOfWeekForMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.NthDayOfWeekForMonth(1, dayOfWeek);
        }

        /// <summary>
        /// Gets the date for the last Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, or Saturday for the month.
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns>DateTime</returns>
        public static DateTime LastDayOfWeekForMonth(this DateTime date, DayOfWeek dayOfWeek)
        {
            DateTime last = date.LastDayOfMonth();

            if (last.DayOfWeek != dayOfWeek)
            {
                last = last.Previous(dayOfWeek);
            }

            return last;
        }

        /// <summary>
        /// Add a number of weeks to the date.
        /// </summary>
        /// <param name="weeks"></param>
        /// <returns>DateTime</returns>
        public static DateTime AddWeeks(this DateTime date, int weeks)
        {
            return date.AddDays(weeks * 7);
        }

        /// <summary>
        /// Gets the first day of the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Gets the last day of the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime LastDayOfMonth(this DateTime date)
        {
            int lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, lastDay);
        }


        /// <summary>
        /// Gets the 1st Monday through Friday occurrence in the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime FirstWeekdayOfMonth(this DateTime date)
        {
            DateTime result = date.FirstDayOfMonth();
            if (result.IsWeekend())
                result = result.Next(DayOfWeek.Monday);
            return result;
        }

        /// <summary>
        /// Gets the 1st Saturday or Sunday occurrence in the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime FirstWeekendDayOfMonth(this DateTime date)
        {
            DateTime result = date.FirstDayOfMonth();
            if (!result.IsWeekend())
                result = result.Next(DayOfWeek.Saturday);
            return result;
        }

        /// <summary>
        /// Gets the last Saturday or Sunday occurrence in the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime LastWeekendDayOfMonth(this DateTime date)
        {
            DateTime result = date.LastDayOfMonth();
            if (!result.IsWeekend())
                result = result.Previous(DayOfWeek.Sunday);
            return result;
        }

        /// <summary>
        /// Gets the last Monday through Friday occurrence in the month.
        /// </summary>
        /// <returns>DateTime</returns>
        public static DateTime LastWeekdayOfMonth(this DateTime date)
        {
            DateTime result = date.LastDayOfMonth();
            if (result.IsWeekend())
                result = result.Previous(DayOfWeek.Friday);
            return result;
        }

        /// <summary>
        /// Gets the date for the previous Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, or Saturday.
        /// </summary>
        /// <param name="dayOfWeek">The day of the week to find</param>
        /// <returns>DateTime</returns>
        public static DateTime Previous(this DateTime date, DayOfWeek dayOfWeek)
        {
            int offsetDays = date.DayOfWeek - dayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime result = date.AddDays(-offsetDays);
            return result;
        }

        /// <summary>
        /// Gets the date for the next Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, or Saturday.
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns>DateTime</returns>
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - date.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime result = date.AddDays(offsetDays);
            return result;
        }

        /// <summary>
        /// Determines if a date falls on a weekend.
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsWeekend(this DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday);
        }

        /// <summary>
        /// Converts UnixTime (seconds from Epoch) to a DateTime value.
        /// Note: Similar code exists in vw.Tools.GetDateFromMillisecondField()
        /// which appears to be designed for ADO data conversion.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToDateTime(this int seconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(seconds);
        }

        public static string ToLongDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy hh:mm tt");
        }
    }
}
