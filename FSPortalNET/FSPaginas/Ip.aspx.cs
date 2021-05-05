// <fileheader>
// <copyright file="buscar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: buscar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class Ip : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = FSNetwork.Net.GetWebIPAddress();
        }
    }
}