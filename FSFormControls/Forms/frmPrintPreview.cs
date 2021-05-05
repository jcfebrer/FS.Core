#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ToolboxItem(false)]
    public class frmPrintPreview : PrintPreviewDialog
    {
        private ArrayList AddedButtons;
        private int nInitialImages;
        protected ToolBar pToolBar;

        public ToolBar ParentToolbar
        {
            get
            {
                var fi = typeof(PrintPreviewDialog).GetField("toolBar1",
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance);

                if (fi == null)
                    return null;
                return (ToolBar) fi.GetValue(this);
            }
        }

        public event ToolBarButtonClickEventHandler AddedButtonsClick;

        private void AddedButtons_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            var i = 0;
            for (i = 0; i <= AddedButtons.Count - 1; i++)
                if (e.Button == AddedButtons[i])
                {
                    if (null != AddedButtonsClick)
                        AddedButtonsClick(this, e);
                    return;
                }
        }


        public void AddToolBarButtons(ToolBarButton[] Buttons)
        {
            var imgList = pToolBar.ImageList;
            var i = 0;
            nInitialImages = imgList.Images.Count;
            AddedButtons = new ArrayList();
            for (i = 0; i <= AddedButtonsImageList.Images.Count - 1; i++)
                imgList.Images.Add(AddedButtonsImageList.Images[i]);

            var initw = 0;
            for (i = 0; i <= Buttons.GetLength(0) - 1; i++)
            {
                AddedButtons.Add(Buttons[i]);
                if (Buttons[i].ImageIndex >= 0) Buttons[i].ImageIndex += nInitialImages;
                pToolBar.Buttons.Add(Buttons[i]);
                initw += pToolBar.Buttons[pToolBar.Buttons.Count - 1].Rectangle.Width;
            }

            var s = MinimumSize;
            s.Width += initw;
            MinimumSize = s;
            var b = GetCloseButton();
            if (b != null)
                b.Left += initw;
        }


        private Button GetCloseButton()
        {
            var fi = typeof(PrintPreviewDialog).GetField("closeButton",
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance);
            if (fi == null)
                return null;
            return (Button) fi.GetValue(this);
        }


        private Label GetPageLabel()
        {
            var fi = typeof(PrintPreviewDialog).GetField("pageLabel",
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance);
            if (fi == null)
                return null;
            return (Label) fi.GetValue(this);
        }

        #region '" Windows Form Designer generated code "' 

        public ImageList AddedButtonsImageList;
        private IContainer components;

        public frmPrintPreview()
        {
            InitializeComponent();

            if (ParentToolbar != null)
            {
                pToolBar = ParentToolbar;

                if (pToolBar != null)
                {
                    pToolBar.Buttons[0].ToolTipText = "Imprimir";
                    pToolBar.Buttons[1].ToolTipText = "Zoom";
                    pToolBar.Buttons[3].ToolTipText = "1 Página";
                    pToolBar.Buttons[4].ToolTipText = "2 Páginas";
                    pToolBar.Buttons[5].ToolTipText = "3 Páginas";
                    pToolBar.Buttons[6].ToolTipText = "4 Páginas";
                    pToolBar.Buttons[7].ToolTipText = "6 Páginas";
                }
            }
            else
            {
                pToolBar = new ToolBar();

                pToolBar.Buttons.Add("Imprimir");
                pToolBar.Buttons.Add("Zoom");
                pToolBar.Buttons.Add("1 Página");
                pToolBar.Buttons.Add("2 Páginas");
                pToolBar.Buttons.Add("3 Páginas");
                pToolBar.Buttons.Add("4 Páginas");
                pToolBar.Buttons.Add("5 Páginas");
            }

            pToolBar.ButtonClick += AddedButtons_Click;

            var p = GetPageLabel();
            if (p != null)
                p.Text = "Página:";

            var b = GetCloseButton();
            if (b != null)
                b.Text = "Cerrar";

            Text = "Vista Preliminar";

            var meAssembly = Assembly.GetAssembly(Type.GetType("FSFormControls.frmPrintPreview"));
            var rm = new ResourceManager("FSFormControls.frmPrintPreview", meAssembly);
            Icon = (Icon) rm.GetObject("PrintPreview.Icon");
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
            AddedButtonsImageList = new ImageList(components);
            SuspendLayout();
            // 
            // AddedButtonsImageList
            // 
            AddedButtonsImageList.ColorDepth = ColorDepth.Depth8Bit;
            AddedButtonsImageList.ImageSize = new Size(16, 16);
            AddedButtonsImageList.TransparentColor = Color.Transparent;
            // 
            // frmPrintPreview
            // 
            ClientSize = new Size(631, 315);
            Name = "frmPrintPreview";
            ResumeLayout(false);
        }

        #endregion
    }
}