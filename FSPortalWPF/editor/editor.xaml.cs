using System.IO;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO.Packaging;
using System.Printing;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Xps;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace FSPortalWPF
{
	public partial class editor
	{
        int updatingUI = 0;
        Boolean spellcheck = false;

		public editor()
		{
            InitializeComponent();
            foreach (FontFamily family in System.Windows.Media.Fonts.SystemFontFamilies)
            {
                cb.Items.Add(family.Source);
            }

            for (int i = 0; i < AvailableFontSizes.Length; i++)
            {
                cbsize.Items.Add(AvailableFontSizes[i]);
            }

            for (int i = 0; i < fontColors.Length; i++)
            {
                SolidColorBrush bx = new SolidColorBrush(fontColors[i]);
                Rectangle colorRect = new Rectangle();
                colorRect.Margin = new Thickness(1, 1, 1, 1);
                colorRect.Width = 25;
                colorRect.Height = 10;
                colorRect.Fill = bx;
                colorRect.Stroke = Brushes.DarkGray;
                colorRect.StrokeThickness = 1;
                cbcolor.Items.Add(colorRect);
            }


            mainRichTextBox.Document.PagePadding = new Thickness(12);
            mainRichTextBox.Focus();


            TextSelectionChanged(null, null);
		}

        void LoadFile(Object sender, RoutedEventArgs args)
        {
            FlowDocument flowdoc = null;

            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.Filter = "FlowDocument Files (*.xaml)|*.xaml|All Files (*.*)|*.*";
            openFile.Filter = "Ficheros FlowDocument (*.xaml)|*.xaml|Ficheros de Texto - Unicode (*.txt)|*.txt|Todos los ficheros (*.*)|*.*";
            
            if (openFile.ShowDialog() == true)
            {

                FileStream xamlFile = openFile.OpenFile() as FileStream;
                if (xamlFile == null) return;
                else
                {
                    try
                    {
                        if (openFile.FilterIndex == 2)
                        {
                            //Load text Text
                            using (StreamReader streamReader = new StreamReader(xamlFile))
                            {
                                //Paragraph paragraph = new Paragraph();
                                String textdata = streamReader.ReadToEnd();
                                Run run = new Run(textdata);
                                mainRichTextBox.Document.Blocks.Clear();
                                mainRichTextBox.Document.Blocks.Add(new Paragraph(run));

                            }

                        }
                        else
                        {
                            flowdoc = XamlReader.Load(xamlFile) as FlowDocument;
                            if (flowdoc == null)
                                throw (new XamlParseException("Imposible cargar FlowDocument."));

                            mainRichTextBox.Document = flowdoc;

                        }

                    }
                    catch (Exception e)
                    {

                        System.Windows.MessageBox.Show(e.Message);
                        return;
                    }


                }

            }

        }
        void Clear(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Document = new FlowDocument();
        }

        void InsertImage(Object sender, RoutedEventArgs args)
        {
            modInputMessage m = new FSPortalWPF.modInputMessage("Indica la dirección URL de la imagen a insertar", "Insertar imagen");
            if (m.ShowDialog() == true)
            {

                Paragraph para = new Paragraph();

                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(m.respuesta));
                    Image image = new Image();
                    image.Width = bitmap.Width;
                    image.Height = bitmap.Height;
                    image.Source = bitmap;
                    para.Inlines.Add(image);

                    mainRichTextBox.Document.Blocks.Add(para);
                }
                catch
                {
                    MessageBox.Show("Imposible insertar imagen en el documento", "Insertar imagen", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        void SaveFile(Object sender, RoutedEventArgs args)
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            FileStream xamlFile = null;
            //saveFile.Filter = "FlowDocument Files (*.xaml)|*.xaml|All Files (*.*)|*.*";
            saveFile.Filter = "Ficheros FlowDocument (*.xaml)|*.xaml|Ficheros de Texto - Unicode (*.txt)|*.txt|Todos los ficheros (*.*)|*.*";
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    xamlFile = saveFile.OpenFile() as FileStream;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                    return;
                }
                if (xamlFile == null) return;
                else
                {
                    if (saveFile.FilterIndex == 2)
                    {
                        //Save as Text
                        //The bullets in unicode text file may appear as a few characters when opened with a normal editor 
                        FlowDocument flowDoc = mainRichTextBox.Document;
                        TextPointer contentstart = flowDoc.ContentStart;
                        TextPointer contentend = flowDoc.ContentEnd;
                        TextRange tr = new TextRange(contentstart, contentend);
                        tr.Save(xamlFile, System.Windows.DataFormats.Text);

                    }
                    else
                    {
                        //Save as XAML
                        XamlWriter.Save(mainRichTextBox.Document, xamlFile);
                        xamlFile.Close();

                    }


                }



            }

        }
        void SaveOpenXML(Object sender, RoutedEventArgs args)
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            FileStream xamlFile = null;
            saveFile.Filter = "Ficheros OpenXML (*.docx)|*.docx|Todos los ficheros (*.*)|*.*";
            if (saveFile.ShowDialog() == true)
            {
                try
                {
                    xamlFile = saveFile.OpenFile() as FileStream;
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show(e.Message);
                    return;
                }
                if (xamlFile == null) return;
                else
                {
                    FlowDocument flowDoc = mainRichTextBox.Document;
                    FlowDocToDocx(xamlFile, flowDoc);

                }
            }



        }

        private void FlowDocToDocx(FileStream xamlFile, FlowDocument flowDoc)
        {
            TextPointer contentstart = flowDoc.ContentStart;
            TextPointer contentend = flowDoc.ContentEnd;
            if (contentstart == null)
            {
                throw new ArgumentNullException("ContentStart");
            }
            if (contentend == null)
            {
                throw new ArgumentNullException("ContentEnd");
            }

            //Create document

            // document package container
            Package zippackage = null;
            zippackage = Package.Open(xamlFile, FileMode.Create, FileAccess.ReadWrite);

            // main document.xml 
            Uri uri = new Uri("/word/document.xml", UriKind.Relative);
            PackagePart partDocumentXML = zippackage.CreatePart(uri, "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml");


            //ver 1.2 Numbering
            Uri uriNumbering = new Uri("/word/numbering.xml", UriKind.Relative);
            Uri uriNumberingRelationship = new Uri("numbering.xml", UriKind.Relative);
            PackagePart partNumberingXML = zippackage.CreatePart(uriNumbering, "application/vnd.openxmlformats-officedocument.wordprocessingml.numbering+xml");
            partDocumentXML.CreateRelationship(uriNumberingRelationship, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/numbering", "rId1");



            using (XmlTextWriter openxmlwriter = new XmlTextWriter(partDocumentXML.GetStream(FileMode.Create, FileAccess.Write), System.Text.Encoding.UTF8))
            {
                openxmlwriter.Formatting = Formatting.Indented;
                openxmlwriter.Indentation = 2;
                openxmlwriter.IndentChar = ' ';

                //ver 1.2 
                using (XmlTextWriter numberingwriter = new XmlTextWriter(partNumberingXML.GetStream(FileMode.Create, FileAccess.Write), System.Text.Encoding.UTF8))
                {
                    numberingwriter.Formatting = Formatting.Indented;
                    numberingwriter.Indentation = 2;
                    numberingwriter.IndentChar = ' ';

                    //Actual Writing
                    new OpenXmlWriter().Write(contentstart, contentend, openxmlwriter, numberingwriter);
                }

            }



            zippackage.Flush();

            // relationship 
            zippackage.CreateRelationship(uri, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", "rId1");
            zippackage.Flush();
            zippackage.Close();
        }

        void PrintContent(Object sender, RoutedEventArgs args)
        {
            //Printing with Paginator

            //set pagination with printer's margins and size
            PrintDocumentImageableArea area = null;
            XpsDocumentWriter xdwriter = PrintQueue.CreateXpsDocumentWriter(ref area);

            //make a copy to print with pagginator w/o crashing
            TextPointer position1 = mainRichTextBox.Document.ContentStart;
            TextPointer position2 = mainRichTextBox.Document.ContentEnd;
            TextRange sourceDocumentRange = new TextRange(position1, position2);

            MemoryStream tempstream = new MemoryStream();
            sourceDocumentRange.Save(tempstream, System.Windows.DataFormats.Xaml);

            FlowDocument sourceDocumentCopy = new FlowDocument();
            TextPointer position3 = sourceDocumentCopy.ContentStart;
            TextPointer position4 = sourceDocumentCopy.ContentEnd;
            TextRange copyDocumentRange = new TextRange(position3, position4);
            copyDocumentRange.Load(tempstream, System.Windows.DataFormats.Xaml);

            if ((xdwriter != null) && (area != null))
            {
                DocumentPaginator paginator = ((IDocumentPaginatorSource)sourceDocumentCopy).DocumentPaginator;
                paginator.PageSize = new Size(area.MediaSizeWidth, area.MediaSizeHeight);

                Thickness pageBounds = sourceDocumentCopy.PagePadding;

                double leftmargin, topmargin, rightmargin, bottommargin;
                if (area.OriginWidth > pageBounds.Left)
                    leftmargin = area.OriginWidth;
                else
                    leftmargin = pageBounds.Left;

                if (area.OriginHeight > pageBounds.Top)
                    topmargin = area.OriginHeight;
                else
                    topmargin = pageBounds.Top;

                double printerRightMargin = area.MediaSizeWidth - (area.OriginWidth + area.ExtentWidth);
                if (printerRightMargin > pageBounds.Right)
                    rightmargin = printerRightMargin;
                else
                    rightmargin = pageBounds.Right;

                double printerBottomMargin = area.MediaSizeHeight - (area.OriginHeight + area.ExtentHeight);
                if (printerBottomMargin > pageBounds.Bottom)
                    bottommargin = printerBottomMargin;
                else
                    bottommargin = pageBounds.Bottom;

                sourceDocumentCopy.PagePadding = new Thickness(leftmargin, topmargin, rightmargin, bottommargin);

                //can be used to set columns
                sourceDocumentCopy.ColumnWidth = double.PositiveInfinity;

                xdwriter.Write(paginator);
            }

        }
        double PointsToPixels(double value)
        {
            return value * 96.0d / 72.0d;
        }

        double PixelsToPoints(double value)
        {
            return Math.Round(value * 72.0 / 96.0);
        }

        void DoCut(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Cut();


        }


        void DoUndo(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Undo();

        }

        void DoRedo(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Redo();

        }
        void DoPaste(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Paste();

        }

        void DoCopy(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.Copy();

        }

        void DoSelectAll(Object sender, RoutedEventArgs args)
        {
            mainRichTextBox.SelectAll();

        }

        
        void ToggleSpellCheck(Object sender, RoutedEventArgs args)
        {
            if (spellcheck)
            {
                spellcheck = false;


            }
            else
            {
                spellcheck = true;


            }


            mainRichTextBox.SpellCheck.IsEnabled = spellcheck;
            SpellCheckMenuItem.IsChecked = spellcheck;


        }


        void FontChanged(Object sender, RoutedEventArgs args)
        {
            if (updatingUI == 1)
                return;

            TextRange range = mainRichTextBox.Selection;
            range.ApplyPropertyValue(TextElement.FontFamilyProperty, cb.SelectedValue.ToString());

            mainRichTextBox.Focus();
        }

        void FontSizeChanged(Object sender, RoutedEventArgs args)
        {

            if (updatingUI == 1)
                return;

            TextRange range = mainRichTextBox.Selection;
            double chosenSize = 8;

            if (double.TryParse(cbsize.SelectedValue.ToString(), out chosenSize))
            {
                range.ApplyPropertyValue(TextElement.FontSizeProperty, PointsToPixels(chosenSize));
                //range.ApplyPropertyValue(TextElement.FontSizeProperty, chosenSize);
            }

            mainRichTextBox.Focus();


        }
        void FontColorChanged(Object sender, RoutedEventArgs args)
        {
            if (updatingUI == 1)
                return;

            TextRange range = mainRichTextBox.Selection;
            int selIndex = cbcolor.SelectedIndex;
            SolidColorBrush bx = new SolidColorBrush(fontColors[selIndex]);
            range.ApplyPropertyValue(TextElement.ForegroundProperty, bx);

            mainRichTextBox.Focus();

        }
        void TextSelectionChanged(Object sender, RoutedEventArgs args)
        {
            updatingUI = 1;

            TextRange range = mainRichTextBox.Selection;
            object fontFamily = range.GetPropertyValue(TextElement.FontFamilyProperty);

            if (cb.Items.Contains(fontFamily.ToString()))
            {
                cb.SelectedItem = fontFamily.ToString();
            }
            else
                cb.SelectedValue = "";

            object fontSize = range.GetPropertyValue(TextElement.FontSizeProperty);
            double fs = 8;
            if (double.TryParse(fontSize.ToString(), out fs))
            {
                fs = PixelsToPoints(fs);
                if (cbsize.Items.Contains(fs))
                {
                    cbsize.SelectedItem = fs;
                }
                else
                    cbsize.SelectedValue = fs;

            }


            object fontColor = range.GetPropertyValue(TextElement.ForegroundProperty);

            SolidColorBrush px = null;
            try
            {
                px = fontColor as SolidColorBrush;
                if (px != null)
                {
                    Color cz = px.Color;

                    int found = -1;
                    for (int i = 0; i < fontColors.Length; i++)
                    {
                        if (fontColors[i] == cz)
                        {
                            found = i;
                            break;
                        }
                    }

                    if (found >= 0)
                        cbcolor.SelectedIndex = found;
                    else
                        cbcolor.SelectedValue = "";

                }
            }
            catch (Exception e)
            {
                string strx = e.ToString();

            }

            updatingUI = 0;


        }


        private static double[] AvailableFontSizes = new double[] {
                3.0d,    4.0d,    5.0d,    6.0d,   7.0d,   8.0d,   
                9.0d,    10.0d,   11.0d,   12.0d,   13.0d,
                14.0d,   15.0d,  16.0d,  17.0d,  18.0d,
                19.0d,  20.0d,  21.0d,  22.0d,  24.0d,
                25.0d,  26.0d,  28.0d,  30.0d,  32.0d,
                34.0d,  35.0d,  36.0d,  38.0d,  40.0d,  42.0d,  44.0d,  45.0d,  46.0d,  48.0d,
                50.0d,  52.0d,  56.0d,  60.0d,  62.0d, 64.0d,  66.0d, 68.0d, 70.0d, 72.0d, 74.0d, 76.0d,
                78.0d,  80.0d,  82.0d,  84.0d, 86.0d, 88.0d, 90.0d, 92.0d, 94.0d, 96.0d, 100.0d,  102.0d, 
                104.0d, 106,0d, 108.0d, 112.0d, 120.0d, 124.0d, 128.0d,132.0d, 136.0d,140.0d, 144.0d, 152.0d,
               160.0d, 168.0d, 176.0d, 180.0d, 186.0d, 192.0d, 1960.0d, 200.0d, 204.0d, 208.0d,212.0d, 216.0d, 220.0d, 224.0d, 232.0d, 240.0d, 250.0d, 260.0d, 270.0d, 280.0d, 290.0d,
               300.0d, 320.0d, 360.0d, 384.0d, 400.0d, 420.0d, 448.0d, 460.0d, 480.0d, 500.0d, 512.0d, 576.0d, 600.0d,  640.0d   };

        Color[] fontColors = {                
                Colors.Black,  
                Colors.White,                                                       
                Colors.Blue,  
                Colors.Red, 
                Colors.Green,
                Colors.Yellow, 
                Colors.Purple,                
                Colors.Brown,        
                Colors.Gray,  
                Colors.DarkGray,
                Colors.DarkBlue,                                
                Colors.DarkGreen,                
                Colors.DarkMagenta,
                Colors.DarkOliveGreen,
                Colors.DarkOrange,
                Colors.DarkOrchid,
                Colors.DarkRed,                
                Colors.DarkTurquoise,                
                Colors.Gold,                
                Colors.Cyan,
                Colors.Violet,                             
                Colors.Aqua,                
                Colors.Beige,               
                Colors.GreenYellow,                
                Colors.Indigo,
                Colors.Ivory,                
                Colors.LightBlue,                
                Colors.Lime,
                Colors.Magenta,
                Colors.Maroon,
                Colors.MediumBlue,
                Colors.Navy, 
                Colors.Olive,                
                Colors.Orange,
                Colors.OrangeRed,                
                Colors.Pink,                                                            
                Colors.Tan,
                Colors.Teal,                
                Colors.Turquoise,                             
                Colors.YellowGreen
                };

	}
}