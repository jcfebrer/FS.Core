// <fileheader>
// <copyright file="detalleNoticia.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: detalleNoticia.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPlugin;
using FSPortal;
using FSLibrary;
using FSNetwork;

namespace FSPaginas
{
    public class Plugin : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
            string plugName = Web.Request("c");
            string param = Web.Request("param");

            if (plugName == "") return "No se ha indicado el Plugin a mostar. ?c=plugName";

            IPlugin plugin = Variables.App.Plugins.GetPlugin(plugName);

            if (plugin == null) return "Imposible recuperar el Plugin indicado. ¿Esta definido correctamente en el fichero web.config?";

            if (plugin.Parameters > 0 && param == "")
            {
                sb.Append(@"<form method=""post"" action=""" + Variables.App.directorioPortal + @"plugin.aspx"">");
                sb.Append(@"<input type=""hidden"" name=""c"" value=""" + plugName + @""" />");
                sb.Append(
                    @"Parámetros (introduzca separados por comas): <input type=""text"" name=""param"" /><input type=""submit"" value=""Ejecutar""/>");
                sb.Append("</form>");
            }
            else
            {
                if (param != "")
                {
                    param = plugName + "," + param;
                    string[] sParam = param.Split(',');

                    sb.Append(plugin.Execute(sParam));
                }
                else
                    sb.Append(plugin.Execute());
            }

            return sb.ToString();
        }
    }
}