// <fileheader>
// <copyright file="inscripcion.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: servicios\inscripcion.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Text;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSMail;

namespace FSPaginas.Servicios
{
    /// <summary>
    ///     Formulario de inscripción
    /// </summary>
    public class Inscripcion : BasePage
    {
        private bool bErr;
        private string sApellido11 = "";
        private string sApellido12 = "";
        private string sApellido13 = "";
        private string sApellido14 = "";
        private string sApellido15 = "";
        private string sApellido21 = "";
        private string sApellido22 = "";
        private string sApellido23 = "";
        private string sApellido24 = "";
        private string sApellido25 = "";
        private string sBody;
        private string sCp = "";
        private string sDireccion = "";
        private string sEmail = "";
        private string sEmail1 = "";

        private string sEmail2 = "";

        private string sEmail3 = "";

        private string sEmail4 = "";

        private string sEmail5 = "";
        private string sErr = "";
        private string sMovil = "";
        private string sName = "";
        private string sNombreDelegado = "";
        private string sNombreDelegado1 = "";
        private string sNombreDelegado2 = "";
        private string sNombreDelegado3 = "";
        private string sNombreDelegado4 = "";
        private string sNombreDelegado5 = "";
        private string sPhone = "";
        private string sPoblacion = "";
        private string sSubject;


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

