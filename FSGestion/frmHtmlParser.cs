
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;
using FSFormControls;


//
// This is an example application to demonstrate the usage of the MIL.Html library.
//
// The only sub of real relevance is the ProcessHTML sub. However, you may be interested
// in the reading the code to see how to iterate through the DOM tree.
//
// If decide to have a look at the Microsoft homepage's HTML, you will see loads of "text"
// items that appear empty. This is due to carriage returns being treated as text items.


namespace FSGestion
{
	public class frmHtmlParser : System.Windows.Forms.Form
	{
		FSFormControls.DBHtmlDocument mDocument;
		
#region  Windows Form Designer generated code
		
		public frmHtmlParser()
		{
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			
		}
		
		//Form overrides dispose to clean up the component list.
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

        //Required by the Windows Form Designer
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		internal System.Windows.Forms.TreeView tvwDOM;
		internal System.Windows.Forms.PictureBox objSlider;
		internal System.Windows.Forms.MainMenu MainMenu1;
		internal System.Windows.Forms.MenuItem mnuFile;
		internal System.Windows.Forms.MenuItem mnuOpenFile;
		internal System.Windows.Forms.MenuItem mnuExit;
		internal System.Windows.Forms.OpenFileDialog OpenHtmlFileDialog;
		internal System.Windows.Forms.ImageList ImageList1;
		internal System.Windows.Forms.ContextMenu TreeNodeMenu;
		internal System.Windows.Forms.MenuItem mnuViewHTML;
		internal System.Windows.Forms.MenuItem mnuViewXHTML;
		internal System.Windows.Forms.Panel pnlBottom;
		internal System.Windows.Forms.TextBox txtHTML;
		internal System.Windows.Forms.PictureBox objSliderBottom;
		internal System.Windows.Forms.PropertyGrid grdProperties;
		internal System.Windows.Forms.MenuItem mnuFileSaveAs;
		internal System.Windows.Forms.SaveFileDialog SaveHtmlFileDialog;
		internal System.Windows.Forms.MenuItem MenuItem1;
		internal System.Windows.Forms.MenuItem MenuItem2;
		internal System.Windows.Forms.MenuItem mnuAbrirWeb;
		internal System.Windows.Forms.MenuItem mnuBuscarNombre;
		internal System.Windows.Forms.MenuItem mnuBuscarAtributo;
		internal System.Windows.Forms.MenuItem mnuBuscarAtributoValor;
		internal System.Windows.Forms.MenuItem mnuBuscarText;
		internal System.Windows.Forms.MenuItem mnuGoogle;
		internal System.Windows.Forms.MenuItem mnuPaginasAmarillas;
		internal System.Windows.Forms.MenuItem mnuAyuntamientos;
		internal System.Windows.Forms.MenuItem mnuExtraerEmails;
		internal System.Windows.Forms.MenuItem mnuWebBrowser;
		internal System.Windows.Forms.MenuItem mnuPeliculas;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tvwDOM = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.objSlider = new System.Windows.Forms.PictureBox();
            this.MainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuOpenFile = new System.Windows.Forms.MenuItem();
            this.mnuAbrirWeb = new System.Windows.Forms.MenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
            this.mnuPeliculas = new System.Windows.Forms.MenuItem();
            this.mnuGoogle = new System.Windows.Forms.MenuItem();
            this.mnuPaginasAmarillas = new System.Windows.Forms.MenuItem();
            this.mnuAyuntamientos = new System.Windows.Forms.MenuItem();
            this.mnuExtraerEmails = new System.Windows.Forms.MenuItem();
            this.mnuWebBrowser = new System.Windows.Forms.MenuItem();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.MenuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuBuscarNombre = new System.Windows.Forms.MenuItem();
            this.mnuBuscarAtributo = new System.Windows.Forms.MenuItem();
            this.mnuBuscarAtributoValor = new System.Windows.Forms.MenuItem();
            this.mnuBuscarText = new System.Windows.Forms.MenuItem();
            this.OpenHtmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TreeNodeMenu = new System.Windows.Forms.ContextMenu();
            this.mnuViewHTML = new System.Windows.Forms.MenuItem();
            this.mnuViewXHTML = new System.Windows.Forms.MenuItem();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.grdProperties = new System.Windows.Forms.PropertyGrid();
            this.objSliderBottom = new System.Windows.Forms.PictureBox();
            this.txtHTML = new System.Windows.Forms.TextBox();
            this.SaveHtmlFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.objSlider)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objSliderBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // tvwDOM
            // 
            this.tvwDOM.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvwDOM.HideSelection = false;
            this.tvwDOM.ImageIndex = 0;
            this.tvwDOM.ImageList = this.ImageList1;
            this.tvwDOM.Location = new System.Drawing.Point(0, 0);
            this.tvwDOM.Name = "tvwDOM";
            this.tvwDOM.SelectedImageIndex = 0;
            this.tvwDOM.Size = new System.Drawing.Size(552, 192);
            this.tvwDOM.TabIndex = 0;
            this.tvwDOM.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwDOM_AfterSelect);
            this.tvwDOM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvwDOM_MouseDown);
            // 
            // ImageList1
            // 
            this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // objSlider
            // 
            this.objSlider.BackColor = System.Drawing.SystemColors.Control;
            this.objSlider.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.objSlider.Dock = System.Windows.Forms.DockStyle.Top;
            this.objSlider.Location = new System.Drawing.Point(0, 192);
            this.objSlider.Name = "objSlider";
            this.objSlider.Size = new System.Drawing.Size(552, 8);
            this.objSlider.TabIndex = 1;
            this.objSlider.TabStop = false;
            this.objSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.objSlider_MouseMove);
            // 
            // MainMenu1
            // 
            this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.MenuItem2});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpenFile,
            this.mnuAbrirWeb,
            this.mnuFileSaveAs,
            this.mnuPeliculas,
            this.mnuGoogle,
            this.mnuPaginasAmarillas,
            this.mnuAyuntamientos,
            this.mnuExtraerEmails,
            this.mnuWebBrowser,
            this.MenuItem1,
            this.mnuExit});
            this.mnuFile.Text = "&File";
            // 
            // mnuOpenFile
            // 
            this.mnuOpenFile.Index = 0;
            this.mnuOpenFile.Text = "&Abrir fichero HTML...";
            this.mnuOpenFile.Click += new System.EventHandler(this.mnuOpenFile_Click);
            // 
            // mnuAbrirWeb
            // 
            this.mnuAbrirWeb.Index = 1;
            this.mnuAbrirWeb.Text = "Abrir &web...";
            this.mnuAbrirWeb.Click += new System.EventHandler(this.MenuItem3_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Index = 2;
            this.mnuFileSaveAs.Text = "&Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // mnuPeliculas
            // 
            this.mnuPeliculas.Index = 3;
            this.mnuPeliculas.Text = "Peliculas";
            this.mnuPeliculas.Click += new System.EventHandler(this.MenuItem8_Click);
            // 
            // mnuGoogle
            // 
            this.mnuGoogle.Index = 4;
            this.mnuGoogle.Text = "Google";
            this.mnuGoogle.Click += new System.EventHandler(this.MenuItem9_Click);
            // 
            // mnuPaginasAmarillas
            // 
            this.mnuPaginasAmarillas.Index = 5;
            this.mnuPaginasAmarillas.Text = "Paginas Amarillas";
            this.mnuPaginasAmarillas.Click += new System.EventHandler(this.MenuItem10_Click);
            // 
            // mnuAyuntamientos
            // 
            this.mnuAyuntamientos.Index = 6;
            this.mnuAyuntamientos.Text = "Ayuntamientos";
            this.mnuAyuntamientos.Click += new System.EventHandler(this.MenuItem11_Click);
            // 
            // mnuExtraerEmails
            // 
            this.mnuExtraerEmails.Index = 7;
            this.mnuExtraerEmails.Text = "Extraer Emails";
            this.mnuExtraerEmails.Click += new System.EventHandler(this.MenuItem12_Click);
            // 
            // mnuWebBrowser
            // 
            this.mnuWebBrowser.Index = 8;
            this.mnuWebBrowser.Text = "Otro Ejemplo de navegador - DBWebBrowser";
            this.mnuWebBrowser.Click += new System.EventHandler(this.MenuItem13_Click);
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 9;
            this.MenuItem1.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 10;
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // MenuItem2
            // 
            this.MenuItem2.Index = 1;
            this.MenuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuBuscarNombre,
            this.mnuBuscarAtributo,
            this.mnuBuscarAtributoValor,
            this.mnuBuscarText});
            this.MenuItem2.Text = "Buscar";
            // 
            // mnuBuscarNombre
            // 
            this.mnuBuscarNombre.Index = 0;
            this.mnuBuscarNombre.Text = "Buscar por nombre";
            this.mnuBuscarNombre.Click += new System.EventHandler(this.MenuItem4_Click);
            // 
            // mnuBuscarAtributo
            // 
            this.mnuBuscarAtributo.Index = 1;
            this.mnuBuscarAtributo.Text = "Buscar por atributo";
            this.mnuBuscarAtributo.Click += new System.EventHandler(this.MenuItem5_Click);
            // 
            // mnuBuscarAtributoValor
            // 
            this.mnuBuscarAtributoValor.Index = 2;
            this.mnuBuscarAtributoValor.Text = "Buscar por atributo y valor";
            this.mnuBuscarAtributoValor.Click += new System.EventHandler(this.MenuItem6_Click);
            // 
            // mnuBuscarText
            // 
            this.mnuBuscarText.Index = 3;
            this.mnuBuscarText.Text = "Buscar por texto";
            this.mnuBuscarText.Click += new System.EventHandler(this.MenuItem7_Click);
            // 
            // OpenHtmlFileDialog
            // 
            this.OpenHtmlFileDialog.Filter = "HTML Files|*.html;*.htm";
            // 
            // TreeNodeMenu
            // 
            this.TreeNodeMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuViewHTML,
            this.mnuViewXHTML});
            // 
            // mnuViewHTML
            // 
            this.mnuViewHTML.Index = 0;
            this.mnuViewHTML.Text = "View HTML";
            this.mnuViewHTML.Click += new System.EventHandler(this.mnuViewHTML_Click);
            // 
            // mnuViewXHTML
            // 
            this.mnuViewXHTML.Index = 1;
            this.mnuViewXHTML.Text = "View XHTML";
            this.mnuViewXHTML.Click += new System.EventHandler(this.mnuViewXHTML_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.grdProperties);
            this.pnlBottom.Controls.Add(this.objSliderBottom);
            this.pnlBottom.Controls.Add(this.txtHTML);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 200);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(552, 129);
            this.pnlBottom.TabIndex = 2;
            // 
            // grdProperties
            // 
            this.grdProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grdProperties.Location = new System.Drawing.Point(248, 0);
            this.grdProperties.Name = "grdProperties";
            this.grdProperties.Size = new System.Drawing.Size(304, 129);
            this.grdProperties.TabIndex = 2;
            this.grdProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grdProperties_PropertyValueChanged);
            // 
            // objSliderBottom
            // 
            this.objSliderBottom.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.objSliderBottom.Dock = System.Windows.Forms.DockStyle.Left;
            this.objSliderBottom.Location = new System.Drawing.Point(240, 0);
            this.objSliderBottom.Name = "objSliderBottom";
            this.objSliderBottom.Size = new System.Drawing.Size(8, 129);
            this.objSliderBottom.TabIndex = 1;
            this.objSliderBottom.TabStop = false;
            this.objSliderBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.objSliderBottom_MouseMove);
            // 
            // txtHTML
            // 
            this.txtHTML.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtHTML.Location = new System.Drawing.Point(0, 0);
            this.txtHTML.Multiline = true;
            this.txtHTML.Name = "txtHTML";
            this.txtHTML.ReadOnly = true;
            this.txtHTML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHTML.Size = new System.Drawing.Size(240, 129);
            this.txtHTML.TabIndex = 0;
            // 
            // SaveHtmlFileDialog
            // 
            this.SaveHtmlFileDialog.FileName = "doc1";
            this.SaveHtmlFileDialog.Filter = "HTML Files|*.html;*.htm|XHTML Files|*.xml";
            // 
            // frmHtmlParser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(552, 329);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.objSlider);
            this.Controls.Add(this.tvwDOM);
            this.Menu = this.MainMenu1;
            this.Name = "frmHtmlParser";
            this.Text = "Test Form";
            ((System.ComponentModel.ISupportInitialize)(this.objSlider)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objSliderBottom)).EndInit();
            this.ResumeLayout(false);

		}
		
