// <fileheader>
// <copyright file="additionalinfo.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: encuestas\additionalinfo.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSDatabase;

namespace FSPortalEncuestas
{
    public class AdditionalInfo : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();

            int opcionId = Web.RequestInt("ID");
            string opcionName = Web.Request("name");

            sb.Append("<html>");
            sb.Append("<head>");
            sb.Append("<title>" + opcionName + "</title>");
            sb.Append("<script type='text/javascript' language='javascript'>");
            sb.Append("<!--");
            sb.Append("window.focus();");
            sb.Append("//-->");
            sb.Append("</script>");
            sb.Append("</head>");
            sb.Append("<body bgcolor='#ffe4b5'>");
            sb.Append("<table border='0' cellspacing='1' cellpadding='2' width='100%' bgcolor='#ffe4b5'>");

            string sSql = "SELECT AdditionalInformation From " + Variables.App.prefijoTablas +
                          "OpcionesEncuesta WHERE OpcionID = " + opcionId;
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dt = db.Execute(sSql);

            if (dt.Rows.Count > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td class='textopeque'>" + Functions.Valor(dt.Rows[0]["AdditionalInformation"]) + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            return sb.ToString();
        }
    }
}