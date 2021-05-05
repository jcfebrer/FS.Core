// <fileheader>
// <copyright file="hasolvidado.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\hasolvidado.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;
using FSCrypto;

namespace FSPaginas.Servicios
{
    public class ActivarUsuario : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            if (Web.Request("u") != "")
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

				Crypto crypt = new Crypto();

                string email = crypt.Decryp(System.Web.HttpUtility.HtmlDecode(Web.Request("u")));

                if (TextUtil.IsEmail(email))
                {
                    int usuarios = db.ExecuteNonQuery("update usuarios set active=1 where email='" + email + "'");

                    if (usuarios == 1)
                    {
                        sb.Append("Activación correcta. Puedes acceder a: <a href='" + Variables.App.paginaLogin + "'>" +
                                  Variables.App.paginaLogin + "</a>, para conectarte.");
                    }
                    else
                    {
                        sb.Append("Error al activar el usuario. Intentelo de nuevo más tarde.");
                    }
                }
                else
                {
                    sb.Append("Información incorrecta.");
                }
            }
            else
            {
                sb.Append("Parámetros incorrectos.");
            }

            return sb.ToString();
        }
    }
}