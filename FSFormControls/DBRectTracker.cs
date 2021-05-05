#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class DBRectTracker : UserControl
    {
        #region RESIZE_BORDER enum

        public enum RESIZE_BORDER
        {
            RB_NONE = 0,
            RB_TOP = 1,
            RB_RIGHT = 2,
            RB_BOTTOM = 3,
            RB_LEFT = 4,
            RB_TOPLEFT = 5,
            RB_TOPRIGHT = 6,
            RB_BOTTOMLEFT = 7,
            RB_BOTTOMRIGHT = 8
        }

        #endregion

        private readonly Container components = null;
        public Rectangle baseRect;
        public Rectangle ControlRect;
        private RESIZE_BORDER CurrBorder;
        public Control currentControl;
        public Graphics g;
        private bool isFirst = true;
        public Color MyBackColor = Color.Wheat;
        private Point prevLeftClick;
        public Rectangle[] SmallRect = new Rectangle[9];
        public Size Sqare = new Size(6, 6);
        public Color SqareColor = Color.White;
        public Color SqareLineColor = Color.Black;

        public DBRectTracker(Control theControl)
        {
            InitializeComponent();
            currentControl = theControl;
            Create();
        }

        public DBRectTracker()
        {
            InitializeComponent();
        }

        public Control Control
        {
            get { return currentControl; }
            set { }
        }

        public Rectangle Rect
        {
            get { return baseRect; }
            set
            {
                var X = Sqare.Width;
                var Y = Sqare.Height;
                var Height = value.Height;
                var Width = value.Width;
                baseRect = new Rectangle(X, Y, Width, Height);
                SetRectangles();
            }
        }

        public void SetRectangles()
        {
            SmallRect[0] = new Rectangle(new Point(baseRect.X - Sqare.Width, baseRect.Y - Sqare.Height), Sqare);
            SmallRect[1] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y - Sqare.Height), Sqare);
            SmallRect[2] = new Rectangle(new Point(baseRect.X - Sqare.Width, baseRect.Y + baseRect.Height), Sqare);
            SmallRect[3] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y + baseRect.Height), Sqare);
            SmallRect[4] =
                new Rectangle(
                    new Point(Convert.ToInt32(baseRect.X + baseRect.Width / 2 - Sqare.Width / 2),
                        baseRect.Y - Sqare.Height), Sqare);
            SmallRect[5] =
                new Rectangle(
                    new Point(Convert.ToInt32(baseRect.X + baseRect.Width / 2 - Sqare.Width / 2),
                        baseRect.Y + baseRect.Height), Sqare);
            SmallRect[6] =
                new Rectangle(
                    new Point(baseRect.X - Sqare.Width,
                        Convert.ToInt32(baseRect.Y + baseRect.Height / 2 - Sqare.Height / 2)), Sqare);
            SmallRect[7] =
                new Rectangle(
                    new Point(baseRect.X + baseRect.Width,
                        Convert.ToInt32(baseRect.Y + baseRect.Height / 2 - Sqare.Height / 2)), Sqare);
            ControlRect = new Rectangle(new Point(0, 0), Bounds.Size);
        }


        public void Draw()
        {
            try
            {
                g.FillRectangles(Brushes.White, SmallRect);
                g.DrawRectangles(Pens.Black, SmallRect);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public bool Hit_Test(Point point)
        {
            if (!ControlRect.Contains(point))
            {
                Cursor.Current = Cursors.Arrow;
                return false;
            }

            if (SmallRect[0].Contains(point))
            {
                Cursor.Current = Cursors.SizeNWSE;
                CurrBorder = RESIZE_BORDER.RB_TOPLEFT;
            }
            else
            {
                if (SmallRect[3].Contains(point))
                {
                    Cursor.Current = Cursors.SizeNWSE;
                    CurrBorder = RESIZE_BORDER.RB_BOTTOMRIGHT;
                }
                else
                {
                    if (SmallRect[1].Contains(point))
                    {
                        Cursor.Current = Cursors.SizeNESW;
                        CurrBorder = RESIZE_BORDER.RB_TOPRIGHT;
                    }
                    else
                    {
                        if (SmallRect[2].Contains(point))
                        {
                            Cursor.Current = Cursors.SizeNESW;
                            CurrBorder = RESIZE_BORDER.RB_BOTTOMLEFT;
                        }
                        else
                        {
                            if (SmallRect[4].Contains(point))
                            {
                                Cursor.Current = Cursors.SizeNS;
                                CurrBorder = RESIZE_BORDER.RB_TOP;
                            }
                            else
                            {
                                if (SmallRect[5].Contains(point))
                                {
                                    Cursor.Current = Cursors.SizeNS;
                                    CurrBorder = RESIZE_BORDER.RB_BOTTOM;
                                }
                                else
                                {
                                    if (SmallRect[6].Contains(point))
                                    {
                                        Cursor.Current = Cursors.SizeWE;
                                        CurrBorder = RESIZE_BORDER.RB_LEFT;
                                    }
                                    else
                                    {
                                        if (SmallRect[7].Contains(point))
                                        {
                                            Cursor.Current = Cursors.SizeWE;
                                            CurrBorder = RESIZE_BORDER.RB_RIGHT;
                                        }
                                        else
                                        {
                                            if (ControlRect.Contains(point))
                                            {
                                                Cursor.Current = Cursors.SizeAll;
                                                CurrBorder = RESIZE_BORDER.RB_NONE;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }


        public bool Hit_Test(int x, int y)
        {
            return Hit_Test(new Point(x, y));
        }


        private void Create()
        {
            var X = currentControl.Bounds.X - Sqare.Width;
            var Y = currentControl.Bounds.Y - Sqare.Height;
            var Height = currentControl.Bounds.Height + Sqare.Height * 2;
            var Width = currentControl.Bounds.Width + Sqare.Width * 2;
            Bounds = new Rectangle(X, Y, Width + 1, Height + 1);
            BringToFront();
            Rect = currentControl.Bounds;
            Region = new Region(BuildFrame());
            g = CreateGraphics();
        }


        private GraphicsPath BuildFrame()
        {
            var path = new GraphicsPath();
            path.AddRectangle(new Rectangle(0, 0, currentControl.Width + Sqare.Width * 2 + 1, Sqare.Height + 1));

            path.AddRectangle(new Rectangle(0, Sqare.Height + 1, Sqare.Width + 1,
                currentControl.Bounds.Height + Sqare.Height + 1));
            path.AddRectangle(new Rectangle(Sqare.Width + 1, currentControl.Bounds.Height + Sqare.Height,
                currentControl.Width + Sqare.Width + 1, Sqare.Height + 1));
            path.AddRectangle(new Rectangle(currentControl.Width + Sqare.Width, Sqare.Height + 1, Sqare.Width + 1,
                currentControl.Height - 2));
            return path;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DBRectTracker
            // 
            BackColor = Color.Wheat;
            Name = "DBRectTracker";
            Size = new Size(63, 57);
            ResumeLayout(false);
        }


        public void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (currentControl.Height < 8)
            {
                currentControl.Height = 8;
                return;
            }

            if (currentControl.Width < 8)
            {
                currentControl.Width = 8;
                return;
            }

            switch (CurrBorder)
            {
                case RESIZE_BORDER.RB_TOP:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8) currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    break;
                case RESIZE_BORDER.RB_TOPLEFT:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8) currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8) currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_TOPRIGHT:
                    currentControl.Height = currentControl.Height - e.Y + prevLeftClick.Y;
                    if (currentControl.Height > 8) currentControl.Top = currentControl.Top + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_RIGHT:
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_BOTTOM:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    break;
                case RESIZE_BORDER.RB_BOTTOMLEFT:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8) currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_BOTTOMRIGHT:
                    currentControl.Height = currentControl.Height + e.Y - prevLeftClick.Y;
                    currentControl.Width = currentControl.Width + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_LEFT:
                    currentControl.Width = currentControl.Width - e.X + prevLeftClick.X;
                    if (currentControl.Width > 8) currentControl.Left = currentControl.Left + e.X - prevLeftClick.X;
                    break;
                case RESIZE_BORDER.RB_NONE:
                    currentControl.Location = new Point(currentControl.Location.X + e.X - prevLeftClick.X,
                        currentControl.Location.Y + e.Y - prevLeftClick.Y);
                    break;
            }
        }


        private void RectTracker_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            if (e.Button == MouseButtons.Left)
            {
                if (isFirst)
                {
                    prevLeftClick = new Point(e.X, e.Y);
                    isFirst = false;
                }
                else
                {
                    Visible = false;
                    Mouse_Move(this, e);
                    prevLeftClick = new Point(e.X, e.Y);
                }
            }
            else
            {
                isFirst = true;
                Visible = true;
                Hit_Test(e.X, e.Y);
            }
        }


        private void RectTracker_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }


        private void RectTracker_MouseUp(object sender, MouseEventArgs e)
        {
            Create();
            Visible = true;
        }
    }
}