// <fileheader>
// <copyright file="hasolvidado.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\hasolvidado.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using FSPortal;
using FSLibrary;
using FSQueryBuilder;
using FSQueryBuilder.Enums;
using FSQueryBuilder.QueryParts.Where;
using FSNetwork;
using FSDatabase;
using FSMail;
using FSCrypto;

namespace FSPaginas.Servicios
{
    public class HasOlvidado : BasePage
    {
        public string sEmail = "";
        public string sErr = "";


        protected void Page_Load(Object sender, EventArgs e)
        {
            contenido = Inicio();
        }

        private string Inicio()
        {
            StringBuilder sb = new StringBuilder("");
            string sPassword = "";
            string sUsuario = "";
            bool bUsuario = false;

            bool bErr = false;
            if (Web.Request("cmdSend") != "")
            {
                sEmail = Web.Request("txtEmail");
                if (Web.Request("chkUsuario") != "")
                {
                    bUsuario = true;
                }

                if (!TextUtil.IsEmail(sEmail))
                {
                    sErr = FuncionesWeb.Idioma(216);
                    bErr = true;
                }
                else
                {
                    BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

                    SelectQueryBuilder sqB = new SelectQueryBuilder();
                    sqB.Columns.SelectColumns("usuario");
                    sqB.TableSource = Variables.App.prefijoTablas + "Usuarios";
                    sqB.Where = new SimpleWhere(sqB.TableSource, "eMail", Comparison.Equals, sEmail);

                    DataTable dt = db.Execute(sqB.BuildQuery());

                    if (dt.Rows.Count == 0)
                    {
                        bErr = true;
                        sErr = FuncionesWeb.Idioma(229);
                    }
                    else
                    {
                        Random r = new Random();
                        
                        for (int iTemp = 1; iTemp <= 10; iTemp++)
                        {
                            int iChar = NumberUtils.NumberInt(97 + (r.Next(25)));
                            sPassword = sPassword + Convert.ToChar(iChar);
                        }
                        sUsuario = dt.Rows[0]["Usuario"].ToString();

						Crypto crypt = new Crypto();

                        string sSql = "UPDATE " + Variables.App.prefijoTablas + "usuarios SET clave='" + crypt.Md5(sPassword) +
                                      "' WHERE usuario='" + sUsuario + "'";
                        db.ExecuteNonQuery(sSql);
                    }
                }


                if (!bErr)
                {
                    string sSubject = FuncionesWeb.Idioma(230);
                    string sBody = FuncionesWeb.Idioma(231) + "\r\n" + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(232) + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(233) + "\r\n" + "\r\n";
                    if (bUsuario)
                    {
                        sBody = sBody + FuncionesWeb.Idioma(234) + ": " + sUsuario + "\r\n";
                    }
                    sBody = sBody + FuncionesWeb.Idioma(235) + ": " + sPassword + "\r\n" + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(236) + "\r\n";


                    string strSendMsg = "";

                    try
                    {
						new SendMail().SendMailAsync(sEmail, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, FuncionesWeb.Idioma(237), Variables.App.plantillaCorreo);
                    }
                    catch (System.Exception e)
                    {
                        strSendMsg = e.Message;
                    }

                    sb.Append(Ui.Lf() + Ui.Lf());
                    sb.Append("<p class='cabemaspeque'>");

                    if (strSendMsg != "")
                    {
                        sb.Append("\r\n" + FuncionesWeb.Idioma(330) + strSendMsg);
                    }
                    else
                    {
                        sb.Append(FuncionesWeb.Idioma(238));
                        sb.Append(", " + Ui.Lf());
                        sb.Append("</p>");
                        sb.Append("<p class='textomaspeque'>");
                        sb.Append(FuncionesWeb.Idioma(239));

                        sb.Append(Ui.Lf());
                        sb.Append(FuncionesWeb.Idioma(233));
                    }

                    sb.Append(Ui.Lf() + Ui.Lf());

                    sb.Append(FuncionesWeb.Idioma(240));
                    sb.Append("</p>");
                }
                else
                {
                    sb.Append(FormHasOlvidado());
                }
            }
            else
            {
                sb.Append(FormHasOlvidado());
            }

            return sb.ToString();
        }


        public string FormHasOlvidado()
        {
            StringBuilder sb = new StringBuilder("");

            if (sErr == "")
            {
                sb.Append("\r\n" + Ui.Lf());
                sb.Append("\r\n" + "<p class='accionpeque'>");
                sb.Append("\r\n" + FuncionesWeb.Idioma(241));
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + "<p class='textomaspeque'>");
                sb.Append("\r\n" + FuncionesWeb.Idioma(242));
                sb.Append("\r\n" + "</p>" + Ui.Lf());
            }
            else
            {
                sb.Append("\r\n" + Ui.Lf());
                sb.Append("\r\n" + "<p class='accionpeque'>");
                sb.Append("\r\n" + FuncionesWeb.Idioma(241));
                sb.Append("\r\n" + "</p>");
                sb.Append("\r\n" + @"<p class=""cabemaspeque"">" + sErr + "</p>");
            }

            sb.Append("\r\n" + Ui.Lf());
            sb.Append("\r\n" + "<form action='hasolvidado.aspx' method='post'>");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(243) + "</td>");
            sb.Append("\r\n" + "<td><input type='text' name='txtEmail' value='" + sEmail +
                      "' size='40' class='textboxplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'></td>");
            sb.Append("\r\n" + "<td><input type='checkbox' name='chkUsuario' value='1' />" + FuncionesWeb.Idioma(244) +
                      "</td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" + "<td><input type='submit' name='cmdSend' value=' " + FuncionesWeb.Idioma(37) +
                      " ' class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");
            sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
            return sb.ToString();
        }
    }
}