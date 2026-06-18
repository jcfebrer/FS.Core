#if NET40_OR_GREATER

using System;
using System.IO;
using System.Text;
using System.Security;
using System.Collections.Generic;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FSConvert
{
    public class ConvertRptToWpf
    {
        private const double TwipsPerPixel = 15.0;

        /// <summary>
        /// Procesa un archivo .rpt, escribe automáticamente los archivos .xaml y .cs tanto del reporte principal 
        /// como de sus subreportes en la misma carpeta, y retorna el contenido XAML principal.
        /// </summary>
        public static string Convert(string rptFilePath)
        {
            if (!File.Exists(rptFilePath))
            {
                throw new FileNotFoundException("El archivo de Crystal Reports especificado no existe.", rptFilePath);
            }

            string targetDirectory = Path.GetDirectoryName(rptFilePath);
            string mainReportName = Path.GetFileNameWithoutExtension(rptFilePath).Replace(" ", "_").Replace("-", "_");

            StringBuilder xamlBuilder = new StringBuilder();
            Dictionary<string, string> subreportsPendingToSave = new Dictionary<string, string>();

            using (ReportDocument report = new ReportDocument())
            {
                try
                {
                    report.Load(rptFilePath);

                    double pageWidthInPixels = CalcularAnchoPaginaEnPixeles(report);
                    string sPageWidth = pageWidthInPixels.ToString("F1", CultureInfo.InvariantCulture);

                    // 1. Cabecera del archivo raíz XAML con el nombre exacto del archivo físico .rpt
                    xamlBuilder.AppendLine($"<UserControl x:Class=\"FSConvert.GeneratedViews.{mainReportName}\"");
                    xamlBuilder.AppendLine("             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
                    xamlBuilder.AppendLine("             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
                    xamlBuilder.AppendLine("             xmlns:views=\"clr-namespace:FSConvert.GeneratedViews\"");
                    xamlBuilder.AppendLine("             Background=\"White\">");
                    xamlBuilder.AppendLine("    <ScrollViewer VerticalScrollBarVisibility=\"Auto\" HorizontalScrollBarVisibility=\"Auto\">");
                    xamlBuilder.AppendLine($"        <StackPanel Orientation=\"Vertical\" Margin=\"20\" Width=\"{sPageWidth}\">");

                    // 2. Procesar las secciones internas
                    GenerarSeccionesXaml(report, xamlBuilder, sPageWidth, subreportsPendingToSave, mainReportName);

                    // 3. Cierre del UserControl principal
                    xamlBuilder.AppendLine("        </StackPanel>");
                    xamlBuilder.AppendLine("    </ScrollViewer>");
                    xamlBuilder.AppendLine("</UserControl>");

                    string mainReportXamlContent = xamlBuilder.ToString();

                    // =================================================================
                    // ESCRITURA AUTOMÁTICA DEL REPORTE PRINCIPAL (.xaml y .xaml.cs)
                    // =================================================================
                    string mainXamlPath = Path.Combine(targetDirectory, $"{mainReportName}.xaml");
                    File.WriteAllText(mainXamlPath, mainReportXamlContent, Encoding.UTF8);

                    string mainCsPath = Path.Combine(targetDirectory, $"{mainReportName}.xaml.cs");
                    string mainCsContent = GenerarCodeBehind(mainReportName);
                    File.WriteAllText(mainCsPath, mainCsContent, Encoding.UTF8);

                    // =================================================================
                    // ESCRITURA AUTOMÁTICA DE LOS SUBREPORTES (.xaml y .xaml.cs)
                    // =================================================================
                    foreach (KeyValuePair<string, string> subreport in subreportsPendingToSave)
                    {
                        string subreportClassName = $"{mainReportName}_{subreport.Key}";

                        // Guardar XAML del subreporte
                        string xamlPath = Path.Combine(targetDirectory, $"{subreportClassName}.xaml");
                        File.WriteAllText(xamlPath, subreport.Value, Encoding.UTF8);

                        // Guardar .cs del subreporte
                        string csPath = Path.Combine(targetDirectory, $"{subreportClassName}.xaml.cs");
                        string csContent = GenerarCodeBehind(subreportClassName);
                        File.WriteAllText(csPath, csContent, Encoding.UTF8);
                    }

                    return mainReportXamlContent;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error crítico en el proceso de generación XAML: {ex.Message}", ex);
                }
                finally
                {
                    report.Close();
                }
            }
        }

        private static string GenerarCodeBehind(string className)
        {
            StringBuilder csSb = new StringBuilder();
            csSb.AppendLine("using System.Windows.Controls;");
            csSb.AppendLine();
            csSb.AppendLine("namespace FSConvert.GeneratedViews");
            csSb.AppendLine("{");
            csSb.AppendLine($"    public partial class {className} : UserControl");
            csSb.AppendLine("    {");
            csSb.AppendLine($"        public {className}()");
            csSb.AppendLine("        {");
            csSb.AppendLine("            InitializeComponent();");
            csSb.AppendLine("        }");
            csSb.AppendLine("    }");
            csSb.AppendLine("}");
            return csSb.ToString();
        }

        private static double CalcularAnchoPaginaEnPixeles(ReportDocument doc)
        {
            try
            {
                double maxRightTwips = 0;
                foreach (Section section in doc.ReportDefinition.Sections)
                {
                    if (section.SectionFormat.EnableSuppress) continue;
                    foreach (ReportObject obj in section.ReportObjects)
                    {
                        if (obj.ObjectFormat.EnableSuppress) continue;
                        double rightEdge = obj.Left + obj.Width;
                        if (rightEdge > maxRightTwips) maxRightTwips = rightEdge;
                    }
                }
                return maxRightTwips > 0 ? (maxRightTwips / TwipsPerPixel) + 30.0 : 820.0;
            }
            catch { return 820.0; }
        }

        private static void GenerarSeccionesXaml(ReportDocument doc, StringBuilder sb, string sPageWidth, Dictionary<string, string> subreportsDict, string mainReportName)
        {
            foreach (Section section in doc.ReportDefinition.Sections)
            {
                if (section.SectionFormat.EnableSuppress) continue;

                int objetosVisibles = 0;
                foreach (ReportObject obj in section.ReportObjects)
                {
                    if (!obj.ObjectFormat.EnableSuppress) objetosVisibles++;
                }

                if (objetosVisibles == 0) continue;

                double heightInPixels = section.Height / TwipsPerPixel;
                string sSectionHeight = heightInPixels.ToString("F1", CultureInfo.InvariantCulture);

                sb.AppendLine($"\n            ");
                sb.AppendLine($"            <Canvas Height=\"{sSectionHeight}\" Width=\"{sPageWidth}\" Background=\"Transparent\" HorizontalAlignment=\"Left\">");

                ProcesarObjetosDeSeccion(section.ReportObjects, sb, subreportsDict, sPageWidth, mainReportName);

                sb.AppendLine("            </Canvas>");
            }
        }

        private static void ProcesarObjetosDeSeccion(ReportObjects reportObjects, StringBuilder sb, Dictionary<string, string> subreportsDict, string sPageWidth, string mainReportName)
        {
            foreach (ReportObject obj in reportObjects)
            {
                if (obj.ObjectFormat.EnableSuppress) continue;

                double left = obj.Left / TwipsPerPixel;
                double top = obj.Top / TwipsPerPixel;
                double width = obj.Width / TwipsPerPixel;
                double height = obj.Height / TwipsPerPixel;

                string sLeft = left.ToString("F1", CultureInfo.InvariantCulture);
                string sTop = top.ToString("F1", CultureInfo.InvariantCulture);
                string sWidth = width.ToString("F1", CultureInfo.InvariantCulture);
                string sHeight = height.ToString("F1", CultureInfo.InvariantCulture);

                switch (obj.Kind)
                {
                    case ReportObjectKind.TextObject:
                        var textObj = (TextObject)obj;
                        AppendTextBlock(sb, SecurityElement.Escape(textObj.Text), textObj.Font, textObj.ObjectFormat.HorizontalAlignment, left, top, width, height);
                        break;

                    case ReportObjectKind.FieldObject:
                        var fieldObj = (FieldObject)obj;
                        string bindingPath = fieldObj.DataSource.Name.Replace("{", "").Replace("}", "").Replace(".", "_");
                        AppendTextBlock(sb, $"{{Binding {bindingPath}, Mode=OneWay}}", fieldObj.Font, fieldObj.ObjectFormat.HorizontalAlignment, left, top, width, height, isBinding: true);
                        break;

                    case ReportObjectKind.LineObject:
                        var lineObj = (LineObject)obj;
                        string slx1 = (lineObj.Left / TwipsPerPixel).ToString("F1", CultureInfo.InvariantCulture);
                        string sly1 = (lineObj.Top / TwipsPerPixel).ToString("F1", CultureInfo.InvariantCulture);
                        string slx2 = (lineObj.Right / TwipsPerPixel).ToString("F1", CultureInfo.InvariantCulture);
                        string sly2 = (lineObj.Bottom / TwipsPerPixel).ToString("F1", CultureInfo.InvariantCulture);
                        sb.AppendLine($"                    <Line X1=\"{slx1}\" Y1=\"{sly1}\" X2=\"{slx2}\" Y2=\"{sly2}\" Stroke=\"#1E293B\" StrokeThickness=\"1.0\" />");
                        break;

                    case ReportObjectKind.BoxObject:
                        sb.AppendLine($"                    <Rectangle Canvas.Left=\"{sLeft}\" Canvas.Top=\"{sTop}\" Width=\"{sWidth}\" Height=\"{sHeight}\" Stroke=\"#64748B\" StrokeThickness=\"1\" RadiusX=\"2\" RadiusY=\"2\" />");
                        break;

                    case ReportObjectKind.SubreportObject:
                        var subreportObj = (SubreportObject)obj;
                        string cleanSubName = subreportObj.Name.Replace(" ", "_").Replace("-", "_");

                        string subreportClassName = $"{mainReportName}_{cleanSubName}";
                        string subreportDataBinding = $"SubreportData_{cleanSubName}";

                        sb.AppendLine($"                    ");
                        sb.AppendLine($"                    <views:{subreportClassName} DataContext=\"{{Binding {subreportDataBinding}, Mode=OneWay}}\" Canvas.Left=\"{sLeft}\" Canvas.Top=\"{sTop}\" Width=\"{sWidth}\" Height=\"{sHeight}\" />");

                        if (!subreportsDict.ContainsKey(cleanSubName))
                        {
                            try
                            {
                                ReportDocument subDoc = subreportObj.OpenSubreport(subreportObj.SubreportName);
                                string subreportXaml = GenerarUserControlSubreporte(subreportClassName, subDoc, sPageWidth, mainReportName);
                                subreportsDict.Add(cleanSubName, subreportXaml);
                            }
                            catch (Exception ex)
                            {
                                subreportsDict.Add(cleanSubName, $"<UserControl x:Class=\"FSConvert.GeneratedViews.{subreportClassName}\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"><Grid><TextBlock Foreground=\"Red\" Text=\"Error: {SecurityElement.Escape(ex.Message)}\"/></Grid></UserControl>");
                            }
                        }
                        break;

                    case ReportObjectKind.PictureObject:
                        string imgBinding = $"Imagen_{obj.Name}";
                        if (obj is BlobFieldObject blobObj && blobObj.DataSource != null)
                            imgBinding = blobObj.DataSource.Name.Replace("{", "").Replace("}", "").Replace(".", "_");
                        else if (obj is PictureObject picObj)
                            imgBinding = picObj.Name;

                        sb.AppendLine($"                    <Image Source=\"{{Binding {imgBinding}, Mode=OneWay}}\" Canvas.Left=\"{sLeft}\" Canvas.Top=\"{sTop}\" Width=\"{sWidth}\" Height=\"{sHeight}\" Stretch=\"Uniform\" />");
                        break;

                    default:
                        sb.AppendLine($"                    <Border Canvas.Left=\"{sLeft}\" Canvas.Top=\"{sTop}\" Width=\"{sWidth}\" Height=\"{sHeight}\" BorderBrush=\"#E2E8F0\" BorderThickness=\"1\" BorderStyle=\"Dash\"><TextBlock Text=\"[{obj.Kind}: {obj.Name}]\" FontSize=\"9\" Foreground=\"#94A3B8\" HorizontalAlignment=\"Center\" VerticalAlignment=\"Center\" /></Border>");
                        break;
                }
            }
        }

        private static string GenerarUserControlSubreporte(string className, ReportDocument subDoc, string sPageWidth, string mainReportName)
        {
            StringBuilder subSb = new StringBuilder();
            subSb.AppendLine($"<UserControl x:Class=\"FSConvert.GeneratedViews.{className}\"");
            subSb.AppendLine("             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            subSb.AppendLine("             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            subSb.AppendLine("             Background=\"Transparent\">");
            subSb.AppendLine("    <StackPanel Orientation=\"Vertical\">");

            var emptyDict = new Dictionary<string, string>();
            foreach (Section subSection in subDoc.ReportDefinition.Sections)
            {
                if (subSection.SectionFormat.EnableSuppress) continue;

                double heightInPixels = subSection.Height / TwipsPerPixel;
                string sSectionHeight = heightInPixels.ToString("F1", CultureInfo.InvariantCulture);

                subSb.AppendLine($"        ");
                subSb.AppendLine($"        <Canvas Height=\"{sSectionHeight}\" Width=\"{sPageWidth}\" Background=\"Transparent\" HorizontalAlignment=\"Left\">");

                ProcesarObjetosDeSeccion(subSection.ReportObjects, subSb, emptyDict, sPageWidth, mainReportName);

                subSb.AppendLine("        </Canvas>");
            }

            subSb.AppendLine("    </StackPanel>");
            subSb.AppendLine("</UserControl>");

            return subSb.ToString();
        }

        private static void AppendTextBlock(StringBuilder sb, string text, System.Drawing.Font font, Alignment horizontalAlign, double left, double top, double width, double height, bool isBinding = false)
        {
            string fontWeight = font.Bold ? "Bold" : "Normal";
            string fontStyle = font.Italic ? "Italic" : "Normal";
            string textDecoration = font.Underline ? "Underline" : "None";

            string wpfAlign = "Left";
            if (horizontalAlign == Alignment.HorizontalCenterAlign) wpfAlign = "Center";
            else if (horizontalAlign == Alignment.RightAlign) wpfAlign = "Right";
            else if (horizontalAlign == Alignment.Justified) wpfAlign = "Justify";

            string sLeft = left.ToString("F1", CultureInfo.InvariantCulture);
            string sTop = top.ToString("F1", CultureInfo.InvariantCulture);
            string sWidth = width.ToString("F1", CultureInfo.InvariantCulture);
            string sHeight = height.ToString("F1", CultureInfo.InvariantCulture);
            string sFontSize = font.Size.ToString("F1", CultureInfo.InvariantCulture);

            sb.AppendLine($"                    <TextBlock Text=\"{text}\" Canvas.Left=\"{sLeft}\" Canvas.Top=\"{sTop}\" Width=\"{sWidth}\" Height=\"{sHeight}\" FontFamily=\"{font.Name}\" FontSize=\"{sFontSize}\" FontWeight=\"{fontWeight}\" FontStyle=\"{fontStyle}\" TextDecorations=\"{textDecoration}\" TextAlignment=\"{wpfAlign}\" TextWrapping=\"Wrap\" VerticalAlignment=\"Center\" Foreground=\"#0F172A\" />");
        }
    }
}

#endif