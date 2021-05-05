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

using System.Web;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     Clase pública con las variables globales del portal.
    /// </summary>
    public class Variables
    {
        /// <summary>
        /// Tipos de editores Html
        /// </summary>
        public enum HtmlEditorType
        {
            /// <summary>
            /// The text area
            /// </summary>
            TextArea = 0,
            /// <summary>
            /// The c keditor
            /// </summary>
            CKeditor = 1,
            /// <summary>
            /// The tiny mce
            /// </summary>
            TinyMce = 2
        }

        /// <summary>
        ///     Modos de formularios
        /// </summary>
        public enum FormMod
        {
            /// <summary>
            /// The nada
            /// </summary>
            Nada = -1,
            /// <summary>
            /// The add record
            /// </summary>
            AddRecord = 0,
            /// <summary>
            /// The edit record
            /// </summary>
            EditRecord = 1,
            /// <summary>
            /// The login
            /// </summary>
            Login = 2,
            /// <summary>
            /// The add user
            /// </summary>
            AddUser = 3,
            /// <summary>
            /// The recordar password
            /// </summary>
            RecordarPassword = 4,
            /// <summary>
            /// The edit user
            /// </summary>
            EditUser = 5,
            /// <summary>
            /// The form
            /// </summary>
            Form = 6,
            /// <summary>
            /// The email
            /// </summary>
            Email = 7
        }

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
                }
                catch
                {
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

        /// <summary>
        ///     Variables de parser
        /// </summary>
        public static VariablesParser Parser
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Session != null)
                    {
                        if (HttpContext.Current.Session["Parser"] == null)
                        {
                            VariablesParser varParser = new VariablesParser();
                            HttpContext.Current.Session["Parser"] = varParser;
                        }

                        return ((VariablesParser)HttpContext.Current.Session["Parser"]);
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
                HttpContext.Current.Session["Parser"] = value;
            }
        }
    }
}