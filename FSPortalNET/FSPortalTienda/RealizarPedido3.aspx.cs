// <fileheader>
// <copyright file="realizarPedido3.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: tienda\realizarPedido3.aspx.cs
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
using FSMail;

namespace FSTienda
{
    public class RealizarPedido3 : BasePage
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string sSQL = "";

            if (Web.RequestInt("idCesta") == 0)
            {
                Response.Redirect(Variables.App.directorioPortal + "default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            DataTable dt =
                db.Execute("select * from " + Variables.App.prefijoTablas + "lineasCesta where idCabeceraCesta=" +
                           Web.RequestInt("idCesta"));

            if (dt.Rows.Count == 0)
            {
                sb.Append("\r\n" + "Problemas en la creación del pedido. Cesta sin lineas." + Ui.Lf() + Ui.Lf());
                sb.Append("\r\n" + "<p><a href='" + Variables.App.directorioPortal + "tienda/'>Volver a la tienda.</a></p>");
            }
            else
            {
                string campos = null;
                string valores = null;

                campos = "";
                valores = "";
                foreach (string item in Request.Form)
                {
                    campos = campos + item + ",";
                    valores = valores + "'" + Request.Form[item] + "',";
                }
                campos = campos + "fecha,idCliente";
				valores = valores + "'" + FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now) + "'," + Functions.Valor(Variables.User.UsuarioId);

                db.ExecuteNonQuery("insert into " + Variables.App.prefijoTablas + "cabecerapedido (" + campos + ") VALUES (" +
                                   valores + ")");
                string codPedido = Functions.Valor(db.GetIdentity());


                dt =
                    db.Execute("select * from " + Variables.App.prefijoTablas + "lineasCesta where idCabeceraCesta=" +
                               Web.RequestInt("idCesta"));


                if (dt.Rows.Count == 0)
                {
                    sb.Append("\r\n" + "Problemas en la creación del pedido. Cesta sin lineas." + Ui.Lf() + Ui.Lf());
                    sb.Append("\r\n" + "<p><a href='" + Variables.App.directorioPortal + "tienda/'>Volver a la tienda.</a></p>");
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        DataTable dtArticulo =
                            db.Execute("select * from " + Variables.App.prefijoTablas + "articulos where idArticulo=" +
                                       row["idArticulo"]);
                        sSQL = "insert into " + Variables.App.prefijoTablas +
                               "lineaspedido (idArticulo,unidades,talla,dto,idCabeceraPedido,descripcion,titulo,precio,precioCompra) VALUES (" +
                               row["idArticulo"] + "," + row["unidades"] + ",'" + row["talla"] + "'," + row["dto"] + "," +
                               codPedido + ",'" + dtArticulo.Rows[0]["descripcion"] + "','" +
                               dtArticulo.Rows[0]["titulo"] + "'," +
                               TextUtil.Replace(NumberUtils.NumberDouble(dtArticulo.Rows[0][Variables.User.PrecioAMostrar]).ToString(), ",",
                                   ".") + "," +
                               TextUtil.Replace(NumberUtils.NumberDouble(dtArticulo.Rows[0]["precioCompra"]).ToString(), ",", ".") + ")";

                        db.ExecuteNonQuery(sSQL);
                    }

                    double total = 0;
                    double portes = 0;
                    total = NumberUtils.NumberDouble(Request["total"]);
                    portes = NumberUtils.NumberDouble(Request["portes"]);

                    db.Execute("delete from " + Variables.App.prefijoTablas + "cabeceraCesta where codigo=" +
                               Web.RequestInt("idCesta"));

                    string sSubject = null;
                    string sbody = null;
                    sSubject = "Pedido por Internet (INICIO)";
                    sbody = "Se ha realizado un pedido por internet." + "\r\n" + "\r\n";
                    sbody = sbody + "Dirección del pedido: " + Variables.App.webHttp + Variables.App.directorioPortal +
                            "tienda/verMisPedidos.aspx?idPedido=" + codPedido + "\r\n";

                    try
                    {
						SendMail.SendMailMessage(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sbody, Variables.App.correoInfo, "Pedido - " + Variables.App.nombreWeb, Variables.App.plantillaCorreo);
                    }
                    catch (System.Exception e)
                    {
                        sb.Append(e.Message);
                    }

//                    string v_nombre = null,
//                        v_apellido1 = null,
//                        v_apellido2 = null,
//                        v_domicilio = null,
//                        v_portal = null,
//                        v_piso = null,
//                        v_poblacion = null,
//                        v_provincia = null,
//                        v_codigopostal = null,
//                        v_telefono1 = null,
//                        v_pais = null,
//                        v_email = null;
//
//                    v_nombre = Functions.ValorRequest("nombre");
//                    v_apellido1 = Functions.ValorRequest("apellido1");
//                    v_apellido2 = Functions.ValorRequest("apellido2");
//                    v_domicilio = Functions.ValorRequest("domicilio");
//                    v_portal = Functions.ValorRequest("portal");
//                    v_piso = Functions.ValorRequest("piso");
//                    v_poblacion = Functions.ValorRequest("poblacion");
//                    v_provincia = Functions.ValorRequest("provincia");
//                    v_codigopostal = Functions.ValorRequest("codigopostal");
//                    v_telefono1 = Functions.ValorRequest("telefono1");
//                    v_pais = Functions.ValorRequest("pais");
//                    v_email = Functions.ValorRequest("email");

                    sb.Append("\r\n" + Ui.Right(Ui.Bold("Paso 3/3")));
                    sb.Append("\r\n" + "<strong>Finalizar Pedido</strong>" + Ui.Lf());
                    sb.Append("\r\n" + "<hr />");
                    sb.Append("\r\n" + @"<p align=""left"">");
                    if (Request["formapago"] == "TARJETA")
                    {
                        sb.Append("\r\n" +
                                  "Para finalizar el proceso de compra, debe realizar el pago, pulsando en el bot?n inferior. Importe del pedido: " +
                                  (total + portes) + ".");
                        sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                        sb.Append("\r\n" + @"<!-- include file=""tpvPaypal/paypal.aspx"" -->");
                        sb.Append("\r\n" + @"<!-- include file=""tpv4b/4b.aspx"" -->");
                        sb.Append("\r\n" + @"<!-- include file=""tpvBBVA/bbva.aspx"" -->");
                    }
                    else if (Request["formapago"] == "INGRESO")
                    {
                        sb.Append("\r\n" +
                                  "Para finalizar el proceso de compra, debe realizar el ingreso en la cuenta: " +
                                  Variables.App.cuentaPago +
                                  ". Una vez el pago sea realizado, debe enviarnos a la cuenta de correo: " +
                                  Variables.App.correoInfo + ", el justificante de ingreso. Importe del pedido: " +
                                  (total + portes) + ".");
                    }
                    else if (Request["formapago"] == "CONTRAREEMBOLSO")
                    {
                        sb.Append("\r\n" +
                                  "En breves días, le enviaremos el pedido a la direcci?n facilitada.  Importe del pedido: " +
                                  (total + portes) + ".");
                    }

                    sb.Append("\r\n" + Ui.Lf() + Ui.Lf() +
                              "Para revisar el estado del pedido, utilize la opción 'Mis pedidos' desde ");
                    sb.Append("\r\n" + "el menú de la tienda.</p>");
                    sb.Append("\r\n" + "<p>Esperamos que la compra haya sido de su agrado.</p>" + Ui.Lf() + Ui.Lf());
                    sb.Append("\r\n" + "<p><a href='" + Variables.App.directorioPortal + "tienda/'>Volver a la tienda.</a></p>");
                }
            }

            sb.Append("\r\n" + Ui.Lf() + Ui.Lf());

            return sb.ToString();
        }
    }
}