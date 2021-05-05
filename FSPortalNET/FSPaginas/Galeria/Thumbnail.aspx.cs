// <fileheader>
// <copyright file="thumbnail.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: galeria\thumbnail.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.UI;
using FSPortal;
using FSLibrary;
using FSException;

namespace FSPaginas.Galeria
{
    public class Thumbnail : Page
    {
        public Hashtable imageOutputFormatsTable = new Hashtable();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool forceaspect = true;
            int reqHeight = 100;
            int reqWidth = 100;

            try
            {
                string imageLocation = Server.MapPath(Request.QueryString["Image"]);

                //Si hay corchetes, no generamos la imagen
                if (imageLocation.IndexOf("{") > 0 && imageLocation.IndexOf("}") > 0)
                {
                    return;
                }

                Response.Cache.VaryByParams["Image;Width;Height;ForceAspect"] = true;
                Response.ContentType = "image/jpeg";

                imageOutputFormatsTable.Add(ImageFormat.Gif.Guid, ImageFormat.Gif);
                imageOutputFormatsTable.Add(ImageFormat.Jpeg.Guid, ImageFormat.Jpeg);
                imageOutputFormatsTable.Add(ImageFormat.Bmp.Guid, ImageFormat.Gif);
                imageOutputFormatsTable.Add(ImageFormat.Tiff.Guid, ImageFormat.Jpeg);
                imageOutputFormatsTable.Add(ImageFormat.Png.Guid, ImageFormat.Jpeg);

                if (Request.QueryString["Height"] != null)
                {
                    reqHeight = Convert.ToInt32(Request.QueryString["Height"]);
                }
                if (Request.QueryString["ForceAspect"] != null)
                {
                    forceaspect = Functions.ValorBool(Request.QueryString["ForceAspect"]);
                }
                if (Request.QueryString["Width"] != null)
                {
                    reqWidth = Convert.ToInt32(Request.QueryString["Width"]);
                }
                if (Request.QueryString["ForceAspect"] == "true")
                {
                    forceaspect = true;
                }

                Bitmap origBitmap = new Bitmap(imageLocation);

                int newHeight;
                int newWidth;
                if ((forceaspect))
                {
                    newHeight = reqHeight;
                    newWidth = reqWidth;
                }
                else if ((origBitmap.Height >= origBitmap.Width))
                {
                    newHeight = reqHeight;
                    newWidth =
                        NumberUtils.NumberInt((Convert.ToDouble(origBitmap.Width)/Convert.ToDouble(origBitmap.Height))*
                                          reqHeight);
                }
                else
                {
                    newWidth = reqWidth;
                    newHeight =
                        NumberUtils.NumberInt((Convert.ToDouble(origBitmap.Height)/Convert.ToDouble(origBitmap.Width))*
                                          reqWidth);
                }

                Resize(imageLocation, newWidth, newHeight);
            }
            catch //(Exception ex)
            {
                Resize(Server.MapPath("imagenes/thumberror.gif"), reqWidth, reqHeight);
            }
        }


        public void Resize(string image, int w, int h)
        {
            try
            {
                Bitmap origBitmap = new Bitmap(image);
                Bitmap outputImage = new Bitmap(w, h);


                outputImage.SetResolution(72, 72);


                ImageFormat outputFormat = ((ImageFormat) (imageOutputFormatsTable[origBitmap.RawFormat.Guid]));

                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(outputImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.FillRectangle(Brushes.White, 0, 0, w, h);
                    g.DrawImage(origBitmap, 0, 0, w, h);
                }

                outputImage.Save(Response.OutputStream, outputFormat);
                outputImage.Dispose();
                origBitmap.Dispose();
            }
            catch (System.Exception e)
            {
				throw new ExceptionUtil(e);
            }
        }

    }

}
