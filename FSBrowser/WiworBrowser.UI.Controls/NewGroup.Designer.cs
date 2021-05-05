namespace WiworBrowser.UI.Controls
{
    partial class NewGroup
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
            this.cmdNewFolder = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre:";
            // 
            // cmdNewFolder
            // 
            this.cmdNewFolder.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdNewFolder.Location = new System.Drawing.Point(159, 48);
            this.cmdNewFolder.Name = "cmdNewFolder";
            this.cmdNewFolder.Size = new System.Drawing.Size(95, 23);
            this.cmdNewFolder.TabIndex = 1;
            this.cmdNewFolder.Text = "Nuevo grupo";
            this.cmdNewFolder.UseVisualStyleBackColor = true;
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
            // groupName
            // 
            this.groupName.Location = new System.Drawing.Point(66, 13);
            this.groupName.Name = "groupName";
            this.groupName.Size = new System.Drawing.Size(269, 20);
            this.groupName.TabIndex = 0;
            // 
            // NewGroup
            // 
            this.AcceptButton = this.cmdNewFolder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 83);
            this.Controls.Add(this.groupName);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdNewFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nuevo grupo";
            this.Load += new System.EventHandler(this.NewGroup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdNewFolder;
        private System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.TextBox groupName;
    }
}