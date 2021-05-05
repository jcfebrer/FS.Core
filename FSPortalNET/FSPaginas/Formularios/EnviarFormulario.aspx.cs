// <fileheader>
// <copyright file="EnviarFormulario.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: formularios\EnviarFormulario.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;
using FSException;
using FSMail;

namespace FSPaginas.Formularios
{
    /// <summary>
    ///     Clase para enviar un formulario
    /// </summary>
    public class EnviarFormulario : BasePage
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
            Portal portal = new Portal();
            Upload funcUpload = new Upload();

            string msg = Variables.Parser.frmMensajeOK;

            //db.BeginTransaction();

            if (Variables.Parser.frmPagina == "")
            {
                Variables.Parser.frmPagina = Variables.App.directorioPortal;
            }

            try
            {
				Utils.CheckData(Request, Variables.Parser.frmCampos, Variables.Parser.frmTruncar);

                if (Request.Files.Count > 0)
                {
                    Server.ScriptTimeout = 1000;
                    Response.Buffer = true;

                    long lngErrorFileSize = 0;

                    bool blnExtensionOk = true;

                    string[] saryFileUploadTypes = TextUtil.Trim(Variables.Parser.frmFileTypes)
                        .Split(",".Split("".ToCharArray()), StringSplitOptions.None);


                    funcUpload.FileUpload(Server.MapPath(Variables.Parser.frmFileUploadPath), saryFileUploadTypes,
						Variables.Parser.frmFileMaxSize, ref lngErrorFileSize, ref blnExtensionOk); //Variables.Parser.frmCampos
                }


                if (!Variables.App.UseXML)
                {
                    // Creación automática de tablas
                    // Falla ExisteTabla cuando la tablas esta recien creada y el schema no esta actualizado
                    BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                    if (Variables.Parser.data.Count > Variables.Parser.frmDataPos && !String.IsNullOrEmpty(Variables.Parser.data[Variables.Parser.frmDataPos].tabla))
                    {
                        if (!db.TableExists(Variables.Parser.data[Variables.Parser.frmDataPos].tabla))
                        {
                            db.ExecuteNonQuery(db.CreateTableSql(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, Request, Variables.Parser.frmCampos));
                        }
                    }
                }

                if (Variables.Parser.frmCaptcha)
                {
					string EncodedResponse = Request.Form["g-Recaptcha-Response"];

                    if (!Captcha.Validate(EncodedResponse))
                    {
                        throw new ExceptionUtil("Captcha incorrecto. Intentelo de nuevo.",
                            ExceptionUtil.ExceptionType.Information);
                    }
                }

                Variables.FormMod m = Variables.Parser.frmModo;
                string mo = Functions.Valor(Request.QueryString["modo"]);
                if (mo != "")
                {
                    //Variables.Parser.frmEmailTo = "";
                    switch (mo)
                    {
                        case "login":
                            m = Variables.FormMod.Login;
                            break;
                        case "adduser":
                            m = Variables.FormMod.AddUser;
                            break;
                        case "edituser":
                            m = Variables.FormMod.EditUser;
                            break;
                        case "addrecord":
                            m = Variables.FormMod.AddRecord;
                            break;
                        case "editrecord":
                            m = Variables.FormMod.EditRecord;
                            break;
                        case "recordar":
                            m = Variables.FormMod.RecordarPassword;
                            break;
                        case "form":
                            m = Variables.FormMod.Form;
                            break;
                        case "email":
                            m = Variables.FormMod.Email;
                            break;
                    }
                }

                switch (m)
                {
                    case Variables.FormMod.AddRecord:
                        if (Variables.App.UseXML)
                        {
                            throw new Exception("Implementar la posibilidad de añadir en XML");
                        }
                        else
                        {
                            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                            db.ExecuteNonQuery(db.InsertSql(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, Request, Variables.Parser.frmCampos, Variables.User.UsuarioId));
                            Variables.Parser.frmIdentity = NumberUtils.NumberLong(db.GetIdentity());
                        }
                        break;
                    case Variables.FormMod.EditRecord:
                        if (Variables.App.UseXML)
                        {
                            throw new Exception("Implementar la posibilidad de editar en XML");
                        }
                        else
                        {
                            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                            db.ExecuteNonQuery(db.UpdateSql(Variables.Parser.data[Variables.Parser.frmDataPos].tabla, Request, Variables.Parser.frmCampos, Variables.Parser.data[Variables.Parser.frmDataPos].seleccion, Variables.User.UsuarioId));
                        }
                        break;
                    case Variables.FormMod.Login:
                        portal.Login(Web.Request("txtUsuario"), Web.Request("txtClave"));
                        msg = "";

                        //if (ret == "")
                        //{
                        //    if (Variables.User.paginaInicio != 0)
                        //    {
                        //        Response.Redirect(Variables.App.directorioPortal + "page" + Variables.User.paginaInicio + ".aspx");
                        //    }
                        //    else
                        //    {
                        //        if (Functions.ValorRequest("comebackto") != "")
                        //        {
                        //            Response.Redirect(Functions.HtmlDecode(Functions.ValorRequest("comebackto")));
                        //        }
                        //        else
                        //        {
                        //            Response.Redirect(Variables.App.directorioPortal + "default.aspx");
                        //        }
                        //    }
                        //}
                        Variables.Parser.frmEmailTo = "";
                        break;
                    case Variables.FormMod.AddUser:
                        portal.AddUser(Web.Request("txtUsuario"), Web.Request("txtEmail"),
                            Web.Request("txtNombre"), Web.Request("txtApellido1"),
                            Web.Request("txtApellido2"), Web.RequestInt("txtMes"),
                            Web.RequestInt(
                                "txtDia"), Web.RequestInt("txtAño"),
                            Web.RequestBool("txtNotifications"), Web.RequestBool("txtNewsLetter"),
                            Web.RequestInt("pais"),
                            Web.Request("txtClave"), Web.
                                RequestInt("provincia"), Web.RequestInt("sexo"),
                            Web.RequestInt("txtEdad"));
                        break;
                    case Variables.FormMod.EditUser:
                        portal.EditUser(Variables.User.UsuarioId, Web.Request("txtUsuario"),
                            Web.Request("txtClave"), Web.Request("txtClaveRe"),
                            Web.Request("txtEmail"), Web.Request("txtNombre"),
                            Web.Request("txtApellido1"), Web.Request("txtApellido2"),
                            Web.Request("txtDireccion"), Web.Request("txtPortal"),
                            Web.Request("txtPiso"), Web.Request("txtPoblacion"),
                            Web.Request("txtCodigoPostal"), Web.Request("txtDNI"),
                            Web.Request("txtTelefono1"), Web.Request("txtTelefono2"),
                            Web.Request("txtPaginaPrincipal"), Web.Request("txtAIM"),
                            Web.Request("txtYahoo"), Web.Request("txtMSN"), Web.Request("txtICQ"),
                            Web.Request("txtOcupacion"), Web.Request("txtInteres"),
                            Web.RequestBool("txtNotificarRespMensajes"),
                            Web.RequestBool("txtMostrarEmail"), Web.RequestInt("txtMes"),
                            Web.RequestInt(
                                "txtDia"), Web.RequestInt("txtAño"),
                            Web.RequestBool("txtNotifications"), Web.RequestBool("txtNewsLetter"),
                            Web.RequestInt("pais"), Web.
                                RequestInt("provincia"), Web.RequestInt("sexo"),
                            Web.RequestBool("txtNotificarCorreo"), Web.RequestBool("txtNoRecibirCorreo"),
                            Web.Request("txtEdad"));
                        break;
                    case Variables.FormMod.RecordarPassword:
                        portal.RecordarPassword(Web.Request("txtEmail"));
                        break;
                    case Variables.FormMod.Email:
                        Variables.Parser.frmEmailTo = Variables.App.correoInfo;
                        break;
                    case Variables.FormMod.Form:
                        int id = Web.RequestInt("id");

                        if (id != 0)
                        {
                            DataTable dtFormulario;
                            DataTable dtFields;
                            if (Variables.App.UseXML)
                            {
                                XML xml = new XML(Variables.App.directorioWeb + "data");
                                xml.Load("formularios.xml");
                                dtFormulario = xml.Select("activo = true and idFormulario=" + id);

                                xml.Load("formulariocampos.xml");
                                dtFields = xml.Select("idFormulario=" + id);
                            }
                            else
                            {
                                BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
                                dtFormulario = db.Execute("SELECT mensajeEnviar,nombre,textoenviar FROM " + Variables.App.prefijoTablas +
                                           "Formularios WHERE activo = true and idFormulario=" + id);
                                dtFields = db.Execute("SELECT descripcion,nombre FROM " + Variables.App.prefijoTablas +
                                               "FormularioCampos WHERE idFormulario=" + id);
                            }

                            string frmName = "";
                            if (dtFormulario != null && dtFormulario.Rows.Count > 0)
                                frmName = dtFormulario.Rows[0]["nombre"].ToString();

                            string sSubject = "Formulario: " + frmName;
                            string sBody = "Contenido del formulario:" + "\r\n" + "\r\n";

                            if (dtFields != null)
                            {
                                foreach (DataRow row in dtFields.Rows)
                                {
                                    sBody = sBody + row["descripcion"] + ": " +
                                            Web.Request(Functions.Valor(row["nombre"])) + "\r\n";
                                }
                            }

						new SendMail().SendMailAsync(Variables.App.correoInfo, Variables.App.correoPrueba, Variables.App.correoCopia, sSubject, sBody, Variables.App.correoInfo, Variables.App.nombreWeb, Variables.App.plantillaCorreo);

                            if (Functions.Valor(dtFormulario.Rows[0]["mensajeEnviar"]) != "")
                            {
                                msg = Functions.Valor(dtFormulario.Rows[0]["mensajeEnviar"]);
                            }
                        }

                        break;
                }


                if (Variables.Parser.frmEmailTo != "")
                {
                    string msgEmail = "Formulario enviado por internet: " + Variables.App.nombreWeb;
                    if (Variables.Parser.frmEmailSubject == "") 
                        Variables.Parser.frmEmailSubject = msgEmail;

                    string sBody = "";
                    sBody += "Formulario enviado por internet: " + Variables.User.NombreCompleto + Ui.Lf() + Ui.Lf();
                    sBody += Ui.FormToHtml(Request);

                    new SendMail().SendMailAsync(Variables.Parser.frmEmailTo, Variables.App.correoPrueba,Variables.App.correoCopia, Variables.Parser.frmEmailSubject, sBody,
						Variables.App.correoInfo, Variables.App.nombreWeb, Variables.App.plantillaCorreo);

                    Variables.Parser.frmEmailTo = "";
                }


                //s.Append(ret);

                //if (ret != "")
                //{
                //s.Append(Variables.Parser.frmMensajeNoOK + " - ");
                //s.Append(UI.Lf());
                //s.Append(ret);
                //s.Append("<br /><br />");
                //s.Append("<br /><br /><a href='javascript:history.back()'>Volver.</a>");
                //}
                //else
                //{
                //    s.Append(Variables.Parser.frmMensajeOK);
                //s.Append("<br /><br />");
                //s.Append("<br /><br /><a href='" + Variables.Parser.frmPagina + "'>Volver.</a>");
                //}

                //string redi = Variables.Parser.frmRedirige;
                //if (ret == "")
                //{
                //    if (redi != "")
                //    {
                //        Variables.Parser.frmRedirige = "";
                //        Server.Transfer(redi, true);
                //    }
                //} 

                //db.CommitTransaction();
                return @"{ ""message"": """ + Web.formatJSON(msg) + @""" }";
            }
            catch (System.Exception e)
            {
                //funcMail.SendMail(Variables.App.correoInfo, "Error al procesar formulario.", e.ToString() + "\r\n------- FORM --------\r\n" + funcMail.FormEmail(Request), Variables.App.correoInfo, Variables.App.correoInfo);
                return @"{ ""message"": """ + Variables.Parser.frmMensajeNoOK + Ui.Lf() + Web.formatJSON(e.Message) + @""" }";
                
                //db.RollbackTransaction();
            }
        }
    }
}