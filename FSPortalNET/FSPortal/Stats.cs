// // <fileheader>
// // <copyright file="Stats.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Data;
using System.Text;
using FSLibrary;
using FSDatabase;

#endregion

namespace FSPortal
{
    public class Stats
    {
		private readonly BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
        private DataTable dt;
		private System.DateTime dtBegin;
		private System.DateTime dtYesterday;
        private int lDescargas;
        private int lEnlacesToday;
        private int lEnlacesTotal;
        private int lEnlacesYesterday;
        private int lErroresToday;
        private int lErroresTotal;
        private int lErroresYesterday;
        private int lPaginasVistas;
        private int lPaginasVistasHoy;
        private int lPaginasVistasTotal;
        private int lPaginasVistasYesterday;
        private int lPreguntasToday;
        private int lPreguntasTotal;
        private int lPreguntasYesterday;
        private int lRecsToday;
        private int lRecsTotal;
        private int lRecsYesterday;
        private int lRespuestasToday;
        private int lRespuestasTotal;
        private int lRespuestasYesterday;
        private int lTotalDescargas;
        private int lTotalPaginasVistas;
        private int lVisitorsToday;
        private int lVisitorsTotal;
        private int lVisitorsYesterday;
        private string sOnline;
        private string sPaginasVistasHoy;
        private string sPaginasVistasTotal;
        private string sPaginasVistasYesterday;

        private string sSQL;
        private string sThisServer;
        private string sToday;
        private string sUpTime;
        private string sVisitorsToday;
        private string sVisitorsTotal;
        private string sVisitorsYesterday;
        private string sYesterday;


        public string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            LoadVariables();

            sb.Append("<p class='textomaspeque'>");
            sb.Append("El portal esta activos desde " + sOnline + Ui.Lf() + Ui.Lf());
            sb.Append("El servidor esta activo desde " + sUpTime);
            sb.Append("</p>");

