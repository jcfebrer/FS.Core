using FSDatabase;
using FSPlugin;
using FSPortal;
using System.Data;

namespace FSCustomPlugin
{
    public class ModMasVendidos : IPlugin
    {
        public string Execute(params string[] p)
        {
            return MasVendidos();
        }

        public string Name
        {
            get { return "ModMasVendidos"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string MasVendidos()
        {
            string modMasVendidosReturn = "";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT idArticulo,titulo," + Variables.User.PrecioAMostrar + " from " + Variables.App.prefijoTablas +
                       "v_ArticulosMasVendidos LIMIT 10";
            }
            else
            {
                ssql = "SELECT top 10 idArticulo,titulo," + Variables.User.PrecioAMostrar + " from " + Variables.App.prefijoTablas +
                       "v_ArticulosMasVendidos";
            }

            DataTable dtMasVendidos = db.Execute(ssql);
            foreach (DataRow row in dtMasVendidos.Rows)
            {
                modMasVendidosReturn = modMasVendidosReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal;
                modMasVendidosReturn = modMasVendidosReturn +
                                       "imagenes/bullet.gif' alt='' />&nbsp;<a style='TEXT-DECORATION: none' href='";
                modMasVendidosReturn = modMasVendidosReturn + Variables.App.directorioPortal + "tienda/detalleArticulo2.aspx?id=";
                modMasVendidosReturn = modMasVendidosReturn + row["idArticulo"] + "'>";
                modMasVendidosReturn = modMasVendidosReturn + row["titulo"] + " - ";
                modMasVendidosReturn = modMasVendidosReturn + row[Variables.User.PrecioAMostrar];
                modMasVendidosReturn = modMasVendidosReturn + " € </a><a href='" + Variables.App.directorioPortal;
                modMasVendidosReturn = modMasVendidosReturn + "tienda/anadirProducto.aspx?id=" + row["idArticulo"];
                modMasVendidosReturn = modMasVendidosReturn + @"'><img alt='' border=""0"" src='" + Variables.App.directorioPortal;
                modMasVendidosReturn = modMasVendidosReturn + "tienda/imagenes/anadir3.gif' /></a>" + Ui.Lf() + "\r\n";
            }

            return modMasVendidosReturn;
        }

    }
}