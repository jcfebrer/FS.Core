using System;
using System.Drawing;
using ZXing;
using ZXing.QrCode;

namespace FSBarcode
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class Barcode
	{
		public enum CodeFormat
		{
			QR,
			Code_128,
			Code_39,
			Ean_13,
			Ean_8
		}
		public Bitmap Generate(string valor, CodeFormat barcodeFormat)
		{
			BarcodeWriter writer = new BarcodeWriter();

			switch (barcodeFormat)
			{
				case CodeFormat.Code_128:
					writer.Format = BarcodeFormat.CODE_128;
					break;

				case CodeFormat.Code_39:
					writer.Format = BarcodeFormat.CODE_39;
					break;

				case CodeFormat.Ean_13:
					writer.Format = BarcodeFormat.EAN_13;
					break;

				case CodeFormat.Ean_8:
					writer.Format = BarcodeFormat.EAN_8;
					break;

				case CodeFormat.QR:
					writer.Format = BarcodeFormat.QR_CODE;
					break;
			}

			Bitmap result = writer.Write(valor);
			return new Bitmap(result);
		}

		public string ReadQRFromFile(string fileName)
		{
			BarcodeReader reader = new BarcodeReader();

			reader.AutoRotate = true;
			reader.TryInverted = true;
			reader.Options = new ZXing.Common.DecodingOptions { TryHarder = true };

			// create an in memory bitmap
			var barcodeBitmap = (Bitmap)Bitmap.FromFile(fileName);

			// decode the barcode from the in memory bitmap
			var result = reader.Decode(barcodeBitmap);

			// output results to console
			//Console.WriteLine($"Decoded barcode text: {barcodeResult?.Text}");
			//Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");

			if (result != null)
				return result.Text;
			else
				return "Imposible leer QR";
		}

		public Bitmap GenerateQR(string valor)
		{
			BarcodeWriter writer = new BarcodeWriter();
			writer.Format = BarcodeFormat.QR_CODE;
			writer.Options = new QrCodeEncodingOptions();
			writer.Options.Width = 400;
			writer.Options.Height = 400;

			Bitmap result = writer.Write(valor);
			return new Bitmap(result);
		}

		public string ReadQRFromImage(Bitmap bitmap)
		{
			try
			{
				BarcodeReader reader = new BarcodeReader
					(null, newbitmap => new BitmapLuminanceSource(bitmap), luminance => new ZXing.Common.GlobalHistogramBinarizer(luminance));

				reader.AutoRotate = true;
				reader.TryInverted = true;
				reader.Options = new ZXing.Common.DecodingOptions { TryHarder = true };

				var result = reader.Decode(bitmap);

				if (result != null)
					return result.Text;
				else
					return "QRCode couldn't be decoded.";
			}
			catch (Exception ex)
			{
				return "QRCode couldn't be detected. Error: " + ex.Message;
			}
		}
	}
}