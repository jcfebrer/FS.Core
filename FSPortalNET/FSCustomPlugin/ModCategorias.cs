using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModCategorias : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Categorias();
        }

        public string Name
        {
            get { return "ModCategorias"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Categorias()
        {
            string modCategoriasReturn = "" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT idCategoria,titulo from " + Variables.App.prefijoTablas + "Categorias where activo=true";
            DataTable dtCategorias = db.Execute(ssql);
            foreach (DataRow row in dtCategorias.Rows)
            {
                modCategoriasReturn = modCategoriasReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                      "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                                      "tienda/verProductos.aspx?modo=lista&amp;cat=" + Functions.Valor(row["idCategoria"]) +
                                      "'>" + row["titulo"] + " (" +
                                      FuncionesWeb.ArticulosCategoria(long.Parse(Functions.Valor(row["idCategoria"]))) +
                                      ")</a>" + Ui.Lf() + "\r\n";
            }

            return modCategoriasReturn;
        }
    }
}