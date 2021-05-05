namespace FileNormalization
{
    partial class frmCompare
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstFileNames = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renombrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ejecutarConToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstDuplicates = new System.Windows.Forms.ListView();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.cmdCambiar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLevenshteinValue = new System.Windows.Forms.ComboBox();
            this.cmbFuzzyValue = new System.Windows.Forms.ComboBox();
            this.cmbSoundExValue = new System.Windows.Forms.ComboBox();
            this.optLevenshtein = new System.Windows.Forms.RadioButton();
            this.optFuzzy = new System.Windows.Forms.RadioButton();
            this.optSimilar = new System.Windows.Forms.RadioButton();
            this.optSoundEx = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstFileNames
            // 
            this.lstFileNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstFileNames.ContextMenuStrip = this.contextMenuStrip1;
            this.lstFileNames.Location = new System.Drawing.Point(12, 84);
            this.lstFileNames.Name = "lstFileNames";
            this.lstFileNames.Size = new System.Drawing.Size(400, 148);
            this.lstFileNames.TabIndex = 0;
            this.lstFileNames.UseCompatibleStateImageBehavior = false;
            this.lstFileNames.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstFileNames_ItemSelectionChanged);
            this.lstFileNames.Click += new System.EventHandler(this.lstFileNames_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renombrarToolStripMenuItem,
            this.borrarToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ejecutarConToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 76);
            // 
            // renombrarToolStripMenuItem
            // 
            this.renombrarToolStripMenuItem.Name = "renombrarToolStripMenuItem";
            this.renombrarToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.renombrarToolStripMenuItem.Text = "Renombrar";
            this.renombrarToolStripMenuItem.Click += new System.EventHandler(this.renombrarToolStripMenuItem_Click);
            // 
            // borrarToolStripMenuItem
            // 
            this.borrarToolStripMenuItem.Name = "borrarToolStripMenuItem";
            this.borrarToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.borrarToolStripMenuItem.Text = "Borrar";
            this.borrarToolStripMenuItem.Click += new System.EventHandler(this.borrarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // ejecutarConToolStripMenuItem
            // 
            this.ejecutarConToolStripMenuItem.Name = "ejecutarConToolStripMenuItem";
            this.ejecutarConToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ejecutarConToolStripMenuItem.Text = "Ejecutar con...";
            // 
            // lstDuplicates
            // 
            this.lstDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDuplicates.ContextMenuStrip = this.contextMenuStrip1;
            this.lstDuplicates.Location = new System.Drawing.Point(418, 12);
            this.lstDuplicates.Name = "lstDuplicates";
            this.lstDuplicates.Size = new System.Drawing.Size(252, 220);
            this.lstDuplicates.TabIndex = 1;
            this.lstDuplicates.UseCompatibleStateImageBehavior = false;
            // 
            // cmdSalir
            // 
            this.cmdSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSalir.Location = new System.Drawing.Point(595, 238);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(75, 23);
            this.cmdSalir.TabIndex = 2;
            this.cmdSalir.Text = "Cerrar";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // cmdCambiar
            // 
            this.cmdCambiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCambiar.Location = new System.Drawing.Point(514, 238);
            this.cmdCambiar.Name = "cmdCambiar";
            this.cmdCambiar.Size = new System.Drawing.Size(75, 23);
            this.cmdCambiar.TabIndex = 3;
            this.cmdCambiar.Text = "Cambiar";
            this.cmdCambiar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbLevenshteinValue);
            this.groupBox1.Controls.Add(this.cmbFuzzyValue);
            this.groupBox1.Controls.Add(this.cmbSoundExValue);
            this.groupBox1.Controls.Add(this.optLevenshtein);
            this.groupBox1.Controls.Add(this.optFuzzy);
            this.groupBox1.Controls.Add(this.optSimilar);
            this.groupBox1.Controls.Add(this.optSoundEx);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Método de búsqueda";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Precisión:";
            // 
            // cmbLevenshteinValue
            // 
            this.cmbLevenshteinValue.FormattingEnabled = true;
            this.cmbLevenshteinValue.Location = new System.Drawing.Point(292, 40);
            this.cmbLevenshteinValue.Name = "cmbLevenshteinValue";
            this.cmbLevenshteinValue.Size = new System.Drawing.Size(81, 21);
            this.cmbLevenshteinValue.TabIndex = 6;
            // 
            // cmbFuzzyValue
            // 
            this.cmbFuzzyValue.FormattingEnabled = true;
            this.cmbFuzzyValue.Location = new System.Drawing.Point(198, 40);
            this.cmbFuzzyValue.Name = "cmbFuzzyValue";
            this.cmbFuzzyValue.Size = new System.Drawing.Size(81, 21);
            this.cmbFuzzyValue.TabIndex = 5;
            // 
            // cmbSoundExValue
            // 
            this.cmbSoundExValue.FormattingEnabled = true;
            this.cmbSoundExValue.Location = new System.Drawing.Point(103, 40);
            this.cmbSoundExValue.Name = "cmbSoundExValue";
            this.cmbSoundExValue.Size = new System.Drawing.Size(81, 21);
            this.cmbSoundExValue.TabIndex = 4;
            // 
            // optLevenshtein
            // 
            this.optLevenshtein.AutoSize = true;
            this.optLevenshtein.Location = new System.Drawing.Point(292, 19);
            this.optLevenshtein.Name = "optLevenshtein";
            this.optLevenshtein.Size = new System.Drawing.Size(83, 17);
            this.optLevenshtein.TabIndex = 3;
            this.optLevenshtein.TabStop = true;
            this.optLevenshtein.Text = "Levenshtein";
            this.optLevenshtein.UseVisualStyleBackColor = true;
            this.optLevenshtein.CheckedChanged += new System.EventHandler(this.optLevenshtein_CheckedChanged);
            // 
            // optFuzzy
            // 
            this.optFuzzy.AutoSize = true;
            this.optFuzzy.Location = new System.Drawing.Point(198, 19);
            this.optFuzzy.Name = "optFuzzy";
            this.optFuzzy.Size = new System.Drawing.Size(52, 17);
            this.optFuzzy.TabIndex = 2;
            this.optFuzzy.TabStop = true;
            this.optFuzzy.Text = "Fuzzy";
            this.optFuzzy.UseVisualStyleBackColor = true;
            this.optFuzzy.CheckedChanged += new System.EventHandler(this.optFuzzy_CheckedChanged);
            // 
            // optSimilar
            // 
            this.optSimilar.AutoSize = true;
            this.optSimilar.Checked = true;
            this.optSimilar.Location = new System.Drawing.Point(6, 19);
            this.optSimilar.Name = "optSimilar";
            this.optSimilar.Size = new System.Drawing.Size(55, 17);
            this.optSimilar.TabIndex = 1;
            this.optSimilar.TabStop = true;
            this.optSimilar.Text = "Similar";
            this.optSimilar.UseVisualStyleBackColor = true;
            this.optSimilar.CheckedChanged += new System.EventHandler(this.optSimilar_CheckedChanged);
            // 
            // optSoundEx
            // 
            this.optSoundEx.AutoSize = true;
            this.optSoundEx.Location = new System.Drawing.Point(103, 19);
            this.optSoundEx.Name = "optSoundEx";
            this.optSoundEx.Size = new System.Drawing.Size(68, 17);
            this.optSoundEx.TabIndex = 0;
            this.optSoundEx.TabStop = true;
            this.optSoundEx.Text = "SoundEx";
            this.optSoundEx.UseVisualStyleBackColor = true;
            this.optSoundEx.CheckedChanged += new System.EventHandler(this.optSoundEx_CheckedChanged);
            // 
            // frmCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 273);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCambiar);
            this.Controls.Add(this.cmdSalir);
            this.Controls.Add(this.lstDuplicates);
            this.Controls.Add(this.lstFileNames);
            this.Name = "frmCompare";
            this.Text = "Eliminar duplicados";
            this.Load += new System.EventHandler(this.frmCompare_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstFileNames;
        private System.Windows.Forms.ListView lstDuplicates;
        private System.Windows.Forms.Button cmdSalir;
        private System.Windows.Forms.Button cmdCambiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optSimilar;
        private System.Windows.Forms.RadioButton optSoundEx;
        private System.Windows.Forms.RadioButton optFuzzy;
        private System.Windows.Forms.RadioButton optLevenshtein;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ejecutarConToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLevenshteinValue;
        private System.Windows.Forms.ComboBox cmbFuzzyValue;
        private System.Windows.Forms.ComboBox cmbSoundExValue;
    }
}