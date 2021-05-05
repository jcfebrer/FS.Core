// <fileheader>
// <copyright file="verFabricantes.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\verFabricantes.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSDatabase;

namespace FSTienda
{
    public class VerFabricantes : BasePage
    {
        /// <summary>
        ///     Carga de la p¨¢gina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string sSQL = "SELECT idProveedor,nombre from " + Variables.App.prefijoTablas + "Proveedores";
            DataTable dt = db.Execute(sSQL);
            foreach (DataRow row in dt.Rows)
            {
                sb.Append(Ui.Lf() + @"<img border=""0"" src=""" + Variables.App.directorioPortal +
                          @"imagenes/bullet.gif"" alt="""" /> <a href=""verProductos.aspx?modo=lista&amp;prov=" +
                          row["idProveedor"] + @""">" + row["Nombre"] + "</a>");
            }

            return sb.ToString();
        }
    }
}