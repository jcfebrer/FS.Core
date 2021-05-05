// <fileheader>
// <copyright file="Default.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: filemanager\Default.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>


using System;
using FSPortal;

namespace FSPaginas.Galeria
{
    public class Default : FileManager.Default
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            modeGallery = true;
            columnas = 4;
            adminControls = Variables.User.Administrador;
            pathInicial = Variables.App.directorioWeb + "galeria/";
        }

        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }
    }
}
