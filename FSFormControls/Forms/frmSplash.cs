#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

#endregion

namespace FSFormControls
{
    public class SplashScreen : Form
    {
        private const int TIMER_INTERVAL = 50;
        private const string REG_KEY_INITIALIZATION = "Initialization";
        private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
        private const string REGVALUE_PB_PERCENTS = "Percents";
        public static SplashScreen ms_frmSplash;
        public static Thread ms_oThread;
        private readonly ArrayList m_alActualTimes = new ArrayList();
        private IContainer components;
        private Label lblStatus;
        private Label lblTimeRemaining;
        private ArrayList m_alPreviousCompletionFraction;
        private bool m_bDTSet;
        private bool m_bFirstLaunch;

        private double m_dblCompletionFraction;

        private double m_dblLastCompletionFraction;
        private readonly double m_dblOpacityDecrement = 0.08;
        private double m_dblOpacityIncrement = 0.05;
        private double m_dblPBIncrementPerTimerInterval = 0.015;

        private DateTime m_dtStart;
        private int m_iActualTicks;
        private readonly int m_iIndex = 1;
        private Rectangle m_rProgress;
        private string m_sStatus;
        private Panel pnlStatus;
        private Timer timer1;

        public SplashScreen()
        {
            InitializeComponent();
            Opacity = 0.0;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            ClientSize = BackgroundImage.Size;

            timer1.Tick += timer1_Tick;
            pnlStatus.Paint += pnlStatus_Paint;
            DoubleClick += SplashScreen_DoubleClick;
            DbTextBox1.KeyPress += DbTextBox1_KeyPress;
        }

        public SplashScreen SplashForm => ms_frmSplash;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        public void ShowSplashScreen()
        {
            if (ms_frmSplash != null) return;
            ms_oThread = new Thread(ms_frmSplash.ShowForm);
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            ms_oThread.Start();
        }


        public void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }


        public void CloseForm()
        {
            if (!(ms_frmSplash == null) & (ms_frmSplash.IsDisposed == false))
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            ms_oThread = null;
            ms_frmSplash = null;
        }


        public void SetStatus(string NewStatus)
        {
            if (ms_frmSplash == null) return;
            ms_frmSplash.m_sStatus = NewStatus;
            ms_frmSplash.SetReferenceInternal();
        }


        public void SetReferencePoint()
        {
            if (ms_frmSplash == null) return;
            ms_frmSplash.SetReferenceInternal();
        }


        private void SetReferenceInternal()
        {
            if (m_bDTSet == false)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }

