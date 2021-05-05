// <fileheader>
// <copyright file="verProductos.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\verProductos.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using Math = System.Math;
using FSNetwork;
using FSDatabase;

namespace FSTienda
{
    /// <summary>
    ///     Mostramos los productos
    /// </summary>
    public class VerProductos : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
            string sModo = null;
            bool mostrarDescripcionCat = false;
            string ssql = "";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            mostrarDescripcionCat = false;

            sModo = Web.Request("modo");

            if (Web.Request("cat") != "")
            {
                ssql = "SELECT * FROM " + Variables.App.prefijoTablas + "v_Articulos WHERE idCategoria=" +
                       Web.RequestInt(Request["cat"]) + " order by popularidad DESC";
                mostrarDescripcionCat = true;
            }
            else
            {
                if (Web.Request("buscar") == "SI")
                {
                    ssql = "SELECT TOP 24 * FROM " + Variables.App.prefijoTablas + "Articulos WHERE descripcion like '%" +
                           Web.Request("textfield") + "%' order by popularidad DESC";
                }
                else
                {
                    ssql = "SELECT TOP 6 * FROM " + Variables.App.prefijoTablas +
                           "Articulos WHERE mostrarHome = true ORDER BY popularidad DESC";
                }
            }

            if (Web.Request("ofertas") == "si")
            {
                ssql = "SELECT * FROM " + Variables.App.prefijoTablas +
                       "Articulos WHERE mostrarHome = true ORDER BY popularidad DESC";
            }

            if (Web.Request("prov") != "")
            {
                ssql = "SELECT * FROM " + Variables.App.prefijoTablas + "Articulos WHERE idProveedor = " +
                       Web.RequestInt("prov") + " ORDER BY popularidad DESC";
            }

            if (Web.Request("buscados") == "si")
            {
                ssql = "SELECT * FROM " + Variables.App.prefijoTablas +
                       "Articulos WHERE masBuscado = true ORDER BY popularidad DESC";
            }


