#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBControl.bmp")]
    [ToolboxItem(true)]
    public class DBDocument : DBUserControlBase
    {
        private AccessMode m_Mode = AccessMode.WriteMode;


        //[Description("DataBindings.")]
        //public new ControlBindingsCollection DataBindings
        //{
        //    get { return Label1.DataBindings; }
        //}


        public AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;

                switch (m_Mode)
                {
                    case AccessMode.ReadMode:
                        Label1.ContextMenu = null;
                        Label1.Enabled = false;
                        break;
                    case AccessMode.WriteMode:
                        Label1.ContextMenu = ContextMenu1;
                        Label1.Enabled = false;
                        break;
                }
            }
        }

        public void Bind()
        {
            //if (Label1.DataBindings.Count > 0) return;

            //Binding dbnDocument = new Binding("Text", DataControl.DataTable, DBField);

            if (DataControl != null)
                Label1.Text = DataControl.GetField(DBField).ToString();

            //dbnDocument.Format += dbnFormat;

            //try
            //{
            //    Label1.DataBindings.Add(dbnDocument);
            //}
            //catch (System.Exception e)
            //{
            //    throw new ExceptionUtil(e);
            //}
        }


        private void dbnFormat(object sender, ConvertEventArgs e)
        {
            if (e.Value is DBNull)
            {
                var b = new Bitmap(1, 1);
                e.Value = b;
                Label1.Visible = true;
                return;
            }

            var img = (byte[]) e.Value;

            var ms = new MemoryStream();
            var offset = 0;
            ms.Write(img, offset, img.Length - offset);
            var bmp = new Bitmap(ms);
            ms.Close();

            e.Value = bmp;
        }


        private void MenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter = "JPEG|*.jpg|Mapa de bits|*.bmp|GIF|*.gif|Metarchivo|*.wmf|Icono|*.ico";
                OpenFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal ContextMenu ContextMenu1;
        internal Label Label1;
        internal MenuItem MenuItem1;
        internal OpenFileDialog OpenFileDialog1;

        public DBDocument()
        {
            InitializeComponent();

            MenuItem1.Click += MenuItem1_Click;
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
            ContextMenu1 = new ContextMenu();
            MenuItem1 = new MenuItem();
            OpenFileDialog1 = new OpenFileDialog();
            Label1 = new Label();
            SuspendLayout();
            // 
            // ContextMenu1
            // 
            ContextMenu1.MenuItems.AddRange(new[]
            {
                MenuItem1
            });
            // 
            // MenuItem1
            // 
            MenuItem1.Index = 0;
            MenuItem1.Text = "Seleccionar Imagen";
            // 
            // Label1
            // 
            Label1.BorderStyle = BorderStyle.Fixed3D;
            Label1.ContextMenu = ContextMenu1;
            Label1.Dock = DockStyle.Fill;
            Label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Label1.Location = new Point(0, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(150, 146);
            Label1.TabIndex = 1;
            Label1.Text = "DBDocument";
            Label1.Visible = false;
            // 
            // DBDocument
            // 
            Controls.Add(Label1);
            Name = "DBDocument";
            Size = new Size(150, 146);
            ResumeLayout(false);
        }

        #endregion
    }
}