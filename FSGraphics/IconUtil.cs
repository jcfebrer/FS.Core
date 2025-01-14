using FSLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGraphics
{
    public class IconUtil
    {
        public static Icon GetAssociatedIconFile(string fileName)
        {
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            if (icon == null)
            {
                ushort uicon;
                StringBuilder strB = new StringBuilder(fileName);
                IntPtr handle = Win32API.ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
                icon = Icon.FromHandle(handle);
            }

            return icon;
        }

        public static Icon GetIconFile(string fileName)
        {
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            if (icon == null)
            {
                IntPtr handle = Win32API.ExtractIcon(IntPtr.Zero, fileName, 0);
                icon = Icon.FromHandle(handle);
            }

            return icon;
        }
    }
}