            if (mostrarDescripcionCat)
            {
                DataTable dtCategoria =
                    db.Execute("select * from " + Variables.App.prefijoTablas + "categorias where activo=true and idCategoria=" +
                               Web.RequestInt("cat"));

                if (dtCategoria.Rows.Count > 0)
                {
                    sb.Append(
                        FuncionesWeb.MuestraSubCategorias2(Convert.ToInt64(dtCategoria.Rows[0]["idCategoriaPadre"].ToString())));

                    sb.Append("\r\n" + "<a href='verCategorias.aspx'>[Volver a categorías]</a>");
                    sb.Append("<h2><b>");

                    sb.Append(Ui.EditPage("Categorias", "idCategoria", Functions.Valor(dtCategoria.Rows[0]["idCategoria"]),
                        "Editar categor?a", "Borrar categor?a"));

                    sb.Append(dtCategoria.Rows[0]["titulo"]);
                    sb.Append("</b></h2><hr />");
                    sb.Append("\r\n" + "<table width='100%'><tr>");
                    sb.Append("\r\n" + "<td>" + dtCategoria.Rows[0]["descripcion"] + "</td>");

                    if (Functions.Valor(dtCategoria.Rows[0]["imagen"]) != "")
                    {
                        sb.Append(@"<td align='center'><img alt="""" src=""" + Variables.App.directorioWeb + "tienda/fotos/" +
                                  dtCategoria.Rows[0]["imagen"] + @""" /></td>");
                    }
                    sb.Append("</tr></table>");
                }
            }

            DataTable dtArticulos = db.Execute(ssql);

            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + "Listado de productos.");
            sb.Append("\r\n" + "<hr />");
            sb.Append("\r\n" + Ui.Lf());


            if (dtArticulos.Rows.Count == 0)
            {
                sb.Append("\r\n" + Ui.Lf());
                sb.Append("\r\n" + "Lo sentimos, la condición de búsqueda indicada, no devuelve ningún resultado.");
                sb.Append("\r\n" + Ui.Lf());
            }


            double totPopu = 0;
            DataTable dtTotPop = db.Execute("SELECT sumadepopularidad from " + Variables.App.prefijoTablas + "v_TotalPopularidad");

            totPopu = NumberUtils.NumberDouble(dtTotPop.Rows[0]["sumadepopularidad"]);

            if (sModo == "lista")
            {
                sb.Append("\r\n" + @"<table border=""1"" width=""100%"">");
                sb.Append("\r\n" + @"<tr class=""Titulo"">");
                string litStock = "<td>Stock</td>";
                sb.Append("\r\n" + "<td>Imagen</td><td>Descripción</td><td>Popularidad</td><td>Precio</td>" +
                          (Variables.App.disponibilidad ? litStock : "<td>Añadir</td>"));
                sb.Append("\r\n" + "</tr>");
            }
            else
            {
                sb.Append("\r\n" + @"<table border=""1"" width=""100%"">");
            }

            int iCont = 1;

            foreach (DataRow row in dtArticulos.Rows)
            {
                if (sModo == "lista")
                {
                    sb.Append("\r\n" + @"<tr><td align=""center"" class=""cabemaspeque"">");
                    sb.Append("\r\n" + @"<a href=""#"" onclick=""javascript:browse('detalleArticulo.aspx?id=" +
                              row["idArticulo"] + @"');""><img alt="""" border=""0"" src=""" + Variables.App.directorioWeb +
                              "tienda/fotos/" + row["imagen1"] + @""" onerror=""// code");
                    sb.Append("\r\n" + "var alternativeSrc = '" + Variables.App.directorioWeb + "tienda/fotos/sinFoto.gif';");
                    sb.Append("\r\n" + @"this.src = alternativeSrc;""/></a>");
                    sb.Append("\r\n" + "</td>");
                    sb.Append("\r\n" + "<td class='cabemaspeque'>");

                    sb.Append(Ui.EditPage("Articulos", "idArticulo", Functions.Valor(row["idArticulo"]), "Editar Artículo",
                        "Borrar Artículo"));

                    sb.Append("\r\n" + @"<a href=""#"" onclick=""javascript:browse('detalleArticulo.aspx?id=" +
                              row["idArticulo"] + @"');"">" + row["titulo"] + Ui.Lf() + row["descripcion"] + "</a>");

                    if (NumberUtils.NumberDouble(row["unidadVenta"].ToString()) > 1)
                    {
                        sb.Append("\r\n" + Ui.Lf() + "Unidad de venta: " +
                                  (NumberUtils.NumberDouble(row["unidadVenta"].ToString()) == 0 ? "1" : row["unidadVenta"].ToString()));
                    }
                    sb.Append("\r\n" + "</td>");
                    sb.Append("\r\n" + "<td class='cabemaspeque'>");

                    double pop = 0;
                    if (totPopu != 0)
                    {
                        pop = (NumberUtils.NumberDouble(row["popularidad"].ToString())*5)/totPopu;
                    }

                    sb.Append("\r\n" + @"<img alt="""" src=""" + Variables.App.directorioWeb + "tienda/imagenes/pop" +
                              Math.Floor(pop) + @".jpg"" />");
                    sb.Append("\r\n" + "</td>");
                    sb.Append("\r\n" + @"<td align=""right"" class='cabemaspeque' nowrap=""nowrap"">");
                    sb.Append("\r\n" + row[Variables.User.PrecioAMostrar] + " €");
                    sb.Append("\r\n" + "</td>");
                    if (Variables.App.disponibilidad)
                    {
                        sb.Append("\r\n" + @"<td align=""right"" class='cabemaspeque'>");
                        sb.Append("\r\n" + row["stock"] + "</td>");
                    }
                    sb.Append("\r\n" + @"<td align=""center"" class='cabemaspeque'><a href=""anadirProducto.aspx?id=" +
                              row["idArticulo"] + "&amp;cat=" + Web.RequestInt("cat") +
                              @"""><img alt="""" border=""0"" src=""imagenes/anadir2.gif"" /></a>");
                    sb.Append("\r\n" + "</td>");
                    sb.Append("\r\n" + "</tr>");
                }
                else
                {
                    if ((iCont%2) == 0)
                    {
                        sb.Append("<tr>");
                    }
                    sb.Append("\r\n" + @"<td width=""50%"" class='textomaspeque'>");

                    sb.Append("\r\n" + @"<a href=""#"" onclick=""javascript:browse('detalleArticulo.aspx?id=" +
                              row["idArticulo"] + @"');""><img alt="""" border=""0"" src=""fotos/" + row["imagen1"] +
                              @".jpg"" onerror=""// code");
                    sb.Append("\r\n" + "var alternativeSrc = 'fotos/sinFoto.gif';");
                    sb.Append("\r\n" + @"this.src = alternativeSrc;""/></a>");
                    sb.Append("\r\n" + Ui.Lf() + @"<div class=""descipcion"">");

                    sb.Append(Ui.EditPage("Articulos", "idArticulo", Functions.Valor(row["idArticulo"]), "Editar Art?culo",
                        "Borrar Artículo"));

                    sb.Append("\r\n" + row["titulo"] +
                              @"</div><a href=""#"" onclick=""javascript:browse('detalleArticulo.aspx?id=" +
                              row["idArticulo"] + @"');"">Más...</a>" + Ui.Lf());
                    sb.Append("\r\n" + "Precio: " + row[Variables.User.PrecioAMostrar] + " € (Stock: " + row["stock"] + ")" +
                              Ui.Lf());
                    sb.Append("\r\n" + "Popularidad: " + row["popularidad"] + @" <img alt="""" src=""" +
                              Variables.App.directorioWeb + "tienda/imagenes/pop" + NumberUtils.NumberInt(row["popularidad"]) + 1 +
                              @".jpg"" />");
                    sb.Append("\r\n" + Ui.Lf() + @"<a href=""anadirProducto.aspx?id=" + row["idArticulo"] + "&amp;cat=" +
                              Web.RequestInt("cat") +
                              @"""><img alt="""" border=""0"" src=""imagenes/anadir2.gif"" /></a>");

                    sb.Append("\r\n" + "</td>");
                    if ((iCont%2) != 0)
                    {
                        sb.Append("\r\n" + "</tr>");
                    }
                }
                iCont = iCont + 1;
            }

            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + Ui.Lf());


