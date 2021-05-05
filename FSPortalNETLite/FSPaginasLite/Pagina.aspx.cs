// <fileheader>
// <copyright file="pagina.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: pagina.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortalLite;

namespace FSPaginasLite
{
    /// <summary>
    ///     Clas base página
    /// </summary>
    public class Pagina : BasePage
    {
        /// <summary>
        ///     Carga de la página
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

            int idPagina = Convert.ToInt32(Request.QueryString["id"]);

            sb.Append(contenido);

            return sb.ToString();
        }
    }
}