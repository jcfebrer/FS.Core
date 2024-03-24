#region

using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FSLibraryCore;
using FSExceptionCore;
using FSGraphicsCore;
using FSSystemInfoCore;

#endregion

namespace FSFormControlsCore
{
    public class DBForm : Form
    {
        private Color m_GradientEndColor = Color.White;
        private LinearGradientMode m_GradientMode = LinearGradientMode.Horizontal;
        private Color m_GradientStartColor = Color.Blue;
        private Form m_mdiParent;
        private Global.AccessMode m_Mode = Global.AccessMode.ReadMode;
        private bool m_ShowMenu = true;
        //private bool m_ShowStatusBar = true;
        private bool m_ShowToolBar = true;
        private DBControl m_DataControl;
        public DBStatusBar barraEstado;
        private ToolStripStatusLabel estado;
        private ToolStripStatusLabel mensaje;
        private ToolStripStatusLabel info;
        private ToolStripMenuItem menuItem1;
        private DateTime loadTime;


        public DBForm()
        {
            loadTime = DateTime.Now;

            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            barraEstado.Items[0].Text = "";
            barraEstado.Items[1].Text = "";
            barraEstado.Items[2].Text = "";

            mnuContext.Items.Add("&Imprimir", null, PrintDocument);
            mnuContext.Items.Add("&Vista Preliminar", null, PrintPreview);
            mnuContext.Items.Add("&Guardar como HTML", null, SaveAsHTML);
            mnuContext.Items.Add("&Guardar como ASPX", null, SaveAsASPX);
            mnuContext.Items.Add("&Refrescar", null, MnuRefresh);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Filtro", null, MnuFilter);
            mnuContext.Items.Add("&Quitar filtro", null, MnuDelFilter);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Buscar", null, MnuFind);
            mnuContext.Items.Add("&Buscar siguiente", null, MnuFindNext);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Modo Editar", null, MnuEditar);
            mnuContext.Items.Add("Modo &Normal", null, MnuNormal);
            mnuContext.Items.Add("-");
            mnuContext.Items.Add("&Acerca de ...", null, MnuAcercade);

            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = false;
            ((ToolStripMenuItem)mnuContext.Items[13]).Checked = true;


            //Guardamos la referencia al formulario en una variable global para poder consultarlo despues.
            if (!Global.Forms.Exist(this))
                Global.Forms.Add(this);

            this.Load += DBForm_Load;
            this.FormClosing += DBForm_FormClosing;
            this.Shown += DBForm_Shown;
        }

        private void DBForm_Shown(object sender, EventArgs e)
        {
            TimeSpan ts = (DateTime.Now - loadTime);
            barraEstado.Items[2].Text = "LT: " + ts.TotalMilliseconds.ToString("0.###") + " ms.";
        }

        /// <summary>
        /// Asignación del DBcontrol.
        /// Asignamos el parent del dbcontrol cuando se user dl dbcontrol sin asignar a un formulario.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set
            {
                if (value != null && value.Parent is null)
                    value.Parent = this;
                m_DataControl = value;
            }
        }

        public bool CanClose { get; set; } = true;

        //public DbConnection DBConnection { get; set; }

        public Form MDIMain
        {
            get { return m_mdiParent; }
            set
            {
                if (m_mdiParent.IsMdiContainer)
                {
                    m_mdiParent = value;
                    MdiParent = m_mdiParent;
                }
            }
        }

        public bool AlertOnSave { get; set; } = true;

        public bool AutoSave { get; set; }

        public double AutoSaveTime { get; set; } = 10 * 60 * 1000;

        public TabOrderManager.TabScheme TabOrder { get; set; } = TabOrderManager.TabScheme.AcrossFirst;


        public Color GradientStartColor
        {
            get { return m_GradientStartColor; }
            set
            {
                m_GradientStartColor = value;
                Invalidate();
            }
        }

        public Color GradientEndColor
        {
            get { return m_GradientEndColor; }
            set
            {
                m_GradientEndColor = value;
                Invalidate();
            }
        }

        public LinearGradientMode GradientMode
        {
            get { return m_GradientMode; }
            set
            {
                m_GradientMode = value;
                Invalidate();
            }
        }