            var dblMilliseconds = ElapsedMilliSeconds();
            m_alActualTimes.Add(dblMilliseconds);
            m_dblLastCompletionFraction = m_dblCompletionFraction;
            if (!(m_alPreviousCompletionFraction == null) & (m_iIndex < m_alPreviousCompletionFraction.Count))
                m_dblCompletionFraction = Convert.ToDouble(m_alPreviousCompletionFraction[m_iIndex + 1]);
            else
                m_dblCompletionFraction = m_iIndex > 0 ? 1 : 0;
        }


        private double ElapsedMilliSeconds()
        {
            var ts = DateTime.Now.Subtract(m_dtStart);
            return ts.TotalMilliseconds;
        }


        private void ReadIncrements()
        {
            var reg = new Registry();

            var sPBIncrementPerTimerInterval = reg.GetStringRegisTryValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
            double dblResult = 0;

            if (double.TryParse(sPBIncrementPerTimerInterval, NumberStyles.Float, NumberFormatInfo.InvariantInfo,
                out dblResult))
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = 0.0015;

            var sPBPreviousPctComplete = reg.GetStringRegisTryValue(REGVALUE_PB_PERCENTS, "");

            if (sPBPreviousPctComplete != "")
            {
                var aTimes = sPBPreviousPctComplete.Split(Convert.ToChar(null));
                m_alPreviousCompletionFraction = new ArrayList();

                var i = 0;
                for (i = 0; i <= aTimes.Length - 1; i += i + 1)
                {
                    double dblVal = 0;
                    if (double.TryParse(aTimes[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out dblVal))
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
                lblTimeRemaining.Text = "";
            }
        }


        private void StoreIncrements()
        {
            var reg = new Registry();

            var sPercent = "";
            var dblElapsedMilliseconds = ElapsedMilliSeconds();
            var i = 0;
            for (i = 0; i <= m_alActualTimes.Count - 1; i += i + 1)
                sPercent += (Convert.ToDouble(m_alActualTimes[i]) / dblElapsedMilliseconds).ToString("0.####") + " ";

            reg.SetStringRegisTryValue(REGVALUE_PB_PERCENTS, sPercent);

            m_dblPBIncrementPerTimerInterval = 1.0 / Convert.ToDouble(m_iActualTicks);
            reg.SetStringRegisTryValue(REGVALUE_PB_MILISECOND_INCREMENT,
                m_dblPBIncrementPerTimerInterval.ToString("#.000000"));
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = m_sStatus;

            if (m_dblOpacityIncrement > 0)
            {
                m_iActualTicks = m_iActualTicks + 1;
                if (Opacity < 1)
                {
                    Opacity += m_dblOpacityIncrement;
                }
                else
                {
                    if (Opacity > 0)
                    {
                        Opacity += m_dblOpacityIncrement;
                    }
                    else
                    {
                        StoreIncrements();
                        Close();
                        Debug.WriteLine("Called this.Close()");
                    }
                }

                if ((m_bFirstLaunch == false) & (m_dblLastCompletionFraction < m_dblCompletionFraction))
                {
                    m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
                    var width = Convert.ToInt32(
                        Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction));
                    var height = pnlStatus.ClientRectangle.Height;
                    var x = pnlStatus.ClientRectangle.X;
                    var y = pnlStatus.ClientRectangle.Y;
                    if ((width > 0) & (height > 0))
                    {
                        m_rProgress = new Rectangle(x, y, width, height);
                        pnlStatus.Invalidate(m_rProgress);
                        var iSecondsLeft = 1 +
                                           Convert.ToInt32(TIMER_INTERVAL *
                                                           ((1.0 - m_dblLastCompletionFraction) /
                                                            m_dblPBIncrementPerTimerInterval) / 1000);
                        if (iSecondsLeft == 1)
                            lblTimeRemaining.Text = string.Format("1 second remaining", null);
                        else
                            lblTimeRemaining.Text = string.Format("{0} seconds remaining", iSecondsLeft);
                    }
                }
            }
        }


        private void pnlStatus_Paint(object sender, PaintEventArgs e)
        {
            if ((m_bFirstLaunch == false) & (e.ClipRectangle.Width > 0) & (m_iActualTicks > 1))
            {
                var brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(50, 50, 100),
                    Color.FromArgb(150, 150, 255),
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brBackground, m_rProgress);
            }
        }


        private void SplashScreen_DoubleClick(object sender, EventArgs e)
        {
            CloseForm();
        }


        private void DbTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        #region '"Windows Form Designer generated code"' 

        internal DBTextBox DbTextBox1;

        private void InitializeComponent()
        {
            components = new Container();
            var resources = new ComponentResourceManager(typeof(SplashScreen));
            lblStatus = new Label();
            pnlStatus = new Panel();
            lblTimeRemaining = new Label();
            timer1 = new Timer(components);
            DbTextBox1 = new DBTextBox();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Location = new Point(48, 112);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(237, 14);
            lblStatus.TabIndex = 0;
            // 
            // pnlStatus
            // 
            pnlStatus.BackColor = Color.Transparent;
            pnlStatus.Location = new Point(29, 128);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Size = new Size(237, 24);
            pnlStatus.TabIndex = 1;
            // 
            // lblTimeRemaining
            // 
            lblTimeRemaining.BackColor = Color.Transparent;
            lblTimeRemaining.Location = new Point(272, 128);
            lblTimeRemaining.Name = "lblTimeRemaining";
            lblTimeRemaining.Size = new Size(168, 16);
            lblTimeRemaining.TabIndex = 2;
            lblTimeRemaining.Text = "Time remaining";
            // 
            // DbTextBox1
            // 
            DbTextBox1.AsociatedCombo = null;
            DbTextBox1.AsociatedDBFindTextBox = null;
            DbTextBox1.BackColorRead = Color.WhiteSmoke;
            DbTextBox1.BorderStyle = BorderStyle.Fixed3D;
            DbTextBox1.Capitalize = DBTextBox.TypeString.Normal;
            DbTextBox1.DataControl = null;
            DbTextBox1.DataType = DBTextBox.TypeData.All;
            DbTextBox1.DateFormat = "dd/mm/yyyy";
            DbTextBox1.DBField = null;
            DbTextBox1.DBFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DbTextBox1.Decimals = 0;
            DbTextBox1.DefaultValue = "";
            DbTextBox1.DotNumber = false;
            DbTextBox1.Editable = true;
            DbTextBox1.Encrypted = false;
            DbTextBox1.Expression = "";
            DbTextBox1.FormatString = "";
            DbTextBox1.GridOperation = DBColumn.OperationTypes.Sum;
            DbTextBox1.Location = new Point(224, 0);
            DbTextBox1.MaskInput = "";
            DbTextBox1.MaxLength = 32767;
            DbTextBox1.MaxValue = decimal.MaxValue;
            DbTextBox1.Mode = Global.AccessMode.WriteMode;
            DbTextBox1.Multiline = true;
            DbTextBox1.Name = "DbTextBox1";
            DbTextBox1.Obligatory = false;
            DbTextBox1.PasswordChar = '\0';
            DbTextBox1.ReadOnly = false;
            DbTextBox1.ShowScrollBars = ScrollBars.None;
            DbTextBox1.Shadow = true;
            DbTextBox1.ShadowColor = Color.Gray;
            DbTextBox1.ShadowSize = 4;
            DbTextBox1.ShowAsCombo = false;
            DbTextBox1.ShowKeyboard = false;
            DbTextBox1.Size = new Size(216, 40);
            DbTextBox1.TabIndex = 3;
            DbTextBox1.Text = "DbTextBox1";
            DbTextBox1.TextAlign = HorizontalAlignment.Center;
            DbTextBox1.ToolTip = "";
            DbTextBox1.XMLName = null;
            // 
            // SplashScreen
            // 
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = Color.LightGray;
            BackgroundImage = (Image) resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(489, 168);
            Controls.Add(DbTextBox1);
            Controls.Add(lblStatus);
            Controls.Add(lblTimeRemaining);
            Controls.Add(pnlStatus);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            ResumeLayout(false);
        }

        #endregion
    }
}