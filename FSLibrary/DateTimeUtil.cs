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

using System;
using System.Globalization;
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
            var offset = TimeZone.CurrentTimeZone.GetUtcOffset(System.DateTime.Now).Hours;
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
        public static string GetDayName(int year, int month, int day)
        {
            var retval = string.Empty;
            var d = new System.DateTime(year, month, day);
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    retval = "L";
                    break;
                case DayOfWeek.Tuesday:
                    retval = "M";
                    break;
                case DayOfWeek.Wednesday:
                    retval = "M";
                    break;
                case DayOfWeek.Thursday:
                    retval = "J";
                    break;
                case DayOfWeek.Friday:
                    retval = "V";
                    break;
                case DayOfWeek.Saturday:
                    retval = "S";
                    break;
                case DayOfWeek.Sunday:
                    retval = "D";
                    break;
            }

            return retval;
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


        //public static string ShortDateTime(System.DateTime date)
        //{
        //    var dat = date.ToShortDateString();

        //    //if (BDType == FSLibrary.Enum.BDType.MySQL)
        //    //    dat = date.ToString("yyyy-MM-dd");

        //    return dat + " " + date.ToString("HH:mm:ss");
        //}


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


        //public static string SDate(string date)
        //{
        //    if (IsDate(date)) 
        //        return TextUtil.Replace(date, ".", "/");
        //    return date;
        //}

        //public static string TRDate(System.DateTime vDate)
        //{
        //    string tRDateReturn = null;
        //    if (!IsDate(vDate.ToShortDateString()))
        //    {
        //        tRDateReturn = "...";
        //        return tRDateReturn;
        //    }

        //    tRDateReturn = vDate.ToLongDateString();

        //    return tRDateReturn;
        //}

        /// <summary>
        /// Monthes the name.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <returns></returns>
        public static string MonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
            }

            return "Enero";
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
        public static string NombreDia(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";
                case DayOfWeek.Wednesday:
                    return "Miercoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sábado";
            }


            return "error!";
        }

        /// <summary>
        /// Devuelve el nombre del mes en formato texto.
        /// </summary>
        /// <param name="mes">Número del mes.</param>
        /// <returns></returns>
        public static string NombreMes(int mes)
        {
            switch (mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
            }


            return "error!";
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


        //		public static string SDate(string dtDateTime)
        //		{
        //			if (FSLibrary.DateTime.IsDate(dtDateTime)) {
        //				return Text.Replace(dtDateTime, ".", "/");
        //			}
        //			return dtDateTime;
        //		}

        //		public static string TRDate(System.DateTime vDate)
        //		{
        //			string tRDateReturn = null;
        //			if (!FSLibrary.DateTime.IsDate(vDate.ToShortDateString())) {
        //				tRDateReturn = "...";
        //				return tRDateReturn;
        //			}
        //
        //			tRDateReturn = FSLibrary.DateTime.LongDate(vDate);
        //
        //			return tRDateReturn;
        //		}

        //		public static string cDate2(System.DateTime d, string dateFormat, string dateSep)
        //		{
        //			string cDate2Return = null;
        //
        //			switch (dateFormat) {
        //				case "ddmmyyyy":
        //					cDate2Return = d.Day + dateSep + d.Month + dateSep + d.Year;
        //					break;
        //				case "mmddyyyy":
        //					cDate2Return = d.Month + dateSep + d.Day + dateSep + d.Year;
        //					break;
        //				case "yyyymmdd":
        //					cDate2Return = d.Year + dateSep + d.Month + dateSep + d.Day;
        //					break;
        //			}
        //
        //			return cDate2Return;
        //		}


        //public static System.DateTime ToDate(string fecha)
        //{
        //    if (fecha + "" != "")
        //    {
        //        if (IsDate(fecha)) return System.DateTime.Parse(fecha);
        //        return System.DateTime.Parse("1/1/2000");
        //    }

        //    return System.DateTime.Parse("1/1/2000");
        //}


        //public static string DateTimeNum(string strElement)
        //{
        //    var strDateElement = "";

        //    switch (strElement)
        //    {
        //        case "Year":
        //            strDateElement = Functions.Valor(System.DateTime.Now.Year);
        //            break;
        //        case "Month":
        //            strDateElement = Functions.Valor(System.DateTime.Now.Month);
        //            break;
        //        case "Day":
        //            strDateElement = Functions.Valor(System.DateTime.Now.Day);
        //            break;
        //        case "Hour":
        //            strDateElement = Functions.Valor(System.DateTime.Now.Hour);
        //            break;
        //        case "Minute":
        //            strDateElement = Functions.Valor(System.DateTime.Now.Minute);
        //            break;
        //        case "Second":
        //            strDateElement = Functions.Valor(Functions.NumeroEntero(System.DateTime.Now.Second));
        //            break;
        //    }

        //    if (double.Parse(strDateElement) < 10) strDateElement = "0" + strDateElement;

        //    return strDateElement;
        //}
    }
}