// <fileheader>
// <copyright file="detalleNoticia.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: detalleNoticia.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class DetalleNoticia : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string ssql = "SELECT titulo,textoCorto,textoLargo,noticiaID FROM " + Variables.App.prefijoTablas +
                          "Noticias where noticiaID=" + Web.RequestInt("id");
            DataTable dt = db.Execute(ssql);
            if (dt.Rows.Count > 0)
            {
                sb.Append(Functions.Valor(dt.Rows[0]["titulo"]) + "<hr>" + Functions.Valor(dt.Rows[0]["textoCorto"]) + "<hr>" +
                          Functions.Valor(dt.Rows[0]["textoLargo"]));
            }
            else
            {
                sb.Append("Noticia no encontrada." + "\r\n");
            }

            return sb.ToString();
        }
    }
}