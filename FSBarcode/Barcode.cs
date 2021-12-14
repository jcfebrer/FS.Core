using System;
using System.Drawing;
using System.IO;
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
		public static Bitmap Generate(string data, int width, int height, CodeFormat barcodeFormat)
		{
			QrCodeEncodingOptions options = new QrCodeEncodingOptions();
			options = new QrCodeEncodingOptions
			{
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = width,
				Height = height,
			};

			BarcodeWriter writer = new ZXing.BarcodeWriter();
			writer.Options = options;

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

			using (Bitmap result = writer.Write(data))
			{
				return new Bitmap(result);
			}
		}

		public static string ReadQRFromFile(string fileName)
		{
			BarcodeReader reader = new BarcodeReader();

			reader.AutoRotate = true;
			reader.TryInverted = true;
			reader.Options = new ZXing.Common.DecodingOptions { TryHarder = true };

			// create an in memory bitmap
			using (Bitmap barcodeBitmap = (Bitmap)Bitmap.FromFile(fileName))
			{
				// decode the barcode from the in memory bitmap
				Result result = reader.Decode(barcodeBitmap);
				// output results to console
				//Console.WriteLine($"Decoded barcode text: {barcodeResult?.Text}");
				//Console.WriteLine($"Barcode format: {barcodeResult?.BarcodeFormat}");

				if (result != null)
					return result.Text;
				else
					return "Imposible leer QR";
			}
		}

		private static void SaveQRToFile(string qrText, int width, int height, string fileName)
		{
			using (Bitmap bitMap = Generate(qrText, width, height, CodeFormat.QR))
			{
				bitMap.Save(fileName);
			}
		}

		private static Bitmap GetQRBitmap(string qrText, int width, int height)
		{
			using (Bitmap bitMap = Generate(qrText, width, height, CodeFormat.QR))
			{
				return bitMap;
			}
		}

		private static byte[] GetQRBytes(string qrText, int width, int height)
		{
			using (Bitmap bitMap = Generate(qrText, width, height, CodeFormat.QR))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					byte[] byteImage = ms.ToArray();
					return byteImage;
				}
			}
		}

		public static string ReadQRFromImage(Bitmap bitmap)
		{
			try
			{
				BarcodeReader reader = new BarcodeReader
					(null, newbitmap => new BitmapLuminanceSource(bitmap), luminance => new ZXing.Common.GlobalHistogramBinarizer(luminance));

				reader.AutoRotate = true;
				reader.TryInverted = true;
				reader.Options = new ZXing.Common.DecodingOptions { TryHarder = true };

				Result result = reader.Decode(bitmap);

				if (result != null)
					return result.Text;
				else
					return "Imposible de decodificar QR.";
			}
			catch (Exception ex)
			{
				return "Imposible de detectar QR. Error: " + ex.Message;
			}
		}
	}
}