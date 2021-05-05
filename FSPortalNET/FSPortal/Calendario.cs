// // <fileheader>
// // <copyright file="Calendario.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Text;
using FSLibrary;
using FSPortal;

#endregion

namespace FSPortal
{
    public class Calendario
    {
        //private static string LeerEvento(int ano, int mes, int dia)
        //{
        //    string ssql = "select titulo,fecha from " + Variables.App.prefijoTablas + "Eventos where fecha=" + BdUtils.SimbDate + mes +
        //                  "/" + dia + "/" + ano + BdUtils.SimbDate + " and repetir=false";

        //    BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
        //    DataTable dtCalendario = db.Execute(ssql);

        //    StringBuilder sb = new StringBuilder("");
        //    foreach (DataRow row in dtCalendario.Rows)
        //    {
        //        sb.Append(UI.Lf() + FuncDate.ValorFecha(row["fecha"]).ToShortTimeString() + "- " +
        //                  Functions.Valor(row["titulo"]));
        //    }

        //    string evanual = null;
        //    evanual = LeerEventoRepetido(mes, dia);
        //    if (evanual != "")
        //    {
        //        sb.Append("Eventos Anuales: " + UI.Lf() + evanual);
        //    }

        //    return sb.ToString();
        //}


        //private static string LeerEventoRepetido(int mes, int dia)
        //{
        //    string ssql = "select titulo,fecha from " + Variables.App.prefijoTablas + "Eventos where repetir=true and day(fecha)=" +
        //                  dia + " and month(fecha)=" + mes;

        //    BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
        //    DataTable dtCalendario = db.Execute(ssql);
        //    StringBuilder sb = new StringBuilder("");

        //    foreach (DataRow row in dtCalendario.Rows)
        //    {
        //        sb.Append(UI.Lf() + Functions.Valor(row["titulo"]));
        //    }

        //    return sb.ToString();
        //}


