namespace FileNormalization
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdRenombrar = new System.Windows.Forms.Button();
            this.lstSource = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renombrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ejecutarConToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdExportar = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.cmdCargar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmdNormalizar = new System.Windows.Forms.Button();
            this.cmdReglas = new System.Windows.Forms.Button();
            this.cmbExtension = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ficherosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirDirectorioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buscarDuplicadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmdCalcCRC32 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.chkCalcCRC32 = new System.Windows.Forms.CheckBox();
            this.dbFileExplorer1 = new FSFormControls.DBFileExplorer();
            this.cmdRemoveComments = new System.Windows.Forms.Button();
            this.toolStripStatusTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdRenombrar
            // 
            this.cmdRenombrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRenombrar.Location = new System.Drawing.Point(733, 276);
            this.cmdRenombrar.Name = "cmdRenombrar";
            this.cmdRenombrar.Size = new System.Drawing.Size(121, 21);
            this.cmdRenombrar.TabIndex = 4;
            this.cmdRenombrar.Text = "Renombrar ficheros";
            this.cmdRenombrar.UseVisualStyleBackColor = true;
            this.cmdRenombrar.Click += new System.EventHandler(this.cmdRenombrar_Click);
            // 
            // lstSource
            // 
            this.lstSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSource.CheckBoxes = true;
            this.lstSource.ContextMenuStrip = this.contextMenuStrip1;
            this.lstSource.HideSelection = false;
            this.lstSource.Location = new System.Drawing.Point(277, 69);
            this.lstSource.Name = "lstSource";
            this.lstSource.Size = new System.Drawing.Size(578, 201);
            this.lstSource.TabIndex = 5;
            this.lstSource.UseCompatibleStateImageBehavior = false;
            this.lstSource.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LstSourceColumnClick);
            this.lstSource.DoubleClick += new System.EventHandler(this.lstSource_DoubleClick);
            this.lstSource.Enter += new System.EventHandler(this.lst_Enter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renombrarToolStripMenuItem,
            this.borrarToolStripMenuItem,
            this.toolStripSeparator1,
            this.ejecutarConToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 76);
            // 
            // renombrarToolStripMenuItem
            // 
            this.renombrarToolStripMenuItem.Name = "renombrarToolStripMenuItem";
            this.renombrarToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.renombrarToolStripMenuItem.Text = "Renombrar";
            // 
            // borrarToolStripMenuItem
            // 
            this.borrarToolStripMenuItem.Name = "borrarToolStripMenuItem";
            this.borrarToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.borrarToolStripMenuItem.Text = "Borrar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // ejecutarConToolStripMenuItem
            // 
            this.ejecutarConToolStripMenuItem.Name = "ejecutarConToolStripMenuItem";
            this.ejecutarConToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ejecutarConToolStripMenuItem.Text = "Ejecutar con...";
            this.ejecutarConToolStripMenuItem.Click += new System.EventHandler(this.ejecutarConToolStripMenuItem_Click);
            // 
            // cmdExportar
            // 
            this.cmdExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportar.Location = new System.Drawing.Point(479, 276);
            this.cmdExportar.Name = "cmdExportar";
            this.cmdExportar.Size = new System.Drawing.Size(121, 21);
            this.cmdExportar.TabIndex = 6;
            this.cmdExportar.Text = "Guardar catálogo";
            this.cmdExportar.UseVisualStyleBackColor = true;
            this.cmdExportar.Click += new System.EventHandler(this.cmdGuardar_Click);
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdLimpiar.Location = new System.Drawing.Point(21, 276);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(76, 21);
            this.cmdLimpiar.TabIndex = 7;
            this.cmdLimpiar.Text = "Limpiar";
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.CmdLimpiarClick);
            // 
            // cmdCargar
            // 
            this.cmdCargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCargar.Location = new System.Drawing.Point(352, 277);
            this.cmdCargar.Name = "cmdCargar";
            this.cmdCargar.Size = new System.Drawing.Size(121, 21);
            this.cmdCargar.TabIndex = 8;
            this.cmdCargar.Text = "Cargar Catálogo";
            this.cmdCargar.UseVisualStyleBackColor = true;
            this.cmdCargar.Click += new System.EventHandler(this.CmdCargarClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Location = new System.Drawing.Point(776, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(78, 21);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.BtnBuscarClick);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.Location = new System.Drawing.Point(517, 41);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(249, 20);
            this.txtBuscar.TabIndex = 10;
            // 
            // cmdNormalizar
            // 
            this.cmdNormalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdNormalizar.Location = new System.Drawing.Point(103, 276);
            this.cmdNormalizar.Name = "cmdNormalizar";
            this.cmdNormalizar.Size = new System.Drawing.Size(77, 21);
            this.cmdNormalizar.TabIndex = 12;
            this.cmdNormalizar.Text = "Normalizar";
            this.cmdNormalizar.UseVisualStyleBackColor = true;
            this.cmdNormalizar.Click += new System.EventHandler(this.cmdNormalizar_Click);
            // 
            // cmdReglas
            // 
            this.cmdReglas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdReglas.Location = new System.Drawing.Point(186, 276);
            this.cmdReglas.Name = "cmdReglas";
            this.cmdReglas.Size = new System.Drawing.Size(77, 21);
            this.cmdReglas.TabIndex = 13;
            this.cmdReglas.Text = "Reglas";
            this.cmdReglas.UseVisualStyleBackColor = true;
            // 
            // cmbExtension
            // 
            this.cmbExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExtension.FormattingEnabled = true;
            this.cmbExtension.Location = new System.Drawing.Point(455, 42);
            this.cmbExtension.Name = "cmbExtension";
            this.cmbExtension.Size = new System.Drawing.Size(56, 21);
            this.cmbExtension.TabIndex = 14;
            this.cmbExtension.SelectedIndexChanged += new System.EventHandler(this.cmbExtension_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ficherosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(876, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ficherosToolStripMenuItem
            // 
            this.ficherosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirDirectorioToolStripMenuItem,
            this.limpiarToolStripMenuItem,
            this.normalizarToolStripMenuItem,
            this.buscarDuplicadosToolStripMenuItem,
            this.toolStripMenuItem1,
            this.salirToolStripMenuItem});
            this.ficherosToolStripMenuItem.Name = "ficherosToolStripMenuItem";
            this.ficherosToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ficherosToolStripMenuItem.Text = "Ficheros";
            // 
            // añadirDirectorioToolStripMenuItem
            // 
            this.añadirDirectorioToolStripMenuItem.Name = "añadirDirectorioToolStripMenuItem";
            this.añadirDirectorioToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.añadirDirectorioToolStripMenuItem.Text = "Añadir directorio";
            this.añadirDirectorioToolStripMenuItem.Click += new System.EventHandler(this.añadirDirectorioToolStripMenuItem_Click);
            // 
            // limpiarToolStripMenuItem
            // 
            this.limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            this.limpiarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.limpiarToolStripMenuItem.Text = "Limpiar";
            this.limpiarToolStripMenuItem.Click += new System.EventHandler(this.limpiarToolStripMenuItem_Click);
            // 
            // normalizarToolStripMenuItem
            // 
            this.normalizarToolStripMenuItem.Name = "normalizarToolStripMenuItem";
            this.normalizarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.normalizarToolStripMenuItem.Text = "Normalizar";
            this.normalizarToolStripMenuItem.Click += new System.EventHandler(this.normalizarToolStripMenuItem_Click);
            // 
            // buscarDuplicadosToolStripMenuItem
            // 
            this.buscarDuplicadosToolStripMenuItem.Name = "buscarDuplicadosToolStripMenuItem";
            this.buscarDuplicadosToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.buscarDuplicadosToolStripMenuItem.Text = "Buscar duplicados";
            this.buscarDuplicadosToolStripMenuItem.Click += new System.EventHandler(this.buscarDuplicadosToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(414, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Filtrar:";
            // 
            // cmdBack
            // 
            this.cmdBack.Location = new System.Drawing.Point(21, 40);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(45, 21);
            this.cmdBack.TabIndex = 21;
            this.cmdBack.Text = "Atrás";
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // cmdCalcCRC32
            // 
            this.cmdCalcCRC32.Location = new System.Drawing.Point(72, 40);
            this.cmdCalcCRC32.Name = "cmdCalcCRC32";
            this.cmdCalcCRC32.Size = new System.Drawing.Size(45, 21);
            this.cmdCalcCRC32.TabIndex = 22;
            this.cmdCalcCRC32.Text = "Crc32";
            this.cmdCalcCRC32.UseVisualStyleBackColor = true;
            this.cmdCalcCRC32.Click += new System.EventHandler(this.cmdCalcCRC32_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(123, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 21);
            this.button3.TabIndex = 23;
            this.button3.Text = "---";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(174, 40);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(45, 21);
            this.button4.TabIndex = 24;
            this.button4.Text = "---";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusTotal,
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 315);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(876, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(624, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // chkCalcCRC32
            // 
            this.chkCalcCRC32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCalcCRC32.AutoSize = true;
            this.chkCalcCRC32.Location = new System.Drawing.Point(277, 40);
            this.chkCalcCRC32.Name = "chkCalcCRC32";
            this.chkCalcCRC32.Size = new System.Drawing.Size(101, 17);
            this.chkCalcCRC32.TabIndex = 27;
            this.chkCalcCRC32.Text = "Calcular CRC32";
            this.chkCalcCRC32.UseVisualStyleBackColor = true;
            // 
            // dbFileExplorer1
            // 
            this.dbFileExplorer1.About = "";
            this.dbFileExplorer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dbFileExplorer1.DataControl = null;
            this.dbFileExplorer1.Location = new System.Drawing.Point(21, 69);
            this.dbFileExplorer1.Name = "dbFileExplorer1";
            this.dbFileExplorer1.SelectedDrive = ((System.IO.DriveInfo)(resources.GetObject("dbFileExplorer1.SelectedDrive")));
            this.dbFileExplorer1.SelectedNode = null;
            this.dbFileExplorer1.Size = new System.Drawing.Size(250, 201);
            this.dbFileExplorer1.TabIndex = 26;
            this.dbFileExplorer1.Track = false;
            // 
            // cmdRemoveComments
            // 
            this.cmdRemoveComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRemoveComments.Location = new System.Drawing.Point(606, 276);
            this.cmdRemoveComments.Name = "cmdRemoveComments";
            this.cmdRemoveComments.Size = new System.Drawing.Size(121, 21);
            this.cmdRemoveComments.TabIndex = 28;
            this.cmdRemoveComments.Text = "Eliminar comentarios";
            this.cmdRemoveComments.UseVisualStyleBackColor = true;
            this.cmdRemoveComments.Click += new System.EventHandler(this.cmdRemoveComments_Click);
            // 
            // toolStripStatusTotal
            // 
            this.toolStripStatusTotal.Name = "toolStripStatusTotal";
            this.toolStripStatusTotal.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusTotal.Text = "Total:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 337);
            this.Controls.Add(this.cmdRemoveComments);
            this.Controls.Add(this.chkCalcCRC32);
            this.Controls.Add(this.dbFileExplorer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmdCalcCRC32);
            this.Controls.Add(this.cmdBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbExtension);
            this.Controls.Add(this.cmdReglas);
            this.Controls.Add(this.cmdNormalizar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.cmdCargar);
            this.Controls.Add(this.cmdLimpiar);
            this.Controls.Add(this.cmdExportar);
            this.Controls.Add(this.lstSource);
            this.Controls.Add(this.cmdRenombrar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(892, 365);
            this.Name = "frmMain";
            this.Text = "Listado de ficheros a normalizar";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdRenombrar;
        private System.Windows.Forms.ListView lstSource;
        private System.Windows.Forms.Button cmdExportar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button cmdLimpiar;
        private System.Windows.Forms.Button cmdCargar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button cmdNormalizar;
        private System.Windows.Forms.Button cmdReglas;
        private System.Windows.Forms.ComboBox cmbExtension;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ficherosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirDirectorioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buscarDuplicadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ejecutarConToolStripMenuItem;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.Button cmdCalcCRC32;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private FSFormControls.DBFileExplorer dbFileExplorer1;
        private System.Windows.Forms.CheckBox chkCalcCRC32;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTotal;
        private System.Windows.Forms.Button cmdRemoveComments;
    }
}

