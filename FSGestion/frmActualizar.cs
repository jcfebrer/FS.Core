
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;
using FSNetwork;

namespace FSGestion
{
	public class frmActualizar : System.Windows.Forms.Form
	{
		
#region  Código generado por el Diseñador de Windows Forms
		
		public frmActualizar()
		{
			
			//El Diseñador de Windows Forms requiere esta llamada.
			InitializeComponent();
			
			//Agregar cualquier inicialización después de la llamada a InitializeComponent()
			
		}
		
		//Form reemplaza a Dispose para limpiar la lista de componentes.
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
		
		//Requerido por el Diseñador de Windows Forms
		private System.ComponentModel.Container components = null;
		
		//NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
		//Puede modificarse utilizando el Diseñador de Windows Forms.
		//No lo modifique con el editor de código.
		internal System.Windows.Forms.GroupBox outputGroupBox;
		internal System.Windows.Forms.TextBox totalBytesTextBox;
		internal System.Windows.Forms.TextBox bytesDownloadedTextBox;
		internal System.Windows.Forms.Label bytesDownloadedLbl;
		internal System.Windows.Forms.Label downloadProgressLbl;
		internal System.Windows.Forms.ProgressBar progressBar;
		internal System.Windows.Forms.Label totalBytesLbl;
		internal System.Windows.Forms.Button downloadBtn;
		internal System.Windows.Forms.Label urlLabel;
		internal System.Windows.Forms.TextBox downloadUrlTextBox;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.outputGroupBox = new System.Windows.Forms.GroupBox();
			dbWeb.CompleteCallback += new FSNetwork.WebDownload.CompleteCallbackEventHandler(dbWeb_CompleteCallback);
			dbWeb.ProgressCallback += new FSNetwork.WebDownload.ProgressCallbackEventHandler(dbWeb_ProgressCallback);
			this.totalBytesTextBox = new System.Windows.Forms.TextBox();
			this.bytesDownloadedTextBox = new System.Windows.Forms.TextBox();
			this.bytesDownloadedLbl = new System.Windows.Forms.Label();
			this.downloadProgressLbl = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.totalBytesLbl = new System.Windows.Forms.Label();
			this.downloadBtn = new System.Windows.Forms.Button();
			this.downloadBtn.Click += new System.EventHandler(this.downloadBtn_Click);
			this.urlLabel = new System.Windows.Forms.Label();
			this.downloadUrlTextBox = new System.Windows.Forms.TextBox();
			this.outputGroupBox.SuspendLayout();
			this.SuspendLayout();
			//
			//outputGroupBox
			//
			this.outputGroupBox.Controls.Add(this.totalBytesTextBox);
			this.outputGroupBox.Controls.Add(this.bytesDownloadedTextBox);
			this.outputGroupBox.Controls.Add(this.bytesDownloadedLbl);
			this.outputGroupBox.Controls.Add(this.downloadProgressLbl);
			this.outputGroupBox.Controls.Add(this.progressBar);
			this.outputGroupBox.Controls.Add(this.totalBytesLbl);
			this.outputGroupBox.Enabled = false;
			this.outputGroupBox.Location = new System.Drawing.Point(8, 56);
			this.outputGroupBox.Name = "outputGroupBox";
			this.outputGroupBox.Size = new System.Drawing.Size(504, 120);
			this.outputGroupBox.TabIndex = 6;
			this.outputGroupBox.TabStop = false;
			this.outputGroupBox.Text = "Output and Callbacks";
			//
			//totalBytesTextBox
			//
			this.totalBytesTextBox.Location = new System.Drawing.Point(120, 56);
			this.totalBytesTextBox.Name = "totalBytesTextBox";
			this.totalBytesTextBox.ReadOnly = true;
			this.totalBytesTextBox.Size = new System.Drawing.Size(168, 20);
			this.totalBytesTextBox.TabIndex = 4;
			this.totalBytesTextBox.Text = "";
			this.totalBytesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//bytesDownloadedTextBox
			//
			this.bytesDownloadedTextBox.Location = new System.Drawing.Point(120, 24);
			this.bytesDownloadedTextBox.Name = "bytesDownloadedTextBox";
			this.bytesDownloadedTextBox.ReadOnly = true;
			this.bytesDownloadedTextBox.Size = new System.Drawing.Size(168, 20);
			this.bytesDownloadedTextBox.TabIndex = 3;
			this.bytesDownloadedTextBox.Text = "";
			this.bytesDownloadedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			//
			//bytesDownloadedLbl
			//
			this.bytesDownloadedLbl.Location = new System.Drawing.Point(16, 28);
			this.bytesDownloadedLbl.Name = "bytesDownloadedLbl";
			this.bytesDownloadedLbl.TabIndex = 2;
			this.bytesDownloadedLbl.Text = "Bytes Downloaded";
			//
			//downloadProgressLbl
			//
			this.downloadProgressLbl.Location = new System.Drawing.Point(16, 88);
			this.downloadProgressLbl.Name = "downloadProgressLbl";
			this.downloadProgressLbl.Size = new System.Drawing.Size(104, 23);
			this.downloadProgressLbl.TabIndex = 1;
			this.downloadProgressLbl.Text = "Download Progress";
			//
			//progressBar
			//
			this.progressBar.Location = new System.Drawing.Point(120, 88);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(376, 23);
			this.progressBar.TabIndex = 0;
			//
			//totalBytesLbl
			//
			this.totalBytesLbl.Location = new System.Drawing.Point(16, 60);
			this.totalBytesLbl.Name = "totalBytesLbl";
			this.totalBytesLbl.TabIndex = 2;
			this.totalBytesLbl.Text = "Total Bytes";
			//
			//downloadBtn
			//
			this.downloadBtn.Location = new System.Drawing.Point(416, 24);
			this.downloadBtn.Name = "downloadBtn";
			this.downloadBtn.Size = new System.Drawing.Size(88, 23);
			this.downloadBtn.TabIndex = 7;
			this.downloadBtn.Text = "Download";
			//
			//urlLabel
			//
			this.urlLabel.Location = new System.Drawing.Point(16, 24);
			this.urlLabel.Name = "urlLabel";
			this.urlLabel.Size = new System.Drawing.Size(100, 16);
			this.urlLabel.TabIndex = 5;
			this.urlLabel.Text = "File To Download :";
			//
			//downloadUrlTextBox
			//
			this.downloadUrlTextBox.Location = new System.Drawing.Point(128, 24);
			this.downloadUrlTextBox.Name = "downloadUrlTextBox";
			this.downloadUrlTextBox.Size = new System.Drawing.Size(280, 20);
			this.downloadUrlTextBox.TabIndex = 4;
			this.downloadUrlTextBox.Text = "";
			//
			//frmActualizar
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 198);
			this.Controls.Add(this.outputGroupBox);
			this.Controls.Add(this.downloadBtn);
			this.Controls.Add(this.urlLabel);
			this.Controls.Add(this.downloadUrlTextBox);
			this.Name = "frmActualizar";
			this.Text = "frmActualizar";
			this.outputGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
#endregion
		
