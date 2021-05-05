// <fileheader>
// <copyright file="muestra.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: muestra.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas
{
    public class Muestra : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            string pagina = Web.Request("pag");
            if (pagina == "") pagina = "default";
            Server.Transfer(pagina + ".aspx");
        }
    }
}