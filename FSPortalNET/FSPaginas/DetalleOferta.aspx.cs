// <fileheader>
// <copyright file="detalleOferta.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: detalleOferta.aspx.cs
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
    public class DetalleOferta : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string ssql = "SELECT titulo,textoCorto,textoLargo,ofertaID FROM " + Variables.App.prefijoTablas +
                          "OfertasTrabajo where ofertaID=" + Web.RequestInt("id");
            DataTable dt = db.Execute(ssql);
            if (dt.Rows.Count > 0)
            {
                sb.Append(Functions.Valor(dt.Rows[0]["titulo"]) + "<hr />" + Functions.Valor(dt.Rows[0]["textoCorto"]) + Ui.Lf() +
                          Functions.Valor(dt.Rows[0]["textoLargo"]) + "<hr />");
            }
            else
            {
                sb.Append("Oferta no encontrada." + "\r\n");
            }

            return sb.ToString();
        }
    }
}