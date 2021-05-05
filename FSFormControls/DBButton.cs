#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBButton.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBButton : DBUserControlBase, IButtonControl
    {
        #region ButtonStyleType enum

        public enum ButtonStyleType
        {
            DropDown,
            Normal,
            Push,
            Troggle
        }

        #endregion

        private ButtonStyleType m_ButtonStyle = ButtonStyleType.Normal;
        private bool m_DownMouse;
        private Color m_FillColorEnd = Color.White;
        private Color m_FillColorStart = Color.LightGray;
        private Color m_FillHoverColorEnd = Color.Beige;
        private Color m_FillHoverColorStart = Color.Beige;
        private bool m_Gradient;
        private LinearGradientMode m_GradientMode = LinearGradientMode.Horizontal;
        private Color m_OutlineColor = Color.White;
        private bool m_SwapMouse;
        private Color m_TextColorEnd = Color.Black;
        private Color m_TextColorStart = Color.Blue;
        private string m_ToolTip = "";


        #region Events

        public new event EventHandler Click;

        #endregion


        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return Button1.Text; }
            set { Button1.Text = value; }
        }

        public Color FillColorStart
        {
            get { return m_FillColorStart; }
            set
            {
                m_FillColorStart = value;
                Button1.Refresh();
            }
        }

        public string Key
        {
            get { return Button1.Name; }
            set { Button1.Name = value; }
        }

        public Color FillColorEnd
        {
            get { return m_FillColorEnd; }
            set
            {
                m_FillColorEnd = value;
                Button1.Refresh();
            }
        }

        public Color FillHoverColorStart
        {
            get { return m_FillHoverColorStart; }
            set
            {
                m_FillHoverColorStart = value;
                Button1.Refresh();
            }
        }

        public Color FillHoverColorEnd
        {
            get { return m_FillHoverColorEnd; }
            set
            {
                m_FillHoverColorEnd = value;
                Button1.Refresh();
            }
        }

        public Color TextColorStart
        {
            get { return m_TextColorStart; }
            set
            {
                m_TextColorStart = value;
                Button1.Refresh();
            }
        }

        public Color TextColorEnd
        {
            get { return m_TextColorEnd; }
            set
            {
                m_TextColorEnd = value;
                Button1.Refresh();
            }
        }

        public Font TextFont
        {
            get { return Button1.Font; }
            set { Button1.Font = value; }
        }

        public DBAppearance Appearance { get; set; }

        public ContentAlignment TextAlign
        {
            get { return Button1.TextAlign; }
            set { Button1.TextAlign = value; }
        }

        public FlatStyle FlatStyle
        {
            get { return Button1.FlatStyle; }
            set { Button1.FlatStyle = value; }
        }

        public ButtonStyleType ButtonStyle
        {
            get { return m_ButtonStyle; }
            set
            {
                var resources = new ResourceManager(typeof(DBButton));
                m_ButtonStyle = value;
                if (value == ButtonStyleType.DropDown)
                {
                    Button1.ImageAlign = ContentAlignment.MiddleRight;
                    Button1.Image = (Bitmap) resources.GetObject("Button1.Image");
                }
                else
                {
                    Button1.ImageAlign = ContentAlignment.MiddleCenter;
                    Button1.Image = null;
                }

                Button1.Refresh();
            }
        }


        public ContextMenu DropDownMenu { get; set; }

        public string ToolTip
        {
            get { return m_ToolTip; }
            set
            {
                m_ToolTip = value;
                ToolTip1.SetToolTip(Button1, m_ToolTip);
            }
        }

        public Image Image
        {
            get { return Button1.Image; }
            set { Button1.Image = value; }
        }

        public ContentAlignment ImageAlign
        {
            get { return Button1.ImageAlign; }
            set { Button1.ImageAlign = value; }
        }

        public LinearGradientMode GradientMode
        {
            get { return m_GradientMode; }
            set
            {
                m_GradientMode = value;
                Button1.Refresh();
            }
        }

        public bool Gradient
        {
            get { return m_Gradient; }
            set
            {
                m_Gradient = value;
                Button1.Refresh();
            }
        }

        public DialogResult DialogResult
        {
            get { return Button1.DialogResult; }

            set { Button1.DialogResult = value; }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (null != Click) Click(this, e);
            DropDownMenu_PopUp(this, e);
        }


        private void DropDownMenu_PopUp(object sender, EventArgs e)
        {
            var pos = new Point();

            if (ButtonStyle == ButtonStyleType.DropDown)
                if (!(DropDownMenu == null))
                {
                    pos = Location;
                    pos.Y = pos.Y + Button1.Height;

                    DropDownMenu.Show(ParentForm, pos);
                }
        }

        private void Button1_Paint(object sender, PaintEventArgs e)
        {
            Brush GradiantBrush = null;
            Brush TextBrush = null;
            Brush HoverBrush = null;
            var StringSize = new SizeF();

            if (m_Gradient == false) return;

            var recF = new RectangleF(0, 0, Width, Height);
            HoverBrush = new LinearGradientBrush(recF, FillHoverColorStart, FillHoverColorEnd, m_GradientMode);
            GradiantBrush = new LinearGradientBrush(recF, m_FillColorStart, m_FillColorEnd, m_GradientMode);
            TextBrush = new LinearGradientBrush(recF, m_TextColorStart, m_TextColorEnd, m_GradientMode);

            if (m_SwapMouse)
                e.Graphics.FillRectangle(HoverBrush, ClientRectangle);
            else
                e.Graphics.FillRectangle(GradiantBrush, ClientRectangle);

            if (m_DownMouse)
                ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Bump);
            else
                ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Etched);

            StringSize = e.Graphics.MeasureString(Button1.Text, TextFont);
            switch (TextAlign)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    e.Graphics.DrawString(Button1.Text, TextFont, TextBrush,
                        Convert.ToInt32(Width / 2) - Convert.ToInt32(StringSize.Width / 2),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    e.Graphics.DrawString(Button1.Text, TextFont, TextBrush, Convert.ToInt32(0 + 5),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    e.Graphics.DrawString(Button1.Text, TextFont, TextBrush,
                        Convert.ToInt32(Width - StringSize.Width - 5),
                        Convert.ToInt32(Height / 2) - Convert.ToInt32(StringSize.Height / 2));
                    break;
            }
        }


        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            m_SwapMouse = false;
            Button1.Refresh();
            base.OnMouseLeave(new EventArgs());
        }


        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            m_SwapMouse = true;
            Button1.Refresh();
            base.OnMouseEnter(new EventArgs());
        }


        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {
            m_DownMouse = true;
            Button1.Refresh();
            base.OnMouseDown(e);
        }


        private void Button1_MouseUp(object sender, MouseEventArgs e)
        {
            m_DownMouse = false;
            Button1.Refresh();
            base.OnMouseUp(e);
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private Button Button1;
        internal ToolTip ToolTip1;
        private IContainer components;


        private void Init()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            Button1.Click += Button1_Click;
            Button1.Paint += Button1_Paint;
            Button1.MouseLeave += Button1_MouseLeave;
            Button1.MouseEnter += Button1_MouseEnter;
            Button1.MouseDown += Button1_MouseDown;
            Button1.MouseUp += Button1_MouseUp;
        }

        public DBButton()
        {
            Init();
        }

        public DBButton(string text)
        {
            Init();

            Text = text;
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
            components = new Container();
            Button1 = new Button();
            ToolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // Button1
            // 
            Button1.Dock = DockStyle.Fill;
            Button1.ImageAlign = ContentAlignment.MiddleRight;
            Button1.Location = new Point(0, 0);
            Button1.Name = "Button1";
            Button1.Size = new Size(95, 39);
            Button1.TabIndex = 0;
            // 
            // DBButton
            // 
            Controls.Add(Button1);
            Name = "DBButton";
            Size = new Size(95, 39);
            ResumeLayout(false);
        }

        public void NotifyDefault(bool value)
        {
            Button1.NotifyDefault(value);
        }

        public void PerformClick()
        {
            Button1.PerformClick();
        }

        #endregion
    }
}