using System;
using FSPlugin;
using System.Data;
using FSLibrary;
using FSPortal;
using FSDatabase;

namespace FSCustomPlugin
{
    public class ModComentarios : IPlugin
    {
        public string Execute(params string[] p)
        {
            if (p.Length != Parameters + 1) return "Parametros incorrectos. Se necesitan [" + Parameters + "].";
            return Comentarios(Convert.ToInt32(p[1]));
        }

        public string Name
        {
            get { return "ModComentarios"; }
        }

        public int Parameters
        {
            get { return 1; }
        }


        public static string Comentarios(int pageId)
        {
            string modComentariosReturn = "";

            string ssql = "SELECT idComentario,comentario,fecha,enviadoPor FROM " + Variables.App.prefijoTablas +
                          "Comentarios WHERE idPagina=" + pageId + " order by fecha";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dtComentarios = db.Execute(ssql);
            if (dtComentarios.Rows.Count > 0)
            {
                foreach (DataRow row in dtComentarios.Rows)
                {
                    modComentariosReturn = modComentariosReturn + "\r\n" +
                                           Ui.EditPage("comentarios", "IdComentario", Functions.Valor(row["idComentario"]),
                                               "Editar Comentario", "Borrar Comentario");
                    modComentariosReturn = modComentariosReturn + @"<img border=""0"" src=""" + Variables.App.directorioPortal +
                                           @"imagenes/comentarios.png"">";
                    modComentariosReturn = modComentariosReturn + row["comentario"] + "\r\n";
                    modComentariosReturn = modComentariosReturn + Ui.Lf() + Ui.Lf() + @"<font size=""1"">enviado el " +
                                           FSLibrary.DateTimeUtil.LongDate(System.DateTime.Parse(Functions.Valor(row["fecha"]))) + "\r\n";
                    modComentariosReturn = modComentariosReturn + " a las " +
                        System.DateTime.Parse(Functions.Valor(row["fecha"])).ToShortTimeString() + ", por: " +
                                           row["enviadoPor"] + "</font>" + Ui.Lf() + Ui.Lf() + "\r\n";
                }
            }
            else
            {
                modComentariosReturn = "No se encontraron comentarios.</td>" + "\r\n";
            }

            modComentariosReturn = modComentariosReturn + Ui.Lf() + @"<a href=""" + Variables.App.directorioPortal +
                                   "comentarios.aspx?id=" + pageId + @""">";
            modComentariosReturn = modComentariosReturn + @"<img alt=""Añadir comentarios"" style=""border:0"" src=""" +
                                   Variables.App.directorioPortal + @"imagenes/nube.png"" />Añade tu comentario(" +
                                   FuncionesWeb.TotalComentarios(pageId) + ")</a>";

            return modComentariosReturn;
        }
    }
}