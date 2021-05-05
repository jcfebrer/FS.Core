
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;


using System.Threading;
using System.Data.SqlClient;
using FSCrypto;

namespace FSGestion
{
	public class frmCrypto : System.Windows.Forms.Form
	{
		
#region  Windows Form Designer generated code
		
		public frmCrypto()
		{
			// VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
			UpdateThreadStart = new ThreadStart(QueryDataBase);
			CallDataBindToDataGrid = new MethodInvoker(this.DataBindToDataGrid);
			
			
			//This call is required by the Windows Form Designer.
			InitializeComponent();
			
			//Add any initialization after the InitializeComponent() call.
			
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
		
		//Required by the Windows Form Designer.
		private System.ComponentModel.Container components = null;
		
		//NOTE: The following procedure is required by the Windows Form Designer.
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		public System.Windows.Forms.DataGrid DataGrid1;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Label Label1;
        private TextBox txtData;
        private Button btnCrypt;
        private Button btnDecrypt;
        internal System.Windows.Forms.TextBox TextBox1;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.DataGrid1 = new System.Windows.Forms.DataGrid();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.btnCrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGrid1
            // 
            this.DataGrid1.DataMember = "";
            this.DataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGrid1.Location = new System.Drawing.Point(356, 8);
            this.DataGrid1.Name = "DataGrid1";
            this.DataGrid1.Size = new System.Drawing.Size(340, 193);
            this.DataGrid1.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(560, 262);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(136, 23);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Query on Thread";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(564, 233);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(132, 23);
            this.Button2.TabIndex = 3;
            this.Button2.Text = "Query on Form";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 332);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(680, 23);
            this.Label1.TabIndex = 4;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(356, 207);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(340, 20);
            this.TextBox1.TabIndex = 5;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(12, 8);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(338, 193);
            this.txtData.TabIndex = 6;
            // 
            // btnCrypt
            // 
            this.btnCrypt.Location = new System.Drawing.Point(12, 207);
            this.btnCrypt.Name = "btnCrypt";
            this.btnCrypt.Size = new System.Drawing.Size(88, 23);
            this.btnCrypt.TabIndex = 7;
            this.btnCrypt.Text = "Crypt";
            this.btnCrypt.UseVisualStyleBackColor = true;
            this.btnCrypt.Click += new System.EventHandler(this.btnCrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(106, 207);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(88, 23);
            this.btnDecrypt.TabIndex = 8;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // frmCrypto
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(704, 300);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnCrypt);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.DataGrid1);
            this.Name = "frmCrypto";
            this.Text = "Crypto";
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
#endregion
		
		Thread UpdateThread;
		ThreadStart UpdateThreadStart; // VBConversions Note: Initial value of "new ThreadStart(QueryDataBase)" cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
		MethodInvoker CallDataBindToDataGrid; // VBConversions Note: Initial value of "new MethodInvoker(this.DataBindToDataGrid)" cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
		
		DataSet MyDataSet;
		SqlDataAdapter MyDataAdapter;
		string MyQueryString = "SELECT Products.* FROM [Order Details] CROSS JOIN Products";
		SqlConnection MyConnection = new SqlConnection("data source=localhost;initial catalog=northwind;integrated security=SSPI;");
		
		private void Button1_Click(System.Object sender, System.EventArgs e)
		{
			UpdateThread = new Thread(UpdateThreadStart);
			UpdateThread.IsBackground = true;
			UpdateThread.Name = "UpdateThread";
			UpdateThread.Start();
		}
		
		private void Button2_Click(System.Object sender, System.EventArgs e)
		{
			QueryDataBase();
		}
		
		public void DataBindToDataGrid()
		{
			//MyDataSet.Tables(0).Columns(2).ColumnMapping = MappingType.Hidden
			DataGrid1.DataSource = MyDataSet;
			DataGrid1.DataMember = "authors";
			MyDataAdapter = null;
			MyDataSet = null;
		}
		
		public void QueryDataBase()
		{
			MyDataSet = new DataSet();
			MyConnection.Open();
			SqlCommand cmd = new SqlCommand(MyQueryString, MyConnection);
			MyDataAdapter = new SqlDataAdapter(cmd);
			Label1.Text = "Filling DataSet";
			MyDataAdapter.Fill(MyDataSet, "authors");
			MyConnection.Close();
			Label1.Text = "DataSet Filled";
			this.BeginInvoke(CallDataBindToDataGrid);
		}

        private void btnCrypt_Click(object sender, EventArgs e)
        {
            Crypto crypt = new Crypto();
            txtData.Text = crypt.Crypt(txtData.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Crypto crypt = new Crypto();
            txtData.Text = crypt.Decryp(txtData.Text);
        }
    }
}
