#if NET451_OR_GREATER || NETCOREAPP

using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.IO;

namespace FSBarcode
{

    /// <summary>
    /// Se encarga de realizar los trabajos de generación de 
    /// dpocumentos pdf.
    /// </summary>
    public class PdfManager
    {

        /// <summary>
        ///  Custom tagworkerfactory for pdfHTML for tag qr.
        /// </summary>
        public QRCodeTagWorkerFactory QRCodeTagWorkerFactory { get; private set; }

        /// <summary>
        /// Convierte texto html de entrada en un archivo pdf.
        /// </summary>
        /// <param name="html">Html a convertir en pdf.</param>
        /// <param name="orientation">Orientación.</param>
        /// <param name="fontData">Byte content of the font program file.</param>
        /// <returns>Datos binarios del pdf.</returns>
        public byte[] GetPdfFormHtml(string html, 
            string orientation = "PORTRAIT", byte[] fontData = null)
        {

            QRCodeTagWorkerFactory = new QRCodeTagWorkerFactory();

            ConverterProperties properties = new ConverterProperties();
            properties.SetTagWorkerFactory(QRCodeTagWorkerFactory);
            properties.SetCssApplierFactory(new QRCodeTagCssApplierFactory());

            if(fontData != null) 
            {
                iText.Layout.Font.FontProvider fontProvider = new iText.Layout.Font.FontProvider();
                fontProvider.AddFont(fontData, PdfEncodings.IDENTITY_H);

                properties.SetFontProvider(fontProvider);
            }

            byte[] result = null;

            using (var ms = new MemoryStream())
            {
                using (var pdfDocument = new PdfDocument(new PdfWriter(ms)))
                {

                    if (orientation.ToUpper() == "LANDSCAPE")
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                    HtmlConverter.ConvertToPdf(html, pdfDocument, properties);
                    result = ms.ToArray();
                }
            }

            return result;

        }

        /// <summary>
        /// Devuelve el código QR como una imágen
        /// de itext.
        /// </summary>
        /// <returns>Código QR como una imágen de itext.</returns>
        public Image GetQrCodeAsImage() 
        {
            return QRCodeTagWorkerFactory?.QRCodeTagWorker?.GetElementResult() as Image;
        }
    
    }
}

#endif