namespace FSPong
{
    partial class frmPong
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
            this.cmdPlay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optSolo = new System.Windows.Forms.RadioButton();
            this.optSquash = new System.Windows.Forms.RadioButton();
            this.optFootball = new System.Windows.Forms.RadioButton();
            this.optTennis = new System.Windows.Forms.RadioButton();
            this.cmdReset = new System.Windows.Forms.Button();
            this.chkSize = new System.Windows.Forms.CheckBox();
            this.chkSpeed = new System.Windows.Forms.CheckBox();
            this.chkAngle = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panelPong = new FSPong.PongPanel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdPlay
            // 
            this.cmdPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPlay.Location = new System.Drawing.Point(541, 260);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(98, 32);
            this.cmdPlay.TabIndex = 0;
            this.cmdPlay.Text = "JUGAR!";
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.optSolo);
            this.groupBox1.Controls.Add(this.optSquash);
            this.groupBox1.Controls.Add(this.optFootball);
            this.groupBox1.Controls.Add(this.optTennis);
            this.groupBox1.Location = new System.Drawing.Point(551, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 119);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Juego";
            // 
            // optSolo
            // 
            this.optSolo.AutoSize = true;
            this.optSolo.Location = new System.Drawing.Point(7, 91);
            this.optSolo.Name = "optSolo";
            this.optSolo.Size = new System.Drawing.Size(70, 17);
            this.optSolo.TabIndex = 6;
            this.optSolo.TabStop = true;
            this.optSolo.Text = "Individual";
            this.optSolo.UseVisualStyleBackColor = true;
            this.optSolo.CheckedChanged += new System.EventHandler(this.optSolo_CheckedChanged);
            // 
            // optSquash
            // 
            this.optSquash.AutoSize = true;
            this.optSquash.Location = new System.Drawing.Point(7, 68);
            this.optSquash.Name = "optSquash";
            this.optSquash.Size = new System.Drawing.Size(61, 17);
            this.optSquash.TabIndex = 2;
            this.optSquash.TabStop = true;
            this.optSquash.Text = "Frontón";
            this.optSquash.UseVisualStyleBackColor = true;
            this.optSquash.CheckedChanged += new System.EventHandler(this.optSquash_CheckedChanged);
            // 
            // optFootball
            // 
            this.optFootball.AutoSize = true;
            this.optFootball.Location = new System.Drawing.Point(7, 44);
            this.optFootball.Name = "optFootball";
            this.optFootball.Size = new System.Drawing.Size(54, 17);
            this.optFootball.TabIndex = 1;
            this.optFootball.TabStop = true;
            this.optFootball.Text = "Futból";
            this.optFootball.UseVisualStyleBackColor = true;
            this.optFootball.CheckedChanged += new System.EventHandler(this.optFootball_CheckedChanged);
            // 
            // optTennis
            // 
            this.optTennis.AutoSize = true;
            this.optTennis.Checked = true;
            this.optTennis.Location = new System.Drawing.Point(7, 20);
            this.optTennis.Name = "optTennis";
            this.optTennis.Size = new System.Drawing.Size(51, 17);
            this.optTennis.TabIndex = 0;
            this.optTennis.TabStop = true;
            this.optTennis.Text = "Tenis";
            this.optTennis.UseVisualStyleBackColor = true;
            this.optTennis.CheckedChanged += new System.EventHandler(this.optTennis_CheckedChanged);
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReset.Location = new System.Drawing.Point(541, 223);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(98, 31);
            this.cmdReset.TabIndex = 6;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // chkSize
            // 
            this.chkSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSize.AutoSize = true;
            this.chkSize.Location = new System.Drawing.Point(557, 19);
            this.chkSize.Name = "chkSize";
            this.chkSize.Size = new System.Drawing.Size(65, 17);
            this.chkSize.TabIndex = 10;
            this.chkSize.Text = "Tamaño";
            this.chkSize.UseVisualStyleBackColor = true;
            this.chkSize.CheckedChanged += new System.EventHandler(this.chkSize_CheckedChanged);
            // 
            // chkSpeed
            // 
            this.chkSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSpeed.AutoSize = true;
            this.chkSpeed.Location = new System.Drawing.Point(557, 43);
            this.chkSpeed.Name = "chkSpeed";
            this.chkSpeed.Size = new System.Drawing.Size(73, 17);
            this.chkSpeed.TabIndex = 11;
            this.chkSpeed.Text = "Velocidad";
            this.chkSpeed.UseVisualStyleBackColor = true;
            this.chkSpeed.CheckedChanged += new System.EventHandler(this.chkSpeed_CheckedChanged);
            // 
            // chkAngle
            // 
            this.chkAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAngle.AutoSize = true;
            this.chkAngle.Location = new System.Drawing.Point(557, 67);
            this.chkAngle.Name = "chkAngle";
            this.chkAngle.Size = new System.Drawing.Size(59, 17);
            this.chkAngle.TabIndex = 12;
            this.chkAngle.Text = "Ángulo";
            this.chkAngle.UseVisualStyleBackColor = true;
            this.chkAngle.CheckedChanged += new System.EventHandler(this.chkAngle_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(541, 356);
            this.trackBar1.Maximum = 6;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(103, 33);
            this.trackBar1.TabIndex = 13;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Value = 3;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // panelPong
            // 
            this.panelPong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPong.BackColor = System.Drawing.Color.Green;
            this.panelPong.Location = new System.Drawing.Point(12, 12);
            this.panelPong.Name = "panelPong";
            this.panelPong.Size = new System.Drawing.Size(510, 377);
            this.panelPong.TabIndex = 9;
            // 
            // frmPong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 401);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.chkAngle);
            this.Controls.Add(this.chkSpeed);
            this.Controls.Add(this.chkSize);
            this.Controls.Add(this.panelPong);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdPlay);
            this.Name = "frmPong";
            this.Text = "FSPong";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdPlay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdReset;
        public System.Windows.Forms.RadioButton optSquash;
        public System.Windows.Forms.RadioButton optFootball;
        public System.Windows.Forms.RadioButton optTennis;
        public System.Windows.Forms.RadioButton optSolo;
        public PongPanel panelPong;
        public System.Windows.Forms.CheckBox chkSize;
        public System.Windows.Forms.CheckBox chkSpeed;
        public System.Windows.Forms.CheckBox chkAngle;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

