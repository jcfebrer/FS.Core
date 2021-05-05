// // <fileheader>
// // <copyright file="Portal.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.Data;
using System.Web;

#endregion

namespace FSPortalLite
{
    /// <summary>
    ///     clases genéricas para la utilización del portal
    /// </summary>
    public class Portal
    {
        /// <summary>
        ///     Inicialización
        /// </summary>
        public Portal()
        {
        }

        /// <summary>
        ///     Método para realizar el login al portal
        /// </summary>
        /// <param name="sUsuario"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public void Login(string sUsuario, string sPassword)
        {
            DataRow[] rowResult = Funciones.XMLSelect("usuarios", "usuario='" + sUsuario + "' and clave='" + sPassword + "'");

            if (rowResult.Length != 0)
            {
                if (rowResult[0]["Clave"].ToString().ToLower() == sPassword.ToLower())
                {
                    if (Convert.ToBoolean(rowResult[0]["active"].ToString()) != true)
                    {
                        throw new Exception(
                            "El perfíl de usuario no esta activo. Para más información contacta con el administrador."
                            );
                    }

                    double uid = Convert.ToInt32(rowResult[0]["UsuarioId"]);
                    string use = rowResult[0]["Usuario"].ToString();

                    Variables.User.Usuario = use;
                    Variables.User.UsuarioId = Convert.ToInt32(uid);
                    Variables.User.GroupId = Convert.ToInt32(rowResult[0]["grupo"]);
                    Variables.User.Administrador = FuncionesWeb.EsAdmin(Variables.User.GroupId);

                    Variables.User.Campo1 = rowResult[0]["campo1"].ToString();
                    Variables.User.Campo2 = rowResult[0]["campo2"].ToString();
                    Variables.User.Campo3 = rowResult[0]["campo3"].ToString();
                    Variables.User.Campo4 = rowResult[0]["campo4"].ToString();

                    Variables.User.UserData = rowResult[0];

                    string cla = rowResult[0]["Clave"].ToString();
                }
                else
                {
                    throw new Exception("Clave de acceso incorrecta.");
                }
            }
            else
            {
                throw new Exception("Credenciales incorrectos.");
            }
        }

        /// <summary>
        ///     Desconexión del portal
        /// </summary>
        public void Desconectar()
        {
            FuncionesWeb.ClearVariables();
        }


        /// <summary>
        ///     Lectura de las variables globales y conexión a la base de datos
        /// </summary>
        public void LoadVariables()
        {
            if (Variables.App.Page != null && Variables.App.Page.Session != null)
            {
                Variables.User.sessionID = Variables.App.Page.Session.SessionID;
                Variables.App.HTTP_HOST = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
                Variables.User.HTTP_USER_AGENT = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

                Variables.App.webHttp = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            }

            Variables.App.directorioPortal = Funciones.VirtualPath;

            string portal = ConfigurationManager.AppSettings["DefaultPortal"];

            Variables.App.portal = portal;
            Variables.App.directorioWeb = Variables.App.directorioPortal + "sitios/" + portal + "/";

            Variables.App.descripcionWeb = ConfigurationManager.AppSettings["DescripcionWeb"];
            Variables.App.palabrasClave = ConfigurationManager.AppSettings["PalabrasClave"];


            Variables.App.registrosPorPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"]);
            Variables.App.nombreWeb = ConfigurationManager.AppSettings["NombreWeb"];
            
            Variables.App.strCookieName = ConfigurationManager.AppSettings["StrCookieName"];

            Variables.App.paginaLogin = ConfigurationManager.AppSettings["PaginaLogin"];

            Variables.App.paginaLogin = Variables.App.directorioPortal + Variables.App.paginaLogin;
        }
    }
}