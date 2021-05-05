using FSLibrary;
using FSPlugin;
using FSPortal;
using System;

namespace FSCustomPlugin
{
    public class ModTienda : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Tienda();
        }

        public string Name
        {
            get { return "ModTienda"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Tienda()
        {
            string modTiendaReturn = "<form action='" + Variables.App.directorioPortal + "tienda/realizarPedido.aspx'>" + "\r\n";
            modTiendaReturn = modTiendaReturn + @"<input type=""hidden"" name=""idCesta"" value=""" +
                              FuncionesWeb.CodigoCesta(0) + @""" />";
            modTiendaReturn = modTiendaReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                              "tienda/verCategorias.aspx'>Catálogo</a>" + Ui.Lf() + "\r\n";
            modTiendaReturn = modTiendaReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                              "tienda/verProductos.aspx?oferta=si&amp;modo=registro'>Ofertas Especiales</a>" + Ui.Lf() +
                              "\r\n";
            modTiendaReturn = modTiendaReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                              "tienda/verCestas.aspx'>Cestas</a>" + Ui.Lf() + "\r\n";
            modTiendaReturn = modTiendaReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /> <a href='" + Variables.App.directorioPortal +
                              "tienda/verMisPedidos.aspx'>Mis Pedidos</a>" + "\r\n";

            modTiendaReturn = modTiendaReturn + Ui.Lf() + Ui.Lf() + "Tienes: " +
                              FuncionesWeb.NumeroArticulos(Convert.ToInt64(NumberUtils.NumberDouble(FuncionesWeb.CodigoCesta(0)))) +
                              " artículos en la <a href='" + Variables.App.directorioPortal + "tienda/verCestas.aspx?idCesta=" +
                              FuncionesWeb.CodigoCesta(0) + "'>cesta</a>." + Ui.Lf() + "\r\n";
            modTiendaReturn = modTiendaReturn + "Total: " + FuncionesWeb.TotalCesta(long.Parse(FuncionesWeb.CodigoCesta(0))) + "€" +
                              Ui.Lf() + Ui.Lf() + "\r\n";
            modTiendaReturn = modTiendaReturn +
                              "<input type='submit' name='cmdComprar' class='botonplano' value='Comprar!' />" + "\r\n";
            modTiendaReturn = modTiendaReturn + "</form>" + "\r\n";

            return modTiendaReturn;
        }
    }
}