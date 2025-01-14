using FSLibraryCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGraphicsCore
{
    public class IconUtil
    {
        public Icon GetAssociatedIconFile(string fileName)
        {
            ushort uicon;
            StringBuilder strB = new StringBuilder(fileName);
            IntPtr handle = Win32API.ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }

        public Icon GetIconFile(string fileName)
        {
            IntPtr handle = Win32API.ExtractIcon(IntPtr.Zero, fileName, 0);
            Icon ico = Icon.FromHandle(handle);

            return ico;
        }
    }
}
