#region

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using FSDatabase;
using FSLibrary;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBCombo.bmp")]
    [DefaultEvent("SelectedValueChanged")]
    [Designer(typeof(DBComboControlDesigner))]
    [ToolboxItem(true)]
    public class DBCombo : DBUserControlBase, ISupportInitialize
    {
        public enum SortStyleEnum
        {
            Ascending,
            AscendingByValue,
            Descending,
            DescendingByValue,
            None
        }

        //public const int WM_ERASEBKGND = 0X14;
        //public const int WM_PAINT = 0XF;
        //public const int WM_NC_PAINT = 0X85;
        //public const int WM_PRINTCLIENT = 0X318;
        private Button cmdEdit;
        private bool doTextChanged = true;
        private bool filled;

        private DBControl m_DBControlList;
        private string m_DBFieldData = "";
        private string m_DBFieldList;
        private ComboBoxStyle m_DropDownStyle = ComboBoxStyle.DropDownList;
        private bool m_Editable = true;
        //private bool m_FlatMode;
        private ImageList m_ImageList = new ImageList();
        private DBComboValues m_Items;
        private AccessMode m_Mode = AccessMode.WriteMode;
        private bool m_Obligatory;
        private object m_SelectedOption;
        private bool m_ShowEdit;


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }


        private string m_DBField;
        [Description("Campo de la base de datos a enlazar.")]
        public string DBField
        {
            get { return m_DBField; }
            set { m_DBField = value; }
        }

        public bool GridMode { get; set; } = false;

        //public bool FlatMode
        //{
        //    get { return m_FlatMode; }
        //    set
        //    {
        //        m_FlatMode = value;
        //        Invalidate();
        //    }
        //}

        public string DBFieldData
        {
            get
            {
                return m_DBFieldData;
            }
            set
            {
                m_DBFieldData = value;
                Combo1.ValueMember = m_DBFieldData;
            }
        }

        public AutoCompleteMode AutoCompleteMode
        {
            get { return Combo1.AutoCompleteMode; }
            set { Combo1.AutoCompleteMode = value; }
        }

        public bool Obligatory
        {
            get { return m_Obligatory; }
            set
            {
                m_Obligatory = value;
                if (value) Combo1.BackColor = Global.ObligatoryBackColor;
            }
        }

        [Description("Modo lectura")]
        public bool ReadOnly
        {
            get { return m_Mode == AccessMode.ReadMode; }
            set { Mode = value ? AccessMode.ReadMode : AccessMode.WriteMode; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get { return m_DropDownStyle; }
            set
            {
                m_DropDownStyle = value;
                Combo1.DropDownStyle = m_DropDownStyle;
            }
        }

        public DBAppearance Appearance { get; set; }

        public bool IsInEditMode
        {
            get { return m_Mode == AccessMode.WriteMode; }
            set { m_Mode = value ? AccessMode.WriteMode : AccessMode.ReadMode; }
        }

        //public int SelectionStart
        //{
        //    get
        //    {
        //        return Combo1.SelectionStart;
        //    }
        //    set
        //    {
        //        Combo1.SelectionStart = value;
        //    }
        //}

        //public int SelectionLength
        //{
        //    get
        //    {
        //        return Combo1.SelectionLength;
        //    }
        //    set
        //    {
        //        Combo1.SelectionStart = 0;
        //        Combo1.SelectionLength = value;
        //    }
        //}

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ValueMember
        {
            get { return Combo1.ValueMember; }
            set
            {
                doTextChanged = false;
                Combo1.ValueMember = value;
                doTextChanged = true;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DisplayMember
        {
            get { return Combo1.DisplayMember; }
            set
            {
                doTextChanged = false;
                Combo1.DisplayMember = value;
                doTextChanged = true;
            }
        }

        public ImageList ImageList
        {
            get { return m_ImageList; }
            set
            {
                m_ImageList = value;
                DrawModeVariable();
            }
        }

        public override Color BackColor
        {
            get { return Combo1.BackColor; }
            set { Combo1.BackColor = value; }
        }


        public bool Sort { get; set; } = true;

        public string DBFieldList
        {
            get { return m_DBFieldList; }
            set
            {
                m_DBFieldList = value;
                Combo1.DisplayMember = m_DBFieldList;
            }
        }

        public SortStyleEnum SortStyle { get; set; }

        public AccessMode Mode
        {
            get
            {
                return m_Mode;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case AccessMode.ReadMode:
                        Combo1.Enabled = false;
                        Combo1.BackColor = Global.NormalBackColor;
                        cmdEdit.Visible = false;
                        break;
                    case AccessMode.WriteMode:
                        Combo1.Enabled = true;
                        Combo1.BackColor = Global.WriteBackColor;
                        cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                    case AccessMode.ProtectedMode:
                        Combo1.Enabled = true;
                        Combo1.BackColor = Global.ObligatoryBackColor;
                        cmdEdit.Visible = true;
                        //Combo1.BringToFront();
                        break;
                }
            }
        }

        public DBComboValues Items
        {
            get { return m_Items; }
            set
            {
                m_Items = value;
                if (value != null)
                    foreach (DBComboboxItem v in value)
                        Combo1.Items.Add(v);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object SelectedOption
        {
            get { return m_SelectedOption; }
            set
            {
                m_SelectedOption = value;
                Combo1.SelectedValue = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SelectedIndex
        {
            get { return Combo1.SelectedIndex; }
            set { Combo1.SelectedIndex = value; }
        }

        public bool BlankSelection { get; set; }

        public bool DroppedDown
        {
            get { return Combo1.DroppedDown; }
            set { Combo1.DroppedDown = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Selected
        {
            set { Combo1.SelectedIndex = Combo1.FindStringExact(Convert.ToString(value)); }
        }

        public DBControl DataControlList
        {
            get
            {
                return m_DBControlList;
            }
            set
            {
                m_DBControlList = value;
                //if (!DesignMode)
                //{
                //    if (m_DBControlList != null)
                //        Fill();
                //}
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override string Text
        {
            get { return Combo1.Text; }
            set { Combo1.Text = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object Value
        {
            get { return Combo1.SelectedValue; }
            set
            {
                if (value == null || Convert.ToInt32(value) == 0)
                    value = -1;

                if (Convert.ToInt32(value) != -1)
                {
                    DBComboboxItem dbitem = FindByValue(value.ToString());
                    if(dbitem != null)
                        Combo1.Text = dbitem.Text;
                }
            }
        }

        public bool ShowCode { get; set; }

        public string OrderBy { get; set; }


        public bool Editable
        {
            get { return m_Editable; }
            set
            {
                m_Editable = value;
                Combo1.Enabled = !m_Editable;
                if (m_Editable)
                {
                    if (!m_Obligatory) Combo1.BackColor = Global.NormalBackColor;
                    Combo1.TabStop = true;
                }
                else
                {
                    Combo1.BackColor = Global.DisableBackColor;
                    Combo1.TabStop = false;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DBButtonCollection ButtonsRight { get; set; } = new DBButtonCollection();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DBButtonCollection ButtonsLeft { get; set; } = new DBButtonCollection();


        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object SelectedValue
        {
            get { return Combo1.SelectedValue; }
            set { Combo1.SelectedValue = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object SelectedItem
        {
            get { return Combo1.SelectedItem; }
            set { Combo1.SelectedItem = value; }
        }


        public new ControlBindingsCollection DataBindings => Combo1.DataBindings;

        public bool ShowEdit
        {
            get { return m_ShowEdit; }
            set
            {
                m_ShowEdit = value;
                cmdEdit.Visible = m_ShowEdit;
            }
        }


        public override Color ForeColor
        {
            get { return Combo1.ForeColor; }

            set { Combo1.ForeColor = value; }
        }

        public bool IsItemInList()
        {
            if (Combo1.Items.Contains(Combo1.SelectedText))
                return true;
            return false;
        }

        public string NameControl()
        {
            return Name;
        }

        public void Fill()
        {
            if (filled)
            {
                return;
            }

            if (m_DBControlList == null) return;
            //throw new ExceptionUtil("DataControlList, no especificado.");

            try
            {
                if (DataControlList.DataTable == null)
                {
                    if (DataControlList.Connected == false)
                    {
                        DataControlList.Connect();
                        if (DataControlList.Connected == false)
                            throw new ExceptionUtil("Imposible conectar DataControlList.");

                        doTextChanged = false;
                        Combo1.DataSource = DataControlList.DataTable;
                        doTextChanged = true;
                    }
                }
                else
                {
                    doTextChanged = false;
                    Combo1.DataSource = DataControlList.DataTable;
                    doTextChanged = true;
                }

                if (DataControlList.TypeDB == DBControl.DbType.Odbc ||
                    DataControlList.TypeDB == DBControl.DbType.OleDB ||
                    DataControlList.TypeDB == DBControl.DbType.SQLServer)
                {
                    var db = new BdUtils(Global.ConnectionStringSetting);

                    if (DBFieldList == null)
                        throw new ExceptionUtil("Campo DBFieldList no especificado.");

                    if (db.GetField(DBFieldList, DataControlList.TableName).Tipo.Name.ToLower() == "string")
                        if (BlankSelection)
                        {
                            DataRow rowblank = null;
                            rowblank = DataControlList.DataTable.NewRow();
                            rowblank[DBFieldList] = "";
                            DataControlList.DataTable.Rows.InsertAt(rowblank, 0);
                        }
                }

                if (Sort)
                {
                    if (!string.IsNullOrEmpty(OrderBy))
                    {
                        DataControlList.DataTable.DefaultView.Sort = OrderBy;
                    }
                    else
                    {
                        if (!(TextUtil.IndexOf(DataControlList.Selection, "order by") > 0))
                            DataControlList.DataTable.DefaultView.Sort = DBFieldList;
                    }
                }

                if (string.IsNullOrEmpty(m_DBFieldData))
                    m_DBFieldData = DataControlList.FieldName(0);

                if (string.IsNullOrEmpty(Combo1.DisplayMember))
                    Combo1.DisplayMember = DataControlList.FieldExactName(DBFieldList);
                if(string.IsNullOrEmpty(Combo1.ValueMember))
                    Combo1.ValueMember = DataControlList.FieldExactName(m_DBFieldData);


                if (!string.IsNullOrEmpty(DBField))
                    if (DataControl == null)
                        throw new ExceptionUtil("DataControl, no especificado.");
                //else
                //{
                //    if (!(DataControl.DataTable == null))
                //    {
                //        if (DropDownStyle == ComboBoxStyle.DropDown)
                //        {
                //            Combo1.DataBindings.Add("Text", DataControl.DataTable, DBField);
                //        }
                //        else
                //        {
                //            Combo1.DataBindings.Add("SelectedValue", DataControl.DataTable, DBField);
                //        }

                //        isBinding = true;
                //    }
                //}

                //Combo1.Select(0, 0); //deselecionamos el primer elemento
                //Combo1.SelectionLength = 0;

                filled = true;
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e.Message);
            }
        }

        private void DrawModeVariable()
        {
            if (ImageList != null)
                if (ImageList.Images.Count > 0)
                    Combo1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        private void Combo1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.ControlKey)
                    if (DataControlList != null)
                        DataControlList.ShowRecord();

                e.Handled = true;
                if (null != KeyDown) KeyDown(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            Combo1.Location = new Point(0, 0);
            Combo1.Size = new Size(Width - (16 * ButtonsRight.Count), Combo1.Height);
            this.Height = Combo1.Height;

            CreateButtons();
        }

        public void BeginUpdate()
        {
            Combo1.BeginUpdate();
        }


        public void EndUpdate()
        {
            Combo1.EndUpdate();
        }


        public int FindString(string s)
        {
            return Combo1.FindString(s);
        }


        public int FindStringExact(string s)
        {
            return Combo1.FindStringExact(s);
        }

        public DBComboboxItem FindByValue(string value)
        {
            if (Items != null)
            {
                foreach (DBComboboxItem dbcol in Items)
                    if (Functions.Valor(dbcol.Value).ToLower() == value.ToLower())
                        return dbcol;
            }
            else
            {
                if (m_DBControlList != null)
                    foreach (DataRow row in m_DBControlList.DataTable.Rows)
                        if (row[Combo1.ValueMember].ToString().ToLower() == value.ToLower())
                            return new DBComboboxItem(row[Combo1.ValueMember].ToString(),
                                row[Combo1.DisplayMember].ToString());
            }

            return null;
        }

        private void Combo1_TextChanged(object sender, EventArgs e)
        {
            if (filled & doTextChanged)
                if (null != TextChanged)
                    TextChanged(this, e);
        }


        private void Combo1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Combo1.SelectedIndex != -1 && !GridMode)
                if (DataControl != null && DBField != "")
                    if (DataControl.Connected)
                        if (Combo1.SelectedItem is DataRowView)
                        {
                            m_SelectedOption = ((DataRowView) Combo1.SelectedItem).Row[0];

                            DataControl.SetField(DBField, m_SelectedOption.ToString());
                        }


            if (doTextChanged)
            {
                if (null != SelectedValueChanged)
                    SelectedValueChanged(this, e);

                if (null != SelectionChanged)
                    SelectionChanged(this, e);
            }
        }


        private void Combo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != SelectedIndexChanged)
                SelectedIndexChanged(this, e);
        }


        private void Combo1_VisibleChanged(object sender, EventArgs e)
        {
            if (null != VisibleChanged)
                VisibleChanged(this, e);
        }


        private void Combo1_Validated(object sender, EventArgs e)
        {
            if (null != Validated)
                Validated(this, e);
        }


        private void Combo1_DropDown(object sender, EventArgs e)
        {
            Invalidate();
            if (null != DropDown)
                DropDown(this, e);
        }


        public void Combo1_LostFocus(object sender, EventArgs e)
        {
            if (null != LostFocus)
                LostFocus(this, e);

            if (null != AfterExitEditMode)
                AfterExitEditMode(this, e);
        }


        private void Combo1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (null != SelectionChangeCommitted)
                SelectionChangeCommitted(this, e);
        }


        private void Combo1_Leave(object sender, EventArgs e)
        {
            AutoCompleteCombo_Leave();
            if (null != Leave)
                Leave(this, e);
        }

        public void AutoCompleteCombo_KeyUp(KeyEventArgs e)
        {
            string sTypedText = null;
            var iFoundIndex = 0;
            object oFoundItem = null;
            string sFoundText = null;
            string sAppendText = null;

            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Delete:
                case Keys.Down:
                    return;
            }


            sTypedText = Combo1.Text;
            iFoundIndex = Combo1.FindString(sTypedText);

            if (iFoundIndex >= 0)
            {
                oFoundItem = Combo1.Items[iFoundIndex];

                sFoundText = Combo1.GetItemText(oFoundItem);

                sAppendText = sFoundText.Substring(sTypedText.Length);
                Combo1.Text = sTypedText + sAppendText;

                Combo1.SelectionStart = sTypedText.Length;
                Combo1.SelectionLength = sAppendText.Length;
            }
        }


        public void AutoCompleteCombo_Leave()
        {
            var iFoundIndex = 0;
            iFoundIndex = FindStringExact(Combo1.Text);
            SelectedIndex = iFoundIndex;
        }


        private void Combo1_KeyUp(object sender, KeyEventArgs e)
        {
            AutoCompleteCombo_KeyUp(e);
            e.Handled = true;
        }

        private void Combo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (null != KeyPress) KeyPress(this, e);
        }

        private void Combo1_Enter(object sender, EventArgs e)
        {
            //Combo1.SelectAll();
            if (null != Enter)
                Enter(this, e);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    if ((DropDownStyle == ComboBoxStyle.Simple) | (FlatMode == false))
        //    {
        //        base.WndProc(ref m);
        //        return;
        //    }

        //    var hDC = Win32API.GetWindowDC(Handle);
        //    var gdc = Graphics.FromHdc(hDC);
        //    switch (m.Msg)
        //    {
        //        case WM_NC_PAINT:
        //            Win32API.SendMessage(Handle, WM_ERASEBKGND, hDC, 0);
        //            SendPrintClientMsg();
        //            PaintFlatControlBorder(this, gdc);
        //            m.Result = new IntPtr(1);
        //            break;
        //        case WM_PAINT:
        //            base.WndProc(ref m);
        //            var c = Enabled ? BackColor : SystemColors.Control;
        //            var p = new Pen(c, 2);
        //            gdc.DrawRectangle(p, new Rectangle(2, 2, Width - 3, Height - 3));
        //            PaintFlatDropDown(this, gdc);
        //            PaintFlatControlBorder(this, gdc);
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }

        //    Win32API.ReleaseDC(m.HWnd, hDC);
        //    gdc.Dispose();
        //}

        //private void SendPrintClientMsg()
        //{
        //    var gClient = CreateGraphics();
        //    var ptrClientDC = gClient.GetHdc();
        //    Win32API.SendMessage(Handle, WM_PRINTCLIENT, ptrClientDC, 0);
        //    gClient.ReleaseHdc(ptrClientDC);
        //    gClient.Dispose();
        //}

        //private void PaintFlatControlBorder(Control ctrl, Graphics g)
        //{
        //    var rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
        //    if ((ctrl.Focused == false) | (ctrl.Enabled == false))
        //        ControlPaint.DrawBorder(g, rect, SystemColors.ControlDark, ButtonBorderStyle.Solid);
        //    else
        //        ControlPaint.DrawBorder(g, rect, Color.Black, ButtonBorderStyle.Solid);
        //}

        //public void PaintFlatDropDown(Control ctrl, Graphics g)
        //{
        //    const int DROPDOWNWIDTH = 18;
        //    var rect = new Rectangle(ctrl.Width - DROPDOWNWIDTH, 0, DROPDOWNWIDTH, ctrl.Height);
        //    ControlPaint.DrawComboButton(g, rect, ButtonState.Flat);
        //}

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    Invalidate();
        //}

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    Invalidate();
        //}

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    Invalidate();
        //}

        //protected override void OnParentChanged(EventArgs e)
        //{
        //    base.OnParentChanged(e);
        //    Invalidate();
        //}

        private void Combo1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        private void Combo1_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        //public void DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    if (ImageList.Images.Count == 0) return;
        //    if ((e.Index >= ImageList.Images.Count) & !BlankSelection) return;

        //    if (e.Index < 0) return;

        //    var str = Convert.ToString(Items[e.Index]);
        //    var im = e.Index;

        //    //if (sender is ComboBox)
        //    //{
        //    //    im =
        //    //        Convert.ToInt32(
        //    //            Functions.CDbl2(((DataRowView) (((ComboBox) sender).Items[e.Index]))[m_DBFieldData]));
        //    //    str = Convert.ToString(((DataRowView) (((ComboBox) sender).Items[e.Index]))[m_DBFieldList]);
        //    //}
        //    //else
        //    //{
        //    //    ComboBoxIconItem item = new ComboBoxIconItem();
        //    //    str = Convert.ToString(Items[e.Index]);
        //    //}

        //    var g = e.Graphics;
        //    var bBlue = new SolidBrush(Color.Blue);
        //    var bWhite = new SolidBrush(Color.White);


        //    float hgt = 0;

        //    if (im >= ImageList.Images.Count) im = ImageList.Images.Count - 1;
        //    if (im < 0) im = 0;

        //    var myImage = ImageList.Images[im];

        //    if (Combo1.DrawMode == DrawMode.OwnerDrawFixed)
        //    {
        //        hgt =
        //            Convert.ToSingle(Math.Max(myImage.Height,
        //                                 Convert.ToInt32(e.Graphics.MeasureString(str, Font).Height)) *
        //                             1.1);
        //        Combo1.ItemHeight = Convert.ToInt32(hgt);
        //    }

        //    var bHighlightItem = ShouldIHighlight(e.State);

        //    if (bHighlightItem)
        //    {
        //        g.FillRectangle(bBlue, e.Bounds);
        //        if (!((e.Index == 0) & BlankSelection))
        //            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) / 2);
        //        g.DrawString(str, Font, bWhite, e.Bounds.Left + myImage.Width + 5, e.Bounds.Top);
        //    }
        //    else
        //    {
        //        g.FillRectangle(bWhite, e.Bounds);
        //        if (!((e.Index == 0) & BlankSelection))
        //            g.DrawImage(myImage, 5, e.Bounds.Top + (e.Bounds.Height - myImage.Height) / 2);
        //        g.DrawString(str, Font, bBlue, e.Bounds.Left + myImage.Width + 5, e.Bounds.Top);
        //    }

        //    bBlue.Dispose();
        //    bWhite.Dispose();
        //    bBlue = null;
        //    bWhite = null;
        //}


        //private bool ShouldIHighlight(DrawItemState State)
        //{
        //    var OSver = Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor;
        //    if (OSver == "5.0")
        //    {
        //        if ((State == (DrawItemState.Selected | DrawItemState.NoFocusRect | DrawItemState.NoAccelerator)) |
        //            (State == (DrawItemState.Selected | DrawItemState.NoFocusRect | DrawItemState.NoAccelerator)) |
        //            (State ==
        //             (DrawItemState.Selected | DrawItemState.Focus | DrawItemState.NoAccelerator |
        //              DrawItemState.NoFocusRect)) | (State == DrawItemState.Selected))
        //            return true;
        //    }
        //    else if (OSver == "5.1")
        //    {
        //        if ((State == (DrawItemState.Selected | DrawItemState.Focus)) | (State == DrawItemState.Selected) |
        //            (State == DrawItemState.Focus))
        //            return true;
        //    }

        //    return false;
        //}


        //private void MeasureItem(object sender, MeasureItemEventArgs e)
        //{
        //    if (ImageList.Images.Count == 0) return;
        //    if ((e.Index >= ImageList.Images.Count) & !BlankSelection) return;

        //    if (e.Index < 0) return;

        //    string str = null;
        //    var im = 0;

        //    if (sender is DBCombo)
        //    {
        //        im =
        //            Convert.ToInt32(
        //                Functions.CDbl2(((DataRowView) ((ComboBox) sender).Items[e.Index])[m_DBFieldData]));
        //        str = Convert.ToString(((DataRowView) ((ComboBox) sender).Items[e.Index])[m_DBFieldList]);
        //    }
        //    else
        //    {
        //        var item = new ComboBoxIconItem();
        //        str = Convert.ToString(Items[e.Index]);
        //    }

        //    object[] transTemp5 = str.Split(Environment.NewLine.ToCharArray());
        //    var strOneLine = string.Join("", (string[]) transTemp5);

        //    if (im >= ImageList.Images.Count) im = ImageList.Images.Count - 1;
        //    if (im < 0) im = 0;

        //    var img = ImageList.Images[im];

        //    var hgt =
        //        Convert.ToSingle(
        //            Math.Max(img.Height, Convert.ToInt32(e.Graphics.MeasureString(str, Combo1.Font).Height)) * 1.1);
        //    e.ItemHeight = Convert.ToInt32(hgt);

        //    if (Combo1.DropDownStyle == ComboBoxStyle.DropDownList)
        //        Combo1.ItemHeight = Convert.ToInt32(hgt);
        //    else
        //        Combo1.ItemHeight = Convert.ToInt32(e.Graphics.MeasureString(strOneLine, Combo1.Font).Height * 1.1);

        //    e.ItemWidth = Combo1.Width;
        //}


        private void CreateButtons()
        {
            var r = 1;
            if (ButtonsRight != null && ButtonsRight.Count > 0)
            {
                foreach (DBButton button in ButtonsRight)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Left = Combo1.Width - 16 * r;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    Controls.Add(button);
                    button.BringToFront();

                    r++;
                }
            }

            var l = 0;

            if (ButtonsLeft != null && ButtonsLeft.Count > 0)
            {
                foreach (DBButton button in ButtonsLeft)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.Width = 16;
                    button.Height = 16;
                    button.Visible = true;
                    button.Left = l * 16;
                    button.Click += Button_Click;
                    button.ToolTip = button.Text;
                    button.MouseEnter += Button_MouseEnter;

                    Controls.Add(button);
                    button.BringToFront();

                    l++;
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (null != MouseEnterElement)
                MouseEnterElement(this, new DBEditorButtonEventArgs());
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (DBButton) sender;

            if (EditorButtonClick != null)
                EditorButtonClick(sender, new DBEditorButtonEventArgs());
        }

        #region Delegates

        public delegate void SelectionChangedEventHandler(object sender, EventArgs e);

        public delegate void DropDownEventHandler(object sender, EventArgs e);

        public delegate void EnterEventHandler(object sender, EventArgs e);

        public delegate void KeyDownEventHandler(object sender, KeyEventArgs e);

        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);

        public delegate void LeaveEventHandler(object sender, EventArgs e);

        public delegate void LostFocusEventHandler(object sender, EventArgs e);

        public delegate void SelectedIndexChangedEventHandler(object sender, EventArgs e);

        public delegate void SelectedValueChangedEventHandler(object sender, EventArgs e);

        public delegate void SelectionChangeCommittedEventHandler(object sender, EventArgs e);

        public delegate void TextChangedEventHandler(object sender, EventArgs e);

        public delegate void ValidatedEventHandler(object sender, EventArgs e);

        public delegate void VisibleChangedEventHandler(object sender, EventArgs e);

        public delegate void MouseEnterElementEventHandler(object sender, DBEditorButtonEventArgs e);

        #endregion

        #region Events

        public event EventHandler SelectionChanged;
        public new event EventHandler TextChanged;
        public event SelectedValueChangedEventHandler SelectedValueChanged;
        public event SelectedIndexChangedEventHandler SelectedIndexChanged;
        public new event VisibleChangedEventHandler VisibleChanged;
        public new event ValidatedEventHandler Validated;
        public new event EventHandler Enter;
        public event DropDownEventHandler DropDown;
        public new event LostFocusEventHandler LostFocus;
        public new event EventHandler GotFocus;
        public new event EventHandler MouseEnter;
        public event SelectionChangeCommittedEventHandler SelectionChangeCommitted;
        public new event KeyDownEventHandler KeyDown;
        public new event LeaveEventHandler Leave;
        public new event KeyPressEventHandler KeyPress;
        public event EventHandler AfterEnterEditMode;
        public event EventHandler AfterExitEditMode;
        public event DBEditorButtonEventHandler EditorButtonClick;
        public event MouseEnterElementEventHandler MouseEnterElement;

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        private readonly IContainer components = null;
        private ComboBox Combo1;

        public DBCombo()
        {
            InitializeComponent();

            SetStyle(ControlStyles.DoubleBuffer, true);

            Combo1.MouseUp += Combo1_MouseUp;
            Combo1.VisibleChanged += Combo1_VisibleChanged;
            //Combo1.DrawItem += DrawItem;
            //Combo1.MeasureItem += MeasureItem;
            Combo1.LostFocus += Combo1_LostFocus;
            Combo1.SelectionChangeCommitted += Combo1_SelectionChangeCommitted;
            Combo1.SelectedIndexChanged += Combo1_SelectedIndexChanged;
            Combo1.Leave += Combo1_Leave;
            Combo1.Enter += Combo1_Enter;
            Combo1.MouseDown += Combo1_MouseDown;
            Combo1.MouseEnter += Combo1_MouseEnter;
            Combo1.KeyPress += Combo1_KeyPress;
            Combo1.KeyUp += Combo1_KeyUp;
            Combo1.SelectedValueChanged += Combo1_SelectedValueChanged;
            Combo1.Validated += Combo1_Validated;
            Combo1.KeyDown += Combo1_KeyDown;
            Combo1.DropDown += Combo1_DropDown;
            Combo1.GotFocus += Combo1_GotFocus;
            Combo1.TextChanged += Combo1_TextChanged;
            base.Resize += Control_Resize;
            cmdEdit.Click += cmdEdit_Click;

            if(m_Items == null)
                m_Items = new DBComboValues(this);

            Load += DBCombo_Load;
        }

        private void DBCombo_Load(object sender, EventArgs e)
        {
            CreateButtons();
        }

        private void Combo1_MouseEnter(object sender, EventArgs e)
        {
            if (MouseEnter != null)
                MouseEnter(sender, e);
        }

        private void Combo1_GotFocus(object sender, EventArgs e)
        {
            if (null != GotFocus)
                GotFocus(sender, e);

            if (null != AfterEnterEditMode)
                AfterEnterEditMode(sender, e);
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (DataControlList != null)
            {
                var frmR = new frmRecord();
                var dbc = new DBControl();
                dbc.Parent = frmR;
                dbc.Selection = DataControlList.Selection;
                dbc.TypeDB = DataControlList.TypeDB;
                dbc.DataTable = DataControlList.DataTable;
                dbc.DataSet = DataControlList.DataSet;
                dbc.DataView = DataControlList.DataView;

                frmR.DataControl = dbc;
                frmR.AutoSize = true;
                frmR.Show();
            }
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
            this.Combo1 = new System.Windows.Forms.ComboBox();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Combo1
            // 
            this.Combo1.Location = new System.Drawing.Point(0, 0);
            this.Combo1.Name = "Combo1";
            this.Combo1.Size = new System.Drawing.Size(123, 21);
            this.Combo1.TabIndex = 1;
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(176, 0);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(16, 21);
            this.cmdEdit.TabIndex = 2;
            this.cmdEdit.Text = "E";
            // 
            // DBCombo
            // 
            this.About = null;
            this.Controls.Add(this.Combo1);
            this.Controls.Add(this.cmdEdit);
            this.Name = "DBCombo";
            this.Size = new System.Drawing.Size(227, 40);
            this.ResumeLayout(false);

        }

        public void BeginInit()
        {
            //((ISupportInitialize)Combo1).BeginInit();
        }

        public void EndInit()
        {
            //((ISupportInitialize)Combo1).EndInit();
        }

        #endregion
    }


    internal class DBComboControlDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules =>
            SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable |
            SelectionRules.Visible;
    }


    public class ComboBoxIconItem
    {
        public ComboBoxIconItem()
        {
            Text = "";
        }

        public ComboBoxIconItem(string text)
        {
            Text = text;
        }

        public ComboBoxIconItem(string text, int imageIndex)
        {
            Text = text;
            ImageIndex = imageIndex;
        }

        public string Text { get; set; }

        public int ImageIndex { get; set; }


        public override string ToString()
        {
            return Text;
        }
    }
}