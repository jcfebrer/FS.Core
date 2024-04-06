using iText.Barcodes;
using iText.Barcodes.Qrcode;
using iText.Html2pdf.Attach;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using System;
using System.Collections.Generic;

namespace FSBarcodeCore
{

    /// <summary>
    /// Custom tagworker implementation for pdfHTML.
    /// The tagworker processes a /<qr/> tag using iText Barcode functionality
    /// </summary>
    public class QRCodeTagWorker : ITagWorker
    {

        private static string[] allowedErrorCorrection = { "L", "M", "Q", "H" };
        private static string[] allowedCharset = { "Cp437", "Shift_JIS", "ISO-8859-1", "ISO-8859-16" };

        private BarcodeQRCode qrCode;
        private Image qrCodeAsImage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="context"></param>
        public QRCodeTagWorker(IElementNode element, ProcessorContext context)
        {

            // Retrieve all necessary properties to create the barcode
            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();

            // Character set
            string charset = element.GetAttribute("charset");
            if (CheckCharacterSet(charset))
            {
                hints.Add(EncodeHintType.CHARACTER_SET, charset);
            }

            // Error-correction level
            string errorCorrection = element.GetAttribute("errorcorrection");
            if (CheckErrorCorrectionAllowed(errorCorrection))
            {
                ErrorCorrectionLevel errorCorrectionLevel = GetErrorCorrectionLevel(errorCorrection);
                hints.Add(EncodeHintType.ERROR_CORRECTION, errorCorrectionLevel);
            }

            // Create the QR-code
            qrCode = new BarcodeQRCode("placeholder", hints);

        }

        /// <summary>
        /// Placeholder for what needs to be done after the content of a tag has been processed.
        /// </summary>
        /// <param name="element">the element node</param>
        /// <param name="context">the processor context</param>
        public void ProcessEnd(IElementNode element, ProcessorContext context)
        {
            float moduleSize = 1.8f;
            // Transform barcode into image
            qrCodeAsImage = new Image(qrCode.CreateFormXObject(ColorConstants.BLACK, 
                moduleSize, context.GetPdfDocument()));

        }

        /// <summary>
        /// Placeholder for what needs to be done while the content of a tag is being processed.
        /// </summary>
        /// <param name="content">the content of a node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if content was successfully processed, otherwise false.</returns>
        public bool ProcessContent(String content, ProcessorContext context)
        {

            // Add content to the barcode
            qrCode.SetCode(content);
            return true;
        }

        /// <summary>
        /// Placeholder for what needs to be done when a child node is being processed.
        /// </summary>
        /// <param name="childTagWorker">the tag worker of the child node</param>
        /// <param name="context">the processor context</param>
        /// <returns> true, if child was successfully processed, otherwise false.</returns>
        public bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context)
        {
            return false;
        }

        /// <summary>
        ///  Gets a processed object if it can be expressed as an iText.Layout.IPropertyContainer
        ///  instance.
        /// </summary>
        /// <returns> The same object on every call. Might return null either if result is not yet
        /// produced or if this particular tag worker doesn't produce result in a form of
        /// iText.Layout.IPropertyContainer.</returns>
        public IPropertyContainer GetElementResult()
        {

            return qrCodeAsImage;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool CheckErrorCorrectionAllowed(string toCheck)
        {
            for (int i = 0; i < allowedErrorCorrection.Length; i++)
            {
                if (toCheck.ToUpper().Equals(allowedErrorCorrection[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static bool CheckCharacterSet(string toCheck)
        {
            for (int i = 0; i < allowedCharset.Length; i++)
            {
                if (toCheck.Equals(allowedCharset[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static ErrorCorrectionLevel GetErrorCorrectionLevel(string level)
        {
            switch (level)
            {
                case "L":
                    return ErrorCorrectionLevel.L;
                case "M":
                    return ErrorCorrectionLevel.M;
                case "Q":
                    return ErrorCorrectionLevel.Q;
                case "H":
                    return ErrorCorrectionLevel.H;
            }
            return null;

        }

    }
}
