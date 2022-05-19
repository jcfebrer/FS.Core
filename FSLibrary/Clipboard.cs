using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FSLibrary
{
    /// <summary>
    /// Funciones relacionadas con el portapapeles
    /// </summary>
    public static class Clipboard
    {
        /// <summary>
        /// Obtiene el texto del portapapeles
        /// </summary>
        /// <returns></returns>
        public static string GetClipBoardText()
        {
            if (System.Windows.Forms.Clipboard.ContainsText(TextDataFormat.Text))
            {
                return System.Windows.Forms.Clipboard.GetText(TextDataFormat.Text);
            }
            return null;
        }

        /// <summary>
        /// Obtiene el objecto del portapapeles
        /// </summary>
        /// <returns></returns>
        public static IDataObject GetDataObject()
        {
            return System.Windows.Forms.Clipboard.GetDataObject();
        }

        /// <summary>
        /// Establece el objeto en el portapapeles
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="copy"></param>
        public static void SetDataObject(object obj, bool copy)
        {
            System.Windows.Forms.Clipboard.SetDataObject(obj, copy);
        }
    }
}
