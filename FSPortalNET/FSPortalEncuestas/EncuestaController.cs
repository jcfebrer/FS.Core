// <fileheader>
// <copyright file="cast.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: encuestas\cast.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSLibrary;
using FSPortal;
using FSDatabase;

namespace FSPortalEncuestas
{

    /// <summary>
    ///     Encuesta Controller
    /// </summary>
    public class EncuestaController
    {
        public string ShowData(int lEncuestaId, string msg)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            StringBuilder sb = new StringBuilder("");
            sb.Append("<table border=\"0\" cellspacing=\"1\" cellpadding=\"2\" width=\"100%\" bgcolor=\"#ffe4b5\">");
            sb.Append("<tr>");
            sb.Append("<td colspan=\"2\" class=\"header2\">" + msg + "</td>");
            sb.Append("</tr>");

            string sSql = "SELECT COUNT(RespuestaID) AS TotalRespuestasEncuestas FROM " + Variables.App.prefijoTablas +
                          "RespuestasEncuestas WHERE EncuestaID = " + lEncuestaId;
            DataTable dt = db.Execute(sSql);

            double lTot = NumberUtils.NumberDouble(dt.Rows[0]["TotalRespuestasEncuestas"]);

            sSql = "SELECT Count(" + Variables.App.prefijoTablas +
                   "RespuestasEncuestas.OpcionID) AS TotalRespuestasEncuestas, " + Variables.App.prefijoTablas +
                   "OpcionesEncuesta.OpcionText ";
            sSql = sSql + "FROM " + Variables.App.prefijoTablas + "RespuestasEncuestas INNER JOIN " + Variables.App.prefijoTablas +
                   "OpcionesEncuesta ON RespuestasEncuestas.OpcionID = " + Variables.App.prefijoTablas +
                   "OpcionesEncuesta.OpcionID ";
            sSql = sSql + "WHERE " + Variables.App.prefijoTablas + "RespuestasEncuestas.EncuestaID=" + lEncuestaId + " ";
            sSql = sSql + "GROUP BY " + Variables.App.prefijoTablas + "OpcionesEncuesta.OpcionText ";
            sSql = sSql + "ORDER BY Count(" + Variables.App.prefijoTablas + "RespuestasEncuestas.OpcionID) DESC;";

            dt = db.Execute(sSql);

            sb.Append("<tr><td colspan=2 class=\"header2\">Total " + lTot + " voto/s.</td></tr>");

            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td width=\"50%\" class=\"textopeque\">");
                sb.Append(Functions.Valor(row["OpcionText"]) + " (" + NumberUtils.NumberDouble(row["TotalRespuestasEncuestas"]) + ")");
                sb.Append("</td>");
                sb.Append("<td width=\"50%\" class=\"textopeque\" align=\"right\">");

                double perc = ((NumberUtils.NumberDouble(row["TotalRespuestasEncuestas"])/lTot)*100);

                sb.Append(perc.ToString("N2") + " %");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("	<tr>");

                sb.Append(
                    "<td class=\"textopeque\" colspan=\"2\"><img src=\"images/pb.gif\" height=\"8\" width=\"" +
                    Convert.ToInt32(perc) + "%\"></td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
    }
}