using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModCorreo : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Correo();
        }

        public string Name
        {
            get { return "ModCorreo"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Correo()
        {
            string modCorreoReturn = @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                     "imagenes/bullet.gif' alt='' /> Tienes <strong><a href='" + Variables.App.directorioPortal +
                                     "correo/default.aspx'>" + FuncionesWeb.TotalMensajes() +
                                     "</a></strong> mensajes nuevos en tu buzón de correo." + "\r\n";
            modCorreoReturn = modCorreoReturn + Ui.Lf() + "\r\n";
            modCorreoReturn = modCorreoReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /><a href='" + Variables.App.directorioPortal +
                              "correo/default.aspx'>Leer correo.</a>" + "\r\n";
            modCorreoReturn = modCorreoReturn + Ui.Lf() + "\r\n";
            modCorreoReturn = modCorreoReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                              "imagenes/bullet.gif' alt='' /><a href='" + Variables.App.directorioPortal +
                              "correo/sendmessage.aspx'>Enviar correo.</a>" + "\r\n";
            if (Variables.User.Administrador)
            {
                modCorreoReturn = modCorreoReturn + @"<img border=""0"" src='" + Variables.App.directorioPortal +
                                  "imagenes/bullet.gif' alt='' /><a href='" + Variables.App.directorioPortal +
                                  "correo/comunicaciones.aspx'>Comunicaciones</a>" + "\r\n";
                modCorreoReturn = modCorreoReturn + Ui.Lf() + "\r\n";
            }

            return modCorreoReturn;
        }
    }
}