            sb.Append("\r\n" + Ui.Lf());

            if (mostrarDescripcionCat)
            {
				if (FuncionesWeb.TieneCategorias(Web.RequestInt("cat")))
                {
					sb.Append(FuncionesWeb.MuestraSubCategorias(Web.RequestInt("cat")));
                }
            }

            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + Ui.Lf() + @"<img alt="""" src=""" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif"" align=""middle"" /> <a href=""javascript:history.back();"" class='cabemaspeque'>Volver atrás</a>" +
                      Ui.Lf());

            if (Web.Request("buscados") == "si")
            {
                sb.Append("\r\n" +
                          "<strong>Si desea solicitar alguno de estos productos, puede hacerlo a través de la dirección de correo: <a href='mailto:" + Variables.App.correoInfo + "'>" + Variables.App.correoInfo + "</a>, o a través del teléfono: 94 4132230. Gracias.</strong>");
            }
            if (Functions.ValorBool(Variables.User.Administrador))
            {
                sb.Append("\r\n" + Ui.Lf() + @"<img alt="""" src=""" + Variables.App.directorioPortal +
                          @"imagenes/bullet.gif"" align=""middle"" /> <a href=""" + Variables.App.directorioPortal +
                          @"admin/editor/showrecord.aspx?tablename=Articulos&amp;add=1&amp;page=1"" class='cabemaspeque'>Añadir producto</a>" +
                          Ui.Lf());
                sb.Append("\r\n" + Ui.Lf() + @"<img alt="""" src=""" + Variables.App.directorioPortal +
                          @"imagenes/bullet.gif"" align=""middle"" /> <a href=""" + Variables.App.directorioPortal +
                          "admin/editor/showrecord.aspx?tablename=Categorias&amp;add=1&amp;page=1&autoSel=" +
                          Web.RequestInt("cat") +
                          @"&autoSelField=idCategoriaPadre"" class='cabemaspeque'>Añadir categoría</a>" + Ui.Lf() +
                          Ui.Lf());
            }
            sb.Append("\r\n" + Ui.Lf());

            return sb.ToString();
        }
    }
}