		private WebDownload dbWeb = new WebDownload();
		
		private void dbWeb_CompleteCallback(byte[] dataDownloaded)
		{
			if (!progressBar.Visible)
			{
				
				progressBar.Visible = true;
				progressBar.Minimum = 0;
				progressBar.Value = System.Convert.ToInt32(progressBar.Maximum == 1);
				totalBytesTextBox.Text = bytesDownloadedTextBox.Text;
			}
			
			MessageBox.Show("Download complete...", "Download Info");
		}
		
		private void dbWeb_ProgressCallback(int bytesRead, int totalBytes)
		{
			bytesDownloadedTextBox.Text = bytesRead.ToString("#,##0");
			
			if (totalBytes != -1)
			{
				
				progressBar.Minimum = 0;
				progressBar.Maximum = totalBytes;
				progressBar.Value = bytesRead;
				totalBytesTextBox.Text = totalBytes.ToString("#,##0");
				
			}
			else
			{
				progressBar.Visible = false;
				totalBytesTextBox.Text = "Total File Size Not Known";
			}
		}
		
		private void downloadBtn_Click(System.Object sender, System.EventArgs e)
		{
			if (this.downloadUrlTextBox.Text != "")
			{
				
				this.outputGroupBox.Enabled = true;
				
				this.bytesDownloadedTextBox.Text = "";
				this.totalBytesTextBox.Text = "";
				this.progressBar.Minimum = 0;
				this.progressBar.Maximum = 0;
				this.progressBar.Value = 0;


                FSNetwork.WebDownload dl = new FSNetwork.WebDownload();
				dl.DownloadUrl = this.downloadUrlTextBox.Text;
				
				dl.Download();
				//Dim t As System.Threading.Thread = New System.Threading.Thread(AddressOf dl.Download)
				//t.Start()
			}
		}
	}
	
}
