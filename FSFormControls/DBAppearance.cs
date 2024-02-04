using System.Drawing;
using System.Windows.Forms;

namespace FSFormControls
{
    /// <summary>
    /// Compatibilidad con Infragistics
    /// </summary>
    public class DBAppearance
    {
        public enum Alpha
        {
            Default,
            Transparent,
            Opaque,
            UseAlphaLevel
        }

        public Color BackColor;
        public Color BackColor2;
        public Alpha BackColorAlpha;
        public object BackGradientAlignment;
        public object BackGradientStyle;
        public Color BorderColor;
        public Font FontData;
        public Color ForeColor;
        public Bitmap Image;
        public object ImageHAlign;
        public object ImageVAlign;
        public string TextHAlignAsString;
        public object TextTrimming;
        public string TextVAlignAsString;

        public HorizontalAlignment TextHAlign { get; set; }
        public HorizontalAlignment TextVAlign { get; set; }

        public void ResetBackColor()
        {
        }

        public void ResetForeColor()
        {
        }
    }
}