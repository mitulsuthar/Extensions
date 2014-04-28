using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsFuture(this DateTime date, DateTime from)
        {
            return date.Date > from.Date;
        }

        public static bool IsFuture(this DateTime date)
        {
            return date.IsFuture(DateTime.Now);
        }

        public static bool IsPast(this DateTime date, DateTime from)
        {
            return date.Date < from.Date;
        }

        public static bool IsPast(this DateTime date)
        {
            return date.IsPast(DateTime.Now);
        }
        
        /// <summary>
        /// Returns the first day of week with in the month.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="dow">What day of week to find the first one of in the month.</param>
        /// <returns>Returns DateTime object that represents the first day of week with in the month.</returns>
        public static DateTime FirstDayOfWeekInMonth(this DateTime obj, DayOfWeek dow)
        {
            DateTime firstDay = new DateTime(obj.Year, obj.Month, 1);
            int diff = firstDay.DayOfWeek - dow;
            if (diff > 0) diff -= 7;
            return firstDay.AddDays(diff * -1);
        }

        /// <summary>
        /// Returns the first weekday (Financial day) of the month
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns DateTime object that represents the first weekday (Financial day) of the month</returns>
        public static DateTime FirstWeekDayOfMonth(this DateTime obj)
        {
            DateTime firstDay = new DateTime(obj.Year, obj.Month, 1);
            for (int i = 0; i < 7; i++)
            {
                if (firstDay.AddDays(i).DayOfWeek != DayOfWeek.Saturday && firstDay.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                    return firstDay.AddDays(i);
            }
            return firstDay;
        }

        /// <summary>
        /// Returns the last day of week with in the month.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="dow">What day of week to find the last one of in the month.</param>
        /// <returns>Returns DateTime object that represents the last day of week with in the month.</returns>
        public static DateTime LastDayOfWeekInMonth(this DateTime obj, DayOfWeek dow)
        {
            DateTime lastDay = new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));
            DayOfWeek lastDow = lastDay.DayOfWeek;

            int diff = dow - lastDow;
            if (diff > 0) diff -= 7;

            return lastDay.AddDays(diff);
        }

        /// <summary>
        /// Returns the last weekday (Financial day) of the month
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns DateTime object that represents the last weekday (Financial day) of the month</returns>
        public static DateTime LastWeekDayOfMonth(this DateTime obj)
        {
            DateTime lastDay = new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));
            for (int i = 0; i < 7; i++)
            {
                if (lastDay.AddDays(i * -1).DayOfWeek != DayOfWeek.Saturday && lastDay.AddDays(i * -1).DayOfWeek != DayOfWeek.Sunday)
                    return lastDay.AddDays(i * -1);
            }
            return lastDay;
        }

        /// <summary>
        /// Returns the closest Weekday (Financial day) Date
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the closest Weekday (Financial day) Date</returns>
        public static DateTime FindClosestWeekDay(this DateTime obj)
        {
            if (obj.DayOfWeek == DayOfWeek.Saturday)
                return obj.AddDays(-1);
            if (obj.DayOfWeek == DayOfWeek.Sunday)
                return obj.AddDays(1);
            else
                return obj;
        }

        /// <summary>
        /// Returns the very end of the given month (the last millisecond of the last hour for the given date)
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the very end of the given month (the last millisecond of the last hour for the given date)</returns>
        public static DateTime EndOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month), 23, 59, 59, 999);
        }

        /// <summary>
        /// Returns the Start of the given month (the fist millisecond of the given date)
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the Start of the given month (the fist millisecond of the given date)</returns>
        public static DateTime BeginningOfMonth(this DateTime obj)
        {
            return new DateTime(obj.Year, obj.Month, 1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Returns the very end of the given day (the last millisecond of the last hour for the given date)
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the very end of the given day (the last millisecond of the last hour for the given date)</returns>
        public static DateTime EndOfDay(this DateTime obj)
        {
            return obj.SetTime(23, 59, 59, 999);
        }

        /// <summary>
        /// Returns the Start of the given day (the fist millisecond of the given date)
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns the Start of the given day (the fist millisecond of the given date)</returns>
        public static DateTime BeginningOfDay(this DateTime obj)
        {
            return obj.SetTime(0, 0, 0, 0);
        }

        /// <summary>
        /// Returns a given datetime according to the week of year and the specified day within the week.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="week">A number of whole and fractional weeks. The value parameter can only be positive.</param>
        /// <param name="dayofweek">A DayOfWeek to find in the week</param>
        /// <returns>A DateTime whose value is the sum according to the week of year and the specified day within the week.</returns>
        public static DateTime GetDateByWeek(this DateTime obj, int week, DayOfWeek dayofweek)
        {
            if (week > 0 && week < 54)
            {
                DateTime FirstDayOfyear = new DateTime(obj.Year, 1, 1);
                int daysToFirstCorrectDay = (((int)dayofweek - (int)FirstDayOfyear.DayOfWeek) + 7) % 7;
                return FirstDayOfyear.AddDays(7 * (week - 1) + daysToFirstCorrectDay);
            }
            else
                return obj;
        }

        private static int Sub(DayOfWeek s, DayOfWeek e)
        {
            if ((s - e) > 0) return (s - e) - 7;
            if ((s - e) == 0) return -7;
            return (s - e);
        }

        /// <summary>
        /// Returns first next occurence of specified DayOfTheWeek
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="day">A DayOfWeek to find the next occurence of</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the enum value represented by the day.</returns>
        public static DateTime Next(this DateTime obj, DayOfWeek day)
        {
            return obj.AddDays(Sub(obj.DayOfWeek, day) * -1);
        }

        /// <summary>
        /// Returns next "first" occurence of specified DayOfTheWeek
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="day">A DayOfWeek to find the previous occurence of</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the enum value represented by the day.</returns>
        public static DateTime Previous(this DateTime obj, DayOfWeek day)
        {
            return obj.AddDays(Sub(day, obj.DayOfWeek));
        }

        private static DateTime SetDateWithChecks(DateTime obj, int year, int month, int day, int? hour, int? minute, int? second, int? millisecond)
        {
            DateTime StartDate;

            if (year == 0)
                StartDate = new DateTime(obj.Year, 1, 1, 0, 0, 0, 0);
            else
            {
                if (DateTime.MaxValue.Year < year)
                    StartDate = new DateTime(DateTime.MinValue.Year, 1, 1, 0, 0, 0, 0);
                else if (DateTime.MinValue.Year > year)
                    StartDate = new DateTime(DateTime.MaxValue.Year, 1, 1, 0, 0, 0, 0);
                else
                    StartDate = new DateTime(year, 1, 1, 0, 0, 0, 0);
            }

            if (month == 0)
                StartDate = StartDate.AddMonths(obj.Month - 1);
            else
                StartDate = StartDate.AddMonths(month - 1);
            if (day == 0)
                StartDate = StartDate.AddDays(obj.Day - 1);
            else
                StartDate = StartDate.AddDays(day - 1);
            if (!hour.HasValue)
                StartDate = StartDate.AddHours(obj.Hour);
            else
                StartDate = StartDate.AddHours(hour.Value);
            if (!minute.HasValue)
                StartDate = StartDate.AddMinutes(obj.Minute);
            else
                StartDate = StartDate.AddMinutes(minute.Value);
            if (!second.HasValue)
                StartDate = StartDate.AddSeconds(obj.Second);
            else
                StartDate = StartDate.AddSeconds(second.Value);
            if (!millisecond.HasValue)
                StartDate = StartDate.AddMilliseconds(obj.Millisecond);
            else
                StartDate = StartDate.AddMilliseconds(millisecond.Value);

            return StartDate;
        }

        /// <summary>
        /// Returns the original DateTime with Hour part changed to supplied hour parameter
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="hour">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetTime(this DateTime obj, int hour)
        {
            return SetDateWithChecks(obj, 0, 0, 0, hour, null, null, null);
        }

        /// <summary>
        /// Returns the original DateTime with Hour and Minute parts changed to supplied hour and minute parameters
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="hour">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
        /// <param name="minute">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetTime(this DateTime obj, int hour, int minute)
        {
            return SetDateWithChecks(obj, 0, 0, 0, hour, minute, null, null);
        }

        /// <summary>
        /// Returns the original DateTime with Hour, Minute and Second parts changed to supplied hour, minute and second parameters
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="hour">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
        /// <param name="minute">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
        /// <param name="second">A number of whole and fractional seconds. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetTime(this DateTime obj, int hour, int minute, int second)
        {
            return SetDateWithChecks(obj, 0, 0, 0, hour, minute, second, null);
        }

        /// <summary>
        /// Returns the original DateTime with Hour, Minute, Second and Millisecond parts changed to supplied hour, minute, second and millisecond parameters
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="hour">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
        /// <param name="minute">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
        /// <param name="second">A number of whole and fractional seconds. The value parameter can be negative or positive.</param>
        /// <param name="millisecond">A number of whole and fractional milliseconds. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetTime(this DateTime obj, int hour, int minute, int second, int millisecond)
        {
            return SetDateWithChecks(obj, 0, 0, 0, hour, minute, second, millisecond);
        }

        /// <summary>
        /// Returns DateTime with changed Year part
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="year">A number of whole and fractional years. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetDate(this DateTime obj, int year)
        {
            return SetDateWithChecks(obj, year, 0, 0, null, null, null, null);
        }

        /// <summary>
        /// Returns DateTime with changed Year and Month part
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="year">A number of whole and fractional years. The value parameter can be negative or positive.</param>
        /// <param name="month">A number of whole and fractional month. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetDate(this DateTime obj, int year, int month)
        {
            return SetDateWithChecks(obj, year, month, 0, null, null, null, null);
        }

        /// <summary>
        /// Returns DateTime with changed Year, Month and Day part
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="year">A number of whole and fractional years. The value parameter can be negative or positive.</param>
        /// <param name="month">A number of whole and fractional month. The value parameter can be negative or positive.</param>
        /// <param name="day">A number of whole and fractional day. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the numbers represented by the parameters.</returns>
        public static DateTime SetDate(this DateTime obj, int year, int month, int day)
        {
            return SetDateWithChecks(obj, year, month, day, null, null, null, null);
        }

        /// <summary>
        /// Adds the specified number of financials days to the value of this instance.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="days">A number of whole and fractional financial days. The value parameter can be negative or positive.</param>
        /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the number of financial days represented by days.</returns>
        public static DateTime AddFinancialDays(this DateTime obj, int days)
        {
            int addint = Math.Sign(days);
            for (int i = 0; i < (Math.Sign(days) * days); i++)
            {
                do { obj = obj.AddDays(addint); }
                while (obj.IsWeekend());
            }
            return obj;
        }

        /// <summary>
        /// Calculate Financial days between two dates.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <param name="otherdate">End or start date to calculate to or from.</param>
        /// <returns>Amount of financial days between the two dates</returns>
        public static int CountFinancialDays(this DateTime obj, DateTime otherdate)
        {
            TimeSpan ts = (otherdate - obj);
            int addint = Math.Sign(ts.Days);
            int unsigneddays = (Math.Sign(ts.Days) * ts.Days);
            int businessdays = 0;
            for (int i = 0; i < unsigneddays; i++)
            {
                obj = obj.AddDays(addint);
                if (!obj.IsWeekend())
                    businessdays++;
            }
            return businessdays;
        }

        /// <summary>
        /// Converts any datetime to the amount of seconds from 1972.01.01 00:00:00
        /// Microsoft sometimes uses the amount of seconds from 1972.01.01 00:00:00 to indicate an datetime.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Total seconds past since 1972.01.01 00:00:00</returns>
        public static double ToMicrosoftNumber(this DateTime obj)
        {
            return (obj - new DateTime(1972, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// Returns true if the day is Saturday or Sunday
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>boolean value indicating if the date is a weekend</returns>
        public static bool IsWeekend(this DateTime obj)
        {
            return (obj.DayOfWeek == DayOfWeek.Saturday || obj.DayOfWeek == DayOfWeek.Sunday);
        }

       

        /// <summary>
        /// Get the quarter that the datetime is in.
        /// </summary>
        /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
        /// <returns>Returns 1 to 4 that represenst the quarter that the datetime is in.</returns>
        public static int Quarter(this DateTime obj)
        {
            return ((obj.Month - 1) / 3) + 1;
        }
        public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
        {
            return (intersectingEndDate >= startDate && intersectingStartDate <= endDate);
        }
       
        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
            DateTime.Today.Month == dateOfBirth.Month &&
             DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            else
                return DateTime.Today.Year - dateOfBirth.Year;
        }
        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static Int64 DateDiff(this DateTime StartDate, String DatePart, DateTime EndDate)
        {
            Int64 DateDiffVal = 0;
            System.Globalization.Calendar cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = (Int64)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (Int64)((((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 4)
                                        + ((cal.GetMonth(EndDate) - 1) / 3))
                                        - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    DateDiffVal = (Int64)(((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 12
                                        + cal.GetMonth(EndDate))
                                        - cal.GetMonth(StartDate));
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (Int64)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (Int64)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    DateDiffVal = (Int64)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (Int64)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (Int64)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    DateDiffVal = (Int64)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }
            return DateDiffVal;
        }

        public static bool IsWeekday(this DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday: return false;

                default: return true;
            }
        }
        public static DateTime AddWorkdays(this DateTime d, int days)
        {
            // start from a weekday
            while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
            for (int i = 0; i < days; ++i)
            {
                d = d.AddDays(1.0);
                while (d.DayOfWeek.IsWeekday()) d = d.AddDays(1.0);
            }
            return d;
        }
        public static string ToOracleSqlDate(this DateTime dateTime)
        {
            return String.Format("to_date('{0}','dd.mm.yyyy hh24.mi.ss')", dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}
