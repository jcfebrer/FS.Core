#region

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class frmInputBox : Form
    {
        public char PasswordChar
        {
            get { return txtRespuesta.PasswordChar; }
            set { txtRespuesta.PasswordChar = value; }
        }
        public string Caption
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public string Message
        {
            get { return lblMensaje.Text; }
            set { lblMensaje.Text = value; }
        }

        public string InputText
        {
            get { return txtRespuesta.Text; }
            set { txtRespuesta.Text = value; }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void frmInputBox_Load(object sender, EventArgs e)
        {
            cmdAceptar.Click += Button1_Click;

            //StartPosition = FormStartPosition.CenterParent;
            FunctionsForms.Center(this);
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;

        internal Button cmdAceptar;
        internal Button cmdCancelar;
        private DBLabel lblMensaje;
        internal DBTextBox txtRespuesta;

        public frmInputBox(string message, string caption, string value, bool password)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            txtRespuesta.Text = value;

            if(password)
                txtRespuesta.PasswordChar = '*';

            Load += frmInputBox_Load;
        }

        public frmInputBox(string message, string caption, string value)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            txtRespuesta.Text = value;

            Load += frmInputBox_Load;
        }

        public frmInputBox(string message, string caption)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            txtRespuesta.Text = "";

            Load += frmInputBox_Load;
        }

        public frmInputBox()
        {
            InitializeComponent();
            
            Load += frmInputBox_Load;
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
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.txtRespuesta = new FSFormControls.DBTextBox();
            this.lblMensaje = new FSFormControls.DBLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtRespuesta)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAceptar.Location = new System.Drawing.Point(227, 125);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.Size = new System.Drawing.Size(128, 37);
            this.cmdAceptar.TabIndex = 1;
            this.cmdAceptar.Text = "Aceptar";
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancelar.Location = new System.Drawing.Point(367, 125);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(128, 37);
            this.cmdCancelar.TabIndex = 2;
            this.cmdCancelar.Text = "Cancelar";
            // 
            // txtRespuesta
            // 
            
            this.txtRespuesta.AcceptsReturn = false;
            this.txtRespuesta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRespuesta.Appearance = null;
            this.txtRespuesta.AsociatedCombo = null;
            this.txtRespuesta.AsociatedDBFindTextBox = null;
            this.txtRespuesta.BackColorRead = System.Drawing.Color.WhiteSmoke;
            this.txtRespuesta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtRespuesta.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
                        this.txtRespuesta.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.txtRespuesta.DateFormat = "dd/MM/yyyy";
            this.txtRespuesta.DBField = null;
            this.txtRespuesta.DBFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRespuesta.Decimals = 0;
            this.txtRespuesta.DefaultValue = "";
            this.txtRespuesta.DotNumber = false;
            this.txtRespuesta.Editable = true;
            this.txtRespuesta.EditAs = FSFormControls.DBTextBox.EditAsType.UseSpecifiedMask;
            this.txtRespuesta.Encrypted = false;
            this.txtRespuesta.Expression = "";
            this.txtRespuesta.FireTextChanged = true;
            this.txtRespuesta.FormatString = "";
            this.txtRespuesta.GridOperation = FSFormControls.DBColumn.OperationTypes.Sum;
            this.txtRespuesta.InputMask = null;
            this.txtRespuesta.Location = new System.Drawing.Point(12, 70);
            this.txtRespuesta.MaskInput = null;
            this.txtRespuesta.MaxLength = 32767;
            this.txtRespuesta.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.txtRespuesta.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.txtRespuesta.Mode = FSFormControls.Global.AccessMode.WriteMode;
            this.txtRespuesta.Multiline = true;
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.NonAutoSizeHeight = 0;
            this.txtRespuesta.NumericType = FSFormControls.DBTextBox.NumericTypeEnum.Double;
            this.txtRespuesta.Obligatory = false;
            this.txtRespuesta.PasswordChar = '\0';
            this.txtRespuesta.PromptChar = '\0';
            this.txtRespuesta.ReadOnly = false;
            this.txtRespuesta.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRespuesta.SelectAllBehavior = FSFormControls.DBTextBox.SelectAllBehaviorEnum.SelectAllCharacters;
            this.txtRespuesta.SelectionLength = 0;
            this.txtRespuesta.SelectionStart = 0;
            this.txtRespuesta.SendCommaAsPoint = true;
            this.txtRespuesta.SendTabAsEnter = true;
            this.txtRespuesta.Shadow = false;
            this.txtRespuesta.ShadowColor = System.Drawing.Color.Gray;
            this.txtRespuesta.ShadowSize = 4;
            this.txtRespuesta.ShowAsCombo = false;
            this.txtRespuesta.ShowKeyboard = false;
            this.txtRespuesta.ShowScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtRespuesta.Size = new System.Drawing.Size(483, 49);
            this.txtRespuesta.TabIndex = 0;
            this.txtRespuesta.TabNavigation = FSFormControls.DBTextBox.TabNavigationEnum.NextControl;
            this.txtRespuesta.Text = "[Respuesta]";
            this.txtRespuesta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtRespuesta.ToolTip = "";
            this.txtRespuesta.Value = "[Respuesta]";
            this.txtRespuesta.XMLName = null;
            // 
            // lblMensaje
            // 
            
            this.lblMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMensaje.Angle = 0F;
            this.lblMensaje.Appearance = null;
            this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
            this.lblMensaje.BorderStyleInner = System.Windows.Forms.BorderStyle.None;
            this.lblMensaje.BorderStyleOuter = System.Windows.Forms.BorderStyle.None;
            this.lblMensaje.Capitalize = FSFormControls.DBTextBox.TypeString.Normal;
                        this.lblMensaje.DataType = FSFormControls.DBTextBox.TypeData.All;
            this.lblMensaje.DateFormat = "dd/MM/yyyy";
            this.lblMensaje.Decimals = 2;
            this.lblMensaje.EndColor = System.Drawing.Color.LightSkyBlue;
            this.lblMensaje.Location = new System.Drawing.Point(12, 26);
            this.lblMensaje.Mode = FSFormControls.Global.AccessMode.ReadMode;
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.ShadowColor = System.Drawing.Color.Black;
            this.lblMensaje.Size = new System.Drawing.Size(483, 38);
            this.lblMensaje.StartColor = System.Drawing.Color.White;
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.TabStop = false;
            this.lblMensaje.Text = "[Mensaje]";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblMensaje.XOffset = 1F;
            this.lblMensaje.YOffset = 1F;
            // 
            // frmInputBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(507, 174);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.cmdAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmInputBox";
            this.Text = "Introduzca la información deseada ...";
            ((System.ComponentModel.ISupportInitialize)(this.txtRespuesta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}