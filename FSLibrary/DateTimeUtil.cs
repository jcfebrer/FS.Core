// // <fileheader>
// // <copyright file="FuncionesFecha.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using FSException;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace FSLibrary
{
    /// <summary>
    /// Clase para el manejo de fechas.
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// Fecha mínima que admite los controles de fecha de infragistics
        /// </summary>
        public static System.DateTime MinDateInfragistics = new System.DateTime(1753, 01, 01);

        /// <summary>
        /// Converts date to iso_8601.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ToISO_8601(string date)
        {
            string data;
            Regex reg = new Regex("#(?<fecha>[^#]*)#");

            MatchCollection matchCollection = reg.Matches(date);

            foreach (Match match in matchCollection)
            {
                data = match.Groups["fecha"].Value;
                System.DateTime newDate = Convert.ToDateTime(data);
                date = TextUtil.Replace(date, data, newDate.ToString("o"));
            }
            return date;
        }

        /// <summary>
        /// Converts date to rfc_822.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ToRFC_822(System.DateTime date)
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours;
            var timeZone = "+" + offset.ToString().PadLeft(2, '0');

            if (offset < 0)
            {
                var i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }

            return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'));
        }


        /// <summary>
        /// Gets the name of the day.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <returns></returns>
        public static string GetFirstDayName(int year, int month, int day)
        {
            var dateTime = new System.DateTime(year, month, day);

            string dayName = DayName(dateTime.DayOfWeek);
            return dayName.Substring(1);
        }

        /// <summary>
        /// Devuelve la fecha en formato fecha corto.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string ShortDate(System.DateTime date)
        {
            var dat = date.ToShortDateString();

            //if (BDType == FSLibrary.Enum.BDType.MySQL)
            //    dat = date.ToString("yyyy-MM-dd");

            return dat;
        }


        /// <summary>
        /// Devuelve la fecha en formato largo.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string LongDate(System.DateTime date)
        {
            var dat = date.ToLongDateString();

            //if (BDType == FSLibrary.Enum.BDType.MySQL)
            //    dat = date.ToString("yyyy-MM-dd");

            return dat;
        }


        /// <summary>
        /// Días totales entre dos fechas.
        /// </summary>
        /// <param name="date1">Fecha 1.</param>
        /// <param name="date2">Fecha 2</param>
        /// <returns></returns>
        public static double TotalDays(System.DateTime date1, System.DateTime date2)
        {
            return (date2 - date1).TotalDays;
        }

        /// <summary>
        /// Totals the milliseconds.
        /// </summary>
        /// <param name="date1">The date1.</param>
        /// <param name="date2">The date2.</param>
        /// <returns></returns>
        public static int TotalMilliseconds(System.DateTime date1, System.DateTime date2)
        {
            return (date2 - date1).Milliseconds;
        }

        /// <summary>
        /// Dates the difference month.
        /// </summary>
        /// <param name="lValue">The l value.</param>
        /// <param name="rValue">The r value.</param>
        /// <returns></returns>
        public static int DateDiffMonth(System.DateTime lValue, System.DateTime rValue)
        {
            return Math.Abs(lValue.Month - rValue.Month + 12 * (lValue.Year - rValue.Year));
        }

        /// <summary>
        /// Devuelve la fecha en formato DateTime.
        /// </summary>
        /// <param name="fecha">Fecha.</param>
        /// <returns></returns>
        public static System.DateTime ValorFecha(string fecha)
        {
            return ValorFecha(fecha, System.DateTime.Now);
        }

        /// <summary>
        /// Devuelve la fecha en formato DateTime.
        /// </summary>
        /// <param name="fecha">Fecha.</param>
        /// <param name="defaultValue">Fecha por defecto.</param>
        /// <returns></returns>
        public static System.DateTime ValorFecha(string fecha, System.DateTime defaultValue)
        {
            if (fecha == null)
                return defaultValue;
            if (string.IsNullOrEmpty(fecha.ToString()))
                return defaultValue;
            if (fecha.ToString() == "01/01/0001 0:00:00")
                return defaultValue;

            return System.DateTime.Parse(fecha.ToString());
        }

        /// <summary>
        /// Determines whether the specified date is date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>
        ///   <c>true</c> if the specified date is date; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return false;

            if (date.Length < 6)
                return false;

            System.DateTime dateTime;
            if (System.DateTime.TryParse(date, out dateTime))
                return true;
            return false;
        }


        /// <summary>
        /// Devuelve el cuatrimestre de la fecha.
        /// </summary>
        /// <param name="date">Fecha.</param>
        /// <returns></returns>
        public static int Quarter(System.DateTime date)
        {
            var i = date.Month;

            if (i <= 3)
                return 1;
            if ((i >= 4) & (i <= 6))
                return 2;
            if ((i >= 7) & (i <= 9))
                return 3;
            if ((i >= 10) & (i <= 12))
                return 4;
            return 0;
        }


        /// <summary>
        /// Devuelve los segundos hasta hoy.
        /// </summary>
        /// <returns></returns>
        public static double Seconds()
        {
            var st = System.DateTime.Now.Subtract(System.DateTime.Today);

            return st.Duration().TotalMilliseconds / 1000;
        }


        /// <summary>
        /// Devuelve la semana de la fecha.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static int Week(System.DateTime date)
        {
            var d = new DateTimeFormatInfo();

            return d.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// Devuelve el día de la semana en formato texto.
        /// </summary>
        /// <param name="dia">Día de la semana</param>
        /// <returns></returns>
        public static string DayName(DayOfWeek dia)
        {
            var day = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dia);

            return day;
        }

        /// <summary>
        /// Devuelve el nombre del mes en formato texto.
        /// </summary>
        /// <param name="mes">Número del mes.</param>
        /// <returns></returns>
        public static string MonthName(int mes)
        {
            string fullMonthName = new DateTime(2015, mes, 1).ToString("MMMM", CultureInfo.CurrentCulture);
            return fullMonthName;
        }

        /// <summary>
        /// Devuelve el nombre del mes en formato texto con 3 carácteres.
        /// </summary>
        /// <param name="mes">Número del mes.</param>
        /// <returns></returns>
        public static string ShortMonthName(int mes)
        {
            string shortMonthName = new DateTime(2015, mes, 1).ToString("MMM", CultureInfo.CurrentCulture);
            return shortMonthName;
        }


        /// <summary>
        /// Convert string date UTC standard datetime.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime UtcToDateTime(string date)
        {
            date = date.Replace(" (PST)", ""); //ignoramos esta cadena
            DateTime dateTime = DateTimeOffset.Parse(date).UtcDateTime;
            return dateTime.ToLocalTime();
        }


        /// <summary>
        /// Convert string date UTC standard datetime (OBSOLETE).
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        /// <exception cref="System.FormatException">
        /// Unknown month.
        /// or
        /// Incompatible date/time string format
        /// </exception>
        public static DateTime UtcDateTime2(string str)
        {
            Regex UtcDateTimeRegex = new Regex(@"^(?:\w+,\s+)?(?<day>\d+)\s+(?<month>\w+)\s+(?<year>\d+)\s+(?<hour>\d{1,2}):(?<minute>\d{1,2}):(?<second>\d{1,2})\s+(?<offsetsign>\-|\+)(?<offsethours>\d{2,2})(?<offsetminutes>\d{2,2})(?:.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Match m = UtcDateTimeRegex.Match(str);
            int day, month, year, hour, min, sec;
            if (m.Success)
            {
                day = Convert.ToInt32(m.Groups["day"].Value);
                year = Convert.ToInt32(m.Groups["year"].Value);
                hour = Convert.ToInt32(m.Groups["hour"].Value);
                min = Convert.ToInt32(m.Groups["minute"].Value);
                sec = Convert.ToInt32(m.Groups["second"].Value);
                switch (m.Groups["month"].Value)
                {
                    case "Ene":
                    case "Jan":
                        month = 1;
                        break;
                    case "Feb":
                        month = 2;
                        break;
                    case "Mar":
                        month = 3;
                        break;
                    case "Abr":
                    case "Apr":
                        month = 4;
                        break;
                    case "May":
                        month = 5;
                        break;
                    case "Jun":
                        month = 6;
                        break;
                    case "Jul":
                        month = 7;
                        break;
                    case "Ago":
                    case "Aug":
                        month = 8;
                        break;
                    case "Sep":
                        month = 9;
                        break;
                    case "Oct":
                        month = 10;
                        break;
                    case "Nov":
                        month = 11;
                        break;
                    case "Dic":
                    case "Dec":
                        month = 12;
                        break;
                    default:
                        throw new FormatException
                            ("Unknown month.");
                }
                string offsetSign = m.Groups["offsetsign"].Value;
                int offsetHours = Convert.ToInt32(m.Groups
                        ["offsethours"].Value);
                int offsetMinutes = Convert.ToInt32(m.Groups
                        ["offsetminutes"].Value);
                DateTime dt = new DateTime(year, month, day,
                        hour, min, sec);
                if (offsetSign == "+")
                {
                    dt.AddHours(offsetHours);
                    dt.AddMinutes(offsetMinutes);
                }
                else if (offsetSign == "-")
                {
                    dt.AddHours(-offsetHours);
                    dt.AddMinutes(-offsetMinutes);
                }
                return dt;
            }
            throw new FormatException
            ("Incompatible date/time string format");
        }

        /// <summary>
        /// Convierte una cadena de fecha de SQLite a DateTime
        /// Formato SQLite: ISO8601 "YYYY-MM-DD HH:MM:SS.SSS"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ConvertSQLiteToDateTime(string str)
        {
            string pattern = @"(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})\.(\d{3})";
            if (Regex.IsMatch(str, pattern))
            {
                Match match = Regex.Match(str, pattern);
                int year = Convert.ToInt32(match.Groups[1].Value);
                int month = Convert.ToInt32(match.Groups[2].Value);
                int day = Convert.ToInt32(match.Groups[3].Value);
                int hour = Convert.ToInt32(match.Groups[4].Value);
                int minute = Convert.ToInt32(match.Groups[5].Value);
                int second = Convert.ToInt32(match.Groups[6].Value);
                int millisecond = Convert.ToInt32(match.Groups[7].Value);
                return new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            else
            {
                throw new ExceptionUtil("Formato de fecha no válido.");
            }
        }


        /// <summary>
        /// Devuelve la fecha en formato ISO8601 a DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ISO8601ToDateTime(string date)
        {
            if (date == null)
                return DateTime.MinValue;

            if (date.Contains("?"))
                return DateTime.MinValue;

            // Problema de fecha con 5 caracteres
            if (date.Length == 5)
                date = date.Substring(0, 4);

            // Cuando solo viene el año
            if (date.Length == 4)
                date = date + "0101T000000";

            // Si el mes y el dia contiene ceros en vez de 01/01
            if (date.Substring(4, 4) == "0000")
                date = date.Replace("0000T", "0101T");

            return DateTime.ParseExact(date, "yyyyMMddTHHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Devuelve la fecha en formato DateTime a IS8601
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToISO8601(DateTime date)
        {
            return date.ToString("yyyyMMddTHHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Calcula la diferencia en días hábiles (excluyendo sábados y domingos) entre dos fechas.
        /// </summary>
        /// <param name="startD">Fecha inicial.</param>
        /// <param name="endD">Fecha final.</param>
        /// <returns>Número de días hábiles.</returns>
        public static double DateDiffBusinessDays(DateTime startD, DateTime endD)
        {
            DateTime tmp;
            if (startD > endD)
            {
                tmp = endD;
                endD = startD;
                startD = tmp;
            }

            // Iterar sobre cada día en el rango y contar los días que no son fines de semana
            double businessDays = 0;
            for (var date = startD.Date; date <= endD.Date; date = date.AddDays(1))
            {
                if (!date.IsWeekendDay())
                {
                    businessDays++;
                }
            }

            return businessDays;
        }

        /// <summary>
        /// Calcula la diferencia en minutos hábiles (excluyendo sábados y domingos) entre dos fechas.
        /// </summary>
        /// <param name="startD">Fecha inicial.</param>
        /// <param name="endD">Fecha final.</param>
        /// <returns>Número de minutos hábiles.</returns>
        public static double DateDiffBusinessMinutes(DateTime startD, DateTime endD)
        {
            double businessDays = DateDiffBusinessDays(startD, endD);
            return businessDays * 60 * 24;
        }

        /// <summary>
        /// Convierte del formato Date de Android a c#
        /// </summary>
        /// <param name="unixTimeMillis"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long unixTimeMillis)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTimeMillis);
        }

        /// <summary>
        /// Dias entre dos fechas
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysInRangeUntil(this DateTime start, DateTime end)
        {
            return Enumerable.Range(0, 1 + (int)(end.Date - start.Date).TotalDays)
                             .Select(dt => start.Date.AddDays(dt));
        }

        /// <summary>
        /// Devuelve true si la fecha es fin de semana
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWeekendDay(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Devuelve la fecha mayor entre dos fechas.
        /// </summary>
        /// <param name="a">Primera fecha.</param>
        /// <param name="b">Segunda fecha.</param>
        /// <returns>La fecha mayor.</returns>
        public static DateTime Max(DateTime a, DateTime b) => (a > b) ? a : b;

        /// <summary>
        /// Devuelve la fecha menor entre dos fechas.
        /// </summary>
        /// <param name="a">Primera fecha.</param>
        /// <param name="b">Segunda fecha.</param>
        /// <returns>La fecha menor.</returns>
        public static DateTime Min(DateTime a, DateTime b) => (a < b) ? a : b;

        /// <summary>
        /// Devuelve los minutos entre dos fechas con la posibilidad de excluir fines de semana.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="excludeWeekend">Excluir fines de semana</param>
        /// <param name="startHour">Inicio de jornada laboral ej. 7</param>
        /// <param name="endHour">Fin jornada laboral ej. 19</param>
        /// <returns></returns>
        public static double MinutesBetween2Dates(DateTime a, DateTime b, bool excludeWeekend, int startHour = 0, int endHour = 24)
        {
            // Obtener todos los días en el rango [a, b]
            var days = DaysInRangeUntil(a, b);

            // Filtrar fines de semana si es necesario
            if (excludeWeekend)
            {
                days = days.Where(day => !IsWeekendDay(day));
            }

            // Calcular los minutos hábiles
            var minutes = days.Select(day =>
            {
                // Inicio del día hábil (07:00 AM) o el inicio del rango, lo que sea más tarde
                var start = Max(day.Date.AddHours(startHour), a);

                // Fin del día hábil (19:00 PM) o el fin del rango, lo que sea más temprano
                var end = Min(day.Date.AddHours(endHour), b);

                // Asegurar que el rango sea válido
                return (end > start) ? (end - start).TotalMinutes : 0;
            });

            // Retornar la suma total de los minutos
            return minutes.Sum();
        }
    }
}