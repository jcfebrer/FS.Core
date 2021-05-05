namespace WiworBrowser.UI.Controls
{
    partial class RenameLink
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmdRename = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.newName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nuevo nombre:";
            // 
            // cmdRename
            // 
            this.cmdRename.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdRename.Location = new System.Drawing.Point(179, 48);
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(75, 23);
            this.cmdRename.TabIndex = 1;
            this.cmdRename.Text = "Renombrar";
            this.cmdRename.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(260, 48);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancelar";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // newName
            // 
            this.newName.Location = new System.Drawing.Point(96, 13);
            this.newName.Name = "newName";
            this.newName.Size = new System.Drawing.Size(239, 20);
            this.newName.TabIndex = 0;
            // 
            // RenameLink
            // 
            this.AcceptButton = this.cmdRename;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 83);
            this.Controls.Add(this.newName);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdRename);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RenameLink";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Renombrar";
            this.Load += new System.EventHandler(this.RenameLink_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdRename;
        private System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.TextBox newName;
    }
}