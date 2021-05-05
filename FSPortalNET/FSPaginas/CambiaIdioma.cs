using System;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas
{
    public class CambiaIdioma : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            Inicio();
        }

        public void Inicio()
        {
            string sIdioma = Web.Request("idioma");

            if (!string.IsNullOrEmpty(sIdioma))
            {
                Session["idioma"] = sIdioma;
                Variables.User.idiomaSel = sIdioma;
            }

            if (string.IsNullOrEmpty(Functions.Valor(Session["idioma"])))
            {
                Session["idioma"] = Web.Cookie(Request.Cookies[Variables.App.strCookieName], "idioma");
            }
            if (string.IsNullOrEmpty(Functions.Valor(Session["idioma"])))
            {
                Session["idioma"] = "castellano";
            }

            if (string.IsNullOrEmpty(Request["comeback"] + ""))
            {
                Response.Redirect(Variables.App.directorioPortal + "default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Response.Redirect(Request["comeback"], false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}