#endregion
		
		// ProcessHTML(html)
		//
		// This will process the given HTML string, and will populate the treeview. This
		// method is called after the user has clicked the "Open HTML file..." menu item.
		
		private void ProcessHTML(string html)
		{
			
			// Clear the treeview
			
			tvwDOM.Nodes.Clear();
			
			// Create an DBHtmlDocument (which parses the html)
			
			mDocument = FSFormControls.DBHtmlDocument.Create(html, false);
			
			// Populate the treeview with the document nodes
			
			BuildTree(mDocument.Nodes, tvwDOM.Nodes);
			
		}
		
		// BuildTree(nodes,treeNodes)
		//
		// This is used by the ProcessHTML method to iterate through the DBHtmlNodes and populate
		// the treeview
		
		private void BuildTree(FSFormControls.DBHtmlNodeCollection nodes, TreeNodeCollection treeNodes)
		{
			string value = "";
			
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			foreach (FSFormControls.DBHtmlNode tempLoopVar_node in nodes)
			{
				node = tempLoopVar_node;
				TreeNode treeNode = new TreeNode(node.ToString());
				treeNode.Tag = node; // Keep the DBHtmlNode object in the tag (for when the user clicks on it)
				treeNodes.Add(treeNode);
				if ((node) is FSFormControls.DBHtmlElement)
				{
					treeNode.SelectedImageIndex = 0;
					treeNode.ImageIndex = 0;
					this.BuildTree(((FSFormControls.DBHtmlElement) node).Nodes, treeNode.Nodes);
				}
				else
				{
					treeNode.Text = "(text)"; // This probably has carriage returns in, so don't render the actual HTML here
					treeNode.SelectedImageIndex = 1;
					treeNode.ImageIndex = 1;
				}
			}
		}
		
		// objSlider_MouseMove(sender,e)
		//
		// This is so the user can resize the top and bottom sections
		
		private void objSlider_MouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (System.Convert.ToBoolean(e.Button & System.Windows.Forms.MouseButtons.Left))
			{
				tvwDOM.Height = e.Y + objSlider.Top;
			}
		}
		
		// objSliderBottom_MouseMove(sender,e)
		//
		// This is so the user can resize the left and right controls in the bottom section
		
		private void objSliderBottom_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (System.Convert.ToBoolean(e.Button & System.Windows.Forms.MouseButtons.Left))
			{
				txtHTML.Width = txtHTML.Width + e.X; //+ objSlider.Left
			}
		}
		
		// mnuExit_Click(sender,e)
		//
		// Quit this application
		
		private void mnuExit_Click(System.Object sender, System.EventArgs e)
		{
            Close();
		}
		
		// mnuOpenFile_Click(sender,e)
		//
		// This will open the file dialog and load the required HTML file
		
		private void mnuOpenFile_Click(System.Object sender, System.EventArgs e)
		{
			if (OpenHtmlFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				//Try
				System.IO.StreamReader sr = System.IO.File.OpenText(OpenHtmlFileDialog.FileName);
				string html = sr.ReadToEnd();
				sr.Close();
				ProcessHTML(html);
				//Catch ex As Exception
				//    MsgBox(ex.Message)
				//    MsgBox("Sorry, I couldn't open that file for some reason.. try another one!")
				//End Try
			}
		}
		
		// tvwDOM_AfterSelect(sender,e)
		//
		// This is called whenever the user selects a different node on the tree
		
		private void tvwDOM_AfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (tvwDOM.SelectedNode == null)
			{
				return;
			}
			
			// I'm just converting the DBHtmlNode to a string here, but you could easily
			// use Typeof to determine its type, and then cast it to either DBHtmlText or
			// DBHtmlElement. That way, you could present each of the attributes in a
			// properties control, for example.
			
			FSFormControls.DBHtmlNode node = (FSFormControls.DBHtmlNode) tvwDOM.SelectedNode.Tag;
			txtHTML.Text = node.HTML;
			grdProperties.SelectedObject = node;
			grdProperties.Text = node.GetType().ToString();
		}
		
		private void mnuViewHTML_Click(System.Object sender, System.EventArgs e)
		{
			if (tvwDOM.SelectedNode == null)
			{
				return;
			}
			FSFormControls.DBHtmlNode node = (FSFormControls.DBHtmlNode) tvwDOM.SelectedNode.Tag;
			frmViewForm.ShowText(node.HTML);
		}
		
		private void mnuViewXHTML_Click(System.Object sender, System.EventArgs e)
		{
			if (tvwDOM.SelectedNode == null)
			{
				return;
			}
			FSFormControls.DBHtmlNode node = (FSFormControls.DBHtmlNode) tvwDOM.SelectedNode.Tag;
			frmViewForm.ShowText(node.XHTML);
		}
		
		private void tvwDOM_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			TreeNode node = tvwDOM.GetNodeAt(new Point(e.X, e.Y));
			if (node == null)
			{
				return;
			}
			tvwDOM.SelectedNode = node;
			if (System.Convert.ToBoolean(e.Button & System.Windows.Forms.MouseButtons.Right))
			{
				tvwDOM.ContextMenu = TreeNodeMenu;
			}
			else
			{
				tvwDOM.ContextMenu = null;
			}
		}
		
		private void grdProperties_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			FSFormControls.DBHtmlNode node = default(FSFormControls.DBHtmlNode);
			node = (FSFormControls.DBHtmlNode) grdProperties.SelectedObject;
			if (node == null)
			{
				return;
			}
			if (tvwDOM.SelectedNode == null)
			{
				return;
			}
			tvwDOM.SelectedNode.Nodes.Clear();
			if (node is FSFormControls.DBHtmlElement)
			{
				tvwDOM.SelectedNode.Text = node.ToString();
				BuildTree(((FSFormControls.DBHtmlElement) node).Nodes, tvwDOM.SelectedNode.Nodes);
			}
			else
			{
				tvwDOM.SelectedNode.Text = Text;
			}
		}
		
		private void mnuFileSaveAs_Click(System.Object sender, System.EventArgs e)
		{
			if (mDocument == null)
			{
				return;
			}
			if (SaveHtmlFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					System.IO.StreamWriter sw = System.IO.File.CreateText(SaveHtmlFileDialog.FileName);
					if (SaveHtmlFileDialog.FilterIndex == 1)
					{
						sw.Write(mDocument.HTML);
					}
					else
					{
						sw.Write(mDocument.XHTML);
					}
					sw.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("Sorry, I couldn\'t save that file for some reason.. try another one!");
				}
			}
		}
		
		
		
		private void MenuItem3_Click(System.Object sender, System.EventArgs e)
		{
			string pagina = InputBox.Show("Página HTTP:").Text;
			string html = FSNetwork.Http.GetHTTP(pagina);
			
			ProcessHTML(html);
		}
		
		private void MenuItem7_Click(System.Object sender, System.EventArgs e)
		{
			string c = FSFormControls.InputBox.Show("Cadena:").Text;
			
			tvwDOM.Nodes.Clear();
			
			if (c == "")
			{
				BuildTree(mDocument.Nodes, tvwDOM.Nodes);
			}
			else
			{
				BuildTree(mDocument.Nodes.FindByText(c), tvwDOM.Nodes);
			}
			
		}
		
		private void MenuItem4_Click(System.Object sender, System.EventArgs e)
		{
			string c = FSFormControls.InputBox.Show("Cadena:").Text;
			
			tvwDOM.Nodes.Clear();
			
			if (c == "")
			{
				BuildTree(mDocument.Nodes, tvwDOM.Nodes);
			}
			else
			{
				BuildTree(mDocument.Nodes.FindByName(c), tvwDOM.Nodes);
			}
		}
		
		private void MenuItem5_Click(System.Object sender, System.EventArgs e)
		{
			string c = FSFormControls.InputBox.Show("Cadena:").Text;
			
			tvwDOM.Nodes.Clear();
			
			if (c == "")
			{
				BuildTree(mDocument.Nodes, tvwDOM.Nodes);
			}
			else
			{
				BuildTree(mDocument.Nodes.FindByAttributeName(c, true), tvwDOM.Nodes);
			}
		}
		
		private void MenuItem6_Click(System.Object sender, System.EventArgs e)
		{
			string c = FSFormControls.InputBox.Show("Cadena:").Text;
            string d = FSFormControls.InputBox.Show("Valor:").Text;
			
			tvwDOM.Nodes.Clear();
			
			if (c == "" && d == "")
			{
				BuildTree(mDocument.Nodes, tvwDOM.Nodes);
			}
			else
			{
				BuildTree(mDocument.Nodes.FindByAttributeNameValue(c, d, true), tvwDOM.Nodes);
			}
		}
		
		private void MenuItem8_Click(System.Object sender, System.EventArgs e)
		{
			frmPeliculas m = new frmPeliculas();
			m.Show();
		}
		
		private void MenuItem9_Click(System.Object sender, System.EventArgs e)
		{
			frmGoogle m = new frmGoogle();
			m.Show();
		}
		
		private void MenuItem10_Click(System.Object sender, System.EventArgs e)
		{
			frmPaginasAmarillas m = new frmPaginasAmarillas();
			m.Show();
		}
		
		private void MenuItem11_Click(System.Object sender, System.EventArgs e)
		{
			frmAyuntamientos m = new frmAyuntamientos();
			m.Show();
		}
		
		private void MenuItem12_Click(System.Object sender, System.EventArgs e)
		{
			frmExtraeEmail m = new frmExtraeEmail();
			m.Show();
		}
		
		private void MenuItem13_Click(System.Object sender, System.EventArgs e)
		{
			frmDemoWebBrowser m = new frmDemoWebBrowser();
			m.Show();
		}
	}
	
	
}
