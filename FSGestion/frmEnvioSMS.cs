
using System.Data;
using System.Drawing;
using System.Diagnostics;

using System;
using System.Collections;
using System.Windows.Forms;


//using FSFormControls.DBPDUEncoder;

//########################################################
//               PDU Encoder Demo Project
//*Author:
//   Hesicong
//
//*Last Modify Date:
//   2005/5/28
//
//*Description:
//       This program demostrates my PDU Encoder and gives a
//   convience way to get PDU Encoder by simply set some
//   properties.
//       This program is only a demo, so the code is not very
//   well written. Maybe some bugs are hidden, and some cashed
//   may happen.You can use this code as the base of your project.
//'
//*Note:
//   You can use and modify this code freely. But if you find or fixed
//this code, please send your code to me. This helps me to improve my code
//and more people will be benefited from your code! Thanks!
//
//*Contace me:
//   Email:hesicong@mail.sc.cninfo.net
//   HomePage(Chinese): http://dream-world.nease.net
//   CSDN Blog: http://blog.csdn.net/hesicong
//   Tencent QQ: 38288890
//   MSN Spaces: http://spaces.msn.com/members/hesicong


namespace FSGestion
{
	public class frmEnvioSMS : System.Windows.Forms.Form
	{
		object SMSObject; //Object To Store SMS or ConcatenatedShortMessage. Late Blinding.
		FSFormControls.PDUEncoder.ENUM_TP_DCS DataCodingScheme;
		FSFormControls.PDUEncoder.ENUM_TP_VPF ValidPeriod;
		string[] PDUCodes;
		
#region  Windows
		
		public frmEnvioSMS()
		{
			
			InitializeComponent();
			
			this.MdiParent = Global.mdiP;
		}
		
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
		