            sb.Append(@"<table border=""0"" cellspacing=""1"" width=""400"">");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("<td align=right class='cabemaspeque'>Páginas Vistas</td>");
            sb.Append("<td align=right class='cabemaspeque'>Visitantes</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Hoy</td>");
            sb.Append("<td align=right>" + sPaginasVistasHoy + "</td>");
            sb.Append("<td align=right>" + sVisitorsToday + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Ayer</td>");
            sb.Append("<td align=right>" + sPaginasVistasYesterday + "</td>");
            sb.Append("<td align=right>" + sVisitorsYesterday + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Total</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + sPaginasVistasTotal + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + sVisitorsTotal + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<p class='textomaspeque'>");
			sb.Append("Fecha del servidor : " + System.DateTime.Now.ToShortTimeString());
            sb.Append("</p>");


            sb.Append(@"<table border=""0"" cellspacing=""1"" width=""400"">");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>&nbsp;</td>");
            sb.Append("<td class='cabemaspeque' align=right>Errores</td>");
            sb.Append("<td class='cabemaspeque' align=right>Preguntas</td>");
            sb.Append("<td class='cabemaspeque' align=right>Respuestas</td>");
            sb.Append("<td class='cabemaspeque' align=right>Recomendaciones</td>");
            sb.Append("<td class='cabemaspeque' align=right>Enlaces</td>");
            sb.Append("</tr>");


            sb.Append("<tr>");
            sb.Append("<td>Hoy</td>");
            sb.Append("<td align=right>" + lErroresToday + "</td>");
            sb.Append("<td align=right>" + lPreguntasToday + "</td>");
            sb.Append("<td align=right>" + lRespuestasToday + "</td>");
            sb.Append("<td align=right>" + lRecsToday + "</td>");
            sb.Append("<td align=right>" + lEnlacesToday + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td>Ayer</td>");
            sb.Append("<td align=right>" + lErroresYesterday + "</td>");
            sb.Append("<td align=right>" + lPreguntasYesterday + "</td>");
            sb.Append("<td align=right>" + lRespuestasYesterday + "</td>");
            sb.Append("<td align=right>" + lRecsYesterday + "</td>");
            sb.Append("<td align=right>" + lEnlacesYesterday + "</td>");
            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Total</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lErroresTotal + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lPreguntasTotal + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lRespuestasTotal + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lRecsTotal + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lEnlacesTotal + "</td>");
            sb.Append("</tr>");

            sb.Append("</table>");

            sb.Append(Ui.Lf() + Ui.Lf());
            sb.Append(@"<table border=""0"" cellspacing=""1"" width=""400"">");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Temas</td>");
            sb.Append("<td class='cabemaspeque' align=right>Descargas</td>");
            sb.Append("<td class='cabemaspeque' align=right>Páginas Vistas</td>");
            sb.Append("</tr>");

            if (db.TableExists(Variables.App.prefijoTablas + "Temas"))
                sb.Append(PagVistas());

            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Total</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lTotalDescargas + "</td>");
            sb.Append("<td class='cabemaspeque' align=right>" + lTotalPaginasVistas + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            sb.Append(Ui.Lf() + Ui.Lf());
            sb.Append(@"<table border=""0"" cellspacing=""1"" width=""400"">");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Documentos</td>");
            sb.Append("<td class='cabemaspeque' align=right>Páginas Vistas</td>");
            sb.Append("</tr>");

            if (db.TableExists(Variables.App.prefijoTablas + "Documentos"))
                sb.Append(Docs());

            sb.Append("</table>");

            sb.Append(Ui.Lf() + Ui.Lf());

            sb.Append(Ui.Lf() + Ui.Lf());
            sb.Append(@"<table border=""0"" cellspacing=""1"" width=""400"">");
            sb.Append("<tr>");
            sb.Append("<td class='cabemaspeque'>Referencias</td>");
            sb.Append("<td class='cabemaspeque' align=right>Total</td>");

            sb.Append("</tr>");
            sb.Append(Refs());
            sb.Append("</table>");

            sb.Append("<br />This application has been accessed " + Variables.App.Page.Application["ApplicationCounter"] + " times<br />");
            sb.Append("There are " + Variables.App.Page.Application["NumberOfUsers"] + " users accessing this application");


            return sb.ToString();
        }

        private void LoadVariables()
        {
            sSQL = "SELECT COUNT(StatID) AS Total FROM " + Variables.App.prefijoTablas + "Stats";
            lPaginasVistasTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

            sToday = Utils.FormatShortDate(System.DateTime.Now);
            sSQL = "SELECT COUNT(StatID) AS Total FROM " + Variables.App.prefijoTablas + "Stats WHERE Date = " +
                   sToday;
            lPaginasVistasHoy = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

			dtYesterday = System.DateTime.Now.Subtract(new TimeSpan(24, 0, 0));
			sYesterday = Utils.FormatShortDate(System.DateTime.Now);
            sSQL = "SELECT COUNT(StatID) AS Total FROM " + Variables.App.prefijoTablas + "Stats WHERE Date = " +
                   sYesterday;
            lPaginasVistasYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

            sSQL = "SELECT count(IP) as total FROM " + Variables.App.prefijoTablas + "Stats GROUP BY IP";
            lVisitorsTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

			sToday = Utils.FormatShortDate(System.DateTime.Now);
            sSQL = "SELECT count(IP) as total FROM " + Variables.App.prefijoTablas + "Stats WHERE Date = " + sToday +
                    " GROUP BY IP";
            lVisitorsToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

            sSQL = "SELECT count(IP) as total FROM " + Variables.App.prefijoTablas + "Stats WHERE Date = " +
                   sYesterday + " GROUP BY IP";
            lVisitorsYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

            sPaginasVistasHoy = lPaginasVistasHoy.ToString();
            sPaginasVistasYesterday = lPaginasVistasYesterday.ToString();
            sPaginasVistasTotal = lPaginasVistasTotal.ToString();

            sVisitorsToday = lVisitorsToday.ToString();
            sVisitorsYesterday = lVisitorsYesterday.ToString();
            sVisitorsTotal = lVisitorsTotal.ToString();

			dtBegin = new System.DateTime(2000, 1, 1); // esta fecha la debería coger del web.config
			sOnline = "<strong>" + dtBegin.Subtract(System.DateTime.Now) + "</strong> días, <strong>" +
				dtBegin.Subtract(System.DateTime.Now) + "</strong> horas.";
			sUpTime = "<strong>" + Convert.ToDateTime(Variables.App.Page.Application["ServerStartTime"]).Subtract(System.DateTime.Now) +
                      "</strong> días, <strong>" +
				Convert.ToDateTime(Variables.App.Page.Application["ServerStartTime"]).Subtract(System.DateTime.Now) +
                      "</strong> horas.";

            //errores
            if (db.TableExists(Variables.App.prefijoTablas + "Errores"))
            {
                sSQL = "SELECT COUNT(ErrorID) AS Total FROM " + Variables.App.prefijoTablas + "Errores WHERE FechaEnvio = " +
                        sToday;
                lErroresToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(ErrorID) AS Total FROM " + Variables.App.prefijoTablas + "Errores WHERE FechaEnvio = " +
                       sYesterday;
                lErroresYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(ErrorID) AS Total FROM " + Variables.App.prefijoTablas + "Errores";
                lErroresTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));
            }

            //Preguntas
            if (db.TableExists(Variables.App.prefijoTablas + "Preguntas"))
            {
                sSQL = "SELECT COUNT(PreguntaID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Preguntas WHERE FechaPregunta = " + sToday;
                lPreguntasToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(PreguntaID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Preguntas WHERE FechaPregunta = " + sYesterday;
                lPreguntasYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(PreguntaID) AS Total FROM " + Variables.App.prefijoTablas + "Preguntas";
                lPreguntasTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));
            }

