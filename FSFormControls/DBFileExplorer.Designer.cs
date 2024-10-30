namespace FSFormControls
{
    partial class DBFileExplorer
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbDrives = new System.Windows.Forms.ComboBox();
            this.dbTreeView1 = new FSFormControls.DBTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.dbTreeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDrives
            // 
            this.cmbDrives.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDrives.FormattingEnabled = true;
            this.cmbDrives.Location = new System.Drawing.Point(0, 0);
            this.cmbDrives.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(313, 24);
            this.cmbDrives.TabIndex = 28;
            this.cmbDrives.SelectedIndexChanged += new System.EventHandler(this.cmbDrives_SelectedIndexChanged);
            // 
            // dbTreeView1
            // 
            this.dbTreeView1.About = "";
            this.dbTreeView1.AllowLoadXML = false;
            this.dbTreeView1.AllowSaveXML = true;
            this.dbTreeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbTreeView1.DataControl = null;
            this.dbTreeView1.EnableReArrange = false;
            this.dbTreeView1.HideSelection = true;
            this.dbTreeView1.HotTracking = false;
            this.dbTreeView1.Level = 0;
            this.dbTreeView1.Location = new System.Drawing.Point(0, 33);
            this.dbTreeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dbTreeView1.Name = "dbTreeView1";
            this.dbTreeView1.SelectedNode = null;
            this.dbTreeView1.ShowLines = true;
            this.dbTreeView1.ShowRootLines = true;
            this.dbTreeView1.Size = new System.Drawing.Size(313, 264);
            this.dbTreeView1.TabIndex = 27;
            this.dbTreeView1.NodeMouseClick += new FSFormControls.DBTreeView.TreeNodeMouseClickEventHandler(this.dbTreeView1_NodeMouseClick);
            this.dbTreeView1.NodeMouseDoubleClick += new FSFormControls.DBTreeView.TreeNodeMouseDoubleClickEventHandler(this.dbTreeView1_NodeMouseDoubleClick);
            // 
            // DBFileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbDrives);
            this.Controls.Add(this.dbTreeView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DBFileExplorer";
            this.Size = new System.Drawing.Size(313, 300);
            ((System.ComponentModel.ISupportInitialize)(this.dbTreeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDrives;
        private DBTreeView dbTreeView1;
    }
}
