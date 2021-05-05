using System;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.IO;

namespace WiworBrowser.Controls
{
    class DBImageProcess
    {
        //for now all methods are static
        public DBImageProcess()
        {
        }

        ///<summary>
        ///this method creates a black and white copy of the image
        ///</summary>
        public void DetectEdges(Bitmap original, ref Bitmap modified, Color c, int noise)
        {
            original = ConvertIndexedImage(original);

            Graphics g = Graphics.FromImage(original);
            for (Int32 x = 0; x < original.Width; x++)
            {
                for (Int32 y = 0; y < original.Height; y++)
                {
                    if (x > 2 && y > 2)
                    {
                        try
                        {
                            Color current = modified.GetPixel(x, y);
                            Color right = modified.GetPixel(x + 1, y);
                            Color left = modified.GetPixel(x - 1, y);

                            int totalCurrent = current.R + current.G + current.B;
                            int totalRight = right.R + right.G + right.B;
                            int totalLeft = left.R + left.G + left.B;

                            if (totalCurrent > (totalLeft + noise) || totalCurrent > (totalRight + noise))
                            {
                                modified.SetPixel(x - 1, y, Color.White);//white
                            }
                            else if (totalCurrent > (totalRight + noise))
                            {
                                modified.SetPixel(x + 1, y, Color.White);//white
                            }
                            else
                                modified.SetPixel(x, y, Color.Black);//black

                            Color upOne = modified.GetPixel(x, y - 1);
                            Color downOne = modified.GetPixel(x, y + 1);

                            int totalUpOne = upOne.R + upOne.G + upOne.B;
                            int totalDownOne = downOne.R + downOne.G + downOne.B;

                            if (totalUpOne > (totalCurrent + noise))
                            {
                                modified.SetPixel(x, y - 1, Color.White);
                            }
                            else if (totalDownOne > (totalCurrent + noise))
                            {
                                modified.SetPixel(x, y + 1, Color.White);
                            }
                            else
                                modified.SetPixel(x, y, Color.Black);




                            Color upLeft = modified.GetPixel(x - 1, y - 1);
                            Color downRight = modified.GetPixel(x + 1, y + 1);

                            int totalUpLeft = upLeft.R + upLeft.G + upLeft.B;
                            int totalDownRight = downRight.R + downRight.G + downRight.B;
                            if (totalUpLeft > (totalCurrent + noise))
                            {
                                modified.SetPixel(x - 1, y - 1, Color.White);
                            }
                            else if (totalDownRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x + 1, y + 1, Color.White);
                            }
                            else
                                modified.SetPixel(x, y, Color.Black);

                            Color upRight = modified.GetPixel(x + 1, y - 1);
                            Color downLeft = modified.GetPixel(x - 1, y + 1);

                            int totalupRight = upRight.R + upRight.G + upRight.B;
                            int totalDownLeft = downLeft.R + downLeft.G + downLeft.B;
                            if (totalupRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x + 1, y - 1, Color.White);
                            }
                            else if (totalDownRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x - 1, y + 1, Color.White);
                            }
                            else
                                modified.SetPixel(x, y, Color.Black);
                        }
                        catch (System.ArgumentException)
                        {

                        }
                    }
                }
            }
        }

        ///<summary>
        ///outlines the edges of an image
        ///</summary>
        public void OutLineEdges(Bitmap original, ref Bitmap modified, Color c, int noise)
        {
            original = ConvertIndexedImage(original);

            Graphics g = Graphics.FromImage(original);
            for (Int32 x = 0; x < original.Width; x++)
            {
                for (Int32 y = 0; y < original.Height; y++)
                {
                    if (x > 2 && y > 2)
                    {
                        try
                        {
                            Color current = modified.GetPixel(x, y);
                            Color right = modified.GetPixel(x + 1, y);
                            Color left = modified.GetPixel(x - 1, y);

                            int totalCurrent = current.R + current.G + current.B;
                            int totalRight = right.R + right.G + right.B;
                            int totalLeft = left.R + left.G + left.B;

                            if (totalCurrent > (totalLeft + noise) || totalCurrent > (totalRight + noise))
                            {
                                modified.SetPixel(x - 1, y, c);
                            }
                            else if (totalCurrent > (totalRight + noise))
                            {
                                modified.SetPixel(x + 1, y, c);
                            }

                            Color upOne = modified.GetPixel(x, y - 1);
                            Color downOne = modified.GetPixel(x, y + 1);

                            int totalUpOne = upOne.R + upOne.G + upOne.B;
                            int totalDownOne = downOne.R + downOne.G + downOne.B;

                            if (totalUpOne > (totalCurrent + noise)) //|| totalDownOne > (totalCurrent + 75))
                            {
                                modified.SetPixel(x, y - 1, c);
                            }
                            else if (totalDownOne > (totalCurrent + noise))
                            {
                                modified.SetPixel(x, y + 1, c);
                            }

                            Color upLeft = modified.GetPixel(x - 1, y - 1);
                            Color downRight = modified.GetPixel(x + 1, y + 1);

                            int totalUpLeft = upLeft.R + upLeft.G + upLeft.B;
                            int totalDownRight = downRight.R + downRight.G + downRight.B;
                            if (totalUpLeft > (totalCurrent + noise))
                            {
                                modified.SetPixel(x - 1, y - 1, c);
                            }
                            else if (totalDownRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x + 1, y + 1, c);
                            }

                            Color upRight = modified.GetPixel(x + 1, y - 1);
                            Color downLeft = modified.GetPixel(x - 1, y + 1);

                            int totalupRight = upRight.R + upRight.G + upRight.B;
                            int totalDownLeft = downLeft.R + downLeft.G + downLeft.B;
                            if (totalupRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x + 1, y - 1, c);
                            }
                            else if (totalDownRight > (totalCurrent + noise))
                            {
                                modified.SetPixel(x - 1, y + 1, c);
                            }

                        }
                        catch (System.ArgumentException)
                        {

                        }
                    }
                }
            }
        }


        /// <summary>
        /// Devuelve el porcentaje de "carne" que hay en una imagen
        /// </summary>
        /// <param name="original"></param>
        /// <param name="modified"></param>
        public int DetectSkinPercent(Bitmap image)
        {
            image = ConvertIndexedImage(image);

            Graphics g = Graphics.FromImage(image);
            ArrayList points = new ArrayList();

            long totalSize = image.Width * image.Height;
            long totalSkin = 0;

            for (Int32 x = 0; x < image.Width; x++)
            {
                for (Int32 y = 0; y < image.Height; y++)
                {

                    Color c = image.GetPixel(x, y);


                    /* convert RGB color space to IRgBy color space using this formula:
                    I= [L(R) + L(B) + L(G)] / 3
                    Rg = L(R) - L(G)
                    By = L(B) - [L(G) +L(R)] / 2
					
                    to calculate the hue:
                    hue = atan2(Rg,By) * (180 / 3.141592654f)
                    */
                    double I = (Math.Log(c.R) + Math.Log(c.B) + Math.Log(c.G)) / 3;
                    double Rg = Math.Log(c.R) - Math.Log(c.G);
                    double By = Math.Log(c.B) - (Math.Log(c.G) + Math.Log(c.R)) / 2;
                    double hue = Math.Atan2(Rg, By) * (180 / Math.PI);


                    //si es "piel"
                    if (I <= 5 && (hue >= 4 && hue <= 255))
                    {
                        //r = 255;
                        //points.Add(new Point(x, y));
                        totalSkin++;
                    }
                }
            }

            int totalPercent = (int)((totalSkin*100)/totalSize);

            return totalPercent;
        }

        /// <summary>
        /// Convierte una imagen "Indexed" a no indexada
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private Bitmap ConvertIndexedImage(Bitmap image)
        {
            if (image.PixelFormat == PixelFormat.Format8bppIndexed || image.PixelFormat == PixelFormat.Format4bppIndexed
                || image.PixelFormat == PixelFormat.Indexed || image.PixelFormat == PixelFormat.Format1bppIndexed)
            {
                Bitmap tmp = new Bitmap(image.Width, image.Height);
                Graphics grPhoto = Graphics.FromImage(tmp);
                grPhoto.DrawImage(image, new Rectangle(0, 0, tmp.Width, tmp.Height), 0, 0, tmp.Width, tmp.Height, GraphicsUnit.Pixel);
                grPhoto.Dispose();

                image = tmp;
            }
            return image;
        }


        ///<summary>
        ///detects skin. loops through the pixels in the image, if the pixel is skin then it leaves that pixel alone,
        ///or else it will color that pixel black.
        ///</summary>
        public void DetectSkin(Bitmap original, ref Bitmap modified)
        {
            original = ConvertIndexedImage(original);

            Graphics g = Graphics.FromImage(original);
            ArrayList points = new ArrayList();
            for (Int32 x = 0; x < original.Width; x++)
            {
                for (Int32 y = 0; y < original.Height; y++)
                {


                    Color c = modified.GetPixel(x, y);


                    /* convert RGB color space to IRgBy color space using this formula:
                    I= [L(R) + L(B) + L(G)] / 3
                    Rg = L(R) - L(G)
                    By = L(B) - [L(G) +L(R)] / 2
					
                    to calculate the hue:
                    hue = atan2(Rg,By) * (180 / 3.141592654f)
                    */
                    double I = (Math.Log(c.R) + Math.Log(c.B) + Math.Log(c.G)) / 3;
                    double Rg = Math.Log(c.R) - Math.Log(c.G);
                    double By = Math.Log(c.B) - (Math.Log(c.G) + Math.Log(c.R)) / 2;
                    double hue = Math.Atan2(Rg, By) * (180 / Math.PI);



                    if (I <= 5 && (hue >= 4 && hue <= 255))
                    {
                        //r = 255;
                        points.Add(new Point(x, y));
                    }
                    else
                    {
                        modified.SetPixel(x, y, Color.Black);
                    }


                }
            }
            //SortPoints(ref points);
            //PlotLines(Graphics.FromImage(modified), (Point)points[0], (Point)points[points.Count - 1]);
        }

        private void PlotLines(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(Pens.White, p1, p2);
        }

        private void SortPoints(ref ArrayList pts)
        {
            //int x = 4;
            for (int i = 1; i < pts.Count; i++)
            {
                Point thisPoint = (Point)pts[i];
                Point lastPoint = (Point)pts[i - 1];
                if (thisPoint.X < lastPoint.X && thisPoint.Y < lastPoint.Y)
                {
                    //thisPoint is closer to 0,0
                }
                else
                {
                    //lastPoint is closer to 0,0
                    //swap thisPoint and lastPoint
                    swap(ref pts, i - 1, i);
                }
            }
            //--x;
            //if(!(x == 0)) SortPoints(ref pts);
        }

        private void swap(ref ArrayList pts, int a, int b)
        {
            Point temp;
            Point pA = (Point)pts[a];
            Point pB = (Point)pts[b];

            temp = pA;
            pA = pB;
            pB = temp;
        }

        private int max(int r, int g, int b)
        {
            if (r > g && r > b)
                return r;
            else if (g > r && g > b)
                return g;
            else
                return b;
        }

        private static int min(int r, int g, int b)
        {
            if (r < g && r < b)
                return r;
            else if (g < r && g < b)
                return g;
            else
                return b;
        }

        public string MakeImageSrcData(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            return "data:image/png;base64," +
              Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
        }

        public Image ConvertBase64ToImage(string base64String)
        {
            if (base64String.StartsWith("data:"))
            {
                base64String = base64String.Replace("data:image/jpg;base64,", "");
                base64String = base64String.Replace("data:image/png;base64,", "");
                base64String = base64String.Replace("data:image/gif;base64,", "");
                base64String = base64String.Replace("data:image/ico;base64,", "");
                base64String = base64String.Replace("data:image/bmp;base64,", "");
                base64String = base64String.Replace("data:image/jpeg;base64,", "");
            }

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
            imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public string ConvertImageToBase64(Image img, ImageFormat imageFormat)
        {
            using (MemoryStream objMemoryStream = new MemoryStream())
            {
                img.Save(objMemoryStream, imageFormat);

                byte[] objImageBytes = objMemoryStream.ToArray();
                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(objImageBytes);
                return base64String;
            }
        }

    }
}