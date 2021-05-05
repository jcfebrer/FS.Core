using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace NudeDetectLib
{
    class SkinMap
    {
        public int id;
        public int skin;
        public int region;
        public int x;
        public int y;
        public bool _checked;
    }

    public class Nude
    {
        byte[][] mergeRegions;
        SkinMap[] skinMap;

        bool scanImage(byte[] imageData, int width, int height)
        {
            bool checker = false;
            byte?[] detectedRegions;
            byte?[] mergeRegions;

            int lastFrom = -1;
            int lastTo = -1;


            addMerge(0, 0);

            // iterate the image from the top left to the bottom right
            int length = imageData.Length;
            
            for (int i = 0, u = 1; i < length; i += 4, u++)
            {

                int r = imageData[i];
                int g = imageData[i + 1];
                int b = imageData[i + 2];
                int x = (u > width) ? ((u % width) - 1) : u;
                int y = (u > width) ? Convert.ToInt32(Math.Ceiling((decimal)(u / width)) - 1) : 1;

                if (classifySkin(r, g, b))
                { // 
                    //skinMap.push({"id": u, "skin": true, "region": 0, "x": x, "y": y, "checked": false});

                    int region = -1;
                    byte[] checkIndexes = new byte[] { (byte)(u - 2), (byte)((u - width) - 2), (byte)(u - width - 1), (byte)(u - width) };
                    checker = false;

                    for (int o = 0; o < 4; o++)
                    {
                        int index = checkIndexes[o];
                        if (skinMap[index]!=null && skinMap[index].skin!=null)
                        {
                            if (skinMap[index].region != region && region != -1 && lastFrom != region && lastTo != skinMap[index].region)
                            {
                                addMerge(region, skinMap[index].region);
                            }
                            region = skinMap[index].region;
                            checker = true;
                        }
                    }

                    if (!checker)
                    {
                        skinMap[u - 1].region = detectedRegions.Length;
                        detectedRegions.push(skinMap[u - 1]);
                        continue;
                    }
                    else
                    {

                        if (region > -1)
                        {

                            if (detectedRegions[region]!=null)
                            {
                                detectedRegions[region] = null;
                            }

                            skinMap[u - 1].region = region;
                            detectedRegions[region].push(skinMap[u - 1]);

                        }
                    }

                }
                else
                {
                    //skinMap.push({"id": u, "skin": false, "region": 0, "x": x, "y": y, "checked": false});
                }

            }

            merge(detectedRegions, mergeRegions);
            analyseRegions();
        }


        void addMerge(int from, int to)
        {
            int lastFrom = from;
            int lastTo = to;
            int len = mergeRegions.Length;
            int fromIndex = -1;
            int toIndex = -1;

            int rlen;

            while ((len--) != 0)
            {

                byte[] region = mergeRegions[len];
                rlen = region.Length;

                while ((rlen--) != 0)
                {

                    if (region[rlen] == from)
                    {
                        fromIndex = len;
                    }

                    if (region[rlen] == to)
                    {
                        toIndex = len;
                    }

                }

            }

            if (fromIndex != -1 && toIndex != -1 && fromIndex == toIndex)
            {
                return;
            }

            if (fromIndex == -1 && toIndex == -1)
            {
                //mergeRegions.push(new byte[2] { from, to });

                return;
            }
            if (fromIndex != -1 && toIndex == -1)
            {
                //mergeRegions[fromIndex].push(to);
                return;
            }
            if (fromIndex == -1 && toIndex != -1)
            {
                //mergeRegions[toIndex].push(from);
                return;
            }
            if (fromIndex != -1 && toIndex != -1 && fromIndex != toIndex)
            {
                //mergeRegions[fromIndex] = mergeRegions[fromIndex].concat(mergeRegions[toIndex]);
                //mergeRegions.remove(toIndex);
                return;
            }

        }


        // function for merging detected regions
        void merge(byte?[] detectedRegions, byte?[] mergeRegions)
        {

            int length = mergeRegions.Length;
            byte?[] detRegions = null;
            int rlen = 0;

            // merging detected regions 
            while ((length--) != 0)
            {

                byte?[] region = mergeRegions[length];
                rlen = region.Length;

                if (detRegions[length]!=null)
                    detRegions[length] = null;

                while ((rlen--) > 0)
                {
                    byte index = region[rlen];
                    detRegions[length] = detRegions[length].concat(detectedRegions[index]);
                    detectedRegions[index] = null;
                }

            }

            // push the rest of the regions to the detRegions array
            // (regions without merging)
            int l = detectedRegions.Length;
            while ((l--) > 0)
            {
                if (detectedRegions[l].Length > 0)
                {
                    detRegions.push(detectedRegions[l]);
                }
            }

            // clean up
            clearRegions(detRegions);

        }

        // clean up function
        // only pushes regions which are bigger than a specific amount to the final result
        void clearRegions(byte[] detectedRegions)
        {

            int length = detectedRegions.Length;

            for (int i = 0; i < length; i++)
            {
                if (detectedRegions[i].Length > 30)
                {
                    skinRegions.push(detectedRegions[i]);
                }
            }

        }

        void analyseRegions()
        {

            // sort the detected regions by size
            int length = skinRegions.length;
            int totalPixels = canvas.width * canvas.height;
            int totalSkin = 0;

            // if there are less than 3 regions
            if (length < 3)
            {
                postMessage(false);
                return;
            }

            // sort the skinRegions with bubble sort algorithm
            skinRegions = order(skinRegions);

            // count total skin pixels
            while (length--)
            {
                totalSkin += skinRegions[length].length;
            }

            // check if there are more than 15% skin pixel in the image
            if ((totalSkin / totalPixels) * 100 < 15)
            {
                // if the percentage lower than 15, it's not nude!
                //console.log("it's not nude :) - total skin percent is "+((totalSkin/totalPixels)*100)+"% ");
                postMessage(false);
                return;
            }


            // check if the largest skin region is less than 35% of the total skin count
            // AND if the second largest region is less than 30% of the total skin count
            // AND if the third largest region is less than 30% of the total skin count
            if ((skinRegions[0].length / totalSkin) * 100 < 35
                    && (skinRegions[1].length / totalSkin) * 100 < 30
                    && (skinRegions[2].length / totalSkin) * 100 < 30)
            {
                // the image is not nude.
                //console.log("it's not nude :) - less than 35%,30%,30% skin in the biggest areas :" + ((skinRegions[0].length/totalSkin)*100) + "%, " + ((skinRegions[1].length/totalSkin)*100)+"%, "+((skinRegions[2].length/totalSkin)*100)+"%");
                postMessage(false);
                return;

            }

            // check if the number of skin pixels in the largest region is less than 45% of the total skin count
            if ((skinRegions[0].length / totalSkin) * 100 < 45)
            {
                // it's not nude
                //console.log("it's not nude :) - the biggest region contains less than 45%: "+((skinRegions[0].length/totalSkin)*100)+"%");
                postMessage(false);
                return;
            }

            // TODO:
            // build the bounding polygon by the regions edge values:
            // Identify the leftmost, the uppermost, the rightmost, and the lowermost skin pixels of the three largest skin regions.
            // Use these points as the corner points of a bounding polygon.

            // TODO:
            // check if the total skin count is less than 30% of the total number of pixels
            // AND the number of skin pixels within the bounding polygon is less than 55% of the size of the polygon
            // if this condition is true, it's not nude.

            // TODO: include bounding polygon functionality
            // if there are more than 60 skin regions and the average intensity within the polygon is less than 0.25
            // the image is not nude
            if (skinRegions.length > 60)
            {
                //console.log("it's not nude :) - more than 60 skin regions");
                postMessage(false);
                return;
            }


            // otherwise it is nude
            postMessage(true);

        }


        byte[] sortRegions(byte[] skinRegions)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < length - 1; i++)
                {
                    if (skinRegions[i].length < skinRegions[i + 1].length)
                    {
                        sorted = false;
                        var temp = skinRegions[i];
                        skinRegions[i] = skinRegions[i + 1];
                        skinRegions[i + 1] = temp;
                    }
                }
            }
        }


        bool classifySkin(int r, int g, int b)
        {
            // A Survey on Pixel-Based Skin Color Detection Techniques
            int rgbClassifier = ((r > 95) && (g > 40 && g < 100) && (b > 20) && ((Math.Max(r, g, b) - Math.Min(r, g, b)) > 15) && (Math.Abs(r - g) > 15) && (r > g) && (r > b));
            byte[] nurgb = toNormalizedRgb(r, g, b);
            int nr = nurgb[0];
            int ng = nurgb[1];
            int nb = nurgb[2];
            normRgbClassifier = (((nr / ng) > 1.185) && (((r * b) / (Math.Pow(r + g + b, 2))) > 0.107) && (((r * g) / (Math.Pow(r + g + b, 2))) > 0.112));
            //int hsv = toHsv(r, g, b);
            //int h = hsv[0]*100;
            //int s = hsv[1];
            //hsvClassifier = (h < 50 && h > 0 && s > 0.23 && s < 0.68);
            int hsv = toHsvTest(r, g, b);
            int h = hsv[0];
            int s = hsv[1];
            hsvClassifier = (h > 0 && h < 35 && s > 0.23 && s < 0.68);
            /*
             * ycc doesnt work
	 
            ycc = toYcc(r, g, b),
            y = ycc[0],
            cb = ycc[1],
            cr = ycc[2],
            yccClassifier = ((y > 80) && (cb > 77 && cb < 127) && (cr > 133 && cr < 173));
            */

            return (rgbClassifier || normRgbClassifier || hsvClassifier); // 
        }

        byte[] toYcc(int r, int g, int b)
        {
            r /= 255;
            g /= 255;
            b /= 255;

            int y = 0.299 * r + 0.587 * g + 0.114 * b;
            int cr = r - y;
            int cb = b - y;

            return new byte[] { y, cr, cb };
        }

        byte[] toHsv(int r, int g, int b)
        {
            return new byte[] {
	        // hue
	        Math.acos((0.5*((r-g)+(r-b)))/(Math.sqrt((Math.pow((r-g),2)+((r-b)*(g-b)))))),
	        // saturation
	        1-(3*((Math.min(r,g,b))/(r+g+b))),
	        // value
	        (1/3)*(r+g+b)
	        };
        }

        byte[] toHsvTest(int r, int g, int b)
        {
            int h = 0;
            int mx = Math.max(r, g, b);
            int mn = Math.min(r, g, b);
            int dif = mx - mn;

            if (mx == r)
            {
                h = (g - b) / dif;
            }
            else if (mx == g)
            {
                h = 2 + ((g - r) / dif);
            }
            else
            {
                h = 4 + ((r - g) / dif);
            }
            h = h * 60;
            if (h < 0)
            {
                h = h + 360;
            }

            return new byte[] { h, 1 - (3 * ((Math.min(r, g, b)) / (r + g + b))), (1 / 3) * (r + g + b) };

        }

        byte[] toNormalizedRgb(int r, int g, int b)
        {
            int sum = r + g + b;
            return new byte[] { (r / sum), (g / sum), (b / sum) };
        }


        public bool DetectNude(string imagePath)
        {
            Bitmap bmp = new Bitmap(imagePath);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


            // procesamos la imagen
            bool isNude = scanImage(rgbValues, bmp.Width, bmp.Height);

            //
            // Set every third value to 255. A 24bpp bitmap will look red.  
            //for (int counter = 2; counter < rgbValues.Length; counter += 3)
            //    rgbValues[counter] = 255;
            //
            //


            // Copy the RGB values back to the bitmap
            //System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Draw the modified image.
            //e.Graphics.DrawImage(bmp, 0, 150);

            return isNude;
        }
    }
}