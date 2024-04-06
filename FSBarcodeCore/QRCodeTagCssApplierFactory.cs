using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.StyledXmlParser.Node;

namespace FSBarcodeCore
{

    /// <summary>
    /// Example of a custom CssApplier factory for pdfHTML
    /// The tag qr is mapped on a BlockCssApplier. Every other tag is mapped to the default.
    /// </summary>
    public class QRCodeTagCssApplierFactory : DefaultCssApplierFactory
    {

        /// <summary>
        /// Gets a custom CSS applier.
        /// This is a hook method. Users wanting to provide a custom mapping or introduce
        /// their own ITagWorkers should implement this method.
        /// </summary>
        /// <param name="tag"> the tag</param>
        /// <returns>the custom tag worker</returns>
        public override ICssApplier GetCustomCssApplier(IElementNode tag)
        {
            if (tag.Name().Equals("qr"))
            {
                return new BlockCssApplier();
            }
            return null;
        }

    }
}
