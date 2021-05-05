using FSException;
using FSLibrary;
using FSTrace;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace FSGraphics
{
    /// <summary>
    ///     This class encapsulates the API functions necessary to get the
    ///     desktop image and form a bitmap from it.
    /// </summary>
    public sealed class CaptureScreen
    {
        /// <summary>
        ///     Captures the windows desktop and returns it as a Bitmap
        /// </summary>
        /// <returns></returns>
        public static Bitmap CaptureScreenDesktop()
        {
            try
            {
                //Get a pointer to the desktop window
                var desktopWindow = Win32API.GetDesktopWindow();

                //Get a device context from the desktop window
                var desktopDC = Win32API.GetDC(desktopWindow);

                var desktopImage = GetBitmap(desktopDC);

                Win32API.ReleaseDC(desktopDC);

                return desktopImage;
            }
            catch (ExceptionUtil e)
            {
                Log.TraceError(e);
                return null;
            }
        }

        internal static Bitmap GetBitmap(IntPtr imagePtr)
        {
            //Get a GDI handle to the image
            var hwnd = Win32API.GetCurrentObject(imagePtr, 7);

            //This call takes as an argument the handle to a GDI image
            var desktopImage = Image.FromHbitmap(hwnd);

            return desktopImage;
        }

        internal static Bitmap GetForegroundWindowBitmap()
        {
            var handle = Win32API.GetForegroundWindow();

            //Get a device context from the desktop window
            var windowDC = Win32API.GetWindowDC(handle);

            //Get a GDI handle to the image
            var hwnd = Win32API.GetCurrentObject(windowDC, 0);

            //This call takes as an argument the handle to a GDI image
            var desktopImage = Image.FromHbitmap(hwnd);

            Win32API.ReleaseDC(windowDC);

            return desktopImage;
        }

        /// <summary>
        ///     Creates a screen capture in a bitmap format. The currently used method in EncodedRectangleFactory.
        /// </summary>
        /// <param name="rectangle">The rectangle from the screen that we should take a screenshot from.</param>
        /// <returns>A bitmap containing the image data of our screenshot. The return value is null only if a problem occured.</returns>
        public static Bitmap CreateScreenCapture(Rectangle rectangle, bool showCursor)
        {
            return CreateScreenCapture(rectangle, showCursor, PixelFormat.Format32bppArgb);
        }

        public static Bitmap CreateScreenCapture(Rectangle rectangle, bool showCursor, PixelFormat pixelFormat)
        {
            try
            {
                //LogUtil.TraceInfo("Screen capture method API32 start");
                //Bitmap b = CaptureScreenDesktop();
                //if (b != null) return b;

                Log.TraceInfo("Capura con pixelformat: " + pixelFormat);
                //Stopwatch t = Stopwatch.StartNew();
                var width = rectangle.Width;
                var height = rectangle.Height;
                var bitmap = new Bitmap(width, height, pixelFormat); //System.Drawing.Imaging.PixelFormat.Format48bppRgb
                var g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new Size(width, height));

                if (showCursor)
                    Cursors.Default.Draw(g,
                        new Rectangle(new Point(Cursor.Position.X - 10, Cursor.Position.Y - 10), new Size(32, 32)));

                //t.Stop();
                //LogUtil.TraceInfo("Screen capture method 1 done in: " + t.ElapsedMilliseconds + "ms. Size: " + bitmap.Size.ToString());
                return bitmap;
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                Thread.Sleep(200);
                try
                {
                    //LogUtil.TraceInfo("Screen capture method 2 start");
                    var width = rectangle.Width;
                    var height = rectangle.Height;
                    var bitmap = new Bitmap(width, height, pixelFormat);
                    var g = Graphics.FromImage(bitmap);
                    g.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, new Size(width, height));

                    if (showCursor)
                        Cursors.Default.Draw(g,
                            new Rectangle(new Point(Cursor.Position.X - 32, Cursor.Position.Y - 32), new Size(32, 32)));

                    //LogUtil.TraceInfo("Screen capture method 2 done");
                    return bitmap;
                }
                catch (Exception)
                {
                    Log.TraceError(ex);
                    return null;
                }
            }
        }

        /// <summary>
        ///     An alternate method of creating a screenshot.
        /// </summary>
        /// <param name="x">The X coordinate of the Rectangle of our screenshot</param>
        /// <param name="y">The Y coordinate of the Rectangle of our screenshot</param>
        /// <param name="w">The width of the Rectangle of our screenshot</param>
        /// <param name="h">The height of the Rectangle of our screenshot</param>
        /// <returns></returns>
        public static Bitmap CreateScreenCapture(int x, int y, int w, int h, bool showCursor)
        {
            var r = new Rectangle(x, y, w, h);
            return CreateScreenCapture(r, showCursor);
        }
    }
}