namespace WiworBrowser.UI.Controls
{
    partial class TreeControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeControl));
            this.favoritesTabControl = new System.Windows.Forms.TabControl();
            this.favTabPage = new System.Windows.Forms.TabPage();
            this.favTreeView = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.historyTabPage = new System.Windows.Forms.TabPage();
            this.cmbHistory = new System.Windows.Forms.ComboBox();
            this.historyTreeView = new System.Windows.Forms.TreeView();
            this.favContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.linkContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoEnlaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoritesTabControl.SuspendLayout();
            this.favTabPage.SuspendLayout();
            this.historyTabPage.SuspendLayout();
            this.favContextMenu.SuspendLayout();
            this.linkContextMenu.SuspendLayout();
            this.histContextMenu.SuspendLayout();
            this.groupContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // favoritesTabControl
            // 
            this.favoritesTabControl.Controls.Add(this.favTabPage);
            this.favoritesTabControl.Controls.Add(this.historyTabPage);
            this.favoritesTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favoritesTabControl.Location = new System.Drawing.Point(0, 0);
            this.favoritesTabControl.Name = "favoritesTabControl";
            this.favoritesTabControl.SelectedIndex = 0;
            this.favoritesTabControl.Size = new System.Drawing.Size(255, 266);
            this.favoritesTabControl.TabIndex = 2;
            // 
            // favTabPage
            // 
            this.favTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.favTabPage.Controls.Add(this.favTreeView);
            this.favTabPage.Location = new System.Drawing.Point(4, 22);
            this.favTabPage.Name = "favTabPage";
            this.favTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.favTabPage.Size = new System.Drawing.Size(247, 240);
            this.favTabPage.TabIndex = 0;
            this.favTabPage.Text = "Favoritos";
            this.favTabPage.UseVisualStyleBackColor = true;
            // 
            // favTreeView
            // 
            this.favTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.favTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favTreeView.ImageIndex = 0;
            this.favTreeView.ImageList = this.imgList;
            this.favTreeView.ItemHeight = 20;
            this.favTreeView.Location = new System.Drawing.Point(3, 3);
            this.favTreeView.Name = "favTreeView";
            this.favTreeView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.favTreeView.SelectedImageIndex = 0;
            this.favTreeView.ShowNodeToolTips = true;
            this.favTreeView.Size = new System.Drawing.Size(237, 230);
            this.favTreeView.TabIndex = 0;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Folder3.ico");
            this.imgList.Images.SetKeyName(1, "net.png");
            this.imgList.Images.SetKeyName(2, "calendar.png");
            this.imgList.Images.SetKeyName(3, "link.png");
            this.imgList.Images.SetKeyName(4, "Folder.ico");
            this.imgList.Images.SetKeyName(5, "folder.gif");
            // 
            // historyTabPage
            // 
            this.historyTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.historyTabPage.Controls.Add(this.cmbHistory);
            this.historyTabPage.Controls.Add(this.historyTreeView);
            this.historyTabPage.Location = new System.Drawing.Point(4, 22);
            this.historyTabPage.Name = "historyTabPage";
            this.historyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.historyTabPage.Size = new System.Drawing.Size(247, 240);
            this.historyTabPage.TabIndex = 2;
            this.historyTabPage.Text = "Historia";
            this.historyTabPage.UseVisualStyleBackColor = true;
            // 
            // cmbHistory
            // 
            this.cmbHistory.FormattingEnabled = true;
            this.cmbHistory.Items.AddRange(new object[] {
            "Visitas por fecha",
            "Visitas por página",
            "Visitas realizadas hoy"});
            this.cmbHistory.Location = new System.Drawing.Point(6, 7);
            this.cmbHistory.Name = "cmbHistory";
            this.cmbHistory.Size = new System.Drawing.Size(127, 21);
            this.cmbHistory.TabIndex = 1;
            // 
            // historyTreeView
            // 
            this.historyTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.historyTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.historyTreeView.ImageIndex = 0;
            this.historyTreeView.ImageList = this.imgList;
            this.historyTreeView.Location = new System.Drawing.Point(3, 34);
            this.historyTreeView.Name = "historyTreeView";
            this.historyTreeView.SelectedImageIndex = 0;
            this.historyTreeView.ShowLines = false;
            this.historyTreeView.ShowNodeToolTips = true;
            this.historyTreeView.ShowRootLines = false;
            this.historyTreeView.Size = new System.Drawing.Size(234, 194);
            this.historyTreeView.TabIndex = 0;
            // 
            // favContextMenu
            // 
            this.favContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem1,
            this.renameToolStripMenuItem1});
            this.favContextMenu.Name = "favContextMenu";
            this.favContextMenu.Size = new System.Drawing.Size(134, 48);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.deleteToolStripMenuItem1.Text = "Borrar";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.renameToolStripMenuItem1.Text = "Renombrar";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // linkContextMenu
            // 
            this.linkContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.linkContextMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.linkContextMenu.Name = "linkContextMenu";
            this.linkContextMenu.Size = new System.Drawing.Size(134, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteToolStripMenuItem.Text = "Borrar";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.renameToolStripMenuItem.Text = "Renombrar";
            // 
            // histContextMenu
            // 
            this.histContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFavoritesToolStripMenuItem,
            this.deleteToolStripMenuItem2});
            this.histContextMenu.Name = "histContextMenu";
            this.histContextMenu.Size = new System.Drawing.Size(168, 48);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Añadir a favoritos";
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(167, 22);
            this.deleteToolStripMenuItem2.Text = "Borrar";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem2_Click);
            // 
            // groupContextMenu
            // 
            this.groupContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.nuevoEnlaceToolStripMenuItem,
            this.newFolderToolStripMenuItem});
            this.groupContextMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.groupContextMenu.Name = "linkContextMenu";
            this.groupContextMenu.Size = new System.Drawing.Size(153, 114);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Borrar";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Renombrar";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // nuevoEnlaceToolStripMenuItem
            // 
            this.nuevoEnlaceToolStripMenuItem.Name = "nuevoEnlaceToolStripMenuItem";
            this.nuevoEnlaceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoEnlaceToolStripMenuItem.Text = "Nuevo enlace";
            this.nuevoEnlaceToolStripMenuItem.Click += new System.EventHandler(this.nuevoEnlaceToolStripMenuItem_Click);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newFolderToolStripMenuItem.Text = "Nueva carpeta";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // TreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.favoritesTabControl);
            this.Name = "TreeControl";
            this.Size = new System.Drawing.Size(255, 266);
            this.favoritesTabControl.ResumeLayout(false);
            this.favTabPage.ResumeLayout(false);
            this.historyTabPage.ResumeLayout(false);
            this.favContextMenu.ResumeLayout(false);
            this.linkContextMenu.ResumeLayout(false);
            this.histContextMenu.ResumeLayout(false);
            this.groupContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl favoritesTabControl;
        private System.Windows.Forms.TabPage favTabPage;
        private System.Windows.Forms.TreeView favTreeView;
        private System.Windows.Forms.TabPage historyTabPage;
        private System.Windows.Forms.ComboBox cmbHistory;
        private System.Windows.Forms.TreeView historyTreeView;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip favContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip linkContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip histContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip groupContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem nuevoEnlaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
    }
}