		private System.ComponentModel.Container components = null;
		
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.TextBox txtMsgRef;
		internal System.Windows.Forms.ComboBox cmbValidPeriod;
		internal System.Windows.Forms.ComboBox cmbDataCodingScheme;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.CheckBox chkStatusReport;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.TextBox txtDestNum;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox txtServiceCenterNum;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.TextBox txtUserData;
		internal System.Windows.Forms.GroupBox GroupBox4;
		internal System.Windows.Forms.Button cmdReset;
		internal System.Windows.Forms.Button cmdGetPDU;
		internal System.Windows.Forms.StatusBar stsBar;
		internal System.Windows.Forms.StatusBarPanel stsBarCharCount;
		internal System.Windows.Forms.TextBox txtPDU;
		internal System.Windows.Forms.Button cmdCopyToClipboard;
		internal System.Windows.Forms.StatusBarPanel stsPDULength;
		internal System.Windows.Forms.Button Button1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			base.Load += new System.EventHandler(frmMain_Load);
			this.txtMsgRef = new System.Windows.Forms.TextBox();
			this.cmbValidPeriod = new System.Windows.Forms.ComboBox();
			this.cmbDataCodingScheme = new System.Windows.Forms.ComboBox();
			this.cmbDataCodingScheme.SelectedIndexChanged += new System.EventHandler(this.cmbDataCodingScheme_SelectedIndexChanged);
			this.Label5 = new System.Windows.Forms.Label();
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.chkStatusReport = new System.Windows.Forms.CheckBox();
			this.cmdReset = new System.Windows.Forms.Button();
			this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.txtDestNum = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.txtServiceCenterNum = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.txtUserData = new System.Windows.Forms.TextBox();
			this.txtUserData.TextChanged += new System.EventHandler(this.txtUserData_TextChanged);
			this.GroupBox4 = new System.Windows.Forms.GroupBox();
			this.txtPDU = new System.Windows.Forms.TextBox();
			this.cmdGetPDU = new System.Windows.Forms.Button();
			this.cmdGetPDU.Click += new System.EventHandler(this.cmdGetPDU_Click);
			this.stsBar = new System.Windows.Forms.StatusBar();
			this.stsBarCharCount = new System.Windows.Forms.StatusBarPanel();
			this.stsPDULength = new System.Windows.Forms.StatusBarPanel();
			this.cmdCopyToClipboard = new System.Windows.Forms.Button();
			this.cmdCopyToClipboard.Click += new System.EventHandler(this.cmdCopyToClipboard_Click);
			this.Button1 = new System.Windows.Forms.Button();
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.GroupBox2.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.GroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.stsBarCharCount).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.stsPDULength).BeginInit();
			this.SuspendLayout();
			//
			//GroupBox2
			//
			this.GroupBox2.Controls.Add(this.txtMsgRef);
			this.GroupBox2.Controls.Add(this.cmbValidPeriod);
			this.GroupBox2.Controls.Add(this.cmbDataCodingScheme);
			this.GroupBox2.Controls.Add(this.Label5);
			this.GroupBox2.Controls.Add(this.Label4);
			this.GroupBox2.Controls.Add(this.Label3);
			this.GroupBox2.Controls.Add(this.chkStatusReport);
			this.GroupBox2.Controls.Add(this.cmdReset);
			this.GroupBox2.Location = new System.Drawing.Point(7, 104);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(280, 145);
			this.GroupBox2.TabIndex = 7;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Options";
			//
			//txtMsgRef
			//
			this.txtMsgRef.Location = new System.Drawing.Point(137, 82);
			this.txtMsgRef.Name = "txtMsgRef";
			this.txtMsgRef.Size = new System.Drawing.Size(133, 20);
			this.txtMsgRef.TabIndex = 9;
			this.txtMsgRef.Text = "";
			//
			//cmbValidPeriod
			//
			this.cmbValidPeriod.Location = new System.Drawing.Point(137, 52);
			this.cmbValidPeriod.Name = "cmbValidPeriod";
			this.cmbValidPeriod.Size = new System.Drawing.Size(136, 21);
			this.cmbValidPeriod.TabIndex = 8;
			//
			//cmbDataCodingScheme
			//
			this.cmbDataCodingScheme.Location = new System.Drawing.Point(137, 22);
			this.cmbDataCodingScheme.Name = "cmbDataCodingScheme";
			this.cmbDataCodingScheme.Size = new System.Drawing.Size(136, 21);
			this.cmbDataCodingScheme.TabIndex = 7;
			//
			//Label5
			//
			this.Label5.Location = new System.Drawing.Point(8, 85);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(125, 15);
			this.Label5.TabIndex = 5;
			this.Label5.Text = "Message Reference";
			this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//Label4
			//
			this.Label4.Location = new System.Drawing.Point(8, 56);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(96, 15);
			this.Label4.TabIndex = 3;
			this.Label4.Text = "ValidityPeriod";
			this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//Label3
			//
			this.Label3.Location = new System.Drawing.Point(8, 26);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(112, 15);
			this.Label3.TabIndex = 1;
			this.Label3.Text = "DataCodingScheme";
			this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//chkStatusReport
			//
			this.chkStatusReport.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
			this.chkStatusReport.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkStatusReport.Location = new System.Drawing.Point(8, 115);
			this.chkStatusReport.Name = "chkStatusReport";
			this.chkStatusReport.Size = new System.Drawing.Size(136, 15);
			this.chkStatusReport.TabIndex = 0;
			this.chkStatusReport.Text = "Status Report";
			this.chkStatusReport.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			//
			//cmdReset
			//
			this.cmdReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdReset.Location = new System.Drawing.Point(160, 110);
			this.cmdReset.Name = "cmdReset";
			this.cmdReset.Size = new System.Drawing.Size(112, 26);
			this.cmdReset.TabIndex = 10;
			this.cmdReset.Text = "&Reset To Default";
			//
			//GroupBox1
			//
			this.GroupBox1.Controls.Add(this.txtDestNum);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.txtServiceCenterNum);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Location = new System.Drawing.Point(7, 11);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(280, 86);
			this.GroupBox1.TabIndex = 6;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Número";
			//
			//txtDestNum
			//
			this.txtDestNum.Location = new System.Drawing.Point(137, 54);
			this.txtDestNum.Name = "txtDestNum";
			this.txtDestNum.Size = new System.Drawing.Size(136, 20);
			this.txtDestNum.TabIndex = 7;
			this.txtDestNum.Text = "629237109";
			//
			//Label2
			//
			this.Label2.Location = new System.Drawing.Point(8, 52);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(112, 22);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "Teléfono:";
			this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//txtServiceCenterNum
			//
			this.txtServiceCenterNum.Location = new System.Drawing.Point(137, 24);
			this.txtServiceCenterNum.Name = "txtServiceCenterNum";
			this.txtServiceCenterNum.Size = new System.Drawing.Size(136, 20);
			this.txtServiceCenterNum.TabIndex = 5;
			this.txtServiceCenterNum.Text = "+34609090909";
			//
			//Label1
			//
			this.Label1.Location = new System.Drawing.Point(8, 22);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(128, 23);
			this.Label1.TabIndex = 4;
			this.Label1.Text = "Service Center Number";
			this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			//
			//GroupBox3
			//
			this.GroupBox3.Controls.Add(this.txtUserData);
			this.GroupBox3.Location = new System.Drawing.Point(293, 11);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(294, 86);
			this.GroupBox3.TabIndex = 8;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Mensaje";
			//
			//txtUserData
			//
			this.txtUserData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtUserData.Location = new System.Drawing.Point(3, 16);
			this.txtUserData.Multiline = true;
			this.txtUserData.Name = "txtUserData";
			this.txtUserData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtUserData.Size = new System.Drawing.Size(288, 67);
			this.txtUserData.TabIndex = 0;
			this.txtUserData.Text = "";
			//
			//GroupBox4
			//
			this.GroupBox4.Controls.Add(this.txtPDU);
			this.GroupBox4.Location = new System.Drawing.Point(293, 104);
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.Size = new System.Drawing.Size(294, 145);
			this.GroupBox4.TabIndex = 9;
			this.GroupBox4.TabStop = false;
			this.GroupBox4.Text = "PDU Code";
			//
			//txtPDU
			//
			this.txtPDU.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPDU.Location = new System.Drawing.Point(3, 16);
			this.txtPDU.Multiline = true;
			this.txtPDU.Name = "txtPDU";
			this.txtPDU.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPDU.Size = new System.Drawing.Size(288, 126);
			this.txtPDU.TabIndex = 2;
			this.txtPDU.Text = "";
			//
			//cmdGetPDU
			//
			this.cmdGetPDU.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdGetPDU.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGetPDU.Location = new System.Drawing.Point(293, 256);
			this.cmdGetPDU.Name = "cmdGetPDU";
			this.cmdGetPDU.Size = new System.Drawing.Size(91, 26);
			this.cmdGetPDU.TabIndex = 11;
			this.cmdGetPDU.Text = "&Get PDU Code";
			//
			//stsBar
			//
			this.stsBar.Location = new System.Drawing.Point(0, 285);
			this.stsBar.Name = "stsBar";
			this.stsBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {this.stsBarCharCount, this.stsPDULength});
			this.stsBar.ShowPanels = true;
			this.stsBar.Size = new System.Drawing.Size(591, 19);
			this.stsBar.TabIndex = 12;
			//
			//stsBarCharCount
			//
			this.stsBarCharCount.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.stsBarCharCount.Width = 475;
			//
			//cmdCopyToClipboard
			//
			this.cmdCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdCopyToClipboard.Location = new System.Drawing.Point(392, 256);
			this.cmdCopyToClipboard.Name = "cmdCopyToClipboard";
			this.cmdCopyToClipboard.Size = new System.Drawing.Size(104, 26);
			this.cmdCopyToClipboard.TabIndex = 13;
			this.cmdCopyToClipboard.Text = "&Copy To Clipboard";
			//
			//Button1
			//
			this.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Button1.Location = new System.Drawing.Point(504, 256);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(80, 26);
			this.Button1.TabIndex = 14;
			this.Button1.Text = "&Enviar!";
			//
			//frmEnvioSMS
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(591, 304);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.cmdCopyToClipboard);
			this.Controls.Add(this.stsBar);
			this.Controls.Add(this.cmdGetPDU);
			this.Controls.Add(this.GroupBox4);
			this.Controls.Add(this.GroupBox3);
			this.Controls.Add(this.GroupBox2);
			this.Controls.Add(this.GroupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEnvioSMS";
			this.Opacity = 0.95;
			this.Text = "Envio de SMS";
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.stsBarCharCount).EndInit();
			((System.ComponentModel.ISupportInitialize) this.stsPDULength).EndInit();
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		private void txtUserData_TextChanged(System.Object sender, System.EventArgs e)
		{
			//Count for number of PDUs
			int i = default(int);
			int Encoding; //0 for English 1 for Unicode
			Encoding = cmbDataCodingScheme.SelectedIndex;
			string Text = txtUserData.Text;
			for (i = 0; i <= Text.Length - 1; i++)
			{
				if (FSLibrary.TextUtil.Ascii(Text[i].ToString())[0] < 0)
				{
					Encoding = 1;
					break;
				}
			}
			
			int TxtLength = txtUserData.TextLength;
			stsBarCharCount.Text = "CharCount:" + TxtLength.ToString();
			int Piece = default(int);
			
			if (Encoding == 0)
			{
				if (TxtLength <= 160)
				{
					Piece = 1;
					stsBarCharCount.Text += "/160";
				}
				else
				{
					Piece = (TxtLength / 152) + System.Convert.ToInt32(System.Convert.ToInt32(((TxtLength % 152) == 0)) + 1 );
					stsBarCharCount.Text += "/152";
				}
			}
			
			if (Encoding == 1)
			{
				if (TxtLength <= 70)
				{
					Piece = 1;
					stsBarCharCount.Text += "/70";
				}
				else
				{
					Piece = (TxtLength / 66) + System.Convert.ToInt32(System.Convert.ToInt32(((TxtLength % 66) == 0)) + 1 );
					stsBarCharCount.Text += "/66";
				}
			}
			
			stsBarCharCount.Text += "  Split into " + Piece.ToString() + " PDUs";
		}
		
		private void frmMain_Load(System.Object sender, System.EventArgs e)
		{
			//Init Controls
			cmbDataCodingScheme.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_DCS.DefaultAlphabet + ":" + FSFormControls.PDUEncoder.ENUM_TP_DCS.DefaultAlphabet.ToString());
			cmbDataCodingScheme.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_DCS.UCS2 + ":" + FSFormControls.PDUEncoder.ENUM_TP_DCS.UCS2.ToString());
			cmbDataCodingScheme.SelectedIndex = 0;
			
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.Maximum + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.Maximum.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneDay + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneDay.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneHour + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneHour.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneWeek + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.OneWeek.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.SixHours + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.SixHours.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.ThreeHours + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.ThreeHours.ToString());
			cmbValidPeriod.Items.Add(FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.TwelveHours + ":" + FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.TwelveHours.ToString());
			cmbValidPeriod.SelectedIndex = 0;
			
			txtMsgRef.Text = "0";
		}
		
		private void cmdGetPDU_Click(System.Object sender, System.EventArgs e)
		{
			//Check all the information has input.
			if (txtServiceCenterNum.TextLength == 0)
			{
				MessageBox.Show("Please Enter Service Center Number");
			}
			return ;
//			if (txtDestNum.TextLength == 0)
//			{
//				MessageBox.Show("Please Enter Destination Number");
//				}
//				return ;
//				if (txtUserData.Text == "")
//				{
//					MessageBox.Show("Please Enter UserData");
//					}
//					return ;
					
					//Get PDU Code
//					FSFormControls.DBPDUEncoder c = new FSFormControls.DBPDUEncoder();
//					PDUCodes = c.GetPDU(txtServiceCenterNum.Text, txtDestNum.Text, Conversion.Val(cmbDataCodingScheme.Text), Conversion.Val(cmbValidPeriod.Text), (int) (Conversion.Val(txtMsgRef.Text)), chkStatusReport.Checked, txtUserData.Text);
					//Add PDU Codes to Text
//					int i = default(int);
//					stsPDULength.Text = "";
//					txtPDU.Text = "";
//					for (i = 0; i <= PDUCodes.Length - 1; i++)
//					{
//						txtPDU.Text += (string) ("PDU Number:" + (i.ToString() + 1));
//						txtPDU.Text += "\t" + "Length For AT:" + (PDUCodes[i].Length - Conversion.Val("&H" + PDUCodes[i].Substring(0, 2)) * 2 - 2) / 2 + "\r\n"; //Calculate PDU Length for AT command
//						txtPDU.Text += (string) (PDUCodes[i] + "\r\n");
//						}
					}
					
					private void cmdCopyToClipboard_Click(System.Object sender, System.EventArgs e)
					{
						try
						{
							string Data = "";
							int i = default(int);
							for (i = 0; i <= PDUCodes.Length - 1; i++)
							{
								Data += (string) (PDUCodes[i] + "\r\n");
							}
							Data = Data.Remove(Data.Length - 2, 2); //Remove the last vbCrLf
							Clipboard.SetDataObject(Data);
						}
						catch (System.Exception ex)
						{
							MessageBox.Show(ex.ToString());
						}
					}
					
					private void cmdReset_Click(System.Object sender, System.EventArgs e)
					{
						txtServiceCenterNum.Text = "";
						txtDestNum.Text = "";
						cmbDataCodingScheme.SelectedIndex = 0;
						cmbValidPeriod.SelectedIndex = 0;
						txtMsgRef.Text = "0";
						chkStatusReport.Checked = false;
						txtUserData.Text = "";
					}
					
					private void cmbDataCodingScheme_SelectedIndexChanged(System.Object sender, System.EventArgs e)
					{
						txtUserData_TextChanged(null, null);
					}
					
					
					private void Button1_Click(System.Object sender, System.EventArgs e)
					{
						string[] d = null;
						FSFormControls.DBComm prt = new FSFormControls.DBComm();
						FSFormControls.SMS ems = new FSFormControls.SMS();
						FSFormControls.PDUEncoder c = new FSFormControls.PDUEncoder();
						d = c.GetPDU(this.txtServiceCenterNum.Text, this.txtDestNum.Text, FSFormControls.PDUEncoder.ENUM_TP_DCS.DefaultAlphabet, FSFormControls.PDUEncoder.ENUM_TP_VALID_PERIOD.Maximum, 0, false, this.txtUserData.Text);
						
						try
						{
							prt.Open(4, 9600, 8, FSFormControls.DBComm.DataParity.Parity_None, FSFormControls.DBComm.DataStopBit.StopBit_1, 1024);
							
							ems.SendEMS(prt, ref d);
							prt.Close();
							MessageBox.Show("Mensaje enviado.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						catch (System.Exception ex)
						{
							Global.Err.ErrorMessage(ex);
						}
					}
				}
				
			}
