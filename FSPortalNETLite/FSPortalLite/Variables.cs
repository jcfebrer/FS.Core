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
using System.Web;

#endregion

namespace FSPortalLite
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class Variables
    {
        /// <summary>
        ///     Variables de usuario
        /// </summary>
        public static VariablesUsuario User
        {
            get
            {
            	try
            	{
	                if (HttpContext.Current.Session != null)
	                {
	                    if (HttpContext.Current.Session["User"] == null)
	                    {
	                        VariablesUsuario varUsr = new VariablesUsuario();
                            HttpContext.Current.Session["User"] = varUsr;
	                    }
	
	                    return ((VariablesUsuario)HttpContext.Current.Session["User"]);
	                }
	                return null;
            	} catch {
            		return null;
            	}
            }
            set { HttpContext.Current.Session["User"] = value; }
        }

        /// <summary>
        ///     Variables de aplicación
        /// </summary>
        public static VariablesAplicacion App
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session != null)
                    {
                        if (HttpContext.Current.Session["App"] == null)
                        {
                            VariablesAplicacion varApp = new VariablesAplicacion();
                            HttpContext.Current.Session["App"] = varApp;
                        }

                        return ((VariablesAplicacion)HttpContext.Current.Session["App"]);
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["App"] = value;
            }
        }
    }
}