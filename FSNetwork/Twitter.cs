// // <fileheader>
// // <copyright file="Twitter.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Collections.Generic;
using System.Xml;
using FSLibrary;
using FSException;

#endregion

namespace FSNetwork
{
    public class Twitter
    {
        public static List<TwitterMsg> Mensajes(string usuario, int numeroMensajes, bool mostrarRT)
        {
            XmlDocument doc = new XmlDocument();

            List<TwitterMsg> mensajes = new List<TwitterMsg>();
            //string url =
            //    String.Format(
            //        "http://api.twitter.com/1/statuses/user_timeline.xml?include_rts={2}&screen_name={0}&count={1}",
            //        usuario, numeroMensajes, mostrarRT.ToString().ToLower());

            string url =
                    String.Format(
                        "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&count={1}",
                        usuario, numeroMensajes);

            try
            {
                doc.LoadXml(Http.GetFromUrl(url));

                XmlNodeList nodeList = doc.GetElementsByTagName("status");

                foreach (XmlNode node in nodeList)
                {
                    XmlElement XMLElement = (XmlElement) node;

                    TwitterMsg msg = new TwitterMsg();
                    msg.Mensaje = XMLElement.GetElementsByTagName("text")[0].InnerText;
                    msg.Mensaje = Web.ConvertToTwitter(msg.Mensaje);
                    msg.FechaPublicacion = ParseDateTime(XMLElement.GetElementsByTagName("created_at")[0].InnerText);

                    XmlNodeList XMLUser = XMLElement.GetElementsByTagName("user");
                    msg.Imagen = ((XmlElement) XMLUser[0]).GetElementsByTagName("profile_image_url")[0].InnerText;
                    msg.TotalAmigos =
                        Convert.ToInt32(((XmlElement) XMLUser[0]).GetElementsByTagName("friends_count")[0].InnerText);
                    msg.TotalMensajes =
                        Convert.ToInt32(((XmlElement) XMLUser[0]).GetElementsByTagName("statuses_count")[0].InnerText);
                    msg.TotalSeguidores =
                        Convert.ToInt32(((XmlElement) XMLUser[0]).GetElementsByTagName("followers_count")[0].InnerText);

                    mensajes.Add(msg);
                }
            }
            catch (System.Exception e)
            {
                throw new ExceptionUtil("Hubo un error al obtener los tweets.", e);
            }

            return mensajes;
        }

        private static System.DateTime ParseDateTime(string date)
        {
            //string dayOfWeek = date.Substring(0, 3).Trim();
            string month = date.Substring(4, 3).Trim();
            string dayInMonth = date.Substring(8, 2).Trim();
            string time = date.Substring(11, 9).Trim();
            //string offset = date.Substring(20, 5).Trim();
            string year = date.Substring(25, 5).Trim();
            string dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
			System.DateTime ret = System.DateTime.Parse(dateTime);
            return ret;
        }

        public class TwitterMsg
        {
        	public string Mensaje;
            public System.DateTime FechaPublicacion;
            public string Imagen;
            public int TotalSeguidores;
            public int TotalMensajes;
            public int TotalAmigos;
        }
    }
}