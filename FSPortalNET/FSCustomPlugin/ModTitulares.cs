using FSPlugin;
using FSPortal;
using System.Text;

namespace FSCustomPlugin
{
    public class ModTitulares : IPlugin
    {
        public string Execute(params string[] p)
        {
            if (p.Length != Parameters + 1) return "Parametros incorrectos. Se necesitan [" + Parameters + "].";
            return Titulares(p[1]);
        }

        public string Name
        {
            get { return "ModTitulares"; }
        }

        public int Parameters
        {
            get { return 1; }
        }


        public static string Titulares(string rss)
        {
            try
            {
                FSLibrary.Rss rssf = new FSLibrary.Rss();

                rssf.LoadFromHttp(rss, Encoding.UTF8);

                string modTitularesReturn = @"<div style=""position:inherit; z-index:1; overflow:auto; top:0px; left:0px; height:50; width:100%; padding-left:3px; padding-right:3px;"">" +
                                            "\r\n";

                int f;
                for (f = 0; f <= rssf.Items.Count - 1; f++)
                {
                    modTitularesReturn = modTitularesReturn + @"<img border=""0"" src=""" + Variables.App.directorioPortal +
                                         @"imagenes/bullet.gif"" alt="""" /> <a target=""_blank"" href=""" +
                                         rssf.Items[f].Link.AbsoluteUri + @""">" + rssf.Items[f].Title +
                                         @"&nbsp;<img border=""0"" src=""" + Variables.App.directorioPortal +
                                         @"imagenes/mas.gif"" alt=""Más Info.""/></a>" + Ui.Lf() + Ui.Lf();
                    if (rssf.Items[f].Description != "")
                    {
                        modTitularesReturn = modTitularesReturn + rssf.Items[f].Description + Ui.Lf() + Ui.Lf() + "\r\n";
                    }
                    if (FSLibrary.DateTimeUtil.LongDate(rssf.Items[f].pubDate) != "")
                    {
                        modTitularesReturn = modTitularesReturn + rssf.Items[f].pubDate.ToUniversalTime() + Ui.Lf() +
                                             Ui.Lf() + "\r\n";
                    }
                }

                modTitularesReturn = modTitularesReturn + "</div>" + "\r\n";
                return modTitularesReturn;
            }
            catch (System.Exception e)
            {
                return "Imposible cargar modulo titulares. " + e.Message;
            }
        }
    }
}