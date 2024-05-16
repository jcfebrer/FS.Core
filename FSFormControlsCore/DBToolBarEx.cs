#region

using FSExceptionCore;
using FSLibraryCore;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace FSFormControlsCore
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControlsCore.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarEx : DBUserControl
    {
        private bool m_AllowAddNew = true;
        private bool m_AllowCancel = true;
        private bool m_AllowClose = true;
        private bool m_AllowDelete = true;
        private bool m_AllowEdit = true;
        private bool m_AllowFilter = true;
        private bool m_AllowList = true;
        private bool m_AllowNavigate = true;
        private bool m_AllowPrint = true;
        private bool m_AllowRecord = true;
        private bool m_AllowSave = true;
        private bool m_AllowSearch = true;

        private bool m_ShowAddNewButton = true;
        private bool m_ShowCancelButton = true;
        private bool m_ShowCloseButton = true;
        private bool m_ShowDeleteButton = true;
        private bool m_ShowEditButton = true;
        private bool m_ShowFilterButton = true;
        private bool m_ShowListButton = true;
        private bool m_ShowNavigateButton = true;
        private bool m_ShowPrintButton = true;
        private bool m_ShowRecordButton = true;
        private bool m_ShowSaveButton = true;
        private bool m_ShowScrollBar = true;
        private bool m_ShowSearchButton = true;
        private bool m_ShowText = true;

        internal ToolStripButton ToolBarButton77;
        internal ToolStripButton ToolBarButton78;
        internal ToolStripButton ToolBarButton84;
        private ToolStripButton toolBarButton87;

        public int Value
        {
            get { return HScroll1.Value; }
            set
            {
                if (DataControl == null) 
                    return;

                if (value != HScroll1.Value)
                {
                    if (value <= HScroll1.Maximum)
                    {
                        HScroll1.Value = value;
                    }
                    else
                    {
                        if (value - 1 == HScroll1.Maximum) HScroll1.Maximum = value;
                        HScroll1.Value = HScroll1.Maximum;
                    }
                }
            }
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        public bool VisibleScroll
        {
            get { return HScroll1.Visible; }
            set { HScroll1.Visible = value; }
        }

        public bool VisibleTotalRecord
        {
            get { return lblReg.Visible; }
            set { lblReg.Visible = value; }
        }

        public bool ShowNavigateButton
        {
            get { return m_ShowNavigateButton; }
            set
            {
                m_ShowNavigateButton = value;
                ShowToolBar();
            }
        }

        public bool ShowSearchButton
        {
            get { return m_ShowSearchButton; }
            set
            {
                m_ShowSearchButton = value;
                ShowToolBar();
            }
        }

        public bool ShowCloseButton
        {
            get { return m_ShowCloseButton; }
            set
            {
                m_ShowCloseButton = value;
                ShowToolBar();
            }
        }

        public bool ShowCancelButton
        {
            get { return m_ShowCancelButton; }
            set
            {
                m_ShowCancelButton = value;
                ShowToolBar();
            }
        }

        public bool ShowSaveButton
        {
            get { return m_ShowSaveButton; }
            set
            {
                m_ShowSaveButton = value;
                ShowToolBar();
            }
        }

        public bool ShowScrollBar
        {
            get { return m_ShowScrollBar; }
            set
            {
                m_ShowScrollBar = value;
                ShowToolBar();
            }
        }

        public bool ShowText
        {
            get { return m_ShowText; }
            set
            {
                m_ShowText = value;
                ShowToolBar();
            }
        }


        public bool ShowAddNewButton
        {
            get { return m_ShowAddNewButton; }
            set
            {
                m_ShowAddNewButton = value;
                ShowToolBar();
            }
        }

        public bool ShowPrintButton
        {
            get { return m_ShowPrintButton; }
            set
            {
                m_ShowPrintButton = value;
                ShowToolBar();
            }
        }

        public bool ShowFilterButton
        {
            get { return m_ShowFilterButton; }
            set
            {
                m_ShowFilterButton = value;
                ShowToolBar();
            }
        }

        public bool ShowRecordButton
        {
            get { return m_ShowRecordButton; }
            set
            {
                m_ShowRecordButton = value;
                ShowToolBar();
            }
        }

        public bool ShowEditButton
        {
            get { return m_ShowEditButton; }
            set
            {
                m_ShowEditButton = value;
                ShowToolBar();
            }
        }

        public bool ShowDeleteButton
        {
            get { return m_ShowDeleteButton; }
            set
            {
                m_ShowDeleteButton = value;
                ShowToolBar();
            }
        }

        public bool ShowListButton
        {
            get { return m_ShowListButton; }
            set
            {
                m_ShowListButton = value;
                ShowToolBar();
            }
        }

        public bool AllowNavigate
        {
            get { return m_AllowNavigate; }
            set
            {
                m_AllowNavigate = value;
                ShowToolBar();
            }
        }

        public bool AllowSearch
        {
            get { return m_AllowSearch; }
            set
            {
                m_AllowSearch = value;
                ShowToolBar();
            }
        }

        public bool AllowCancel
        {
            get { return m_AllowCancel; }
            set
            {
                m_AllowCancel = value;
                ShowToolBar();
            }
        }

        public bool AllowSave
        {
            get { return m_AllowSave; }
            set
            {
                m_AllowSave = value;
                ShowToolBar();
            }
        }

        public bool AllowAddNew
        {
            get { return m_AllowAddNew; }
            set
            {
                m_AllowAddNew = value;
                ShowToolBar();
            }
        }

        public bool AllowPrint
        {
            get { return m_AllowPrint; }
            set
            {
                m_AllowPrint = value;
                ShowToolBar();
            }
        }

        public bool AllowFilter
        {
            get { return m_AllowFilter; }
            set
            {
                m_AllowFilter = value;
                ShowToolBar();
            }
        }

        public bool AllowRecord
        {
            get { return m_AllowRecord; }
            set
            {
                m_AllowRecord = value;
                ShowToolBar();
            }
        }

        public bool AllowEdit
        {
            get { return m_AllowEdit; }
            set
            {
                m_AllowEdit = value;
                ShowToolBar();
            }
        }

        public bool AllowDelete
        {
            get { return m_AllowDelete; }
            set
            {
                m_AllowDelete = value;
                ShowToolBar();
            }
        }

        public bool AllowList
        {
            get { return m_AllowList; }
            set
            {
                m_AllowList = value;
                ShowToolBar();
            }
        }

        public bool AllowClose
        {
            get { return m_AllowClose; }
            set
            {
                m_AllowClose = value;
                ShowToolBar();
            }
        }

        public event ChangeEventHandler Change;
        public event EventHandler ButtonClick;

        public void Initialize()
        {
            if (DataControl == null)
            {
                AllowAddNew = false;
                AllowDelete = false;
                AllowFilter = false;
                AllowList = false;
                AllowNavigate = false;
                AllowPrint = false;
                AllowRecord = false;
                ShowScrollBar = false;
                AllowSearch = false;

                //DBGlobal.Err.ErrorMessage( this.FindForm(), this, "DataControl no especificado.", "", MessageBoxIcon.Error, null, false ); 
            }
            else
            {
                DataControl.ChangeRecord += m_dbcontrol_ChangeRecord;

                HScroll1.SmallChange = 1;
                HScroll1.Minimum = 0;
                HScroll1.Maximum = DataControl.RecordCount() - 1 == -1 ? 0 : DataControl.RecordCount() - 1;
                HScroll1.LargeChange = 1;

                HScroll1.Value = DataControl.DBPosition;
                DataControl.Go(DataControl.DBPosition);

                ShowRecordLabel();
            }
        }

        private void tbrRegistrosBIG_ButtonClick(object sender, EventArgs e)
        {
            ToolbarOptions(sender, e);
        }

        private void ToolbarOptions(object sender, EventArgs e)
        {
            ToolStripItemClickedEventArgs button = (ToolStripItemClickedEventArgs)e;
            ToolStripItem tsi = button.ClickedItem;
            if (tsi != null && tsi.Tag != null)
                switch (tsi.Tag.ToString().ToUpper())
                {
                    case "IMPRIMIR":
                        if (DataControl != null) DataControl.ShowReport();
                        break;
                    case "BUSCAR":
                        if (DataControl != null) DataControl.ShowFind();
                        break;
                    case "BUSCARSIGUIENTE":
                        if (DataControl != null) DataControl.FindNext();
                        break;
                    case "ESTABLECERFILTROS":
                        if (DataControl != null) DataControl.ShowFilter();
                        break;
                    case "QUITARFILTROS":
                        if (DataControl != null) DataControl.DeleteFilter();
                        break;
                    case "MOVERPRIMERO":
                        if (DataControl != null) DataControl.MoveFirst();
                        break;
                    case "MOVERANTERIOR":
                        if (DataControl != null) DataControl.MovePrevious();
                        break;
                    case "MOVERSIGUIENTE":
                        if (DataControl != null) DataControl.MoveNext();
                        break;
                    case "MOVERULTIMO":
                        if (DataControl != null) DataControl.MoveLast();
                        break;
                    case "NUEVO":
                        if (DataControl != null) DataControl.AddNew();
                        if (DataControl != null) DataControl.Mode = Global.AccessMode.WriteMode;
                        break;
                    case "CANCELARALTA":
                        if (DataControl != null)
                        {
                            DataControl.CancelEdit();
                            DataControl.Mode = Global.AccessMode.ReadMode;
                        }
                        else
                        {
                            CancelAll();
                        }

                        break;
                    case "GUARDAR":
                        Save();
                        break;
                    case "REFRESCAR":
                        Refrescar();
                        break;
                    case "ELIMINAR":
                        if (DataControl != null)
                            DataControl.Delete();
                        break;
                    case "EDITAR":
                        if (DataControl != null)
                            DataControl.Mode = Global.AccessMode.WriteMode;
                        else
                            EditAll();
                        break;
                    case "GO":
                        if (DataControl != null)
                        {
                            var pos = 0;
                            var ibr = InputBox.ShowDialog("Ir a:", "", "", InputBox.Icon.Question, InputBox.Buttons.Ok, InputBox.Type.TextBox);
                            pos = Convert.ToInt32(InputBox.ResultValue);
                            if (pos != 0)
                                if ((pos >= HScroll1.Minimum) & (pos <= HScroll1.Maximum + 1))
                                    HScroll1.Value = pos - 1;
                        }

                        break;
                    case "LISTADO":
                        if (DataControl != null)
                            DataControl.ShowList();
                        break;
                    case "REGISTRO":
                        if (DataControl != null)
                            DataControl.ShowRecord();
                        break;
                    case "CERRAR":
                        var findForm = FindForm();
                        if (findForm != null)
                            findForm.Close();
                        break;
                }


            if (null != ButtonClick)
                ButtonClick(sender, e);
        }


        public void CancelAll()
        {
            var findForm = FindForm();
            if (findForm != null) CancelAllDbControls(findForm.Controls);
        }


        public void EditAll()
        {
            WriteModeAllDbControls(FindForm().Controls);
        }


        public void SaveAll()
        {
            var findForm = FindForm();
            if (findForm != null)
                SaveAllDbControls(findForm.Controls);
        }


        public void Save()
        {
            SaveAll();

            //if (DataControl != null)
            //{
            //    DataControl.Save();
            //    Form findForm = FindForm();
            //    if (findForm != null && !(DataControl.RelationSaveError(findForm.Controls)))
            //    {
            //        DataControl.Mode = Global.AccessMode.ReadMode;
            //    }
            //}
            //else
            //{
            //    SaveAll();
            //}
        }


        public void Refrescar()
        {
            if (DataControl == null)
                return;
            DataControl.ReConnect();
            DataControl.Go(0);
        }


        private bool CancelAllDbControls(ControlCollection frm)
        {
            if (frm == null) return false;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    CancelAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                    {
                        ((DBControl) ctr).CancelEdit();
                        ((DBControl) ctr).Mode = Global.AccessMode.ReadMode;
                    }
                }

            return false;
        }


        private void WriteModeAllDbControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    WriteModeAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).Mode = Global.AccessMode.WriteMode;
                }
        }


        private void SaveAllDbControls(ControlCollection frm)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    SaveAllDbControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                    {
                        ((DBControl) ctr).Save();

                        if (!((DBControl) ctr).RelationSaveError(FindForm().Controls))
                            ((DBControl) ctr).Mode = Global.AccessMode.ReadMode;
                    }
                }
        }


        private void HScroll1_ValueChanged(object sender, EventArgs e)
        {
            if (DataControl == null) return;
            if (HScroll1.Value != -1)
            {
                DataControl.Go(HScroll1.Value);
                ShowRecordLabel();
            }

            if (null != Change) Change();
        }


        private void ShowRecordLabel()
        {
            lblReg.Text = (int)(HScroll1.Value + 1) + " / " + (int)(HScroll1.Maximum + 1);
        }


        private void ShowToolBar()
        {
            if (DataControl != null)
            {
                HScroll1.SmallChange = 1;
                HScroll1.Minimum = 0;
                HScroll1.Maximum = DataControl.RecordCount() - 1 == -1 ? 0 : DataControl.RecordCount() - 1;

                HScroll1.LargeChange = DataControl.RecordCount() - 1 < 10 ? 1 : 10;
                ShowRecordLabel();
            }


            Height = tbrRegistros.Height;
            Width = tbrRegistros.Width;

            HScroll1.Enabled = m_AllowNavigate;
            HScroll1.Visible = m_ShowScrollBar;

            tbrRegistros.Items[6].Enabled = m_AllowNavigate;
            tbrRegistros.Items[7].Enabled = m_AllowNavigate;
            tbrRegistros.Items[8].Enabled = m_AllowNavigate;
            tbrRegistros.Items[9].Enabled = m_AllowNavigate;
            
            tbrRegistros.Items[1].Enabled = m_AllowSearch;
            tbrRegistros.Items[2].Enabled = m_AllowSearch;
            tbrRegistros.Items[15].Enabled = m_AllowSave;
            tbrRegistros.Items[14].Enabled = m_AllowCancel;
            tbrRegistros.Items[11].Enabled = m_AllowAddNew;
            tbrRegistros.Items[24].Enabled = m_AllowPrint;
            tbrRegistros.Items[4].Enabled = m_AllowFilter;
            tbrRegistros.Items[22].Enabled = m_AllowRecord;
            tbrRegistros.Items[12].Enabled = m_AllowEdit;
            tbrRegistros.Items[18].Enabled = m_AllowDelete;
            tbrRegistros.Items[21].Enabled = m_AllowList;
            tbrRegistros.Items[26].Enabled = m_AllowClose;

            tbrRegistros.Items[6].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[7].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[8].Visible = m_ShowNavigateButton;
            tbrRegistros.Items[9].Visible = m_ShowNavigateButton;
            
            tbrRegistros.Items[1].Visible = m_ShowSearchButton;
            tbrRegistros.Items[2].Visible = m_ShowSearchButton;
            tbrRegistros.Items[15].Visible = m_ShowSaveButton;
            tbrRegistros.Items[14].Visible = m_ShowCancelButton;
            tbrRegistros.Items[11].Visible = m_ShowAddNewButton;
            tbrRegistros.Items[24].Visible = m_ShowPrintButton;
            tbrRegistros.Items[4].Visible = m_ShowFilterButton;
            tbrRegistros.Items[22].Visible = m_ShowRecordButton;
            tbrRegistros.Items[12].Visible = m_ShowEditButton;
            tbrRegistros.Items[18].Visible = m_ShowDeleteButton;
            tbrRegistros.Items[21].Visible = m_ShowListButton;
            tbrRegistros.Items[26].Visible = m_ShowCloseButton;

            //HideSeparatorDuplicates();
            UpdateButtons();
        }


        //private void HideSeparatorDuplicates()
        //{
        //    ToolStripButton lastVisible = null;
        //    var ant = false;

        //    foreach (ToolStripButton button in tbrRegistros.Items)
        //        if (button.Visible)
        //        {
        //            if (button.Style == ToolBarButtonStyle.Separator)
        //            {
        //                if (ant) 
        //                    button.Visible = false;

        //                ant = true;
        //            }
        //            else
        //            {
        //                ant = false;
        //            }

        //            lastVisible = button;
        //        }

        //    if (lastVisible != null)
        //        if (lastVisible.Style == ToolBarButtonStyle.Separator)
        //            lastVisible.Visible = false;
        //}

        private void UpdateButtons()
        {
            //if(m_ShowText)
            //    tbrRegistros.TextAlign = ToolBarTextAlign.Underneath;
            //else
            //    tbrRegistros.TextAlign = ToolBarTextAlign.Right;

            foreach (ToolStripButton button in tbrRegistros.Items)
            {
                if (m_ShowText)
                {
                    button.Text = button.ToolTipText;
                }
                else
                {
                    button.Text = "";
                }
            }
        }


        private void m_dbcontrol_ChangeRecord()
        {
            ShowRecordLabel();
        }

        #region Delegates

        public delegate void ButtonClickEventHandler(object sender, EventArgs e);

        public delegate void ChangeEventHandler();

        #endregion


        #region '" Código generado por el Diseñador de Windows Forms "' 

        private HScrollBar HScroll1;
        internal ToolStripButton Separador8;
        internal ToolStripButton ToolBarButton1;
        internal ToolStripButton ToolBarButton10;
        internal ToolStripButton ToolBarButton11;
        internal ToolStripButton ToolBarButton12;
        internal ToolStripButton ToolBarButton13;
        internal ToolStripButton ToolBarButton14;
        internal ToolStripButton ToolBarButton15;
        internal ToolStripButton ToolBarButton16;
        internal ToolStripButton ToolBarButton17;
        internal ToolStripButton ToolBarButton18;
        internal ToolStripButton ToolBarButton19;
        internal ToolStripButton ToolBarButton2;
        internal ToolStripButton ToolBarButton20;
        internal ToolStripButton ToolBarButton21;
        internal ToolStripButton ToolBarButton22;
        internal ToolStripButton ToolBarButton23;
        internal ToolStripButton ToolBarButton24;
        internal ToolStripButton ToolBarButton25;
        internal ToolStripButton ToolBarButton26;
        internal ToolStripButton ToolBarButton3;
        internal ToolStripButton ToolBarButton4;
        internal ToolStripButton ToolBarButton5;
        internal ToolStripButton ToolBarButton7;
        internal ToolStripButton ToolBarButton8;
        internal ToolStripButton ToolBarButton9;
        private IContainer components;
        internal ImageList imgStandard;
        internal ImageList imgXPToolBarBIG;
        internal ImageList imgXPToolbar;
        internal Label lblReg;
        internal ToolStrip tbrRegistros;

        public DBToolBarEx()
        {
            Init();
        }

        public DBToolBarEx(string name)
        {
            Name = name;
            Init();
        }

        private void Init()
        {
            InitializeComponent();

            TabStop = false;

            HScroll1.ValueChanged += HScroll1_ValueChanged;
            tbrRegistros.ItemClicked += tbrRegistrosBIG_ButtonClick;

            Resize += DBToolBarEx_Resize;
            Load += DBToolBarEx_Load;
            Paint += DBToolBarEx_Paint;
        }

        private void DBToolBarEx_Paint(object sender, PaintEventArgs e)
        {
            ShowToolBar();
        }

        private void DBToolBarEx_Load(object sender, EventArgs e)
        {
            ShowToolBar();
        }

        private void DBToolBarEx_Resize(object sender, EventArgs e)
        {
            ShowToolBar();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBToolBarEx));
            this.imgXPToolbar = new System.Windows.Forms.ImageList(this.components);
            this.Separador8 = new System.Windows.Forms.ToolStripButton();
            this.HScroll1 = new System.Windows.Forms.HScrollBar();
            this.tbrRegistros = new System.Windows.Forms.ToolStrip();
            this.ToolBarButton1 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton7 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton8 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton9 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton10 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton11 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton12 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton13 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton14 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton15 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton16 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton17 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton84 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton18 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton19 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton20 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton21 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton22 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton23 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton24 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton25 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton26 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton77 = new System.Windows.Forms.ToolStripButton();
            this.ToolBarButton78 = new System.Windows.Forms.ToolStripButton();
            this.imgXPToolBarBIG = new System.Windows.Forms.ImageList(this.components);
            this.lblReg = new System.Windows.Forms.Label();
            this.imgStandard = new System.Windows.Forms.ImageList(this.components);
            this.toolBarButton87 = new System.Windows.Forms.ToolStripButton();
            this.SuspendLayout();
            // 
            // imgXPToolbar
            // 
            this.imgXPToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgXPToolbar.ImageStream")));
            this.imgXPToolbar.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imgXPToolbar.Images.SetKeyName(0, "");
            this.imgXPToolbar.Images.SetKeyName(1, "");
            this.imgXPToolbar.Images.SetKeyName(2, "");
            this.imgXPToolbar.Images.SetKeyName(3, "");
            this.imgXPToolbar.Images.SetKeyName(4, "");
            this.imgXPToolbar.Images.SetKeyName(5, "");
            this.imgXPToolbar.Images.SetKeyName(6, "");
            this.imgXPToolbar.Images.SetKeyName(7, "");
            this.imgXPToolbar.Images.SetKeyName(8, "");
            this.imgXPToolbar.Images.SetKeyName(9, "");
            this.imgXPToolbar.Images.SetKeyName(10, "");
            this.imgXPToolbar.Images.SetKeyName(11, "");
            this.imgXPToolbar.Images.SetKeyName(12, "");
            this.imgXPToolbar.Images.SetKeyName(13, "");
            this.imgXPToolbar.Images.SetKeyName(14, "");
            // 
            // Separador8
            // 
            this.Separador8.Name = "Separador8";
            //this.Separador8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // HScroll1
            // 
            this.HScroll1.Location = new System.Drawing.Point(1034, 12);
            this.HScroll1.Name = "HScroll1";
            this.HScroll1.Size = new System.Drawing.Size(104, 16);
            this.HScroll1.TabIndex = 10;
            // 
            // tbrRegistros
            // 
            //this.tbrRegistros.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbrRegistros.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.ToolBarButton1,
            this.ToolBarButton2,
            this.ToolBarButton3,
            this.ToolBarButton4,
            this.ToolBarButton5,
            this.ToolBarButton7,
            this.ToolBarButton8,
            this.ToolBarButton9,
            this.ToolBarButton10,
            this.ToolBarButton11,
            this.ToolBarButton12,
            this.ToolBarButton13,
            this.ToolBarButton14,
            this.ToolBarButton15,
            this.ToolBarButton16,
            this.ToolBarButton17,
            this.ToolBarButton84,
            this.ToolBarButton18,
            this.ToolBarButton19,
            this.ToolBarButton20,
            this.ToolBarButton21,
            this.ToolBarButton22,
            this.ToolBarButton23,
            this.ToolBarButton24,
            this.ToolBarButton25,
            this.ToolBarButton26,
            this.ToolBarButton77,
            this.ToolBarButton78});
            //this.tbrRegistros.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrRegistros.Dock = System.Windows.Forms.DockStyle.None;
            //this.tbrRegistros.DropDownArrows = true;
            this.tbrRegistros.ImageList = this.imgXPToolBarBIG;
            this.tbrRegistros.Location = new System.Drawing.Point(0, 0);
            this.tbrRegistros.Name = "tbrRegistros";
            this.tbrRegistros.ShowItemToolTips = true;
            this.tbrRegistros.Size = new System.Drawing.Size(1020, 50);
            this.tbrRegistros.TabIndex = 12;
            // 
            // ToolBarButton1
            // 
            this.ToolBarButton1.Name = "ToolBarButton1";
            //this.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.ImageIndex = 0;
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Tag = "BUSCAR";
            this.ToolBarButton2.Text = "Buscar";
            this.ToolBarButton2.ToolTipText = "Buscar";
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.ImageIndex = 1;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Tag = "BUSCARSIGUIENTE";
            this.ToolBarButton3.Text = "Sig...";
            this.ToolBarButton3.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.Name = "ToolBarButton4";
            //this.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 2;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Tag = "ESTABLECERFILTROS";
            this.ToolBarButton5.Text = "Filtro";
            this.ToolBarButton5.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton7
            // 
            this.ToolBarButton7.Name = "ToolBarButton7";
            //this.ToolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton8
            // 
            this.ToolBarButton8.ImageIndex = 3;
            this.ToolBarButton8.Name = "ToolBarButton8";
            this.ToolBarButton8.Tag = "MOVERPRIMERO";
            this.ToolBarButton8.Text = "Inicio";
            this.ToolBarButton8.ToolTipText = "Primero";
            // 
            // ToolBarButton9
            // 
            this.ToolBarButton9.ImageIndex = 4;
            this.ToolBarButton9.Name = "ToolBarButton9";
            this.ToolBarButton9.Tag = "MOVERANTERIOR";
            this.ToolBarButton9.Text = "Atrás";
            this.ToolBarButton9.ToolTipText = "Anterior";
            // 
            // ToolBarButton10
            // 
            this.ToolBarButton10.ImageIndex = 5;
            this.ToolBarButton10.Name = "ToolBarButton10";
            this.ToolBarButton10.Tag = "MOVERSIGUIENTE";
            this.ToolBarButton10.Text = "Sig...";
            this.ToolBarButton10.ToolTipText = "Siguiente";
            // 
            // ToolBarButton11
            // 
            this.ToolBarButton11.ImageIndex = 6;
            this.ToolBarButton11.Name = "ToolBarButton11";
            this.ToolBarButton11.Tag = "MOVERULTIMO";
            this.ToolBarButton11.Text = "Fin";
            this.ToolBarButton11.ToolTipText = "Fin";
            // 
            // ToolBarButton12
            // 
            this.ToolBarButton12.Name = "ToolBarButton12";
            //this.ToolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton13
            // 
            this.ToolBarButton13.ImageIndex = 7;
            this.ToolBarButton13.Name = "ToolBarButton13";
            this.ToolBarButton13.Tag = "NUEVO";
            this.ToolBarButton13.Text = "Nuevo";
            this.ToolBarButton13.ToolTipText = "Nuevo";
            // 
            // ToolBarButton14
            // 
            this.ToolBarButton14.ImageIndex = 8;
            this.ToolBarButton14.Name = "ToolBarButton14";
            this.ToolBarButton14.Tag = "EDITAR";
            this.ToolBarButton14.Text = "Editar";
            this.ToolBarButton14.ToolTipText = "Editar";
            // 
            // ToolBarButton15
            // 
            this.ToolBarButton15.Name = "ToolBarButton15";
            //this.ToolBarButton15.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton16
            // 
            this.ToolBarButton16.ImageIndex = 9;
            this.ToolBarButton16.Name = "ToolBarButton16";
            this.ToolBarButton16.Tag = "CANCELARALTA";
            this.ToolBarButton16.Text = "Canc...";
            this.ToolBarButton16.ToolTipText = "Canc...";
            // 
            // ToolBarButton17
            // 
            this.ToolBarButton17.ImageIndex = 10;
            this.ToolBarButton17.Name = "ToolBarButton17";
            this.ToolBarButton17.Tag = "GUARDAR";
            this.ToolBarButton17.Text = "Salvar";
            this.ToolBarButton17.ToolTipText = "Salvar";
            // 
            // ToolBarButton84
            // 
            this.ToolBarButton84.ImageIndex = 11;
            this.ToolBarButton84.Name = "ToolBarButton84";
            this.ToolBarButton84.Tag = "REFRESCAR";
            this.ToolBarButton84.Text = "Refrescar";
            this.ToolBarButton84.ToolTipText = "Refrescar";
            // 
            // ToolBarButton18
            // 
            this.ToolBarButton18.Name = "ToolBarButton18";
            //this.ToolBarButton18.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton19
            // 
            this.ToolBarButton19.ImageIndex = 12;
            this.ToolBarButton19.Name = "ToolBarButton19";
            this.ToolBarButton19.Tag = "ELIMINAR";
            this.ToolBarButton19.Text = "Borrar";
            this.ToolBarButton19.ToolTipText = "Borrar";
            // 
            // ToolBarButton20
            // 
            this.ToolBarButton20.Name = "ToolBarButton20";
            //this.ToolBarButton20.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton21
            // 
            this.ToolBarButton21.ImageIndex = 13;
            this.ToolBarButton21.Name = "ToolBarButton21";
            this.ToolBarButton21.Tag = "GO";
            this.ToolBarButton21.Text = "Ir a";
            this.ToolBarButton21.ToolTipText = "Ir a";
            this.ToolBarButton21.Visible = false;
            // 
            // ToolBarButton22
            // 
            this.ToolBarButton22.ImageIndex = 13;
            this.ToolBarButton22.Name = "ToolBarButton22";
            this.ToolBarButton22.Tag = "LISTADO";
            this.ToolBarButton22.Text = "List...";
            this.ToolBarButton22.ToolTipText = "Listado";
            // 
            // ToolBarButton23
            // 
            this.ToolBarButton23.ImageIndex = 14;
            this.ToolBarButton23.Name = "ToolBarButton23";
            this.ToolBarButton23.Tag = "REGISTRO";
            this.ToolBarButton23.Text = "Reg...";
            this.ToolBarButton23.ToolTipText = "Registro";
            // 
            // ToolBarButton24
            // 
            this.ToolBarButton24.Name = "ToolBarButton24";
            //this.ToolBarButton24.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton25
            // 
            this.ToolBarButton25.ImageIndex = 18;
            this.ToolBarButton25.Name = "ToolBarButton25";
            this.ToolBarButton25.Tag = "IMPRIMIR";
            this.ToolBarButton25.Text = "Impr...";
            this.ToolBarButton25.ToolTipText = "Imprimir";
            // 
            // ToolBarButton26
            // 
            this.ToolBarButton26.Name = "ToolBarButton26";
            //this.ToolBarButton26.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton77
            // 
            this.ToolBarButton77.ImageIndex = 9;
            this.ToolBarButton77.Name = "ToolBarButton77";
            this.ToolBarButton77.Tag = "CERRAR";
            this.ToolBarButton77.Text = "Cerrar";
            this.ToolBarButton77.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton78
            // 
            this.ToolBarButton78.Name = "ToolBarButton78";
            //this.ToolBarButton78.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // imgXPToolBarBIG
            // 
            this.imgXPToolBarBIG.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgXPToolBarBIG.ImageStream")));
            this.imgXPToolBarBIG.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imgXPToolBarBIG.Images.SetKeyName(0, "");
            this.imgXPToolBarBIG.Images.SetKeyName(1, "");
            this.imgXPToolBarBIG.Images.SetKeyName(2, "");
            this.imgXPToolBarBIG.Images.SetKeyName(3, "");
            this.imgXPToolBarBIG.Images.SetKeyName(4, "");
            this.imgXPToolBarBIG.Images.SetKeyName(5, "");
            this.imgXPToolBarBIG.Images.SetKeyName(6, "");
            this.imgXPToolBarBIG.Images.SetKeyName(7, "");
            this.imgXPToolBarBIG.Images.SetKeyName(8, "");
            this.imgXPToolBarBIG.Images.SetKeyName(9, "");
            this.imgXPToolBarBIG.Images.SetKeyName(10, "");
            this.imgXPToolBarBIG.Images.SetKeyName(11, "");
            this.imgXPToolBarBIG.Images.SetKeyName(12, "");
            this.imgXPToolBarBIG.Images.SetKeyName(13, "");
            this.imgXPToolBarBIG.Images.SetKeyName(14, "");
            this.imgXPToolBarBIG.Images.SetKeyName(15, "");
            this.imgXPToolBarBIG.Images.SetKeyName(16, "");
            this.imgXPToolBarBIG.Images.SetKeyName(17, "");
            this.imgXPToolBarBIG.Images.SetKeyName(18, "");
            // 
            // lblReg
            // 
            this.lblReg.AutoSize = true;
            this.lblReg.Location = new System.Drawing.Point(1078, 28);
            this.lblReg.Name = "lblReg";
            this.lblReg.Size = new System.Drawing.Size(24, 13);
            this.lblReg.TabIndex = 13;
            this.lblReg.Text = "0/0";
            // 
            // imgStandard
            // 
            this.imgStandard.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgStandard.ImageStream")));
            this.imgStandard.TransparentColor = System.Drawing.Color.Transparent;
            this.imgStandard.Images.SetKeyName(0, "");
            this.imgStandard.Images.SetKeyName(1, "");
            this.imgStandard.Images.SetKeyName(2, "");
            this.imgStandard.Images.SetKeyName(3, "");
            this.imgStandard.Images.SetKeyName(4, "");
            this.imgStandard.Images.SetKeyName(5, "");
            this.imgStandard.Images.SetKeyName(6, "");
            this.imgStandard.Images.SetKeyName(7, "");
            this.imgStandard.Images.SetKeyName(8, "");
            this.imgStandard.Images.SetKeyName(9, "");
            this.imgStandard.Images.SetKeyName(10, "");
            this.imgStandard.Images.SetKeyName(11, "");
            this.imgStandard.Images.SetKeyName(12, "");
            this.imgStandard.Images.SetKeyName(13, "");
            this.imgStandard.Images.SetKeyName(14, "");
            // 
            // toolBarButton87
            // 
            this.toolBarButton87.Name = "toolBarButton87";
            // 
            // DBToolBarEx
            // 
            this.Controls.Add(this.lblReg);
            this.Controls.Add(this.tbrRegistros);
            this.Controls.Add(this.HScroll1);
            this.Name = "DBToolBarEx";
            this.Size = new System.Drawing.Size(1175, 65);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}