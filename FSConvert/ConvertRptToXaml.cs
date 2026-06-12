#if NETFRAMEWORK

using System;
using System.IO;
using System.Text;
using System.Security; // Necesario para sanitizar strings de escape XML
// Referencias de SAP Crystal Reports
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace FSConvert
{
    public class ConvertRptToXaml
    {
        private const double TwipsPerPixel = 15.0;

        /// <summary>
        /// Procesa un archivo .rpt y retorna una cadena con el marcado XAML (UserControl) completo.
        /// </summary>
        public static string Convert(string rptFilePath)
        {
            if (!File.Exists(rptFilePath))
            {
                throw new FileNotFoundException("El archivo de Crystal Reports especificado no existe.", rptFilePath);
            }

            StringBuilder xamlBuilder = new StringBuilder();

            using (ReportDocument report = new ReportDocument())
            {
                try
                {
                    report.Load(rptFilePath);

                    // 1. Cabecera estándar del archivo raíz XAML (UserControl)
                    xamlBuilder.AppendLine("<UserControl x:Class=\"FSConvert.GeneratedViews.ReportView\"");
                    xamlBuilder.AppendLine("             xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
                    xamlBuilder.AppendLine("             xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
                    xamlBuilder.AppendLine("             Width=\"800\">");
                    xamlBuilder.AppendLine("    <ScrollViewer VerticalScrollBarVisibility=\"Auto\" HorizontalScrollBarVisibility=\"Auto\">");
                    xamlBuilder.AppendLine("        <StackPanel Orientation=\"Vertical\" Margin=\"15\">");

                    xamlBuilder.AppendLine("            ");
                    xamlBuilder.AppendLine("            ");
                    xamlBuilder.AppendLine("            ");

                    // 2. Procesar las secciones del reporte principal
                    GenerarSeccionesXaml(report, xamlBuilder, isSubreport: false);

                    // 3. Procesar los subinformes embebidos si existen
                    if (report.Subreports.Count > 0)
                    {
                        xamlBuilder.AppendLine("\n            ");
                        xamlBuilder.AppendLine("            ");
                        xamlBuilder.AppendLine("            ");

                        foreach (ReportDocument subreport in report.Subreports)
                        {
                            xamlBuilder.AppendLine($"            ");
                            GenerarSeccionesXaml(subreport, xamlBuilder, isSubreport: true);
                        }
                    }

                    // 4. Cierre de etiquetas del UserControl
                    xamlBuilder.AppendLine("        </StackPanel>");
                    xamlBuilder.AppendLine("    </ScrollViewer>");
                    xamlBuilder.AppendLine("</UserControl>");
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

            return xamlBuilder.ToString();
        }

        /// <summary>
        /// Recorre recursivamente o secuencialmente las secciones y traduce sus elementos internos a código XAML.
        /// </summary>
        private static void GenerarSeccionesXaml(ReportDocument doc, StringBuilder sb, bool isSubreport)
        {
            string prefijoAmbiente = isSubreport ? $"[Subreport: {doc.Name}] " : "";

            foreach (Section section in doc.ReportDefinition.Sections)
            {
                // Convertir la altura de la sección a píxeles lógicos WPF
                double heightInPixels = section.Height / TwipsPerPixel;

                // Agrupamos cada sección en un GroupBox de WPF para mantener claridad semántica y visual
                sb.AppendLine($"\n            ");
                sb.AppendLine($"            <GroupBox Header=\"{section.Name}\" Margin=\"0,0,0,15\" BorderBrush=\"#CBD5E1\">");

                // Usamos un Canvas porque Crystal maneja posicionamiento absoluto (coordenadas X, Y fijas)
                sb.AppendLine($"                <Canvas Height=\"{heightInPixels:F1}\" Background=\"#F8FAFC\">");

                foreach (ReportObject obj in section.ReportObjects)
                {
                    // Cálculo de coordenadas relativas de dibujo en el lienzo
                    double left = obj.Left / TwipsPerPixel;
                    double top = obj.Top / TwipsPerPixel;
                    double width = obj.Width / TwipsPerPixel;
                    double height = obj.Height / TwipsPerPixel;

                    switch (obj.Kind)
                    {
                        case ReportObjectKind.TextObject:
                            var textObj = (TextObject)obj;
                            string textoSanitizado = SecurityElement.Escape(textObj.Text);
                            string fontWeight = textObj.Font.Bold ? "Bold" : "Normal";
                            string fontStyle = textObj.Font.Italic ? "Italic" : "Normal";

                            sb.AppendLine($"                    <TextBlock Text=\"{textoSanitizado}\" " +
                                          $"Canvas.Left=\"{left:F1}\" Canvas.Top=\"{top:F1}\" " +
                                          $"Width=\"{width:F1}\" Height=\"{height:F1}\" " +
                                          $"FontFamily=\"{textObj.Font.Name}\" FontSize=\"{textObj.Font.Size:F0}\" " +
                                          $"FontWeight=\"{fontWeight}\" FontStyle=\"{fontStyle}\" TextWrapping=\"Wrap\" />");
                            break;

                        case ReportObjectKind.FieldObject:
                            var fieldObj = (FieldObject)obj;
                            // En WPF automatizamos usando Data Binding directo al modelo de datos (MVVM)
                            // Reemplazamos caracteres especiales del origen para tener un binding válido en C#
                            string bindingPath = fieldObj.DataSource.Name.Replace("{", "").Replace("}", "").Replace(".", "_");

                            sb.AppendLine($"                    <TextBox Text=\"{{Binding {bindingPath}, Mode=OneWay}}\" " +
                                          $"Canvas.Left=\"{left:F1}\" Canvas.Top=\"{top:F1}\" " +
                                          $"Width=\"{width:F1}\" Height=\"{height:F1}\" " +
                                          $"IsReadOnly=\"True\" BorderThickness=\"1\" BorderBrush=\"#E2E8F0\" " +
                                          $"ToolTip=\"Campo de Origen: {fieldObj.Name}\" />");
                            break;

                        case ReportObjectKind.LineObject:
                            var lineObj = (LineObject)obj;
                            // En Crystal las líneas tienen coordenadas de inicio y fin propias
                            double endLeft = lineObj.Right / TwipsPerPixel;
                            double endTop = lineObj.Bottom / TwipsPerPixel;

                            sb.AppendLine($"                    <Line X1=\"{left:F1}\" Y1=\"{top:F1}\" X2=\"{endLeft:F1}\" Y2=\"{endTop:F1}\" " +
                                          $"Stroke=\"#64748B\" StrokeThickness=\"1\" />");
                            break;

                        case ReportObjectKind.SubreportObject:
                            var subreportObj = (SubreportObject)obj;
                            // Un subinforme visual es un componente inyectado dinámicamente mediante ContentControl
                            sb.AppendLine($"                    ");
                            sb.AppendLine($"                    <ContentControl Content=\"{{Binding SubreportView_{subreportObj.Name}}}\" " +
                                          $"Canvas.Left=\"{left:F1}\" Canvas.Top=\"{top:F1}\" " +
                                          $"Width=\"{width:F1}\" Height=\"{height:F1}\" " +
                                          $"BorderThickness=\"1\" BorderBrush=\"#3B82F6\" BorderStyle=\"Dash\" />");
                            break;

                        default:
                            // Marcador visual por si el reporte posee imágenes u objetos OLE no soportados directamente por texto
                            sb.AppendLine($"                    ");
                            break;
                    }
                }

                sb.AppendLine("                </Canvas>");
                sb.AppendLine("            </GroupBox>");
            }
        }
    }
}

#endif