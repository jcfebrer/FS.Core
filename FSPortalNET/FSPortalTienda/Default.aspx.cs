// <fileheader>
// <copyright file="default.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\default.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;

namespace FSTienda
{
    public class Default : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append(Ui.Lf());
            sb.Append("<p class='cabepeque'>Tienda</p>");

            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""verCategorias.aspx"">Catalogo de productos</a>" +
                      Ui.Lf() + Ui.Lf());
            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""verFabricantes.aspx"">Catalogo de productos por proveedores</a>" +
                      Ui.Lf() + Ui.Lf());
            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""verProductos.aspx?oferta=si&amp;modo=registro"">Ofertas Especiales</a>" +
                      Ui.Lf() + Ui.Lf());
            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""verCestas.aspx"">Mi cesta de la compra</a>" +
                      Ui.Lf() + Ui.Lf());
            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""verMisPedidos.aspx"">Mis pedidos</a>" +
                      Ui.Lf() + Ui.Lf());
            sb.Append(@"<img src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" alt="""" /> <a href=""" + Variables.App.directorioPortal +
                      @"paypal.aspx"">Pago con PayPal</a>" + Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}