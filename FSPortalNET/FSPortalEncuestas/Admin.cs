using FSDatabase;
using FSPortal;
using System;
using System.Data;
using System.Text;

namespace FSPortalEncuestas
{
    public class Admin : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            StringBuilder sb = new StringBuilder();
            string sSQL = ("SELECT EncuestaID, EncuestaName, StartDate, FinishDate, Active FROM "
                                + (Variables.App.prefijoTablas + "Encuestas ORDER BY Active, EncuestaID DESC"));
            DataTable dt = db.Execute(sSQL);
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffebcd"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""textopeque""><img border=""0"" src=""");
            sb.AppendLine(Variables.App.directorioPortal);
            sb.AppendLine(@"imagenes/bullet.gif"" alt="""" /> Administrador de encuestas </td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffe4b5"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td class=""header2""><a href=""edit.aspx"">Nueva Encuesta</a></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine(@"<table border=""0"" cellspacing=""1"" cellpadding=""2"" width=""600"" bgcolor=""#ffe4b5"">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td width=20></td>");
            sb.AppendLine(@"<td width=""30"" class=""header1"">ID</td>");
            sb.AppendLine(@"<td width=""320"" class=""header1"">Nombre de la Encuesta</td>");
            sb.AppendLine(@"<td width=""100"" class=""header1"" align=""right"">Comienzo</td>");
            sb.AppendLine(@"<td width=""100"" class=""header1"" align=""right"">Fin</td>");
            sb.AppendLine("<td width=30></td>");
            sb.AppendLine("</tr>");
            string sImageURL = "";
            foreach (DataRow row in dt.Rows)
            {
                if ((Convert.ToBoolean(row["Active"]) == true))
                {
                    sImageURL = "images/active.gif";
                }
                else
                {
                    sImageURL = "images/passive.gif";
                }

                sb.AppendLine(@"<tr bgcolor=""#fffaf0"">");
                sb.AppendLine(@"<td><img src=""");
                sb.AppendLine(sImageURL);
                sb.AppendLine(@""" width=""20"" height=""17""></td>");
                sb.AppendLine(@"<td class=""textopeque"" align=right>");
                sb.AppendLine(row["EncuestaID"].ToString());
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td class=""textopeque""><a href=""edit.aspx?ID=");
                sb.AppendLine(row["EncuestaID"].ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(row["EncuestaName"].ToString());
                sb.AppendLine("</a></td>");
                sb.AppendLine(@"<td class=""textopeque"" align=""right"">");
                sb.AppendLine(row["StartDate"].ToString());
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td class=""textopeque"" align=""right"">");
                sb.AppendLine(row["FinishDate"].ToString());
                sb.AppendLine("</td>");
                sb.AppendLine(@"<td class=""textopeque"" align=center><a href=""edit.aspx?ID=");
                sb.AppendLine(row["EncuestaID"].ToString());
                sb.AppendLine(@""" title=""edit""><img src=""images/duzenle.gif"" width=9 height=11 border=""0""></a>");
                sb.AppendLine(@"<a href=""delete.aspx?ID=");
                sb.AppendLine(row["EncuestaID"].ToString());
                sb.AppendLine(@""" title=""delete""><img src=""images/sil.gif"" width=9 height=11 border=""0""></a></td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }

    }
}