            //Respuestas
            if (db.TableExists(Variables.App.prefijoTablas + "Respuestas"))
            {
                sSQL = "SELECT COUNT(RespuestaID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Respuestas WHERE FechaRespuesta = " + sToday;
                lRespuestasToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(RespuestaID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Respuestas WHERE FechaRespuesta = " + sYesterday;
                lRespuestasYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(RespuestaID) AS Total FROM " + Variables.App.prefijoTablas + "Respuestas";
                lRespuestasTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));
            }

            //Recomendaciones 
            if (db.TableExists(Variables.App.prefijoTablas + "Recomendaciones"))
            {
                sSQL = "SELECT COUNT(RecomendacionID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Recomendaciones WHERE FechaRecomendacion = " + sToday;
                lRecsToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(RecomendacionID) AS Total FROM " + Variables.App.prefijoTablas +
                       "Recomendaciones WHERE FechaRecomendacion = " + sYesterday;
                lRecsYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(RecomendacionID) AS Total FROM " + Variables.App.prefijoTablas + "Recomendaciones";
                lRecsTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));
            }

            //Enlaces 
            if (db.TableExists(Variables.App.prefijoTablas + "Enlaces"))
            {
                sSQL = "SELECT COUNT(EnlaceID) AS Total FROM " + Variables.App.prefijoTablas + "Enlaces WHERE FechaEnvio = " +
                        sToday;
                lEnlacesToday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(EnlaceID) AS Total FROM " + Variables.App.prefijoTablas + "Enlaces WHERE FechaEnvio = " +
                       sYesterday;
                lEnlacesYesterday = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));

                sSQL = "SELECT COUNT(EnlaceID) AS Total FROM " + Variables.App.prefijoTablas + "Enlaces";
                lEnlacesTotal = NumberUtils.NumberInt(db.ExecuteScalar(sSQL));
            }
        }


        private string PagVistas()
        {
            StringBuilder sb = new StringBuilder("");

            sSQL = "SELECT Nombre, Descargas, PaginasVistas FROM " + Variables.App.prefijoTablas +
                   "Temas ORDER BY PaginasVistas DESC";
            dt = db.Execute(sSQL);

            foreach (DataRow row in dt.Rows)
            {
                lDescargas = NumberUtils.NumberInt(row["Descargas"]);
                lPaginasVistas = NumberUtils.NumberInt(row["PaginasVistas"]);
                lTotalDescargas = lTotalDescargas + lDescargas;
                lTotalPaginasVistas = lTotalPaginasVistas + lPaginasVistas;

                sb.Append("<tr><td>" + NumberUtils.NumberInt(row["nombre"]) + "</td>");
                sb.Append("<td align=right>" + lDescargas + "</td>");
                sb.Append("<td align=right>" + lPaginasVistas + "</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }

        private string Docs()
        {
            StringBuilder sb = new StringBuilder("");

            sSQL = "select " + Variables.App.prefijoTablas + "Temas.Nombre as temnombre, " + Variables.App.prefijoTablas +
                   "Documentos.Nombre as nomdoc, " + Variables.App.prefijoTablas + "Documentos.PaginasVistas as pagvistas from " +
                   Variables.App.prefijoTablas + "Documentos left join " + Variables.App.prefijoTablas + "Temas on (" + Variables.App.prefijoTablas +
                   "Documentos.TemaID = " + Variables.App.prefijoTablas + "Temas.TemaID) order by " + Variables.App.prefijoTablas +
                   "Documentos.PaginasVistas desc, " + Variables.App.prefijoTablas + "Temas.Nombre";
            dt = db.Execute(sSQL);

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + row["TemNombre"] + "::" + row["nomdoc"] + "</td>");
                sb.Append("<td align=right>" + row["pagvistas"] + "</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }

        private string Refs()
        {
            StringBuilder sb = new StringBuilder("");

            sThisServer = Variables.App.nombreWeb;

            //Get referers other than the Enlace itself 
            if (Utils.BDType == Utils.TypeBd.SQLServer)
            {
                sSQL = "SELECT TOP 20 RefName, Total FROM " + Variables.App.prefijoTablas + "StatsRefs WHERE RefName like '%" +
                       sThisServer + "%' ORDER BY Total DESC";
            }
            else
            {
                sSQL = "SELECT TOP 20 RefName, Total FROM " + Variables.App.prefijoTablas + "StatsRefs WHERE Instr(RefName, '" +
                       sThisServer + "') = 0 ORDER BY Total DESC";
            }

            dt = db.Execute(sSQL);

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + row["RefName"] + "</td>");
                sb.Append("<td align=right>" + row["Total"] + "</td>");
                sb.Append("</tr>");
            }

            return sb.ToString();
        }
    }
}