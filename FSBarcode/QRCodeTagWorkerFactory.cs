using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.StyledXmlParser.Node;
using System;

namespace FSBarcode
{

    /// <summary>
    /// Custom tagworkerfactory for pdfHTML for tag qr.
    /// </summary>
    [CLSCompliant(false)]
    public class QRCodeTagWorkerFactory : DefaultTagWorkerFactory
    {

        /// <summary>
        /// The custom tag worker.
        /// </summary>
        public QRCodeTagWorker QRCodeTagWorker { get; private set; }

        /// <summary>
        /// Custom tagworkerfactory for pdfHTML
        /// The tag /<qr/> is mapped on a QRCode tagworker. Every other tag is mapped to the default.
        /// This is a hook method. Users wanting to provide a custom mapping or introduce
        /// their own ITagWorkers should implement this method.
        /// </summary>
        /// <param name="tag"> the tag</param>
        /// <param name="context"> the context</param>
        /// <returns>the custom tag worker</returns>
        public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context)
        {

            if (tag.Name().Equals("qr"))
            {
                QRCodeTagWorker = new QRCodeTagWorker(tag, context);
                return QRCodeTagWorker;
            }

            return null;
        }

    }
}
