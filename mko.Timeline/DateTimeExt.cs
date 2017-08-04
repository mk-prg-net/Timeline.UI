//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.7.2017
//
//  Projekt.......: mko.Timeline
//  Name..........: DateTimeExt
//  Aufgabe/Fkt...: Erweiterungsmethoden für DateTime
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Timeline
{
    public enum DateDiffInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }

    public static class DateTimeExt
    {

        /// <summary>
        /// Klassenfabrik füe DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime Create(IDate date, ITime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }


        /// <summary>
        ///Martin Korneffel, 2017
        /// Basiert auf (https://forums.asp.net/t/361390.aspx?DateDiff+function+in+C+): 
        /// 
        /// Copyright © 2003 - 2013 Tangible Software Solutions Inc.
        /// This class can be used by anyone provided that the copyright notice remains intact.
        ///
        /// This class simulates the behavior of the classic VB 'DateDiff' function. 
        /// </summary>
        /// <param name="dateOne"></param>
        /// <param name="dateTwo"></param>
        /// <param name="intervalType"></param>
        /// <returns></returns>
        public static int DateDiff(this DateTime dateTwo, System.DateTime dateOne, DateDiffInterval intervalType)
        {
            switch (intervalType)
            {
                case DateDiffInterval.Day:
                case DateDiffInterval.DayOfYear:
                    System.TimeSpan spanForDays = dateTwo - dateOne;
                    return (int)spanForDays.TotalDays;
                case DateDiffInterval.Hour:
                    System.TimeSpan spanForHours = dateTwo - dateOne;
                    return (int)spanForHours.TotalHours;
                case DateDiffInterval.Minute:
                    System.TimeSpan spanForMinutes = dateTwo - dateOne;
                    return (int)spanForMinutes.TotalMinutes;
                case DateDiffInterval.Month:
                    return ((dateTwo.Year - dateOne.Year) * 12) + (dateTwo.Month - dateOne.Month);
                case DateDiffInterval.Quarter:
                    int dateOneQuarter = (int)System.Math.Ceiling(dateOne.Month / 3.0);
                    int dateTwoQuarter = (int)System.Math.Ceiling(dateTwo.Month / 3.0);
                    return (4 * (dateTwo.Year - dateOne.Year)) + dateTwoQuarter - dateOneQuarter;
                case DateDiffInterval.Second:
                    System.TimeSpan spanForSeconds = dateTwo - dateOne;
                    return (int)spanForSeconds.TotalSeconds;
                case DateDiffInterval.Weekday:
                    System.TimeSpan spanForWeekdays = dateTwo - dateOne;
                    return (int)(spanForWeekdays.TotalDays / 7.0);
                case DateDiffInterval.WeekOfYear:
                    System.DateTime dateOneModified = dateOne;
                    System.DateTime dateTwoModified = dateTwo;
                    while (dateTwoModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateTwoModified = dateTwoModified.AddDays(-1);
                    }
                    while (dateOneModified.DayOfWeek != System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek)
                    {
                        dateOneModified = dateOneModified.AddDays(-1);
                    }
                    System.TimeSpan spanForWeekOfYear = dateTwoModified - dateOneModified;
                    return (int)(spanForWeekOfYear.TotalDays / 7.0);
                case DateDiffInterval.Year:
                    return dateTwo.Year - dateOne.Year;
                default:
                    return 0;
            }
        }


        /// <summary>
        /// Präsentation einer Datumsdifferenz als "Zahl"
        /// Die Stellen bilden die Anteile Jahre, Monate, Tage...
        /// Dabei gilt: 1 Jahr > Monat x Monate, ...
        /// </summary>
        public struct DateDiffNumber
        {
            public int Years;
            public int Month;
            public int Days;
            public int Hours;
            public int Minutes;
            public int Secounds;
        }

        /// <summary>
        /// Gibt eine Datumsdifferenz als "Zahl" zurück. 
        /// </summary>
        /// <param name="dateOne"></param>
        /// <param name="dateTwo"></param>
        /// <returns></returns>
        public static DateDiffNumber DateDiff(this DateTime dateOne, DateTime dateTwo)
        {
            var diff = new DateDiffNumber();

            diff.Years = dateOne.DateDiff(dateTwo, DateDiffInterval.Year);
            var Rest = dateTwo.AddYears(diff.Years);

            diff.Month = dateOne.DateDiff(Rest, DateDiffInterval.Month);
            Rest = Rest.AddMonths(diff.Month);

            diff.Days = dateOne.DateDiff(Rest, DateDiffInterval.Day);
            Rest = Rest.AddDays(diff.Days);

            diff.Hours = dateOne.DateDiff(Rest, DateDiffInterval.Hour);
            Rest = Rest.AddHours(diff.Hours);

            diff.Minutes = dateOne.DateDiff(Rest, DateDiffInterval.Minute);
            Rest = Rest.AddMinutes(diff.Minutes);

            diff.Secounds = dateOne.DateDiff(Rest, DateDiffInterval.Second);

            return diff;

        }

    }
}
