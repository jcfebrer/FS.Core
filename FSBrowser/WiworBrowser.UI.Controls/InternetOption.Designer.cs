namespace WiworBrowser.UI.Controls
{
    partial class InternetOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternetOption));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.num = new System.Windows.Forms.NumericUpDown();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdBackground = new System.Windows.Forms.Button();
            this.cmdFont = new System.Windows.Forms.Button();
            this.cmdColor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.homepage = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdUseBlankPage = new System.Windows.Forms.Button();
            this.cmdUseCurrent = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.cmdCheckVersion = new System.Windows.Forms.Button();
            this.btnDefaultValues = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkPasswordOnExit = new System.Windows.Forms.CheckBox();
            this.chkRemoveHistory = new System.Windows.Forms.CheckBox();
            this.chkWebBrowserShortcutsEnabled = new System.Windows.Forms.CheckBox();
            this.chkAllowImageExternalLinks = new System.Windows.Forms.CheckBox();
            this.chkRemoveContextMenu = new System.Windows.Forms.CheckBox();
            this.chkAllowIFrames = new System.Windows.Forms.CheckBox();
            this.chkScriptErrorsSuppressed = new System.Windows.Forms.CheckBox();
            this.chkAllowFileDownload = new System.Windows.Forms.CheckBox();
            this.chkAllowNewWindow = new System.Windows.Forms.CheckBox();
            this.chkCheckIsChildValidPage = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmdDeleteUrl = new System.Windows.Forms.Button();
            this.txtNewUrl = new System.Windows.Forms.TextBox();
            this.cmdNewUrl = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lstBlackList = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkCheckContent = new System.Windows.Forms.CheckBox();
            this.cmdDeleteBadWord = new System.Windows.Forms.Button();
            this.txtNewWord = new System.Windows.Forms.TextBox();
            this.cmdNewBadWord = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lstBadWords = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(295, 347);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.cmdOK);
            this.tabPage1.Controls.Add(this.cmdCancel);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(287, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.num);
            this.groupBox4.Location = new System.Drawing.Point(6, 237);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 49);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Direcciones combo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Número de urls en combo";
            // 
            // num
            // 
            this.num.Location = new System.Drawing.Point(156, 19);
            this.num.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num.Name = "num";
            this.num.Size = new System.Drawing.Size(105, 20);
            this.num.TabIndex = 0;
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(115, 292);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 11;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(198, 292);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "Cancelar";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdBackground);
            this.groupBox3.Controls.Add(this.cmdFont);
            this.groupBox3.Controls.Add(this.cmdColor);
            this.groupBox3.Location = new System.Drawing.Point(8, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 70);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Apariencia";
            // 
            // cmdBackground
            // 
            this.cmdBackground.Location = new System.Drawing.Point(98, 25);
            this.cmdBackground.Name = "cmdBackground";
            this.cmdBackground.Size = new System.Drawing.Size(75, 23);
            this.cmdBackground.TabIndex = 14;
            this.cmdBackground.Text = "Color fondo";
            this.cmdBackground.UseVisualStyleBackColor = true;
            this.cmdBackground.Click += new System.EventHandler(this.button7_Click);
            // 
            // cmdFont
            // 
            this.cmdFont.Location = new System.Drawing.Point(184, 25);
            this.cmdFont.Name = "cmdFont";
            this.cmdFont.Size = new System.Drawing.Size(75, 23);
            this.cmdFont.TabIndex = 12;
            this.cmdFont.Text = "Fuentes";
            this.cmdFont.UseVisualStyleBackColor = true;
            this.cmdFont.Click += new System.EventHandler(this.button5_Click);
            // 
            // cmdColor
            // 
            this.cmdColor.Location = new System.Drawing.Point(6, 25);
            this.cmdColor.Name = "cmdColor";
            this.cmdColor.Size = new System.Drawing.Size(75, 23);
            this.cmdColor.TabIndex = 13;
            this.cmdColor.Text = "Color";
            this.cmdColor.UseVisualStyleBackColor = true;
            this.cmdColor.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Location = new System.Drawing.Point(6, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 46);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Password de acceso (opciones / salir aplicación)";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(20, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(226, 20);
            this.txtPassword.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.homepage);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cmdUseBlankPage);
            this.groupBox1.Controls.Add(this.cmdUseCurrent);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 87);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Página principal";
            // 
            // homepage
            // 
            this.homepage.Location = new System.Drawing.Point(46, 20);
            this.homepage.Name = "homepage";
            this.homepage.Size = new System.Drawing.Size(219, 20);
            this.homepage.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 30);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // cmdUseBlankPage
            // 
            this.cmdUseBlankPage.Location = new System.Drawing.Point(142, 46);
            this.cmdUseBlankPage.Name = "cmdUseBlankPage";
            this.cmdUseBlankPage.Size = new System.Drawing.Size(123, 23);
            this.cmdUseBlankPage.TabIndex = 4;
            this.cmdUseBlankPage.Text = "Usar página en blanco";
            this.cmdUseBlankPage.UseVisualStyleBackColor = true;
            this.cmdUseBlankPage.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdUseCurrent
            // 
            this.cmdUseCurrent.Location = new System.Drawing.Point(61, 46);
            this.cmdUseCurrent.Name = "cmdUseCurrent";
            this.cmdUseCurrent.Size = new System.Drawing.Size(75, 23);
            this.cmdUseCurrent.TabIndex = 3;
            this.cmdUseCurrent.Text = "Usar actual";
            this.cmdUseCurrent.UseVisualStyleBackColor = true;
            this.cmdUseCurrent.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.cmdCheckVersion);
            this.tabPage5.Controls.Add(this.btnDefaultValues);
            this.tabPage5.Controls.Add(this.groupBox5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(287, 321);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Avanzado";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // cmdCheckVersion
            // 
            this.cmdCheckVersion.Location = new System.Drawing.Point(8, 290);
            this.cmdCheckVersion.Name = "cmdCheckVersion";
            this.cmdCheckVersion.Size = new System.Drawing.Size(116, 23);
            this.cmdCheckVersion.TabIndex = 11;
            this.cmdCheckVersion.Text = "Comprobar versión";
            this.cmdCheckVersion.UseVisualStyleBackColor = true;
            this.cmdCheckVersion.Click += new System.EventHandler(this.cmdCheckVersion_Click);
            // 
            // btnDefaultValues
            // 
            this.btnDefaultValues.Location = new System.Drawing.Point(146, 290);
            this.btnDefaultValues.Name = "btnDefaultValues";
            this.btnDefaultValues.Size = new System.Drawing.Size(127, 23);
            this.btnDefaultValues.TabIndex = 10;
            this.btnDefaultValues.Text = "Valores por defecto";
            this.btnDefaultValues.UseVisualStyleBackColor = true;
            this.btnDefaultValues.Click += new System.EventHandler(this.btnDefaultValues_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkPasswordOnExit);
            this.groupBox5.Controls.Add(this.chkRemoveHistory);
            this.groupBox5.Controls.Add(this.chkWebBrowserShortcutsEnabled);
            this.groupBox5.Controls.Add(this.chkAllowImageExternalLinks);
            this.groupBox5.Controls.Add(this.chkRemoveContextMenu);
            this.groupBox5.Controls.Add(this.chkAllowIFrames);
            this.groupBox5.Controls.Add(this.chkScriptErrorsSuppressed);
            this.groupBox5.Controls.Add(this.chkAllowFileDownload);
            this.groupBox5.Controls.Add(this.chkAllowNewWindow);
            this.groupBox5.Controls.Add(this.chkCheckIsChildValidPage);
            this.groupBox5.Location = new System.Drawing.Point(8, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(265, 278);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Configuración navegador";
            // 
            // chkPasswordOnExit
            // 
            this.chkPasswordOnExit.AutoSize = true;
            this.chkPasswordOnExit.Location = new System.Drawing.Point(6, 230);
            this.chkPasswordOnExit.Name = "chkPasswordOnExit";
            this.chkPasswordOnExit.Size = new System.Drawing.Size(214, 17);
            this.chkPasswordOnExit.TabIndex = 9;
            this.chkPasswordOnExit.Text = "Pedir password para cerrar la aplicación";
            this.chkPasswordOnExit.UseVisualStyleBackColor = true;
            // 
            // chkRemoveHistory
            // 
            this.chkRemoveHistory.AutoSize = true;
            this.chkRemoveHistory.Location = new System.Drawing.Point(6, 207);
            this.chkRemoveHistory.Name = "chkRemoveHistory";
            this.chkRemoveHistory.Size = new System.Drawing.Size(174, 17);
            this.chkRemoveHistory.TabIndex = 8;
            this.chkRemoveHistory.Text = "Eliminar historial de navegación";
            this.chkRemoveHistory.UseVisualStyleBackColor = true;
            // 
            // chkWebBrowserShortcutsEnabled
            // 
            this.chkWebBrowserShortcutsEnabled.AutoSize = true;
            this.chkWebBrowserShortcutsEnabled.Location = new System.Drawing.Point(6, 184);
            this.chkWebBrowserShortcutsEnabled.Name = "chkWebBrowserShortcutsEnabled";
            this.chkWebBrowserShortcutsEnabled.Size = new System.Drawing.Size(162, 17);
            this.chkWebBrowserShortcutsEnabled.TabIndex = 7;
            this.chkWebBrowserShortcutsEnabled.Text = "Permitir atajos del navegador";
            this.chkWebBrowserShortcutsEnabled.UseVisualStyleBackColor = true;
            // 
            // chkAllowImageExternalLinks
            // 
            this.chkAllowImageExternalLinks.AutoSize = true;
            this.chkAllowImageExternalLinks.Location = new System.Drawing.Point(6, 161);
            this.chkAllowImageExternalLinks.Name = "chkAllowImageExternalLinks";
            this.chkAllowImageExternalLinks.Size = new System.Drawing.Size(212, 17);
            this.chkAllowImageExternalLinks.TabIndex = 6;
            this.chkAllowImageExternalLinks.Text = "Permitir imagenes con enlaces externos";
            this.chkAllowImageExternalLinks.UseVisualStyleBackColor = true;
            // 
            // chkRemoveContextMenu
            // 
            this.chkRemoveContextMenu.AutoSize = true;
            this.chkRemoveContextMenu.Location = new System.Drawing.Point(6, 138);
            this.chkRemoveContextMenu.Name = "chkRemoveContextMenu";
            this.chkRemoveContextMenu.Size = new System.Drawing.Size(143, 17);
            this.chkRemoveContextMenu.TabIndex = 5;
            this.chkRemoveContextMenu.Text = "Eliminar menu contextual";
            this.chkRemoveContextMenu.UseVisualStyleBackColor = true;
            // 
            // chkAllowIFrames
            // 
            this.chkAllowIFrames.AutoSize = true;
            this.chkAllowIFrames.Location = new System.Drawing.Point(6, 92);
            this.chkAllowIFrames.Name = "chkAllowIFrames";
            this.chkAllowIFrames.Size = new System.Drawing.Size(110, 17);
            this.chkAllowIFrames.TabIndex = 3;
            this.chkAllowIFrames.Text = "Permitir IFRAMES";
            this.chkAllowIFrames.UseVisualStyleBackColor = true;
            // 
            // chkScriptErrorsSuppressed
            // 
            this.chkScriptErrorsSuppressed.AutoSize = true;
            this.chkScriptErrorsSuppressed.Location = new System.Drawing.Point(6, 69);
            this.chkScriptErrorsSuppressed.Name = "chkScriptErrorsSuppressed";
            this.chkScriptErrorsSuppressed.Size = new System.Drawing.Size(146, 17);
            this.chkScriptErrorsSuppressed.TabIndex = 2;
            this.chkScriptErrorsSuppressed.Text = "Suprimir errores de scripts";
            this.chkScriptErrorsSuppressed.UseVisualStyleBackColor = true;
            // 
            // chkAllowFileDownload
            // 
            this.chkAllowFileDownload.AutoSize = true;
            this.chkAllowFileDownload.Location = new System.Drawing.Point(6, 46);
            this.chkAllowFileDownload.Name = "chkAllowFileDownload";
            this.chkAllowFileDownload.Size = new System.Drawing.Size(162, 17);
            this.chkAllowFileDownload.TabIndex = 1;
            this.chkAllowFileDownload.Text = "Permitir descarga de ficheros";
            this.chkAllowFileDownload.UseVisualStyleBackColor = true;
            // 
            // chkAllowNewWindow
            // 
            this.chkAllowNewWindow.AutoSize = true;
            this.chkAllowNewWindow.Location = new System.Drawing.Point(6, 23);
            this.chkAllowNewWindow.Name = "chkAllowNewWindow";
            this.chkAllowNewWindow.Size = new System.Drawing.Size(145, 17);
            this.chkAllowNewWindow.TabIndex = 0;
            this.chkAllowNewWindow.Text = "Permitir nuevas ventanas";
            this.chkAllowNewWindow.UseVisualStyleBackColor = true;
            // 
            // chkCheckIsChildValidPage
            // 
            this.chkCheckIsChildValidPage.AutoSize = true;
            this.chkCheckIsChildValidPage.Location = new System.Drawing.Point(6, 115);
            this.chkCheckIsChildValidPage.Name = "chkCheckIsChildValidPage";
            this.chkCheckIsChildValidPage.Size = new System.Drawing.Size(252, 17);
            this.chkCheckIsChildValidPage.TabIndex = 4;
            this.chkCheckIsChildValidPage.Text = "Comprobación de páginas validas para menores";
            this.chkCheckIsChildValidPage.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmdDeleteUrl);
            this.tabPage3.Controls.Add(this.txtNewUrl);
            this.tabPage3.Controls.Add(this.cmdNewUrl);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(287, 321);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Url no validadas";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cmdDeleteUrl
            // 
            this.cmdDeleteUrl.Location = new System.Drawing.Point(118, 248);
            this.cmdDeleteUrl.Name = "cmdDeleteUrl";
            this.cmdDeleteUrl.Size = new System.Drawing.Size(155, 23);
            this.cmdDeleteUrl.TabIndex = 16;
            this.cmdDeleteUrl.Text = "Borrar seleccionada";
            this.cmdDeleteUrl.UseVisualStyleBackColor = true;
            this.cmdDeleteUrl.Click += new System.EventHandler(this.cmdDeleteUrl_Click);
            // 
            // txtNewUrl
            // 
            this.txtNewUrl.Location = new System.Drawing.Point(10, 219);
            this.txtNewUrl.Name = "txtNewUrl";
            this.txtNewUrl.Size = new System.Drawing.Size(162, 20);
            this.txtNewUrl.TabIndex = 15;
            // 
            // cmdNewUrl
            // 
            this.cmdNewUrl.Location = new System.Drawing.Point(178, 219);
            this.cmdNewUrl.Name = "cmdNewUrl";
            this.cmdNewUrl.Size = new System.Drawing.Size(95, 23);
            this.cmdNewUrl.TabIndex = 14;
            this.cmdNewUrl.Text = "Añadir nueva";
            this.cmdNewUrl.UseVisualStyleBackColor = true;
            this.cmdNewUrl.Click += new System.EventHandler(this.cmdNewUrl_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lstBlackList);
            this.groupBox7.Location = new System.Drawing.Point(8, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(265, 207);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Listado url no validas";
            // 
            // lstBlackList
            // 
            this.lstBlackList.FormattingEnabled = true;
            this.lstBlackList.Location = new System.Drawing.Point(6, 19);
            this.lstBlackList.Name = "lstBlackList";
            this.lstBlackList.Size = new System.Drawing.Size(244, 173);
            this.lstBlackList.Sorted = true;
            this.lstBlackList.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkCheckContent);
            this.tabPage4.Controls.Add(this.cmdDeleteBadWord);
            this.tabPage4.Controls.Add(this.txtNewWord);
            this.tabPage4.Controls.Add(this.cmdNewBadWord);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(287, 321);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Palabras no adecuadas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkCheckContent
            // 
            this.chkCheckContent.AutoSize = true;
            this.chkCheckContent.Location = new System.Drawing.Point(14, 286);
            this.chkCheckContent.Name = "chkCheckContent";
            this.chkCheckContent.Size = new System.Drawing.Size(219, 17);
            this.chkCheckContent.TabIndex = 13;
            this.chkCheckContent.Text = "Realizar análisis de contenido por página";
            this.chkCheckContent.UseVisualStyleBackColor = true;
            // 
            // cmdDeleteBadWord
            // 
            this.cmdDeleteBadWord.Location = new System.Drawing.Point(118, 252);
            this.cmdDeleteBadWord.Name = "cmdDeleteBadWord";
            this.cmdDeleteBadWord.Size = new System.Drawing.Size(155, 23);
            this.cmdDeleteBadWord.TabIndex = 12;
            this.cmdDeleteBadWord.Text = "Borrar seleccionada";
            this.cmdDeleteBadWord.UseVisualStyleBackColor = true;
            this.cmdDeleteBadWord.Click += new System.EventHandler(this.cmdDeleteBadWord_Click);
            // 
            // txtNewWord
            // 
            this.txtNewWord.Location = new System.Drawing.Point(10, 223);
            this.txtNewWord.Name = "txtNewWord";
            this.txtNewWord.Size = new System.Drawing.Size(162, 20);
            this.txtNewWord.TabIndex = 11;
            // 
            // cmdNewBadWord
            // 
            this.cmdNewBadWord.Location = new System.Drawing.Point(178, 223);
            this.cmdNewBadWord.Name = "cmdNewBadWord";
            this.cmdNewBadWord.Size = new System.Drawing.Size(95, 23);
            this.cmdNewBadWord.TabIndex = 10;
            this.cmdNewBadWord.Text = "Añadir nueva";
            this.cmdNewBadWord.UseVisualStyleBackColor = true;
            this.cmdNewBadWord.Click += new System.EventHandler(this.cmdNewBadWord_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lstBadWords);
            this.groupBox6.Location = new System.Drawing.Point(8, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(265, 207);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Listado";
            // 
            // lstBadWords
            // 
            this.lstBadWords.FormattingEnabled = true;
            this.lstBadWords.Location = new System.Drawing.Point(6, 19);
            this.lstBadWords.Name = "lstBadWords";
            this.lstBadWords.Size = new System.Drawing.Size(244, 173);
            this.lstBadWords.Sorted = true;
            this.lstBadWords.TabIndex = 1;
            // 
            // InternetOption
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 347);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InternetOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opciones";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdUseBlankPage;
        private System.Windows.Forms.Button cmdUseCurrent;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.TextBox homepage;
        private System.Windows.Forms.Button cmdColor;
        private System.Windows.Forms.Button cmdFont;
        private System.Windows.Forms.Button cmdBackground;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown num;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.CheckBox chkRemoveHistory;
        public System.Windows.Forms.CheckBox chkWebBrowserShortcutsEnabled;
        public System.Windows.Forms.CheckBox chkAllowImageExternalLinks;
        public System.Windows.Forms.CheckBox chkRemoveContextMenu;
        public System.Windows.Forms.CheckBox chkAllowIFrames;
        public System.Windows.Forms.CheckBox chkScriptErrorsSuppressed;
        public System.Windows.Forms.CheckBox chkAllowFileDownload;
        public System.Windows.Forms.CheckBox chkAllowNewWindow;
        private System.Windows.Forms.Button btnDefaultValues;
        private System.Windows.Forms.Button cmdNewBadWord;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox lstBadWords;
        private System.Windows.Forms.TextBox txtNewWord;
        private System.Windows.Forms.Button cmdDeleteBadWord;
        private System.Windows.Forms.CheckBox chkCheckContent;
        private System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.CheckBox chkCheckIsChildValidPage;
        public System.Windows.Forms.CheckBox chkPasswordOnExit;
        private System.Windows.Forms.Button cmdDeleteUrl;
        private System.Windows.Forms.TextBox txtNewUrl;
        private System.Windows.Forms.Button cmdNewUrl;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox lstBlackList;
        private System.Windows.Forms.Button cmdCheckVersion;
    }
}