            bErr = false;
            if (Web.Request("cmdSend") != "")
            {
                sName = Web.Request("txtName");
                sNombreDelegado = Web.Request("txtNombreDelegado");

                sDireccion = Web.Request("txtDireccion");
                sCp = Web.Request("txtCP");
                sPoblacion = Web.Request("txtPoblacion");
                sMovil = Web.Request("txtMovil");

                sEmail = Web.Request("txtEmail");
                sPhone = Web.Request("txtPhone");

                sNombreDelegado1 = Web.Request("txtNombreDelegado1");
                sApellido11 = Web.Request("txtApellido11");
                sApellido21 = Web.Request("txtApellido21");
                sEmail1 = Web.Request("txtEmail1");

                sNombreDelegado2 = Web.Request("txtNombreDelegado2");
                sApellido12 = Web.Request("txtApellido12");
                sApellido22 = Web.Request("txtApellido22");
                sEmail2 = Web.Request("txtEmail2");

                sNombreDelegado3 = Web.Request("txtNombreDelegado3");
                sApellido13 = Web.Request("txtApellido13");
                sApellido23 = Web.Request("txtApellido23");
                sEmail3 = Web.Request("txtEmail3");

                sNombreDelegado4 = Web.Request("txtNombreDelegado4");
                sApellido14 = Web.Request("txtApellido14");
                sApellido24 = Web.Request("txtApellido24");
                sEmail4 = Web.Request("txtEmail4");

                sNombreDelegado5 = Web.Request("txtNombreDelegado5");
                sApellido15 = Web.Request("txtApellido15");
                sApellido25 = Web.Request("txtApellido25");
                sEmail5 = Web.Request("txtEmail5");

                if (sName == "")
                {
                    sErr = "Debes indicar el nombre del equipo.";
                    bErr = true;
                }

                if (sNombreDelegado == "")
                {
                    sErr = "Debes indicar el nombre de la persona de contacto.";
                    bErr = true;
                }

                if (sNombreDelegado1 == "")
                {
                    sErr = "Debes indicar el nombre del 1er jugador.";
                    bErr = true;
                }

                if (sNombreDelegado2 == "")
                {
                    sErr = "Debes indicar el nombre del 2o jugador.";
                    bErr = true;
                }

                if (sNombreDelegado3 == "")
                {
                    sErr = "Debes indicar el nombre del 3er jugador.";
                    bErr = true;
                }

                if (sApellido11 == "")
                {
                    sErr = "Debes indicar el 1er apellido del 1er jugador.";
                    bErr = true;
                }

                if (sApellido12 == "")
                {
                    sErr = "Debes indicar el 1er apellido del 2o jugador.";
                    bErr = true;
                }

                if (sApellido13 == "")
                {
                    sErr = "Debes indicar el 1er apellido del 3er jugador.";
                    bErr = true;
                }

                if (sDireccion == "")
                {
                    sErr = "Debes indicar la dirección de la persona de contacto.";
                    bErr = true;
                }

                if (sCp == "")
                {
                    sErr = "Debes indicar el C.P. de la persona de contacto.";
                    bErr = true;
                }

                if (sPoblacion == "")
                {
                    sErr = "Debes indicar la población de la persona de contacto.";
                    bErr = true;
                }

                if (sPhone == "")
                {
                    sErr = "Debes indicar el teléfono de la persona de contacto.";
                    bErr = true;
                }

                if (!(TextUtil.IsEmail(sEmail)))
                {
                    sErr = FuncionesWeb.Idioma(216);
                    bErr = true;
                }

                if (!(bErr))
                {
                    sSubject = "Solicitud de inscripción: " + Variables.App.nombreWeb;
                    sBody = FuncionesWeb.Idioma(183) + " : " + sName + "\r\n";
                    sBody = sBody + "Persona de contacto : " + sNombreDelegado + "\r\n";

                    sBody = sBody + "Dirección : " + sDireccion + "\r\n";
                    sBody = sBody + "C.P. : " + sCp + "\r\n";
                    sBody = sBody + "Población : " + sPoblacion + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(181) + " : " + sEmail + "\r\n";
                    sBody = sBody + FuncionesWeb.Idioma(329) + " : " + sPhone + "\r\n";
                    sBody = sBody + "Movil : " + sMovil + "\r\n";


                    sBody = sBody + "Nombre Jugador1 : " + sNombreDelegado1 + "\r\n";
                    sBody = sBody + "Apellido1 : " + sApellido11 + "\r\n";
                    sBody = sBody + "Apellido2 : " + sApellido21 + "\r\n";
                    sBody = sBody + "Email : " + sEmail1 + "\r\n";

                    sBody = sBody + "Nombre Jugador2 : " + sNombreDelegado2 + "\r\n";
                    sBody = sBody + "Apellido1 : " + sApellido12 + "\r\n";
                    sBody = sBody + "Apellido2 : " + sApellido22 + "\r\n";
                    sBody = sBody + "Email : " + sEmail2 + "\r\n";

                    sBody = sBody + "Nombre Jugador3 : " + sNombreDelegado3 + "\r\n";
                    sBody = sBody + "Apellido1 : " + sApellido13 + "\r\n";
                    sBody = sBody + "Apellido2 : " + sApellido23 + "\r\n";
                    sBody = sBody + "Email : " + sEmail3 + "\r\n";

                    sBody = sBody + "Nombre Jugador4 : " + sNombreDelegado4 + "\r\n";
                    sBody = sBody + "Apellido1 : " + sApellido14 + "\r\n";
                    sBody = sBody + "Apellido2 : " + sApellido24 + "\r\n";
                    sBody = sBody + "Email : " + sEmail4 + "\r\n";

                    sBody = sBody + "Nombre Jugador5 : " + sNombreDelegado5 + "\r\n";
                    sBody = sBody + "Apellido1 : " + sApellido15 + "\r\n";
                    sBody = sBody + "Apellido2 : " + sApellido25 + "\r\n";
                    sBody = sBody + "Email : " + sEmail5 + "\r\n";

                    string strSendMsg = "";

                    try
                    {
						new SendMail().SendMailAsync(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, "Inscripción", Variables.App.plantillaCorreo);
                    }
                    catch (System.Exception e)
                    {
                        strSendMsg = e.Message;
                    }

                    sb.Append("\r\n" + Ui.Lf() + Ui.Lf());
                    sb.Append("\r\n" + "<p class='cabemaspeque'>");

                    if (strSendMsg != "")
                    {
                        sb.Append("\r\n" + FuncionesWeb.Idioma(330) + strSendMsg);
                    }
                    else
                    {
                        sb.Append("\r\n" + FuncionesWeb.Idioma(238) + ", " + Ui.Lf() + Ui.Lf());
                        sb.Append("\r\n" + FuncionesWeb.Idioma(294));
                    }

                    sb.Append("\r\n" + "</p>");
                    sb.Append("\r\n" + "<p class='textomaspeque'>");
                    sb.Append("\r\n" + FuncionesWeb.Idioma(240));
                    sb.Append("\r\n" + "</p>");
                }
                else
                {
                    sb.Append(ShowForm());
                }
            }
            else
            {
                sb.Append(ShowForm());
            }

