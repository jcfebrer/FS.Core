using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSPong
{
    public class PongPanel : Panel
    {
        public Pong pong;
        Bitmap Backbuffer;

        Timer timer1 = new Timer();

        //public frmPong pongFrm;
        public int ScaleValue = 1;

        public PongPanel()
        {
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);


            timer1.Tick += Timer1_Tick;
            timer1.Interval = 10;


            this.Resize += new EventHandler(pong_CreateBackBuffer);
            //this.Load += new EventHandler(pong_CreateBackBuffer);
            this.SizeChanged += new EventHandler(pong_CreateBackBuffer);
            this.Paint += new PaintEventHandler(pong_Paint);
        }

        public void Start()
        {
            pong = new Pong();

            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private void pong_Paint(object sender, PaintEventArgs e)
        {
            if (Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }
        }

        void pong_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
            {
                Backbuffer.Dispose();
            }
            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }


        public void reset()
        {
            pong.reset();
        }

        private void Draw()
        {
            if (Backbuffer != null)
            {
                using (Graphics g = Graphics.FromImage(Backbuffer))
                {
                    g.ResetTransform();
                    Scale(g, ScaleValue);
                    g.Clear(Color.Green);
                    pong.Draw(g);
                }

                Invalidate();
            }
        }

        private void Scale(Graphics g, int value)
        {
            if (value == 0) return;
            
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            //g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            g.ScaleTransform((this.Width / pong.fieldWidth), (this.Height / pong.fieldHeight));

            //g.ScaleTransform(value, value);
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams createParams = base.CreateParams;
        //        createParams.ExStyle |= 0x00000020;
        //        return createParams;
        //    }
        //}

        //protected override void OnPaintBackground(PaintEventArgs e) { }
    }
}
