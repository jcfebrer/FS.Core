using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModFabricantes : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Fabricantes();
        }

        public string Name
        {
            get { return "ModFabricantes"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Fabricantes()
        {
            string modFabricantesReturn = "" + "\r\n";

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql = "SELECT idProveedor,nombre from " + Variables.App.prefijoTablas + "Proveedores";
            DataTable dtFabricantes = db.Execute(ssql);
            foreach (DataRow row in dtFabricantes.Rows)
            {
                modFabricantesReturn = modFabricantesReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                       "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                                       "tienda/verProductos.aspx?modo=lista&amp;prov=" + row["idProveedor"] + "'>" +
                                       row["Nombre"] + "</a>" + Ui.Lf() + "\r\n";
            }

            return modFabricantesReturn;
        }
    }
}