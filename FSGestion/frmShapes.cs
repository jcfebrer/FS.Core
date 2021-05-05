
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;



namespace FSGestion
{
    public class frmShapes : System.Windows.Forms.Form
    {
        
#region  Windows Form Designer generated code
        
        public frmShapes()
        {
            
            //This call is required by the Windows Form Designer.
            InitializeComponent();
            
            //Add any initialization after the InitializeComponent() call
            
        }
        
        //Form overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!(components == null))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        //Required by the Windows Form Designer
        private System.ComponentModel.Container components = null;
        
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.
        //Do not modify it using the code editor.
        internal FSFormControls.DBShape ShapeEx1;
        internal FSFormControls.DBShape ShapeEx2;
        internal FSFormControls.DBShape ShapeEx3;
        internal FSFormControls.DBShape ShapeEx4;
        internal FSFormControls.DBShape ShapeEx5;
        internal FSFormControls.DBShape ShapeEx6;
        internal FSFormControls.DBShape ShapeEx7;
        internal FSFormControls.DBShape ShapeEx8;
        internal FSFormControls.DBShape ShapeEx9;
        internal FSFormControls.DBShape ShapeEx10;
        internal FSFormControls.DBShape ShapeEx11;
        internal FSFormControls.DBShape ShapeEx12;
        internal FSFormControls.DBShape ShapeEx13;
        internal FSFormControls.DBShape ShapeEx14;
        internal FSFormControls.DBShape ShapeEx15;
        internal FSFormControls.DBShape ShapeEx16;
        internal FSFormControls.DBShape ShapeEx17;
        internal FSFormControls.DBShape ShapeEx18;
        internal FSFormControls.DBShape ShapeEx19;
        internal FSFormControls.DBShape ShapeEx20;
        internal FSFormControls.DBShape ShapeEx21;
        internal FSFormControls.DBShape ShapeEx22;
        internal FSFormControls.DBShape ShapeEx23;
        internal FSFormControls.DBShape ShapeEx24;
        internal FSFormControls.cValue CValue1;
        internal FSFormControls.cValue CValue2;
        internal FSFormControls.cValue CValue3;
        internal FSFormControls.cValue CValue4;
        internal FSFormControls.cValue CValue5;
        internal FSFormControls.cValue CValue6;
        internal FSFormControls.cValue CValue7;
        internal FSFormControls.cValue CValue8;
        internal FSFormControls.cValue CValue9;
        internal FSFormControls.cValue CValue10;
        internal FSFormControls.cValue CValue11;
        internal FSFormControls.cValue CValue12;
        internal FSFormControls.cValue CValue13;
        internal FSFormControls.cValue CValue14;
        internal FSFormControls.cValue CValue15;
        internal FSFormControls.cValue CValue16;
        internal FSFormControls.cValue CValue17;
        internal FSFormControls.DBChart gs;
        [System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
        {
            this.ShapeEx1 = new FSFormControls.DBShape();
            base.Load += new System.EventHandler(Form2_Load);
            this.ShapeEx2 = new FSFormControls.DBShape();
            this.ShapeEx3 = new FSFormControls.DBShape();
            this.ShapeEx4 = new FSFormControls.DBShape();
            this.ShapeEx5 = new FSFormControls.DBShape();
            this.ShapeEx6 = new FSFormControls.DBShape();
            this.ShapeEx7 = new FSFormControls.DBShape();
            this.ShapeEx8 = new FSFormControls.DBShape();
            this.ShapeEx9 = new FSFormControls.DBShape();
            this.ShapeEx10 = new FSFormControls.DBShape();
            this.ShapeEx11 = new FSFormControls.DBShape();
            this.ShapeEx12 = new FSFormControls.DBShape();
            this.ShapeEx13 = new FSFormControls.DBShape();
            this.ShapeEx14 = new FSFormControls.DBShape();
            this.ShapeEx15 = new FSFormControls.DBShape();
            this.ShapeEx16 = new FSFormControls.DBShape();
            this.ShapeEx17 = new FSFormControls.DBShape();
            this.ShapeEx18 = new FSFormControls.DBShape();
            this.ShapeEx19 = new FSFormControls.DBShape();
            this.ShapeEx20 = new FSFormControls.DBShape();
            this.ShapeEx21 = new FSFormControls.DBShape();
            this.ShapeEx22 = new FSFormControls.DBShape();
            this.ShapeEx23 = new FSFormControls.DBShape();
            this.ShapeEx24 = new FSFormControls.DBShape();
            this.CValue1 = new FSFormControls.cValue();
            this.CValue2 = new FSFormControls.cValue();
            this.CValue3 = new FSFormControls.cValue();
            this.CValue4 = new FSFormControls.cValue();
            this.CValue5 = new FSFormControls.cValue();
            this.CValue6 = new FSFormControls.cValue();
            this.CValue7 = new FSFormControls.cValue();
            this.CValue8 = new FSFormControls.cValue();
            this.CValue9 = new FSFormControls.cValue();
            this.CValue10 = new FSFormControls.cValue();
            this.CValue11 = new FSFormControls.cValue();
            this.CValue12 = new FSFormControls.cValue();
            this.CValue13 = new FSFormControls.cValue();
            this.gs = new FSFormControls.DBChart();
            this.CValue14 = new FSFormControls.cValue();
            this.CValue15 = new FSFormControls.cValue();
            this.CValue16 = new FSFormControls.cValue();
            this.CValue17 = new FSFormControls.cValue();
            this.SuspendLayout();
            //
            //ShapeEx1
            //
            this.ShapeEx1.About = null;
            this.ShapeEx1.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx1.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx1.BackgroundTexture = null;
            this.ShapeEx1.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx1.DrawShadow = true;
            this.ShapeEx1.FillColor = System.Drawing.Color.CadetBlue;
            this.ShapeEx1.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx1.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx1.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx1.Location = new System.Drawing.Point(16, 16);
            this.ShapeEx1.Name = "ShapeEx1";
            this.ShapeEx1.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx1.Shape = FSFormControls.Shapes.Rectangle;
            this.ShapeEx1.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx1.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx1.TabIndex = 0;
            this.ShapeEx1.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx2
            //
            this.ShapeEx2.About = null;
            this.ShapeEx2.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx2.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx2.BackgroundTexture = null;
            this.ShapeEx2.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx2.DrawGradiant = true;
            this.ShapeEx2.DrawShadow = true;
            this.ShapeEx2.DrawText = true;
            this.ShapeEx2.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx2.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx2.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Beige, System.Drawing.Color.DarkKhaki, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx2.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx2.Location = new System.Drawing.Point(96, 16);
            this.ShapeEx2.Name = "ShapeEx2";
            this.ShapeEx2.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomLeft, (short) (6));
            this.ShapeEx2.Shape = FSFormControls.Shapes.Rectangle;
            this.ShapeEx2.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx2.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx2.TabIndex = 1;
            this.ShapeEx2.TextOptions = new FSFormControls.TextProperties("Title", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx3
            //
            this.ShapeEx3.About = null;
            this.ShapeEx3.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx3.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx3.BackgroundTexture = null;
            this.ShapeEx3.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx3.DrawShadow = true;
            this.ShapeEx3.DrawTexture = true;
            this.ShapeEx3.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx3.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx3.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx3.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx3.Location = new System.Drawing.Point(176, 24);
            this.ShapeEx3.Name = "ShapeEx3";
            this.ShapeEx3.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopRight, (short) (6));
            this.ShapeEx3.Shape = FSFormControls.Shapes.Rectangle;
            this.ShapeEx3.Size = new System.Drawing.Size(64, 40);
            this.ShapeEx3.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx3.TabIndex = 2;
            this.ShapeEx3.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx4
            //
            this.ShapeEx4.About = null;
            this.ShapeEx4.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx4.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx4.BackgroundTexture = null;
            this.ShapeEx4.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx4.DrawHatch = true;
            this.ShapeEx4.DrawShadow = true;
            this.ShapeEx4.FillColor = System.Drawing.Color.SkyBlue;
            this.ShapeEx4.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx4.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx4.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.White, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Trellis);
            this.ShapeEx4.Location = new System.Drawing.Point(248, 16);
            this.ShapeEx4.Name = "ShapeEx4";
            this.ShapeEx4.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopLeft, (short) (6));
            this.ShapeEx4.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx4.Size = new System.Drawing.Size(208, 216);
            this.ShapeEx4.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx4.TabIndex = 3;
            this.ShapeEx4.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx5
            //
            this.ShapeEx5.About = null;
            this.ShapeEx5.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx5.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx5.BackgroundTexture = null;
            this.ShapeEx5.BorderOptions = new FSFormControls.BorderProperties((short) (3), System.Drawing.Color.Black, true, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Gray, System.Drawing.Color.FromArgb(System.Convert.ToByte(224), System.Convert.ToByte(224), System.Convert.ToByte(224)), System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx5.FillColor = System.Drawing.Color.YellowGreen;
            this.ShapeEx5.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx5.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx5.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx5.Location = new System.Drawing.Point(336, 16);
            this.ShapeEx5.Name = "ShapeEx5";
            this.ShapeEx5.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx5.Shape = FSFormControls.Shapes.Rectangle;
            this.ShapeEx5.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx5.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx5.TabIndex = 4;
            this.ShapeEx5.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx6
            //
            this.ShapeEx6.About = null;
            this.ShapeEx6.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx6.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx6.BackgroundTexture = null;
            this.ShapeEx6.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx6.FillColor = System.Drawing.Color.Gold;
            this.ShapeEx6.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx6.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx6.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx6.Location = new System.Drawing.Point(432, 192);
            this.ShapeEx6.Name = "ShapeEx6";
            this.ShapeEx6.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx6.Shape = FSFormControls.Shapes.Rectangle;
            this.ShapeEx6.Size = new System.Drawing.Size(64, 72);
            this.ShapeEx6.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx6.TabIndex = 5;
            this.ShapeEx6.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx7
            //
            this.ShapeEx7.About = null;
            this.ShapeEx7.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx7.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx7.BackgroundTexture = null;
            this.ShapeEx7.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, true, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.FromArgb(System.Convert.ToByte(224), System.Convert.ToByte(224), System.Convert.ToByte(224)), System.Drawing.Color.Blue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx7.DrawGradiant = true;
            this.ShapeEx7.DrawShadow = true;
            this.ShapeEx7.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx7.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx7.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Azure, System.Drawing.Color.DodgerBlue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx7.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx7.Location = new System.Drawing.Point(16, 96);
            this.ShapeEx7.Name = "ShapeEx7";
            this.ShapeEx7.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx7.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx7.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx7.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx7.TabIndex = 6;
            this.ShapeEx7.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx8
            //
            this.ShapeEx8.About = null;
            this.ShapeEx8.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx8.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx8.BackgroundTexture = null;
            this.ShapeEx8.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx8.DrawShadow = true;
            this.ShapeEx8.DrawTexture = true;
            this.ShapeEx8.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx8.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx8.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx8.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx8.Location = new System.Drawing.Point(96, 96);
            this.ShapeEx8.Name = "ShapeEx8";
            this.ShapeEx8.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomLeft, (short) (6));
            this.ShapeEx8.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx8.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx8.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx8.TabIndex = 7;
            this.ShapeEx8.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx9
            //
            this.ShapeEx9.About = null;
            this.ShapeEx9.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx9.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx9.BackgroundTexture = null;
            this.ShapeEx9.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx9.DrawShadow = true;
            this.ShapeEx9.FillColor = System.Drawing.Color.Olive;
            this.ShapeEx9.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx9.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx9.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx9.Location = new System.Drawing.Point(176, 96);
            this.ShapeEx9.Name = "ShapeEx9";
            this.ShapeEx9.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopRight, (short) (6));
            this.ShapeEx9.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx9.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx9.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx9.TabIndex = 8;
            this.ShapeEx9.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx10
            //
            this.ShapeEx10.About = null;
            this.ShapeEx10.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx10.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx10.BackgroundTexture = null;
            this.ShapeEx10.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx10.DrawHatch = true;
            this.ShapeEx10.DrawShadow = true;
            this.ShapeEx10.FillColor = System.Drawing.Color.YellowGreen;
            this.ShapeEx10.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx10.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx10.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.FromArgb(System.Convert.ToByte(224), System.Convert.ToByte(224), System.Convert.ToByte(224)), System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Plaid);
            this.ShapeEx10.Location = new System.Drawing.Point(256, 96);
            this.ShapeEx10.Name = "ShapeEx10";
            this.ShapeEx10.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopLeft, (short) (6));
            this.ShapeEx10.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx10.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx10.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx10.TabIndex = 9;
            this.ShapeEx10.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx11
            //
            this.ShapeEx11.About = null;
            this.ShapeEx11.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx11.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx11.BackgroundTexture = null;
            this.ShapeEx11.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Silver, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx11.DrawGradiant = true;
            this.ShapeEx11.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx11.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx11.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.FromArgb(System.Convert.ToByte(224), System.Convert.ToByte(224), System.Convert.ToByte(224)), System.Drawing.Color.Silver, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx11.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx11.Location = new System.Drawing.Point(336, 96);
            this.ShapeEx11.Name = "ShapeEx11";
            this.ShapeEx11.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx11.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx11.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx11.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx11.TabIndex = 10;
            this.ShapeEx11.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx12
            //
            this.ShapeEx12.About = null;
            this.ShapeEx12.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx12.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx12.BackgroundTexture = null;
            this.ShapeEx12.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx12.DrawHatch = true;
            this.ShapeEx12.FillColor = System.Drawing.Color.DodgerBlue;
            this.ShapeEx12.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx12.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx12.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.White, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.DiagonalBrick);
            this.ShapeEx12.Location = new System.Drawing.Point(416, 184);
            this.ShapeEx12.Name = "ShapeEx12";
            this.ShapeEx12.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx12.Shape = FSFormControls.Shapes.Circle;
            this.ShapeEx12.Size = new System.Drawing.Size(48, 64);
            this.ShapeEx12.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.ShapeEx12.TabIndex = 11;
            this.ShapeEx12.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx13
            //
            this.ShapeEx13.About = null;
            this.ShapeEx13.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx13.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx13.BackgroundTexture = null;
            this.ShapeEx13.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx13.DrawShadow = true;
            this.ShapeEx13.FillColor = System.Drawing.Color.Goldenrod;
            this.ShapeEx13.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx13.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx13.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx13.Location = new System.Drawing.Point(16, 176);
            this.ShapeEx13.Name = "ShapeEx13";
            this.ShapeEx13.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx13.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx13.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx13.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx13.TabIndex = 12;
            this.ShapeEx13.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx14
            //
            this.ShapeEx14.About = null;
            this.ShapeEx14.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx14.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx14.BackgroundTexture = null;
            this.ShapeEx14.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx14.DrawShadow = true;
            this.ShapeEx14.DrawTexture = true;
            this.ShapeEx14.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx14.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx14.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx14.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx14.Location = new System.Drawing.Point(96, 176);
            this.ShapeEx14.Name = "ShapeEx14";
            this.ShapeEx14.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomLeft, (short) (6));
            this.ShapeEx14.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx14.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx14.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx14.TabIndex = 13;
            this.ShapeEx14.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx15
            //
            this.ShapeEx15.About = null;
            this.ShapeEx15.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx15.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx15.BackgroundTexture = null;
            this.ShapeEx15.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, true, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.FromArgb(System.Convert.ToByte(255), System.Convert.ToByte(192), System.Convert.ToByte(255)), System.Drawing.Color.FromArgb(System.Convert.ToByte(192), System.Convert.ToByte(0), System.Convert.ToByte(192)), System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx15.DrawGradiant = true;
            this.ShapeEx15.DrawShadow = true;
            this.ShapeEx15.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx15.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx15.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Thistle, System.Drawing.Color.DarkOrchid, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx15.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx15.Location = new System.Drawing.Point(176, 176);
            this.ShapeEx15.Name = "ShapeEx15";
            this.ShapeEx15.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopRight, (short) (6));
            this.ShapeEx15.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx15.Size = new System.Drawing.Size(64, 48);
            this.ShapeEx15.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx15.TabIndex = 14;
            this.ShapeEx15.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx16
            //
            this.ShapeEx16.About = null;
            this.ShapeEx16.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx16.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx16.BackgroundTexture = null;
            this.ShapeEx16.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx16.DrawHatch = true;
            this.ShapeEx16.DrawShadow = true;
            this.ShapeEx16.FillColor = System.Drawing.Color.Yellow;
            this.ShapeEx16.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx16.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx16.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.FromArgb(System.Convert.ToByte(64), System.Convert.ToByte(64), System.Convert.ToByte(64)), System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Divot);
            this.ShapeEx16.Location = new System.Drawing.Point(256, 176);
            this.ShapeEx16.Name = "ShapeEx16";
            this.ShapeEx16.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopLeft, (short) (6));
            this.ShapeEx16.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx16.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx16.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx16.TabIndex = 15;
            this.ShapeEx16.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx17
            //
            this.ShapeEx17.About = null;
            this.ShapeEx17.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx17.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx17.BackgroundTexture = null;
            this.ShapeEx17.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Thistle, false, System.Drawing.Drawing2D.DashStyle.DashDotDot, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx17.FillColor = System.Drawing.Color.BlueViolet;
            this.ShapeEx17.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx17.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx17.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx17.Location = new System.Drawing.Point(336, 176);
            this.ShapeEx17.Name = "ShapeEx17";
            this.ShapeEx17.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx17.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx17.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx17.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx17.TabIndex = 16;
            this.ShapeEx17.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx18
            //
            this.ShapeEx18.About = null;
            this.ShapeEx18.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx18.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx18.BackgroundTexture = null;
            this.ShapeEx18.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx18.DrawShadow = true;
            this.ShapeEx18.FillColor = System.Drawing.Color.BlueViolet;
            this.ShapeEx18.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx18.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx18.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx18.Location = new System.Drawing.Point(16, 248);
            this.ShapeEx18.Name = "ShapeEx18";
            this.ShapeEx18.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopRight, (short) (6));
            this.ShapeEx18.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx18.Size = new System.Drawing.Size(64, 64);
            this.ShapeEx18.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx18.TabIndex = 17;
            this.ShapeEx18.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx19
            //
            this.ShapeEx19.About = null;
            this.ShapeEx19.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx19.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx19.BackgroundTexture = null;
            this.ShapeEx19.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx19.DrawHatch = true;
            this.ShapeEx19.DrawShadow = true;
            this.ShapeEx19.DrawTexture = true;
            this.ShapeEx19.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx19.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx19.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx19.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.FromArgb(System.Convert.ToByte(255), System.Convert.ToByte(255), System.Convert.ToByte(128)), System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Shingle);
            this.ShapeEx19.Location = new System.Drawing.Point(320, 312);
            this.ShapeEx19.Name = "ShapeEx19";
            this.ShapeEx19.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.TopLeft, (short) (8));
            this.ShapeEx19.Shape = FSFormControls.Shapes.Octagon;
            this.ShapeEx19.Size = new System.Drawing.Size(136, 96);
            this.ShapeEx19.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx19.TabIndex = 18;
            this.ShapeEx19.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx20
            //
            this.ShapeEx20.About = null;
            this.ShapeEx20.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx20.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx20.BackgroundTexture = null;
            this.ShapeEx20.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, true, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.FromArgb(System.Convert.ToByte(224), System.Convert.ToByte(224), System.Convert.ToByte(224)), System.Drawing.Color.Gray, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            this.ShapeEx20.DrawGradiant = true;
            this.ShapeEx20.DrawShadow = true;
            this.ShapeEx20.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx20.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx20.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Khaki, System.Drawing.Color.DarkGoldenrod, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
            this.ShapeEx20.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.DarkGoldenrod, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx20.Location = new System.Drawing.Point(176, 248);
            this.ShapeEx20.Name = "ShapeEx20";
            this.ShapeEx20.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomLeft, (short) (6));
            this.ShapeEx20.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx20.Size = new System.Drawing.Size(80, 104);
            this.ShapeEx20.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx20.TabIndex = 19;
            this.ShapeEx20.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx21
            //
            this.ShapeEx21.About = null;
            this.ShapeEx21.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx21.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx21.BackgroundTexture = null;
            this.ShapeEx21.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx21.DrawHatch = true;
            this.ShapeEx21.DrawShadow = true;
            this.ShapeEx21.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx21.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx21.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx21.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Black, System.Drawing.Color.IndianRed, System.Drawing.Drawing2D.HatchStyle.SmallConfetti);
            this.ShapeEx21.Location = new System.Drawing.Point(256, 248);
            this.ShapeEx21.Name = "ShapeEx21";
            this.ShapeEx21.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx21.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx21.Size = new System.Drawing.Size(64, 64);
            this.ShapeEx21.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx21.TabIndex = 20;
            this.ShapeEx21.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx22
            //
            this.ShapeEx22.About = null;
            this.ShapeEx22.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx22.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx22.BackgroundTexture = null;
            this.ShapeEx22.BorderOptions = new FSFormControls.BorderProperties((short) (8), System.Drawing.Color.Black, true, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.PeachPuff, System.Drawing.Color.SaddleBrown, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
            this.ShapeEx22.DrawShadow = true;
            this.ShapeEx22.FillColor = System.Drawing.Color.Peru;
            this.ShapeEx22.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx22.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx22.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx22.Location = new System.Drawing.Point(336, 248);
            this.ShapeEx22.Name = "ShapeEx22";
            this.ShapeEx22.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx22.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx22.Size = new System.Drawing.Size(64, 64);
            this.ShapeEx22.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx22.TabIndex = 21;
            this.ShapeEx22.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx23
            //
            this.ShapeEx23.About = null;
            this.ShapeEx23.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx23.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx23.BackgroundTexture = null;
            this.ShapeEx23.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx23.DrawGradiant = true;
            this.ShapeEx23.FillColor = System.Drawing.Color.Crimson;
            this.ShapeEx23.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx23.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.FromArgb(System.Convert.ToByte(255), System.Convert.ToByte(192), System.Convert.ToByte(128)), System.Drawing.Color.FromArgb(System.Convert.ToByte(192), System.Convert.ToByte(64), System.Convert.ToByte(0)), System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx23.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx23.Location = new System.Drawing.Point(416, 168);
            this.ShapeEx23.Name = "ShapeEx23";
            this.ShapeEx23.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx23.Shape = FSFormControls.Shapes.Hexagon;
            this.ShapeEx23.Size = new System.Drawing.Size(64, 48);
            this.ShapeEx23.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx23.TabIndex = 22;
            this.ShapeEx23.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //ShapeEx24
            //
            this.ShapeEx24.About = null;
            this.ShapeEx24.AlphaBlend = System.Convert.ToByte(255);
            this.ShapeEx24.BackColor = System.Drawing.Color.Transparent;
            this.ShapeEx24.BackgroundTexture = null;
            this.ShapeEx24.BorderOptions = new FSFormControls.BorderProperties((short) (2), System.Drawing.Color.Black, false, System.Drawing.Drawing2D.DashStyle.Solid, System.Windows.Forms.Border3DStyle.Flat, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx24.DrawTexture = true;
            this.ShapeEx24.FillColor = System.Drawing.SystemColors.Control;
            this.ShapeEx24.ForeColor = System.Drawing.Color.Black;
            this.ShapeEx24.GradiantOptions = new FSFormControls.GradiantProperties(System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            this.ShapeEx24.HatchOptions = new FSFormControls.HatchProperties(System.Drawing.Color.Empty, System.Drawing.Color.Transparent, System.Drawing.Drawing2D.HatchStyle.Horizontal);
            this.ShapeEx24.Location = new System.Drawing.Point(424, 72);
            this.ShapeEx24.Name = "ShapeEx24";
            this.ShapeEx24.ShadowOptions = new FSFormControls.ShadowProperties(System.Drawing.Color.DarkGray, FSFormControls.Shadowpositions.BottomRight, (short) (6));
            this.ShapeEx24.Shape = FSFormControls.Shapes.Triangle;
            this.ShapeEx24.Size = new System.Drawing.Size(64, 56);
            this.ShapeEx24.SmoothMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.ShapeEx24.TabIndex = 23;
            this.ShapeEx24.TextOptions = new FSFormControls.TextProperties("", new System.Drawing.Font("Microsoft Sans Serif", (float) (8.25F), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, System.Convert.ToByte(0)), System.Drawing.Color.Black, System.Drawing.ContentAlignment.TopCenter);
            //
            //CValue1
            //
            this.CValue1.Color = System.Drawing.Color.Empty;
            this.CValue1.Legend = "";
            this.CValue1.MovePie = false;
            this.CValue1.Value = 0;
            //
            //CValue2
            //
            this.CValue2.Color = System.Drawing.Color.Empty;
            this.CValue2.Legend = "";
            this.CValue2.MovePie = false;
            this.CValue2.Value = 0;
            //
            //CValue3
            //
            this.CValue3.Color = System.Drawing.Color.Empty;
            this.CValue3.Legend = "";
            this.CValue3.MovePie = false;
            this.CValue3.Value = 0;
            //
            //CValue4
            //
            this.CValue4.Color = System.Drawing.Color.Empty;
            this.CValue4.Legend = "";
            this.CValue4.MovePie = false;
            this.CValue4.Value = 0;
            //
            //CValue5
            //
            this.CValue5.Color = System.Drawing.Color.Empty;
            this.CValue5.Legend = "";
            this.CValue5.MovePie = false;
            this.CValue5.Value = 0;
            //
            //CValue6
            //
            this.CValue6.Color = System.Drawing.Color.Empty;
            this.CValue6.Legend = "";
            this.CValue6.MovePie = false;
            this.CValue6.Value = 0;
            //
            //CValue7
            //
            this.CValue7.Color = System.Drawing.Color.Empty;
            this.CValue7.Legend = "";
            this.CValue7.MovePie = false;
            this.CValue7.Value = 8;
            //
            //CValue8
            //
            this.CValue8.Color = System.Drawing.Color.Empty;
            this.CValue8.Legend = "";
            this.CValue8.MovePie = false;
            this.CValue8.Value = 7;
            //
            //CValue9
            //
            this.CValue9.Color = System.Drawing.Color.Empty;
            this.CValue9.Legend = "";
            this.CValue9.MovePie = false;
            this.CValue9.Value = 7;
            //
            //CValue10
            //
            this.CValue10.Color = System.Drawing.Color.Empty;
            this.CValue10.Legend = "";
            this.CValue10.MovePie = false;
            this.CValue10.Value = 67;
            //
            //CValue11
            //
            this.CValue11.Color = System.Drawing.Color.Empty;
            this.CValue11.Legend = "";
            this.CValue11.MovePie = false;
            this.CValue11.Value = 0;
            //
            //CValue12
            //
            this.CValue12.Color = System.Drawing.Color.Empty;
            this.CValue12.Legend = "";
            this.CValue12.MovePie = false;
            this.CValue12.Value = 0;
            //
            //CValue13
            //
            this.CValue13.Color = System.Drawing.Color.Empty;
            this.CValue13.Legend = "";
            this.CValue13.MovePie = false;
            this.CValue13.Value = 0;
            //
            //gs
            //
            this.gs.DisplayBarValue = false;
            this.gs.DisplayUnits = false;
            this.gs.Location = new System.Drawing.Point(512, 56);
            this.gs.Name = "gs";
            this.gs.ShowChartLegends = true;
            this.gs.Size = new System.Drawing.Size(328, 256);
            this.gs.TabIndex = 24;
            this.gs.Values.AddRange(new FSFormControls.cValue[] {this.CValue14, this.CValue15, this.CValue16, this.CValue17});
            //
            //CValue14
            //
            this.CValue14.Color = System.Drawing.Color.Empty;
            this.CValue14.Legend = "";
            this.CValue14.MovePie = false;
            this.CValue14.Value = 0;
            //
            //CValue15
            //
            this.CValue15.Color = System.Drawing.Color.Empty;
            this.CValue15.Legend = "";
            this.CValue15.MovePie = false;
            this.CValue15.Value = 0;
            //
            //CValue16
            //
            this.CValue16.Color = System.Drawing.Color.Empty;
            this.CValue16.Legend = "";
            this.CValue16.MovePie = false;
            this.CValue16.Value = 0;
            //
            //CValue17
            //
            this.CValue17.Color = System.Drawing.Color.Empty;
            this.CValue17.Legend = "";
            this.CValue17.MovePie = false;
            this.CValue17.Value = 0;
            //
            //Form2
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(856, 486);
            this.Controls.Add(this.gs);
            this.Controls.Add(this.ShapeEx4);
            this.Controls.Add(this.ShapeEx24);
            this.Controls.Add(this.ShapeEx23);
            this.Controls.Add(this.ShapeEx22);
            this.Controls.Add(this.ShapeEx21);
            this.Controls.Add(this.ShapeEx20);
            this.Controls.Add(this.ShapeEx19);
            this.Controls.Add(this.ShapeEx18);
            this.Controls.Add(this.ShapeEx17);
            this.Controls.Add(this.ShapeEx16);
            this.Controls.Add(this.ShapeEx15);
            this.Controls.Add(this.ShapeEx14);
            this.Controls.Add(this.ShapeEx13);
            this.Controls.Add(this.ShapeEx12);
            this.Controls.Add(this.ShapeEx11);
            this.Controls.Add(this.ShapeEx10);
            this.Controls.Add(this.ShapeEx9);
            this.Controls.Add(this.ShapeEx8);
            this.Controls.Add(this.ShapeEx7);
            this.Controls.Add(this.ShapeEx6);
            this.Controls.Add(this.ShapeEx5);
            this.Controls.Add(this.ShapeEx3);
            this.Controls.Add(this.ShapeEx2);
            this.Controls.Add(this.ShapeEx1);
            this.Name = "Form2";
            this.Text = "frmShapes";
            this.ResumeLayout(false);
            
        }
        
#endregion
        
        private void Form2_Load(System.Object sender, System.EventArgs e)
        {
            
            gs.Values.Add(12, Color.Blue, "Valor1");
            gs.Values.Add(32, Color.Blue, "Valor2");
            gs.Values.Add(42, Color.Blue, "Valor3");
            gs.Values.Add(78, Color.Blue, "Valor4");
            gs.Values.Add(1, Color.Blue, "Valor5");
            gs.Values.Add(6, Color.Blue, "Valor6");
            gs.Values.Add(90, Color.Blue, "Valor7");
            gs.Values.Add(45, Color.Blue, "Valor8");
            
            
            gs.DisplayBarValue = true;
            gs.DisplayUnits = true;
            gs.ShowBorder = true;
            gs.Values.Xscale_Max = 100;
            gs.Values.Yscale_Max = 100;
            gs.Values.Xscale_units = 10;
            gs.Values.Yscale_units = 10;
            
            gs.ShowBorder = true;
            gs.ShowGridLines = true;
            
            gs.Values.MarginX = 0;
            gs.Values.MarginY = 0;
            gs.InitializeGraphSheet();

            gs.PlotType = FSFormControls.DBChart.plotTypeEnum.Line;
            gs.Color = Color.Blue;
            gs.DrawGraph(true);
            
            gs.ClearValues();
            
            //gs.DrawGraph(FSFormControls.DBChart.PlotType.Bar, _
            //   Color.Yellow, False)
            
            //gs.DrawPoint(20, 50, Color.SaddleBrown)
            
            //Dim cv() As Single = {12, 34, 56, 32, 13, 45, 10, 56, 2, 78, 13, 500}
            //Dim cn() As String = {"Valor1", "Valor2", "Valor3", "Valor4", "Valor5", "Valor6", "Valor7", "Valor8", "Valor9", "Valor10", "Valor11", "Valor12"}
            
            gs.Values.Add(12, Color.Blue, "Valor1");
            gs.Values.Add(32, Color.Red, "Valor2");
            gs.Values.Add(42, Color.Green, "Valor3");
            gs.Values.Add(78, Color.Yellow, "Valor4");
            gs.Values.Add(1, Color.Brown, "Valor5");
            gs.Values.Add(6, Color.Aquamarine, "Valor6");
            gs.Values.Add(90, Color.Beige, "Valor7");
            gs.Values.Add(45, Color.BurlyWood, "Valor8");
            
            //gs.ChartData(cv, cn)
            gs.ShowChartLegends = true;
            gs.PlotType = FSFormControls.DBChart.plotTypeEnum.Chart;
            gs.Color = Color.Black;
            gs.DrawGraph(false);
            
        }
        
    }
    
}
