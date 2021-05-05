using FSPlugin;
using FSLibrary;
using System;
using System.Data;
using FSPortal;
using FSDatabase;

namespace FSCustomPlugin
{
    public class ModBanner : IPlugin
    {
        public string Execute(params string[] p)
        {
            if (p.Length != Parameters + 1) return "Parametros incorrectos. Se necesitan [" + Parameters + "].";
            return Banner(Functions.ValorBool(p[1]));
        }

        public string Name
        {
            get { return "ModBanner"; }
        }

        public int Parameters
        {
            get { return 1; }
        }


        public static string Banner(bool horizontal)
        {
            string ssql = "Select idBanner,imagen,mensaje,nuevaVentana,link from " + Variables.App.prefijoTablas +
                          "Banners where activo=true and horizontal=" + horizontal +
                          " and (vecesMostrado < vecesQueMostrar) and (vecesPulsado < vecesQuePulsar)";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt = db.Execute(ssql);

            string s = "";
            string frame = "";

            if (dt.Rows.Count > 0)
            {
                Random r = new Random();
                DataRow dr = dt.Rows[r.Next(dt.Rows.Count)];

                if (!(Variables.User.Administrador))
                {
                    db.ExecuteNonQuery("update " + Variables.App.prefijoTablas +
                                       "banners set vecesMostrado=vecesMostrado+1 where idBanner=" + dr["idBanner"]);
                }

                string imageString = @"<img border=""0"" src=""" + dr["Imagen"] + @""" alt=""" + dr["mensaje"] + @"""/>";

                if (Functions.ValorBool(dr["nuevaVentana"]))
                {
                    frame = @"target=""_blank"" ";
                }

                s = @"<a " + frame + @"href=""" + Variables.App.directorioPortal + "banners/Redirect.aspx?";
                s = s + "bannerID=" + dr["idBanner"] + @""">" + imageString + "</a>";
                s = Ui.Center(s);
            }
            else
            {
                s = s + "No hay banners.";
            }

            return s;
        }
    }
}