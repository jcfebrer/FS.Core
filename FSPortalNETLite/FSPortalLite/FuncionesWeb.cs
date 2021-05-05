// // <fileheader>
// // <copyright file="FuncionesWeb.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;

#endregion

namespace FSPortalLite
{
	public static class FuncionesWeb
	{

		public static string Enlace(string lnk)
		{
			if ((lnk.Substring(0, 4).ToLower() == "http") || ((lnk.Substring(0, 3)).ToLower() == "www")) {
				return lnk;
			}
			return Variables.App.directorioPortal + lnk;
		}

		public static string ObtenerPlantilla(string nombrePlantilla)
		{
			DataRow[] rowResult = Funciones.XMLSelect("plantillas", "plantilla='" + nombrePlantilla + "'");

			return rowResult[0]["contenido"].ToString();
		}
    
        public static void ClearVariables()
        {
            BasePage basePage = Variables.App.Page;
            if ( basePage != null)
                basePage.Session.Abandon();

            Variables.User = null;
            Variables.App = null;
            Variables.User = new VariablesUsuario();
            Variables.App = new VariablesAplicacion();

            Variables.App.Page = basePage;

            Portal portal = new Portal();
            portal.LoadVariables();
        }

        public static bool EsAdmin(int grupo)
        {
            if (grupo == 1)
                return true;
            return false;
        }
    }
}