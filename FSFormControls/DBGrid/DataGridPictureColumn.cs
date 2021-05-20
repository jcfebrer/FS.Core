#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class DataGridPictureColumn : DBUserControl, IImageContainer
    {
        private static readonly Bitmap sEmptyBitmap = new Bitmap(1, 1);
        private IContainer components;
        private PictureBox pictureBox1;

        public DataGridPictureColumn()
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable | ControlStyles.StandardClick, true);
            DataBindings.CollectionChanged += DataBindings_CollectionChanged;

            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
        }

        [Category("Appearance")]
        [Description("The image displayed in this DataPicture")]
        public Image Image
        {
            get { return pictureBox1.Image; }
            set
            {
                if (!(pictureBox1.Image == value))
                {
                    pictureBox1.Image = value;
                    OnImageChanged();
                }
            }
        }

        // interface properties implemented by Image


        [Category("Behavior")]
        [Description("Controls how the picture box will handle image placement and control sizing.")]
        public PictureBoxSizeMode SizeMode
        {
            get { return pictureBox1.SizeMode; }
            set { pictureBox1.SizeMode = value; }
        }


        protected override bool ShowFocusCues => true;

        #region IImageContainer Members

        [Category("Appearance")]
        [Description("The image displayed in this DataPicture")]
        Image IImageContainer.Image
        {
            get { return Image; }
            set { Image = value; }
        }

        #endregion

        public event EventHandler ImageChanged;

        protected void OnImageChanged()
        {
            if (null != ImageChanged) ImageChanged(this, EventArgs.Empty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            pictureBox1.BackColor = SystemColors.Highlight;
        }


        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            pictureBox1.BackColor = BackColor;
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Focus();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Focus();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }


        private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            var binding = DataBindings["Image"];
            if (!(binding == null))
            {
                binding.Parse += binding_Parse;
                binding.Format += binding_Format;
            }
        }


        private void binding_Format(object sender, ConvertEventArgs ev)
        {
            if (!(ev.Value == Image) & (ev.DesiredType.ToString().ToLower() == "image"))
            {
                var img = (byte[]) ev.Value;
                ev.Value = sEmptyBitmap;
                if (!(img == null) & (img.Length > 0))
                {
                    var ms = new MemoryStream();
                    try
                    {
                        var offset = 0;
                        ms.Write(img, offset, img.Length - offset);
                        var bmp = new Bitmap(ms);
                        ms.Close();
                        ev.Value = bmp;
                    }
                    catch
                    {
                    }
                }
            }
        }


        private void binding_Parse(object sender, ConvertEventArgs ev)
        {
            if (ev.Value is Bitmap & (ev.DesiredType.ToString().ToLower() == "byte()"))
            {
                var bmp = (Bitmap) ev.Value;
                var ms = new MemoryStream();
                var format = bmp.RawFormat;
                if (format == ImageFormat.MemoryBmp) format = ImageFormat.Bmp;
                bmp.Save(ms, format);
                ms.Close();
                ev.Value = ms.GetBuffer();
            }
            else if ((ev.Value == null) | (ev.Value == sEmptyBitmap))
            {
                ev.Value = DBNull.Value;
            }
        }

        #region '"Component Designer generated code"' 

        internal DataGridPictureContextMenu DataGridPictureContextMenu1;

        private void InitializeComponent()
        {
            components = new Container();
            pictureBox1 = new PictureBox();
            DataGridPictureContextMenu1 = new DataGridPictureContextMenu(components);
            ((ISupportInitialize) pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(148, 130);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // DataGridPictureColumn
            // 
            ContextMenu = DataGridPictureContextMenu1;
            Controls.Add(pictureBox1);
            Name = "DataGridPictureColumn";
            Size = new Size(148, 130);
            ((ISupportInitialize) pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}