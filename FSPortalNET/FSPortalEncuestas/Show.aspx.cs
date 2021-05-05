// <fileheader>
// <copyright file="show.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: encuestas\show.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;

namespace FSPortalEncuestas
{
    public class Show : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        public string Inicio()
        {
            EncuestaController ec = new EncuestaController();

            int id = NumberUtils.NumberInt(Request["ID"]);

            return ec.ShowData(id, "");
        }
    }
}