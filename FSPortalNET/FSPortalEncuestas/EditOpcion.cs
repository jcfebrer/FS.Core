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
    class EditOpcion : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string sSQL = "";
            string sOpcionText = "";
            string sAdditionalInfo = "";
            string sEncuestaName = "";
            string sEncuestaQuestion = "";

            long lSecenekID = long.Parse(Request["ID"]);
            long lEncuestaID = long.Parse(Request["EncuestaID"]);
            if (Request["kaydet"] != "")
            {
                if ((lSecenekID == 0))
                {
                    Register reg = new Register();
                    reg.Add("EncuestaID", lEncuestaID);
                    reg.Add("OpcionText", Request["txtOpcionText"].ToString());
                    reg.Add("AdditionalInformation", Request["txtAdditionalInfo"].ToString());

                    db.InsertSql(Variables.App.prefijoTablas + "OpcionesEncuesta", reg, Variables.User.UsuarioId);
                }
                else
                {
                    Register reg = new Register();
                    reg.Add("EncuestaID", lEncuestaID);
                    reg.Add("OpcionText", Request["txtOpcionText"].ToString());
                    reg.Add("AdditionalInformation", Request["txtAdditionalInfo"].ToString());

                    db.UpdateSql(Variables.App.prefijoTablas + "OpcionesEncuesta", reg, "OpcionID=" + lSecenekID);
                }

                Response.Redirect("edit.aspx?ID=" + lEncuestaID, false);
            }
            else
            {
                sSQL = ("SELECT EncuestaID, OpcionText, AdditionalInformation From "
                            + (Variables.App.prefijoTablas + ("OpcionesEncuesta WHERE OpcionID = " + lSecenekID)));
                DataTable dtOpcionesEncuesta = db.Execute(sSQL);
                if (dtOpcionesEncuesta.Rows.Count > 0)
                {
                    sOpcionText = dtOpcionesEncuesta.Rows[0]["OpcionText"].ToString();
                    sAdditionalInfo = dtOpcionesEncuesta.Rows[0]["AdditionalInformation"].ToString();
                    lEncuestaID = long.Parse(dtOpcionesEncuesta.Rows[0]["EncuestaID"].ToString());
                }

                sSQL = ("SELECT EncuestaName, EncuestaQuestion FROM "
                            + (Variables.App.prefijoTablas + ("Encuestas WHERE EncuestaID = " + lEncuestaID)));
                DataTable dtEncuestas = db.Execute(sSQL);
                sEncuestaName = dtEncuestas.Rows[0]["EncuestaName"].ToString();
                sEncuestaQuestion = dtEncuestas.Rows[0]["EncuestaQuestion"].ToString();
            }
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffebcd"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""textopeque""><img border=""0"" src=""");
            sb.AppendLine(Variables.App.directorioPortal);
            sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> <a href=""admin.aspx"">Admin</a> <img border=""0"" src=""");
            sb.AppendLine(Variables.App.directorioPortal);
            sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> <a href=""edit.aspx?ID=");
            sb.AppendLine(lEncuestaID.ToString());
            sb.AppendLine(@""">Edit Encuesta</a> <img border=""0"" src=""");
            sb.AppendLine(Variables.App.directorioPortal);
            sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> Edit Opcion</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine(@"<form action=""editOpcion.aspx?kaydet=1&iD=");
            sb.AppendLine(lSecenekID.ToString());
            sb.AppendLine("&encuestaID=");
            sb.AppendLine(lEncuestaID.ToString());
            sb.AppendLine(@""" method=""post"">");
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""3"" width=""600"" bgcolor=""#ffe4b5"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td colspan=2 class=""header1"" align=center bgcolor=""#d2b48c"">");
            sb.AppendLine(sEncuestaName);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""header2"" width=100>Opcion</td>");
            sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><input type='text' name=""txtOpcionText"" value=""");
            sb.AppendLine(sOpcionText);
            sb.AppendLine(@""" size=""50"" maxlength=""50"" class=""flattextbox""></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""header2"" width=100>Additional Information</td>");
            sb.AppendLine(@"<td class=""textopeque"" bgcolor=""#fffaf0""><textarea rows=""5"" cols=""50"" name=""txtAdditionalInfo"" class=""flattextbox"">");
            sb.AppendLine(sAdditionalInfo);
            sb.AppendLine("</textarea></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffe4b5"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td width=100></td>");
            sb.AppendLine("<td>");
            sb.AppendLine(@"<input type='submit' name=""cmdKaydet"" value="" Save "" class=""flatbutton"">");
            sb.AppendLine(@"<input type=""button"" name=""cmdIptal"" value="" Cancel "" class=""flatbutton"" onclick=""javasctipt:history.back();"">");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</form>");

            return sb.ToString();
        }

    }
}
