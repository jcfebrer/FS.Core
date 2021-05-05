
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;


using System.Xml;
using FSFormControls;
using System.Configuration;
using FSDatabase;
using FSLibrary;
using FSException;

namespace FSGestion
{
	public class mdiPrincipal : System.Windows.Forms.Form
	{
		
		frmLogin frmL;
		
#region  Código generado por el Diseñador de Windows Forms
		
		public mdiPrincipal()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			//Agregar cualquier inicialización después de la llamada a InitializeComponent()
			Global.mdiP = this;
		}
		
		//Form reemplaza a Dispose para limpiar la lista de componentes.
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

        private System.ComponentModel.IContainer components;

        //Requerido por el Diseñador de Windows Forms
		
		//NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
		//Puede modificarse utilizando el Diseñador de Windows Forms.
		//No lo modifique con el editor de código.
		internal System.Windows.Forms.ToolBar ToolBar1;
		internal System.Windows.Forms.ImageList ImageList1;
		internal System.Windows.Forms.MainMenu MainMenu1;
		internal System.Windows.Forms.MenuItem MenuItem1;
		internal System.Windows.Forms.MenuItem MenuItem2;
		internal System.Windows.Forms.ToolBarButton ToolBarButton1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButton3;
		internal System.Windows.Forms.ToolBarButton ToolBarButton4;
		internal System.Windows.Forms.ToolBarButton ToolBarButton5;
		internal System.Windows.Forms.ToolBarButton ToolBarButton6;
		internal System.Windows.Forms.ToolBarButton ToolBarButton7;
		internal System.Windows.Forms.ToolBarButton ToolBarButton8;
		internal System.Windows.Forms.ToolBarButton ToolBarButton9;
		internal FSFormControls.DBControl DbControl1;
		internal FSFormControls.DBControl DbControl2;
		internal System.Windows.Forms.ContextMenu ContextMenu1;
		internal System.Windows.Forms.MenuItem MenuItem4;
		internal FSFormControls.DBGroupBoxXPList DbGroupBoxXPList1;
		internal FSFormControls.DBGroupBoxXP DbGroupBoxXP2;
		internal FSFormControls.DBGroupBoxXP DbGroupBoxXP1;
		internal FSFormControls.DBTreeView DbTreeViewClientes;
        internal FSFormControls.DBOfficeMenu DbOfficeMenu1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton10;
		internal System.Windows.Forms.ToolBarButton ToolBarButton11;
		internal FSFormControls.DBGroupBoxXP DbGroupBoxXP3;
		internal FSFormControls.DBTreeView DbTreeViewFacturas;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.Label lblUsuario;
		internal System.Windows.Forms.Label lblUltimaConexion;
		internal System.Windows.Forms.ToolBarButton ToolBarButton12;
		internal System.Windows.Forms.ToolBarButton ToolBarButton13;
		internal System.Windows.Forms.MenuItem MenuItem5;
		internal System.Windows.Forms.MenuItem MenuItem6;
		internal System.Windows.Forms.MenuItem MenuItem7;
		internal System.Windows.Forms.MenuItem MenuItem8;
		internal System.Windows.Forms.MenuItem MenuItem9;
		internal System.Windows.Forms.ToolBarButton ToolBarButton15;
		internal System.Windows.Forms.ToolBarButton ToolBarButton14;
		internal System.Windows.Forms.MenuItem MenuItem3;
		internal System.Windows.Forms.MenuItem MenuItem11;
        private MenuItem menuItem12;
        private MenuItem menuItem13;
        internal Splitter Splitter1;
        private MenuItem menuItem14;
        private MenuItem menuItem15;
        internal DBGroupBoxXP dbGroupBoxXP4;
        internal DBTreeView dbTreeViewDispositivos;
        private ToolBarButton toolBarButton17;
        private ToolBarButton toolBarButton16;
        private MenuItem menuItem16;
        internal System.Windows.Forms.MenuItem MenuItem10;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiPrincipal));
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton17 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton16 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton15 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton14 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton10 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton8 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton11 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton12 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton13 = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButton9 = new System.Windows.Forms.ToolBarButton();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.MenuItem5 = new System.Windows.Forms.MenuItem();
            this.MenuItem7 = new System.Windows.Forms.MenuItem();
            this.MenuItem8 = new System.Windows.Forms.MenuItem();
            this.MenuItem9 = new System.Windows.Forms.MenuItem();
            this.MenuItem3 = new System.Windows.Forms.MenuItem();
            this.MenuItem11 = new System.Windows.Forms.MenuItem();
            this.MenuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.MenuItem6 = new System.Windows.Forms.MenuItem();
            this.MenuItem2 = new System.Windows.Forms.MenuItem();
            this.ContextMenu1 = new System.Windows.Forms.ContextMenu();
            this.MenuItem4 = new System.Windows.Forms.MenuItem();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.DbGroupBoxXPList1 = new FSFormControls.DBGroupBoxXPList();
            this.dbGroupBoxXP4 = new FSFormControls.DBGroupBoxXP();
            this.dbTreeViewDispositivos = new FSFormControls.DBTreeView();
            this.DbGroupBoxXP3 = new FSFormControls.DBGroupBoxXP();
            this.DbTreeViewFacturas = new FSFormControls.DBTreeView();
            this.DbGroupBoxXP2 = new FSFormControls.DBGroupBoxXP();
            this.lblUltimaConexion = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.DbGroupBoxXP1 = new FSFormControls.DBGroupBoxXP();
            this.DbTreeViewClientes = new FSFormControls.DBTreeView();
            this.DbControl2 = new FSFormControls.DBControl();
            this.DbControl1 = new FSFormControls.DBControl();
            this.DbOfficeMenu1 = new FSFormControls.DBOfficeMenu(this.components);
            this.DbGroupBoxXPList1.SuspendLayout();
            this.dbGroupBoxXP4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbTreeViewDispositivos)).BeginInit();
            this.DbGroupBoxXP3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DbTreeViewFacturas)).BeginInit();
            this.DbGroupBoxXP2.SuspendLayout();
            this.DbGroupBoxXP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DbTreeViewClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolBar1
            // 
            this.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton17,
            this.toolBarButton16,
            this.ToolBarButton1,
            this.ToolBarButton15,
            this.ToolBarButton14,
            this.ToolBarButton2,
            this.ToolBarButton3,
            this.ToolBarButton4,
            this.ToolBarButton6,
            this.ToolBarButton7,
            this.ToolBarButton10,
            this.ToolBarButton8,
            this.ToolBarButton5,
            this.ToolBarButton11,
            this.ToolBarButton12,
            this.ToolBarButton13,
            this.ToolBarButton9});
            this.ToolBar1.ButtonSize = new System.Drawing.Size(24, 24);
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.ImageList = this.ImageList1;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(1027, 138);
            this.ToolBar1.TabIndex = 1;
            this.ToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
            // 
            // toolBarButton17
            // 
            this.toolBarButton17.ImageIndex = 1;
            this.toolBarButton17.Name = "toolBarButton17";
            this.toolBarButton17.Text = "Dispositivos";
            // 
            // toolBarButton16
            // 
            this.toolBarButton16.Name = "toolBarButton16";
            this.toolBarButton16.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton1
            // 
            this.ToolBarButton1.ImageIndex = 4;
            this.ToolBarButton1.Name = "ToolBarButton1";
            this.ToolBarButton1.Tag = "CLIENTES";
            this.ToolBarButton1.Text = "Clientes";
            this.ToolBarButton1.ToolTipText = "Clientes";
            // 
            // ToolBarButton15
            // 
            this.ToolBarButton15.Name = "ToolBarButton15";
            this.ToolBarButton15.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton14
            // 
            this.ToolBarButton14.ImageIndex = 3;
            this.ToolBarButton14.Name = "ToolBarButton14";
            this.ToolBarButton14.Tag = "POTENCIALES";
            this.ToolBarButton14.Text = "Potenciales";
            this.ToolBarButton14.ToolTipText = "Clientes Potenciales";
            // 
            // ToolBarButton2
            // 
            this.ToolBarButton2.Name = "ToolBarButton2";
            this.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton3
            // 
            this.ToolBarButton3.Enabled = false;
            this.ToolBarButton3.ImageIndex = 3;
            this.ToolBarButton3.Name = "ToolBarButton3";
            this.ToolBarButton3.Tag = "USUARIOS";
            this.ToolBarButton3.Text = "Mantenimiento Usuarios";
            this.ToolBarButton3.ToolTipText = "Mantenimiento Usuarios";
            // 
            // ToolBarButton4
            // 
            this.ToolBarButton4.Name = "ToolBarButton4";
            this.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton6
            // 
            this.ToolBarButton6.ImageIndex = 0;
            this.ToolBarButton6.Name = "ToolBarButton6";
            this.ToolBarButton6.Tag = "LOGIN";
            this.ToolBarButton6.Text = "Login";
            this.ToolBarButton6.ToolTipText = "Login";
            // 
            // ToolBarButton7
            // 
            this.ToolBarButton7.Name = "ToolBarButton7";
            this.ToolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton10
            // 
            this.ToolBarButton10.ImageIndex = 4;
            this.ToolBarButton10.Name = "ToolBarButton10";
            this.ToolBarButton10.Tag = "ARTICULOS";
            this.ToolBarButton10.Text = "Articulos";
            // 
            // ToolBarButton8
            // 
            this.ToolBarButton8.Name = "ToolBarButton8";
            this.ToolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton5
            // 
            this.ToolBarButton5.ImageIndex = 1;
            this.ToolBarButton5.Name = "ToolBarButton5";
            this.ToolBarButton5.Tag = "IMPRESION";
            this.ToolBarButton5.Text = "Modulo de Impresión";
            this.ToolBarButton5.ToolTipText = "Modulo de Impresión";
            // 
            // ToolBarButton11
            // 
            this.ToolBarButton11.Name = "ToolBarButton11";
            this.ToolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton12
            // 
            this.ToolBarButton12.ImageIndex = 4;
            this.ToolBarButton12.Name = "ToolBarButton12";
            this.ToolBarButton12.Tag = "FACTURACION";
            this.ToolBarButton12.Text = "Facturación";
            this.ToolBarButton12.ToolTipText = "Facturación";
            // 
            // ToolBarButton13
            // 
            this.ToolBarButton13.Name = "ToolBarButton13";
            this.ToolBarButton13.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButton9
            // 
            this.ToolBarButton9.ImageIndex = 5;
            this.ToolBarButton9.Name = "ToolBarButton9";
            this.ToolBarButton9.Tag = "SALIR";
            this.ToolBarButton9.Text = "Salir";
            this.ToolBarButton9.ToolTipText = "Salir";
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            this.ImageList1.Images.SetKeyName(3, "");
            this.ImageList1.Images.SetKeyName(4, "");
            this.ImageList1.Images.SetKeyName(5, "");
            // 
            // MainMenu1
            // 
            this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem1});
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 0;
            this.MenuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem14,
            this.menuItem15,
            this.MenuItem5,
            this.MenuItem7,
            this.MenuItem8,
            this.MenuItem9,
            this.MenuItem3,
            this.MenuItem11,
            this.MenuItem10,
            this.menuItem12,
            this.menuItem13,
            this.menuItem16,
            this.MenuItem6,
            this.MenuItem2});
            this.MenuItem1.Text = "Archivo";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 0;
            this.menuItem14.Text = "Aplicaciones";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 1;
            this.menuItem15.Text = "-";
            // 
            // MenuItem5
            // 
            this.MenuItem5.Index = 2;
            this.MenuItem5.Text = "&Preferencias";
            this.MenuItem5.Click += new System.EventHandler(this.MenuItem5_Click);
            // 
            // MenuItem7
            // 
            this.MenuItem7.Index = 3;
            this.MenuItem7.Text = "Empresas";
            this.MenuItem7.Click += new System.EventHandler(this.MenuItem7_Click);
            // 
            // MenuItem8
            // 
            this.MenuItem8.Index = 4;
            this.MenuItem8.Text = "Comunicaciones";
            this.MenuItem8.Click += new System.EventHandler(this.MenuItem8_Click);
            // 
            // MenuItem9
            // 
            this.MenuItem9.Index = 5;
            this.MenuItem9.Text = "Envio de SMS";
            this.MenuItem9.Click += new System.EventHandler(this.MenuItem9_Click);
            // 
            // MenuItem3
            // 
            this.MenuItem3.Index = 6;
            this.MenuItem3.Text = "SnapShot";
            this.MenuItem3.Click += new System.EventHandler(this.MenuItem3_Click);
            // 
            // MenuItem11
            // 
            this.MenuItem11.Index = 7;
            this.MenuItem11.Text = "Asistente";
            this.MenuItem11.Click += new System.EventHandler(this.MenuItem11_Click);
            // 
            // MenuItem10
            // 
            this.MenuItem10.Index = 8;
            this.MenuItem10.Text = "Html Parser";
            this.MenuItem10.Click += new System.EventHandler(this.MenuItem10_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 9;
            this.menuItem12.Text = "Crypto";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 10;
            this.menuItem13.Text = "Google Maps";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 11;
            this.menuItem16.Text = "Google Firebase";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // MenuItem6
            // 
            this.MenuItem6.Index = 12;
            this.MenuItem6.Text = "-";
            // 
            // MenuItem2
            // 
            this.MenuItem2.Index = 13;
            this.MenuItem2.Text = "Salir";
            this.MenuItem2.Click += new System.EventHandler(this.MenuItem2_Click);
            // 
            // ContextMenu1
            // 
            this.ContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem4});
            // 
            // MenuItem4
            // 
            this.MenuItem4.Index = 0;
            this.MenuItem4.Text = "Refrescar";
            this.MenuItem4.Click += new System.EventHandler(this.MenuItem4_Click);
            // 
            // Splitter1
            // 
            this.Splitter1.Location = new System.Drawing.Point(208, 138);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(4, 110);
            this.Splitter1.TabIndex = 16;
            this.Splitter1.TabStop = false;
            // 
            // DbGroupBoxXPList1
            // 
            this.DbGroupBoxXPList1.AutoScroll = true;
            this.DbGroupBoxXPList1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(117)))), ((int)(((byte)(222)))));
            this.DbGroupBoxXPList1.Controls.Add(this.dbGroupBoxXP4);
            this.DbGroupBoxXPList1.Controls.Add(this.DbGroupBoxXP3);
            this.DbGroupBoxXPList1.Controls.Add(this.DbGroupBoxXP2);
            this.DbGroupBoxXPList1.Controls.Add(this.DbGroupBoxXP1);
            this.DbGroupBoxXPList1.Dock = System.Windows.Forms.DockStyle.Left;
            this.DbGroupBoxXPList1.Location = new System.Drawing.Point(0, 138);
            this.DbGroupBoxXPList1.Name = "DbGroupBoxXPList1";
            this.DbGroupBoxXPList1.Size = new System.Drawing.Size(208, 110);
            this.DbGroupBoxXPList1.TabIndex = 15;
            // 
            // dbGroupBoxXP4
            // 
            this.dbGroupBoxXP4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbGroupBoxXP4.BackColor = System.Drawing.Color.Transparent;
            this.dbGroupBoxXP4.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.dbGroupBoxXP4.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.dbGroupBoxXP4.CaptionFormatFlag = FSFormControls.DBGroupBoxXP.FormatFlag.NoWrap;
            this.dbGroupBoxXP4.CaptionStyle = FSFormControls.DBGroupBoxXP.CaptionStyleEnum.Normal;
            this.dbGroupBoxXP4.CaptionText = "Dispositivos";
            this.dbGroupBoxXP4.CaptionTextAlign = FSFormControls.DBGroupBoxXP.CaptionTextAlignment.Left;
            this.dbGroupBoxXP4.ChevronStyle = FSFormControls.DBGroupBoxXP.ChevronStyleEnum.Image;
            this.dbGroupBoxXP4.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("dbGroupBoxXP4.CollapsedHighlightImage")));
            this.dbGroupBoxXP4.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("dbGroupBoxXP4.CollapsedImage")));
            this.dbGroupBoxXP4.Controls.Add(this.dbTreeViewDispositivos);
            this.dbGroupBoxXP4.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("dbGroupBoxXP4.ExpandedHighlightImage")));
            this.dbGroupBoxXP4.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("dbGroupBoxXP4.ExpandedImage")));
            this.dbGroupBoxXP4.Location = new System.Drawing.Point(8, 481);
            this.dbGroupBoxXP4.Name = "dbGroupBoxXP4";
            this.dbGroupBoxXP4.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.dbGroupBoxXP4.Size = new System.Drawing.Size(184, 193);
            this.dbGroupBoxXP4.TabIndex = 3;
            this.dbGroupBoxXP4.Tag = 0;
            this.dbGroupBoxXP4.TooltipText = null;
            // 
            // dbTreeViewDispositivos
            // 
            this.dbTreeViewDispositivos.About = null;
            this.dbTreeViewDispositivos.AllowLoadXML = false;
            this.dbTreeViewDispositivos.AllowSaveXML = true;
            this.dbTreeViewDispositivos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbTreeViewDispositivos.DataControl = null;
            this.dbTreeViewDispositivos.EnableReArrange = false;
            this.dbTreeViewDispositivos.HideSelection = true;
            this.dbTreeViewDispositivos.HotTracking = false;
            this.dbTreeViewDispositivos.Level = 0;
            this.dbTreeViewDispositivos.Location = new System.Drawing.Point(12, 28);
            this.dbTreeViewDispositivos.Name = "dbTreeViewDispositivos";
            this.dbTreeViewDispositivos.SelectedNode = null;
            this.dbTreeViewDispositivos.ShowLines = true;
            this.dbTreeViewDispositivos.ShowRootLines = true;
            this.dbTreeViewDispositivos.Size = new System.Drawing.Size(157, 154);
            this.dbTreeViewDispositivos.TabIndex = 4;
            this.dbTreeViewDispositivos.Track = false;
            // 
            // DbGroupBoxXP3
            // 
            this.DbGroupBoxXP3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbGroupBoxXP3.BackColor = System.Drawing.Color.Transparent;
            this.DbGroupBoxXP3.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.DbGroupBoxXP3.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.DbGroupBoxXP3.CaptionFormatFlag = FSFormControls.DBGroupBoxXP.FormatFlag.NoWrap;
            this.DbGroupBoxXP3.CaptionStyle = FSFormControls.DBGroupBoxXP.CaptionStyleEnum.Normal;
            this.DbGroupBoxXP3.CaptionText = "Facturas";
            this.DbGroupBoxXP3.CaptionTextAlign = FSFormControls.DBGroupBoxXP.CaptionTextAlignment.Left;
            this.DbGroupBoxXP3.ChevronStyle = FSFormControls.DBGroupBoxXP.ChevronStyleEnum.Image;
            this.DbGroupBoxXP3.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP3.CollapsedHighlightImage")));
            this.DbGroupBoxXP3.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP3.CollapsedImage")));
            this.DbGroupBoxXP3.Controls.Add(this.DbTreeViewFacturas);
            this.DbGroupBoxXP3.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP3.ExpandedHighlightImage")));
            this.DbGroupBoxXP3.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP3.ExpandedImage")));
            this.DbGroupBoxXP3.Location = new System.Drawing.Point(8, 10);
            this.DbGroupBoxXP3.Name = "DbGroupBoxXP3";
            this.DbGroupBoxXP3.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.DbGroupBoxXP3.Size = new System.Drawing.Size(184, 148);
            this.DbGroupBoxXP3.TabIndex = 2;
            this.DbGroupBoxXP3.Tag = 1;
            this.DbGroupBoxXP3.TooltipText = null;
            // 
            // DbTreeViewFacturas
            // 
            this.DbTreeViewFacturas.About = null;
            this.DbTreeViewFacturas.AllowLoadXML = false;
            this.DbTreeViewFacturas.AllowSaveXML = true;
            this.DbTreeViewFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbTreeViewFacturas.DataControl = null;
            this.DbTreeViewFacturas.EnableReArrange = false;
            this.DbTreeViewFacturas.HideSelection = true;
            this.DbTreeViewFacturas.HotTracking = false;
            this.DbTreeViewFacturas.Level = 0;
            this.DbTreeViewFacturas.Location = new System.Drawing.Point(12, 32);
            this.DbTreeViewFacturas.Name = "DbTreeViewFacturas";
            this.DbTreeViewFacturas.SelectedNode = null;
            this.DbTreeViewFacturas.ShowLines = true;
            this.DbTreeViewFacturas.ShowRootLines = true;
            this.DbTreeViewFacturas.Size = new System.Drawing.Size(157, 103);
            this.DbTreeViewFacturas.TabIndex = 5;
            this.DbTreeViewFacturas.Track = false;
            this.DbTreeViewFacturas.DoubleClick += new FSFormControls.DBTreeView.DoubleClickEventHandler(this.DbTreeView2_DoubleClick);
            // 
            // DbGroupBoxXP2
            // 
            this.DbGroupBoxXP2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbGroupBoxXP2.BackColor = System.Drawing.Color.Transparent;
            this.DbGroupBoxXP2.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.DbGroupBoxXP2.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.DbGroupBoxXP2.CaptionFormatFlag = FSFormControls.DBGroupBoxXP.FormatFlag.NoWrap;
            this.DbGroupBoxXP2.CaptionStyle = FSFormControls.DBGroupBoxXP.CaptionStyleEnum.Normal;
            this.DbGroupBoxXP2.CaptionText = "Información";
            this.DbGroupBoxXP2.CaptionTextAlign = FSFormControls.DBGroupBoxXP.CaptionTextAlignment.Left;
            this.DbGroupBoxXP2.ChevronStyle = FSFormControls.DBGroupBoxXP.ChevronStyleEnum.Image;
            this.DbGroupBoxXP2.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP2.CollapsedHighlightImage")));
            this.DbGroupBoxXP2.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP2.CollapsedImage")));
            this.DbGroupBoxXP2.Controls.Add(this.lblUltimaConexion);
            this.DbGroupBoxXP2.Controls.Add(this.lblUsuario);
            this.DbGroupBoxXP2.Controls.Add(this.Label2);
            this.DbGroupBoxXP2.Controls.Add(this.Label1);
            this.DbGroupBoxXP2.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP2.ExpandedHighlightImage")));
            this.DbGroupBoxXP2.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP2.ExpandedImage")));
            this.DbGroupBoxXP2.Location = new System.Drawing.Point(8, 164);
            this.DbGroupBoxXP2.Name = "DbGroupBoxXP2";
            this.DbGroupBoxXP2.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.DbGroupBoxXP2.Size = new System.Drawing.Size(184, 112);
            this.DbGroupBoxXP2.TabIndex = 1;
            this.DbGroupBoxXP2.Tag = 2;
            this.DbGroupBoxXP2.TooltipText = null;
            // 
            // lblUltimaConexion
            // 
            this.lblUltimaConexion.AutoSize = true;
            this.lblUltimaConexion.Location = new System.Drawing.Point(56, 80);
            this.lblUltimaConexion.Name = "lblUltimaConexion";
            this.lblUltimaConexion.Size = new System.Drawing.Size(0, 13);
            this.lblUltimaConexion.TabIndex = 3;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(72, 48);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 13);
            this.lblUsuario.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 64);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(86, 13);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Última Conexión:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 48);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(46, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Usuario:";
            // 
            // DbGroupBoxXP1
            // 
            this.DbGroupBoxXP1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbGroupBoxXP1.BackColor = System.Drawing.Color.Transparent;
            this.DbGroupBoxXP1.BorderStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.DbGroupBoxXP1.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.DbGroupBoxXP1.CaptionFormatFlag = FSFormControls.DBGroupBoxXP.FormatFlag.NoWrap;
            this.DbGroupBoxXP1.CaptionStyle = FSFormControls.DBGroupBoxXP.CaptionStyleEnum.Normal;
            this.DbGroupBoxXP1.CaptionText = "Clientes";
            this.DbGroupBoxXP1.CaptionTextAlign = FSFormControls.DBGroupBoxXP.CaptionTextAlignment.Left;
            this.DbGroupBoxXP1.ChevronStyle = FSFormControls.DBGroupBoxXP.ChevronStyleEnum.Image;
            this.DbGroupBoxXP1.CollapsedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP1.CollapsedHighlightImage")));
            this.DbGroupBoxXP1.CollapsedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP1.CollapsedImage")));
            this.DbGroupBoxXP1.Controls.Add(this.DbTreeViewClientes);
            this.DbGroupBoxXP1.ExpandedHighlightImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP1.ExpandedHighlightImage")));
            this.DbGroupBoxXP1.ExpandedImage = ((System.Drawing.Bitmap)(resources.GetObject("DbGroupBoxXP1.ExpandedImage")));
            this.DbGroupBoxXP1.Location = new System.Drawing.Point(8, 282);
            this.DbGroupBoxXP1.Name = "DbGroupBoxXP1";
            this.DbGroupBoxXP1.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.DbGroupBoxXP1.Size = new System.Drawing.Size(184, 193);
            this.DbGroupBoxXP1.TabIndex = 0;
            this.DbGroupBoxXP1.Tag = 3;
            this.DbGroupBoxXP1.TooltipText = null;
            // 
            // DbTreeViewClientes
            // 
            this.DbTreeViewClientes.About = null;
            this.DbTreeViewClientes.AllowLoadXML = false;
            this.DbTreeViewClientes.AllowSaveXML = true;
            this.DbTreeViewClientes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DbTreeViewClientes.DataControl = null;
            this.DbTreeViewClientes.EnableReArrange = false;
            this.DbTreeViewClientes.HideSelection = true;
            this.DbTreeViewClientes.HotTracking = false;
            this.DbTreeViewClientes.Level = 0;
            this.DbTreeViewClientes.Location = new System.Drawing.Point(12, 28);
            this.DbTreeViewClientes.Name = "DbTreeViewClientes";
            this.DbTreeViewClientes.SelectedNode = null;
            this.DbTreeViewClientes.ShowLines = true;
            this.DbTreeViewClientes.ShowRootLines = true;
            this.DbTreeViewClientes.Size = new System.Drawing.Size(157, 154);
            this.DbTreeViewClientes.TabIndex = 4;
            this.DbTreeViewClientes.Track = false;
            this.DbTreeViewClientes.DoubleClick += new FSFormControls.DBTreeView.DoubleClickEventHandler(this.DbTreeView1_DoubleClick);
            // 
            // DbControl2
            // 
            this.DbControl2.About = null;
            this.DbControl2.ArrayList = null;
            this.DbControl2.AutoConnect = true;
            this.DbControl2.DataControl = null;
            this.DbControl2.DataSet = null;
            this.DbControl2.DataTable = null;
            this.DbControl2.DataView = null;
            this.DbControl2.DBFieldData = "";
            this.DbControl2.DBPosition = 0;
            this.DbControl2.EraseDBControl = null;
            this.DbControl2.Filter = "";
            this.DbControl2.isEOF = true;
            this.DbControl2.Location = new System.Drawing.Point(500, 264);
            this.DbControl2.LOCK = null;
            this.DbControl2.LOPD = null;
            this.DbControl2.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl2.Name = "DbControl2";
            this.DbControl2.Page = 0;
            this.DbControl2.PageSettings = null;
            this.DbControl2.Paging = false;
            this.DbControl2.PagingSize = 0;
            this.DbControl2.ReadOnly = false;
            this.DbControl2.RelationDataControl = null;
            this.DbControl2.RelationDBField = "";
            this.DbControl2.RelationParentDBField = "";
            this.DbControl2.SaveError = false;
            this.DbControl2.SaveOnChangeRecord = false;
            this.DbControl2.Selection = "select * from contratos";
            this.DbControl2.Size = new System.Drawing.Size(88, 48);
            this.DbControl2.StoreInBase64Format = false;
            this.DbControl2.TabIndex = 5;
            this.DbControl2.TableName = "contratos";
            this.DbControl2.TabStop = false;
            this.DbControl2.Text = "SQL: select * from contratos";
            this.DbControl2.Track = false;
            this.DbControl2.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl2.Versionable = false;
            this.DbControl2.VersionableDateField = "";
            this.DbControl2.VersionableTable = "";
            this.DbControl2.VersionableUserField = "";
            this.DbControl2.VersionableVersionField = "";
            this.DbControl2.Visible = false;
            this.DbControl2.XmlFile = "";
            this.DbControl2.XMLName = "";
            // 
            // DbControl1
            // 
            this.DbControl1.About = null;
            this.DbControl1.ArrayList = null;
            this.DbControl1.AutoConnect = true;
            this.DbControl1.DataControl = null;
            this.DbControl1.DataSet = null;
            this.DbControl1.DataTable = null;
            this.DbControl1.DataView = null;
            this.DbControl1.DBFieldData = "";
            this.DbControl1.DBPosition = 0;
            this.DbControl1.EraseDBControl = null;
            this.DbControl1.Filter = "";
            this.DbControl1.isEOF = true;
            this.DbControl1.Location = new System.Drawing.Point(452, 120);
            this.DbControl1.LOCK = null;
            this.DbControl1.LOPD = null;
            this.DbControl1.Mode = FSFormControls.DBUserControlBase.AccessMode.ReadMode;
            this.DbControl1.Name = "DbControl1";
            this.DbControl1.Page = 0;
            this.DbControl1.PageSettings = null;
            this.DbControl1.Paging = false;
            this.DbControl1.PagingSize = 0;
            this.DbControl1.ReadOnly = false;
            this.DbControl1.RelationDataControl = null;
            this.DbControl1.RelationDBField = "";
            this.DbControl1.RelationParentDBField = "";
            this.DbControl1.SaveError = false;
            this.DbControl1.SaveOnChangeRecord = false;
            this.DbControl1.Selection = "select * from Clientes";
            this.DbControl1.Size = new System.Drawing.Size(56, 48);
            this.DbControl1.StoreInBase64Format = false;
            this.DbControl1.TabIndex = 4;
            this.DbControl1.TableName = "Clientes";
            this.DbControl1.TabStop = false;
            this.DbControl1.Text = "SQL: select * from Clientes";
            this.DbControl1.Track = false;
            this.DbControl1.TypeDB = FSFormControls.DBControl.DbType.SQLServer;
            this.DbControl1.Versionable = false;
            this.DbControl1.VersionableDateField = "";
            this.DbControl1.VersionableTable = "";
            this.DbControl1.VersionableUserField = "";
            this.DbControl1.VersionableVersionField = "";
            this.DbControl1.Visible = false;
            this.DbControl1.XmlFile = "";
            this.DbControl1.XMLName = "";
            // 
            // DbOfficeMenu1
            // 
            this.DbOfficeMenu1.ImageList = null;
            // 
            // mdiPrincipal
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1027, 248);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.DbGroupBoxXPList1);
            this.Controls.Add(this.DbControl2);
            this.Controls.Add(this.DbControl1);
            this.Controls.Add(this.ToolBar1);
            this.ForeColor = System.Drawing.Color.Black;
            this.IsMdiContainer = true;
            this.Menu = this.MainMenu1;
            this.Name = "mdiPrincipal";
            this.Text = "FSGestión - (c)2005 Febrer Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.mdiPrincipal_Closing);
            this.Load += new System.EventHandler(this.mdiPrincipal_Load);
            this.DbGroupBoxXPList1.ResumeLayout(false);
            this.dbGroupBoxXP4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbTreeViewDispositivos)).EndInit();
            this.DbGroupBoxXP3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DbTreeViewFacturas)).EndInit();
            this.DbGroupBoxXP2.ResumeLayout(false);
            this.DbGroupBoxXP2.PerformLayout();
            this.DbGroupBoxXP1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DbTreeViewClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		private void mdiPrincipal_Load(System.Object sender, System.EventArgs e)
		{
			this.Text = Global.appTitle;
			
			//FSLibrary.Xml d = new FSLibrary.Xml("Config\\config.xml");
			
			this.DbOfficeMenu1.Start(this);
			
			FSFormControls.Global.ProjectName = "FSGestion";
			
			Global.mdiP = this;

            //string connString = d.GetConfigValue("dbconn", "dns");

            //If connString = "" Then
            //    connString = Functions.CreateConnectionString

            //    d.SetConfigValue("dbconn", "dns", connString)
            //    d.Save()
            //End If

            //string connString = ConfigurationManager.ConnectionStrings["FSConnection"].ConnectionString;
            //connString = connString.Replace("{app_path}", Application.StartupPath);

            FSFormControls.Global.ConnectionStringSetting = ConfigurationManager.ConnectionStrings["FSConnection"];

            //Module1.dbconn.ConnectionString = connString;
            //Constants.DefaultEntry = "FSConnection";
            BdUtils db = new BdUtils(FSFormControls.Global.ConnectionStringSetting);

            
            FillTreeView();
			
			frmL = new frmLogin();
			frmL.frmDest = new frmClientes();
			frmL.Show();
		}
		
		private void FillTreeView()
		{
			//this.DbControl1.DBConnection = FSFormControls.Global.DBconnection;
			//this.DbControl2.DBConnection = FSFormControls.Global.DBconnection;
			
			this.DbTreeViewClientes.AddLevel(this.DbControl1, "nombre", "", "idCliente");
			this.DbTreeViewClientes.AddLevel(this.DbControl2, "fechacontrato", "idCliente", "idContrato");
		}
		
		
		private void MenuItem2_Click(System.Object sender, System.EventArgs e)
		{
            Close();
		}
		
		private void ToolBar1_ButtonClick(System.Object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            try
            {
                if ((string)e.Button.Tag == "SALIR")
                {
                    Close();
                    return;
                }

                if (Global.tipoUsuario == -1)
                {
                    if (FSFormControls.Global.Forms.Exist("login") == false)
                    {
                        frmL = new frmLogin();
                    }
                    else
                    {
                        frmL = (FSGestion.frmLogin)(FSFormControls.Global.Forms.Find("login"));
                    }
                    switch ((e.Button.Tag).ToString())
                    {
                        case "CLIENTES":
                            frmL.frmDest = new frmClientes();
                            break;
                        case "POTENCIALES":
                            frmL.frmDest = new frmPotenciales();
                            break;
                        case "IMPRESION":
                            break;
                        case "ARTICULOS":
                            frmL.frmDest = new frmArticulos();
                            break;
                        case "USUARIOS":
                            frmL.frmDest = new frmUsuarios();
                            break;
                        case "LOGIN":
                            frmL.frmDest = new frmClientes();
                            break;
                        case "FACTURACION":
                            frmL.frmDest = new frmFacturacion();
                            break;
                    }
                    frmL.Show();
                    return;
                }

                switch ((e.Button.Tag).ToString())
                {
                    case "CLIENTES":
                        frmClientes frmS_1 = default(frmClientes);
                        if (FSFormControls.Global.Forms.Exist("frmClientes") == false)
                        {
                            frmS_1 = new frmClientes();
                        }
                        else
                        {
                            frmS_1 = (FSGestion.frmClientes)(FSFormControls.Global.Forms.Find("frmClientes"));
                            frmS_1.Focus();
                        }
                        //frmS_1.DBConnection = FSFormControls.Global.ConnectionStringSetting;
                        frmS_1.Show();
                        break;
                    case "POTENCIALES":
                        frmPotenciales frmS_2 = default(frmPotenciales);
                        if (FSFormControls.Global.Forms.Exist("frmPotenciales") == false)
                        {
                            frmS_2 = new frmPotenciales();
                        }
                        else
                        {
                            frmS_2 = (FSGestion.frmPotenciales)(FSFormControls.Global.Forms.Find("frmPotenciales"));
                            frmS_2.Focus();
                        }
                        //frmS_2.DBConnection = FSFormControls.Global.DBconnection;
                        frmS_2.Show();
                        break;
                    case "LOGIN":
                        frmLogin frmS_3 = default(frmLogin);
                        if (FSFormControls.Global.Forms.Exist("login") == false)
                        {
                            frmS_3 = new frmLogin();
                        }
                        else
                        {
                            frmS_3 = (FSGestion.frmLogin)(FSFormControls.Global.Forms.Find("login"));
                            frmS_3.Focus();
                        }
                        //frmS_3.DBConnection = FSFormControls.Global.DBconnection;
                        frmS_3.Show();
                        break;
                    case "IMPRESION":
                        break;
                    case "ARTICULOS":
                        frmArticulos frmS_4 = default(frmArticulos);
                        if (FSFormControls.Global.Forms.Exist("frmArticulos") == false)
                        {
                            frmS_4 = new frmArticulos();
                        }
                        else
                        {
                            frmS_4 = (FSGestion.frmArticulos)(FSFormControls.Global.Forms.Find("frmArticulos"));
                            frmS_4.Focus();
                        }
                        //frmS_4.DBConnection = FSFormControls.Global.DBconnection;
                        frmS_4.Show();
                        break;
                    case "USUARIOS":
                        frmUsuarios frmS_5 = default(frmUsuarios);
                        if (FSFormControls.Global.Forms.Exist("frmUsuarios") == false)
                        {
                            frmS_5 = new frmUsuarios();
                        }
                        else
                        {
                            frmS_5 = (FSGestion.frmUsuarios)(FSFormControls.Global.Forms.Find("frmUsuarios"));
                            frmS_5.Focus();
                        }
                        //frmS_5.DBConnection = FSFormControls.Global.DBconnection;
                        frmS_5.Show();
                        break;
                    case "FACTURACION":
                        frmFacturacion frmS = default(frmFacturacion);
                        if (FSFormControls.Global.Forms.Exist("frmFacturacion") == false)
                        {
                            frmS = new frmFacturacion();
                        }
                        else
                        {
                            frmS = (FSGestion.frmFacturacion)(FSFormControls.Global.Forms.Find("frmFacturacion"));
                            frmS.Focus();
                        }
                        //frmS.DBConnection = FSFormControls.Global.DBconnection;
                        frmS.Show();
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void mdiPrincipal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !salir();
		}
		
		private bool salir()
		{
            if (MessageBox.Show("¿Deseas salir de la aplicación?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				return true;
			}
			return false;
		}
		
		private void DbTreeView1_DoubleClick(System.Object sender, System.EventArgs e)
		{
            try
            {
                if (Global.tipoUsuario == -1)
                {
                    if (FSFormControls.Global.Forms.Exist("login") == false)
                    {
                        frmL = new frmLogin();
                        frmL.frmDest = new frmClientes();
                        frmL.Show();
                    }
                    else
                    {
                        frmL = (FSGestion.frmLogin)(FSFormControls.Global.Forms.Find("login"));
                        frmL.Focus();
                    }
                    return;
                }

                switch (this.DbTreeViewClientes.SelectedDBNode.Level)
                {
                    case 0:
                        Global.MuestraCliente(Convert.ToString(this.DbTreeViewClientes.SelectedDBNode.Value));
                        break;
                    case 1:
                        Global.MuestraContrato(Convert.ToString(this.DbTreeViewClientes.SelectedDBNode.Value));
                        break;
                }
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void DbTreeView2_DoubleClick(System.Object sender, System.EventArgs e)
		{
            try
            {
                if (Global.tipoUsuario == -1)
                {
                    if (FSFormControls.Global.Forms.Exist("login") == false)
                    {
                        frmL = new frmLogin();
                        frmL.frmDest = new frmClientes();
                        frmL.Show();
                    }
                    else
                    {
                        frmL = (FSGestion.frmLogin)(FSFormControls.Global.Forms.Find("login"));
                        frmL.Focus();
                    }
                    return;
                }

                switch (this.DbTreeViewFacturas.SelectedDBNode.Level)
                {
                    case 0:
                        break;
                    //MuestraCliente(Me.DbTreeView1.SelectedDBNode.Value)
                    case 1:
                        Global.MuestraArticulos((string)(this.DbTreeViewFacturas.SelectedDBNode.Value));
                        break;
                }
            }
            catch(Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem4_Click(System.Object sender, System.EventArgs e)
		{
			this.DbTreeViewClientes.Clear();
			FillTreeView();
		}
		
		private void MenuItem7_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmEmpresas m = new frmEmpresas();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem5_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmPreferencias m = new frmPreferencias();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem8_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmCommDialog m = new frmCommDialog();
                m.Show();
            }
            catch(Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem9_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmEnvioSMS m = new frmEnvioSMS();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem10_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmHtmlParser m = new frmHtmlParser();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem3_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmSnapShot m = new frmSnapShot();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}
		
		private void MenuItem11_Click(System.Object sender, System.EventArgs e)
		{
            try
            {
                frmAsistente m = new frmAsistente();
                m.Show();
            }
            catch(Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
		}

        private void menuItem12_Click(object sender, EventArgs e)
        {
            try
            {
                frmCrypto m = new frmCrypto();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            try
            {
                frmGoogleMaps m = new frmGoogleMaps();
                m.Show();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            frmAplicacion app = new frmAplicacion();
            app.Show();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            frmGoogleFirebase firebase = new frmGoogleFirebase();
            firebase.Show();
        }
    }
	
}
