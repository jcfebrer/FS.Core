using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System;
using System.Data;

namespace FSCustomPlugin
{
    public class ModTareas : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Tareas();
        }

        public string Name
        {
            get { return "ModTareas"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Tareas()
        {
            string modTareasReturn = "";
            if (Variables.User.UsuarioId != 0)
            {
                string ssql = "SELECT idTarea,nombreTarea,idPrioridad,duracion from " + Variables.App.prefijoTablas +
                              "tareas where idestado<>3 and asignadoa=" + Variables.User.UsuarioId +
                              " order by fechaasignacion";
                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                DataTable dtTareas = db.Execute(ssql);

                if (dtTareas.Rows.Count == 0)
                {
                    modTareasReturn = "No hay tareas.";
                }
                else
                {
                    long totDuracion = 0;
                    foreach (DataRow row in dtTareas.Rows)
                    {
                        modTareasReturn = modTareasReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                          @"imagenes/bullet.gif' alt='' /> <img alt="""" border=""0"" src=""/calidad/tareas/images/prior" +
                                          row["idPrioridad"] + @".gif"" /> ";
                        if (Variables.User.Administrador)
                        {
                            modTareasReturn = modTareasReturn +
                                              Ui.Link(row["nombreTarea"].ToString(),
                                                  Variables.App.directorioPortal +
                                                  "admin/editor/showrecord.aspx?amp;q=&amp;tablename=tareas&amp;fld=idTarea&amp;val=" +
                                                  row["idtarea"] + "&amp;fldtype=System.Integer&amp;page=1") + "\r\n";
                        }
                        else
                        {
                            modTareasReturn = modTareasReturn +
                                              Ui.Link(row["nombreTarea"].ToString(),
                                                  Variables.App.directorioPortal + "tareas/editarTarea.aspx?idTarea=" +
                                                  row["idTarea"]) + "\r\n";
                        }

                        if (NumberUtils.IsNumeric(row["duracion"].ToString()))
                        {
                            totDuracion = totDuracion + Convert.ToInt64(row["duracion"]);
                        }
                    }

                    modTareasReturn = modTareasReturn + Ui.Lf() + "Duración: " + totDuracion + Ui.Lf();

                    if (Variables.User.Administrador)
                    {
                        modTareasReturn = modTareasReturn + Ui.Lf() +
                                          Ui.Link("Ver todas", Variables.App.directorioPortal + "tareas/default.aspx");
                    }
                }
            }
            else
            {
                modTareasReturn = modTareasReturn + "Debes registrarte para acceder a este modulo.";
            }
            return modTareasReturn;
        }
    }
}