        public string CrearCalendario(int anny, int mes, int dia, string urldestino, int modo)
        {
            int row;
            StringBuilder sb = new StringBuilder("");


            int myMonth = mes;
            int myYear = anny;

            if (myMonth == 0)
            {
				myMonth = System.DateTime.Now.Month;
            }
            if (myYear == 0)
            {
				myYear = System.DateTime.Now.Year;
            }

            sb.Append(CalendarHeader(myMonth, myYear, modo, urldestino));


			int firstDay = NumberUtils.NumberInt(System.DateTime.Parse("1/" + myMonth + "/" + myYear).DayOfWeek) + 1 - 2;
            if (firstDay == -1)
            {
                firstDay = 6;
            }
            int currentDay = 1;

            for (row = 0; row <= 5; row++)
            {
                sb.Append("<tr>");
                int col;
                for (col = 0; col <= 6; col++)
                {
                    if (((row == 0) & (col < firstDay)))
                    {
                        sb.Append("<td bgcolor='#FFFFFF' class='calSimbolo' width='15%'>&nbsp;</td>");
                    }
                    else if ((currentDay > LastDay(myMonth, myYear)))
                    {
                        sb.Append("<td bgcolor='#FFFFFF' class='calSimbolo' width='15%'>&nbsp;</td>");
                    }
                    else
                    {
                        if (modo == 2)
                        {
							if ((System.DateTime.Now.Year == myYear) & (System.DateTime.Now.Month == myMonth) &
								(System.DateTime.Now.Day == currentDay))
                            {
                                sb.Append(
                                    "<td bgcolor='#FFFFFF' class='calCeldaResaltado' align='center' width='15%' valign='top'>");
                            }
                            else
                            {
                                sb.Append("<td bgcolor='#FFFFFF' align='center' width='15%' valign='top'>");
                            }
                        }
                        else
                        {
                            if ((myYear == anny) & (myMonth == mes) & (currentDay == dia))
                            {
                                sb.Append(
                                    "<td bgcolor='#FFFFFF' class='calCeldaResaltado' align='center' width='15%' valign='top'>");
                            }
                            else
                            {
                                sb.Append("<td bgcolor='#FFFFFF' align='center' width='15%' valign='top'>");
                            }
                        }

                        if (modo == 2)
                        {
							if ((System.DateTime.Now.Year == myYear) & (System.DateTime.Now.Month == myMonth) &
								(System.DateTime.Now.Day == currentDay))
                            {
                                sb.Append("<div class='calResaltado'>");
                            }
                            else
                            {
                                sb.Append("<div class='calSimbolo'>");
                            }
                        }
                        else
                        {
                            if ((myYear == anny) & (myMonth == mes) & (currentDay == dia))
                            {
                                sb.Append("<div class='calResaltado'>");
                            }
                            else
                            {
                                sb.Append("<div class='calSimbolo'>");
                            }
                        }

                        string lk = urldestino + "?day=" + currentDay + "&amp;month=" + myMonth + "&amp;year=" + myYear;

                        // comento la lectura de los eventos ya que hay que optimizarlo para que no acceda a la bd tantas veces.
                        //string evento = "";

                        //evento = LeerEvento(MyYear, MyMonth, CurrentDay);

                        //if (evento != "")
                        //{
                        //    if (modo == 0 | modo == 2)
                        //    {
                        //        sb.Append("<b>");
                        //    }
                        //    else
                        //    {
                        //        sb.Append("<b><span style='color: #00CC33'>");
                        //    }
                        //}

						System.DateTime t = System.DateTime.Parse(currentDay + "/" + myMonth + "/" + myYear);
                        bool b = (NumberUtils.NumberInt(t.DayOfWeek) + 1 == 1 | NumberUtils.NumberInt(t.DayOfWeek) + 1 == 7);

                        sb.Append(b
                            ? Ui.Bold(Ui.Italic(Ui.Link(currentDay.ToString(), lk)))
                            : Ui.Link(currentDay.ToString(), lk));

                        //if (evento != "")
                        //{
                        //    if (modo == 0 | modo == 2)
                        //    {
                        //        sb.Append("</b>");
                        //    }
                        //    else
                        //    {
                        //        sb.Append("</span></b>" + UI.Lf() + evento);
                        //    }
                        //}

                        sb.Append("</div></td>");
                        currentDay = currentDay + 1;
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            if (modo != 2)
            {
                sb.Append(
                    Ui.Center(@"<img alt='' border=""0"" src='" + Variables.App.directorioPortal + "imagenes/calendario.gif" +
                              "' /> <a href='#' onclick='javascript:Hoy(calendario" + modo + ")'>Fecha actual</a>"));
            }
            sb.Append("</form>");

            return sb.ToString();
        }


        private string CalendarHeader(int myMonth, int myYear, int modo, string urlDestino)
        {
            int i;
            string cadena;

            string calendarHeaderReturn = "";

            if (modo != 2)
            {
                calendarHeaderReturn = calendarHeaderReturn + "<form name='calendario" + modo + "' action='" +
                                       urlDestino + "' method='get'>";
            }
            calendarHeaderReturn = calendarHeaderReturn +
                                   "<table border='0' cellspacing='1' cellpadding='1' align='center' class='calFondoCalendario' width='100%'>";
            calendarHeaderReturn = calendarHeaderReturn + " <tr align='center'> ";
            calendarHeaderReturn = calendarHeaderReturn + "     <td colspan='7'>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   "         <table border='0' cellspacing='1' cellpadding='1' width='100%' class='calFondoEncabe'>";
            calendarHeaderReturn = calendarHeaderReturn + "             <tr>";
            calendarHeaderReturn = calendarHeaderReturn + "                 <td align='center' class='calDatos'>";
            if (modo != 2)
            {
                calendarHeaderReturn = calendarHeaderReturn + "                 Mes:";
                calendarHeaderReturn = calendarHeaderReturn +
                                       "                     <select name='Month' onchange='javascript:document.calendario" +
                                       modo + ".submit();' class='calDatos'>";
                for (i = 1; i <= 12; i++)
                {
                    cadena = "";
                    if ((myMonth == i))
                    {
                        cadena = @"selected=""selected""";
                    }
                    calendarHeaderReturn = calendarHeaderReturn + "<option value='" + i + "' " + cadena + ">" +
                                           MonthName(i) + "</option>";
                }
                calendarHeaderReturn = calendarHeaderReturn + "                     </select>";
            }
            else
            {
                calendarHeaderReturn = calendarHeaderReturn + " " + MonthName(myMonth);
            }
            calendarHeaderReturn = calendarHeaderReturn + "                 </td>";
            calendarHeaderReturn = calendarHeaderReturn + "                 <td align='center' class='calDatos'>";
            if (modo != 2)
            {
                calendarHeaderReturn = calendarHeaderReturn + "                 Año:";
                calendarHeaderReturn = calendarHeaderReturn +
                                       "                     <select name='Year' onchange='javascript:document.calendario" +
                                       modo + ".submit();' class='calDatos'>";
				int anyActual = System.DateTime.Now.Year;
                for (i = 1; i <= 20; i++)
                {
                    cadena = "";
                    int anys = (anyActual - 10) + i;
                    if ((myYear == anys))
                    {
                        cadena = @"selected=""selected""";
                    }
                    calendarHeaderReturn = calendarHeaderReturn + "<option value='" + anys + "' " + cadena + ">" + anys +
                                           "</option>";
                }
                calendarHeaderReturn = calendarHeaderReturn + "                     </select>";
            }
            calendarHeaderReturn = calendarHeaderReturn + "                 </td>";
            calendarHeaderReturn = calendarHeaderReturn + "             </tr>";
            calendarHeaderReturn = calendarHeaderReturn + "         </table>";
            calendarHeaderReturn = calendarHeaderReturn + "     </td>";
            calendarHeaderReturn = calendarHeaderReturn + " </tr>";
            calendarHeaderReturn = calendarHeaderReturn + " <tr align='center'> ";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">L</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">M</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">X</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">J</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">V</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">S</div></td>";
            calendarHeaderReturn = calendarHeaderReturn +
                                   @"     <td bgcolor='#FFCC99'><div class='calDias' style=""width:'15%'"">D</div></td>";
            calendarHeaderReturn = calendarHeaderReturn + " </tr>";
            return calendarHeaderReturn;
        }


        public string MonthName(int myMonth)
        {
            string monthNameReturn;
            switch (myMonth)
            {
                case 1:
                    monthNameReturn = "Enero";
                    break;
                case 2:
                    monthNameReturn = "Febrero";
                    break;
                case 3:
                    monthNameReturn = "Marzo";
                    break;
                case 4:
                    monthNameReturn = "Abril";
                    break;
                case 5:
                    monthNameReturn = "Mayo";
                    break;
                case 6:
                    monthNameReturn = "Junio";
                    break;
                case 7:
                    monthNameReturn = "Julio";
                    break;
                case 8:
                    monthNameReturn = "Agosto";
                    break;
                case 9:
                    monthNameReturn = "Septiembre";
                    break;
                case 10:
                    monthNameReturn = "Octubre";
                    break;
                case 11:
                    monthNameReturn = "Noviembre";
                    break;
                case 12:
                    monthNameReturn = "Diciembre";
                    break;
                default:
                    monthNameReturn = "ERROR!";
                    break;
            }

            return monthNameReturn;
        }


        private static int LastDay(int myMonth, int myYear)
        {
            int lastDayReturn;
            switch (myMonth)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    lastDayReturn = 31;

                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    lastDayReturn = 30;

                    break;
                case 2:
                    lastDayReturn = FSLibrary.DateTimeUtil.IsDate(myYear + "-" + myMonth + "-" + "29") ? 29 : 28;

                    break;
                default:
                    lastDayReturn = 0;
                    break;
            }

            return lastDayReturn;
        }
    }
}