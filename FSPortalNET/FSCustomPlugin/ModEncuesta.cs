using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System;
using System.Data;

namespace FSCustomPlugin
{
    public class ModEncuesta : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Encuesta();
        }

        public string Name
        {
            get { return "ModEncuesta"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Encuesta()
        {
            string modEncuestaReturn;
            DataTable dtEncuesta;

            long lEncuestaId = Convert.ToInt64(double.Parse(Variables.App.encuesta));

            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load("encuestas.xml");
                dtEncuesta = xml.Select("EncuestaID=" + lEncuestaId);
            }
            else
            {
                string ssql = "SELECT EncuestaID, EncuestaName, EncuestaQuestion FROM " + Variables.App.prefijoTablas +
                              "Encuestas WHERE EncuestaID = " + lEncuestaId;
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                dtEncuesta = db.Execute(ssql);
            }

            if (dtEncuesta == null || dtEncuesta.Rows.Count == 0)
            {
                modEncuestaReturn = "Error : Encuesta no encontrada. " + Ui.Lf() + "Encuesta ID:" + lEncuestaId;

                return modEncuestaReturn;
            }

            string sEncuestaQuestion = Functions.Valor(dtEncuesta.Rows[0]["EncuestaQuestion"]);

            modEncuestaReturn = "<form action='" + Variables.App.directorioPortal + "encuestas/cast.aspx?ID=" + lEncuestaId +
                                "' method='post'>";
            modEncuestaReturn = modEncuestaReturn + "<table border='0' width='100%'>";
            modEncuestaReturn = modEncuestaReturn + "<tr>";
            modEncuestaReturn = modEncuestaReturn + "<td class='header2'>" + sEncuestaQuestion + " " +
                                Ui.EditPage("Encuestas", "EncuestaID", lEncuestaId.ToString()) + "</td>";
            modEncuestaReturn = modEncuestaReturn + "</tr>";

            DataTable dtEncuestaOpciones;
            if (Variables.App.UseXML)
            {
                XML xml = new XML(Variables.App.directorioWeb + "data");
                xml.Load("opcionesencuesta.xml");
                dtEncuestaOpciones = xml.Select("EncuestaID=" + lEncuestaId);
            }
            else
            {
                string ssql = "SELECT OpcionID, OpcionText, AdditionalInformation From " + Variables.App.prefijoTablas + "OpcionesEncuesta WHERE EncuestaID = " +
                   lEncuestaId;
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                dtEncuestaOpciones = db.Execute(ssql);
            }

            if (dtEncuestaOpciones != null)
            {
                int f = 0;
                foreach (DataRow row in dtEncuestaOpciones.Rows)
                {
                    modEncuestaReturn = modEncuestaReturn + "<tr>";
                    modEncuestaReturn = modEncuestaReturn + "<td class='textopeque'>";
                    modEncuestaReturn = modEncuestaReturn + "<input type='radio' name='optSecenek' value='" +
                                        row["OpcionID"] + "' ";
                    if (f == 0)
                    {
                        modEncuestaReturn = modEncuestaReturn + @"checked=""checked""";
                    }
                    modEncuestaReturn = modEncuestaReturn + " />";
                    if (Functions.Valor(row["AdditionalInformation"]) != "")
                    {
                        modEncuestaReturn = modEncuestaReturn + @"<a href=""#"" onclick=""javascript:open_window('" +
                                            Variables.App.directorioPortal + "encuestas/additionalinfo.aspx?simple=1&id=" +
                                            row["OpcionID"] + "&amp;name=" + System.Web.HttpUtility.UrlEncode(Functions.Valor(row["OpcionText"])) +
                                            "');" + Convert.ToChar(34) + ">" + row["OpcionText"] + "</a>";
                    }
                    else
                    {
                        modEncuestaReturn = modEncuestaReturn + row["OpcionText"];
                    }

                    modEncuestaReturn += " " + Ui.EditPage("OpcionesEncuesta", "OpcionID", row["OpcionID"].ToString());

                    modEncuestaReturn = modEncuestaReturn + "</td>";
                    modEncuestaReturn = modEncuestaReturn + "</tr>";

                    f = f + 1;
                }
            }

            modEncuestaReturn = modEncuestaReturn + "<tr>";
            modEncuestaReturn = modEncuestaReturn +
                                "<td align='center'><input type='submit' name='cmdOyVer' value=' Enviar ' class='botonplano' />";
            modEncuestaReturn = modEncuestaReturn + "</td>";
            modEncuestaReturn = modEncuestaReturn + "</tr>";
            modEncuestaReturn = modEncuestaReturn + "<tr>";
            modEncuestaReturn = modEncuestaReturn + @"<td align='center'><img border=""0"" src='" + Variables.App.directorioPortal +
                                "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                                "encuestas/show.aspx?ID=" + lEncuestaId + "'>Ver resultados</a>";
            modEncuestaReturn = modEncuestaReturn + "</td>";
            modEncuestaReturn = modEncuestaReturn + "</tr>";
            modEncuestaReturn = modEncuestaReturn + "</table>";
            modEncuestaReturn = modEncuestaReturn + "</form>";

            return modEncuestaReturn;
        }
    }
}