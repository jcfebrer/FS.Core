using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGraphics
{
    class FontUtil
    {
        public static int FontArray(ref string[] aArray)
        {
            var fc = new InstalledFontCollection();

            FontFamily[] afm = null;
            afm = fc.Families;

            aArray = new string[afm.Length + 1];

            var i = 0;
            for (i = 0; i <= afm.Length - 1; i += i + 1) aArray.SetValue(afm[i].Name, i);

            return i;
        }


        public static IEnumerator FontCollection()
        {
            var fc = new InstalledFontCollection();

            return fc.Families.GetEnumerator();
        }
    }
}
