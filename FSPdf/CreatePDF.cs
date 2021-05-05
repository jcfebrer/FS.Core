using System;
using System.IO;
using System.Text.RegularExpressions;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.Core.Entities;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using FSNetwork;

namespace FSPdf
{
	/// <summary>
	/// Description of CreatePDF.
	/// </summary>
	public class CreatePDF
	{
		public CreatePDF()
		{
		}
		
       
		
		public static void Generate(System.Web.HttpContext context, string html, bool landscape)
		{
			PdfGenerateConfig pdfConfig = new PdfGenerateConfig();
        	pdfConfig.PageSize = PdfSharp.PageSize.A4;
        	pdfConfig.MarginBottom = 25;
        	pdfConfig.MarginLeft = 25;
        	pdfConfig.MarginRight = 25;
        	pdfConfig.MarginTop = 25;
        	
        	
        	if(landscape)pdfConfig.PageOrientation = PageOrientation.Landscape;
			
			html = Web.ReplaceUrlRes(html);
			html = FSLibrary.TextUtil.RemoveLinks(html);
			
			//string tmpFile = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            //doc.Save(tmpFile);
            
            using (MemoryStream ms = new MemoryStream())
		    {
		        PdfDocument doc = PdfGenerator.GeneratePdf(html, pdfConfig, null, null, null);
		        doc.Save(ms);
		        
	            context.Response.ContentType = "application/pdf";
	            context.Response.AddHeader("content-length", ms.ToArray().Length.ToString());
	            context.Response.BinaryWrite(ms.ToArray());
	 			context.Response.Flush();
	 			context.Response.Close();
            }

//            context.Response.AppendHeader("Content-Disposition", 
//                                          "inline; filename=" + tmpFile);

		}
	}
}