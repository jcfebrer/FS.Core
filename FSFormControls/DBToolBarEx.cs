#region

using FSException;
using FSLibrary;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBToolBar.bmp")]
    [ToolboxItem(true)]
    public class DBToolBarEx : DBUserControlBase
    {
        #region tToolbar enum

        public enum tToolbar
        {
            ToolbarOffice,
            ToolbarOfficeBIG,
            ToolbarXP,
            ToolbarXPBig
        }

        #endregion


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

        public ToolBar m_Toolbar;
        private tToolbar m_ToolBarType = tToolbar.ToolbarXPBig;

        internal ToolBarButton ToolBarButton77;
        internal ToolBarButton ToolBarButton78;
        internal ToolBarButton ToolBarButton79;
        internal ToolBarButton ToolBarButton80;
        internal ToolBarButton ToolBarButton81;
        internal ToolBarButton ToolBarButton82;
        internal ToolBarButton ToolBarButton83;
        internal ToolBarButton ToolBarButton84;
        internal ToolBarButton ToolBarButton85;
        private ToolBarButton toolBarButton86;

        public int Value
        {
            get { return HScroll1.Value; }
            set
            {
                if (DataControl == null) return;
                try
                {
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
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }
            }
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


        public tToolbar ToolBarType
        {
            get { return m_ToolBarType; }
            set
            {
                m_ToolBarType = value;
                ShowToolBar();
            }
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
        public event ButtonClickEventHandler ButtonClick;

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

                PonReg();
            }
        }


        private void tbrRegistros_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolbarOptions(sender, e);
        }

        private void tbrRegistrosBIG_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolbarOptions(sender, e);
        }


        private void ToolbarOptions(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button != null && e.Button.Tag != null)
                switch (e.Button.Tag.ToString().ToUpper())
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
                        if (DataControl != null) DataControl.Mode = AccessMode.WriteMode;
                        break;
                    case "CANCELARALTA":
                        if (DataControl != null)
                        {
                            DataControl.CancelEdit();
                            DataControl.Mode = AccessMode.ReadMode;
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
                        if (DataControl != null) DataControl.Delete();
                        break;
                    case "EDITAR":
                        if (DataControl != null)
                            DataControl.Mode = AccessMode.WriteMode;
                        else
                            EditAll();
                        break;
                    case "GO":
                        if (DataControl != null)
                        {
                            var pos = 0;
                            var ibr = InputBox.Show("Ir a:");
                            pos = Convert.ToInt32(ibr);
                            if (pos != 0)
                                if ((pos >= HScroll1.Minimum) & (pos <= HScroll1.Maximum + 1))
                                    HScroll1.Value = pos - 1;
                        }

                        break;
                    case "LISTADO":
                        if (DataControl != null) DataControl.ShowList();
                        break;
                    case "REGISTRO":
                        if (DataControl != null) DataControl.ShowRecord();
                        break;
                    case "CERRAR":
                        var findForm = FindForm();
                        if (findForm != null) findForm.Close();
                        break;
                }


            if (null != ButtonClick) ButtonClick(sender, e);
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
            //        DataControl.Mode = DBUsercontrolBase.AccessMode.ReadMode;
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
                        ((DBControl) ctr).Mode = AccessMode.ReadMode;
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
                    if (ctr is DBControl) ((DBControl) ctr).Mode = AccessMode.WriteMode;
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
                            ((DBControl) ctr).Mode = AccessMode.ReadMode;
                    }
                }
        }


        private void HScroll1_ValueChanged(object sender, EventArgs e)
        {
            if (DataControl == null) return;
            if (HScroll1.Value != -1)
            {
                DataControl.Go(HScroll1.Value);
                PonReg();
            }

            if (null != Change) Change();
        }


        private void PonReg()
        {
            lblReg.Text = HScroll1.Value + 1 + " / " + HScroll1.Maximum + 1;
        }


        private void ShowToolBar()
        {
            if (DataControl != null)
            {
                HScroll1.SmallChange = 1;
                HScroll1.Minimum = 0;
                HScroll1.Maximum = DataControl.RecordCount() - 1 == -1 ? 0 : DataControl.RecordCount() - 1;

                HScroll1.LargeChange = DataControl.RecordCount() - 1 < 10 ? 1 : 10;
                PonReg();
            }


            tbrRegistros.Dock = DockStyle.Fill;
            tbrRegistrosBIG.Dock = DockStyle.Fill;
            tbrRegistrosStandard.Dock = DockStyle.Fill;
            tbrRegistrosStandardBIG.Dock = DockStyle.Fill;
            switch (m_ToolBarType)
            {
                case tToolbar.ToolbarOffice:
                    lblReg.Left = 590;
                    lblReg.Top = 4;
                    HScroll1.Left = 480;
                    HScroll1.Top = 4;
                    HScroll1.Height = 16;

                    tbrRegistros.Visible = false;
                    tbrRegistrosBIG.Visible = false;
                    tbrRegistrosStandard.Visible = true;
                    tbrRegistrosStandardBIG.Visible = false;

                    Height = tbrRegistrosStandard.Height;
                    Width = tbrRegistrosStandard.Width;
                    m_Toolbar = tbrRegistrosStandard;
                    break;
                case tToolbar.ToolbarXP:
                    lblReg.Left = 590;
                    lblReg.Top = 4;
                    HScroll1.Left = 480;
                    HScroll1.Top = 4;
                    HScroll1.Height = 16;

                    tbrRegistros.Visible = true;
                    tbrRegistrosBIG.Visible = false;
                    tbrRegistrosStandard.Visible = false;
                    tbrRegistrosStandardBIG.Visible = false;

                    Height = tbrRegistros.Height;
                    Width = tbrRegistros.Width;
                    m_Toolbar = tbrRegistros;
                    break;
                case tToolbar.ToolbarOfficeBIG:
                    lblReg.Left = 850;
                    lblReg.Top = 14;
                    HScroll1.Left = 740;
                    HScroll1.Top = 10;
                    HScroll1.Height = 25;

                    tbrRegistros.Visible = false;
                    tbrRegistrosBIG.Visible = false;
                    tbrRegistrosStandard.Visible = false;
                    tbrRegistrosStandardBIG.Visible = true;

                    Height = tbrRegistrosStandardBIG.Height;
                    Width = tbrRegistrosStandardBIG.Width;
                    m_Toolbar = tbrRegistrosStandardBIG;
                    break;
                case tToolbar.ToolbarXPBig:
                    lblReg.Left = 850;
                    lblReg.Top = 14;
                    HScroll1.Left = 740;
                    HScroll1.Top = 10;
                    HScroll1.Height = 25;

                    tbrRegistros.Visible = false;
                    tbrRegistrosBIG.Visible = true;
                    tbrRegistrosStandard.Visible = false;
                    tbrRegistrosStandardBIG.Visible = false;

                    Height = tbrRegistrosBIG.Height;
                    Width = tbrRegistrosBIG.Width;
                    m_Toolbar = tbrRegistrosBIG;
                    break;
            }


            m_Toolbar.Buttons[6].Enabled = m_AllowNavigate;
            m_Toolbar.Buttons[7].Enabled = m_AllowNavigate;
            m_Toolbar.Buttons[8].Enabled = m_AllowNavigate;
            m_Toolbar.Buttons[9].Enabled = m_AllowNavigate;
            HScroll1.Enabled = m_AllowNavigate;
            m_Toolbar.Buttons[1].Enabled = m_AllowSearch;
            m_Toolbar.Buttons[2].Enabled = m_AllowSearch;
            m_Toolbar.Buttons[15].Enabled = m_AllowSave;
            m_Toolbar.Buttons[14].Enabled = m_AllowCancel;
            m_Toolbar.Buttons[11].Enabled = m_AllowAddNew;
            m_Toolbar.Buttons[24].Enabled = m_AllowPrint;
            m_Toolbar.Buttons[4].Enabled = m_AllowFilter;
            m_Toolbar.Buttons[22].Enabled = m_AllowRecord;
            m_Toolbar.Buttons[12].Enabled = m_AllowEdit;
            m_Toolbar.Buttons[18].Enabled = m_AllowDelete;
            m_Toolbar.Buttons[21].Enabled = m_AllowList;
            m_Toolbar.Buttons[26].Enabled = m_AllowClose;

            m_Toolbar.Buttons[6].Visible = m_ShowNavigateButton;
            m_Toolbar.Buttons[7].Visible = m_ShowNavigateButton;
            m_Toolbar.Buttons[8].Visible = m_ShowNavigateButton;
            m_Toolbar.Buttons[9].Visible = m_ShowNavigateButton;
            HScroll1.Visible = m_ShowScrollBar;
            m_Toolbar.Buttons[1].Visible = m_ShowSearchButton;
            m_Toolbar.Buttons[2].Visible = m_ShowSearchButton;
            m_Toolbar.Buttons[15].Visible = m_ShowSaveButton;
            m_Toolbar.Buttons[14].Visible = m_ShowCancelButton;
            m_Toolbar.Buttons[11].Visible = m_ShowAddNewButton;
            m_Toolbar.Buttons[24].Visible = m_ShowPrintButton;
            m_Toolbar.Buttons[4].Visible = m_ShowFilterButton;
            m_Toolbar.Buttons[22].Visible = m_ShowRecordButton;
            m_Toolbar.Buttons[12].Visible = m_ShowEditButton;
            m_Toolbar.Buttons[18].Visible = m_ShowDeleteButton;
            m_Toolbar.Buttons[21].Visible = m_ShowListButton;
            m_Toolbar.Buttons[26].Visible = m_ShowCloseButton;

            HideSeparatorDuplicates();
        }


        private void HideSeparatorDuplicates()
        {
            ToolBarButton lastVisible = null;
            var ant = false;

            foreach (ToolBarButton b in m_Toolbar.Buttons)
                if (b.Visible)
                {
                    if (b.Style == ToolBarButtonStyle.Separator)
                    {
                        if (ant) b.Visible = false;

                        ant = true;
                    }
                    else
                    {
                        ant = false;
                    }

                    lastVisible = b;
                }

            if (lastVisible != null)
                if (lastVisible.Style == ToolBarButtonStyle.Separator)
                    lastVisible.Visible = false;
        }

        private void DBToolBar_Paint(object sender, PaintEventArgs e)
        {
            //ShowToolBar(); 
        }


        private void DBToolBar_Load(object sender, EventArgs e)
        {
            ShowToolBar();
        }


        private void m_dbcontrol_ChangeRecord()
        {
            PonReg();
        }

        #region Delegates

        public delegate void ButtonClickEventHandler(object sender, ToolBarButtonClickEventArgs e);

        public delegate void ChangeEventHandler();

        #endregion


        #region '" Código generado por el Diseñador de Windows Forms "' 

        private HScrollBar HScroll1;
        internal ToolBarButton Separador1;
        internal ToolBarButton Separador2;
        internal ToolBarButton Separador3;
        internal ToolBarButton Separador4;
        internal ToolBarButton Separador5;
        internal ToolBarButton Separador6;
        internal ToolBarButton Separador7;
        internal ToolBarButton Separador8;
        internal ToolBarButton Separator8;
        internal ToolBarButton ToolBarButton1;
        internal ToolBarButton ToolBarButton10;
        internal ToolBarButton ToolBarButton11;
        internal ToolBarButton ToolBarButton12;
        internal ToolBarButton ToolBarButton13;
        internal ToolBarButton ToolBarButton14;
        internal ToolBarButton ToolBarButton15;
        internal ToolBarButton ToolBarButton16;
        internal ToolBarButton ToolBarButton17;
        internal ToolBarButton ToolBarButton18;
        internal ToolBarButton ToolBarButton19;
        internal ToolBarButton ToolBarButton2;
        internal ToolBarButton ToolBarButton20;
        internal ToolBarButton ToolBarButton21;
        internal ToolBarButton ToolBarButton22;
        internal ToolBarButton ToolBarButton23;
        internal ToolBarButton ToolBarButton24;
        internal ToolBarButton ToolBarButton25;
        internal ToolBarButton ToolBarButton26;
        internal ToolBarButton ToolBarButton27;
        internal ToolBarButton ToolBarButton28;
        internal ToolBarButton ToolBarButton29;
        internal ToolBarButton ToolBarButton3;
        internal ToolBarButton ToolBarButton30;
        internal ToolBarButton ToolBarButton31;
        internal ToolBarButton ToolBarButton32;
        internal ToolBarButton ToolBarButton33;
        internal ToolBarButton ToolBarButton34;
        internal ToolBarButton ToolBarButton35;
        internal ToolBarButton ToolBarButton36;
        internal ToolBarButton ToolBarButton37;
        internal ToolBarButton ToolBarButton38;
        internal ToolBarButton ToolBarButton39;
        internal ToolBarButton ToolBarButton4;
        internal ToolBarButton ToolBarButton40;
        internal ToolBarButton ToolBarButton41;
        internal ToolBarButton ToolBarButton42;
        internal ToolBarButton ToolBarButton43;
        internal ToolBarButton ToolBarButton44;
        internal ToolBarButton ToolBarButton45;
        internal ToolBarButton ToolBarButton46;
        internal ToolBarButton ToolBarButton47;
        internal ToolBarButton ToolBarButton48;
        internal ToolBarButton ToolBarButton49;
        internal ToolBarButton ToolBarButton5;
        internal ToolBarButton ToolBarButton50;
        internal ToolBarButton ToolBarButton51;
        internal ToolBarButton ToolBarButton52;
        internal ToolBarButton ToolBarButton53;
        internal ToolBarButton ToolBarButton54;
        internal ToolBarButton ToolBarButton55;
        internal ToolBarButton ToolBarButton56;
        internal ToolBarButton ToolBarButton57;
        internal ToolBarButton ToolBarButton58;
        internal ToolBarButton ToolBarButton59;
        internal ToolBarButton ToolBarButton6;
        internal ToolBarButton ToolBarButton60;
        internal ToolBarButton ToolBarButton61;
        internal ToolBarButton ToolBarButton62;
        internal ToolBarButton ToolBarButton63;
        internal ToolBarButton ToolBarButton64;
        internal ToolBarButton ToolBarButton65;
        internal ToolBarButton ToolBarButton66;
        internal ToolBarButton ToolBarButton67;
        internal ToolBarButton ToolBarButton68;
        internal ToolBarButton ToolBarButton69;
        internal ToolBarButton ToolBarButton7;
        internal ToolBarButton ToolBarButton70;
        internal ToolBarButton ToolBarButton71;
        internal ToolBarButton ToolBarButton72;
        internal ToolBarButton ToolBarButton73;
        internal ToolBarButton ToolBarButton74;
        internal ToolBarButton ToolBarButton75;
        internal ToolBarButton ToolBarButton76;
        internal ToolBarButton ToolBarButton8;
        internal ToolBarButton ToolBarButton9;
        internal ToolBarButton btlReport;
        internal ToolBarButton btnBuscar;
        internal ToolBarButton btnBuscarSiguiente;
        internal ToolBarButton btnCancelarAlta;
        internal ToolBarButton btnEditar;
        internal ToolBarButton btnEliminar;
        internal ToolBarButton btnEstablecerFiltros;
        internal ToolBarButton btnGo;
        internal ToolBarButton btnGuardar;
        internal ToolBarButton btnImprimir;
        internal ToolBarButton btnListado;
        internal ToolBarButton btnMoverAnterior;
        internal ToolBarButton btnMoverPrimero;
        internal ToolBarButton btnMoverSiguiente;
        internal ToolBarButton btnMoverUltimo;
        internal ToolBarButton btnNuevo;
        private IContainer components;
        internal ImageList imgStandard;
        internal ImageList imgXPToolBarBIG;
        internal ImageList imgXPToolbar;
        internal Label lblReg;
        private ToolBar tbrRegistros;
        internal ToolBar tbrRegistrosBIG;
        internal ToolBar tbrRegistrosStandard;
        internal ToolBar tbrRegistrosStandardBIG;

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

            tbrRegistros.ButtonClick += tbrRegistros_ButtonClick;
            HScroll1.ValueChanged += HScroll1_ValueChanged;
            tbrRegistrosBIG.ButtonClick += tbrRegistrosBIG_ButtonClick;
            Resize += DBToolBar_Resize;
        }

        private void DBToolBar_Resize(object sender, EventArgs e)
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
            components = new Container();
            var resources = new ComponentResourceManager(typeof(DBToolBarEx));
            tbrRegistros = new ToolBar();
            Separador1 = new ToolBarButton();
            btnBuscar = new ToolBarButton();
            btnBuscarSiguiente = new ToolBarButton();
            Separador2 = new ToolBarButton();
            btnEstablecerFiltros = new ToolBarButton();
            Separador3 = new ToolBarButton();
            btnMoverPrimero = new ToolBarButton();
            btnMoverAnterior = new ToolBarButton();
            btnMoverSiguiente = new ToolBarButton();
            btnMoverUltimo = new ToolBarButton();
            Separador4 = new ToolBarButton();
            btnNuevo = new ToolBarButton();
            btnEditar = new ToolBarButton();
            Separador6 = new ToolBarButton();
            btnCancelarAlta = new ToolBarButton();
            btnGuardar = new ToolBarButton();
            ToolBarButton85 = new ToolBarButton();
            Separador5 = new ToolBarButton();
            btnEliminar = new ToolBarButton();
            Separador7 = new ToolBarButton();
            btnGo = new ToolBarButton();
            btnListado = new ToolBarButton();
            btlReport = new ToolBarButton();
            Separator8 = new ToolBarButton();
            btnImprimir = new ToolBarButton();
            ToolBarButton6 = new ToolBarButton();
            ToolBarButton79 = new ToolBarButton();
            ToolBarButton82 = new ToolBarButton();
            imgXPToolbar = new ImageList(components);
            Separador8 = new ToolBarButton();
            HScroll1 = new HScrollBar();
            tbrRegistrosBIG = new ToolBar();
            ToolBarButton1 = new ToolBarButton();
            ToolBarButton2 = new ToolBarButton();
            ToolBarButton3 = new ToolBarButton();
            ToolBarButton4 = new ToolBarButton();
            ToolBarButton5 = new ToolBarButton();
            ToolBarButton7 = new ToolBarButton();
            ToolBarButton8 = new ToolBarButton();
            ToolBarButton9 = new ToolBarButton();
            ToolBarButton10 = new ToolBarButton();
            ToolBarButton11 = new ToolBarButton();
            ToolBarButton12 = new ToolBarButton();
            ToolBarButton13 = new ToolBarButton();
            ToolBarButton14 = new ToolBarButton();
            ToolBarButton15 = new ToolBarButton();
            ToolBarButton16 = new ToolBarButton();
            ToolBarButton17 = new ToolBarButton();
            ToolBarButton84 = new ToolBarButton();
            ToolBarButton18 = new ToolBarButton();
            ToolBarButton19 = new ToolBarButton();
            ToolBarButton20 = new ToolBarButton();
            ToolBarButton21 = new ToolBarButton();
            ToolBarButton22 = new ToolBarButton();
            ToolBarButton23 = new ToolBarButton();
            ToolBarButton24 = new ToolBarButton();
            ToolBarButton25 = new ToolBarButton();
            ToolBarButton26 = new ToolBarButton();
            ToolBarButton77 = new ToolBarButton();
            ToolBarButton78 = new ToolBarButton();
            imgXPToolBarBIG = new ImageList(components);
            lblReg = new Label();
            imgStandard = new ImageList(components);
            tbrRegistrosStandardBIG = new ToolBar();
            ToolBarButton27 = new ToolBarButton();
            ToolBarButton28 = new ToolBarButton();
            ToolBarButton29 = new ToolBarButton();
            ToolBarButton30 = new ToolBarButton();
            ToolBarButton31 = new ToolBarButton();
            ToolBarButton32 = new ToolBarButton();
            ToolBarButton33 = new ToolBarButton();
            ToolBarButton34 = new ToolBarButton();
            ToolBarButton35 = new ToolBarButton();
            ToolBarButton36 = new ToolBarButton();
            ToolBarButton37 = new ToolBarButton();
            ToolBarButton38 = new ToolBarButton();
            ToolBarButton39 = new ToolBarButton();
            ToolBarButton40 = new ToolBarButton();
            ToolBarButton41 = new ToolBarButton();
            ToolBarButton42 = new ToolBarButton();
            ToolBarButton43 = new ToolBarButton();
            ToolBarButton44 = new ToolBarButton();
            ToolBarButton45 = new ToolBarButton();
            ToolBarButton46 = new ToolBarButton();
            ToolBarButton47 = new ToolBarButton();
            ToolBarButton48 = new ToolBarButton();
            ToolBarButton49 = new ToolBarButton();
            ToolBarButton50 = new ToolBarButton();
            ToolBarButton51 = new ToolBarButton();
            ToolBarButton83 = new ToolBarButton();
            toolBarButton86 = new ToolBarButton();
            tbrRegistrosStandard = new ToolBar();
            ToolBarButton52 = new ToolBarButton();
            ToolBarButton53 = new ToolBarButton();
            ToolBarButton54 = new ToolBarButton();
            ToolBarButton55 = new ToolBarButton();
            ToolBarButton56 = new ToolBarButton();
            ToolBarButton57 = new ToolBarButton();
            ToolBarButton58 = new ToolBarButton();
            ToolBarButton59 = new ToolBarButton();
            ToolBarButton60 = new ToolBarButton();
            ToolBarButton61 = new ToolBarButton();
            ToolBarButton62 = new ToolBarButton();
            ToolBarButton63 = new ToolBarButton();
            ToolBarButton64 = new ToolBarButton();
            ToolBarButton65 = new ToolBarButton();
            ToolBarButton66 = new ToolBarButton();
            ToolBarButton67 = new ToolBarButton();
            ToolBarButton68 = new ToolBarButton();
            ToolBarButton69 = new ToolBarButton();
            ToolBarButton70 = new ToolBarButton();
            ToolBarButton71 = new ToolBarButton();
            ToolBarButton72 = new ToolBarButton();
            ToolBarButton73 = new ToolBarButton();
            ToolBarButton74 = new ToolBarButton();
            ToolBarButton75 = new ToolBarButton();
            ToolBarButton76 = new ToolBarButton();
            ToolBarButton80 = new ToolBarButton();
            ToolBarButton81 = new ToolBarButton();
            SuspendLayout();
            // 
            // tbrRegistros
            // 
            tbrRegistros.Appearance = ToolBarAppearance.Flat;
            tbrRegistros.Buttons.AddRange(new[]
            {
                Separador1,
                btnBuscar,
                btnBuscarSiguiente,
                Separador2,
                btnEstablecerFiltros,
                Separador3,
                btnMoverPrimero,
                btnMoverAnterior,
                btnMoverSiguiente,
                btnMoverUltimo,
                Separador4,
                btnNuevo,
                btnEditar,
                Separador6,
                btnCancelarAlta,
                btnGuardar,
                ToolBarButton85,
                Separador5,
                btnEliminar,
                Separador7,
                btnGo,
                btnListado,
                btlReport,
                Separator8,
                btnImprimir,
                ToolBarButton6,
                ToolBarButton79,
                ToolBarButton82
            });
            tbrRegistros.ButtonSize = new Size(16, 16);
            tbrRegistros.Dock = DockStyle.None;
            tbrRegistros.DropDownArrows = true;
            tbrRegistros.ImageList = imgXPToolbar;
            tbrRegistros.Location = new Point(24, 8);
            tbrRegistros.Name = "tbrRegistros";
            tbrRegistros.ShowToolTips = true;
            tbrRegistros.Size = new Size(900, 28);
            tbrRegistros.TabIndex = 9;
            // 
            // Separador1
            // 
            Separador1.Name = "Separador1";
            Separador1.Style = ToolBarButtonStyle.Separator;
            // 
            // btnBuscar
            // 
            btnBuscar.ImageIndex = 0;
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Tag = "BUSCAR";
            btnBuscar.ToolTipText = "Buscar";
            // 
            // btnBuscarSiguiente
            // 
            btnBuscarSiguiente.ImageIndex = 1;
            btnBuscarSiguiente.Name = "btnBuscarSiguiente";
            btnBuscarSiguiente.Tag = "BUSCARSIGUIENTE";
            btnBuscarSiguiente.ToolTipText = "Buscar siguiente";
            // 
            // Separador2
            // 
            Separador2.Name = "Separador2";
            Separador2.Style = ToolBarButtonStyle.Separator;
            // 
            // btnEstablecerFiltros
            // 
            btnEstablecerFiltros.ImageIndex = 2;
            btnEstablecerFiltros.Name = "btnEstablecerFiltros";
            btnEstablecerFiltros.Tag = "ESTABLECERFILTROS";
            btnEstablecerFiltros.ToolTipText = "Establecer filtro";
            // 
            // Separador3
            // 
            Separador3.Name = "Separador3";
            Separador3.Style = ToolBarButtonStyle.Separator;
            // 
            // btnMoverPrimero
            // 
            btnMoverPrimero.ImageIndex = 3;
            btnMoverPrimero.Name = "btnMoverPrimero";
            btnMoverPrimero.Tag = "MOVERPRIMERO";
            btnMoverPrimero.ToolTipText = "Primero";
            // 
            // btnMoverAnterior
            // 
            btnMoverAnterior.ImageIndex = 4;
            btnMoverAnterior.Name = "btnMoverAnterior";
            btnMoverAnterior.Tag = "MOVERANTERIOR";
            btnMoverAnterior.ToolTipText = "Anterior";
            // 
            // btnMoverSiguiente
            // 
            btnMoverSiguiente.ImageIndex = 5;
            btnMoverSiguiente.Name = "btnMoverSiguiente";
            btnMoverSiguiente.Tag = "MOVERSIGUIENTE";
            btnMoverSiguiente.ToolTipText = "Siguiente";
            // 
            // btnMoverUltimo
            // 
            btnMoverUltimo.ImageIndex = 6;
            btnMoverUltimo.Name = "btnMoverUltimo";
            btnMoverUltimo.Tag = "MOVERULTIMO";
            btnMoverUltimo.ToolTipText = "Último";
            // 
            // Separador4
            // 
            Separador4.Name = "Separador4";
            Separador4.Style = ToolBarButtonStyle.Separator;
            // 
            // btnNuevo
            // 
            btnNuevo.ImageIndex = 7;
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Tag = "NUEVO";
            btnNuevo.ToolTipText = "Nuevo";
            // 
            // btnEditar
            // 
            btnEditar.ImageIndex = 8;
            btnEditar.Name = "btnEditar";
            btnEditar.Tag = "EDITAR";
            btnEditar.ToolTipText = "Editar";
            // 
            // Separador6
            // 
            Separador6.Name = "Separador6";
            Separador6.Style = ToolBarButtonStyle.Separator;
            // 
            // btnCancelarAlta
            // 
            btnCancelarAlta.ImageIndex = 9;
            btnCancelarAlta.Name = "btnCancelarAlta";
            btnCancelarAlta.Tag = "CANCELARALTA";
            btnCancelarAlta.ToolTipText = "Cancelar";
            // 
            // btnGuardar
            // 
            btnGuardar.ImageIndex = 10;
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Tag = "GUARDAR";
            btnGuardar.ToolTipText = "Guardar";
            // 
            // ToolBarButton85
            // 
            ToolBarButton85.ImageIndex = 14;
            ToolBarButton85.Name = "ToolBarButton85";
            ToolBarButton85.Tag = "REFRESCAR";
            ToolBarButton85.ToolTipText = "Refrescar";
            // 
            // Separador5
            // 
            Separador5.Name = "Separador5";
            Separador5.Style = ToolBarButtonStyle.Separator;
            // 
            // btnEliminar
            // 
            btnEliminar.ImageIndex = 11;
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "ELIMINAR";
            btnEliminar.ToolTipText = "Eliminar";
            // 
            // Separador7
            // 
            Separador7.Name = "Separador7";
            Separador7.Style = ToolBarButtonStyle.Separator;
            // 
            // btnGo
            // 
            btnGo.ImageIndex = 12;
            btnGo.Name = "btnGo";
            btnGo.Tag = "GO";
            btnGo.ToolTipText = "Ir a";
            btnGo.Visible = false;
            // 
            // btnListado
            // 
            btnListado.ImageIndex = 13;
            btnListado.Name = "btnListado";
            btnListado.Tag = "LISTADO";
            btnListado.ToolTipText = "Listado";
            // 
            // btlReport
            // 
            btlReport.ImageIndex = 12;
            btlReport.Name = "btlReport";
            btlReport.Tag = "REGISTRO";
            btlReport.ToolTipText = "Registro";
            // 
            // Separator8
            // 
            Separator8.Name = "Separator8";
            Separator8.Style = ToolBarButtonStyle.Separator;
            // 
            // btnImprimir
            // 
            btnImprimir.ImageIndex = 14;
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Tag = "IMPRIMIR";
            btnImprimir.ToolTipText = "Imprimir";
            // 
            // ToolBarButton6
            // 
            ToolBarButton6.Name = "ToolBarButton6";
            ToolBarButton6.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton79
            // 
            ToolBarButton79.ImageIndex = 9;
            ToolBarButton79.Name = "ToolBarButton79";
            ToolBarButton79.Tag = "CERRAR";
            ToolBarButton79.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton82
            // 
            ToolBarButton82.Name = "ToolBarButton82";
            ToolBarButton82.Style = ToolBarButtonStyle.Separator;
            // 
            // imgXPToolbar
            // 
            imgXPToolbar.ImageStream = (ImageListStreamer) resources.GetObject("imgXPToolbar.ImageStream");
            imgXPToolbar.TransparentColor = Color.Fuchsia;
            imgXPToolbar.Images.SetKeyName(0, "");
            imgXPToolbar.Images.SetKeyName(1, "");
            imgXPToolbar.Images.SetKeyName(2, "");
            imgXPToolbar.Images.SetKeyName(3, "");
            imgXPToolbar.Images.SetKeyName(4, "");
            imgXPToolbar.Images.SetKeyName(5, "");
            imgXPToolbar.Images.SetKeyName(6, "");
            imgXPToolbar.Images.SetKeyName(7, "");
            imgXPToolbar.Images.SetKeyName(8, "");
            imgXPToolbar.Images.SetKeyName(9, "");
            imgXPToolbar.Images.SetKeyName(10, "");
            imgXPToolbar.Images.SetKeyName(11, "");
            imgXPToolbar.Images.SetKeyName(12, "");
            imgXPToolbar.Images.SetKeyName(13, "");
            imgXPToolbar.Images.SetKeyName(14, "");
            // 
            // Separador8
            // 
            Separador8.Name = "Separador8";
            Separador8.Style = ToolBarButtonStyle.Separator;
            // 
            // HScroll1
            // 
            HScroll1.Location = new Point(640, 104);
            HScroll1.Name = "HScroll1";
            HScroll1.Size = new Size(104, 16);
            HScroll1.TabIndex = 10;
            // 
            // tbrRegistrosBIG
            // 
            tbrRegistrosBIG.Appearance = ToolBarAppearance.Flat;
            tbrRegistrosBIG.Buttons.AddRange(new[]
            {
                ToolBarButton1,
                ToolBarButton2,
                ToolBarButton3,
                ToolBarButton4,
                ToolBarButton5,
                ToolBarButton7,
                ToolBarButton8,
                ToolBarButton9,
                ToolBarButton10,
                ToolBarButton11,
                ToolBarButton12,
                ToolBarButton13,
                ToolBarButton14,
                ToolBarButton15,
                ToolBarButton16,
                ToolBarButton17,
                ToolBarButton84,
                ToolBarButton18,
                ToolBarButton19,
                ToolBarButton20,
                ToolBarButton21,
                ToolBarButton22,
                ToolBarButton23,
                ToolBarButton24,
                ToolBarButton25,
                ToolBarButton26,
                ToolBarButton77,
                ToolBarButton78
            });
            tbrRegistrosBIG.ButtonSize = new Size(24, 24);
            tbrRegistrosBIG.Dock = DockStyle.None;
            tbrRegistrosBIG.DropDownArrows = true;
            tbrRegistrosBIG.ImageList = imgXPToolBarBIG;
            tbrRegistrosBIG.Location = new Point(0, 40);
            tbrRegistrosBIG.Name = "tbrRegistrosBIG";
            tbrRegistrosBIG.ShowToolTips = true;
            tbrRegistrosBIG.Size = new Size(900, 138);
            tbrRegistrosBIG.TabIndex = 12;
            // 
            // ToolBarButton1
            // 
            ToolBarButton1.Name = "ToolBarButton1";
            ToolBarButton1.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton2
            // 
            ToolBarButton2.ImageIndex = 3;
            ToolBarButton2.Name = "ToolBarButton2";
            ToolBarButton2.Tag = "BUSCAR";
            ToolBarButton2.Text = "Buscar";
            ToolBarButton2.ToolTipText = "Buscar";
            // 
            // ToolBarButton3
            // 
            ToolBarButton3.ImageIndex = 9;
            ToolBarButton3.Name = "ToolBarButton3";
            ToolBarButton3.Tag = "BUSCARSIGUIENTE";
            ToolBarButton3.Text = "Sig...";
            ToolBarButton3.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton4
            // 
            ToolBarButton4.Name = "ToolBarButton4";
            ToolBarButton4.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton5
            // 
            ToolBarButton5.ImageIndex = 14;
            ToolBarButton5.Name = "ToolBarButton5";
            ToolBarButton5.Tag = "ESTABLECERFILTROS";
            ToolBarButton5.Text = "Filtro";
            ToolBarButton5.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton7
            // 
            ToolBarButton7.Name = "ToolBarButton7";
            ToolBarButton7.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton8
            // 
            ToolBarButton8.ImageIndex = 8;
            ToolBarButton8.Name = "ToolBarButton8";
            ToolBarButton8.Tag = "MOVERPRIMERO";
            ToolBarButton8.Text = "Inicio";
            ToolBarButton8.ToolTipText = "Primero";
            // 
            // ToolBarButton9
            // 
            ToolBarButton9.ImageIndex = 0;
            ToolBarButton9.Name = "ToolBarButton9";
            ToolBarButton9.Tag = "MOVERANTERIOR";
            ToolBarButton9.Text = "Atrás";
            ToolBarButton9.ToolTipText = "Anterior";
            // 
            // ToolBarButton10
            // 
            ToolBarButton10.ImageIndex = 1;
            ToolBarButton10.Name = "ToolBarButton10";
            ToolBarButton10.Tag = "MOVERSIGUIENTE";
            ToolBarButton10.Text = "Sig...";
            ToolBarButton10.ToolTipText = "Siguiente";
            // 
            // ToolBarButton11
            // 
            ToolBarButton11.ImageIndex = 2;
            ToolBarButton11.Name = "ToolBarButton11";
            ToolBarButton11.Tag = "MOVERULTIMO";
            ToolBarButton11.Text = "Fin";
            ToolBarButton11.ToolTipText = "?ltimo";
            // 
            // ToolBarButton12
            // 
            ToolBarButton12.Name = "ToolBarButton12";
            ToolBarButton12.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton13
            // 
            ToolBarButton13.ImageIndex = 10;
            ToolBarButton13.Name = "ToolBarButton13";
            ToolBarButton13.Tag = "NUEVO";
            ToolBarButton13.Text = "Nuevo";
            ToolBarButton13.ToolTipText = "Nuevo";
            // 
            // ToolBarButton14
            // 
            ToolBarButton14.ImageIndex = 17;
            ToolBarButton14.Name = "ToolBarButton14";
            ToolBarButton14.Tag = "EDITAR";
            ToolBarButton14.Text = "Editar";
            ToolBarButton14.ToolTipText = "Editar";
            // 
            // ToolBarButton15
            // 
            ToolBarButton15.Name = "ToolBarButton15";
            ToolBarButton15.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton16
            // 
            ToolBarButton16.ImageIndex = 11;
            ToolBarButton16.Name = "ToolBarButton16";
            ToolBarButton16.Tag = "CANCELARALTA";
            ToolBarButton16.Text = "Canc...";
            ToolBarButton16.ToolTipText = "Cancelar";
            // 
            // ToolBarButton17
            // 
            ToolBarButton17.ImageIndex = 6;
            ToolBarButton17.Name = "ToolBarButton17";
            ToolBarButton17.Tag = "GUARDAR";
            ToolBarButton17.Text = "Salvar";
            ToolBarButton17.ToolTipText = "Salvar";
            // 
            // ToolBarButton84
            // 
            ToolBarButton84.ImageIndex = 18;
            ToolBarButton84.Name = "ToolBarButton84";
            ToolBarButton84.Tag = "REFRESCAR";
            ToolBarButton84.Text = "Refrescar";
            ToolBarButton84.ToolTipText = "Refrescar";
            // 
            // ToolBarButton18
            // 
            ToolBarButton18.Name = "ToolBarButton18";
            ToolBarButton18.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton19
            // 
            ToolBarButton19.ImageIndex = 5;
            ToolBarButton19.Name = "ToolBarButton19";
            ToolBarButton19.Tag = "ELIMINAR";
            ToolBarButton19.Text = "Borrar";
            ToolBarButton19.ToolTipText = "Borrar";
            // 
            // ToolBarButton20
            // 
            ToolBarButton20.Name = "ToolBarButton20";
            ToolBarButton20.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton21
            // 
            ToolBarButton21.ImageIndex = 12;
            ToolBarButton21.Name = "ToolBarButton21";
            ToolBarButton21.Tag = "GO";
            ToolBarButton21.ToolTipText = "Ir a";
            ToolBarButton21.Visible = false;
            // 
            // ToolBarButton22
            // 
            ToolBarButton22.ImageIndex = 13;
            ToolBarButton22.Name = "ToolBarButton22";
            ToolBarButton22.Tag = "LISTADO";
            ToolBarButton22.Text = "List...";
            ToolBarButton22.ToolTipText = "Listado";
            // 
            // ToolBarButton23
            // 
            ToolBarButton23.ImageIndex = 16;
            ToolBarButton23.Name = "ToolBarButton23";
            ToolBarButton23.Tag = "REGISTRO";
            ToolBarButton23.Text = "Reg...";
            ToolBarButton23.ToolTipText = "Registro";
            // 
            // ToolBarButton24
            // 
            ToolBarButton24.Name = "ToolBarButton24";
            ToolBarButton24.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton25
            // 
            ToolBarButton25.ImageIndex = 18;
            ToolBarButton25.Name = "ToolBarButton25";
            ToolBarButton25.Tag = "IMPRIMIR";
            ToolBarButton25.Text = "Impr...";
            ToolBarButton25.ToolTipText = "Imprimir";
            // 
            // ToolBarButton26
            // 
            ToolBarButton26.Name = "ToolBarButton26";
            ToolBarButton26.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton77
            // 
            ToolBarButton77.ImageIndex = 11;
            ToolBarButton77.Name = "ToolBarButton77";
            ToolBarButton77.Tag = "CERRAR";
            ToolBarButton77.Text = "Cerrar";
            ToolBarButton77.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton78
            // 
            ToolBarButton78.Name = "ToolBarButton78";
            ToolBarButton78.Style = ToolBarButtonStyle.Separator;
            // 
            // imgXPToolBarBIG
            // 
            imgXPToolBarBIG.ImageStream = (ImageListStreamer) resources.GetObject("imgXPToolBarBIG.ImageStream");
            imgXPToolBarBIG.TransparentColor = Color.Fuchsia;
            imgXPToolBarBIG.Images.SetKeyName(0, "");
            imgXPToolBarBIG.Images.SetKeyName(1, "");
            imgXPToolBarBIG.Images.SetKeyName(2, "");
            imgXPToolBarBIG.Images.SetKeyName(3, "");
            imgXPToolBarBIG.Images.SetKeyName(4, "");
            imgXPToolBarBIG.Images.SetKeyName(5, "");
            imgXPToolBarBIG.Images.SetKeyName(6, "");
            imgXPToolBarBIG.Images.SetKeyName(7, "");
            imgXPToolBarBIG.Images.SetKeyName(8, "");
            imgXPToolBarBIG.Images.SetKeyName(9, "");
            imgXPToolBarBIG.Images.SetKeyName(10, "");
            imgXPToolBarBIG.Images.SetKeyName(11, "");
            imgXPToolBarBIG.Images.SetKeyName(12, "");
            imgXPToolBarBIG.Images.SetKeyName(13, "");
            imgXPToolBarBIG.Images.SetKeyName(14, "");
            imgXPToolBarBIG.Images.SetKeyName(15, "");
            imgXPToolBarBIG.Images.SetKeyName(16, "");
            imgXPToolBarBIG.Images.SetKeyName(17, "");
            imgXPToolBarBIG.Images.SetKeyName(18, "");
            // 
            // lblReg
            // 
            lblReg.AutoSize = true;
            lblReg.Location = new Point(544, 104);
            lblReg.Name = "lblReg";
            lblReg.Size = new Size(24, 13);
            lblReg.TabIndex = 13;
            lblReg.Text = "0/0";
            // 
            // imgStandard
            // 
            imgStandard.ImageStream = (ImageListStreamer) resources.GetObject("imgStandard.ImageStream");
            imgStandard.TransparentColor = Color.Transparent;
            imgStandard.Images.SetKeyName(0, "");
            imgStandard.Images.SetKeyName(1, "");
            imgStandard.Images.SetKeyName(2, "");
            imgStandard.Images.SetKeyName(3, "");
            imgStandard.Images.SetKeyName(4, "");
            imgStandard.Images.SetKeyName(5, "");
            imgStandard.Images.SetKeyName(6, "");
            imgStandard.Images.SetKeyName(7, "");
            imgStandard.Images.SetKeyName(8, "");
            imgStandard.Images.SetKeyName(9, "");
            imgStandard.Images.SetKeyName(10, "");
            imgStandard.Images.SetKeyName(11, "");
            imgStandard.Images.SetKeyName(12, "");
            imgStandard.Images.SetKeyName(13, "");
            imgStandard.Images.SetKeyName(14, "");
            // 
            // tbrRegistrosStandardBIG
            // 
            tbrRegistrosStandardBIG.Appearance = ToolBarAppearance.Flat;
            tbrRegistrosStandardBIG.Buttons.AddRange(new[]
            {
                ToolBarButton27,
                ToolBarButton28,
                ToolBarButton29,
                ToolBarButton30,
                ToolBarButton31,
                ToolBarButton32,
                ToolBarButton33,
                ToolBarButton34,
                ToolBarButton35,
                ToolBarButton36,
                ToolBarButton37,
                ToolBarButton38,
                ToolBarButton39,
                ToolBarButton40,
                ToolBarButton41,
                ToolBarButton42,
                ToolBarButton43,
                ToolBarButton44,
                ToolBarButton45,
                ToolBarButton46,
                ToolBarButton47,
                ToolBarButton48,
                ToolBarButton49,
                ToolBarButton50,
                ToolBarButton51,
                ToolBarButton83,
                toolBarButton86
            });
            tbrRegistrosStandardBIG.ButtonSize = new Size(24, 24);
            tbrRegistrosStandardBIG.Dock = DockStyle.None;
            tbrRegistrosStandardBIG.DropDownArrows = true;
            tbrRegistrosStandardBIG.ImageList = imgStandard;
            tbrRegistrosStandardBIG.Location = new Point(56, 136);
            tbrRegistrosStandardBIG.Name = "tbrRegistrosStandardBIG";
            tbrRegistrosStandardBIG.ShowToolTips = true;
            tbrRegistrosStandardBIG.Size = new Size(900, 42);
            tbrRegistrosStandardBIG.TabIndex = 14;
            // 
            // ToolBarButton27
            // 
            ToolBarButton27.Name = "ToolBarButton27";
            ToolBarButton27.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton28
            // 
            ToolBarButton28.ImageIndex = 0;
            ToolBarButton28.Name = "ToolBarButton28";
            ToolBarButton28.Tag = "BUSCAR";
            ToolBarButton28.Text = "Buscar";
            ToolBarButton28.ToolTipText = "Buscar";
            // 
            // ToolBarButton29
            // 
            ToolBarButton29.ImageIndex = 1;
            ToolBarButton29.Name = "ToolBarButton29";
            ToolBarButton29.Tag = "BUSCARSIGUIENTE";
            ToolBarButton29.Text = "Sig...";
            ToolBarButton29.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton30
            // 
            ToolBarButton30.Name = "ToolBarButton30";
            ToolBarButton30.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton31
            // 
            ToolBarButton31.ImageIndex = 2;
            ToolBarButton31.Name = "ToolBarButton31";
            ToolBarButton31.Tag = "ESTABLECERFILTROS";
            ToolBarButton31.Text = "Filtro";
            ToolBarButton31.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton32
            // 
            ToolBarButton32.Name = "ToolBarButton32";
            ToolBarButton32.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton33
            // 
            ToolBarButton33.ImageIndex = 3;
            ToolBarButton33.Name = "ToolBarButton33";
            ToolBarButton33.Tag = "MOVERPRIMERO";
            ToolBarButton33.Text = "Inicio";
            ToolBarButton33.ToolTipText = "Primero";
            // 
            // ToolBarButton34
            // 
            ToolBarButton34.ImageIndex = 4;
            ToolBarButton34.Name = "ToolBarButton34";
            ToolBarButton34.Tag = "MOVERANTERIOR";
            ToolBarButton34.Text = "Atrás";
            ToolBarButton34.ToolTipText = "Anterior";
            // 
            // ToolBarButton35
            // 
            ToolBarButton35.ImageIndex = 5;
            ToolBarButton35.Name = "ToolBarButton35";
            ToolBarButton35.Tag = "MOVERSIGUIENTE";
            ToolBarButton35.Text = "Sig...";
            ToolBarButton35.ToolTipText = "Siguiente";
            // 
            // ToolBarButton36
            // 
            ToolBarButton36.ImageIndex = 6;
            ToolBarButton36.Name = "ToolBarButton36";
            ToolBarButton36.Tag = "MOVERULTIMO";
            ToolBarButton36.Text = "Fin";
            ToolBarButton36.ToolTipText = "?ltimo";
            // 
            // ToolBarButton37
            // 
            ToolBarButton37.ImageIndex = 7;
            ToolBarButton37.Name = "ToolBarButton37";
            ToolBarButton37.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton38
            // 
            ToolBarButton38.ImageIndex = 7;
            ToolBarButton38.Name = "ToolBarButton38";
            ToolBarButton38.Tag = "NUEVO";
            ToolBarButton38.Text = "Nuevo";
            ToolBarButton38.ToolTipText = "Nuevo";
            // 
            // ToolBarButton39
            // 
            ToolBarButton39.ImageIndex = 12;
            ToolBarButton39.Name = "ToolBarButton39";
            ToolBarButton39.Tag = "EDITAR";
            ToolBarButton39.Text = "Editar";
            ToolBarButton39.ToolTipText = "Editar";
            // 
            // ToolBarButton40
            // 
            ToolBarButton40.ImageIndex = 9;
            ToolBarButton40.Name = "ToolBarButton40";
            ToolBarButton40.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton41
            // 
            ToolBarButton41.ImageIndex = 9;
            ToolBarButton41.Name = "ToolBarButton41";
            ToolBarButton41.Tag = "CANCELARALTA";
            ToolBarButton41.Text = "Canc...";
            ToolBarButton41.ToolTipText = "Cancelar";
            // 
            // ToolBarButton42
            // 
            ToolBarButton42.ImageIndex = 8;
            ToolBarButton42.Name = "ToolBarButton42";
            ToolBarButton42.Tag = "GUARDAR";
            ToolBarButton42.Text = "Salvar";
            ToolBarButton42.ToolTipText = "Guardar";
            // 
            // ToolBarButton43
            // 
            ToolBarButton43.Name = "ToolBarButton43";
            ToolBarButton43.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton44
            // 
            ToolBarButton44.ImageIndex = 10;
            ToolBarButton44.Name = "ToolBarButton44";
            ToolBarButton44.Tag = "ELIMINAR";
            ToolBarButton44.Text = "Borrar";
            ToolBarButton44.ToolTipText = "Eliminar";
            // 
            // ToolBarButton45
            // 
            ToolBarButton45.Name = "ToolBarButton45";
            ToolBarButton45.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton46
            // 
            ToolBarButton46.ImageIndex = 13;
            ToolBarButton46.Name = "ToolBarButton46";
            ToolBarButton46.Tag = "GO";
            ToolBarButton46.ToolTipText = "Ir a";
            ToolBarButton46.Visible = false;
            // 
            // ToolBarButton47
            // 
            ToolBarButton47.ImageIndex = 11;
            ToolBarButton47.Name = "ToolBarButton47";
            ToolBarButton47.Tag = "LISTADO";
            ToolBarButton47.Text = "List...";
            ToolBarButton47.ToolTipText = "Listado";
            // 
            // ToolBarButton48
            // 
            ToolBarButton48.ImageIndex = 13;
            ToolBarButton48.Name = "ToolBarButton48";
            ToolBarButton48.Tag = "REGISTRO";
            ToolBarButton48.Text = "Reg...";
            ToolBarButton48.ToolTipText = "Registro";
            // 
            // ToolBarButton49
            // 
            ToolBarButton49.Name = "ToolBarButton49";
            ToolBarButton49.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton50
            // 
            ToolBarButton50.ImageIndex = 14;
            ToolBarButton50.Name = "ToolBarButton50";
            ToolBarButton50.Tag = "IMPRIMIR";
            ToolBarButton50.Text = "Impr...";
            ToolBarButton50.ToolTipText = "Imprimir";
            // 
            // ToolBarButton51
            // 
            ToolBarButton51.Name = "ToolBarButton51";
            ToolBarButton51.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton83
            // 
            ToolBarButton83.ImageIndex = 10;
            ToolBarButton83.Name = "ToolBarButton83";
            ToolBarButton83.Tag = "CERRAR";
            ToolBarButton83.Text = "Cerrar";
            ToolBarButton83.ToolTipText = "CErrar formulario";
            // 
            // toolBarButton86
            // 
            toolBarButton86.Name = "toolBarButton86";
            toolBarButton86.Style = ToolBarButtonStyle.Separator;
            // 
            // tbrRegistrosStandard
            // 
            tbrRegistrosStandard.Appearance = ToolBarAppearance.Flat;
            tbrRegistrosStandard.Buttons.AddRange(new[]
            {
                ToolBarButton52,
                ToolBarButton53,
                ToolBarButton54,
                ToolBarButton55,
                ToolBarButton56,
                ToolBarButton57,
                ToolBarButton58,
                ToolBarButton59,
                ToolBarButton60,
                ToolBarButton61,
                ToolBarButton62,
                ToolBarButton63,
                ToolBarButton64,
                ToolBarButton65,
                ToolBarButton66,
                ToolBarButton67,
                ToolBarButton68,
                ToolBarButton69,
                ToolBarButton70,
                ToolBarButton71,
                ToolBarButton72,
                ToolBarButton73,
                ToolBarButton74,
                ToolBarButton75,
                ToolBarButton76,
                ToolBarButton80,
                ToolBarButton81
            });
            tbrRegistrosStandard.ButtonSize = new Size(24, 24);
            tbrRegistrosStandard.Dock = DockStyle.None;
            tbrRegistrosStandard.DropDownArrows = true;
            tbrRegistrosStandard.ImageList = imgStandard;
            tbrRegistrosStandard.Location = new Point(32, 100);
            tbrRegistrosStandard.Name = "tbrRegistrosStandard";
            tbrRegistrosStandard.ShowToolTips = true;
            tbrRegistrosStandard.Size = new Size(900, 28);
            tbrRegistrosStandard.TabIndex = 15;
            // 
            // ToolBarButton52
            // 
            ToolBarButton52.Name = "ToolBarButton52";
            ToolBarButton52.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton53
            // 
            ToolBarButton53.ImageIndex = 0;
            ToolBarButton53.Name = "ToolBarButton53";
            ToolBarButton53.Tag = "BUSCAR";
            ToolBarButton53.ToolTipText = "Buscar";
            // 
            // ToolBarButton54
            // 
            ToolBarButton54.ImageIndex = 1;
            ToolBarButton54.Name = "ToolBarButton54";
            ToolBarButton54.Tag = "BUSCARSIGUIENTE";
            ToolBarButton54.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton55
            // 
            ToolBarButton55.Name = "ToolBarButton55";
            ToolBarButton55.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton56
            // 
            ToolBarButton56.ImageIndex = 2;
            ToolBarButton56.Name = "ToolBarButton56";
            ToolBarButton56.Tag = "ESTABLECERFILTROS";
            ToolBarButton56.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton57
            // 
            ToolBarButton57.Name = "ToolBarButton57";
            ToolBarButton57.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton58
            // 
            ToolBarButton58.ImageIndex = 3;
            ToolBarButton58.Name = "ToolBarButton58";
            ToolBarButton58.Tag = "MOVERPRIMERO";
            ToolBarButton58.ToolTipText = "Primero";
            // 
            // ToolBarButton59
            // 
            ToolBarButton59.ImageIndex = 4;
            ToolBarButton59.Name = "ToolBarButton59";
            ToolBarButton59.Tag = "MOVERANTERIOR";
            ToolBarButton59.ToolTipText = "Anterior";
            // 
            // ToolBarButton60
            // 
            ToolBarButton60.ImageIndex = 5;
            ToolBarButton60.Name = "ToolBarButton60";
            ToolBarButton60.Tag = "MOVERSIGUIENTE";
            ToolBarButton60.ToolTipText = "Siguiente";
            // 
            // ToolBarButton61
            // 
            ToolBarButton61.ImageIndex = 6;
            ToolBarButton61.Name = "ToolBarButton61";
            ToolBarButton61.Tag = "MOVERULTIMO";
            ToolBarButton61.ToolTipText = "?ltimo";
            // 
            // ToolBarButton62
            // 
            ToolBarButton62.ImageIndex = 7;
            ToolBarButton62.Name = "ToolBarButton62";
            ToolBarButton62.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton63
            // 
            ToolBarButton63.ImageIndex = 7;
            ToolBarButton63.Name = "ToolBarButton63";
            ToolBarButton63.Tag = "NUEVO";
            ToolBarButton63.ToolTipText = "Nuevo";
            // 
            // ToolBarButton64
            // 
            ToolBarButton64.ImageIndex = 12;
            ToolBarButton64.Name = "ToolBarButton64";
            ToolBarButton64.Tag = "EDITAR";
            ToolBarButton64.ToolTipText = "Editar";
            // 
            // ToolBarButton65
            // 
            ToolBarButton65.ImageIndex = 9;
            ToolBarButton65.Name = "ToolBarButton65";
            ToolBarButton65.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton66
            // 
            ToolBarButton66.ImageIndex = 9;
            ToolBarButton66.Name = "ToolBarButton66";
            ToolBarButton66.Tag = "CANCELARALTA";
            ToolBarButton66.ToolTipText = "Cancelar";
            // 
            // ToolBarButton67
            // 
            ToolBarButton67.ImageIndex = 8;
            ToolBarButton67.Name = "ToolBarButton67";
            ToolBarButton67.Tag = "GUARDAR";
            ToolBarButton67.ToolTipText = "Guardar";
            // 
            // ToolBarButton68
            // 
            ToolBarButton68.Name = "ToolBarButton68";
            ToolBarButton68.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton69
            // 
            ToolBarButton69.ImageIndex = 10;
            ToolBarButton69.Name = "ToolBarButton69";
            ToolBarButton69.Tag = "ELIMINAR";
            ToolBarButton69.ToolTipText = "Eliminar";
            // 
            // ToolBarButton70
            // 
            ToolBarButton70.Name = "ToolBarButton70";
            ToolBarButton70.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton71
            // 
            ToolBarButton71.ImageIndex = 13;
            ToolBarButton71.Name = "ToolBarButton71";
            ToolBarButton71.Tag = "GO";
            ToolBarButton71.ToolTipText = "Ir a";
            ToolBarButton71.Visible = false;
            // 
            // ToolBarButton72
            // 
            ToolBarButton72.ImageIndex = 11;
            ToolBarButton72.Name = "ToolBarButton72";
            ToolBarButton72.Tag = "LISTADO";
            ToolBarButton72.ToolTipText = "Listado";
            // 
            // ToolBarButton73
            // 
            ToolBarButton73.ImageIndex = 13;
            ToolBarButton73.Name = "ToolBarButton73";
            ToolBarButton73.Tag = "REGISTRO";
            ToolBarButton73.ToolTipText = "Registro";
            // 
            // ToolBarButton74
            // 
            ToolBarButton74.Name = "ToolBarButton74";
            ToolBarButton74.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton75
            // 
            ToolBarButton75.ImageIndex = 14;
            ToolBarButton75.Name = "ToolBarButton75";
            ToolBarButton75.Tag = "IMPRIMIR";
            ToolBarButton75.ToolTipText = "Imprimir";
            // 
            // ToolBarButton76
            // 
            ToolBarButton76.Name = "ToolBarButton76";
            ToolBarButton76.Style = ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton80
            // 
            ToolBarButton80.ImageIndex = 10;
            ToolBarButton80.Name = "ToolBarButton80";
            ToolBarButton80.Tag = "CERRAR";
            ToolBarButton80.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton81
            // 
            ToolBarButton81.Name = "ToolBarButton81";
            ToolBarButton81.Style = ToolBarButtonStyle.Separator;
            // 
            // DBToolBarEx
            // 
            Controls.Add(HScroll1);
            Controls.Add(lblReg);
            Controls.Add(tbrRegistrosStandard);
            Controls.Add(tbrRegistrosStandardBIG);
            Controls.Add(tbrRegistrosBIG);
            Controls.Add(tbrRegistros);
            Name = "DBToolBarEx";
            Size = new Size(1041, 202);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}