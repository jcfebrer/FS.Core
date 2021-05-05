// // <fileheader>
// // <copyright file="Formularios.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSPortal
// //     Solution: FSPortalNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System.Data;
using System.Text;
using FSLibrary;
using FSDatabase;

#endregion

namespace FSPortal
{
    /// <summary>
    ///     Funciones para el manejo de formularios html
    /// </summary>
    public class Formularios
    {
        /// <summary>
        ///     Mostramos el formulario indicado en el parámetro ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string MuestraFormulario(int id)
        {
            StringBuilder sb = new StringBuilder("");
            DataTable dtFormulario;
            DataTable dtFields2;

            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load("formularios.xml");
                dtFormulario = xml.Select("activo = true and idFormulario=" + id);

                xml.Load("formulariocampos.xml");
                dtFields2 = xml.Select("idFormulario=" + id);
            }
            else
            {
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                if (!db.TableExists(Variables.App.prefijoTablas + "Formularios")) return "Tabla formularios, no existente.";

                dtFormulario =
                    db.Execute("SELECT mensajeEnviar,nombre,textoenviar FROM " + Variables.App.prefijoTablas +
                               "Formularios WHERE activo = true and idFormulario=" + id);

                dtFields2 =
                    db.Execute("SELECT descripcion,tipo,nombre,tamano,IdCamposFormulario FROM " + Variables.App.prefijoTablas +
                           "FormularioCampos WHERE idFormulario=" + id);
            }

            if (dtFormulario != null && dtFormulario.Rows.Count > 0)
            {
                sb.Append(Ui.Lf());

                sb.Append(Ui.EditPage("formularios", "idFormulario", id.ToString(), "Editar Formulario", "Borrar Formulario"));

                sb.Append("\r\n" + "<b>" + Functions.Valor(dtFormulario.Rows[0]["nombre"]) + "</b>" + Ui.Lf());

                sb.Append("\r\n" + @"<form id=""frmForm"" class=""frmForm"" action=""" + Variables.App.directorioPortal +
                          "formularios/EnviarFormulario.aspx?modo=form&id=" + id + @""" method=""post"">");
                sb.Append("\r\n" + @"<table width=""100%"" border=""0"" class=""texto"">");

                if (dtFields2 != null && dtFields2.Rows.Count > 0)
                {
                    foreach (DataRow row in dtFields2.Rows)
                    {
                        sb.Append("\r\n" + "<tr><td class='cabemaspeque'>");

                        sb.Append(Ui.EditPage("formularioCampos", "idCamposFormulario", Functions.Valor(row["IdCamposFormulario"]),
                            "Editar Campo", "Borrar Campo"));

                        sb.Append("\r\n" + Functions.Valor(row["descripcion"]) + "</td><td>");

                        switch (Functions.Valor(row["tipo"]))
                        {
                            case "1":
                            case "2":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                                sb.Append(@"<input class='textboxplano' name=""" + Functions.Valor(row["nombre"]) +
                                          @""" type=""text"" size=""" + Functions.Valor(row["tamano"]) + @"""/>");
                                break;
                            case "3":
                                sb.Append(@"<textarea class='textboxplano' name=""" + Functions.Valor(row["nombre"]) + @""" cols=""" +
                                          Functions.Valor(row["tamano"]) + @""" rows=""5""></textarea>");
                                break;
                        }


                        sb.Append("</td>" + "\r\n");
                        sb.Append("\r\n" + "</tr>");
                    }
                }

                string tenviar = Functions.Valor(dtFormulario.Rows[0]["textoenviar"]);
                if (tenviar == "")
                {
                    tenviar = "Enviar formulario";
                }
                sb.Append(@"<tr><td>&nbsp;</td><td><input name=""cmdEnviar"" type=""submit"" value=""" + tenviar +
                          @"""/></td></tr>");
                sb.Append("\r\n" + "</table>");
                sb.Append("</form>");
            }

            if (Variables.User.Administrador)
            {
                sb.Append("\r\n" + Ui.Lf() + @"<img src=""" + Variables.App.directorioPortal +
                          @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""" + Variables.App.directorioPortal +
                          "admin/editor/showrecord.aspx?tablename=FormularioCampos&amp;add=1&amp;page=1&amp;autoSel=" +
                          id + @"&amp;autoSelField=idFormulario"" class=""cabemaspeque"">Añadir campo</a>" + Ui.Lf() +
                          Ui.Lf());
            }

            return sb.ToString();
        }
    }
}