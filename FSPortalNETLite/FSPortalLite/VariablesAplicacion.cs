// // <fileheader>
// // <copyright file="Variables.App.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System.Collections;
using System.Web.SessionState;

#endregion

namespace FSPortalLite
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class VariablesAplicacion
    {
        //#pragma warning disable 1591

        //
        // variables globales
        //
        public string version = "2.9Lite";
        public string nombreWeb;
        public string HTTP_HOST;
        public string directorioPortal;
        public string directorioWeb;
        public string webHttp;
        public string portal;
        public string paginaLogin = "";
        public string descripcionWeb;
        public string palabrasClave;
        public int registrosPorPagina;
        public string strCookieName;
        public int invitado = 2;

        //pagina cargada
        public BasePage Page;


        //#pragma warning restore 1591
    }
}