// <fileheader>
// <copyright file="verCategorias.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\verCategorias.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSDatabase;

namespace FSTienda
{
    /// <summary>
    ///     Muestra las categorías
    /// </summary>
    public class VerCategorias : BasePage
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
            int cat = 0;
            if (Web.RequestInt("cat") == 0)
            {
                cat = 1;
            }
            else
            {
                cat = Web.RequestInt("cat");
            }

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            DataTable dtCategorias =
                db.Execute("SELECT * FROM " + Variables.App.prefijoTablas + "Categorias WHERE activo = true and idCategoriaPadre=" +
                           cat + " ORDER BY titulo ASC");

            sb.Append("\r\n" + Ui.Lf() + "Seleccione la categoría deseada:<hr />");
            sb.Append("\r\n" + @"<table width=""75%"" border=""0"" class=""texto"">");


            foreach (DataRow row in dtCategorias.Rows)
            {
                sb.Append("\r\n" + "<tr><td>");
                sb.Append("\r\n" + @"<img alt="""" border=""0"" src=""" + Variables.App.directorioPortal +
                          @"imagenes/arrow_c.gif""/>");
                if (Functions.ValorBool(Variables.User.Administrador))
                {
                    sb.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal +
                              "admin/editor/showrecord.aspx?q=&amp;tablename=categorias&amp;comebackto=" +
                              Variables.User.ComeBack + "&amp;fld=IdCategoria&amp;val=" + row["idCategoria"] +
                              @"&amp;fldtype=System.Integer&amp;page=1""><img src=""" + Variables.App.directorioPortal +
                              @"imagenes/edit.gif"" alt=""Editar Categoría"" border=""0"" align=""middle"" /></a>&nbsp;");
                    sb.Append("\r\n" + @"<a href=""" + Variables.App.directorioPortal +
                              "admin/editor/deleterecord.aspx?q=&amp;tablename=categorias&amp;comebackto=" +
                              Variables.User.ComeBack + "&amp;fld=IdCategoria&amp;val=" + row["idCategoria"] +
                              @"&amp;fldtype=System.Integer&amp;page=1""><img src=""" + Variables.App.directorioPortal +
                              @"imagenes/papelera.gif"" alt=""Borrar Categoría"" border=""0"" align=""middle"" /></a>&nbsp;");
                }

				if (!(FuncionesWeb.TieneCategorias(long.Parse(row["idCategoria"].ToString()))))
                {
                    sb.Append("\r\n" + @"<a href=""verProductos.aspx?modo=lista&amp;cat=" + row["idCategoria"] + @""">" +
                              row["titulo"] + "</a>");
                }
                else
                {
					sb.Append("\r\n" + FuncionesWeb.MuestraSubCategorias(long.Parse(row["idCategoria"].ToString())));
                }

                sb.Append("\r\n" + "</td>");
                sb.Append("\r\n" + "</tr>");
            }


            sb.Append("\r\n" + "</table>");

            if (cat != 1)
            {
                sb.Append("\r\n" + Ui.Lf() + "<img src='" + Variables.App.directorioPortal +
                          "imagenes/bullet.gif' align='middle'> <a href='verCategorias.aspx'>Volver atrás.</a>");
            }

            if (Functions.ValorBool(Variables.User.Administrador))
            {
                sb.Append("\r\n" + Ui.Lf() + @"<img alt="""" src=""" + Variables.App.directorioPortal +
                          @"imagenes/bullet.gif"" align=""middle"" /> <a href=""" + Variables.App.directorioPortal +
                          "admin/editor/showrecord.aspx?tablename=Categorias&amp;add=1&amp;page=1&amp;autoSel=" + cat +
                          @"&amp;autoSelField=idCategoriaPadre"" class=""cabemaspeque"">Añadir categoría</a>" + Ui.Lf() +
                          Ui.Lf());
            }

            return sb.ToString();
        }
    }
}