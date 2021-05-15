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
        private ToolBarButton toolBarButton87;
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

        private void TbrRegistrosStandardBIG_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            ToolbarOptions(sender, e);
        }

        private void TbrRegistrosStandard_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
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
            tbrRegistrosStandard.ButtonClick += TbrRegistrosStandard_ButtonClick;
            tbrRegistrosStandardBIG.ButtonClick += TbrRegistrosStandardBIG_ButtonClick;

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
            this.tbrRegistros = new System.Windows.Forms.ToolBar();
            this.imgXPToolbar = new System.Windows.Forms.ImageList(this.components);
            this.Separador1 = new System.Windows.Forms.ToolBarButton();
            this.btnBuscar = new System.Windows.Forms.ToolBarButton();
            this.btnBuscarSiguiente = new System.Windows.Forms.ToolBarButton();
            this.Separador2 = new System.Windows.Forms.ToolBarButton();
            this.btnEstablecerFiltros = new System.Windows.Forms.ToolBarButton();
            this.Separador3 = new System.Windows.Forms.ToolBarButton();
            this.btnMoverPrimero = new System.Windows.Forms.ToolBarButton();
            this.btnMoverAnterior = new System.Windows.Forms.ToolBarButton();
            this.btnMoverSiguiente = new System.Windows.Forms.ToolBarButton();
            this.btnMoverUltimo = new System.Windows.Forms.ToolBarButton();
            this.Separador4 = new System.Windows.Forms.ToolBarButton();
            this.btnNuevo = new System.Windows.Forms.ToolBarButton();
            this.btnEditar = new System.Windows.Forms.ToolBarButton();
            this.Separador6 = new System.Windows.Forms.ToolBarButton();
            this.btnCancelarAlta = new System.Windows.Forms.ToolBarButton();
            this.btnGuardar = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton85 = new System.Windows.Forms.ToolBarButton();
            this.Separador5 = new System.Windows.Forms.ToolBarButton();
            this.btnEliminar = new System.Windows.Forms.ToolBarButton();
            this.Separador7 = new System.Windows.Forms.ToolBarButton();
            this.btnGo = new System.Windows.Forms.ToolBarButton();
            this.btnListado = new System.Windows.Forms.ToolBarButton();
            this.btlReport = new System.Windows.Forms.ToolBarButton();
            this.Separator8 = new System.Windows.Forms.ToolBarButton();
            this.btnImprimir = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton79 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton82 = new System.Windows.Forms.ToolBarButton();
            this.Separador8 = new System.Windows.Forms.ToolBarButton();
            this.HScroll1 = new System.Windows.Forms.HScrollBar();
            this.tbrRegistrosBIG = new System.Windows.Forms.ToolBar();
            this.imgXPToolBarBIG = new System.Windows.Forms.ImageList(this.components);
            this.ToolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton8 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton9 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton10 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton11 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton12 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton13 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton14 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton15 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton16 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton17 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton84 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton18 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton19 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton20 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton21 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton22 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton23 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton24 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton25 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton26 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton77 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton78 = new System.Windows.Forms.ToolBarButton();
            this.lblReg = new System.Windows.Forms.Label();
            this.imgStandard = new System.Windows.Forms.ImageList(this.components);
            this.tbrRegistrosStandardBIG = new System.Windows.Forms.ToolBar();
            this.ToolBarButton27 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton28 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton29 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton30 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton31 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton32 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton33 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton34 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton35 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton36 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton37 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton38 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton39 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton40 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton41 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton42 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton43 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton44 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton45 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton46 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton47 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton48 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton49 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton50 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton51 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton83 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton86 = new System.Windows.Forms.ToolBarButton();
            this.tbrRegistrosStandard = new System.Windows.Forms.ToolBar();
            this.ToolBarButton52 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton53 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton54 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton55 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton56 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton57 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton58 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton59 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton60 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton61 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton62 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton63 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton64 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton65 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton66 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton67 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton68 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton69 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton70 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton71 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton72 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton73 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton74 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton75 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton76 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton80 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton81 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton87 = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbrRegistros
            // 
            this.tbrRegistros.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbrRegistros.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
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
            this.tbrRegistros.ButtonSize = new System.Drawing.Size(16, 16);
            this.tbrRegistros.Dock = System.Windows.Forms.DockStyle.None;
            this.tbrRegistros.DropDownArrows = true;
            this.tbrRegistros.ImageList = this.imgXPToolbar;
            this.tbrRegistros.Location = new System.Drawing.Point(24, 8);
            this.tbrRegistros.Name = "tbrRegistros";
            this.tbrRegistros.ShowToolTips = true;
            this.tbrRegistros.Size = new System.Drawing.Size(900, 28);
            this.tbrRegistros.TabIndex = 9;
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
            // Separador1
            // 
            this.Separador1.Name = "Separador1";
            this.Separador1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageIndex = 0;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Tag = "BUSCAR";
            this.btnBuscar.ToolTipText = "Buscar";
            // 
            // btnBuscarSiguiente
            // 
            this.btnBuscarSiguiente.ImageIndex = 1;
            this.btnBuscarSiguiente.Name = "btnBuscarSiguiente";
            this.btnBuscarSiguiente.Tag = "BUSCARSIGUIENTE";
            this.btnBuscarSiguiente.ToolTipText = "Buscar siguiente";
            // 
            // Separador2
            // 
            this.Separador2.Name = "Separador2";
            this.Separador2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnEstablecerFiltros
            // 
            this.btnEstablecerFiltros.ImageIndex = 2;
            this.btnEstablecerFiltros.Name = "btnEstablecerFiltros";
            this.btnEstablecerFiltros.Tag = "ESTABLECERFILTROS";
            this.btnEstablecerFiltros.ToolTipText = "Establecer filtro";
            // 
            // Separador3
            // 
            this.Separador3.Name = "Separador3";
            this.Separador3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnMoverPrimero
            // 
            this.btnMoverPrimero.ImageIndex = 3;
            this.btnMoverPrimero.Name = "btnMoverPrimero";
            this.btnMoverPrimero.Tag = "MOVERPRIMERO";
            this.btnMoverPrimero.ToolTipText = "Primero";
            // 
            // btnMoverAnterior
            // 
            this.btnMoverAnterior.ImageIndex = 4;
            this.btnMoverAnterior.Name = "btnMoverAnterior";
            this.btnMoverAnterior.Tag = "MOVERANTERIOR";
            this.btnMoverAnterior.ToolTipText = "Anterior";
            // 
            // btnMoverSiguiente
            // 
            this.btnMoverSiguiente.ImageIndex = 5;
            this.btnMoverSiguiente.Name = "btnMoverSiguiente";
            this.btnMoverSiguiente.Tag = "MOVERSIGUIENTE";
            this.btnMoverSiguiente.ToolTipText = "Siguiente";
            // 
            // btnMoverUltimo
            // 
            this.btnMoverUltimo.ImageIndex = 6;
            this.btnMoverUltimo.Name = "btnMoverUltimo";
            this.btnMoverUltimo.Tag = "MOVERULTIMO";
            this.btnMoverUltimo.ToolTipText = "Último";
            // 
            // Separador4
            // 
            this.Separador4.Name = "Separador4";
            this.Separador4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnNuevo
            // 
            this.btnNuevo.ImageIndex = 7;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Tag = "NUEVO";
            this.btnNuevo.ToolTipText = "Nuevo";
            // 
            // btnEditar
            // 
            this.btnEditar.ImageIndex = 8;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Tag = "EDITAR";
            this.btnEditar.ToolTipText = "Editar";
            // 
            // Separador6
            // 
            this.Separador6.Name = "Separador6";
            this.Separador6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnCancelarAlta
            // 
            this.btnCancelarAlta.ImageIndex = 9;
            this.btnCancelarAlta.Name = "btnCancelarAlta";
            this.btnCancelarAlta.Tag = "CANCELARALTA";
            this.btnCancelarAlta.ToolTipText = "Cancelar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.ImageIndex = 10;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Tag = "GUARDAR";
            this.btnGuardar.ToolTipText = "Guardar";
            // 
            // ToolBarButton85
            // 
            this.ToolBarButton85.ImageIndex = 14;
            this.ToolBarButton85.Name = "ToolBarButton85";
            this.ToolBarButton85.Tag = "REFRESCAR";
            this.ToolBarButton85.ToolTipText = "Refrescar";
            // 
            // Separador5
            // 
            this.Separador5.Name = "Separador5";
            this.Separador5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnEliminar
            // 
            this.btnEliminar.ImageIndex = 11;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Tag = "ELIMINAR";
            this.btnEliminar.ToolTipText = "Eliminar";
            // 
            // Separador7
            // 
            this.Separador7.Name = "Separador7";
            this.Separador7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnGo
            // 
            this.btnGo.ImageIndex = 12;
            this.btnGo.Name = "btnGo";
            this.btnGo.Tag = "GO";
            this.btnGo.ToolTipText = "Ir a";
            this.btnGo.Visible = false;
            // 
            // btnListado
            // 
            this.btnListado.ImageIndex = 13;
            this.btnListado.Name = "btnListado";
            this.btnListado.Tag = "LISTADO";
            this.btnListado.ToolTipText = "Listado";
            // 
            // btlReport
            // 
            this.btlReport.ImageIndex = 12;
            this.btlReport.Name = "btlReport";
            this.btlReport.Tag = "REGISTRO";
            this.btlReport.ToolTipText = "Registro";
            // 
            // Separator8
            // 
            this.Separator8.Name = "Separator8";
            this.Separator8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnImprimir
            // 
            this.btnImprimir.ImageIndex = 14;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Tag = "IMPRIMIR";
            this.btnImprimir.ToolTipText = "Imprimir";
            // 
            // ToolBarButton6
            // 
            this.ToolBarButton6.Name = "ToolBarButton6";
            this.ToolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton79
            // 
            this.ToolBarButton79.ImageIndex = 9;
            this.ToolBarButton79.Name = "ToolBarButton79";
            this.ToolBarButton79.Tag = "CERRAR";
            this.ToolBarButton79.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton82
            // 
            this.ToolBarButton82.Name = "ToolBarButton82";
            this.ToolBarButton82.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // Separador8
            // 
            this.Separador8.Name = "Separador8";
            this.Separador8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // HScroll1
            // 
            this.HScroll1.Location = new System.Drawing.Point(640, 104);
            this.HScroll1.Name = "HScroll1";
            this.HScroll1.Size = new System.Drawing.Size(104, 16);
            this.HScroll1.TabIndex = 10;
            // 
            // tbrRegistrosBIG
            // 
            this.tbrRegistrosBIG.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            tbrRegistrosBIG.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[]
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
            this.tbrRegistrosBIG.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrRegistrosBIG.Dock = System.Windows.Forms.DockStyle.None;
            this.tbrRegistrosBIG.DropDownArrows = true;
            this.tbrRegistrosBIG.ImageList = this.imgXPToolBarBIG;
            this.tbrRegistrosBIG.Location = new System.Drawing.Point(0, 40);
            this.tbrRegistrosBIG.Name = "tbrRegistrosBIG";
            this.tbrRegistrosBIG.ShowToolTips = true;
            this.tbrRegistrosBIG.Size = new System.Drawing.Size(900, 30);
            this.tbrRegistrosBIG.TabIndex = 12;
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
            // ToolBarButton1
            // 
            this.ToolBarButton1.Name = "ToolBarButton1";
            this.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.ImageIndex = 3;
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Tag = "BUSCAR";
            this.ToolBarButton2.Text = "Buscar";
            this.ToolBarButton2.ToolTipText = "Buscar";
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.ImageIndex = 9;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Tag = "BUSCARSIGUIENTE";
            this.ToolBarButton3.Text = "Sig...";
            this.ToolBarButton3.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.Name = "ToolBarButton4";
            this.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 14;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Tag = "ESTABLECERFILTROS";
            this.ToolBarButton5.Text = "Filtro";
            this.ToolBarButton5.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton7
            // 
            this.ToolBarButton7.Name = "ToolBarButton7";
            this.ToolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton8
            // 
            this.ToolBarButton8.ImageIndex = 8;
            this.ToolBarButton8.Name = "ToolBarButton8";
            this.ToolBarButton8.Tag = "MOVERPRIMERO";
            this.ToolBarButton8.Text = "Inicio";
            this.ToolBarButton8.ToolTipText = "Primero";
            // 
            // ToolBarButton9
            // 
            this.ToolBarButton9.ImageIndex = 0;
            this.ToolBarButton9.Name = "ToolBarButton9";
            this.ToolBarButton9.Tag = "MOVERANTERIOR";
            this.ToolBarButton9.Text = "Atrás";
            this.ToolBarButton9.ToolTipText = "Anterior";
            // 
            // ToolBarButton10
            // 
            this.ToolBarButton10.ImageIndex = 1;
            this.ToolBarButton10.Name = "ToolBarButton10";
            this.ToolBarButton10.Tag = "MOVERSIGUIENTE";
            this.ToolBarButton10.Text = "Sig...";
            this.ToolBarButton10.ToolTipText = "Siguiente";
            // 
            // ToolBarButton11
            // 
            this.ToolBarButton11.ImageIndex = 2;
            this.ToolBarButton11.Name = "ToolBarButton11";
            this.ToolBarButton11.Tag = "MOVERULTIMO";
            this.ToolBarButton11.Text = "Fin";
            this.ToolBarButton11.ToolTipText = "?ltimo";
            // 
            // ToolBarButton12
            // 
            this.ToolBarButton12.Name = "ToolBarButton12";
            this.ToolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton13
            // 
            this.ToolBarButton13.ImageIndex = 10;
            this.ToolBarButton13.Name = "ToolBarButton13";
            this.ToolBarButton13.Tag = "NUEVO";
            this.ToolBarButton13.Text = "Nuevo";
            this.ToolBarButton13.ToolTipText = "Nuevo";
            // 
            // ToolBarButton14
            // 
            this.ToolBarButton14.ImageIndex = 17;
            this.ToolBarButton14.Name = "ToolBarButton14";
            this.ToolBarButton14.Tag = "EDITAR";
            this.ToolBarButton14.Text = "Editar";
            this.ToolBarButton14.ToolTipText = "Editar";
            // 
            // ToolBarButton15
            // 
            this.ToolBarButton15.Name = "ToolBarButton15";
            this.ToolBarButton15.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton16
            // 
            this.ToolBarButton16.ImageIndex = 11;
            this.ToolBarButton16.Name = "ToolBarButton16";
            this.ToolBarButton16.Tag = "CANCELARALTA";
            this.ToolBarButton16.Text = "Canc...";
            this.ToolBarButton16.ToolTipText = "Cancelar";
            // 
            // ToolBarButton17
            // 
            this.ToolBarButton17.ImageIndex = 6;
            this.ToolBarButton17.Name = "ToolBarButton17";
            this.ToolBarButton17.Tag = "GUARDAR";
            this.ToolBarButton17.Text = "Salvar";
            this.ToolBarButton17.ToolTipText = "Salvar";
            // 
            // ToolBarButton84
            // 
            this.ToolBarButton84.ImageIndex = 18;
            this.ToolBarButton84.Name = "ToolBarButton84";
            this.ToolBarButton84.Tag = "REFRESCAR";
            this.ToolBarButton84.Text = "Refrescar";
            this.ToolBarButton84.ToolTipText = "Refrescar";
            // 
            // ToolBarButton18
            // 
            this.ToolBarButton18.Name = "ToolBarButton18";
            this.ToolBarButton18.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton19
            // 
            this.ToolBarButton19.ImageIndex = 5;
            this.ToolBarButton19.Name = "ToolBarButton19";
            this.ToolBarButton19.Tag = "ELIMINAR";
            this.ToolBarButton19.Text = "Borrar";
            this.ToolBarButton19.ToolTipText = "Borrar";
            // 
            // ToolBarButton20
            // 
            this.ToolBarButton20.Name = "ToolBarButton20";
            this.ToolBarButton20.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton21
            // 
            this.ToolBarButton21.ImageIndex = 12;
            this.ToolBarButton21.Name = "ToolBarButton21";
            this.ToolBarButton21.Tag = "GO";
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
            this.ToolBarButton23.ImageIndex = 16;
            this.ToolBarButton23.Name = "ToolBarButton23";
            this.ToolBarButton23.Tag = "REGISTRO";
            this.ToolBarButton23.Text = "Reg...";
            this.ToolBarButton23.ToolTipText = "Registro";
            // 
            // ToolBarButton24
            // 
            this.ToolBarButton24.Name = "ToolBarButton24";
            this.ToolBarButton24.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
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
            this.ToolBarButton26.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton77
            // 
            this.ToolBarButton77.ImageIndex = 11;
            this.ToolBarButton77.Name = "ToolBarButton77";
            this.ToolBarButton77.Tag = "CERRAR";
            this.ToolBarButton77.Text = "Cerrar";
            this.ToolBarButton77.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton78
            // 
            this.ToolBarButton78.Name = "ToolBarButton78";
            this.ToolBarButton78.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // lblReg
            // 
            this.lblReg.AutoSize = true;
            this.lblReg.Location = new System.Drawing.Point(544, 104);
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
            // tbrRegistrosStandardBIG
            // 
            this.tbrRegistrosStandardBIG.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            tbrRegistrosStandardBIG.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[]
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
            this.tbrRegistrosStandardBIG.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrRegistrosStandardBIG.Dock = System.Windows.Forms.DockStyle.None;
            this.tbrRegistrosStandardBIG.DropDownArrows = true;
            this.tbrRegistrosStandardBIG.ImageList = this.imgStandard;
            this.tbrRegistrosStandardBIG.Location = new System.Drawing.Point(56, 136);
            this.tbrRegistrosStandardBIG.Name = "tbrRegistrosStandardBIG";
            this.tbrRegistrosStandardBIG.ShowToolTips = true;
            this.tbrRegistrosStandardBIG.Size = new System.Drawing.Size(900, 30);
            this.tbrRegistrosStandardBIG.TabIndex = 14;
            // 
            // ToolBarButton27
            // 
            this.ToolBarButton27.Name = "ToolBarButton27";
            this.ToolBarButton27.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton28
            // 
            this.ToolBarButton28.ImageIndex = 0;
            this.ToolBarButton28.Name = "ToolBarButton28";
            this.ToolBarButton28.Tag = "BUSCAR";
            this.ToolBarButton28.Text = "Buscar";
            this.ToolBarButton28.ToolTipText = "Buscar";
            // 
            // ToolBarButton29
            // 
            this.ToolBarButton29.ImageIndex = 1;
            this.ToolBarButton29.Name = "ToolBarButton29";
            this.ToolBarButton29.Tag = "BUSCARSIGUIENTE";
            this.ToolBarButton29.Text = "Sig...";
            this.ToolBarButton29.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton30
            // 
            this.ToolBarButton30.Name = "ToolBarButton30";
            this.ToolBarButton30.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton31
            // 
            this.ToolBarButton31.ImageIndex = 2;
            this.ToolBarButton31.Name = "ToolBarButton31";
            this.ToolBarButton31.Tag = "ESTABLECERFILTROS";
            this.ToolBarButton31.Text = "Filtro";
            this.ToolBarButton31.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton32
            // 
            this.ToolBarButton32.Name = "ToolBarButton32";
            this.ToolBarButton32.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton33
            // 
            this.ToolBarButton33.ImageIndex = 3;
            this.ToolBarButton33.Name = "ToolBarButton33";
            this.ToolBarButton33.Tag = "MOVERPRIMERO";
            this.ToolBarButton33.Text = "Inicio";
            this.ToolBarButton33.ToolTipText = "Primero";
            // 
            // ToolBarButton34
            // 
            this.ToolBarButton34.ImageIndex = 4;
            this.ToolBarButton34.Name = "ToolBarButton34";
            this.ToolBarButton34.Tag = "MOVERANTERIOR";
            this.ToolBarButton34.Text = "Atrás";
            this.ToolBarButton34.ToolTipText = "Anterior";
            // 
            // ToolBarButton35
            // 
            this.ToolBarButton35.ImageIndex = 5;
            this.ToolBarButton35.Name = "ToolBarButton35";
            this.ToolBarButton35.Tag = "MOVERSIGUIENTE";
            this.ToolBarButton35.Text = "Sig...";
            this.ToolBarButton35.ToolTipText = "Siguiente";
            // 
            // ToolBarButton36
            // 
            this.ToolBarButton36.ImageIndex = 6;
            this.ToolBarButton36.Name = "ToolBarButton36";
            this.ToolBarButton36.Tag = "MOVERULTIMO";
            this.ToolBarButton36.Text = "Fin";
            this.ToolBarButton36.ToolTipText = "Último";
            // 
            // ToolBarButton37
            // 
            this.ToolBarButton37.ImageIndex = 7;
            this.ToolBarButton37.Name = "ToolBarButton37";
            this.ToolBarButton37.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton38
            // 
            this.ToolBarButton38.ImageIndex = 7;
            this.ToolBarButton38.Name = "ToolBarButton38";
            this.ToolBarButton38.Tag = "NUEVO";
            this.ToolBarButton38.Text = "Nuevo";
            this.ToolBarButton38.ToolTipText = "Nuevo";
            // 
            // ToolBarButton39
            // 
            this.ToolBarButton39.ImageIndex = 12;
            this.ToolBarButton39.Name = "ToolBarButton39";
            this.ToolBarButton39.Tag = "EDITAR";
            this.ToolBarButton39.Text = "Editar";
            this.ToolBarButton39.ToolTipText = "Editar";
            // 
            // ToolBarButton40
            // 
            this.ToolBarButton40.ImageIndex = 9;
            this.ToolBarButton40.Name = "ToolBarButton40";
            this.ToolBarButton40.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton41
            // 
            this.ToolBarButton41.ImageIndex = 9;
            this.ToolBarButton41.Name = "ToolBarButton41";
            this.ToolBarButton41.Tag = "CANCELARALTA";
            this.ToolBarButton41.Text = "Canc...";
            this.ToolBarButton41.ToolTipText = "Cancelar";
            // 
            // ToolBarButton42
            // 
            this.ToolBarButton42.ImageIndex = 8;
            this.ToolBarButton42.Name = "ToolBarButton42";
            this.ToolBarButton42.Tag = "GUARDAR";
            this.ToolBarButton42.Text = "Salvar";
            this.ToolBarButton42.ToolTipText = "Guardar";
            // 
            // ToolBarButton43
            // 
            this.ToolBarButton43.Name = "ToolBarButton43";
            this.ToolBarButton43.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton44
            // 
            this.ToolBarButton44.ImageIndex = 10;
            this.ToolBarButton44.Name = "ToolBarButton44";
            this.ToolBarButton44.Tag = "ELIMINAR";
            this.ToolBarButton44.Text = "Borrar";
            this.ToolBarButton44.ToolTipText = "Eliminar";
            // 
            // ToolBarButton45
            // 
            this.ToolBarButton45.Name = "ToolBarButton45";
            this.ToolBarButton45.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton46
            // 
            this.ToolBarButton46.ImageIndex = 13;
            this.ToolBarButton46.Name = "ToolBarButton46";
            this.ToolBarButton46.Tag = "GO";
            this.ToolBarButton46.ToolTipText = "Ir a";
            this.ToolBarButton46.Visible = false;
            // 
            // ToolBarButton47
            // 
            this.ToolBarButton47.ImageIndex = 11;
            this.ToolBarButton47.Name = "ToolBarButton47";
            this.ToolBarButton47.Tag = "LISTADO";
            this.ToolBarButton47.Text = "List...";
            this.ToolBarButton47.ToolTipText = "Listado";
            // 
            // ToolBarButton48
            // 
            this.ToolBarButton48.ImageIndex = 13;
            this.ToolBarButton48.Name = "ToolBarButton48";
            this.ToolBarButton48.Tag = "REGISTRO";
            this.ToolBarButton48.Text = "Reg...";
            this.ToolBarButton48.ToolTipText = "Registro";
            // 
            // ToolBarButton49
            // 
            this.ToolBarButton49.Name = "ToolBarButton49";
            this.ToolBarButton49.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton50
            // 
            this.ToolBarButton50.ImageIndex = 14;
            this.ToolBarButton50.Name = "ToolBarButton50";
            this.ToolBarButton50.Tag = "IMPRIMIR";
            this.ToolBarButton50.Text = "Impr...";
            this.ToolBarButton50.ToolTipText = "Imprimir";
            // 
            // ToolBarButton51
            // 
            this.ToolBarButton51.Name = "ToolBarButton51";
            this.ToolBarButton51.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton83
            // 
            this.ToolBarButton83.ImageIndex = 10;
            this.ToolBarButton83.Name = "ToolBarButton83";
            this.ToolBarButton83.Tag = "CERRAR";
            this.ToolBarButton83.Text = "Cerrar";
            this.ToolBarButton83.ToolTipText = "CErrar formulario";
            // 
            // toolBarButton86
            // 
            this.toolBarButton86.Name = "toolBarButton86";
            this.toolBarButton86.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbrRegistrosStandard
            // 
            this.tbrRegistrosStandard.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            tbrRegistrosStandard.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[]
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
            this.tbrRegistrosStandard.ButtonSize = new System.Drawing.Size(24, 24);
            this.tbrRegistrosStandard.Dock = System.Windows.Forms.DockStyle.None;
            this.tbrRegistrosStandard.DropDownArrows = true;
            this.tbrRegistrosStandard.ImageList = this.imgStandard;
            this.tbrRegistrosStandard.Location = new System.Drawing.Point(32, 100);
            this.tbrRegistrosStandard.Name = "tbrRegistrosStandard";
            this.tbrRegistrosStandard.ShowToolTips = true;
            this.tbrRegistrosStandard.Size = new System.Drawing.Size(900, 30);
            this.tbrRegistrosStandard.TabIndex = 15;
            // 
            // ToolBarButton52
            // 
            this.ToolBarButton52.Name = "ToolBarButton52";
            this.ToolBarButton52.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton53
            // 
            this.ToolBarButton53.ImageIndex = 0;
            this.ToolBarButton53.Name = "ToolBarButton53";
            this.ToolBarButton53.Tag = "BUSCAR";
            this.ToolBarButton53.ToolTipText = "Buscar";
            // 
            // ToolBarButton54
            // 
            this.ToolBarButton54.ImageIndex = 1;
            this.ToolBarButton54.Name = "ToolBarButton54";
            this.ToolBarButton54.Tag = "BUSCARSIGUIENTE";
            this.ToolBarButton54.ToolTipText = "Buscar siguiente";
            // 
            // ToolBarButton55
            // 
            this.ToolBarButton55.Name = "ToolBarButton55";
            this.ToolBarButton55.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton56
            // 
            this.ToolBarButton56.ImageIndex = 2;
            this.ToolBarButton56.Name = "ToolBarButton56";
            this.ToolBarButton56.Tag = "ESTABLECERFILTROS";
            this.ToolBarButton56.ToolTipText = "Establecer filtro";
            // 
            // ToolBarButton57
            // 
            this.ToolBarButton57.Name = "ToolBarButton57";
            this.ToolBarButton57.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton58
            // 
            this.ToolBarButton58.ImageIndex = 3;
            this.ToolBarButton58.Name = "ToolBarButton58";
            this.ToolBarButton58.Tag = "MOVERPRIMERO";
            this.ToolBarButton58.ToolTipText = "Primero";
            // 
            // ToolBarButton59
            // 
            this.ToolBarButton59.ImageIndex = 4;
            this.ToolBarButton59.Name = "ToolBarButton59";
            this.ToolBarButton59.Tag = "MOVERANTERIOR";
            this.ToolBarButton59.ToolTipText = "Anterior";
            // 
            // ToolBarButton60
            // 
            this.ToolBarButton60.ImageIndex = 5;
            this.ToolBarButton60.Name = "ToolBarButton60";
            this.ToolBarButton60.Tag = "MOVERSIGUIENTE";
            this.ToolBarButton60.ToolTipText = "Siguiente";
            // 
            // ToolBarButton61
            // 
            this.ToolBarButton61.ImageIndex = 6;
            this.ToolBarButton61.Name = "ToolBarButton61";
            this.ToolBarButton61.Tag = "MOVERULTIMO";
            this.ToolBarButton61.ToolTipText = "?ltimo";
            // 
            // ToolBarButton62
            // 
            this.ToolBarButton62.ImageIndex = 7;
            this.ToolBarButton62.Name = "ToolBarButton62";
            this.ToolBarButton62.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton63
            // 
            this.ToolBarButton63.ImageIndex = 7;
            this.ToolBarButton63.Name = "ToolBarButton63";
            this.ToolBarButton63.Tag = "NUEVO";
            this.ToolBarButton63.ToolTipText = "Nuevo";
            // 
            // ToolBarButton64
            // 
            this.ToolBarButton64.ImageIndex = 12;
            this.ToolBarButton64.Name = "ToolBarButton64";
            this.ToolBarButton64.Tag = "EDITAR";
            this.ToolBarButton64.ToolTipText = "Editar";
            // 
            // ToolBarButton65
            // 
            this.ToolBarButton65.ImageIndex = 9;
            this.ToolBarButton65.Name = "ToolBarButton65";
            this.ToolBarButton65.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton66
            // 
            this.ToolBarButton66.ImageIndex = 9;
            this.ToolBarButton66.Name = "ToolBarButton66";
            this.ToolBarButton66.Tag = "CANCELARALTA";
            this.ToolBarButton66.ToolTipText = "Cancelar";
            // 
            // ToolBarButton67
            // 
            this.ToolBarButton67.ImageIndex = 8;
            this.ToolBarButton67.Name = "ToolBarButton67";
            this.ToolBarButton67.Tag = "GUARDAR";
            this.ToolBarButton67.ToolTipText = "Guardar";
            // 
            // ToolBarButton68
            // 
            this.ToolBarButton68.Name = "ToolBarButton68";
            this.ToolBarButton68.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton69
            // 
            this.ToolBarButton69.ImageIndex = 10;
            this.ToolBarButton69.Name = "ToolBarButton69";
            this.ToolBarButton69.Tag = "ELIMINAR";
            this.ToolBarButton69.ToolTipText = "Eliminar";
            // 
            // ToolBarButton70
            // 
            this.ToolBarButton70.Name = "ToolBarButton70";
            this.ToolBarButton70.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton71
            // 
            this.ToolBarButton71.ImageIndex = 13;
            this.ToolBarButton71.Name = "ToolBarButton71";
            this.ToolBarButton71.Tag = "GO";
            this.ToolBarButton71.ToolTipText = "Ir a";
            this.ToolBarButton71.Visible = false;
            // 
            // ToolBarButton72
            // 
            this.ToolBarButton72.ImageIndex = 11;
            this.ToolBarButton72.Name = "ToolBarButton72";
            this.ToolBarButton72.Tag = "LISTADO";
            this.ToolBarButton72.ToolTipText = "Listado";
            // 
            // ToolBarButton73
            // 
            this.ToolBarButton73.ImageIndex = 13;
            this.ToolBarButton73.Name = "ToolBarButton73";
            this.ToolBarButton73.Tag = "REGISTRO";
            this.ToolBarButton73.ToolTipText = "Registro";
            // 
            // ToolBarButton74
            // 
            this.ToolBarButton74.Name = "ToolBarButton74";
            this.ToolBarButton74.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton75
            // 
            this.ToolBarButton75.ImageIndex = 14;
            this.ToolBarButton75.Name = "ToolBarButton75";
            this.ToolBarButton75.Tag = "IMPRIMIR";
            this.ToolBarButton75.ToolTipText = "Imprimir";
            // 
            // ToolBarButton76
            // 
            this.ToolBarButton76.Name = "ToolBarButton76";
            this.ToolBarButton76.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton80
            // 
            this.ToolBarButton80.ImageIndex = 10;
            this.ToolBarButton80.Name = "ToolBarButton80";
            this.ToolBarButton80.Tag = "CERRAR";
            this.ToolBarButton80.ToolTipText = "Cerrar formulario";
            // 
            // ToolBarButton81
            // 
            this.ToolBarButton81.Name = "ToolBarButton81";
            this.ToolBarButton81.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton87
            // 
            this.toolBarButton87.Name = "toolBarButton87";
            // 
            // DBToolBarEx
            // 
            this.Controls.Add(this.HScroll1);
            this.Controls.Add(this.lblReg);
            this.Controls.Add(this.tbrRegistrosStandard);
            this.Controls.Add(this.tbrRegistrosStandardBIG);
            this.Controls.Add(this.tbrRegistrosBIG);
            this.Controls.Add(this.tbrRegistros);
            this.Name = "DBToolBarEx";
            this.Size = new System.Drawing.Size(1041, 202);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}