
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;


using System.Text;


namespace FSGestion
{
	public class frmCommDialog : System.Windows.Forms.Form
	{
		
#region  Windows Form Designer generated code
		
		public frmCommDialog()
		{
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call
			this.MdiParent = Global.mdiP;
		}
		
		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Required by the Windows Form Designer
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		internal System.Windows.Forms.MainMenu MainMenu1;
		internal System.Windows.Forms.MenuItem MenuItem1;
		internal System.Windows.Forms.MenuItem MenuItem2;
		internal System.Windows.Forms.MenuItem MenuItem3;
		internal System.Windows.Forms.MenuItem MenuItem4;
		internal System.Windows.Forms.MenuItem MenuItem5;
		internal System.Windows.Forms.ToolBar ToolBar1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton1;
		internal System.Windows.Forms.ToolBarButton ToolBarButton2;
		internal System.Windows.Forms.ToolBarButton ToolBarButton3;
		internal System.Windows.Forms.ToolBarButton ToolBarButton4;
		internal System.Windows.Forms.StatusBar sbr;
		internal System.Windows.Forms.StatusBarPanel sbpComNo;
		internal System.Windows.Forms.StatusBarPanel sbpSettings;
		internal System.Windows.Forms.StatusBarPanel sbpStatus;
		internal System.Windows.Forms.Timer tmrRead;
		internal System.Windows.Forms.TextBox TextBox1;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.TextBox TextBox2;
		internal System.Windows.Forms.ImageList imlTB;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.Load += new System.EventHandler(Form1_Load);
			base.Closing += new System.ComponentModel.CancelEventHandler(Form1_Closing);
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmCommDialog));
			this.MainMenu1 = new System.Windows.Forms.MainMenu();
			this.MenuItem1 = new System.Windows.Forms.MenuItem();
			this.MenuItem2 = new System.Windows.Forms.MenuItem();
			this.MenuItem2.Click += new System.EventHandler(this.MenuItem2_Click);
			this.MenuItem3 = new System.Windows.Forms.MenuItem();
			this.MenuItem3.Click += new System.EventHandler(this.MenuItem3_Click);
			this.MenuItem4 = new System.Windows.Forms.MenuItem();
			this.MenuItem5 = new System.Windows.Forms.MenuItem();
			this.MenuItem5.Click += new System.EventHandler(this.MenuItem5_Click);
			this.ToolBar1 = new System.Windows.Forms.ToolBar();
			this.ToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
			this.ToolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.sbr = new System.Windows.Forms.StatusBar();
			this.sbpComNo = new System.Windows.Forms.StatusBarPanel();
			this.sbpSettings = new System.Windows.Forms.StatusBarPanel();
			this.sbpStatus = new System.Windows.Forms.StatusBarPanel();
			this.tmrRead = new System.Windows.Forms.Timer(this.components);
			this.tmrRead.Tick += new System.EventHandler(this.tmrRead_Tick);
			this.TextBox1 = new System.Windows.Forms.TextBox();
			this.Button1 = new System.Windows.Forms.Button();
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.TextBox2 = new System.Windows.Forms.TextBox();
			this.imlTB = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize) this.sbpComNo).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.sbpSettings).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.sbpStatus).BeginInit();
			this.SuspendLayout();
			//
			//MainMenu1
			//
			this.MainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.MenuItem1});
			//
			//MenuItem1
			//
			this.MenuItem1.Index = 0;
			this.MenuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {this.MenuItem2, this.MenuItem3, this.MenuItem4, this.MenuItem5});
			this.MenuItem1.Text = "&File";
			//
			//MenuItem2
			//
			this.MenuItem2.Index = 0;
			this.MenuItem2.Text = "&Connect...";
			//
			//MenuItem3
			//
			this.MenuItem3.Enabled = false;
			this.MenuItem3.Index = 1;
			this.MenuItem3.Text = "&Disconnect";
			//
			//MenuItem4
			//
			this.MenuItem4.Index = 2;
			this.MenuItem4.Text = "-";
			//
			//MenuItem5
			//
			this.MenuItem5.Index = 3;
			this.MenuItem5.Text = "E&xit";
			//
			//ToolBar1
			//
			this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.ToolBarButton1, this.ToolBarButton2, this.ToolBarButton3, this.ToolBarButton4});
			this.ToolBar1.DropDownArrows = true;
			this.ToolBar1.ImageList = this.imlTB;
			this.ToolBar1.Location = new System.Drawing.Point(0, 0);
			this.ToolBar1.Name = "ToolBar1";
			this.ToolBar1.ShowToolTips = true;
			this.ToolBar1.Size = new System.Drawing.Size(915, 44);
			this.ToolBar1.TabIndex = 0;
			//
			//ToolBarButton1
			//
			this.ToolBarButton1.ImageIndex = 0;
			this.ToolBarButton1.ToolTipText = "Connect to RS232";
			//
			//ToolBarButton2
			//
			this.ToolBarButton2.Enabled = false;
			this.ToolBarButton2.ImageIndex = 1;
			this.ToolBarButton2.ToolTipText = "Disconnect";
			//
			//ToolBarButton3
			//
			this.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			//
			//ToolBarButton4
			//
			this.ToolBarButton4.ImageIndex = 2;
			this.ToolBarButton4.ToolTipText = "Exit the application";
			//
			//sbr
			//
			this.sbr.Location = new System.Drawing.Point(0, 516);
			this.sbr.Name = "sbr";
			this.sbr.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {this.sbpComNo, this.sbpSettings, this.sbpStatus});
			this.sbr.ShowPanels = true;
			this.sbr.Size = new System.Drawing.Size(915, 22);
			this.sbr.TabIndex = 1;
			this.sbr.Text = "StatusBar1";
			//
			//sbpSettings
			//
			this.sbpSettings.Width = 300;
			//
			//sbpStatus
			//
			this.sbpStatus.Width = 500;
			//
			//tmrRead
			//
			//
			//TextBox1
			//
			this.TextBox1.Enabled = false;
			this.TextBox1.Location = new System.Drawing.Point(4, 54);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new System.Drawing.Size(817, 20);
			this.TextBox1.TabIndex = 2;
			this.TextBox1.Text = "";
			//
			//Button1
			//
			this.Button1.Enabled = false;
			this.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Button1.Location = new System.Drawing.Point(831, 48);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(81, 30);
			this.Button1.TabIndex = 3;
			this.Button1.Text = "&Send";
			this.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			//
			//TextBox2
			//
			this.TextBox2.Enabled = false;
			this.TextBox2.Location = new System.Drawing.Point(4, 83);
			this.TextBox2.Multiline = true;
			this.TextBox2.Name = "TextBox2";
			this.TextBox2.ReadOnly = true;
			this.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextBox2.Size = new System.Drawing.Size(906, 426);
			this.TextBox2.TabIndex = 4;
			this.TextBox2.Text = "";
			//
			//imlTB
			//
			this.imlTB.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imlTB.ImageSize = new System.Drawing.Size(32, 32);
			this.imlTB.ImageStream = (System.Windows.Forms.ImageListStreamer) (resources.GetObject("imlTB.ImageStream"));
			this.imlTB.TransparentColor = System.Drawing.Color.Transparent;
			//
			//frmCommDialog
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(915, 538);
			this.Controls.Add(this.TextBox2);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.TextBox1);
			this.Controls.Add(this.sbr);
			this.Controls.Add(this.ToolBar1);
			this.Menu = this.MainMenu1;
			this.Name = "frmCommDialog";
			this.Text = "My RS232 Tester";
			((System.ComponentModel.ISupportInitialize) this.sbpComNo).EndInit();
			((System.ComponentModel.ISupportInitialize) this.sbpSettings).EndInit();
			((System.ComponentModel.ISupportInitialize) this.sbpStatus).EndInit();
			this.ResumeLayout(false);
			
		}
		
