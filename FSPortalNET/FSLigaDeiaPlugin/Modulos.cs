// <fileheader>
// <copyright file="Modulos.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: Modulos.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>


using System;
using System.Data;
using FSPortal;
using FSLibrary;
using FSDatabase;

namespace FSLigaDeiaPlugin
{
    /// <summary>
    ///     Clase para mostrar los módulos del portal
    /// </summary>
    public class Modulos
    {
        public static string ModBannerHorizontal()
        {
            return ModBanner(true);
        }

        public static string ModBannerVertical()
        {
            return ModBanner(false);
        }

        public static string ModBanner(bool horizontal)
        {
            string ssql = "Select idBanner,imagen,mensaje,nuevaVentana,link from " + Variables.App.prefijoTablas +
                          "Banners where activo=true and horizontal=" + horizontal +
                          " and (vecesMostrado < vecesQueMostrar) and (vecesPulsado < vecesQuePulsar)";
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            DataTable dt;
            DataTable bann = null; //(DataTable)Functions.GetValorCache("banner" + horizontal.ToString());
            if (bann != null)
                dt = null; //(DataTable)Functions.GetValorCache("banner" + horizontal.ToString());
            else
            {
                dt = db.Execute(ssql);
                //Functions.SetValorCache("banner" + horizontal.ToString(), dt);
            }


            DataRow dr = null;
            string s = "";
            string frame = "";

            if (dt.Rows.Count > 0)
            {
                Random r = new Random();
                dr = dt.Rows[r.Next(dt.Rows.Count)];

                if (!(Variables.User.Administrador))
                {
                    db.ExecuteNonQuery("update " + Variables.App.prefijoTablas +
                                       "banners set vecesMostrado=vecesMostrado+1 where idBanner=" + dr["idBanner"]);
                }

                string ImageString = @"<img border=""0"" src=""" + dr["Imagen"] + @""" alt=""" + dr["mensaje"] + @"""/>";

                if (Functions.ValorBool(dr["nuevaVentana"]))
                {
                    frame = @"target=""_blank"" ";
                }

                s = @"<a " + frame + @"href=""" + Variables.App.directorioPortal + "banners/Redirect.aspx?";
                s = s + "bannerID=" + dr["idBanner"] + @""">" + ImageString + "</a>";
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