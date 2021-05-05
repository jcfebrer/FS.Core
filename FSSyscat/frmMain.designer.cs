using System.Windows.Forms;
namespace FSSyscat
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.main_Menu = new System.Windows.Forms.MainMenu(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmdSendEmail = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBackup = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUnzip = new System.Windows.Forms.Button();
            this.btnLaunchNow = new System.Windows.Forms.Button();
            this.chkCompress = new System.Windows.Forms.CheckBox();
            this.chkCopyHidden = new System.Windows.Forms.CheckBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listViewBackups = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.borrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDestino = new System.Windows.Forms.Button();
            this.btnOrigen = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnProgram = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.optWeekly = new System.Windows.Forms.RadioButton();
            this.optMounthly = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMinutes = new System.Windows.Forms.NumericUpDown();
            this.txtHour = new System.Windows.Forms.NumericUpDown();
            this.optDiary = new System.Windows.Forms.RadioButton();
            this.chkDomingo = new System.Windows.Forms.CheckBox();
            this.chkSabado = new System.Windows.Forms.CheckBox();
            this.chkViernes = new System.Windows.Forms.CheckBox();
            this.chkJueves = new System.Windows.Forms.CheckBox();
            this.chkMiercoles = new System.Windows.Forms.CheckBox();
            this.chkMartes = new System.Windows.Forms.CheckBox();
            this.chkLunes = new System.Windows.Forms.CheckBox();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.cmdActualizar = new System.Windows.Forms.Button();
            this.cmdConectarFSVNC = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtConversation = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdEnviar = new System.Windows.Forms.Button();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.txtSystemInfo = new System.Windows.Forms.TextBox();
            this.tabUPnP = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabCapture = new System.Windows.Forms.TabPage();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.txt_CurrentWindowstitle = new System.Windows.Forms.TextBox();
            this.cmdNext = new System.Windows.Forms.Button();
            this.picScreen = new System.Windows.Forms.PictureBox();
            this.cmdPrev = new System.Windows.Forms.Button();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdLimpiar = new System.Windows.Forms.Button();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.btnHideProcess = new System.Windows.Forms.Button();
            this.lstProcess = new System.Windows.Forms.ListBox();
            this.btnShowProcess = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabBackup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).BeginInit();
            this.tabChat.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabUPnP.SuspendLayout();
            this.tabCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).BeginInit();
            this.tabLog.SuspendLayout();
            this.tabHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 580);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(306, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelInfo
            // 
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(88, 17);
            this.labelInfo.Text = "Syscat Iniciado.";
            // 
            // cmdSendEmail
            // 
            this.cmdSendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSendEmail.AutoSize = true;
            this.cmdSendEmail.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdSendEmail.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cmdSendEmail.FlatAppearance.BorderSize = 0;
            this.cmdSendEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSendEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdSendEmail.Location = new System.Drawing.Point(12, 553);
            this.cmdSendEmail.Name = "cmdSendEmail";
            this.cmdSendEmail.Size = new System.Drawing.Size(103, 22);
            this.cmdSendEmail.TabIndex = 18;
            this.cmdSendEmail.Text = "Enviar correo ahora";
            this.cmdSendEmail.UseVisualStyleBackColor = false;
            this.cmdSendEmail.Click += new System.EventHandler(this.cmdSendEmail_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBackup);
            this.tabControl1.Controls.Add(this.tabChat);
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabUPnP);
            this.tabControl1.Controls.Add(this.tabCapture);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabHelp);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(306, 580);
            this.tabControl1.TabIndex = 22;
            // 
            // tabBackup
            // 
            this.tabBackup.Controls.Add(this.groupBox1);
            this.tabBackup.Location = new System.Drawing.Point(4, 22);
            this.tabBackup.Name = "tabBackup";
            this.tabBackup.Padding = new System.Windows.Forms.Padding(3);
            this.tabBackup.Size = new System.Drawing.Size(298, 554);
            this.tabBackup.TabIndex = 3;
            this.tabBackup.Text = "Backup";
            this.tabBackup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnUnzip);
            this.groupBox1.Controls.Add(this.btnLaunchNow);
            this.groupBox1.Controls.Add(this.chkCompress);
            this.groupBox1.Controls.Add(this.chkCopyHidden);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.chkOverwrite);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.listViewBackups);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnProgram);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.optWeekly);
            this.groupBox1.Controls.Add(this.optMounthly);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMinutes);
            this.groupBox1.Controls.Add(this.txtHour);
            this.groupBox1.Controls.Add(this.optDiary);
            this.groupBox1.Controls.Add(this.chkDomingo);
            this.groupBox1.Controls.Add(this.chkSabado);
            this.groupBox1.Controls.Add(this.chkViernes);
            this.groupBox1.Controls.Add(this.chkJueves);
            this.groupBox1.Controls.Add(this.chkMiercoles);
            this.groupBox1.Controls.Add(this.chkMartes);
            this.groupBox1.Controls.Add(this.chkLunes);
            this.groupBox1.Location = new System.Drawing.Point(8, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 530);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Programar copia de seguridad:";
            // 
            // btnUnzip
            // 
            this.btnUnzip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnzip.Location = new System.Drawing.Point(108, 366);
            this.btnUnzip.Name = "btnUnzip";
            this.btnUnzip.Size = new System.Drawing.Size(86, 23);
            this.btnUnzip.TabIndex = 39;
            this.btnUnzip.Text = "Descomprimir";
            this.btnUnzip.UseVisualStyleBackColor = true;
            this.btnUnzip.Click += new System.EventHandler(this.btnUnzip_Click);
            // 
            // btnLaunchNow
            // 
            this.btnLaunchNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunchNow.Location = new System.Drawing.Point(200, 343);
            this.btnLaunchNow.Name = "btnLaunchNow";
            this.btnLaunchNow.Size = new System.Drawing.Size(75, 23);
            this.btnLaunchNow.TabIndex = 38;
            this.btnLaunchNow.Text = "Ahora!";
            this.btnLaunchNow.UseVisualStyleBackColor = true;
            this.btnLaunchNow.Click += new System.EventHandler(this.btnLaunchNow_Click);
            // 
            // chkCompress
            // 
            this.chkCompress.AutoSize = true;
            this.chkCompress.Location = new System.Drawing.Point(108, 349);
            this.chkCompress.Name = "chkCompress";
            this.chkCompress.Size = new System.Drawing.Size(71, 17);
            this.chkCompress.TabIndex = 37;
            this.chkCompress.Text = "Comprimir";
            this.chkCompress.UseVisualStyleBackColor = true;
            // 
            // chkCopyHidden
            // 
            this.chkCopyHidden.AutoSize = true;
            this.chkCopyHidden.Location = new System.Drawing.Point(9, 349);
            this.chkCopyHidden.Name = "chkCopyHidden";
            this.chkCopyHidden.Size = new System.Drawing.Size(93, 17);
            this.chkCopyHidden.TabIndex = 17;
            this.chkCopyHidden.Text = "Copiar ocultos";
            this.chkCopyHidden.UseVisualStyleBackColor = true;
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.Location = new System.Drawing.Point(71, 25);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 36;
            this.txtNombre.Text = "Copia1";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(9, 372);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(87, 17);
            this.chkOverwrite.TabIndex = 16;
            this.chkOverwrite.Text = "Sobreescribir";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "Nombre:";
            // 
            // listViewBackups
            // 
            this.listViewBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBackups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewBackups.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewBackups.FullRowSelect = true;
            this.listViewBackups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewBackups.HideSelection = false;
            this.listViewBackups.Location = new System.Drawing.Point(4, 395);
            this.listViewBackups.MultiSelect = false;
            this.listViewBackups.Name = "listViewBackups";
            this.listViewBackups.Size = new System.Drawing.Size(271, 129);
            this.listViewBackups.TabIndex = 34;
            this.listViewBackups.UseCompatibleStateImageBehavior = false;
            this.listViewBackups.View = System.Windows.Forms.View.Details;
            this.listViewBackups.SelectedIndexChanged += new System.EventHandler(this.listViewBackups_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nombre";
            this.columnHeader2.Width = 178;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.borrarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 26);
            // 
            // borrarToolStripMenuItem
            // 
            this.borrarToolStripMenuItem.Name = "borrarToolStripMenuItem";
            this.borrarToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.borrarToolStripMenuItem.Text = "Borrar";
            this.borrarToolStripMenuItem.Click += new System.EventHandler(this.borrarToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnDestino);
            this.groupBox2.Controls.Add(this.btnOrigen);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDestino);
            this.groupBox2.Controls.Add(this.txtOrigen);
            this.groupBox2.Location = new System.Drawing.Point(9, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 102);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Copia de seguridad";
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(225, 72);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(24, 20);
            this.btnDestino.TabIndex = 5;
            this.btnDestino.Text = "V";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.btnDestino_Click);
            // 
            // btnOrigen
            // 
            this.btnOrigen.Location = new System.Drawing.Point(225, 32);
            this.btnOrigen.Name = "btnOrigen";
            this.btnOrigen.Size = new System.Drawing.Size(24, 20);
            this.btnOrigen.TabIndex = 4;
            this.btnOrigen.Text = "V";
            this.btnOrigen.UseVisualStyleBackColor = true;
            this.btnOrigen.Click += new System.EventHandler(this.btnOrigen_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Origen:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destino:";
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(10, 72);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(210, 20);
            this.txtDestino.TabIndex = 1;
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(10, 33);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(210, 20);
            this.txtOrigen.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(71, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 32;
            // 
            // btnProgram
            // 
            this.btnProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProgram.Location = new System.Drawing.Point(200, 366);
            this.btnProgram.Name = "btnProgram";
            this.btnProgram.Size = new System.Drawing.Size(75, 23);
            this.btnProgram.TabIndex = 9;
            this.btnProgram.Text = "Programar";
            this.btnProgram.UseVisualStyleBackColor = true;
            this.btnProgram.Click += new System.EventHandler(this.btnProgram_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 133);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 31;
            // 
            // optWeekly
            // 
            this.optWeekly.AutoSize = true;
            this.optWeekly.Location = new System.Drawing.Point(9, 160);
            this.optWeekly.Name = "optWeekly";
            this.optWeekly.Size = new System.Drawing.Size(66, 17);
            this.optWeekly.TabIndex = 30;
            this.optWeekly.Text = "Semanal";
            this.optWeekly.UseVisualStyleBackColor = true;
            // 
            // optMounthly
            // 
            this.optMounthly.AutoSize = true;
            this.optMounthly.Location = new System.Drawing.Point(9, 110);
            this.optMounthly.Name = "optMounthly";
            this.optMounthly.Size = new System.Drawing.Size(65, 17);
            this.optMounthly.TabIndex = 29;
            this.optMounthly.Text = "Mensual";
            this.optMounthly.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Hora:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Min.:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Hora de ejecucion (formato 24h):";
            // 
            // txtMinutes
            // 
            this.txtMinutes.Location = new System.Drawing.Point(127, 207);
            this.txtMinutes.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(34, 20);
            this.txtMinutes.TabIndex = 25;
            this.txtMinutes.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(40, 207);
            this.txtHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(35, 20);
            this.txtHour.TabIndex = 24;
            this.txtHour.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // optDiary
            // 
            this.optDiary.AutoSize = true;
            this.optDiary.Checked = true;
            this.optDiary.Location = new System.Drawing.Point(9, 55);
            this.optDiary.Name = "optDiary";
            this.optDiary.Size = new System.Drawing.Size(52, 17);
            this.optDiary.TabIndex = 23;
            this.optDiary.TabStop = true;
            this.optDiary.Text = "Diaria";
            this.optDiary.UseVisualStyleBackColor = true;
            this.optDiary.CheckedChanged += new System.EventHandler(this.optDiary_CheckedChanged);
            // 
            // chkDomingo
            // 
            this.chkDomingo.AutoSize = true;
            this.chkDomingo.Location = new System.Drawing.Point(237, 86);
            this.chkDomingo.Name = "chkDomingo";
            this.chkDomingo.Size = new System.Drawing.Size(34, 17);
            this.chkDomingo.TabIndex = 22;
            this.chkDomingo.Text = "D";
            this.chkDomingo.UseVisualStyleBackColor = true;
            // 
            // chkSabado
            // 
            this.chkSabado.AutoSize = true;
            this.chkSabado.Location = new System.Drawing.Point(199, 86);
            this.chkSabado.Name = "chkSabado";
            this.chkSabado.Size = new System.Drawing.Size(33, 17);
            this.chkSabado.TabIndex = 21;
            this.chkSabado.Text = "S";
            this.chkSabado.UseVisualStyleBackColor = true;
            // 
            // chkViernes
            // 
            this.chkViernes.AutoSize = true;
            this.chkViernes.Location = new System.Drawing.Point(161, 86);
            this.chkViernes.Name = "chkViernes";
            this.chkViernes.Size = new System.Drawing.Size(33, 17);
            this.chkViernes.TabIndex = 20;
            this.chkViernes.Text = "V";
            this.chkViernes.UseVisualStyleBackColor = true;
            // 
            // chkJueves
            // 
            this.chkJueves.AutoSize = true;
            this.chkJueves.Location = new System.Drawing.Point(123, 86);
            this.chkJueves.Name = "chkJueves";
            this.chkJueves.Size = new System.Drawing.Size(31, 17);
            this.chkJueves.TabIndex = 19;
            this.chkJueves.Text = "J";
            this.chkJueves.UseVisualStyleBackColor = true;
            // 
            // chkMiercoles
            // 
            this.chkMiercoles.AutoSize = true;
            this.chkMiercoles.Location = new System.Drawing.Point(85, 86);
            this.chkMiercoles.Name = "chkMiercoles";
            this.chkMiercoles.Size = new System.Drawing.Size(37, 17);
            this.chkMiercoles.TabIndex = 18;
            this.chkMiercoles.Text = "Mi";
            this.chkMiercoles.UseVisualStyleBackColor = true;
            // 
            // chkMartes
            // 
            this.chkMartes.AutoSize = true;
            this.chkMartes.Location = new System.Drawing.Point(47, 86);
            this.chkMartes.Name = "chkMartes";
            this.chkMartes.Size = new System.Drawing.Size(35, 17);
            this.chkMartes.TabIndex = 17;
            this.chkMartes.Text = "M";
            this.chkMartes.UseVisualStyleBackColor = true;
            // 
            // chkLunes
            // 
            this.chkLunes.AutoSize = true;
            this.chkLunes.Location = new System.Drawing.Point(9, 86);
            this.chkLunes.Name = "chkLunes";
            this.chkLunes.Size = new System.Drawing.Size(32, 17);
            this.chkLunes.TabIndex = 16;
            this.chkLunes.Text = "L";
            this.chkLunes.UseVisualStyleBackColor = true;
            // 
            // tabChat
            // 
            this.tabChat.Controls.Add(this.cmdActualizar);
            this.tabChat.Controls.Add(this.cmdConectarFSVNC);
            this.tabChat.Controls.Add(this.btnSend);
            this.tabChat.Controls.Add(this.tabControl2);
            this.tabChat.Controls.Add(this.txtMessage);
            this.tabChat.Controls.Add(this.listViewClients);
            this.tabChat.Controls.Add(this.cmdEnviar);
            this.tabChat.Location = new System.Drawing.Point(4, 22);
            this.tabChat.Name = "tabChat";
            this.tabChat.Size = new System.Drawing.Size(298, 554);
            this.tabChat.TabIndex = 5;
            this.tabChat.Text = "Chat";
            this.tabChat.UseVisualStyleBackColor = true;
            // 
            // cmdActualizar
            // 
            this.cmdActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdActualizar.Location = new System.Drawing.Point(8, 162);
            this.cmdActualizar.Name = "cmdActualizar";
            this.cmdActualizar.Size = new System.Drawing.Size(50, 20);
            this.cmdActualizar.TabIndex = 7;
            this.cmdActualizar.Text = "Update";
            this.cmdActualizar.UseVisualStyleBackColor = true;
            this.cmdActualizar.Click += new System.EventHandler(this.cmdActualizar_Click);
            // 
            // cmdConectarFSVNC
            // 
            this.cmdConectarFSVNC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConectarFSVNC.Location = new System.Drawing.Point(64, 162);
            this.cmdConectarFSVNC.Name = "cmdConectarFSVNC";
            this.cmdConectarFSVNC.Size = new System.Drawing.Size(110, 20);
            this.cmdConectarFSVNC.TabIndex = 6;
            this.cmdConectarFSVNC.Text = "Conectar FSVnc";
            this.cmdConectarFSVNC.UseVisualStyleBackColor = true;
            this.cmdConectarFSVNC.Click += new System.EventHandler(this.cmdConectarFSVNC_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(230, 518);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(56, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Enviar";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Location = new System.Drawing.Point(8, 188);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(282, 324);
            this.tabControl2.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtConversation);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(274, 298);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtConversation
            // 
            this.txtConversation.AcceptsReturn = true;
            this.txtConversation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConversation.Location = new System.Drawing.Point(3, 3);
            this.txtConversation.Multiline = true;
            this.txtConversation.Name = "txtConversation";
            this.txtConversation.Size = new System.Drawing.Size(268, 292);
            this.txtConversation.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Location = new System.Drawing.Point(11, 520);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(213, 20);
            this.txtMessage.TabIndex = 4;
            // 
            // listViewClients
            // 
            this.listViewClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.HideSelection = false;
            this.listViewClients.Location = new System.Drawing.Point(8, 3);
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(282, 153);
            this.listViewClients.TabIndex = 2;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nombre";
            this.columnHeader3.Width = 120;
            // 
            // cmdEnviar
            // 
            this.cmdEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnviar.Location = new System.Drawing.Point(180, 162);
            this.cmdEnviar.Name = "cmdEnviar";
            this.cmdEnviar.Size = new System.Drawing.Size(110, 20);
            this.cmdEnviar.TabIndex = 0;
            this.cmdEnviar.Text = "Enviar mensaje";
            this.cmdEnviar.UseVisualStyleBackColor = true;
            this.cmdEnviar.Click += new System.EventHandler(this.cmdEnviar_Click);
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.btnGetInfo);
            this.tabInfo.Controls.Add(this.txtSystemInfo);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(298, 554);
            this.tabInfo.TabIndex = 1;
            this.tabInfo.Text = "System Info";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(6, 6);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(116, 23);
            this.btnGetInfo.TabIndex = 1;
            this.btnGetInfo.Text = "Obtener Información";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // txtSystemInfo
            // 
            this.txtSystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSystemInfo.Location = new System.Drawing.Point(6, 35);
            this.txtSystemInfo.Multiline = true;
            this.txtSystemInfo.Name = "txtSystemInfo";
            this.txtSystemInfo.ReadOnly = true;
            this.txtSystemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystemInfo.Size = new System.Drawing.Size(286, 516);
            this.txtSystemInfo.TabIndex = 0;
            // 
            // tabUPnP
            // 
            this.tabUPnP.Controls.Add(this.button1);
            this.tabUPnP.Controls.Add(this.listView1);
            this.tabUPnP.Location = new System.Drawing.Point(4, 22);
            this.tabUPnP.Name = "tabUPnP";
            this.tabUPnP.Size = new System.Drawing.Size(298, 554);
            this.tabUPnP.TabIndex = 6;
            this.tabUPnP.Text = "UPnP";
            this.tabUPnP.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(215, 512);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Descubrir!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(8, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(282, 503);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tabCapture
            // 
            this.tabCapture.Controls.Add(this.txt_Log);
            this.tabCapture.Controls.Add(this.lblPosition);
            this.tabCapture.Controls.Add(this.txt_CurrentWindowstitle);
            this.tabCapture.Controls.Add(this.cmdNext);
            this.tabCapture.Controls.Add(this.picScreen);
            this.tabCapture.Controls.Add(this.cmdPrev);
            this.tabCapture.Location = new System.Drawing.Point(4, 22);
            this.tabCapture.Name = "tabCapture";
            this.tabCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tabCapture.Size = new System.Drawing.Size(298, 554);
            this.tabCapture.TabIndex = 0;
            this.tabCapture.Text = "Capture Data";
            this.tabCapture.UseVisualStyleBackColor = true;
            // 
            // txt_Log
            // 
            this.txt_Log.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Log.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Log.Location = new System.Drawing.Point(6, 340);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.ReadOnly = true;
            this.txt_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Log.Size = new System.Drawing.Size(282, 199);
            this.txt_Log.TabIndex = 22;
            // 
            // lblPosition
            // 
            this.lblPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.Location = new System.Drawing.Point(243, 14);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(33, 9);
            this.lblPosition.TabIndex = 27;
            this.lblPosition.Text = "1/1";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_CurrentWindowstitle
            // 
            this.txt_CurrentWindowstitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CurrentWindowstitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CurrentWindowstitle.Location = new System.Drawing.Point(6, 7);
            this.txt_CurrentWindowstitle.Name = "txt_CurrentWindowstitle";
            this.txt_CurrentWindowstitle.ReadOnly = true;
            this.txt_CurrentWindowstitle.Size = new System.Drawing.Size(218, 22);
            this.txt_CurrentWindowstitle.TabIndex = 23;
            this.txt_CurrentWindowstitle.Text = "Current Window Title";
            // 
            // cmdNext
            // 
            this.cmdNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNext.Location = new System.Drawing.Point(275, 7);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(17, 22);
            this.cmdNext.TabIndex = 26;
            this.cmdNext.Text = ">";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // picScreen
            // 
            this.picScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picScreen.Location = new System.Drawing.Point(6, 35);
            this.picScreen.Name = "picScreen";
            this.picScreen.Size = new System.Drawing.Size(282, 299);
            this.picScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScreen.TabIndex = 24;
            this.picScreen.TabStop = false;
            // 
            // cmdPrev
            // 
            this.cmdPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrev.Location = new System.Drawing.Point(227, 7);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(17, 22);
            this.cmdPrev.TabIndex = 25;
            this.cmdPrev.Text = "<";
            this.cmdPrev.UseVisualStyleBackColor = true;
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrev_Click);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.listViewLog);
            this.tabLog.Controls.Add(this.cmdLimpiar);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(298, 554);
            this.tabLog.TabIndex = 4;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colTipo,
            this.colMessage});
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(9, 32);
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(279, 508);
            this.listViewLog.TabIndex = 2;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Fecha";
            this.colDate.Width = 117;
            // 
            // colTipo
            // 
            this.colTipo.Text = "Tipo";
            // 
            // colMessage
            // 
            this.colMessage.Text = "Mensaje";
            this.colMessage.Width = 600;
            // 
            // cmdLimpiar
            // 
            this.cmdLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLimpiar.Location = new System.Drawing.Point(213, 5);
            this.cmdLimpiar.Name = "cmdLimpiar";
            this.cmdLimpiar.Size = new System.Drawing.Size(75, 23);
            this.cmdLimpiar.TabIndex = 1;
            this.cmdLimpiar.Text = "Limpiar";
            this.cmdLimpiar.UseVisualStyleBackColor = true;
            this.cmdLimpiar.Click += new System.EventHandler(this.cmdLimpiar_Click);
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.btnHideProcess);
            this.tabHelp.Controls.Add(this.lstProcess);
            this.tabHelp.Controls.Add(this.btnShowProcess);
            this.tabHelp.Controls.Add(this.btnProcess);
            this.tabHelp.Controls.Add(this.textBox1);
            this.tabHelp.Location = new System.Drawing.Point(4, 22);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Size = new System.Drawing.Size(298, 554);
            this.tabHelp.TabIndex = 2;
            this.tabHelp.Text = "Help";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // btnHideProcess
            // 
            this.btnHideProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideProcess.Location = new System.Drawing.Point(215, 70);
            this.btnHideProcess.Name = "btnHideProcess";
            this.btnHideProcess.Size = new System.Drawing.Size(75, 23);
            this.btnHideProcess.TabIndex = 5;
            this.btnHideProcess.Text = "Ocultar";
            this.btnHideProcess.UseVisualStyleBackColor = true;
            this.btnHideProcess.Click += new System.EventHandler(this.btnHideProcess_Click);
            // 
            // lstProcess
            // 
            this.lstProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProcess.FormattingEnabled = true;
            this.lstProcess.Location = new System.Drawing.Point(8, 12);
            this.lstProcess.Name = "lstProcess";
            this.lstProcess.Size = new System.Drawing.Size(201, 407);
            this.lstProcess.TabIndex = 4;
            // 
            // btnShowProcess
            // 
            this.btnShowProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowProcess.Location = new System.Drawing.Point(215, 41);
            this.btnShowProcess.Name = "btnShowProcess";
            this.btnShowProcess.Size = new System.Drawing.Size(75, 23);
            this.btnShowProcess.TabIndex = 3;
            this.btnShowProcess.Text = "Mostrar";
            this.btnShowProcess.UseVisualStyleBackColor = true;
            this.btnShowProcess.Click += new System.EventHandler(this.btnShowProcess_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(215, 12);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Procesos";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(0, 430);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(298, 124);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Para ocultar la ventana activa: \r\nCTR + SHIFT + ALT + F10\r\nPara mostrar todas las" +
    " ventanas ocultas: \r\nCTR + SHIFT + ALT + F11\r\nPara mostrar la aplicación: \r\nCTR " +
    "+ SHIFT + ALT + F12";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(306, 602);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdSendEmail);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(322, 641);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Syscat v2.0] ";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabBackup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).EndInit();
            this.tabChat.ResumeLayout(false);
            this.tabChat.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.tabUPnP.ResumeLayout(false);
            this.tabCapture.ResumeLayout(false);
            this.tabCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreen)).EndInit();
            this.tabLog.ResumeLayout(false);
            this.tabHelp.ResumeLayout(false);
            this.tabHelp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private MainMenu main_Menu;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel labelInfo;
        private Button cmdSendEmail;
        private TabControl tabControl1;
        private TabPage tabCapture;
        private TextBox txt_Log;
        private Label lblPosition;
        private TextBox txt_CurrentWindowstitle;
        private Button cmdNext;
        private PictureBox picScreen;
        private Button cmdPrev;
        private TabPage tabInfo;
        private TextBox txtSystemInfo;
        private TabPage tabHelp;
        private TextBox textBox1;
        private TabPage tabBackup;
        private Button btnProgram;
        private GroupBox groupBox1;
        private ComboBox comboBox1;
        private RadioButton optWeekly;
        private RadioButton optMounthly;
        private Label label4;
        private Label label3;
        private Label label2;
        private NumericUpDown txtMinutes;
        private NumericUpDown txtHour;
        private RadioButton optDiary;
        private CheckBox chkDomingo;
        private CheckBox chkSabado;
        private CheckBox chkViernes;
        private CheckBox chkJueves;
        private CheckBox chkMiercoles;
        private CheckBox chkMartes;
        private CheckBox chkLunes;
        private DateTimePicker dateTimePicker1;
        private GroupBox groupBox2;
        private Button btnDestino;
        private Button btnOrigen;
        private Label label5;
        private Label label1;
        private TextBox txtDestino;
        private TextBox txtOrigen;
        private TabPage tabLog;
        private Button cmdLimpiar;
        private CheckBox chkOverwrite;
        private CheckBox chkCopyHidden;
        private ListView listViewBackups;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem borrarToolStripMenuItem;
        private TextBox txtNombre;
        private Label label6;
        private ListView listViewLog;
        private ColumnHeader colDate;
        private ColumnHeader colMessage;
        private CheckBox chkCompress;
        private Button btnLaunchNow;
        private ListBox lstProcess;
        private Button btnShowProcess;
        private Button btnProcess;
        private Button btnHideProcess;
        private Button btnGetInfo;
        private Button btnUnzip;
        private TabPage tabChat;
        private ListView listViewClients;
        private Button cmdEnviar;
        private TabPage tabUPnP;
        private Button button1;
        private ListView listView1;
        private ColumnHeader columnHeader3;
        private TabControl tabControl2;
        private TabPage tabPage1;
        private TextBox txtConversation;
        private Button btnSend;
        public TextBox txtMessage;
        private Button cmdConectarFSVNC;
        private Button cmdActualizar;
        private ColumnHeader colTipo;
    }
}

