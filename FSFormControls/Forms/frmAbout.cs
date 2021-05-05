#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class frmAbout : Form
    {
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.febrersoftware.com");
        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frmAbout_Load(object sender, EventArgs e)
        {
            string[] fileNames = null;

            try
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                fileNames = Directory.GetFiles(assemblyPath);

                foreach (var fileName in fileNames)
                {
                    ListViewItem item = null;
                    item = lstAssemblies.Items.Add(fileName);

                    if (fileName.EndsWith(".dll"))
                        item.SubItems.Add(Assembly.LoadFrom(fileName).GetName().Version.ToString());
                }

                FillEnvironment();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void FillEnvironment()
        {
            var transTemp1 = ListView1;
            transTemp1.View = View.Details;
            transTemp1.FullRowSelect = true;
            transTemp1.GridLines = true;
            transTemp1.LabelEdit = false;
            transTemp1.Columns.Clear();
            transTemp1.Columns.Add("Propiedad", 170, HorizontalAlignment.Left);
            transTemp1.Columns.Add("Valor", 300, HorizontalAlignment.Left);


            ListViewItem lvItem = null;
            var i = 0;
            lvItem = ListView1.Items.Add("CommandLine");
            lvItem.SubItems.Add(Environment.CommandLine);
            lvItem = ListView1.Items.Add("CurrentDirectory");
            lvItem.SubItems.Add(Environment.CurrentDirectory);
            lvItem = ListView1.Items.Add("GetCommandLineArgs.Length");
            lvItem.SubItems.Add(Environment.GetCommandLineArgs().Length.ToString());
            for (i = 0; i <= Environment.GetCommandLineArgs().Length - 1; i++)
            {
                lvItem = ListView1.Items.Add("GetCommandLineArgs(" + i + ")");
                lvItem.SubItems.Add(Environment.GetCommandLineArgs()[i]);
            }

            lvItem = ListView1.Items.Add("GetEnvironmentVariables.App.Count");
            lvItem.SubItems.Add(Environment.GetEnvironmentVariables().Count.ToString());
            foreach (DictionaryEntry tDE in Environment.GetEnvironmentVariables())
            {
                lvItem = ListView1.Items.Add("...(" + tDE.Key + ")");
                lvItem.SubItems.Add(tDE.Value.ToString());
            }

            lvItem = ListView1.Items.Add("GetFolderPath(SpecialFolder. ...)");
            lvItem.SubItems.Add(" ");
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.ApplicationData + ")");
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.CommonProgramFiles);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Cookies);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.DesktopDirectory);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Favorites);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.History);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.History));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.InternetCache);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.LocalApplicationData);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Personal);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.ProgramFiles);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Programs);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Programs));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Recent);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Recent));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.SendTo);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.StartMenu);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Startup);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.System);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.System));
            lvItem = ListView1.Items.Add("..." + Environment.SpecialFolder.Templates);
            lvItem.SubItems.Add(Environment.GetFolderPath(Environment.SpecialFolder.Templates));
            lvItem = ListView1.Items.Add("GetLogicalDrives.Length");
            lvItem.SubItems.Add(Environment.GetLogicalDrives().Length.ToString());
            for (i = 0; i <= Environment.GetLogicalDrives().Length - 1; i++)
            {
                lvItem = ListView1.Items.Add("GetLogicalDrives(" + i + ")");
                lvItem.SubItems.Add(Environment.GetLogicalDrives()[i]);
            }

            lvItem = ListView1.Items.Add("MachineName");
            lvItem.SubItems.Add(Environment.MachineName);

            lvItem = ListView1.Items.Add("OSVersion.Platform");
            lvItem.SubItems.Add(Environment.OSVersion.Platform.ToString());

            lvItem = ListView1.Items.Add("OSVersion.Version");
            lvItem.SubItems.Add(Environment.OSVersion.Version.ToString());

            lvItem = ListView1.Items.Add("SystemDirectory");
            lvItem.SubItems.Add(Environment.SystemDirectory);

            lvItem = ListView1.Items.Add("TickCount");
            lvItem.SubItems.Add(Environment.TickCount.ToString("###,###"));

            lvItem = ListView1.Items.Add("UserDomainName");
            lvItem.SubItems.Add(Environment.UserDomainName);

            lvItem = ListView1.Items.Add("UserName");
            lvItem.SubItems.Add(Environment.UserName);

            lvItem = ListView1.Items.Add("UserInteractive");
            lvItem.SubItems.Add(Environment.UserInteractive.ToString());

            lvItem = ListView1.Items.Add("CLR Version");
            lvItem.SubItems.Add(Environment.Version.ToString());

            lvItem = ListView1.Items.Add("WorkingSet (memoria asignada)");
            lvItem.SubItems.Add(Environment.WorkingSet.ToString("###,###"));

            if (Global.ConnectionStringSetting != null)
            {
                lvItem = ListView1.Items.Add("ConnectionString");
                lvItem.SubItems.Add(Global.ConnectionStringSetting.ConnectionString);

                lvItem = ListView1.Items.Add("ProviderName");
                lvItem.SubItems.Add(Global.ConnectionStringSetting.ProviderName);
            }

            lvItem = ListView1.Items.Add("MDAC Version");
            lvItem.SubItems.Add(Utils.GetMdacVersion().ToString());

            lvItem = ListView1.Items.Add("Permisos registro");
            lvItem.SubItems.Add(Functions.Permissions());

            lvItem = ListView1.Items.Add("StackTrace...");
            foreach (var s in Environment.StackTrace.Split(char.Parse(Global.Lf)))
            {
                lvItem.SubItems.Add(s);
            }
        }


        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.febrersoftware.com");
        }


        private void cmdInfo_Click(object sender, EventArgs e)
        {
            Process.Start("msinfo32.exe");
        }


        private void cmdEntorno_Click(object sender, EventArgs e)
        {
            switch (cmdEntorno.Text)
            {
                case "Entorno":
                    cmdEntorno.Text = "Dll's";
                    ListView1.BringToFront();
                    break;
                case "Dll's":
                    cmdEntorno.Text = "Entorno";
                    lstAssemblies.BringToFront();
                    break;
            }
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        internal ColumnHeader ColumnHeader1;
        internal ColumnHeader ColumnHeader2;

        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal LinkLabel LinkLabel1;
        internal ListView ListView1;
        internal PictureBox PictureBox1;
        internal PictureBox PictureBox2;
        internal Button cmdClose;
        internal Button cmdEntorno;
        internal Button cmdInfo;
        internal ColumnHeader colName;
        internal ColumnHeader colVersion;
        internal ListView lstAssemblies;

        public frmAbout()
        {
            InitializeComponent();

            LinkLabel1.TabStop = false;

            PictureBox2.Click += PictureBox2_Click;
            LinkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            cmdClose.Click += cmdClose_Click;
            cmdInfo.Click += cmdInfo_Click;
            cmdEntorno.Click += cmdEntorno_Click;
            Load += frmAbout_Load;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof(frmAbout));
            PictureBox2 = new PictureBox();
            Label2 = new Label();
            LinkLabel1 = new LinkLabel();
            Label1 = new Label();
            Label4 = new Label();
            Label5 = new Label();
            cmdClose = new Button();
            lstAssemblies = new ListView();
            colName = new ColumnHeader();
            colVersion = new ColumnHeader();
            cmdInfo = new Button();
            ColumnHeader1 = new ColumnHeader();
            ColumnHeader2 = new ColumnHeader();
            ListView1 = new ListView();
            cmdEntorno = new Button();
            PictureBox1 = new PictureBox();
            Label3 = new Label();
            ((ISupportInitialize) PictureBox2).BeginInit();
            ((ISupportInitialize) PictureBox1).BeginInit();
            SuspendLayout();
            PictureBox2.Cursor = Cursors.Hand;
            PictureBox2.Enabled = false;
            PictureBox2.ForeColor = Color.Black;
            PictureBox2.Image = (Image) resources.GetObject("PictureBox2.Image");
            PictureBox2.Location = new Point(16, 8);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(240, 63);
            PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox2.TabIndex = 8;
            PictureBox2.TabStop = false;
            Label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Label2.AutoSize = true;
            Label2.BackColor = Color.White;
            Label2.FlatStyle = FlatStyle.Flat;
            Label2.Font = new Font("Microsoft Sans Serif", 8.25F);
            Label2.Location = new Point(16, 309);
            Label2.Name = "Label2";
            Label2.Size = new Size(156, 13);
            Label2.TabIndex = 11;
            Label2.Text = "juancarlos@febrersoftware.com";
            LinkLabel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LinkLabel1.AutoSize = true;
            LinkLabel1.BackColor = Color.White;
            LinkLabel1.Location = new Point(16, 293);
            LinkLabel1.Name = "LinkLabel1";
            LinkLabel1.Size = new Size(155, 13);
            LinkLabel1.TabIndex = 9;
            LinkLabel1.TabStop = true;
            LinkLabel1.Text = "http://www.febrersoftware.com";
            Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label1.AutoSize = true;
            Label1.Font = new Font("Arial Narrow", 8.25F);
            Label1.Location = new Point(313, 40);
            Label1.Name = "Label1";
            Label1.Size = new Size(98, 15);
            Label1.TabIndex = 14;
            Label1.Text = "Bº Marusas Nº5 Lonja";
            Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label4.AutoSize = true;
            Label4.Font = new Font("Arial Narrow", 8.25F);
            Label4.Location = new Point(305, 56);
            Label4.Name = "Label4";
            Label4.Size = new Size(113, 15);
            Label4.TabIndex = 15;
            Label4.Text = "48610 - Urduliz (Vizcaya)";
            Label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label5.AutoSize = true;
            Label5.Font = new Font("Arial Narrow", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label5.Location = new Point(297, 24);
            Label5.Name = "Label5";
            Label5.Size = new Size(133, 15);
            Label5.TabIndex = 16;
            Label5.Text = "Tfno: 94 6764972 - 629237109";
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.BackColor = SystemColors.Control;
            cmdClose.Location = new Point(479, 293);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(72, 24);
            cmdClose.TabIndex = 17;
            cmdClose.Text = "Cerrar";
            cmdClose.UseVisualStyleBackColor = false;
            lstAssemblies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                    | AnchorStyles.Left
                                                    | AnchorStyles.Right;
            lstAssemblies.BorderStyle = BorderStyle.FixedSingle;
            lstAssemblies.Columns.AddRange(new ColumnHeader[]
            {
                colName,
                colVersion
            });
            lstAssemblies.FullRowSelect = true;
            lstAssemblies.GridLines = true;
            lstAssemblies.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstAssemblies.Location = new Point(16, 88);
            lstAssemblies.MultiSelect = false;
            lstAssemblies.Name = "lstAssemblies";
            lstAssemblies.Size = new Size(537, 149);
            lstAssemblies.TabIndex = 18;
            lstAssemblies.UseCompatibleStateImageBehavior = false;
            lstAssemblies.View = View.Details;
            colName.Text = "Nombre";
            colName.Width = 369;
            colVersion.Text = "Versión";
            colVersion.Width = 80;
            cmdInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdInfo.BackColor = SystemColors.Control;
            cmdInfo.Location = new Point(327, 293);
            cmdInfo.Name = "cmdInfo";
            cmdInfo.Size = new Size(144, 24);
            cmdInfo.TabIndex = 19;
            cmdInfo.Text = "Información del Sistema";
            cmdInfo.UseVisualStyleBackColor = false;
            ColumnHeader1.Text = "Nombre";
            ColumnHeader1.Width = 369;
            ColumnHeader2.Text = "Versi?n";
            ColumnHeader2.Width = 80;
            ListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                                | AnchorStyles.Left
                                                | AnchorStyles.Right;
            ListView1.BorderStyle = BorderStyle.FixedSingle;
            ListView1.Columns.AddRange(new ColumnHeader[]
            {
                ColumnHeader1,
                ColumnHeader2
            });
            ListView1.FullRowSelect = true;
            ListView1.GridLines = true;
            ListView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ListView1.Location = new Point(16, 88);
            ListView1.MultiSelect = false;
            ListView1.Name = "ListView1";
            ListView1.Size = new Size(537, 149);
            ListView1.TabIndex = 20;
            ListView1.UseCompatibleStateImageBehavior = false;
            ListView1.View = View.Details;
            cmdEntorno.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdEntorno.BackColor = SystemColors.Control;
            cmdEntorno.Location = new Point(239, 293);
            cmdEntorno.Name = "cmdEntorno";
            cmdEntorno.Size = new Size(80, 24);
            cmdEntorno.TabIndex = 21;
            cmdEntorno.Text = "Entorno";
            cmdEntorno.UseVisualStyleBackColor = false;
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PictureBox1.Image = (Image) resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(449, 16);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(112, 60);
            PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox1.TabIndex = 22;
            PictureBox1.TabStop = false;
            Label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                                                | AnchorStyles.Right;
            Label3.BorderStyle = BorderStyle.FixedSingle;
            Label3.Font = new Font("Arial Narrow", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Label3.Location = new Point(16, 237);
            Label3.Name = "Label3";
            Label3.Size = new Size(537, 48);
            Label3.TabIndex = 23;
            Label3.Text = resources.GetString("Label3.Text");
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = Color.White;
            ClientSize = new Size(569, 323);
            Controls.Add(Label3);
            Controls.Add(PictureBox1);
            Controls.Add(cmdEntorno);
            Controls.Add(cmdInfo);
            Controls.Add(cmdClose);
            Controls.Add(Label5);
            Controls.Add(Label4);
            Controls.Add(Label1);
            Controls.Add(Label2);
            Controls.Add(LinkLabel1);
            Controls.Add(PictureBox2);
            Controls.Add(lstAssemblies);
            Controls.Add(ListView1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Febrer Software";
            ((ISupportInitialize) PictureBox2).EndInit();
            ((ISupportInitialize) PictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}