            return sb.ToString();
        }


        private string ShowForm()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("\r\n" + @"<p class=""error"">");
            sb.Append("\r\n" + sErr);
            sb.Append("\r\n" + "</p>");
            sb.Append("\r\n" + @"<form action=""inscripcion.aspx?send=1"" method=""post"">");
            sb.Append("\r\n" + @"<table border=""0"">");
            sb.Append("\r\n" + "<tr><td><b>DATOS DEL EQUIPO</b></td></tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre del equipo</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtName"" class='textboxplano' value=""" + sName +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Persona de contacto</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado"" class='textboxplano' value=""" +
                      sNombreDelegado + @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Direcci?n</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtDireccion"" class='textboxplano' value=""" +
                      sDireccion + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* C.P.</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtCP"" class='textboxplano' value=""" + sCp +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Poblaci?n</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtPoblacion"" class='textboxplano' value=""" +
                      sPoblacion + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* " + FuncionesWeb.Idioma(329) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtPhone"" class='textboxplano' value=""" + sPhone +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Movil</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtMovil"" class='textboxplano' value=""" + sMovil +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* " + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail"" class='textboxplano' value=""" + sEmail +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");


            sb.Append("\r\n" + "<tr><td><b>DATOS 1er JUGADOR</b></td></tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado1"" class='textboxplano' value=""" +
                      sNombreDelegado1 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Apellido 1</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido11"" class='textboxplano' value=""" +
                      sApellido11 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Apellido 2</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido21"" class='textboxplano' value=""" +
                      sApellido21 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail1"" class='textboxplano' value=""" + sEmail1 +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr><td><b>DATOS 2o JUGADOR</b></td></tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado2"" class='textboxplano' value=""" +
                      sNombreDelegado2 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Apellido 1</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido12"" class='textboxplano' value=""" +
                      sApellido12 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Apellido 2</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido22"" class='textboxplano' value=""" +
                      sApellido22 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail2"" class='textboxplano' value=""" + sEmail2 +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr><td><b>DATOS 3ro JUGADOR</b></td></tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado3"" class='textboxplano' value=""" +
                      sNombreDelegado3 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Apellido 1</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido13"" class='textboxplano' value=""" +
                      sApellido13 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Apellido 2</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido23"" class='textboxplano' value=""" +
                      sApellido23 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail3"" class='textboxplano' value=""" + sEmail3 +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr><td><b>DATOS 4o JUGADOR (opcional)</b></td></tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado4"" class='textboxplano' value=""" +
                      sNombreDelegado4 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Apellido 1</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido14"" class='textboxplano' value=""" +
                      sApellido14 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Apellido 2</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido24"" class='textboxplano' value=""" +
                      sApellido24 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail4"" class='textboxplano' value=""" + sEmail4 +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr><td><b>DATOS 5o JUGADOR (opcional)</b></td></tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Nombre</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtNombreDelegado5"" class='textboxplano' value=""" +
                      sNombreDelegado5 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>* Apellido 1</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido15"" class='textboxplano' value=""" +
                      sApellido15 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>Apellido 2</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtApellido25"" class='textboxplano' value=""" +
                      sApellido25 + @"""></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td class='cabemaspeque'>" + FuncionesWeb.Idioma(181) + "</td>");
            sb.Append("\r\n" + @"<td><input type='text' name=""txtEmail5"" class='textboxplano' value=""" + sEmail5 +
                      @"""></td>");
            sb.Append("\r\n" + "</tr>");

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + "<td>&nbsp;</td>");
            sb.Append("\r\n" +
                      @"<td><input type='submit' name=""cmdSend"" value="" Enviar inscripci?n "" class='botonplano' /></td>");
            sb.Append("\r\n" + "</tr>");
            sb.Append("\r\n" + "</table>");
            sb.Append("\r\n" + "</form>");

            return sb.ToString();
        }
    }
}