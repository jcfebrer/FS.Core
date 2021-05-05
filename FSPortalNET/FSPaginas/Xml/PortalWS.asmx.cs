using FSException;
using FSMail;
using FSPortal;
using System;
using System.Web.Services;

namespace FSPaginas.Xml
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://febrersoftware.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PortalWS : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public string Login(string username, string password)
        {
            try
            {
                FSPortal.Portal FSPortal = new FSPortal.Portal();
                //HttpContextManager.SetCurrentContext(Session);
                FSPortal.Login(username, password);
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string AddUser(string sUsuario, string sEMail, string sNombre, string sApellido1, string sApellido2, int sBirthMonth, int sBirthDay, int sBirthYear, bool sNotifications, bool sNewsletter, int sCountryCode, bool sRemember, bool sGenderMale, bool sGenderFemale)
        {
            try
            {
                FSPortal.Portal FSPortal = new FSPortal.Portal();
                //HttpContextManager.SetCurrentContext(Session);
                FSPortal.AddUser(sUsuario, sEMail, sNombre, sApellido1, sApellido2, sBirthMonth, sBirthDay, sBirthYear, sNotifications, sNewsletter, sCountryCode, sRemember);
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public string NombreCompleto()
        {
            return Variables.User.NombreCompleto;
        }

        [WebMethod(EnableSession = true)]
        public string UsuarioId()
        {
            return Variables.User.UsuarioId.ToString();
        }

        [WebMethod(EnableSession = true)]
        public string SendMail(string sTo, string sSubject, string sBody, string sFrom, string sFromName)
        {
            try
            {
                if (Variables.User.Administrador)
                {
                    FSMail.SendMail.SendMailMessage(sTo, "", "", sSubject, sBody, sFrom, sFromName, "");
                    return "OK";
                }
                else
                {
                    return "Error: No dispones de los permisos necesarios para enviar un correo.";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
