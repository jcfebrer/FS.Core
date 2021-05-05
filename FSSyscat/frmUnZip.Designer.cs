namespace FSSyscat
{
    partial class frmUnZip
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
            this.cmdSourceFile = new System.Windows.Forms.Button();
            this.cmdUnzip = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.cmdTargetFolder = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSourceFile
            // 
            this.cmdSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSourceFile.Location = new System.Drawing.Point(292, 33);
            this.cmdSourceFile.Name = "cmdSourceFile";
            this.cmdSourceFile.Size = new System.Drawing.Size(23, 23);
            this.cmdSourceFile.TabIndex = 0;
            this.cmdSourceFile.Text = "V";
            this.cmdSourceFile.UseVisualStyleBackColor = true;
            this.cmdSourceFile.Click += new System.EventHandler(this.cmdSourceFile_Click);
            // 
            // cmdUnzip
            // 
            this.cmdUnzip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUnzip.Location = new System.Drawing.Point(221, 110);
            this.cmdUnzip.Name = "cmdUnzip";
            this.cmdUnzip.Size = new System.Drawing.Size(94, 23);
            this.cmdUnzip.TabIndex = 1;
            this.cmdUnzip.Text = "Descomprimir";
            this.cmdUnzip.UseVisualStyleBackColor = true;
            this.cmdUnzip.Click += new System.EventHandler(this.cmdUnzip_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFile.Location = new System.Drawing.Point(27, 35);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(259, 20);
            this.txtSourceFile.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selecciona el fichero que deseas descomprimir:";
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetFolder.Location = new System.Drawing.Point(27, 77);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(259, 20);
            this.txtTargetFolder.TabIndex = 5;
            // 
            // cmdTargetFolder
            // 
            this.cmdTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTargetFolder.Location = new System.Drawing.Point(292, 75);
            this.cmdTargetFolder.Name = "cmdTargetFolder";
            this.cmdTargetFolder.Size = new System.Drawing.Size(23, 23);
            this.cmdTargetFolder.TabIndex = 4;
            this.cmdTargetFolder.Text = "V";
            this.cmdTargetFolder.UseVisualStyleBackColor = true;
            this.cmdTargetFolder.Click += new System.EventHandler(this.cmdTargetFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 149);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(344, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(329, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Descomprimir fichero.";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Selecciona el destino:";
            // 
            // frmUnZip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 171);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtTargetFolder);
            this.Controls.Add(this.cmdTargetFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.cmdUnzip);
            this.Controls.Add(this.cmdSourceFile);
            this.MinimumSize = new System.Drawing.Size(360, 210);
            this.Name = "frmUnZip";
            this.Text = "Descomprimir fichero FSZ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUnZip_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSourceFile;
        private System.Windows.Forms.Button cmdUnzip;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Button cmdTargetFolder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label2;
    }
}