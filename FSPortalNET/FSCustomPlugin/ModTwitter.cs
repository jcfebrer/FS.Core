using System;
using FSPlugin;
using System.Collections.Generic;
using FSPortal;
using FSNetwork;

namespace FSCustomPlugin
{
    public class ModTwitter : IPlugin
    {
        public string Execute(params string[] p)
        {
            if (p.Length != Parameters + 1) return "Parametros incorrectos. Se necesitan [" + Parameters + "].";
            return Twitter((string) p[1], Convert.ToInt32(p[2]));
        }

        public string Name
        {
            get { return "ModTwitter"; }
        }

        public int Parameters
        {
            get { return 2; }
        }


        public static string Twitter(string user, int count)
        {
            try
            {
                List<Twitter.TwitterMsg> twitterMsgs = FSNetwork.Twitter.Mensajes(user, count, true);

                string modTwitterReturn = @"<div style=""position:inherit; z-index:1; overflow-x:hidden; overflow-y:scroll; top:0px; left:0px; height:300px; width:100%; padding-left:3px; padding-right:3px;"">" +
                                          "\r\n";
                modTwitterReturn = modTwitterReturn +
                                   @"<table cellpadding=""2"" cellspacing=""0"" width=""100%"" border=""0"">";

                foreach (Twitter.TwitterMsg twitterMsg in twitterMsgs)
                {
                    modTwitterReturn = modTwitterReturn + @"<tr><td>";
                    modTwitterReturn = modTwitterReturn + "<img src='" + twitterMsg.Imagen + "' /></td><td>";
                    modTwitterReturn = modTwitterReturn + twitterMsg.Mensaje + Ui.Lf() + "<i><small>" +
                                       FSLibrary.DateTimeUtil.LongDate(twitterMsg.FechaPublicacion) + " " +
                                       twitterMsg.FechaPublicacion.ToLongTimeString() + "</small></i></td></tr>" +
                                       "\r\n";
                }

                modTwitterReturn = modTwitterReturn + "</table></div>" + "\r\n";
                return modTwitterReturn;
            }
            catch (System.Exception e)
            {
                return "Imposible cargar modulo Twitter. " + e.Message;
            }
        }
    }
}