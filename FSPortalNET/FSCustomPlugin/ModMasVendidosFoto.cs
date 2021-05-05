using FSDatabase;
using FSLibrary;
using FSPlugin;
using FSPortal;
using System;
using System.Data;

namespace FSCustomPlugin
{
    public class ModMasVendidosFoto : IPlugin
    {
        public string Execute(params string[] p)
        {
            return MasVendidosFoto();
        }

        public string Name
        {
            get { return "ModMasVendidosFoto"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string MasVendidosFoto()
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string modMasVendidosFotoReturn = "";


            const int columnas = 3;

            string ssql;

            if (Utils.BDType == Utils.TypeBd.MySQL)
            {
                ssql = "SELECT titulo,imagen1,idArticulo,precioAnterior," + Variables.User.PrecioAMostrar + " from " +
                       Variables.App.prefijoTablas + "v_ArticulosMasVendidos LIMIT 15";
            }
            else
            {
                ssql = "SELECT top 15 titulo,imagen1,idArticulo,precioAnterior," + Variables.User.PrecioAMostrar + " from " +
                       Variables.App.prefijoTablas + "v_ArticulosMasVendidos";
            }

            DataTable dtVendidos = db.Execute(ssql);
            int f = 0;
            bool haz = (dtVendidos.Rows.Count > 0);

            if (haz)
            {
                modMasVendidosFotoReturn = "<table border='0'>" + "\r\n";
            }

            foreach (DataRow row in dtVendidos.Rows)
            {
                if ((f % columnas) == 0 | f == 0)
                {
                    modMasVendidosFotoReturn = modMasVendidosFotoReturn + "<tr>" + "\r\n";
                }
                int por = (int)System.Math.Floor((double)100 / columnas);
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "<td align='center' width='" + por + "%'>" +
                                           "\r\n";
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "<img alt='" + Functions.Valor(row["titulo"]) +
                                           @"' border=""0"" src='";
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + Variables.App.directorioWeb + "tienda/fotos/";
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + Functions.Valor(row["imagen1"]) + "' />" + Ui.Lf();
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "<a style='TEXT-DECORATION: none' href='" +
                                           Variables.App.directorioPortal;
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "tienda/detalleArticulo2.aspx?id=" +
                                           Functions.Valor(row["idArticulo"]);
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "'>" + Functions.Valor(row["titulo"]);
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "</a>";


                if (Convert.ToDouble(row["precioAnterior"]) > 0)
                {
                    modMasVendidosFotoReturn = modMasVendidosFotoReturn + Ui.Lf() + "Precio anterior: <s>" +
                                               Functions.Valor(row["precioAnterior"]) + "</s> €";
                }

                modMasVendidosFotoReturn = modMasVendidosFotoReturn + Ui.Lf() + "Precio: " +
                                           Functions.Valor(row[Variables.User.PrecioAMostrar]) + " €" + Ui.Lf() +
                                           Ui.Link(Ui.Image("tienda/anadir4.gif", 0, ""),
                                               Variables.App.directorioPortal + "tienda/anadirProducto.aspx?id=" +
                                               Functions.Valor(row["idArticulo"].ToString())) + "\r\n";
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "</td>" + "\r\n";

                f = f + 1;

                if (!((f % columnas) == 0 | haz == false)) continue;
                if (!(haz))
                {
                    for (int t = 1; t <= (columnas - (f % columnas)); t++)
                    {
                        modMasVendidosFotoReturn = modMasVendidosFotoReturn + "<td>&nbsp;</td>";
                    }
                }
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "</tr>" + "\r\n";
            }

            if (modMasVendidosFotoReturn != "")
            {
                modMasVendidosFotoReturn = modMasVendidosFotoReturn + "</table>" + "\r\n";
            }

            return modMasVendidosFotoReturn;
        }
    }
}