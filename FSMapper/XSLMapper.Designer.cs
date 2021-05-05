
namespace XML2XML
{
	partial class XSLMapper
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		private System.Windows.Forms.MenuItem mnuItmTargetXml;
		private System.Windows.Forms.ContextMenu cntxtMnuTargetTree;
		private System.Windows.Forms.MenuItem cmnuItemRemove;
		private System.Windows.Forms.MenuItem cmnuItemAdd;
		private System.Windows.Forms.MenuItem mnuItemRemoveAllMap;
		private System.Windows.Forms.MenuItem mnuItmSourceXml;
		private System.Windows.Forms.MainMenu mainMenuEditor;
		private System.Windows.Forms.MenuItem mnuItmFile;
		private System.Windows.Forms.MenuItem mnuItemSaveXSL;
		private System.Windows.Forms.MenuItem MenuItem2;
		private System.Windows.Forms.MenuItem mnuLoadMappg;
		private System.Windows.Forms.MenuItem mnuItemSaveMappg;
		private System.Windows.Forms.MenuItem MenuItem3;
		private System.Windows.Forms.MenuItem mnuItmExit;
		private System.Windows.Forms.OpenFileDialog OpenFileDlgXML;
		private System.Windows.Forms.SaveFileDialog SaveFileDlgXSL;
		private System.Windows.Forms.Splitter Splitter2;
		private System.Windows.Forms.Panel pnlMainBottom;
		private System.Windows.Forms.Panel pnlMainFill;
		private System.Windows.Forms.ImageList ImageList1;
		private System.Windows.Forms.MenuItem mnuItmOpen;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.TreeView tvTargetXML;
        private System.Windows.Forms.TreeView tvSourceXML;
        private System.Windows.Forms.RichTextBox rtxtSource;
        private System.Windows.Forms.RichTextBox rtxtTarget;
        private System.Windows.Forms.RichTextBox rtxtTrace;
        private System.Windows.Forms.RichTextBox rtxtXSL;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.TabControl tbcXSLEditor;
        private System.Windows.Forms.TabPage tbpOutput;
        private System.Windows.Forms.TabPage tbpXSL;
        private System.Windows.Forms.TabPage tbpXSLMapper;
        private System.Windows.Forms.Splitter Splitter1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuItem mnuItemCopyPath;
        private System.Windows.Forms.ContextMenu cntxtMnuSourceTree;
        private System.Windows.Forms.MenuItem mnuItemCopyPathSource;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton7;

		
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if ((components != null)) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSLMapper));
			this.mnuItmTargetXml = new System.Windows.Forms.MenuItem();
			this.cntxtMnuTargetTree = new System.Windows.Forms.ContextMenu();
			this.cmnuItemAdd = new System.Windows.Forms.MenuItem();
			this.cmnuItemRemove = new System.Windows.Forms.MenuItem();
			this.mnuItemRemoveAllMap = new System.Windows.Forms.MenuItem();
			this.mnuItemCopyPath = new System.Windows.Forms.MenuItem();
			this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
			this.mnuItmSourceXml = new System.Windows.Forms.MenuItem();
			this.mainMenuEditor = new System.Windows.Forms.MainMenu(this.components);
			this.mnuItmFile = new System.Windows.Forms.MenuItem();
			this.mnuItmOpen = new System.Windows.Forms.MenuItem();
			this.mnuLoadMappg = new System.Windows.Forms.MenuItem();
			this.mnuItemSaveXSL = new System.Windows.Forms.MenuItem();
			this.MenuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuItemSaveMappg = new System.Windows.Forms.MenuItem();
			this.MenuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuItmExit = new System.Windows.Forms.MenuItem();
			this.OpenFileDlgXML = new System.Windows.Forms.OpenFileDialog();
			this.SaveFileDlgXSL = new System.Windows.Forms.SaveFileDialog();
			this.pnlMainBottom = new System.Windows.Forms.Panel();
			this.rtxtTrace = new System.Windows.Forms.RichTextBox();
			this.Splitter2 = new System.Windows.Forms.Splitter();
			this.pnlMainFill = new System.Windows.Forms.Panel();
			this.tbcXSLEditor = new System.Windows.Forms.TabControl();
			this.tbpXSLMapper = new System.Windows.Forms.TabPage();
			this.Splitter1 = new System.Windows.Forms.Splitter();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tvTargetXML = new System.Windows.Forms.TreeView();
			this.rtxtTarget = new System.Windows.Forms.RichTextBox();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tvSourceXML = new System.Windows.Forms.TreeView();
			this.cntxtMnuSourceTree = new System.Windows.Forms.ContextMenu();
			this.mnuItemCopyPathSource = new System.Windows.Forms.MenuItem();
			this.rtxtSource = new System.Windows.Forms.RichTextBox();
			this.tbpXSL = new System.Windows.Forms.TabPage();
			this.rtxtXSL = new System.Windows.Forms.RichTextBox();
			this.tbpOutput = new System.Windows.Forms.TabPage();
			this.rtxtOutput = new System.Windows.Forms.RichTextBox();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
			this.pnlMainBottom.SuspendLayout();
			this.pnlMainFill.SuspendLayout();
			this.tbcXSLEditor.SuspendLayout();
			this.tbpXSLMapper.SuspendLayout();
			this.pnlRight.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tbpXSL.SuspendLayout();
			this.tbpOutput.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuItmTargetXml
			// 
			this.mnuItmTargetXml.Index = 1;
			this.mnuItmTargetXml.Text = "Des&tino XML...";
			// 
			// cntxtMnuTargetTree
			// 
			this.cntxtMnuTargetTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.cmnuItemAdd,
			this.cmnuItemRemove,
			this.mnuItemRemoveAllMap,
			this.mnuItemCopyPath});
			// 
			// cmnuItemAdd
			// 
			this.cmnuItemAdd.Index = 0;
			this.cmnuItemAdd.Text = "Añadir Mapeo";
			// 
			// cmnuItemRemove
			// 
			this.cmnuItemRemove.Index = 1;
			this.cmnuItemRemove.Text = "Quitar Mapeo";
			// 
			// mnuItemRemoveAllMap
			// 
			this.mnuItemRemoveAllMap.Index = 2;
			this.mnuItemRemoveAllMap.Text = "Quitar todos los mapeos";
			// 
			// mnuItemCopyPath
			// 
			this.mnuItemCopyPath.Index = 3;
			this.mnuItemCopyPath.Text = "Copiar ruta";
			// 
			// ImageList1
			// 
			this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
			this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.ImageList1.Images.SetKeyName(0, "");
			this.ImageList1.Images.SetKeyName(1, "");
			this.ImageList1.Images.SetKeyName(2, "");
			this.ImageList1.Images.SetKeyName(3, "");
			this.ImageList1.Images.SetKeyName(4, "");
			// 
			// mnuItmSourceXml
			// 
			this.mnuItmSourceXml.Index = 0;
			this.mnuItmSourceXml.Text = "&Origen XML...";
			// 
			// mainMenuEditor
			// 
			this.mainMenuEditor.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.mnuItmFile});
			// 
			// mnuItmFile
			// 
			this.mnuItmFile.Index = 0;
			this.mnuItmFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.mnuItmOpen,
			this.mnuItemSaveXSL,
			this.MenuItem2,
			this.mnuItemSaveMappg,
			this.MenuItem3,
			this.mnuItmExit});
			this.mnuItmFile.Text = "&Archivo";
			// 
			// mnuItmOpen
			// 
			this.mnuItmOpen.Index = 0;
			this.mnuItmOpen.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.mnuItmSourceXml,
			this.mnuItmTargetXml,
			this.mnuLoadMappg});
			this.mnuItmOpen.Text = "&Abrir";
			// 
			// mnuLoadMappg
			// 
			this.mnuLoadMappg.Index = 2;
			this.mnuLoadMappg.Text = "Fichero &Mapeo";
			// 
			// mnuItemSaveXSL
			// 
			this.mnuItemSaveXSL.Index = 1;
			this.mnuItemSaveXSL.Text = "&Guardar XSL";
			// 
			// MenuItem2
			// 
			this.MenuItem2.Index = 2;
			this.MenuItem2.Text = "-";
			// 
			// mnuItemSaveMappg
			// 
			this.mnuItemSaveMappg.Index = 3;
			this.mnuItemSaveMappg.Text = "Guardar Ma&peo";
			// 
			// MenuItem3
			// 
			this.MenuItem3.Index = 4;
			this.MenuItem3.Text = "-";
			// 
			// mnuItmExit
			// 
			this.mnuItmExit.Index = 5;
			this.mnuItmExit.Text = "Sali&r";
			// 
			// OpenFileDlgXML
			// 
			this.OpenFileDlgXML.Filter = "Ficheros XML|*.xml";
			// 
			// SaveFileDlgXSL
			// 
			this.SaveFileDlgXSL.Filter = "Ficheros XSL|*.xsl";
			// 
			// pnlMainBottom
			// 
			this.pnlMainBottom.Controls.Add(this.rtxtTrace);
			this.pnlMainBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlMainBottom.Location = new System.Drawing.Point(0, 637);
			this.pnlMainBottom.Name = "pnlMainBottom";
			this.pnlMainBottom.Size = new System.Drawing.Size(920, 104);
			this.pnlMainBottom.TabIndex = 5;
			// 
			// rtxtTrace
			// 
			this.rtxtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtTrace.Location = new System.Drawing.Point(0, 0);
			this.rtxtTrace.Name = "rtxtTrace";
			this.rtxtTrace.Size = new System.Drawing.Size(920, 104);
			this.rtxtTrace.TabIndex = 0;
			this.rtxtTrace.Text = "";
			// 
			// Splitter2
			// 
			this.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Splitter2.Location = new System.Drawing.Point(0, 634);
			this.Splitter2.Name = "Splitter2";
			this.Splitter2.Size = new System.Drawing.Size(920, 3);
			this.Splitter2.TabIndex = 6;
			this.Splitter2.TabStop = false;
			// 
			// pnlMainFill
			// 
			this.pnlMainFill.Controls.Add(this.tbcXSLEditor);
			this.pnlMainFill.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMainFill.Location = new System.Drawing.Point(0, 42);
			this.pnlMainFill.Name = "pnlMainFill";
			this.pnlMainFill.Size = new System.Drawing.Size(920, 592);
			this.pnlMainFill.TabIndex = 7;
			// 
			// tbcXSLEditor
			// 
			this.tbcXSLEditor.Controls.Add(this.tbpXSLMapper);
			this.tbcXSLEditor.Controls.Add(this.tbpXSL);
			this.tbcXSLEditor.Controls.Add(this.tbpOutput);
			this.tbcXSLEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbcXSLEditor.Location = new System.Drawing.Point(0, 0);
			this.tbcXSLEditor.Name = "tbcXSLEditor";
			this.tbcXSLEditor.SelectedIndex = 0;
			this.tbcXSLEditor.Size = new System.Drawing.Size(920, 592);
			this.tbcXSLEditor.TabIndex = 3;
			// 
			// tbpXSLMapper
			// 
			this.tbpXSLMapper.BackColor = System.Drawing.Color.SlateGray;
			this.tbpXSLMapper.Controls.Add(this.Splitter1);
			this.tbpXSLMapper.Controls.Add(this.pnlRight);
			this.tbpXSLMapper.Controls.Add(this.pnlLeft);
			this.tbpXSLMapper.Location = new System.Drawing.Point(4, 22);
			this.tbpXSLMapper.Name = "tbpXSLMapper";
			this.tbpXSLMapper.Size = new System.Drawing.Size(912, 566);
			this.tbpXSLMapper.TabIndex = 0;
			this.tbpXSLMapper.Text = "Mapeo";
			// 
			// Splitter1
			// 
			this.Splitter1.Location = new System.Drawing.Point(296, 0);
			this.Splitter1.Name = "Splitter1";
			this.Splitter1.Size = new System.Drawing.Size(3, 566);
			this.Splitter1.TabIndex = 6;
			this.Splitter1.TabStop = false;
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.splitContainer1);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRight.Location = new System.Drawing.Point(296, 0);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(616, 566);
			this.pnlRight.TabIndex = 5;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer1.Size = new System.Drawing.Size(616, 566);
			this.splitContainer1.SplitterDistance = 205;
			this.splitContainer1.TabIndex = 6;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tvTargetXML);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.rtxtTarget);
			this.splitContainer3.Size = new System.Drawing.Size(205, 566);
			this.splitContainer3.SplitterDistance = 399;
			this.splitContainer3.TabIndex = 6;
			// 
			// tvTargetXML
			// 
			this.tvTargetXML.ContextMenu = this.cntxtMnuTargetTree;
			this.tvTargetXML.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvTargetXML.ImageIndex = 0;
			this.tvTargetXML.ImageList = this.ImageList1;
			this.tvTargetXML.Location = new System.Drawing.Point(0, 0);
			this.tvTargetXML.Name = "tvTargetXML";
			this.tvTargetXML.SelectedImageIndex = 0;
			this.tvTargetXML.ShowNodeToolTips = true;
			this.tvTargetXML.Size = new System.Drawing.Size(205, 399);
			this.tvTargetXML.TabIndex = 6;
			// 
			// rtxtTarget
			// 
			this.rtxtTarget.AcceptsTab = true;
			this.rtxtTarget.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtTarget.Location = new System.Drawing.Point(0, 0);
			this.rtxtTarget.Name = "rtxtTarget";
			this.rtxtTarget.Size = new System.Drawing.Size(205, 163);
			this.rtxtTarget.TabIndex = 2;
			this.rtxtTarget.Text = "";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(407, 566);
			this.propertyGrid1.TabIndex = 6;
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.splitContainer2);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(296, 566);
			this.pnlLeft.TabIndex = 4;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tvSourceXML);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.rtxtSource);
			this.splitContainer2.Size = new System.Drawing.Size(296, 566);
			this.splitContainer2.SplitterDistance = 399;
			this.splitContainer2.TabIndex = 3;
			// 
			// tvSourceXML
			// 
			this.tvSourceXML.ContextMenu = this.cntxtMnuSourceTree;
			this.tvSourceXML.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvSourceXML.ImageIndex = 0;
			this.tvSourceXML.ImageList = this.ImageList1;
			this.tvSourceXML.Location = new System.Drawing.Point(0, 0);
			this.tvSourceXML.Name = "tvSourceXML";
			this.tvSourceXML.SelectedImageIndex = 0;
			this.tvSourceXML.ShowNodeToolTips = true;
			this.tvSourceXML.Size = new System.Drawing.Size(296, 399);
			this.tvSourceXML.TabIndex = 3;
			// 
			// cntxtMnuSourceTree
			// 
			this.cntxtMnuSourceTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.mnuItemCopyPathSource});
			// 
			// mnuItemCopyPathSource
			// 
			this.mnuItemCopyPathSource.Index = 0;
			this.mnuItemCopyPathSource.Text = "Copiar ruta";
			// 
			// rtxtSource
			// 
			this.rtxtSource.AcceptsTab = true;
			this.rtxtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtSource.Location = new System.Drawing.Point(0, 0);
			this.rtxtSource.Name = "rtxtSource";
			this.rtxtSource.Size = new System.Drawing.Size(296, 163);
			this.rtxtSource.TabIndex = 2;
			this.rtxtSource.Text = "";
			// 
			// tbpXSL
			// 
			this.tbpXSL.Controls.Add(this.rtxtXSL);
			this.tbpXSL.Location = new System.Drawing.Point(4, 22);
			this.tbpXSL.Name = "tbpXSL";
			this.tbpXSL.Size = new System.Drawing.Size(912, 566);
			this.tbpXSL.TabIndex = 1;
			this.tbpXSL.Text = "XSL";
			this.tbpXSL.Visible = false;
			// 
			// rtxtXSL
			// 
			this.rtxtXSL.AcceptsTab = true;
			this.rtxtXSL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtXSL.Location = new System.Drawing.Point(0, 0);
			this.rtxtXSL.Name = "rtxtXSL";
			this.rtxtXSL.Size = new System.Drawing.Size(912, 566);
			this.rtxtXSL.TabIndex = 0;
			this.rtxtXSL.Text = "";
			// 
			// tbpOutput
			// 
			this.tbpOutput.Controls.Add(this.rtxtOutput);
			this.tbpOutput.Location = new System.Drawing.Point(4, 22);
			this.tbpOutput.Name = "tbpOutput";
			this.tbpOutput.Size = new System.Drawing.Size(912, 566);
			this.tbpOutput.TabIndex = 2;
			this.tbpOutput.Text = "Salida";
			this.tbpOutput.Visible = false;
			// 
			// rtxtOutput
			// 
			this.rtxtOutput.AcceptsTab = true;
			this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtOutput.Location = new System.Drawing.Point(0, 0);
			this.rtxtOutput.Name = "rtxtOutput";
			this.rtxtOutput.Size = new System.Drawing.Size(912, 566);
			this.rtxtOutput.TabIndex = 0;
			this.rtxtOutput.Text = "";
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.toolStrip1);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(920, 42);
			this.pnlTop.TabIndex = 8;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripButton5,
			this.toolStripButton1,
			this.toolStripButton2,
			this.toolStripSeparator2,
			this.toolStripButton3,
			this.toolStripSeparator3,
			this.toolStripButton4,
			this.toolStripButton6,
			this.toolStripSeparator1,
			this.toolStripButton7});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(920, 42);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton5.Text = "Cargar fichero de mapeo";
			this.toolStripButton5.Click += new System.EventHandler(this.mnuLoadMappg_Click);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.ToolTipText = "XML Origen";
			this.toolStripButton1.Click += new System.EventHandler(this.mnuItmSourceXml_Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton2.Text = "toolStripButton2";
			this.toolStripButton2.ToolTipText = "XML Destino";
			this.toolStripButton2.Click += new System.EventHandler(this.mnuItmTargetXml_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton3.Text = "toolStripButton3";
			this.toolStripButton3.ToolTipText = "Borrar mapeos";
			this.toolStripButton3.Click += new System.EventHandler(this.mnuItemRemoveAllMap_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton4.Text = "Guardar fichero de mapeo";
			this.toolStripButton4.Click += new System.EventHandler(this.mnuItemSaveMappg_Click);
			// 
			// toolStripButton6
			// 
			this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
			this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton6.Text = "Guardar XLS";
			this.toolStripButton6.Click += new System.EventHandler(this.mnuItemSaveXSL_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
			// 
			// toolStripButton7
			// 
			this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
			this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Size = new System.Drawing.Size(36, 39);
			this.toolStripButton7.Text = "toolStripButton7";
			this.toolStripButton7.ToolTipText = "Salir";
			this.toolStripButton7.Click += new System.EventHandler(this.mnuItmExit_Click);
			// 
			// XSLMapper
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(920, 741);
			this.Controls.Add(this.pnlMainFill);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.Splitter2);
			this.Controls.Add(this.pnlMainBottom);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenuEditor;
			this.Name = "XSLMapper";
			this.Text = "XML2XML";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.pnlMainBottom.ResumeLayout(false);
			this.pnlMainFill.ResumeLayout(false);
			this.tbcXSLEditor.ResumeLayout(false);
			this.tbpXSLMapper.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.tbpXSL.ResumeLayout(false);
			this.tbpOutput.ResumeLayout(false);
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}
    }
}