        public bool Gradient { get; set; }

        public bool ShowContextMenu { get; set; } = true;


        public Global.AccessMode Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                ModeAllControls(Controls, value);
            }
        }


        public bool ShowToolBar
        {
            get { return m_ShowToolBar; }
            set
            {
                m_ShowToolBar = value;
                DbToolBar1.Visible = m_ShowToolBar;
            }
        }

        //public bool ShowStatusBar
        //{
        //    get { return m_ShowStatusBar; }
        //    set
        //    {
        //        m_ShowStatusBar = value;
        //        DbStatusBar1.Visible = m_ShowStatusBar;
        //    }
        //}

        public bool ShowMenu
        {
            get { return m_ShowMenu; }
            set
            {
                m_ShowMenu = value;

                ShowMenuBar(m_ShowMenu);
            }
        }

        public bool AllowNavigate
        {
            get { return DbToolBar1.AllowNavigate; }
            set { DbToolBar1.AllowNavigate = value; }
        }

        public bool AllowSearch
        {
            get { return DbToolBar1.AllowSearch; }
            set { DbToolBar1.AllowSearch = value; }
        }

        public bool AllowCancel
        {
            get { return DbToolBar1.AllowCancel; }
            set { DbToolBar1.AllowCancel = value; }
        }

        public bool AllowSave
        {
            get { return DbToolBar1.AllowSave; }
            set { DbToolBar1.AllowSave = value; }
        }

        public bool AllowAddNew
        {
            get { return DbToolBar1.AllowAddNew; }
            set { DbToolBar1.AllowAddNew = value; }
        }

        public bool AllowPrint
        {
            get { return DbToolBar1.AllowPrint; }
            set { DbToolBar1.AllowPrint = value; }
        }

        public bool AllowFilter
        {
            get { return DbToolBar1.AllowFilter; }
            set { DbToolBar1.AllowFilter = value; }
        }

        public bool AllowRecord
        {
            get { return DbToolBar1.AllowRecord; }
            set { DbToolBar1.AllowRecord = value; }
        }

        public bool AllowEdit
        {
            get { return DbToolBar1.AllowEdit; }
            set { DbToolBar1.AllowEdit = value; }
        }

        public bool AllowDelete
        {
            get { return DbToolBar1.AllowDelete; }
            set { DbToolBar1.AllowDelete = value; }
        }

        public bool AllowList
        {
            get { return DbToolBar1.AllowList; }
            set { DbToolBar1.AllowList = value; }
        }

        public bool ShowNavigate
        {
            get { return DbToolBar1.ShowNavigateButton; }
            set { DbToolBar1.ShowNavigateButton = value; }
        }

        public bool ShowSearch
        {
            get { return DbToolBar1.ShowSearchButton; }
            set { DbToolBar1.ShowSearchButton = value; }
        }

        public bool ShowScrollBar
        {
            get { return DbToolBar1.ShowScrollBar; }
            set { DbToolBar1.ShowScrollBar = value; }
        }

        public bool ShowCancel
        {
            get { return DbToolBar1.ShowCancelButton; }
            set { DbToolBar1.ShowCancelButton = value; }
        }

        public bool ShowSave
        {
            get { return DbToolBar1.ShowSaveButton; }
            set { DbToolBar1.ShowSaveButton = value; }
        }

        public bool ShowAddNew
        {
            get { return DbToolBar1.ShowAddNewButton; }
            set { DbToolBar1.ShowAddNewButton = value; }
        }

        public bool ShowPrint
        {
            get { return DbToolBar1.ShowPrintButton; }
            set { DbToolBar1.ShowPrintButton = value; }
        }

        public bool ShowFilter
        {
            get { return DbToolBar1.ShowFilterButton; }
            set { DbToolBar1.ShowFilterButton = value; }
        }

        public bool ShowRecord
        {
            get { return DbToolBar1.ShowRecordButton; }
            set { DbToolBar1.ShowRecordButton = value; }
        }

        public bool ShowEdit
        {
            get { return DbToolBar1.ShowEditButton; }
            set { DbToolBar1.ShowEditButton = value; }
        }

        public bool ShowDelete
        {
            get { return DbToolBar1.ShowDeleteButton; }
            set { DbToolBar1.ShowDeleteButton = value; }
        }

        public bool ShowList
        {
            get { return DbToolBar1.ShowListButton; }
            set { DbToolBar1.ShowListButton = value; }
        }

        public bool ShowClose
        {
            get { return DbToolBar1.ShowCloseButton; }
            set { DbToolBar1.ShowCloseButton = value; }
        }

        public bool AutomaticConnect { get; set; } = true;

        //public DBStatusBar StatusBar
        //{
        //    get { return DbStatusBar1; }
        //    set { DbStatusBar1 = value; }
        //}

        //public void ProgressStep()
        //{
        //    DbStatusBarProgressPanel1.Step();
        //}

        //public void ProgressReset()
        //{
        //    DbStatusBarProgressPanel1.Reset();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (Gradient)
                GraphicsUtil.DrawGradient(this, m_GradientStartColor, m_GradientEndColor, m_GradientMode);
        }

        private void DBForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataTable dt = null;

            if (AutoSave) 
                DataControl.Save();

            if (AlertOnSave == false) 
                return;

            if (DataControl != null)
            {
                dt = DataControl.HasDataToSave();
                if (dt != null)
                {
                    var frmS = new frmShowTable();
                    frmS.DataTable = dt;
                    frmS.ShowDialog("Tiene datos sin guardar. ¿Está seguro de querer salir?", "Salir");
                    if (frmS.button == frmShowTable.button_enum.no)
                        e.Cancel = true;
                    else
                        e.Cancel = false;
                }
                else
                    e.Cancel = false;
            }
            else
                e.Cancel = false;

            if (!CanClose)
                e.Cancel = true;
        }


        private void DBForm_Load(object sender, EventArgs e)
        {
            //si estamos en modo diseño, no hacer nada
            if(this.Site != null && this.Site.DesignMode)
                return;

            Cursor.Current = Cursors.WaitCursor;

            //DbOfficeMenu1.Start(this);

            UpdateTabPageData(this);


            if (DataControl != null)
            {
                DataControl.ChangeRecord += m_DBControl_ChangeRecord;

                //if (!(DataControl.TypeDB == DBControl.DbType.XML || DataControl.TypeDB == DBControl.DbType.Data))
                //    if (DBConnection == null)
                //    {
                //        DBConnection = DataControl.DBConnection;
                //        if (DBConnection == null)
                //            if (AutomaticConnect)
                //                throw new ExceptionUtil(
                //                    "Imposible establecer conexión con la base de datos. Propiedad DBConnection, no establecida." +
                //                    "\r\n" + "Formulario: " + Name);
                //    }
            }


            if (AutomaticConnect)
            {
                ConnectDBControls(Controls);
                FillCombosAndGrids(Controls);
            }

            DbToolBar1.DataControl = DataControl;

            DbToolBar1.Initialize();


            if (ShowContextMenu == false) ContextMenuStrip = null;

            LinkUnBoundControls(this);


            if (CanClose == false) 
                ShowClose = false;


            //if (DBConnection != null)
            //    if (DBConnection.State == ConnectionState.Open)
            //        DBConnection.Close();

            //DbStatusBar1.Panels[0].Text = "Usuario: " + Global.UserName;

            if (DataControl != null) 
                UpdateScrollBar();

            tmrAutoSave.Enabled = AutoSave;

            if (AutoSaveTime > 0)
                tmrAutoSave.Interval = Convert.ToInt32(AutoSaveTime);
            else
                tmrAutoSave.Enabled = false;

            var tom = new TabOrderManager(this);
            tom.SetTabOrder(TabOrder);

            Cursor.Current = Cursors.Default;
        }


        private void ModeAllControls(Control.ControlCollection frm, Global.AccessMode mode)
        {
            if (frm == null) return;

            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ModeAllControls(ctr.Controls, mode);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).Mode = mode;
                }
        }

        public new void Refresh()
        {
            Reconnect(this);
        }


        private bool Reconnect(Form frm)
        {
            if (frm == null) return false;

            foreach (Control ctr in frm.Controls)
                if (FunctionsForms.IsContainer(ctr))
                {
                    Reconnect((Form) ctr);
                }
                else
                {
                    if (ctr is DBControl) ((DBControl) ctr).ReConnect();
                }

            return false;
        }


        public void InitializeScrollBar()
        {
            DbToolBar1.DataControl = DataControl;
            DbToolBar1.Initialize();
            DataControl.Go(0);
        }


        private void LinkUnBoundControls(Control frm)
        {
            try
            {
                foreach (Control ctr in frm.Controls)
                {
                    if (FunctionsForms.IsContainer(ctr))
                        LinkUnBoundControls(ctr);


                    if (ctr is DBCheckBox)
                    {
                        if (((DBCheckBox)ctr).DataControl == null)
                            ((DBCheckBox)ctr).UpdateCheckBox();
                    }

                    if (ctr is DBTextBox)
                    {
                        if (((DBTextBox)ctr).DataControl == null)
                            ((DBTextBox)ctr).UpdateText();
                    }

                    if (ctr is DBFindTextBox)
                    {
                        if (((DBFindTextBox)ctr).DataControl == null)
                            ((DBFindTextBox)ctr).UpdateText();
                    }
                }
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        //private void TrackMode(Control frm, bool mode)
        //{
        //    try
        //    {
        //        foreach (Control ctr in frm.Controls)
        //        {
        //            if (FunctionsForms.IsContainer(ctr)) TrackMode(ctr, mode);
        //            if (ctr is DBUserControl) ((DBUserControl) ctr).Track = mode;
        //        }
        //    }
        //    catch (ExceptionUtil ex)
        //    {
        //        throw new ExceptionUtil(ex);
        //    }
        //}


        public void Save()
        {
            DbToolBar1.Save();
        }


        private void MnuEditar(object sender, EventArgs e)
        {
            //TrackMode(this, true);

            ((ToolStripMenuItem)mnuContext.Items[11]).Checked = true;
            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = false;
        }


        private void MnuNormal(object sender, EventArgs e)
        {
            //TrackMode(this, false);

            ((ToolStripMenuItem)mnuContext.Items[11]).Checked = false;
            ((ToolStripMenuItem)mnuContext.Items[12]).Checked = true;
        }


        private void MnuFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.ShowFilter();
        }


        private void MnuRefresh(object sender, EventArgs e)
        {
            Refresh();
        }


        private void MnuDelFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.DeleteFilter();
        }


        private void MnuFind(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.ShowFind();
        }


        private void MnuAcercade(object sender, EventArgs e)
        {
            try
            {
                var s = new frmAbout();
                s.ShowDialog();
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void MnuFindNext(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.FindNext();
        }


        private void SaveAsHTML(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.ShowDialog();
                var fic = SaveFileDialog1.FileName;
                if (fic == "") return;
                var tw = new StreamWriter(fic);

                //var dbform2html = new ConvertToHtml();
                string convertStr = "Funcionalidad no implementada.";
                //= dbform2html.GenerateHTML(this);
                tw.Write(convertStr);
                tw.Close();
                tw = null;
                ProcessUtil.OpenDocument(fic);
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil("Errores en la exportación.", ex);
            }
        }

        private void SaveAsASPX(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog1.ShowDialog();
                var fic = SaveFileDialog1.FileName;
                if (fic == "") return;

                //var dbform2aspx = new Convert2Aspx(Convert2Aspx.AspxTypes.Page);
                //dbform2aspx.Convert(this, Path.GetDirectoryName(fic));
                //ProcessUtil.OpenDocument(fic);
                throw new Exception("Funcionalidad no implementada.");
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil("Errores en la exportación.", ex);
            }
        }


        private void PrintDocument(object sender, EventArgs e)
        {
            var fp = new DBFormPrint(this);
            fp.PrintPreview = false;
            fp.Print();
        }


        private void PrintPreview(object sender, EventArgs e)
        {
            var fp = new DBFormPrint(this);
            fp.PrintPreview = true;
            fp.Print();
        }


        private void ConnectDBControls(Control.ControlCollection frm)
        {
            if (frm == null) 
                return;


            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    ConnectDBControls(ctr.Controls);
                }
                else
                {
                    if (ctr is DBControl)
                        if (((DBControl) ctr).RelationDataControl == null)
                        {
                            if (!string.IsNullOrEmpty(((DBControl) ctr).Selection))
                            {
                                //if (DBConnection != null)
                                //{
                                //    ((DBControl) ctr).DBConnection = DBConnection;
                                //    if (((DBControl) ctr).AutoConnect) 
                                //        ((DBControl) ctr).Connect();
                                //}
                                if (((DBControl)ctr).AutoConnect)
                                    ((DBControl)ctr).Connect();
                            }
                            else
                            {
                                if (((DBControl) ctr).TypeDB == DBControl.DbType.XML ||
                                    ((DBControl) ctr).TypeDB == DBControl.DbType.Data)
                                    if (((DBControl) ctr).AutoConnect)
                                        ((DBControl) ctr).Connect();
                            }
                        }
                }
        }

        private void FillCombosAndGrids(Control.ControlCollection frm)
        {
            if (frm == null) return;


            foreach (Control ctr in frm)
                if (FunctionsForms.IsContainer(ctr))
                {
                    FillCombosAndGrids(ctr.Controls);
                }
                else
                {
                    if (ctr is DBCombo)
                        ((DBCombo)ctr).Fill();

                    if (ctr is DBGridView)
                        ((DBGridView)ctr).Fill();
                }
        }

        private void mnuConfigurarPagina_Click(object sender, EventArgs e)
        {
            PageSetup.Setup();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ////Convert the form to an ASP.NET Web Form
                    //Convert2Aspx convert2Aspx = new Convert2Aspx();
                    //convert2Aspx.AspxType = Convert2Aspx.AspxTypes.Page;
                    //convert2Aspx.SourceLanguage = Convert2Aspx.SourceLanguages.C_Sharp;
                    //convert2Aspx.Convert(this, Path.GetDirectoryName(saveDialog.FileName));

                    ////Convert the form to an ASP.NET user control
                    //Convert2Aspx convert2AspxUC = new Convert2Aspx();
                    //convert2AspxUC.AspxType = Convert2Aspx.AspxTypes.UserControl;
                    //convert2AspxUC.SourceLanguage = Convert2Aspx.SourceLanguages.C_Sharp;
                    //convert2AspxUC.RootName = this.Name + "UC";
                    //convert2AspxUC.Convert(this, Path.GetDirectoryName(saveDialog.FileName));
                    throw new Exception("Funcionalidad no implementada.");
                }
                catch (Exception ex)
                {
                    Error.ErrorMessage(ex);
                }
            }
        }

        private void mnuCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowMenuBar(bool show)
        {
            mnuForm.Visible = show;
            for (var f = 0; f <= mnuFormMain.Items.Count - 1; f++) mnuFormMain.Items[f].Visible = show;
        }

        private void mnuCalculadora_Click(object sender, EventArgs e)
        {
            var fCalc = new frmCalculator();
            fCalc.Show();
        }


        public void UpdateTabPageData(Control con)
        {
            if (con.HasChildren)
                foreach (Control subcon in con.Controls)
                    if (subcon is TabPage | subcon is TabControl)
                    {
                        if (subcon is TabPage) ((TabPage) subcon).Visible = true;
                        UpdateTabPageData(subcon);
                    }
        }


        private void m_DBControl_ChangeRecord()
        {
            UpdateScrollBar();
        }


        private void UpdateScrollBar()
        {
            //DbStatusBar1.Panels[2].Text = "SQL: " + DataControl.Selection;
            //DbStatusBar1.Panels[3].Text = "Tabla: " + DataControl.TableName;
            //DbStatusBar1.Panels[4].Text = DataControl.DBPosition + 1 + "/" + DataControl.RecordCount();
        }


        private void tmrAutoSave_Tick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //var tx = StatusBar.Panels[0].Text;
            //StatusBar.Panels[0].Text = "Guardando datos ...";
            DataControl.Save();
            //StatusBar.Panels[0].Text = "Datos guardados";
            //StatusBar.Panels[0].Text = tx;
            Cursor.Current = Cursors.Default;
        }


        private void mnuAcercaDe_Click(object sender, EventArgs e)
        {
            try
            {
                var s = new frmAbout();
                s.ShowDialog();
            }
            catch (ExceptionUtil ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal TabOrderSchemaProvider DbTabOrderSchemeProvider1;
        public DBToolBarEx DbToolBar1;
        internal ToolStripMenuItem MenuItem3;
        internal ToolStripMenuItem MenuItem7;
        internal SaveFileDialog SaveFileDialog1;
        private IContainer components;
        internal ToolStripMenuItem mnuAbout;
        internal ToolStripMenuItem mnuCalc;
        internal ToolStripMenuItem mnuClose;
        internal ToolStripMenuItem mnuConfPag;
        internal ContextMenuStrip mnuContext;
        public ToolStripMenuItem mnuForm;
        public MenuStrip mnuFormMain;
        internal Timer tmrAutoSave;
        

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;

        //        return cp;
        //    }
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);

            Global.Forms.Remove(this);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(DBForm));
            mnuFormMain = new MenuStrip();
            mnuForm = new ToolStripMenuItem();
            mnuConfPag = new ToolStripMenuItem();
            menuItem1 = new ToolStripMenuItem();
            mnuCalc = new ToolStripMenuItem();
            MenuItem7 = new ToolStripMenuItem();
            mnuAbout = new ToolStripMenuItem();
            MenuItem3 = new ToolStripMenuItem();
            mnuClose = new ToolStripMenuItem();
            mnuContext = new ContextMenuStrip(components);
            SaveFileDialog1 = new SaveFileDialog();
            tmrAutoSave = new Timer(components);
            barraEstado = new DBStatusBar();
            estado = new ToolStripStatusLabel();
            mensaje = new ToolStripStatusLabel();
            info = new ToolStripStatusLabel();
            DbToolBar1 = new DBToolBarEx();
            DbTabOrderSchemeProvider1 = new TabOrderSchemaProvider();
            mnuFormMain.SuspendLayout();
            ((ISupportInitialize)barraEstado).BeginInit();
            barraEstado.SuspendLayout();
            SuspendLayout();
            // 
            // mnuFormMain
            // 
            mnuFormMain.Items.AddRange(new ToolStripItem[] { mnuForm });
            mnuFormMain.Location = new Point(0, 0);
            mnuFormMain.Name = "mnuFormMain";
            mnuFormMain.Size = new Size(200, 24);
            mnuFormMain.TabIndex = 0;
            // 
            // mnuForm
            // 
            mnuForm.DropDownItems.AddRange(new ToolStripItem[] { mnuConfPag, menuItem1, mnuCalc, MenuItem7, mnuAbout, MenuItem3, mnuClose });
            mnuForm.Name = "mnuForm";
            mnuForm.Size = new Size(77, 20);
            mnuForm.Text = "&Formulario";
            // 
            // mnuConfPag
            // 
            mnuConfPag.Name = "mnuConfPag";
            mnuConfPag.Size = new Size(222, 22);
            mnuConfPag.Text = "&Configuración página";
            mnuConfPag.Click += mnuConfigurarPagina_Click;
            // 
            // menuItem1
            // 
            menuItem1.Name = "menuItem1";
            menuItem1.Size = new Size(222, 22);
            menuItem1.Text = "Convertir formulario a ASPX";
            menuItem1.Click += menuItem1_Click;
            // 
            // mnuCalc
            // 
            mnuCalc.Name = "mnuCalc";
            mnuCalc.Size = new Size(222, 22);
            mnuCalc.Text = "Calculadora";
            mnuCalc.Click += mnuCalculadora_Click;
            // 
            // MenuItem7
            // 
            MenuItem7.Name = "MenuItem7";
            MenuItem7.Size = new Size(222, 22);
            MenuItem7.Text = "-";
            // 
            // mnuAbout
            // 
            mnuAbout.Name = "mnuAbout";
            mnuAbout.Size = new Size(222, 22);
            mnuAbout.Text = "&Acerca de ...";
            mnuAbout.Click += mnuAcercaDe_Click;
            // 
            // MenuItem3
            // 
            MenuItem3.Name = "MenuItem3";
            MenuItem3.Size = new Size(222, 22);
            MenuItem3.Text = "-";
            // 
            // mnuClose
            // 
            mnuClose.Name = "mnuClose";
            mnuClose.Size = new Size(222, 22);
            mnuClose.Text = "&Cerrar";
            mnuClose.Click += mnuCerrar_Click;
            // 
            // mnuContext
            // 
            mnuContext.Name = "mnuContext";
            mnuContext.Size = new Size(61, 4);
            // 
            // SaveFileDialog1
            // 
            SaveFileDialog1.Filter = "Archivos HTML|*.htm*|Todos los archivos|*.*";
            // 
            // tmrAutoSave
            // 
            tmrAutoSave.Interval = 60000;
            tmrAutoSave.Tick += tmrAutoSave_Tick;
            // 
            // barraEstado
            // 
            barraEstado.Items.AddRange(new ToolStripItem[] { estado, mensaje, info });
            barraEstado.Location = new Point(0, 394);
            barraEstado.Name = "barraEstado";
            barraEstado.Size = new Size(1154, 22);
            barraEstado.TabIndex = 2;
            barraEstado.Text = "dbStatusBar1";
            barraEstado.ViewStyle = DBStatusBar.ViewStyleEnum.Default;
            barraEstado.WrapText = false;
            // 
            // estado
            // 
            estado.Name = "estado";
            estado.Size = new Size(0, 17);
            // 
            // mensaje
            // 
            mensaje.Name = "mensaje";
            mensaje.Size = new Size(0, 17);
            // 
            // info
            // 
            info.Alignment = ToolStripItemAlignment.Right;
            info.Name = "info";
            info.Size = new Size(0, 17);
            // 
            // DbToolBar1
            // 
            DbToolBar1.About = "";
            DbToolBar1.AllowAddNew = true;
            DbToolBar1.AllowCancel = true;
            DbToolBar1.AllowClose = true;
            DbToolBar1.AllowDelete = true;
            DbToolBar1.AllowEdit = true;
            DbToolBar1.AllowFilter = true;
            DbToolBar1.AllowList = true;
            DbToolBar1.AllowNavigate = true;
            DbToolBar1.AllowPrint = true;
            DbToolBar1.AllowRecord = true;
            DbToolBar1.AllowSave = true;
            DbToolBar1.AllowSearch = true;
            DbToolBar1.DataControl = null;
            DbToolBar1.Dock = DockStyle.Top;
            DbToolBar1.Location = new Point(0, 0);
            DbToolBar1.Name = "DbToolBar1";
            DbToolBar1.ShowAddNewButton = true;
            DbToolBar1.ShowCancelButton = true;
            DbToolBar1.ShowCloseButton = true;
            DbToolBar1.ShowDeleteButton = true;
            DbToolBar1.ShowEditButton = true;
            DbToolBar1.ShowFilterButton = true;
            DbToolBar1.ShowListButton = true;
            DbToolBar1.ShowNavigateButton = true;
            DbToolBar1.ShowPrintButton = true;
            DbToolBar1.ShowRecordButton = true;
            DbToolBar1.ShowSaveButton = true;
            DbToolBar1.ShowScrollBar = true;
            DbToolBar1.ShowSearchButton = true;
            DbToolBar1.ShowText = true;
            DbToolBar1.Size = new Size(1483, 25);
            DbToolBar1.TabIndex = 1;
            DbToolBar1.TabStop = false;
            DbToolBar1.Value = 0;
            DbToolBar1.VisibleScroll = true;
            DbToolBar1.VisibleTotalRecord = false;
            // 
            // DBForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1154, 416);
            ContextMenuStrip = mnuContext;
            Controls.Add(barraEstado);
            Controls.Add(DbToolBar1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mnuFormMain;
            Name = "DBForm";
            Text = "DBForm";
            mnuFormMain.ResumeLayout(false);
            mnuFormMain.PerformLayout();
            ((ISupportInitialize)barraEstado).EndInit();
            barraEstado.ResumeLayout(false);
            barraEstado.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}