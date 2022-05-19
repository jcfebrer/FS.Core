#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class DataGridPictureContextMenu : ContextMenu
    {
        #region Delegates

        public delegate void ImageChangedEventHandler(object sender, EventArgs e);

        #endregion

        private const string cPropertyUnavailable = "This property cannot be accessed";
        private IContainer components;
        private MenuItem Copy;
        private MenuItem Cut;
        private MenuItem Delete;
        internal ImageList imageList1;
        private OpenFileDialog openFileDialog1;
        private MenuItem Paste;
        private MenuItem PasteFrom;


        public DataGridPictureContextMenu(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        public DataGridPictureContextMenu()
        {
            InitializeComponent();
        }

        public static Bitmap EmptyBitmap { get; } = new Bitmap(1, 1);

        private Image Image
        {
            get
            {
                Image bm = null;
                var imageContainer = (IImageContainer) SourceControl;
                if (!(imageContainer == null))
                {
                    bm = imageContainer.Image;
                }
                else
                {
                    var pictureBox = (PictureBox) SourceControl;
                    if (!(pictureBox == null)) bm = pictureBox.Image;
                }

                return bm;
            }
            set
            {
                var imageContainer = (IImageContainer) SourceControl;
                if (!(imageContainer == null))
                {
                    imageContainer.Image = value;
                }
                else
                {
                    var pictureBox = (PictureBox) SourceControl;
                    if (!(pictureBox == null)) pictureBox.Image = value;
                }

                if (null != ImageChanged) ImageChanged(this, EventArgs.Empty);
            }
        }

        public event ImageChangedEventHandler ImageChanged;


        private void InitializeComponent()
        {
            components = new Container();
            var resources = new ResourceManager(typeof(DataGridPictureContextMenu));
            Cut = new MenuItem();
            Copy = new MenuItem();
            Paste = new MenuItem();
            PasteFrom = new MenuItem();
            Delete = new MenuItem();
            openFileDialog1 = new OpenFileDialog();
            imageList1 = new ImageList(components);
            Cut.Index = 0;
            Cut.Shortcut = Shortcut.CtrlX;
            Cut.ShowShortcut = false;
            Cut.Text = "Cortar";
            Cut.Click += menuItemClick;
            Copy.Index = 1;
            Copy.Shortcut = Shortcut.CtrlC;
            Copy.ShowShortcut = false;
            Copy.Text = "Copiar";
            Copy.Click += menuItemClick;
            Paste.Index = 2;
            Paste.Shortcut = Shortcut.CtrlV;
            Paste.ShowShortcut = false;
            Paste.Text = "Pegar";
            Paste.Click += menuItemClick;
            PasteFrom.Index = 3;
            PasteFrom.Shortcut = Shortcut.CtrlV;
            PasteFrom.ShowShortcut = false;
            PasteFrom.Text = "Pegar desde ...";
            PasteFrom.Click += menuItemClick;
            Delete.Index = 4;
            Delete.Shortcut = Shortcut.Del;
            Delete.ShowShortcut = false;
            Delete.Text = "Borrar";
            Delete.Click += menuItemClick;
            openFileDialog1.Filter =
                "All image files(*.bmp,*.gif,*.jpg,*.png,*.ico,*.emf,*.wmf)|*.bmp;*.gif;*.jpg;*.pn" +
                "g;*.ico;*.emf;*.wmf|Bitmap files(*.bmp,*.gif,*.jpg,*.png,*.ico)|*.bmp;*.gif;*.jp" +
                "g;*.png;*.ico|Metafiles(*.emf,*.wmf)|*.emf;*.wmf";
            imageList1.ImageSize = new Size(16, 16);
            imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            MenuItems.AddRange(new[] {Cut, Copy, Paste, PasteFrom, Delete});

            // events handled by menuItemClick

            Cut.Click += menuItemClick;
            Copy.Click += menuItemClick;
            Paste.Click += menuItemClick;
            PasteFrom.Click += menuItemClick;
            Delete.Click += menuItemClick;
        }


        protected override void OnPopup(EventArgs e)
        {
            base.OnPopup(e);

            var bm = Image;

            if (bm == EmptyBitmap) bm = null;
            var dataObj = FSLibrary.Clipboard.GetDataObject();

            Cut.Enabled = !(bm == null);
            Copy.Enabled = !(bm == null);
            Paste.Enabled = !(dataObj == null) & dataObj.GetDataPresent(DataFormats.Bitmap);

            Delete.Enabled = !(bm == null);
            PasteFrom.Enabled = true;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        private void menuItemClick(object sender, EventArgs e)
        {
            var bm = Image;

            if ((sender == Cut) | (sender == Copy))
                if (!(bm == null))
                    FSLibrary.Clipboard.SetDataObject(bm, false);
            if (!(sender == Copy) & !(sender == PasteFrom)) Image = EmptyBitmap;
            if (sender == Paste)
            {
                var dataObj = FSLibrary.Clipboard.GetDataObject();
                var Newbm = (Bitmap) dataObj.GetData(DataFormats.Bitmap);
                Image = Newbm;
            }
            else if (sender == PasteFrom)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    try
                    {
                        var Newbm = Image.FromFile(openFileDialog1.FileName);
                        Image = Newbm;
                    }
                    catch (ExceptionUtil ex)
                    {
                        throw new ExceptionUtil(ex);
                    }
            }
        }
    }


    public interface IImageContainer
    {
        Image Image { set; get; }
    }
}