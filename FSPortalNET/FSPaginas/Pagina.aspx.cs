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
using FSPortal;
using FSNetwork;
using FSException;
using FSTrace;

namespace FSPaginas
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
            Log.TraceInfo("Inicio Pagina.aspx");
            contenido = Inicio();
            Log.TraceInfo("Fin Pagina.aspx");
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            //if (Variables.App.mostrarNavegacionPaginas && Variables.App.Page.simple == false)
            //{
            //    sb.Append(@"<p align=""right""><a href=""" + Variables.App.directorioPortal + @""">" + FuncWeb.Idioma(46) + @"</a> | <a href=""javascript:history.back();"">" + FuncWeb.Idioma(320) + "</a></p>" + "\r\n");
            //    sb.Append(UI.Lf() + "\r\n");
            //}

            if (Web.RequestInt("lee") == 1)
                //fuerza el refresco de la variable "Contenido", en caso contrario, "Contenido" se inicializa desde "LoadPageVariables()"
            {
				int idPagina = Web.RequestInt("id");
                contenido = Ui.MuestraPagina(idPagina);
            }

            sb.Append(contenido);


            return sb.ToString();
        }
    }
}