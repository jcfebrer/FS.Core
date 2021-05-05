using FSDatabase;
using FSPortal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSPortalEncuestas
{
    public class Edit : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = new DataTable();
            DataRow row = dt.NewRow();
            string sSQL;
            string sEncuestaName = "";
            string sEncuestaQuestion = "";
            string sStartDate = "";
            string sFinishDate = "";
            bool bActive = false;

            long lEncuestaID = long.Parse(Request["ID"]);
            //lEncuestaID = long.Parse(row["EncuestaID"].ToString());

            if (Request["guardar"] != null && Request["guardar"] != "")
            {
                Register reg = new Register();
                reg.Add("EncuestaName", Request["txtEncuestaName"].ToString());
                reg.Add("EncuestaQuestion", Request["txtEncuestaQuestion"].ToString());
                if (FSLibrary.DateTimeUtil.IsDate(Request["txtStartDate"]))
                {
                    reg.Add("StartDate", DateTime.Parse(Request["txtStartDate"]));
                }
                if (FSLibrary.DateTimeUtil.IsDate(Request["txtFinishDate"]))
                {
                    reg.Add("FinishDate", DateTime.Parse(Request["txtFinishDate"]));
                }
                if (Request["chkActive"] == "1")
                {
                    reg.Add("Active", true);
                }
                else
                {
                    reg.Add("Active", false);
                }

                if (lEncuestaID == 0)
                {
                    db.InsertSql(Variables.App.prefijoTablas + "Encuestas", reg, Variables.User.UsuarioId);
                }
                else
                {
                    db.UpdateSql(Variables.App.prefijoTablas + "Encuestas", reg, "EncuestaID=" + lEncuestaID);
                }

                Response.Redirect(("edit.aspx?ID=" + lEncuestaID), false);
            }
            else
            {

                sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffebcd"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""textopeque""><img border=""0"" src=""");
                sb.AppendLine(Variables.App.directorioPortal);
                sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> <a href=""admin.aspx"">Admin</a> <img border=""0"" src=""");
                sb.AppendLine(Variables.App.directorioPortal);
                sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> Edit Encuesta</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                if ((lEncuestaID == 0))
                {
                    // new
                }
                else
                {
                    sSQL = ("SELECT EncuestaID, EncuestaName, EncuestaQuestion, StartDate, FinishDate, Active FROM "
                                + (Variables.App.prefijoTablas + ("Encuestas WHERE EncuestaID = " + lEncuestaID)));
                    dt = db.Execute(sSQL);
                    sEncuestaName = dt.Rows[0]["EncuestaName"].ToString();
                    sEncuestaQuestion = dt.Rows[0]["EncuestaQuestion"].ToString();
                    sStartDate = dt.Rows[0]["StartDate"].ToString();
                    sFinishDate = dt.Rows[0]["FinishDate"].ToString();
                    bActive = bool.Parse(dt.Rows[0]["Active"].ToString());
                }
                sb.AppendLine(@"<form action=""edit.aspx?guardar=1&iD=");
                sb.AppendLine(lEncuestaID.ToString());
                sb.AppendLine(@""" method=""post"">");
                sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""3"" width=""600"" bgcolor=""#ffe4b5"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td colspan=2 class=""header1"" align=center bgcolor=""#d2b48c"">Encuesta Info</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""header2"" width=100>Encuesta Name</td>");
                sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><input type='text' name=""txtEncuestaName"" value=""");
                sb.AppendLine(sEncuestaName);
                sb.AppendLine(@""" size=""50"" maxlength=""50"" class=""flattextbox""></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""header2"">Encuesta Question</td>");
                sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><input type='text' name=""txtEncuestaQuestion"" value=""");
                sb.AppendLine(sEncuestaQuestion);
                sb.AppendLine(@""" size=""80"" maxlength=""250"" class=""flattextbox""></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""header2"">Start Date</td>");
                sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><input type='text' name=""txtStartDate"" value=""");
                sb.AppendLine(sStartDate);
                sb.AppendLine(@""" size=""15"" maxlength=""15"" class=""flattextbox""></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""header2"">Finish Date</td>");
                sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><input type='text' name=""txtFinishDate"" value=""");
                sb.AppendLine(sFinishDate);
                sb.AppendLine(@""" size=""15"" maxlength=""15"" class=""flattextbox""></td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td class=""header2"">State</td>");
                sb.AppendLine(@"<td class=""textopeque"">");
                if (bActive == true)
                {
                    sb.AppendLine("<input type=\"checkbox\" name=\"chkActive\" value=\"1\" checked>");
                }
                else
                {
                    sb.AppendLine("<input type=\"checkbox\" name=\"chkActive\" value=\"1\">");
                }
                sb.AppendLine("Active");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("<br />");
                if ((lEncuestaID != 0))
                {
                    // If new Encuesta, dont show the OpcionesEncuesta}
                }
                sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffe4b5"">");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td colspan=5 class=""header1"" align=center bgcolor=""#d2b48c"">Respuesta OpcionesEncuesta</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td width=""25""></td>");
                sb.AppendLine(@"<td width=""15""></td>");
                sb.AppendLine(@"<td class=""textopeque""><a href=""editOpcion.aspx?EncuestaID=");
                sb.AppendLine(lEncuestaID.ToString());
                sb.AppendLine(@""">(Add Respuesta Opcion)</a></td>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("</tr>");
                sSQL = ("SELECT COUNT(RespuestaID) AS TotalRespuestasEncuestas FROM "
                                    + (Variables.App.prefijoTablas + ("RespuestasEncuestas WHERE EncuestaID = " + lEncuestaID)));
                dt = db.Execute(sSQL);
                int lToplamOy = int.Parse(dt.Rows[0]["TotalRespuestasEncuestas"].ToString());
                sSQL = ("SELECT Count("
                            + (Variables.App.prefijoTablas + ("RespuestasEncuestas.OpcionID) AS TotalRespuestasEncuestas, "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta.OpcionID, "
                            + (Variables.App.prefijoTablas + "OpcionesEncuesta.OpcionText "))))));
                sSQL = (sSQL + ("From "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta LEFT JOIN "
                            + (Variables.App.prefijoTablas + ("RespuestasEncuestas ON "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta.OpcionID = "
                            + (Variables.App.prefijoTablas + "RespuestasEncuestas.OpcionID ")))))))));
                sSQL = (sSQL + ("WHERE "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta.EncuestaID="
                            + (lEncuestaID + " ")))));
                sSQL = (sSQL + ("GROUP BY "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta.OpcionID, "
                            + (Variables.App.prefijoTablas + "OpcionesEncuesta.OpcionText ")))));
                sSQL = (sSQL + ("ORDER BY Count("
                            + (Variables.App.prefijoTablas + ("RespuestasEncuestas.OpcionID) DESC, "
                            + (Variables.App.prefijoTablas + "OpcionesEncuesta.OpcionID;")))));
                dt = db.Execute(sSQL);

                sb.AppendLine("<tr><td> </td><td> </td><td class=\"header2\">Total " + lToplamOy + " votes cast.</td><td> </td><td> </td></tr>");


                long lSecenek = 0;
                foreach (DataRow rowTot in dt.Rows)
                {
                    long lYuzde = 0;
                    if (lToplamOy != 0)
                        lYuzde = int.Parse(rowTot["TotalRespuestasEncuestas"].ToString()) / lToplamOy * 100;
                    else
                        lYuzde = 0;

                    lSecenek++;

                    sb.AppendLine("<tr>");
                    sb.AppendLine(@"<td class=""textopeque""><a href=""deleteOpcion.aspx?ID=");
                    sb.AppendLine(rowTot["OpcionID"].ToString());
                    sb.AppendLine(@""">(delete)</a></td>");
                    sb.AppendLine(@"<td class=""header2"" align=center>");
                    sb.AppendLine(lSecenek.ToString());
                    sb.AppendLine(".</td>");
                    sb.AppendLine(@"<td class=""textopeque""><a href=""editOpcion.aspx?ID=");
                    sb.AppendLine(rowTot["OpcionID"].ToString());
                    sb.AppendLine(@""">");
                    sb.AppendLine(rowTot["OpcionText"].ToString());
                    sb.AppendLine("</a></td>");
                    sb.AppendLine(@"<td class=""textopeque"" align=right>");
                    sb.AppendLine(String.Format(lYuzde.ToString(), "%"));
                    sb.AppendLine("</td>");
                    sb.AppendLine(@"<td width=100><img src=""images/pb.gif"" height=8 width=");
                    sb.AppendLine(((lYuzde) * 0.9).ToString());
                    sb.AppendLine(@"%""><img src=""images/pbw.gif"" height=8 width=");
                    sb.AppendLine(((100 - lYuzde) * 0.9).ToString());
                    sb.AppendLine(@"%""></td>");
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</table>");
                sb.AppendLine("<br />");
                sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffe4b5"">");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td width=100></td>");
                sb.AppendLine("<td>");
                sb.AppendLine(@"<input type='submit' name=""cmdGuardar"" value="" Guardar "" class=""flatbutton"">");
                sb.AppendLine(@"<input type=""button"" name=""cmdCancelar"" value="" Cancelar "" class=""flatbutton"" onclick=""javasctipt:history.back();"">");
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</table>");
                sb.AppendLine("</form>");
            }

            return sb.ToString();
        }
    }
}