#endregion
		private FSFormControls.DBComm oCP = new FSFormControls.DBComm();
		
		private int intCommPort;
		private int intBaud;
		private int intData;
		private FSFormControls.DBComm.DataStopBit bytStop;
		private FSFormControls.DBComm.DataParity bytParity;
		
		private void MenuItem2_Click(System.Object sender, System.EventArgs e)
		{
			Options fOpt = new Options();
			
			fOpt.ShowDialog();
			if (fOpt.bGo)
			{
				sbr.Panels[0].Text = (string) fOpt.ComboBox1.SelectedItem;
				sbr.Panels[1].Text = fOpt.ComboBox2.SelectedItem + " - " + fOpt.ComboBox3.SelectedItem + " - " + fOpt.ComboBox4.SelectedItem + " - " + fOpt.ComboBox5.SelectedItem;
				intCommPort = int.Parse(((string) fOpt.ComboBox1.SelectedItem).Substring(0, 4));
				intBaud = System.Convert.ToInt32(fOpt.ComboBox2.SelectedItem);
				intData = System.Convert.ToInt32(fOpt.ComboBox3.SelectedItem);
				switch (fOpt.ComboBox4.SelectedIndex)
				{
					case 0:
						bytParity = FSFormControls.DBComm.DataParity.Parity_Even;
						break;
					case 1:
						bytParity = FSFormControls.DBComm.DataParity.Pariti_Odd;
						break;
					case 2:
						bytParity = FSFormControls.DBComm.DataParity.Parity_None;
						break;
					default:
						bytParity = FSFormControls.DBComm.DataParity.Parity_Mark;
						break;
				}
				switch (fOpt.ComboBox5.SelectedIndex)
				{
					case 0:
						bytStop = FSFormControls.DBComm.DataStopBit.StopBit_1;
						break;
					default:
						bytStop = FSFormControls.DBComm.DataStopBit.StopBit_2;
						break;
				}
				oCP.Open(intCommPort, intBaud, intData, bytParity, bytStop, 4096);
				tmrRead.Enabled = true;
				MenuItem2.Enabled = false;
				MenuItem3.Enabled = true;
				ToolBar1.Buttons[0].Enabled = false;
				ToolBar1.Buttons[1].Enabled = true;
				TextBox1.Enabled = true;
				TextBox2.Enabled = true;
				Button1.Enabled = true;
			}
			fOpt.Close();
			fOpt = null;
		}
		
		private void Form1_Load(System.Object sender, System.EventArgs e)
		{
			int i = default(int);
			
			for (i = 0; i <= 3; i++)
			{
				Global.mComs[i] = IsPortAvailable(i + 1);
			}
		}
		
		private void ToolBar1_ButtonClick(System.Object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch (ToolBar1.Buttons.IndexOf(e.Button))
			{
				case 0:
					MenuItem2_Click(MenuItem2, new System.EventArgs());
					break;
				case 1:
					MenuItem3_Click(MenuItem3, new System.EventArgs());
					break;
				default:
					MenuItem5_Click(MenuItem5, new System.EventArgs());
					break;
			}
		}
		
		private void MenuItem3_Click(System.Object sender, System.EventArgs e)
		{
			oCP.Close();
			tmrRead.Enabled = false;
			MenuItem2.Enabled = true;
			MenuItem3.Enabled = false;
			ToolBar1.Buttons[0].Enabled = true;
			ToolBar1.Buttons[1].Enabled = false;
			TextBox1.Enabled = false;
			TextBox2.Enabled = false;
			Button1.Enabled = false;
		}
		
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (MenuItem3.Enabled)
			{
				MenuItem3_Click(MenuItem3, new System.EventArgs());
			}
		}
		
		private void MenuItem5_Click(System.Object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				// Enable the timer.
				// Write an user specified Command to the Port.
				oCP.Write(Encoding.ASCII.GetBytes(this.TextBox1.Text + '\r'));
				WriteMessage(TextBox1.Text, true);
				
			}
			catch (Exception)
			{
				// Warn the user.
				MessageBox.Show("Unable to write to comm port");
			}
			finally
			{
				TextBox1.Text = "";
				TextBox1.Focus();
			}
		}
		
		private void tmrRead_Tick(System.Object sender, System.EventArgs e)
		{
			try
			{
				// As long as there is information, read one byte at a time and
				//   output it.
				while (oCP.Read(1) != -1)
				{
					// Write the output to the screen.
					WriteMessage(System.Convert.ToString(oCP.InputStream[0]), false);
				}
			}
			catch (Exception)
			{
				// An exception is raised when there is no information to read.
				//   Don't do anything here, just let the exception go.
			}
		}
		
		// This subroutine writes a message to the txtStatus TextBox.
		private void WriteMessage(string message)
		{
			this.TextBox2.Text += message + "\r\n";
			TextBox2.SelectionStart = TextBox2.Text.Length;
		}
		
		// This subroutine writes a message to the txtStatus TextBox and allows
		//   the line feed to be suppressed.
		private void WriteMessage(string message, bool linefeed)
		{
			this.TextBox2.Text += message;
			if (linefeed)
			{
				this.TextBox2.Text += "\r\n";
			}
			TextBox2.SelectionStart = TextBox2.Text.Length;
		}
		
		// This function attempts to open the passed Comm Port. If it is
		//   available, it returns True, else it returns False. To determine
		//   availability a Try-Catch block is used.
		private bool IsPortAvailable(int ComPort)
		{
			try
			{
				oCP.Open(ComPort, 115200, 8, FSFormControls.DBComm.DataParity.Parity_None, FSFormControls.DBComm.DataStopBit.StopBit_1, 4096);
				// If it makes it to here, then the Comm Port is available.
				oCP.Close();
				return true;
			}
			catch
			{
				// If it gets here, then the attempt to open the Comm Port
				//   was unsuccessful.
				return false;
			}
		}
	}
	
}
