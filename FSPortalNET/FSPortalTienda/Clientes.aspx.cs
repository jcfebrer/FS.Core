// <fileheader>
// <copyright file="clientes.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\clientes.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSDatabase;

namespace FSTienda
{
    public class Clientes : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string ssql = "select * from " + Variables.App.prefijoTablas + "v_Clientes order by apellido1,apellido2,nombre";
            DataTable dt = db.Execute(ssql);

            sb.Append(@"<table border=""1"" class=""texto"" width=""100%"">");
            sb.Append("<tr>");

            sb.Append(@"<td valign=""top"" colspan=""5""> <b>Seleccione el cliente deseado.</b>" + Ui.Lf());
            sb.Append(Ui.Lf() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(
                "<td><b>Id</b></td><td><b>Nombre</b></td><td><b>1er Apellido</b></td><td><b>2º Apellido</b></td><td><b>Total Pedido</b></td>");
            sb.Append("</tr>");

            double totalClientes = 0;
            totalClientes = 0;
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append(@"<td valign=""center""><a href=""verMisPedidos.aspx?idCliente=" + row["usuarioID"] +
                          @"""><img alt="""" src=""imagenes/arrow_c.gif"" border=""0"" />");
                sb.Append(row["usuarioID"] + @"</a></td><td><a href=""" + Variables.App.paginaPerfil +
                          (Variables.App.paginaPerfil.IndexOf("?") > 0 ? "&" : "?") + "idCliente=" + row["usuarioID"] + @""">" +
                          row["nombre"] + "</a></td><td>" + row["apellido1"] + "</td><td>" + row["apellido2"] +
                          @"</td><td align=""right"">" + NumberUtils.NumberDouble(row["sumadetotal"]) +
                          NumberUtils.NumberDouble(row["sumadeportes"]) + "€</td>");
                sb.Append("</tr>");

                totalClientes += NumberUtils.NumberDouble(NumberUtils.NumberDouble(row["sumadetotal"]) + NumberUtils.NumberDouble(row["sumadeportes"]));
            }

            sb.Append("<tr>");
            sb.Append(@"<td valign=""top"" colspan=""5"" align=""right""> <b>" + totalClientes.ToString("D2") +
                      " €</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}