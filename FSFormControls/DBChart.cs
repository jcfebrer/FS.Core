#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBChart.bmp")]
    [ToolboxItem(true)]
    public class DBChart : DBUserControl, ISupportInitialize
    {
        #region plotType enum

        public enum plotTypeEnum
        {
            Line = 0,
            Curve = 1,
            Bar = 2,
            Chart = 3,
            Point = 4
        }

        #endregion

        private DataTable _dataTable;

        private bool bInitializedSheet;

        public float FontSize = 8;
        public bool m_ShowChartLegends = true;
        private Font oFont;

        private Graphics oG;
        private Bitmap oGraph;

        private Pen oPen;
        private SolidBrush oSolidBrush;

        public bool ShowBorder = true;
        public bool ShowGridLines = true;

        public plotTypeEnum PlotType { get; set; } = plotTypeEnum.Chart;

        public Color Color { get; set; } = Color.Blue;


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataValue Values { get; set; }


        public bool DisplayUnits { get; set; }

        public bool DisplayBarValue { get; set; }

        public bool ShowChartLegends
        {
            get { return m_ShowChartLegends; }
            set { m_ShowChartLegends = value; }
        }

        public DataTable DataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                Values.Clear();
                if (_dataTable != null)
                    foreach (DataRow row in _dataTable.Rows)
                        Values.Add((int) row[0], (Color) row[1], row[2].ToString());
            }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public void ClearValues()
        {
            Values.Clear();
        }


        public bool DrawPoint(decimal x, decimal y, Color color, float pointSize)
        {
            try
            {
                if (!bInitializedSheet) InitializeGraphSheet();

                float internalX = 0, internalY = 0;
                internalX =
                    Convert.ToSingle((Convert.ToDouble(x) / Values.Xscale_units) *
                                     Values.Xaxis_scale +
                                     Values.MarginX);
                internalY =
                    Convert.ToSingle(
                        (Convert.ToDouble(Values.Yscale_Max - y) / Values.Yscale_units) *
                        Values.Yaxis_scale +
                        Values.MarginY);

                internalX -= pointSize / 2;
                internalY -= pointSize / 2;

                oG.FillEllipse(new SolidBrush(color), internalX, internalY, pointSize, pointSize);
                Invalidate();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DrawPoint(decimal x, decimal y, Color color)
        {
            return DrawPoint(x, y, color, 6);
        }

        public bool DrawGraph()
        {
            try
            {
                bool bMarkPoints = true;

                if (!bInitializedSheet)
                    InitializeGraphSheet();

                if (Values == null)
                    throw new ExceptionUtil("Debes inicializar los valores.");

                if (Values.Count == 0)
                    throw new ExceptionUtil("Debes añadir valores.");

                var p = Values.Points();

                ClearChecks();

                switch (PlotType)
                {
                    case plotTypeEnum.Point:
                        bMarkPoints = true;

                        mnuModePoint.Checked = true;
                        break;
                    case plotTypeEnum.Curve:
                        oG.DrawCurve(new Pen(Color), p);

                        mnuModeCurve.Checked = true;
                        break;
                    case plotTypeEnum.Line:
                        oG.DrawLines(new Pen(Color), p);

                        mnuModeLine.Checked = true;
                        break;
                    case plotTypeEnum.Bar:
                        bMarkPoints = false;
                        DrawBar();

                        mnuModeBar.Checked = true;
                        break;
                    case plotTypeEnum.Chart:
                        bMarkPoints = false;
                        DrawChart();

                        mnuModeChart.Checked = true;
                        break;
                }


                if (bMarkPoints)
                {
                    float internalX = 0, internalY = 0;

                    foreach (Point item in Values.Points())
                    {
                        internalX = item.X;
                        internalY = item.Y;

                        internalX -= 2;
                        internalY -= 2;

                        oG.FillEllipse(new SolidBrush(Color), internalX, internalY, 4, 4);
                    }
                }

                Invalidate();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DrawBar()
        {
            for (int f = 0; f <= Values.Count - 1; f++)
            {
                var r = new Rectangle(Values.Points()[f].X, Values.Points()[f].Y,
                    (int)((pictureMain.Width / Values.Count) / 2),
                    (int)(pictureMain.Height - Values.Points()[f].Y - Values.MarginY));

                oG.FillRectangle(new SolidBrush(Color.Gray), r.X + 5, r.Y + 5, r.Width, r.Height);

                Brush br = new LinearGradientBrush(r, getLightColor(Values[f].Color, 55),
                    getDarkColor(Values[f].Color, 55), LinearGradientMode.ForwardDiagonal);
                
                oG.FillRectangle(br, r);
                oG.DrawRectangle(new Pen(Color.Black), r);

                if (DisplayBarValue)
                    oG.DrawString(Values[f].Value.ToString(), new Font("Verdana", FontSize, FontStyle.Regular),
                        new SolidBrush(Color.Black), r.X, r.Y + 10,
                        new StringFormat(StringFormatFlags.DirectionVertical));
            }
        }


        public void Save(string fileName, ImageFormat format)
        {
            oGraph.Save(fileName, format);
        }


        public bool InitializeGraphSheet()
        {
            double XPoints = 0, YPoints = 0;

            try
            {
                oGraph = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                oG = Graphics.FromImage(oGraph);

                oG.SmoothingMode = SmoothingMode.AntiAlias;

                oG.Clear(Color.White);

                oPen = new Pen(Color.Black);
                oFont = new Font("Verdana", 7, FontStyle.Regular);
                oSolidBrush = new SolidBrush(Color.Black);

                pictureMain.Left = 0;
                pictureMain.Top = 0;
                pictureMain.Width = Width;
                pictureMain.Height = Height;

                XPoints = (Values.Xscale_Max / Values.Xscale_units);
                YPoints = (Values.Yscale_Max / Values.Yscale_units);

                Values.Xaxis_scale = (pictureMain.Width - Values.MarginX) / XPoints;
                Values.Yaxis_scale = (pictureMain.Height - Values.MarginY) / YPoints;

                if (ShowBorder) pictureMain.BorderStyle = BorderStyle.FixedSingle;

                if (ShowGridLines)
                {
                    float tempX = 0, tempY = 0;
                    var br = new Pen(new SolidBrush(Color.LightSkyBlue));

                    for (int iTemp = 0; iTemp <= XPoints; iTemp++)
                    {
                        oG.DrawLine(br, (float)(iTemp * Values.Xaxis_scale + Values.MarginX),
                            pictureMain.Height - Values.MarginY,
                            (float)(iTemp * Values.Xaxis_scale + Values.MarginX), 
                            0);

                        tempX = (float)(iTemp * Values.Xaxis_scale + Values.MarginX);
                        tempY = pictureMain.Height + Values.MarginY;
                        if (DisplayUnits)
                            oG.DrawString(Convert.ToString(iTemp * Values.Xscale_units),
                                new Font("Verdana", FontSize, FontStyle.Regular), new SolidBrush(Color.Black),
                                tempX, tempY);
                    }

                    for (int iTemp = 0; iTemp <= YPoints; iTemp++)
                    {
                        oG.DrawLine(br, 0 + Values.MarginX, 
                            Convert.ToSingle(iTemp * Values.Yaxis_scale + Values.MarginY),
                            pictureMain.Width - Values.MarginY, 
                            (float)(iTemp * Values.Yaxis_scale + Values.MarginY));

                        tempX = 0 + Values.MarginX;
                        tempY = (float)(iTemp * Values.Yaxis_scale + Values.MarginY);
                        if (DisplayUnits)
                            oG.DrawString(Convert.ToString(Values.Yscale_Max - iTemp * Values.Yscale_units),
                                new Font("Verdana", FontSize, FontStyle.Regular), new SolidBrush(Color.Black),
                                tempX, tempY);
                    }
                }

                oG.Flush();

                Invalidate();
            }
            catch (Exception)
            {
                return false;
            }

            bInitializedSheet = true;
            return true;
        }


        private void GraphSheet_Paint(object sender, PaintEventArgs e)
        {
            if (oGraph != null)
            {
                lblDesc.Visible = false;
                pictureMain.Image = oGraph;
                pictureMain.Visible = true;
            }
        }


        private void DBChart_Resize(object sender, EventArgs e)
        {
            if (oGraph == null)
            {
                lblDesc.Left = 0;
                lblDesc.Top = 0;
                lblDesc.Width = Width;
                lblDesc.Height = Height;
                lblDesc.Text = "DBChart Control - " + "\r\n" + "\r\n" + "Gráfico no inicializado o sin datos";
                lblDesc.TextAlign = ContentAlignment.MiddleCenter;
                lblDesc.Visible = true;
            }
            else
            {
                bInitializedSheet = false;
                DrawGraph();
            }
        }


        private Color getLightColor(Color c, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (Convert.ToInt32(c.R) + Convert.ToInt32(d) <= 255) r = Convert.ToByte(c.R + d);
            if (Convert.ToInt32(c.G) + Convert.ToInt32(d) <= 255) g = Convert.ToByte(c.G + d);
            if (Convert.ToInt32(c.B) + Convert.ToInt32(d) <= 255) b = Convert.ToByte(c.B + d);

            var c2 = Color.FromArgb(r, g, b);
            return c2;
        }


        private Color getDarkColor(Color c, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if (c.R > d) r = Convert.ToByte(c.R - d);
            if (c.G > d) g = Convert.ToByte(c.G - d);
            if (c.B > d) b = Convert.ToByte(c.B - d);

            var c1 = Color.FromArgb(r, g, b);
            return c1;
        }


        private void DrawChart()
        {
            InitializeChart();

            Brush mybrush = null;
            var elements = Values.Count;

            var rect = new Rectangle();
            if (ShowChartLegends)
                rect = new Rectangle(10, 10, Convert.ToInt32(Width / 2), Convert.ToInt32(Height / 2));
            else
                rect = new Rectangle(10, 10, Width - 20, Height - 30);

            var myfont = new Font("Verdana", 8, FontStyle.Regular);
            mybrush = new SolidBrush(Color.Black);
            var imgHeight = Height;


            if (elements * 15 + 40 > imgHeight) imgHeight = elements * 15 + 40;
            var gtemp = Graphics.FromImage(new Bitmap(10, 10));
            var maxNamesWidth = 0;
            var maxValsWidth = 0;

            int imgwidth = 0, tempNamesWidth = 0, tempValsWidth = 0;
            imgwidth = 200;
            for (int i = 0; i <= elements - 1; i++)
            {
                tempValsWidth = Convert.ToInt32(gtemp.MeasureString(Values[i].Value.ToString(), myfont).Width);
                tempNamesWidth = Convert.ToInt32(gtemp.MeasureString(Values[i].Legend, myfont).Width);

                if (tempNamesWidth > maxNamesWidth) maxNamesWidth = tempNamesWidth;
                if (tempValsWidth > maxValsWidth) maxValsWidth = tempValsWidth;
            }

            imgwidth = Width + 20 + 10 + 10 + 60 + maxValsWidth + maxNamesWidth + 5;
            gtemp.Dispose();

            oG.Clear(Color.White);
            var j = Convert.ToInt32(Width / 20);
            var xMovePie = 0;
            var yMovePie = 0;
            while (j > 0)
            {
                for (int i = 0; i <= elements - 1; i++)
                    if (Values[i].MovePie)
                    {
                        var selectVal = Values[i].StartAngle;
                        if (0 <= selectVal && selectVal <= 90)
                        {
                            xMovePie = 5;
                            yMovePie = 5;
                        }
                        else if (90 <= selectVal && selectVal <= 180)
                        {
                            xMovePie = -5;
                            yMovePie = 5;
                        }
                        else if (180 <= selectVal && selectVal <= 270)
                        {
                            xMovePie = -5;
                            yMovePie = -5;
                        }
                        else if (270 <= selectVal && selectVal <= 360)
                        {
                            xMovePie = 5;
                            yMovePie = -5;
                        }


                        oG.FillPie(new HatchBrush(HatchStyle.Percent50, Values[i].Color),
                            new Rectangle(rect.X + xMovePie, rect.Y + j + yMovePie, rect.Width, rect.Height),
                            Values[i].StartAngle, Values[i].Span);
                    }
                    else
                    {
                        oG.FillPie(new HatchBrush(HatchStyle.Percent50, Values[i].Color),
                            new Rectangle(rect.X, rect.Y + j, rect.Width, rect.Height), Values[i].StartAngle,
                            Values[i].Span);
                    }

                j = j - 1;
            }

            for (int i = 0; i <= elements - 1; i++)
            {
                if (Values[i].MovePie)
                {
                    var rc = new Rectangle(rect.X + xMovePie, rect.Y + yMovePie, rect.Width, rect.Height);
                    Brush br = new LinearGradientBrush(rc, getLightColor(Values[i].Color, 55),
                        getDarkColor(Values[i].Color, 55),
                        LinearGradientMode.ForwardDiagonal);
                    oG.FillPie(br, rc, Values[i].StartAngle, Values[i].Span);
                }
                else
                {
                    Brush br = new LinearGradientBrush(rect, getLightColor(Values[i].Color, 55),
                        getDarkColor(Values[i].Color, 55),
                        LinearGradientMode.ForwardDiagonal);
                    oG.FillPie(br, rect, Values[i].StartAngle, Values[i].Span);
                }

                if (ShowChartLegends)
                {
                    oG.DrawString("" + Values[i].Percent + "%", myfont, new SolidBrush(Color.Blue),
                        Convert.ToSingle(Width / 2 + 35 + maxValsWidth + maxNamesWidth), i * 15 + 10);
                    oG.DrawString(Values[i].Value.ToString(), myfont, mybrush, Width / 2 + 35, i * 15 + 10);
                    oG.DrawString(Values[i].Legend, myfont, mybrush, Convert.ToSingle(Width / 2 + 35 + maxValsWidth),
                        i * 15 + 10);
                    var recLegend = new Rectangle(Convert.ToInt32(Width / 2 + 20), i * 15 + 10, 10, 10);
                    Brush br2 = new LinearGradientBrush(recLegend, getLightColor(Values[i].Color, 55),
                        getDarkColor(Values[i].Color, 55),
                        LinearGradientMode.ForwardDiagonal);
                    oG.FillRectangle(br2, recLegend);
                    oG.DrawRectangle(new Pen(Color.Black), recLegend);
                }
            }
        }


        private void InitializeChart()
        {
            var arrsize = Values.Count;

            var i = 0;
            float totalval = 0;
            float total = 0;
            for (i = 0; i <= arrsize - 1; i++) 
                totalval = totalval + Values[i].Value;

            for (i = 0; i <= arrsize - 1; i++)
            {
                Values[i].StartAngle = total;
                Values[i].Span = Values[i].Value * 360 / totalval;
                Values[i].Percent =
                    Convert.ToSingle(Convert.ToDouble(Convert.ToInt32(Values[i].Value * 10000 / totalval)) / 100);
                total = total + Values[i].Value * 360 / totalval;
            }
        }


        private void mnuSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.Filter =
                    "JPEG (*.jpg)|*.jpg|Mapa de bits (*.bmp)|*.bmp|GIF (*.gif)|*.gif|TIFF (*.tiff)|*.tiff|WMF (*.wmf)|*.wmf|PNG (*.png)|*.png|EMF (*.emf)|*.emf|Todos los archivos (*.*)|*.*";
                SaveFileDialog1.ShowDialog();
                var file = SaveFileDialog1.FileName;

                var imFormat = ImageFormat.Jpeg;
                switch (file.Substring(file.Length - 3, 3).ToLower())
                {
                    case "jpg":
                        imFormat = ImageFormat.Jpeg;
                        break;
                    case "bmp":
                        imFormat = ImageFormat.Bmp;
                        break;
                    case "gif":
                        imFormat = ImageFormat.Gif;
                        break;
                    case "iff":
                        imFormat = ImageFormat.Tiff;
                        break;
                    case "emf":
                        imFormat = ImageFormat.Emf;
                        break;
                    case "wmf":
                        imFormat = ImageFormat.Wmf;
                        break;
                    case "png":
                        imFormat = ImageFormat.Png;
                        break;
                }

                Save(file, imFormat);
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil("Imposible guardar imagen.", ex);
            }
        }


        private void mnuModeBar_Click(object sender, EventArgs e)
        {
            bInitializedSheet = false;
            PlotType = plotTypeEnum.Bar;
            Color = Color.Blue;
            DrawGraph();

            ClearChecks();

            mnuModeBar.Checked = true;
        }


        private void mnuModeLine_Click(object sender, EventArgs e)
        {
            bInitializedSheet = false;
            PlotType = plotTypeEnum.Line;
            Color = Color.Blue;
            DrawGraph();

            ClearChecks();

            mnuModeLine.Checked = true;
        }


        private void mnuModePoint_Click(object sender, EventArgs e)
        {
            bInitializedSheet = false;
            PlotType = plotTypeEnum.Point;
            Color = Color.Blue;
            DrawGraph();

            ClearChecks();

            mnuModePoint.Checked = true;
        }


        private void mnuModeChart_Click(object sender, EventArgs e)
        {
            bInitializedSheet = false;
            PlotType = plotTypeEnum.Chart;
            Color = Color.Blue;
            DrawGraph();

            ClearChecks();

            mnuModeChart.Checked = true;
        }


        private void mnuModeCurve_Click(object sender, EventArgs e)
        {
            bInitializedSheet = false;
            PlotType = plotTypeEnum.Curve;
            Color = Color.Blue;
            DrawGraph();

            ClearChecks();

            mnuModeCurve.Checked = true;
        }


        private void ClearChecks()
        {
            mnuModeBar.Checked = false;
            mnuModeCurve.Checked = false;
            mnuModeLine.Checked = false;
            mnuModeChart.Checked = false;
            mnuModePoint.Checked = false;
        }


        private void pictureMain_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }


        private void pictureMain_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region '" Windows Form Designer generated code "' 

        private readonly IContainer components = null;

        internal ContextMenu ContextMenu1;
        internal MenuItem MenuItem2;
        internal SaveFileDialog SaveFileDialog1;
        internal Label lblDesc;
        internal MenuItem mnuModeBar;
        internal MenuItem mnuModeChart;
        internal MenuItem mnuModeCurve;
        internal MenuItem mnuModeLine;
        internal MenuItem mnuModePoint;
        internal MenuItem mnuSave;
        internal PictureBox pictureMain;

        public DBChart()
        {
            InitializeComponent();

            Paint += GraphSheet_Paint;
            Resize += DBChart_Resize;
            mnuSave.Click += mnuSave_Click;
            mnuModeBar.Click += mnuModeBar_Click;
            mnuModeLine.Click += mnuModeLine_Click;
            mnuModePoint.Click += mnuModePoint_Click;
            mnuModeChart.Click += mnuModeChart_Click;
            mnuModeCurve.Click += mnuModeCurve_Click;
            pictureMain.MouseDown += pictureMain_MouseDown;
            pictureMain.MouseUp += pictureMain_MouseUp;

            if (Values == null) Values = new DataValue();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            lblDesc = new Label();
            pictureMain = new PictureBox();
            ContextMenu1 = new ContextMenu();
            mnuSave = new MenuItem();
            MenuItem2 = new MenuItem();
            mnuModePoint = new MenuItem();
            mnuModeLine = new MenuItem();
            mnuModeCurve = new MenuItem();
            mnuModeBar = new MenuItem();
            mnuModeChart = new MenuItem();
            SaveFileDialog1 = new SaveFileDialog();
            ((ISupportInitialize) pictureMain).BeginInit();
            SuspendLayout();
            // 
            // lblDesc
            // 
            lblDesc.BorderStyle = BorderStyle.FixedSingle;
            lblDesc.Location = new Point(8, 104);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(100, 23);
            lblDesc.TabIndex = 1;
            lblDesc.Visible = false;
            // 
            // pictureMain
            // 
            pictureMain.ContextMenu = ContextMenu1;
            pictureMain.Location = new Point(8, 8);
            pictureMain.Name = "pictureMain";
            pictureMain.Size = new Size(100, 50);
            pictureMain.TabIndex = 2;
            pictureMain.TabStop = false;
            pictureMain.Visible = false;
            // 
            // ContextMenu1
            // 
            ContextMenu1.MenuItems.AddRange(new[]
            {
                mnuSave,
                MenuItem2
            });
            // 
            // mnuSave
            // 
            mnuSave.Index = 0;
            mnuSave.Text = "Guardar Imagen";
            // 
            // MenuItem2
            // 
            MenuItem2.Index = 1;
            MenuItem2.MenuItems.AddRange(new[]
            {
                mnuModePoint,
                mnuModeLine,
                mnuModeCurve,
                mnuModeBar,
                mnuModeChart
            });
            MenuItem2.Text = "Modo";
            // 
            // mnuModePoint
            // 
            mnuModePoint.Checked = true;
            mnuModePoint.Index = 0;
            mnuModePoint.RadioCheck = true;
            mnuModePoint.Text = "Punto";
            // 
            // mnuModeLine
            // 
            mnuModeLine.Index = 1;
            mnuModeLine.RadioCheck = true;
            mnuModeLine.Text = "Linea";
            // 
            // mnuModeCurve
            // 
            mnuModeCurve.Index = 2;
            mnuModeCurve.RadioCheck = true;
            mnuModeCurve.Text = "Curva";
            // 
            // mnuModeBar
            // 
            mnuModeBar.Index = 3;
            mnuModeBar.RadioCheck = true;
            mnuModeBar.Text = "Barra";
            // 
            // mnuModeChart
            // 
            mnuModeChart.Index = 4;
            mnuModeChart.RadioCheck = true;
            mnuModeChart.Text = "Tarta";
            // 
            // DBChart
            // 
            Controls.Add(pictureMain);
            Controls.Add(lblDesc);
            Name = "DBChart";
            Size = new Size(174, 144);
            ((ISupportInitialize) pictureMain).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }


    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class ChartValue : Component
    {
        public Color m_Color = Color.Blue;
        public string m_Legend = "";
        public bool m_MovePie;

        public float m_Percent;
        public float m_Span;
        public float m_StartAngle;
        public int m_Value;

        public ChartValue()
        {
        }

        public ChartValue(int value)
        {
            m_Value = value;
        }

        public ChartValue(int value, Color color)
        {
            m_Value = value;
            m_Color = color;
        }

        public ChartValue(int value, Color color, string legend)
        {
            m_Value = value;
            m_Color = color;
            m_Legend = legend;
        }

        public ChartValue(int value, string legend)
        {
            m_Value = value;
            m_Legend = legend;
        }

        public int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public string Legend
        {
            get { return m_Legend; }
            set { m_Legend = value; }
        }

        public bool MovePie
        {
            get { return m_MovePie; }
            set { m_MovePie = value; }
        }

        public float Percent
        {
            get { return m_Percent; }
            set { m_Percent = value; }
        }

        public float Span
        {
            get { return m_Span; }
            set { m_Span = value; }
        }

        public float StartAngle
        {
            get { return m_StartAngle; }
            set { m_StartAngle = value; }
        }
    }


    public class DataValue : CollectionBase
    {
        public int MarginX;
        public int MarginY;
        public double Xaxis_scale;
        public long Xscale_Max = 10;
        public int Xscale_units = 1;
        public double Yaxis_scale;
        public long Yscale_Max = 10;
        public int Yscale_units = 1;

        public ChartValue this[int index]
        {
            get { return (ChartValue) List[index]; }
            set { List[index] = value; }
        }

        public void Add()
        {
            List.Add(new ChartValue());
        }


        public void Add(ChartValue cV)
        {
            List.Add(cV);
        }


        public void Add(int value)
        {
            List.Add(new ChartValue(value));
        }


        public void Add(int value, Color color)
        {
            List.Add(new ChartValue(value, color));
        }


        public void Add(int value, Color color, string legend)
        {
            List.Add(new ChartValue(value, color, legend));
        }


        public void Add(int value, string legend)
        {
            List.Add(new ChartValue(value, legend));
        }


        public void AddRange(ChartValue[] Values)
        {
            var f = 0;
            for (f = 0; f <= Values.Length - 1; f++) List.Add(Values[f]);
        }


        public void Remove(ChartValue Value)
        {
            List.Remove(Value);
        }


        public void Insert(int index, ChartValue Value)
        {
            List.Insert(index, Value);
        }


        public bool Contains(ChartValue Value)
        {
            return List.Contains(Value);
        }


        public int IndexOf(ChartValue Value)
        {
            return List.IndexOf(Value);
        }


        public Point[] Points()
        {
            Point[] p = new Point[List.Count];
            for (int f = 0; f < List.Count; f++)
            {
                p[f].X = (int)(f * Xaxis_scale + MarginX);
                p[f].Y = (int)(((Yscale_Max - ((ChartValue)List[f]).Value) / Yscale_units) * Yaxis_scale);
            }

            return p;
        }
    }
}