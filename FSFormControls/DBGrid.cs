#region

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using FSDatabase;
using FSExcel;
using FSLibrary;
using DateTime = System.DateTime;
using FSException;

#endregion


namespace FSFormControls
{
    [ToolboxBitmap(typeof(resfinder), "FSFormControls.Resources.DBGrid.bmp")]
    [DefaultEvent("Click")]
    [ToolboxItem(true)]
    public class DBGrid : DBUserControlBase
    {
        private readonly DataGridTableStyle dgTableStyle = new DataGridTableStyle();
        private readonly ImageList imageList_DragDrop = new ImageList();
        private GridPrinter dataGridPrinter1;

        private DBRecord DbRecord1;
        public bool FilledRecord;


        private Color m_alternatingColor;

        //private int intCell = -1;
        private string m_captionText;
        private int m_columnMove = -1;
        private bool m_CustomColumnHeaders;
        private Font m_DefaultHeaderFont;
        private int m_LastRowClicked = -1;
        private AccessMode m_Mode = AccessMode.ReadMode;
        private int m_mouseDownColumn = -1;
        private int m_RowsInCaption = 2;
        private bool m_ShowTotals;
        private string m_XMLName = "";
        internal PictureBox PictureBox1;

        public string RowDraw = "";
        internal SplitContainer SplitContainer1;
        private VScrollBar vGridScrollBar;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataGrid datagrid => DataGrid1;

        public bool AutoSave { get; set; } = true;

        public int CurrentRowIndex
        {
            get { return DataGrid1.CurrentRowIndex; }
            set
            {
                if (value != -1) DataGrid1.CurrentRowIndex = value;
            }
        }

        public int DefaultDecimals { get; set; } = 2;

        public int RowsInCaption
        {
            get { return m_RowsInCaption; }
            set
            {
                if (value <= 0) return;
                m_RowsInCaption = value;
            }
        }

        public DBColumn.OperationTypes TotalOperation { get; set; } = DBColumn.OperationTypes.Sum;

        public Font DefaultHeaderFont
        {
            get
            {
                if (m_DefaultHeaderFont == null)
                    m_DefaultHeaderFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point,
                        Convert.ToByte(0));
                return m_DefaultHeaderFont;
            }
            set { m_DefaultHeaderFont = value; }
        }

        public bool CustomColumnHeaders
        {
            get { return m_CustomColumnHeaders; }
            set
            {
                if (value)
                {
                    var gf = DataGrid1.CreateGraphics();
                    DataGrid1.HeaderFont = new Font(FontFamily.GenericSerif,
                        m_RowsInCaption * DataGrid1.HeaderFont.GetHeight(gf) - 8,
                        FontStyle.Regular, GraphicsUnit.Point);
                    gf.Dispose();
                    DataGrid1.CaptionVisible = false;
                    DataGrid1.ColumnHeadersVisible = true;
                }
                else
                {
                    DataGrid1.HeaderFont = DefaultFont;
                }

                m_CustomColumnHeaders = value;
            }
        }

        public bool ShowTotals
        {
            get { return m_ShowTotals; }
            set
            {
                m_ShowTotals = value;
                DataGridTotal.Visible = value;
                SplitContainer1.Panel2Collapsed = !value;
                Resize();
            }
        }

        public int RowHeight
        {
            get { return dgTableStyle.PreferredRowHeight; }
            set { dgTableStyle.PreferredRowHeight = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DBColumnCollection Columns { get; set; }

        public Color AlternatingColor
        {
            get { return m_alternatingColor; }
            set
            {
                if (value.A != 0)
                {
                    m_alternatingColor = value;
                    dgTableStyle.AlternatingBackColor = m_alternatingColor;
                }
            }
        }

        public string CaptionText
        {
            get { return m_captionText; }
            set
            {
                m_captionText = value;
                DataGrid1.CaptionText = m_captionText;
            }
        }

        public bool ShowRecordScrollBar { get; set; } = true;

        public bool AllowSorting
        {
            get { return dgTableStyle.AllowSorting; }
            set { dgTableStyle.AllowSorting = value; }
        }

        [Description("DataBindings.")] public new ControlBindingsCollection DataBindings => DataGrid1.DataBindings;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int VisibleRowCount => DataGrid1.VisibleRowCount;

        public bool AllowAddNew { get; set; } = true;

        public bool AllowDelete { get; set; } = true;

        public bool Editable { get; set; } = true;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RowSel
        {
            get { return DataGrid1.CurrentRowIndex; }
            set
            {
                if (value >= 0) DataGrid1.CurrentRowIndex = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ColSel => DataGrid1.CurrentCell.ColumnNumber;

        public bool CaptionVisible
        {
            get { return DataGrid1.CaptionVisible; }
            set { DataGrid1.CaptionVisible = value; }
        }

        public bool RowHeadersVisible
        {
            get { return DataGrid1.RowHeadersVisible; }
            set { DataGrid1.RowHeadersVisible = value; }
        }

        public bool ColumnHeadersVisible
        {
            get { return DataGrid1.ColumnHeadersVisible; }
            set { DataGrid1.ColumnHeadersVisible = value; }
        }

        public Font HeaderFont
        {
            get { return DataGrid1.HeaderFont; }
            set { DataGrid1.HeaderFont = value; }
        }

        public Color HeaderForeColor
        {
            get { return DataGrid1.HeaderForeColor; }
            set { DataGrid1.HeaderForeColor = value; }
        }

        public Color HeaderBackColor
        {
            get { return DataGrid1.HeaderBackColor; }
            set { DataGrid1.HeaderBackColor = value; }
        }

        public Color GridLineColor
        {
            get { return DataGrid1.GridLineColor; }
            set { DataGrid1.GridLineColor = value; }
        }

        public DataGridLineStyle GridLineStyle
        {
            get { return DataGrid1.GridLineStyle; }
            set { DataGrid1.GridLineStyle = value; }
        }

        public new BorderStyle BorderStyle
        {
            get { return DataGrid1.BorderStyle; }
            set { DataGrid1.BorderStyle = value; }
        }

        public Color BackGroundColor
        {
            get { return DataGrid1.BackgroundColor; }
            set { DataGrid1.BackgroundColor = value; }
        }

        public Color CaptionBackColor
        {
            get { return DataGrid1.CaptionBackColor; }
            set { DataGrid1.CaptionBackColor = value; }
        }

        public Font CaptionFont
        {
            get { return DataGrid1.CaptionFont; }
            set { DataGrid1.CaptionFont = value; }
        }

        public Color CaptionForeColor
        {
            get { return DataGrid1.CaptionForeColor; }
            set { DataGrid1.CaptionForeColor = value; }
        }

        public bool FlatMode
        {
            get { return DataGrid1.FlatMode; }
            set { DataGrid1.FlatMode = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font DefaultFont => Control.DefaultFont;

        [Editor(typeof(DBDataGridTableStylesCollectionEditor), typeof(UITypeEditor))]
        public GridTableStylesCollection TableStyles => DataGrid1.TableStyles;

        public GridColumnStylesCollection ColumnStyles => dgTableStyle.GridColumnStyles;

        public bool RecordMode { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DataGridCell CurrentCell
        {
            get { return DataGrid1.CurrentCell; }
            set { DataGrid1.CurrentCell = value; }
        }

        public override bool AutoSize { get; set; } = true;

        public AccessMode Mode
        {
            get
            {
                AccessMode modeReturn = 0;
                modeReturn = m_Mode;
                return modeReturn;
            }
            set
            {
                m_Mode = value;
                switch (m_Mode)
                {
                    case AccessMode.ReadMode:
                        if (DataControl != null)
                            if (DataControl.DataTable != null)
                            {
                                DataControl.DataTable.DefaultView.AllowEdit = false;
                                DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                DataControl.DataTable.DefaultView.AllowNew = false;
                            }

                        if (DbRecord1 != null) DbRecord1.Mode = AccessMode.ReadMode;
                        break;
                    case AccessMode.WriteMode:
                        if (Editable)
                        {
                            if (DataControl != null)
                                if (DataControl.DataTable != null)
                                {
                                    DataControl.DataTable.DefaultView.AllowEdit = true;
                                    DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                    DataControl.DataTable.DefaultView.AllowNew = AllowAddNew;
                                }

                            DataGrid1.ReadOnly = false;
                        }

                        if (DbRecord1 != null) DbRecord1.Mode = AccessMode.WriteMode;
                        UnSelect(CurrentCell.RowNumber);
                        break;
                    case AccessMode.ProtectedMode:
                        if (DataControl != null)
                            if (DataControl.DataTable != null)
                            {
                                DataControl.DataTable.DefaultView.AllowEdit = false;
                                DataControl.DataTable.DefaultView.AllowDelete = AllowDelete;
                                DataControl.DataTable.DefaultView.AllowNew = AllowAddNew;
                            }

                        if (DbRecord1 != null) DbRecord1.Mode = AccessMode.ProtectedMode;
                        break;
                }


                ModeControls(m_Mode);
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int LastRow { get; set; } = -1;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int LastCol { get; set; } = -1;

        [Description(
            "Nombre XML del control. Esta propiedad se utiliza para la generación de código XML a partir de un formulario."
        )]
        public string XMLName
        {
            get
            {
                string xMLNameReturn = null;
                xMLNameReturn = m_XMLName;
                return xMLNameReturn;
            }
            set { m_XMLName = value; }
        }

        public new event ClickEventHandler Click;

        public new event DoubleClickEventHandler DoubleClick;

        public event ClickButtonEventHandler ClickButton;

        public event CurrentCellChangedEventHandler CurrentCellChanged;

        public event RowChangedEventHandler RowChanged;

        public event ColumnChangedEventHandler ColumnChanged;

        public event RowChangingEventHandler RowChanging;

        public event ColumnChangingEventHandler ColumnChanging;

        //public event CellKeyPressEventHandler CellKeyPress; 

        public void SetupGridPrinter()
        {
            dataGridPrinter1 = new GridPrinter(DataGrid1, PrintDocument1, DataControl.DataTable, Columns);
        }


        private void AddColumn(int col)
        {
            DataGridDBTextBoxColumn textCol;
            BdUtils db = new BdUtils(Global.ConnectionStringSetting);

            try
            {
                if ((Columns[col].FieldDB == "") & (Columns[col].ColumnType != DBColumn.ColumnTypes.Button2Column))
                    throw new ExceptionUtil(
                        "Campo DBField no definido. La propiedad DBField debe estar indicada en todas las columnas del DBGrid.");

                if (!(Columns[col].ColumnType == DBColumn.ColumnTypes.DescriptionColumn ||
                      Columns[col].ColumnType == DBColumn.ColumnTypes.FormulaColumn))
                    Columns[col].FieldDB = DataControl.FieldExactName(Columns[col].FieldDB);


                if (Columns[col].HeaderCaption == "") Columns[col].HeaderCaption = TextUtil.PCase(Columns[col].FieldDB);

                switch (Columns[col].ColumnType)
                {
                    case DBColumn.ColumnTypes.PictureColumn:
                        var picCol = new DataGridDataPictureColumn();
                        picCol.MappingName = Columns[col].FieldDB;
                        picCol.HeaderText = Columns[col].HeaderCaption;
                        picCol.Width = Convert.ToInt32(Columns[col].Width);
                        picCol.NullText = "";
                        picCol.Alignment = Columns[col].Alignment;
                        picCol.ReadOnly = Columns[col].ReadColumn;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(picCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.CheckColumn:
                    {
                        var boolCol = new DataGridBoolColumn();

                        boolCol.MappingName = Columns[col].FieldDB;
                        boolCol.HeaderText = Columns[col].HeaderCaption;
                        boolCol.Width = Convert.ToInt32(Columns[col].Width);
                        boolCol.NullText = "";
                        boolCol.NullValue = false;
                        boolCol.AllowNull = false;
                        boolCol.TrueValue = true;
                        boolCol.FalseValue = false;
                        boolCol.Alignment = Columns[col].Alignment;
                        boolCol.ReadOnly = Columns[col].ReadColumn;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(boolCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.FormulaColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        string newColumn = "_" + Columns[col].FieldDB;
                        if (!DataControl.DataTable.Columns.Contains(newColumn))
                            DataControl.DataTable.Columns.Add(newColumn, Type.GetType("System.Decimal"), Columns[col].Expression);
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[newColumn].DefaultValue = Columns[col].DefaultValue;
                        textCol.MappingName = newColumn;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.NullText = "";
                        textCol.Format = "n" + Columns[col].Decimals;
                        textCol.ReadOnly = true;
                        textCol.Alignment = HorizontalAlignment.Right;
                        Columns[col].Alignment = HorizontalAlignment.Right;
                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.TextColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        textCol.Alignment = Columns[col].Alignment;
                        textCol.ReadOnly = Columns[col].ReadColumn;
                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.TimePickerColumn:
                        var tp = new DataGridTimePickerColumn();
                        tp.MappingName = Columns[col].FieldDB;
                        tp.HeaderText = Columns[col].HeaderCaption;
                        tp.Width = 75;
                        tp.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        tp.Alignment = Columns[col].Alignment;
                        tp.ReadOnly = Columns[col].ReadColumn;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(tp);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.MoneyColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.Format = "c" + Columns[col].Decimals;
                        textCol.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        textCol.ReadOnly = Columns[col].ReadColumn;
                        textCol.Alignment = HorizontalAlignment.Right;
                        Columns[col].Alignment = HorizontalAlignment.Right;
                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.DateColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";


                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.Format = Global.DATE_FORMAT;
                        textCol.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        textCol.ReadOnly = Columns[col].ReadColumn;
                        textCol.Alignment = Columns[col].Alignment;
                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.TimeColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.Format = Global.TIME_FORMAT;
                        textCol.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        textCol.ReadOnly = Columns[col].ReadColumn;
                        textCol.Alignment = Columns[col].Alignment;
                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.FileColumn:
                        var fileCol = new DataGridFileColumn(col, Columns[col], this);

                        fileCol.MappingName = Columns[col].FieldDB;
                        fileCol.HeaderText = Columns[col].HeaderCaption;
                        fileCol.Width = Convert.ToInt32(Columns[col].Width);
                        fileCol.NullText = "";
                        fileCol.ReadOnly = Columns[col].ReadColumn;
                        fileCol.Alignment = Columns[col].Alignment;
                        fileCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(fileCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.NumberColumn:
                    case DBColumn.ColumnTypes.AutoNumericColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        if (Columns[col].ColumnType == DBColumn.ColumnTypes.AutoNumericColumn)
                        {
                            DataControl.DataTable.Columns[Columns[col].FieldDB].AutoIncrement = true;
                            DataControl.DataTable.Columns[Columns[col].FieldDB].AutoIncrementSeed =
                                Convert.ToInt64(Utils.MaxColumn(DataControl.DataTable, Columns[col].FieldDB) + 1);
                            DataControl.DataTable.Columns[Columns[col].FieldDB].AutoIncrementStep = 1;
                        }

                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.Format = "n" + Columns[col].Decimals;
                        textCol.NullText = "";
                        if (!string.IsNullOrEmpty(Columns[col].DefaultValue))
                            DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue =
                                Columns[col].DefaultValue;
                        textCol.ReadOnly = Columns[col].ReadColumn;

                        if (Columns[col].Alignment == HorizontalAlignment.Left)
                        {
                            textCol.Alignment = HorizontalAlignment.Right;
                            Columns[col].Alignment = HorizontalAlignment.Right;
                        }
                        else
                        {
                            textCol.Alignment = Columns[col].Alignment;
                        }

                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }
                        break;
                    case DBColumn.ColumnTypes.ComboColumn:
                        var comboCol = new DataGridComboBoxColumn(Columns[col], this);

                        comboCol.MappingName = Columns[col].FieldDB;
                        comboCol.HeaderText = Columns[col].HeaderCaption;
                        comboCol.Width = Convert.ToInt32(Columns[col].Width);
                        comboCol.NullText = "";

                        comboCol.ColumnComboBox.ImageList = Columns[col].ComboImageList;

                        comboCol.ColumnComboBox.DataControl = DataControl;
                        comboCol.ColumnComboBox.DataControlList = Columns[col].ColumnDBControl;
                        comboCol.ColumnComboBox.DBField = Columns[col].FieldDB;
                        comboCol.ColumnComboBox.DBFieldList = Columns[col].ComboListField;

                        if (Columns[col].ColumnDBControl != null)
                        {
                            Columns[col].ColumnDBControl.ReadOnly = true;
                            if (Columns[col].ColumnDBFieldData == "")
                                Columns[col].ColumnDBFieldData = Columns[col].ColumnDBControl.FieldName(0);
                        }
                        comboCol.ColumnComboBox.DBFieldData = Columns[col].ColumnDBFieldData;
                        comboCol.ColumnComboBox.BlankSelection = Columns[col].ComboBlankSelection;
                        comboCol.ColumnComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        comboCol.ColumnComboBox.Visible = false;

                        comboCol.ColumnComboBox.Fill();

                        if (dgTableStyle.PreferredRowHeight < comboCol.ColumnComboBox.Height)
                            dgTableStyle.PreferredRowHeight = comboCol.ColumnComboBox.Height;

                        if (Columns[col].ComboImageList != null)
                            dgTableStyle.PreferredRowHeight = Columns[col].ComboImageList.ImageSize.Height;


                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(comboCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.ButtonColumn:
                        var buttonCol = new DataGridButtonColumn(col, Columns[col]);
                        if ((Columns[col].ColumnDBControl == null) & Columns[col].ShowSelectForm)
                            Columns[col].HeaderCaption = "Error! Parámetro ColumnDBControl, no especificado.";

                        if (dgTableStyle.PreferredRowHeight < buttonCol.ButtonColumn.Height)
                            dgTableStyle.PreferredRowHeight = buttonCol.ButtonColumn.Height;

                        buttonCol.MappingName = Columns[col].FieldDB;
                        buttonCol.HeaderText = Columns[col].HeaderCaption;
                        buttonCol.Width = Convert.ToInt32(Columns[col].Width);
                        buttonCol.NullText = "";
                        buttonCol.ReadOnly = Columns[col].ReadColumn;
                        buttonCol.Alignment = HorizontalAlignment.Left;
                        Columns[col].Alignment = HorizontalAlignment.Left;
                        buttonCol.TextBox.MaxLength = Columns[col].MaxLength;
                        buttonCol.CellButtonClicked += ButtonColumnClick;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(buttonCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.Button2Column:
                        var button2Col = new DataGridButton2Column(col, Columns[col], this);
                        if (Columns[col].ColumnDBControl == null)
                            Columns[col].HeaderCaption = "Error! Parámetro ColumnDBControl, no especificado.";


                        DataControl.DataTable.Columns.Add("_" + Columns[col].HeaderCaption,
                            Type.GetType("System.Decimal"));

                        button2Col.MappingName = "_" + Columns[col].HeaderCaption;

                        button2Col.HeaderText = "Acción";

                        button2Col.Width = Convert.ToInt32(Columns[col].Width);
                        button2Col.NullText = "";
                        button2Col.ReadOnly = Columns[col].ReadColumn;
                        button2Col.Alignment = HorizontalAlignment.Left;
                        Columns[col].Alignment = HorizontalAlignment.Left;
                        button2Col.TextBox.MaxLength = Columns[col].MaxLength;
                        button2Col.CellButton2Clicked += ButtonColumn2Click;

                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(button2Col);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                    case DBColumn.ColumnTypes.DescriptionColumn:
                    {
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);

                        textCol.BackColor = Columns[col].ColumnBackColor;
                        textCol.ForeColor = Columns[col].ColumnForeColor;
                        textCol.Format = Columns[col].FormatString;
                        textCol.NullText = "";

                        var readCol = false;
                        if (!DataControl.DataTable.Columns.Contains(Columns[col].FieldDB))
                        {
                            DataControl.DataTable.Columns.Add(Columns[col].FieldDB);
                            readCol = true;
                        }
                        else
                        {
                            readCol = false;
                        }

                        Columns[col].ReadColumn = readCol;

                        try
                        {
                            switch (Columns[col].DescriptionType)
                            {
                                case DBColumn.DescriptionTypes.CheckDescription:
                                    var boolCol = new DataGridBoolColumn();

                                    boolCol.MappingName = Columns[col].FieldDB;
                                    boolCol.HeaderText = Columns[col].HeaderCaption;
                                    boolCol.Width = Convert.ToInt32(Columns[col].Width);
                                    textCol.NullText = "";
                                    boolCol.AllowNull = false;
                                    boolCol.Alignment = Columns[col].Alignment;
                                    boolCol.ReadOnly = readCol;
                                    dgTableStyle.GridColumnStyles.Add(boolCol);
                                    break;
                                case DBColumn.DescriptionTypes.DateDescription:
                                    textCol.MappingName = Columns[col].FieldDB;
                                    textCol.HeaderText = Columns[col].HeaderCaption;
                                    textCol.Width = Convert.ToInt32(Columns[col].Width);
                                    textCol.NullText = "";
                                    textCol.Format = Global.DATE_FORMAT;
                                    textCol.ReadOnly = readCol;
                                    textCol.Alignment = Columns[col].Alignment;
                                    textCol.TextBox.MaxLength = Columns[col].MaxLength;
                                    dgTableStyle.GridColumnStyles.Add(textCol);
                                    break;
                                case DBColumn.DescriptionTypes.NumberDescription:
                                    textCol.MappingName = Columns[col].FieldDB;
                                    textCol.HeaderText = Columns[col].HeaderCaption;
                                    textCol.Width = Convert.ToInt32(Columns[col].Width);
                                    textCol.NullText = "";
                                    textCol.Format = "n" + Columns[col].Decimals;
                                    textCol.ReadOnly = readCol;
                                    textCol.Alignment = HorizontalAlignment.Right;
                                    Columns[col].Alignment = HorizontalAlignment.Right;
                                    textCol.TextBox.MaxLength = Columns[col].MaxLength;
                                    dgTableStyle.GridColumnStyles.Add(textCol);
                                    break;
                                case DBColumn.DescriptionTypes.MoneyDescription:
                                    textCol.MappingName = Columns[col].FieldDB;
                                    textCol.HeaderText = Columns[col].HeaderCaption;
                                    textCol.Width = Convert.ToInt32(Columns[col].Width);
                                    textCol.NullText = "";
                                    textCol.Format = "c" + Columns[col].Decimals;
                                    textCol.ReadOnly = readCol;
                                    textCol.Alignment = HorizontalAlignment.Right;
                                    Columns[col].Alignment = HorizontalAlignment.Right;
                                    textCol.TextBox.MaxLength = Columns[col].MaxLength;
                                    dgTableStyle.GridColumnStyles.Add(textCol);
                                    break;
                                case DBColumn.DescriptionTypes.TextDescription:
                                    textCol.MappingName = Columns[col].FieldDB;
                                    textCol.HeaderText = Columns[col].HeaderCaption;
                                    textCol.Width = Convert.ToInt32(Columns[col].Width);
                                    textCol.NullText = "";
                                    textCol.ReadOnly = readCol;
                                    textCol.Alignment = Columns[col].Alignment;
                                    textCol.TextBox.MaxLength = Columns[col].MaxLength;
                                    dgTableStyle.GridColumnStyles.Add(textCol);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }
                    }

                        break;
                    case DBColumn.ColumnTypes.PercentColumn:
                        textCol = new DataGridDBTextBoxColumn(col, Columns[col], this);


                        textCol.MappingName = Columns[col].FieldDB;
                        textCol.HeaderText = Columns[col].HeaderCaption;
                        textCol.Width = Convert.ToInt32(Columns[col].Width);
                        textCol.Format = "p0";
                        textCol.NullText = "";
                        DataControl.DataTable.Columns[Columns[col].FieldDB].DefaultValue = "0";
                        textCol.ReadOnly = Columns[col].ReadColumn;
                        textCol.Alignment = Columns[col].Alignment;

                        textCol.TextBox.MaxLength = Columns[col].MaxLength;
                        try
                        {
                            dgTableStyle.GridColumnStyles.Add(textCol);
                        }
                        catch (Exception e)
                        {
                            throw new ExceptionUtil("Error columna: " + Columns[col].FieldDB, e);
                        }

                        break;
                }


                if (DataControl.DataTable != null)
                    try
                    {
                        var dtcol = DataControl.DataTable.Columns[Columns[col].FieldDB];
                        if (dtcol != null) dtcol.Unique = Columns[col].Unique;
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }

                if (Columns[col].Hidden) HideColumn(Columns[col].FieldDB);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil("Error al Añadir la columna: " + Columns[col].FieldDB, e);
            }
        }


        public bool IsSelected(int row)
        {
            return DataGrid1.IsSelected(row);
        }


        public new void Select()
        {
            DataGrid1.Select();
        }


        public void Select(int row)
        {
            try
            {
                DataGrid1.Select(row);
            }
            catch
            {
            }
        }


        public void ButtonColumnClick(object sender, DataGridCellButtonClickEventArgs e, ref object value)
        {
            var frm = new frmListView();
            var selectcol = e.ColIndex;
            var selectrow = e.RowIndex;
            var f = 0;

            if (Columns[selectcol].ShowSelectForm)
            {
                if (Columns[selectcol].ColumnDBControl == null)
                    throw new ExceptionUtil("[" + Columns[selectcol].FieldDB + "] ButtonBox sin DBControl asociado.");

                if (Columns[selectcol].ColumnType == DBColumn.ColumnTypes.DescriptionColumn)
                {
                    if (Columns[selectcol].AsociatedButtonColumn != -1)
                        if (Columns[Columns[selectcol].AsociatedButtonColumn].ColumnDBControl != null)
                            frm.DataControl = Columns[Columns[selectcol].AsociatedButtonColumn].ColumnDBControl;
                }
                else
                {
                    frm.DataControl = Columns[selectcol].ColumnDBControl;
                }

                frm.ShowDialog();

                if (Mode == AccessMode.WriteMode)
                    if (frm.SelectedRow != null)
                        for (f = 0; f <= Columns.Count - 1; f++)
                            if (Columns[f].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                                if (Columns[f].ColumnDBControl.NameControl() == frm.DataControl.NameControl())
                                {
                                    if (!string.IsNullOrEmpty(Columns[f].ColumnDBFieldData))
                                        try
                                        {
                                            DataControl.Action = DBControl.DbActionTypes.Change;
                                            value = frm.SelectedRow[Columns[f].ColumnDBFieldData];
                                            DataControl.DataTable.DefaultView[selectrow].Row[Columns[f].FieldDB] =
                                                value;
                                            UpdateAsociatedColumns(DataControl.DataTable.DefaultView[selectrow].Row,
                                                true, true);
                                            DataControl.Action = DBControl.DbActionTypes.None;
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new ExceptionUtil(ex);
                                        }
                                    else
                                        throw new ExceptionUtil(
                                            "Campo 'ColumnDBFieldData' no especificado en la columna: " + f);
                                }

                frm.Close();
                frm = null;
            }

            if (null != ClickButton) ClickButton(selectcol, selectrow);
        }


        public void ButtonColumn2Click(object sender, DataGridCellButton2ClickEventArgs e)
        {
            var selectcol = e.ColIndex;
            var selectrow = e.RowIndex;

            if (null != ClickButton) ClickButton(selectcol, selectrow);
        }


        private void HandleCellButtonClick(object sender, DataGridCellButtonClickEventArgs e)
        {
            MessageBox.Show("row " + e.RowIndex + "  col " + e.ColIndex + " clicked.");
        }


        private ArrayList GetDataGridColumns()
        {
            var htblCols = new Hashtable();
            var alistCols = new ArrayList();


            if (DataGrid1.DataSource != null)
                if (DataGrid1.DataSource.GetType() == typeof(DataTable))
                {
                    var dtbl = (DataTable) DataGrid1.DataSource;

                    foreach (DataColumn dcol in dtbl.Columns) alistCols.Add(dcol.ColumnName);
                }

            return alistCols;
        }


        public void ResizeColumns()
        {
            if (AutoSize)
                FunctionsGrid.AutoSizeColumnsToContent(DataControl, Columns, DataGrid1.CreateGraphics(),
                    DataGrid1.Font, dgTableStyle.GridColumnStyles);
            else
                FunctionsGrid.ColumnsSetSize(Columns, DataControl, true);
        }


        public void Print()
        {
            try
            {
                var pd = new GridPrintDocument(this, DataControl.DataTable.DefaultView,
                    DataControl.DataTable, -1, null);

                if (PageSetup.PageSettings != null) pd.DefaultPageSettings = PageSetup.PageSettings;


                var dlg = new PrintDialog();
                dlg.Document = pd;
                var result = dlg.ShowDialog();

                if (result == DialogResult.OK) pd.Print();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void PrintPreview()
        {
            try
            {
                var pd = new GridPrintDocument(this, DataControl.DataTable.DefaultView,
                    DataControl.DataTable, 25,
                    "Desea visualizar más páginas antes de imprimir?");

                if (!(PageSetup.PageSettings == null)) pd.DefaultPageSettings = PageSetup.PageSettings;

                var dlg = new frmPrintPreviewExport();
                dlg.TableDocument = pd;
                dlg.WindowState = FormWindowState.Maximized;
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void DataGrid1_Click(object sender, EventArgs e)
        {
            UpdateDBControl();
            if (null != Click) Click(this, e);
        }


        private void DataGrid1_Leave(object sender, EventArgs e)
        {
            if (AutoSave)
                if (DataControl != null)
                    if (DataControl.HasDataToSave() != null)
                        DataControl.Save();
        }


        private void DataGrid1_Paint(object sender, PaintEventArgs e)
        {
            if (m_CustomColumnHeaders) DrawHeader(e.Graphics, true, e.ClipRectangle, 0, 0);
        }


        private void DataGrid1_Scroll(object sender, EventArgs e)
        {
            DataGrid1.Invalidate();
        }


        public void DrawHeader(Graphics g, int OffsetX, int OffsetY)
        {
            DrawHeader(g, false, new Rectangle(), OffsetX, OffsetY);
        }


        private void DrawHeader(Graphics g, bool CallNativePaint, Rectangle ClipRect, int OffsetX, int OffsetY)
        {
            var rs = new Rectangle();
            var rs2 = new Rectangle();
            var rs3 = new Rectangle();

            if (g == null) g = DataGrid1.CreateGraphics();
            PaintEventArgs ev = null;
            var top = DataGrid1.HeaderFont.Height;
            if (CallNativePaint)
            {
                ev = new PaintEventArgs(g, ClipRect);
                base.OnPaint(ev);
            }

            if (ClipRect.Top < top)
            {
                var b = new SolidBrush(DataGrid1.HeaderBackColor);
                var b2 = new SolidBrush(DataGrid1.HeaderForeColor);
                var b3 = new SolidBrush(DataGrid1.BackgroundColor);
                var x = DataGrid1.RowHeaderWidth + 4;
                var i = 0;
                int w = 0, r = 0;
                var dx = 0;
                rs3 = new Rectangle(0, 2, x - 2, top + 6);
                for (i = DataGrid1.FirstVisibleColumn;
                    i <= DataGrid1.FirstVisibleColumn + DataGrid1.VisibleColumnCount - 1;
                    i++)
                {
                    if (i == DataGrid1.FirstVisibleColumn)
                        try
                        {
                            w = DataGrid1.GetCellBounds(DataGrid1.CurrentCell.RowNumber, i).Right - x - 1;
                        }
                        catch
                        {
                        }
                    else
                        try
                        {
                            w = DataGrid1.TableStyles[0].GridColumnStyles[i].Width;
                        }
                        catch
                        {
                        }

                    r = w + 2;

                    if (i == DataGrid1.FirstVisibleColumn)
                        dx = -4;
                    else
                        dx = 0;
                    rs = new Rectangle(x + 2 + dx, 3, r - 3 - dx, top + 3);
                    if (DataGrid1.FirstVisibleColumn + DataGrid1.VisibleColumnCount - 1 == i)
                        rs2 = new Rectangle(x + 1 + dx, 2, r - dx - 1, top + 6);
                    else
                        rs2 = new Rectangle(x + 1 + dx, 2, r - dx, top + 6);
                    g.FillRectangle(b, rs);
                    ControlPaint.DrawBorder3D(g, rs2);
                    PaintHeaderCell(g, DataGrid1.TableStyles[0].GridColumnStyles[i].HeaderText, DefaultHeaderFont, b2,
                        rs, OffsetX, OffsetY);
                    x += w;
                    if (DataGrid1.FirstVisibleColumn + DataGrid1.VisibleColumnCount - 1 == i)
                    {
                        var pn = new Pen(DataGrid1.BackColor);
                        g.DrawLine(pn, x + 1, 2, x + 1, top + 6);
                        pn.Dispose();
                    }
                }

                var p = new Pen(DataGrid1.BackgroundColor);
                g.DrawLine(p, x + 2, 2, x + 2, top + 7);
                p.Dispose();
                b.Dispose();
                b2.Dispose();
                b3.Dispose();
            }
        }


        public void PaintHeaderCell(Graphics g, string Sentence, Font F, Brush br, Rectangle Bounds, int offsetx,
            int offsety)
        {
            var strs = new string[m_RowsInCaption - 1 + 1];
            string[] tt = null;
            tt = Sentence.Split(' ');
            int i = 0, j = 0, n = 0;
            j = 0;
            var s = "";
            string os = null;
            os = s;
            for (i = 0; i <= m_RowsInCaption - 1; i++) strs[i] = "";
            for (i = 0; i <= tt.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    s += " " + tt[i];
                }
                else
                {
                    s = tt[i];
                    os = s;
                }

                var w = Convert.ToInt32(g.MeasureString(s, F).Width);
                if (w >= Bounds.Width)
                {
                    strs[j] = os;
                    if (s != tt[i])
                    {
                        s = tt[i];
                        os = tt[i];
                    }
                    else
                    {
                        s = "";
                        os = "";
                    }

                    if ((j == m_RowsInCaption - 1) & (i <= tt.Length - 1))
                    {
                        strs[j] = "...";
                        break;
                    }

                    j += 1;
                }
                else
                {
                    os = s;
                }

                if ((i == tt.Length - 1) & (j != m_RowsInCaption)) strs[j] = s;
            }

            n = m_RowsInCaption - 1;
            var h = Bounds.Height % F.Height / 2;
            for (i = 0; i <= n; i++)
            {
                var w = Convert.ToInt32(g.MeasureString(strs[i], F).Width);
                if (w < Bounds.Width)
                {
                    w = (Bounds.Width - w) / 2;
                }
                else
                {
                    if (Bounds.Width > 0)
                        while (w >= Bounds.Width)
                        {
                            strs[i] = strs[i].Substring(0, strs[i].Length - 1);
                            w = Convert.ToInt32(g.MeasureString(strs[i], F).Width);
                        }
                    else
                        return;

                    w = 0;
                }

                g.DrawString(strs[i], F, br, Bounds.X + w + offsetx, Bounds.Y + h + offsety);
                h += F.Height;
            }
        }


        private void SelectColumns()
        {
            var alistCols = GetDataGridColumns();

            if (alistCols.Count < 1) return;

            var frmSlt = new frmColumns();

            if (DataGrid1.TableStyles.Count < 1) return;

            var tblStyle = DataGrid1.TableStyles[0];

            var colStyles = tblStyle.GridColumnStyles;

            if (colStyles.Count < 1) return;


            foreach (DataGridColumnStyle colStyle in colStyles)
                if (!colStyle.MappingName.StartsWith("hidden_"))
                    frmSlt.lboxSelected.Items.Add(colStyle.HeaderText);
                else
                    frmSlt.lboxNSelected.Items.Add(colStyle.HeaderText);

            var res = frmSlt.ShowDialog();
            var cols = new ArrayList();


            if (res == DialogResult.OK)
            {
                foreach (DataGridColumnStyle colStyle in colStyles)
                    if (frmSlt.lboxNSelected.Items.Contains(colStyle.HeaderText))
                        HideColumn(Columns.FindByHeaderCaption(colStyle.HeaderText).FieldDB);
                    else
                        ShowColumn(Columns.FindByHeaderCaption(colStyle.HeaderText).FieldDB);

                foreach (var lItem in frmSlt.lboxSelected.Items)
                foreach (DataGridColumnStyle colStyle in colStyles)
                    if (colStyle.HeaderText == Convert.ToString(lItem))
                        cols.Add(colStyle);

                foreach (var lItem in frmSlt.lboxNSelected.Items)
                foreach (DataGridColumnStyle colStyle in colStyles)
                    if (colStyle.HeaderText == Convert.ToString(lItem))
                        cols.Add(colStyle);
                colStyles.Clear();

                foreach (DataGridColumnStyle dgcs in cols) colStyles.Add(dgcs);
            }

            frmSlt.Close();
            frmSlt = null;
        }


        private void ShowTotal()
        {
            if (DataGridTotal.DataSource != null) return;

            int f = 0;
            DataTable dt;
            var dgts = new DataGridTableStyle();

            try
            {
                dt = DataControl.DataTable.Clone();
                DataGridTotal.DataSource = dt;

                dgts.MappingName = "Dt" + DataControl.Name;

                for (f = 0; f <= dgTableStyle.GridColumnStyles.Count - 1; f++)
                {
                    var dgcs = new DataGridDBTextBoxColumn();

                    if (dgTableStyle.GridColumnStyles[f] is DataGridPercentColumn)
                    {
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        dgcs.HeaderText = dgTableStyle.GridColumnStyles[f].HeaderText;
                        dgcs.Alignment = dgTableStyle.GridColumnStyles[f].Alignment;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.Format = "p0";
                    }
                    else if (dgTableStyle.GridColumnStyles[f] is DataGridButtonColumn)
                    {
                        dgcs.HeaderText = "";
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                    }
                    else if (dgTableStyle.GridColumnStyles[f] is DataGridBoolColumn)
                    {
                        dgcs.HeaderText = "";
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                    }
                    else if (dgTableStyle.GridColumnStyles[f] is DataGridComboBoxColumn)
                    {
                        dgcs.HeaderText = "";
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                    }
                    else if (dgTableStyle.GridColumnStyles[f] is DataGridDBTextBoxColumn)
                    {
                        dgcs.HeaderText = "";
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        if (!string.IsNullOrEmpty(dgcs.MappingName))
                            if ((Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                                 DBColumn.ColumnTypes.FormulaColumn) |
                                (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                                 DBColumn.ColumnTypes.MoneyColumn) |
                                (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                                 DBColumn.ColumnTypes.NumberColumn) |
                                (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                                 DBColumn.ColumnTypes.PercentColumn) |
                                (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].DescriptionType ==
                                 DBColumn.DescriptionTypes.NumberDescription))
                                dgcs.HeaderText = dgTableStyle.GridColumnStyles[f].HeaderText;
                        dgcs.Alignment = dgTableStyle.GridColumnStyles[f].Alignment;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.Format = ((DataGridDBTextBoxColumn) dgTableStyle.GridColumnStyles[f]).Format;
                    }
                    else if (dgTableStyle.GridColumnStyles[f] is DataGridTextBoxColumn)
                    {
                        dgcs.MappingName = dgTableStyle.GridColumnStyles[f].MappingName;
                        if ((Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                             DBColumn.ColumnTypes.FormulaColumn) |
                            (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                             DBColumn.ColumnTypes.MoneyColumn) |
                            (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                             DBColumn.ColumnTypes.NumberColumn) |
                            (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].ColumnType ==
                             DBColumn.ColumnTypes.PercentColumn) |
                            (Columns[Columns.GetColumnOrdinal(dgcs.MappingName)].DescriptionType ==
                             DBColumn.DescriptionTypes.NumberDescription))
                            dgcs.HeaderText = dgTableStyle.GridColumnStyles[f].HeaderText;
                        else
                            dgcs.HeaderText = "";
                        dgcs.Alignment = dgTableStyle.GridColumnStyles[f].Alignment;
                        dgcs.NullText = dgTableStyle.GridColumnStyles[f].NullText;
                        dgcs.Width = dgTableStyle.GridColumnStyles[f].Width;
                        dgcs.Format = ((DataGridTextBoxColumn) dgTableStyle.GridColumnStyles[f]).Format;
                    }

                    dgts.GridColumnStyles.Add(dgcs);
                }

                DataGridTotal.TableStyles.Add(dgts);

                var dr = dt.NewRow();

                for (f = 0; f <= ColumnStyles.Count - 1; f++)
                    if ((Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                         DBColumn.ColumnTypes.FormulaColumn) |
                        (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                         DBColumn.ColumnTypes.MoneyColumn) |
                        (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                         DBColumn.ColumnTypes.NumberColumn) |
                        (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                         DBColumn.ColumnTypes.PercentColumn) |
                        (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].DescriptionType ==
                         DBColumn.DescriptionTypes.NumberDescription))
                        switch (TotalOperation)
                        {
                            case DBColumn.OperationTypes.Sum:
                                dr[ColumnStyles[f].MappingName] =
                                    Utils.SumColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Average:
                                dr[ColumnStyles[f].MappingName] =
                                    Utils.AvgColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Max:
                                dr[ColumnStyles[f].MappingName] = Utils.MaxColumn(DataControl.DataTable,
                                    ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Min:
                                dr[ColumnStyles[f].MappingName] =
                                    Utils.MinColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                        }

                dt.Rows.Add(dr);

                Resize();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public void RefreshTotals()
        {
            BdUtils db = new BdUtils(Global.ConnectionStringSetting);
            var f = 0;

            if (DataGridTotal.DataSource == null) return;

            for (f = 0; f <= ColumnStyles.Count - 1; f++)
                if ((Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                     DBColumn.ColumnTypes.FormulaColumn) |
                    (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                     DBColumn.ColumnTypes.MoneyColumn) |
                    (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                     DBColumn.ColumnTypes.NumberColumn) |
                    (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].ColumnType ==
                     DBColumn.ColumnTypes.PercentColumn) |
                    (Columns[Columns.GetColumnOrdinal(ColumnStyles[f].MappingName)].DescriptionType ==
                     DBColumn.DescriptionTypes.NumberDescription))
                    if (ColumnStyles[f].ReadOnly == false)
                        switch (TotalOperation)
                        {
                            case DBColumn.OperationTypes.Sum:
                                ((DataTable) DataGridTotal.DataSource).Rows[0][ColumnStyles[f].MappingName] =
                                    Utils.SumColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Average:
                                ((DataTable) DataGridTotal.DataSource).Rows[0][ColumnStyles[f].MappingName] =
                                    Utils.AvgColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Max:
                                ((DataTable) DataGridTotal.DataSource).Rows[0][ColumnStyles[f].MappingName] =
                                    Utils.MaxColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                            case DBColumn.OperationTypes.Min:
                                ((DataTable) DataGridTotal.DataSource).Rows[0][ColumnStyles[f].MappingName] =
                                    Utils.MinColumn(DataControl.DataTable, ColumnStyles[f].MappingName);
                                break;
                        }
        }


        private void CopySelectedRowsToClipboard()
        {
            var iRow = 0;
            //int iCol = 0; 
            StringBuilder sb = null;

            var cm =
                (CurrencyManager) DataGrid1.BindingContext[DataGrid1.DataSource, DataGrid1.DataMember];
            var iRowCount = 0;
            var iMaxColIndex = 0;

            sb = new StringBuilder();

            try
            {
                sb.Append(GetHeaderRow());

                if (cm != null)
                {
                    iRowCount = cm.Count - 1;
                    iMaxColIndex = GetMaxColumnIndex();

                    while (iRow <= iRowCount)
                    {
                        if (DataGrid1.IsSelected(iRow)) sb.Append(GetGridRow(iRow, iMaxColIndex));
                        iRow += 1;
                    }
                }

                Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private void CopySingleRowToClipboard()
        {
            if (m_LastRowClicked < 0) return;

            var iRow = m_LastRowClicked;
            //int iCol = 0; 
            var iMaxColIndex = 0;

            var sb = new StringBuilder();


            try
            {
                sb.Append(GetHeaderRow());

                iMaxColIndex = GetMaxColumnIndex();
                sb.Append(GetGridRow(iRow, iMaxColIndex));

                Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private void CopyAllRowsToClipboard()
        {
            var iRow = 0;
            //int iCol = 0; 
            var cm =
                (CurrencyManager) DataGrid1.BindingContext[DataGrid1.DataSource, DataGrid1.DataMember];
            StringBuilder sb = null;
            var iRowCount = 0;
            var iMaxColIndex = 0;


            sb = new StringBuilder();

            try
            {
                sb.Append(GetHeaderRow());

                if (cm != null)
                {
                    iRowCount = cm.Count - 1;
                    iMaxColIndex = GetMaxColumnIndex();

                    while (iRow <= iRowCount)
                    {
                        sb.Append(GetGridRow(iRow, iMaxColIndex));
                        iRow += 1;
                    }
                }

                Clipboard.SetDataObject(sb.ToString(), true);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
            finally
            {
                m_LastRowClicked = -1;
            }
        }


        private int GetMaxColumnIndex()
        {
            var i = 0;
            object obj = null;

            try
            {
                do
                {
                    obj = DataGrid1[0, i];
                    i += 1;
                } while (true);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (i > 0)
                    return i;
                return -1;
            }
            catch
            {
                return -1;
            }
        }


        private string GetGridRow(int iRow, int iMaxColIndex)
        {
            StringBuilder sb = null;
            var iCol = 0;
            string strCell = null;

            sb = new StringBuilder();

            try
            {
                for (iCol = 0; iCol <= iMaxColIndex; iCol++)
                {
                    try
                    {
                        strCell = DataGrid1[iRow, iCol].ToString();
                    }
                    catch
                    {
                        strCell = "";
                    }

                    sb.Append(strCell);
                    sb.Append(Global.Tab);
                }

                sb.Append("\r\n");
            }
            catch
            {
            }

            return sb.ToString();
        }


        private string GetHeaderRow()
        {
            DataGridTableStyle ts = null;

            StringBuilder sb = null;

            sb = new StringBuilder();

            try
            {
                ts = dgTableStyle;

                if (ts == null) return string.Empty;

                foreach (DataGridColumnStyle col in ts.GridColumnStyles)
                {
                    sb.Append(col.HeaderText);
                    sb.Append(Global.Tab);
                }

                sb.Append("\r\n");
            }
            catch
            {
            }

            return sb.ToString();
        }


        private void DataGridMouseDownEventHandler(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var myGrid = (DataGrid) sender;
            DataGrid.HitTestInfo hti = null;
            hti = myGrid.HitTest(e.X, e.Y);
            if (hti.Type == DataGrid.HitTestType.ColumnHeader)
            {
                var mItem = new MenuItem();
                var mItem2 = new MenuItem();
                var subMenu1 = new MenuItem();
                mItem.Text = "Editar columnas";
                mItem2.Text = "Ocultar columna";
                subMenu1.Text = "Mostrar columna";

                var i = 0;
                for (i = 0; i <= Columns.Count - 1; i++)
                {
                    subMenu1.MenuItems.Add(Columns[i].HeaderCaption).Checked = !Columns[i].Hidden;
                    subMenu1.MenuItems[i].Click += MnuShowColumn;
                }

                mItem.Click += MnuColumnEdit;
                mItem2.Click += MenuItem2_EventHandler;

                var cMenu = new ContextMenu();
                cMenu.MenuItems.Add(mItem);
                cMenu.MenuItems.Add(mItem2);

                cMenu.MenuItems.Add(subMenu1);

                cMenu.Show(this, new Point(e.X, e.Y));
            }
        }


        public void MnuShowColumn(object sender, EventArgs e)
        {
            if (((MenuItem) sender).Checked)
                HideColumn(Columns.FindByHeaderCaption(((MenuItem) sender).Text).FieldDB);
            else
                ShowColumn(Columns.FindByHeaderCaption(((MenuItem) sender).Text).FieldDB);
        }


        public void MnuColumnEdit(object sender, EventArgs e)
        {
            SelectColumns();
        }


        public void MnuRefrescar(object sender, EventArgs e)
        {
            Refresh();
        }


        public void MenuItem2_EventHandler(object sender, EventArgs e)
        {
            HideColumn(m_columnMove);
        }


        public void Fill()
        {
            var f = 0;

            if (DataControl == null)
                return; // throw new ExceptionUtil("[" + Name + "] DBGrid sin DBControl asociado.");

            BdUtils db = new BdUtils(Global.ConnectionStringSetting);

            DataControl.AsociatedDBGrid = this;
            DataControl.OnReConnect += DataControl_OnReConnect;

            if (!DataControl.Connected)
                DataControl.Connect();
                //throw new ExceptionUtil("DataControl no conectado.");

            DataControl.Action = DBControl.DbActionTypes.Fill;

            if (DataControl.DBFieldData == "") DataControl.DBFieldData = DataControl.FieldName(0);

            DataGrid1.TableStyles.Clear();
            dgTableStyle.GridColumnStyles.Clear();

            DBColumn dbc1 = null;

            if (Columns.Count == 0)
            {
                if (DataControl.ColumnMapping.Count == 0)
                    for (f = 0; f <= Convert.ToInt32(DataControl.FieldsCount() - 1); f++)
                    {
                        dbc1 = new DBColumn(DataControl.FieldName(f), TextUtil.PCase(DataControl.FieldName(f)),
                            FunctionsForms.ConvertFieldTypeToColumnType(db.GetField(DataControl.FieldName(f),
                                DataControl.TableName).Tipo));
                        dbc1.Decimals = DefaultDecimals;
                        Columns.Add(dbc1);
                    }
                else
                    for (f = 0; f <= DataControl.ColumnMapping.Count - 1; f++)
                        Columns.Add(DataControl.ColumnMapping[f].FieldDB, DataControl.ColumnMapping[f].HeaderCaption);
            }

            if (AutoSize)
                FunctionsGrid.AutoSizeColumnsToContent(DataControl, Columns, DataGrid1.CreateGraphics(),
                    DataGrid1.Font, ColumnStyles);
            else
                FunctionsGrid.ColumnsSetSize(Columns, DataControl, false);

            for (f = 0; f <= Columns.Count - 1; f++)
                try
                {
                    AddColumn(f);
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            FillDescriptionColumns();

            dgTableStyle.MappingName = "Dt" + DataControl.Name;
            DataGrid1.TableStyles.Clear();
            DataGrid1.TableStyles.Add(dgTableStyle);
            DataGrid1.DataSource = DataControl.DataTable;

            FillContextMenu();

            if (RecordMode) FillRecord();

            if (!DataControl.Paging)
            {
                cmdPageFirst.Visible = false;
                cmdPageLast.Visible = false;
                cmdPageNext.Visible = false;
                cmdPagePrevious.Visible = false;
                lblPage.Visible = false;
            }
            else
            {
                cmdPageFirst.Visible = true;
                cmdPageLast.Visible = true;
                cmdPageNext.Visible = true;
                cmdPagePrevious.Visible = true;
                lblPage.Visible = true;
                UpdatePage();

                vGridScrollBar = (VScrollBar) DataGrid1.Controls[1];
                vGridScrollBar.Maximum = Convert.ToInt32(DataControl.RecordCount());
                vGridScrollBar.LargeChange = DataControl.PagingSize;
            }

            DataControl.DataTable.ColumnChanging += ColumnChangingEvt;
            DataControl.DataTable.RowChanging += RowChangingEvt;
            DataControl.DataTable.ColumnChanged += ColumnChangedEvt;
            DataControl.DataTable.RowChanged += RowChangedEvt;

            DataControl.Action = DBControl.DbActionTypes.None;

            if (DataControl.ColumnMapping.Count == 0)
                for (f = 0; f <= Columns.Count - 1; f++)
                    DataControl.ColumnMapping.Add(Columns[f].FieldDB, Columns[f].HeaderCaption);

            SetupGridPrinter();

            if (ShowTotals)
            {
                ShowTotal();
                SplitContainer1.Panel2Collapsed = false;
            }
            else
            {
                SplitContainer1.Panel2Collapsed = true;
            }

            Resize();
        }


        private void ColumnChangingEvt(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;

                CheckObligatoryColumn(e);

                if (null != ColumnChanging) ColumnChanging(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void RowChangingEvt(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;


                CheckObligatoryRow(e.Row);

                if (null != RowChanging)
                    RowChanging(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void RowChangedEvt(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change)
                    return;

                DataControl.Action = DBControl.DbActionTypes.Change;

                if (null != RowChanged)
                    RowChanged(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void ColumnChangedEvt(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (DataControl.Action == DBControl.DbActionTypes.Change) return;
                DataControl.Action = DBControl.DbActionTypes.Change;

                if (Columns.Find(e.Column.ColumnName) != null)
                    switch (Columns.Find(e.Column.ColumnName).ColumnType)
                    {
                        case DBColumn.ColumnTypes.ButtonColumn:
                            UpdateAsociatedColumns(e.Row, false, true);
                            break;
                    }

                if (null != ColumnChanged) ColumnChanged(this, e);
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            RefreshTotals();

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        private void CheckUniqueRow(DataRow row)
        {
            var arr = new ArrayList();
            var arrRow = new ArrayList();
            var g = 0;
            var f = 0;

            for (f = 0; f <= Columns.Count - 1; f++)
                if (Columns[f].Unique)
                    arr.Add(row[Columns[f].FieldDB, DataRowVersion.Proposed]);

            if (arr.Count == 0) return;

            for (f = 0; f <= DataControl.DataTable.Rows.Count - 1; f++)
            {
                arrRow.Clear();

                for (g = 0; g <= Columns.Count - 1; g++)
                    if (Columns[g].Unique)
                        arrRow.Add(DataControl.DataTable.Rows[f][Columns[g].FieldDB, DataRowVersion.Current]);

                if (Functions.ArrayListCompare(arr, arrRow))
                {
                    row.RowError = "Clave duplicada!";
                    break;
                }

                row.RowError = "";
            }
        }


        private void CheckObligatoryRow(DataRow row)
        {
            if (row.RowState == DataRowState.Unchanged) return;
            if (row.RowState == DataRowState.Deleted) return;
            if (row.RowState == DataRowState.Detached) return;

            foreach (DBColumn dbc in Columns)
                if (dbc.Obligatory)
                {
                    if (row[dbc.FieldDB] is DBNull)
                        row.SetColumnError(dbc.FieldDB, "Columna obligatoria!");
                    else
                        row.SetColumnError(dbc.FieldDB, "");
                }
        }


        private void CheckObligatoryColumn(DataColumnChangeEventArgs e)
        {
            if (Columns.Find(e.Column.ColumnName) != null)
                if (Columns.Find(e.Column.ColumnName).Obligatory)
                    if (e.ProposedValue != DBNull.Value)
                        e.Row.SetColumnError(e.Column, "");
        }


        public void UpdateAsociatedColumns(int row, bool updateDescriptionColumn, bool acceptChanges)
        {
            try
            {
                if (DataControl.DataTable.Rows.Count > row)
                    UpdateAsociatedColumns(DataControl.DataTable.Rows[row], updateDescriptionColumn, acceptChanges);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public void UpdateAsociatedColumns(DataRow row, bool updateDescriptionColumn, bool acceptChanges)
        {
            string findField = null;

            if (row == null) return;

            foreach (DBColumn col in Columns)
                if (col.ColumnType == DBColumn.ColumnTypes.DescriptionColumn)
                    try
                    {
                        if (col.AsociatedButtonColumn != -1)
                        {
                            //si tiene una columna con DBControl, lo conectamos.
                            if (Columns[col.AsociatedButtonColumn].ColumnDBControl != null)
                                if (Columns[col.AsociatedButtonColumn].ColumnDBControl.Connected == false)
                                    Columns[col.AsociatedButtonColumn].ColumnDBControl.Connect();

                            if (Columns[col.AsociatedButtonColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                                if (col.ReadColumn | updateDescriptionColumn)
                                {
                                    if (Columns[col.AsociatedButtonColumn].ColumnDBFieldData != "")
                                        findField = Columns[col.AsociatedButtonColumn].ColumnDBFieldData;
                                    else
                                        findField = Columns[col.AsociatedButtonColumn].FieldDB;
                                    if (string.IsNullOrEmpty(col.FormatString))
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] =
                                                    DateTime.Parse(
                                                        Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] =
                                                    NumberUtils.NumberDouble(
                                                        Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] =
                                                    NumberUtils.NumberByte(
                                                        Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.TextDescription:
                                                row[col.FieldDB] =
                                                    Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                        findField,
                                                        row[Columns[col.AsociatedButtonColumn].FieldDB].ToString(),
                                                        col.FieldDB);
                                                break;
                                        }
                                    else
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        DateTime.Parse(
                                                            Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedButtonColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        NumberUtils.NumberDouble(
                                                            Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedButtonColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        NumberUtils.NumberByte(
                                                            Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedButtonColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                        }
                                }
                        }

                        if (col.AsociatedComboColumn != -1)
                            if (Columns[col.AsociatedComboColumn].ColumnType == DBColumn.ColumnTypes.ComboColumn)
                                if (col.ReadColumn | updateDescriptionColumn)
                                {
                                    if (
                                        !string.IsNullOrEmpty(Columns[col.AsociatedComboColumn].ColumnDBFieldData))
                                        findField = Columns[col.AsociatedComboColumn].ColumnDBFieldData;
                                    else
                                        findField = Columns[col.AsociatedComboColumn].FieldDB;
                                    if (col.FormatString == "")
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] =
                                                    DateTime.Parse(
                                                        Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] =
                                                    NumberUtils.NumberDouble(
                                                        Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] =
                                                    NumberUtils.NumberByte(
                                                        Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                            findField,
                                                            row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                            col.FieldDB));
                                                break;
                                            case DBColumn.DescriptionTypes.TextDescription:
                                                row[col.FieldDB] =
                                                    Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                        findField,
                                                        row[Columns[col.AsociatedComboColumn].FieldDB].ToString(),
                                                        col.FieldDB);
                                                break;
                                        }
                                    else
                                        switch (col.DescriptionType)
                                        {
                                            case DBColumn.DescriptionTypes.DateDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        DateTime.Parse(
                                                            Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedComboColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.NumberDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        NumberUtils.NumberDouble(
                                                            Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedComboColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                            case DBColumn.DescriptionTypes.CheckDescription:
                                                row[col.FieldDB] =
                                                    string.Format("{0:" + col.FormatString + "}",
                                                        NumberUtils.NumberByte(
                                                            Columns[col.AsociatedComboColumn].ColumnDBControl.Find(
                                                                findField,
                                                                row[
                                                                    Columns[
                                                                        col.AsociatedComboColumn
                                                                    ].FieldDB].ToString(),
                                                                col.FieldDB)));
                                                break;
                                        }
                                }
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }
                else if (col.ColumnType != DBColumn.ColumnTypes.FormulaColumn)
                    try
                    {
                        if (col.AsociatedButtonColumn != -1)
                            if (Columns[col.AsociatedButtonColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                            {
                                if (!string.IsNullOrEmpty(Columns[col.AsociatedButtonColumn].ColumnDBFieldData))
                                    findField = Columns[col.AsociatedButtonColumn].ColumnDBFieldData;
                                else
                                    findField = Columns[col.AsociatedButtonColumn].FieldDB;

                                bool doIt;

                                if (DataControl.DataTable.Rows.Count > CurrentCell.RowNumber)
                                    doIt = !Convert.IsDBNull(
                                        DataControl.DataTable.Rows[CurrentCell.RowNumber][col.FieldDB]);
                                else
                                    doIt = false;

                                if (doIt)
                                {
                                    row[col.FieldDB] =
                                        Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(findField,
                                            row[findField].ToString(), col.FieldDB);
                                }
                                else
                                {
                                    if (!(row[col.FieldDB] is DBNull))
                                        if ((row[col.FieldDB].ToString() == "") |
                                            (Convert.ToDouble(row[col.FieldDB]) == 0))
                                            row[col.FieldDB] =
                                                Columns[col.AsociatedButtonColumn].ColumnDBControl.Find(
                                                    findField, row[findField].ToString(), col.FieldDB);
                                }
                            }

                        if (col.AsociatedComboColumn != -1)
                            if (Columns[col.AsociatedComboColumn].ColumnType == DBColumn.ColumnTypes.ButtonColumn)
                            {
                                if (!string.IsNullOrEmpty(Columns[col.AsociatedComboColumn].ColumnDBFieldData))
                                    findField = Columns[col.AsociatedComboColumn].ColumnDBFieldData;
                                else
                                    findField = Columns[col.AsociatedComboColumn].FieldDB;
                                if (
                                    Convert.IsDBNull(
                                        DataControl.DataTable.Rows[CurrentCell.RowNumber][col.FieldDB]))
                                {
                                    row[col.FieldDB] =
                                        Columns[col.AsociatedComboColumn].ColumnDBControl.Find(findField,
                                            row[findField].ToString(), col.FieldDB);
                                }
                                else
                                {
                                    if ((row[col.FieldDB].ToString() == "") |
                                        (Convert.ToDouble(row[col.FieldDB]) == 0))
                                        row[col.FieldDB] =
                                            Columns[col.AsociatedComboColumn].ColumnDBControl.Find(findField,
                                                row[findField].ToString(), col.FieldDB);
                                }
                            }
                    }
                    catch (Exception e)
                    {
                        throw new ExceptionUtil(e);
                    }

            if (acceptChanges)
                //TODO: Problema con las columnas de tipo DescriptionColumns
                DataControl.DataTable.AcceptChanges();
        }


        private void UpdatePage()
        {
            lblPage.Text = DataControl.Page + 1 + "/" + DataControl.PageCount();
        }


        public new void Refresh()
        {
            DataGrid1.Refresh();
            if (DataControl != null)
            {
                DataControl.ReConnect();
                DataControl.Go(0);
            }
        }


        public new void Update()
        {
            DataGrid1.Update();
        }


        public void FillContextMenu()
        {
            var cm = new ContextMenu();
            var cmTotal = new ContextMenu();

            cm.MenuItems.Add("&Vista preliminar", MnuPrintPreview);
            cm.MenuItems.Add("&Imprimir", MnuPrint);
            cm.MenuItems.Add("&Exportar a Excel", MnuExcelExport);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Ajustar tamaño", MnuAutoAdjust);
            cm.MenuItems.Add("&Modo Registro", ModeRecord);
            cm.MenuItems.Add("&Refrescar", MnuRefrescar);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Copiar registro", MnuCopyOneReg);
            cm.MenuItems.Add("Copiar &seleccionados", MnuCopySelectedReg);
            cm.MenuItems.Add("Copiar &todos", MnuCopyAllReg);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Filtro", MnuFilter);
            cm.MenuItems.Add("&Quitar filtro", MnuDelFilter);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Buscar", MnuFind);
            cm.MenuItems.Add("&Buscar siguiente", MnuFindNext);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Editar Columnas", MnuColumnEdit);
            cm.MenuItems.Add("&Seleccionar todas", MnuSelect);
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("&Totales", MnuShowTotals);


            DataGrid1.ContextMenu = cm;

            cmTotal.MenuItems.Add("&Suma", MnuSum).RadioCheck = true;
            cmTotal.MenuItems.Add("&Máximo", MnuMax).RadioCheck = true;
            cmTotal.MenuItems.Add("&Mínimo", MnuMin).RadioCheck = true;
            cmTotal.MenuItems.Add("&Promedio", MnuAverage).RadioCheck = true;


            DataGridTotal.ContextMenu = cmTotal;
        }


        private void MnuExcelExport(object sender, EventArgs e)
        {
            var excel = new FSExcel.Excel();

            excel.Export(DataControl.DataTable);
        }


        private void UnCheckTotalMenu()
        {
            var f = 0;
            for (f = 0; f <= DataGridTotal.ContextMenu.MenuItems.Count - 1; f++)
                DataGridTotal.ContextMenu.MenuItems[f].Checked = false;
        }


        private void MnuCopyOneReg(object sender, EventArgs e)
        {
            CopySingleRowToClipboard();
        }


        private void MnuCopySelectedReg(object sender, EventArgs e)
        {
            CopySelectedRowsToClipboard();
        }


        private void MnuCopyAllReg(object sender, EventArgs e)
        {
            CopyAllRowsToClipboard();
        }


        private void MnuSum(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Sum;
            UnCheckTotalMenu();
            ((MenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuMax(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Max;
            UnCheckTotalMenu();
            ((MenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuMin(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Min;
            UnCheckTotalMenu();
            ((MenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuAverage(object sender, EventArgs e)
        {
            TotalOperation = DBColumn.OperationTypes.Average;
            UnCheckTotalMenu();
            ((MenuItem) sender).Checked = true;
            RefreshTotals();
        }


        private void MnuShowTotals(object sender, EventArgs e)
        {
            if (ShowTotals == false)
            {
                ShowTotals = true;
                SplitContainer1.Panel2Collapsed = false;
                ShowTotal();
            }
            else
            {
                SplitContainer1.Panel2Collapsed = true;
                ShowTotals = false;
            }

            ((MenuItem) sender).Checked = ShowTotals;
        }


        private void MnuFind(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.ShowFind();
        }


        private void MnuFindNext(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.FindNext();
        }


        private void MnuFilter(object sender, EventArgs e)
        {
            if (!(DataControl == null)) DataControl.ShowFilter();
        }


        private void MnuDelFilter(object sender, EventArgs e)
        {
            if (DataControl != null) DataControl.DeleteFilter();
        }


        private void MnuPrintPreview(object sender, EventArgs e)
        {
            dataGridPrinter1.PageNumber = 1;
            dataGridPrinter1.RowCount = 0;
            PrintDocument1.DefaultPageSettings = PageSetup.PageSettings;
            if (PrintPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
            }
        }


        private void MnuSelect(object sender, EventArgs e)
        {
            var f = 0;
            for (f = 0; f <= DataGrid1.VisibleRowCount - 1; f++) DataGrid1.Select(f);
        }


        private void MnuPrint(object sender, EventArgs e)
        {
            dataGridPrinter1.PageNumber = 1;
            dataGridPrinter1.RowCount = 0;
            PrintDocument1.DefaultPageSettings = PageSetup.PageSettings;
            if (PrintDialog1.ShowDialog() == DialogResult.OK) PrintDocument1.Print();
        }


        private void MnuAutoAdjust(object sender, EventArgs e)
        {
            FunctionsGrid.AutoSizeColumnsToContent(DataControl, Columns, CreateGraphics(), Font,
                dgTableStyle.GridColumnStyles);
        }


        private void HideMenu(object sender, EventArgs e)
        {
            if (m_columnMove != -1)
            {
                HideColumn(m_columnMove);
                DataGrid1.ContextMenu.MenuItems[1].MenuItems[m_columnMove].Checked = true;
            }
        }


        private void ShowMenu(object sender, EventArgs e)
        {
            if (sender is MenuItem)
            {
                ShowColumn(((MenuItem) sender).Index);
                ((MenuItem) sender).Checked = false;
            }
        }


        private void UpdateDBControl()
        {
            object v = null;

            if (!(DataControl == null))
                try
                {
                    if (DataControl.DBFieldData == "") return;
                    v = get_RowValue(DataControl.DBFieldData, -1);
                    if (v != null)
                        if (!Convert.IsDBNull(v))
                            DataControl.UpdateRelationDBControls(FindForm().Controls, true, v);
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil(ex);
                }
        }


        private void DataGrid1_DoubleClick(object sender, EventArgs e)
        {
            UpdateDBControl();
            if (null != DoubleClick) DoubleClick(this, e);
        }


        public object get_RowValue(int column)
        {
            return get_RowValue(column, -1);
        }


        public object get_RowValue(int column, int row)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            if (DataGrid1.VisibleRowCount == 0) return null;
            try
            {
                return DataGrid1[row, column];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void set_RowValue(int column, object Value)
        {
            set_RowValue(column, -1, Value);
        }

        public void set_RowValue(int column, int row, object Value)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            DataGrid1[row, column] = Value;
        }

        public object get_RowValue(string columnName)
        {
            return get_RowValue(columnName, -1);
        }

        public object get_RowValue(string columnName, int row)
        {
            if (columnName == "") return null;
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            if (DataGrid1.VisibleRowCount == 0) return null;
            var i = Columns.GetColumnOrdinal(columnName);
            if (i != -1)
                try
                {
                    return DataGrid1[row, i];
                }
                catch (Exception e)
                {
                    throw new ExceptionUtil(e);
                }

            return null;
        }

        public void set_RowValue(string columnName, object Value)
        {
            set_RowValue(columnName, -1, Value);
        }

        public void set_RowValue(string columnName, int row, object Value)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            DataGrid1[row, DataControl.FieldOrdinal(columnName)] = Value;
        }


        public object get_RowDataValue(int column)
        {
            return get_RowDataValue(column);
        }

        public object get_RowDataValue(int column, int row)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            try
            {
                return DataControl.DataTable.Rows[row][Columns[column].FieldDB];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void set_RowDataValue(int column, object Value)
        {
            set_RowDataValue(column, -1, Value);
        }

        public void set_RowDataValue(int column, int row, object Value)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            DataControl.DataTable.Rows[row][Columns[column].FieldDB] = Value;
        }

        public object get_RowDataValue(string columnName)
        {
            return get_RowDataValue(columnName, -1);
        }

        public object get_RowDataValue(string columnName, int row)
        {
            if (DataControl.DataTable.Rows.Count == 0) return null;
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            try
            {
                return DataControl.DataTable.Rows[row][columnName];
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }

        public void set_RowDataValue(string columnName, object Value)
        {
            set_RowDataValue(columnName, -1, Value);
        }

        public void set_RowDataValue(string columnName, int row, object Value)
        {
            if (row == -1) row = DataGrid1.CurrentCell.RowNumber;
            DataControl.DataTable.Rows[row][columnName] = Value;
        }


        public int ColumnOrdinal(string columnName)
        {
            var i = 0;
            if (columnName == "") return 0;

            for (i = 0; i <= Columns.Count - 1; i++)
                try
                {
                    if (Columns[i].FieldDB.ToUpper() == columnName.ToUpper()) return i;
                }
                catch
                {
                }

            return 0;
        }


        private void DataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            var col = DataGrid1.CurrentCell.ColumnNumber;
            var row = DataGrid1.CurrentCell.RowNumber;

            if (Mode == AccessMode.ReadMode) return;

            UpdateLastRowValue(row);


            if (null != CurrentCellChanged) CurrentCellChanged(this, e);

            LastRow = row;
            LastCol = col;
        }


        public void UpdateLastRowValue(int row)
        {
            var f = 0;

            if (row <= 0) return;
            if (DataControl.DataTable.Rows.Count <= row) return;

            for (f = 0; f <= Columns.Count - 1; f++)
                if (Columns[f].LastValue)
                    if (DataControl.DataTable.Rows[row].RowState == DataRowState.Added)
                        if (DataControl.DataTable.Rows[row][Columns[f].FieldDB] == DBNull.Value)
                            DataControl.DataTable.Rows[row][Columns[f].FieldDB] = DataGrid1[row - 1, f];
        }


        public void ModeControls(AccessMode mode)
        {
            if (DataControl != null) DataControl.ModeDBControls(DataGrid1.Controls, mode);
        }


        public void UnSelect(int row)
        {
            try
            {
                if(DataGrid1.DataSource != null)
                    DataGrid1.UnSelect(row);
            }
            catch(Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public void UnSelect()
        {
            try
            {
                var f = 0;
                for (f = 0; f <= DataGrid1.VisibleRowCount - 1; f++) DataGrid1.UnSelect(f);
            }
            catch
            {
            }
        }


        private void DataGrid1_MouseUp(object sender, MouseEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            var hti = ((DataGrid) sender).HitTest(pt);
            var iRow = -1;

            m_mouseDownColumn = -1;

            if (Mode == AccessMode.WriteMode) return;

            if (e.Button != MouseButtons.Left) return;
            if (!(DataControl == null)) DataControl.Action = DBControl.DbActionTypes.Change;

            if (hti.Type == DataGrid.HitTestType.Cell || hti.Type == DataGrid.HitTestType.RowHeader) iRow = hti.Row;

            if (iRow != -1) m_LastRowClicked = iRow;


            switch (hti.Type)
            {
                case DataGrid.HitTestType.Cell:
                    DataGrid1.CurrentCell = new DataGridCell(hti.Row, hti.Column);
                    DataGrid1.CurrentRowIndex = hti.Row;
                    DataGrid1.Select(hti.Row);

                    if (e.Clicks == 1)
                    {
                        if (null != Click) Click(this, e);
                    }
                    else
                    {
                        if (null != DoubleClick) DoubleClick(this, e);
                    }

                    break;
                case DataGrid.HitTestType.RowHeader:

                    if (e.Clicks == 1)
                    {
                        if (null != Click) Click(this, e);
                    }
                    else
                    {
                        if (null != DoubleClick) DoubleClick(this, e);
                    }

                    break;
            }


            if (!(DataControl == null)) DataControl.Action = DBControl.DbActionTypes.None;
            base.OnMouseUp(e);
        }


        private void FillDescriptionColumns()
        {
            DataControl.Action = DBControl.DbActionTypes.Change;

            foreach (DataRow row in DataControl.DataTable.Rows) UpdateAsociatedColumns(row, false, true);

            DataControl.Action = DBControl.DbActionTypes.None;
        }


        public void MoveColumn(string _mappingName, int fromCol, int toCol)
        {
            if (fromCol == toCol) return;
            if ((fromCol == -1) | (toCol == -1)) return;

            var i = 0;
            var oldTS = DataGrid1.TableStyles[_mappingName];
            var newTS = new DataGridTableStyle();

            newTS.MappingName = _mappingName;
            i = 0;

            while (i < oldTS.GridColumnStyles.Count)
            {
                if ((i != fromCol) & (fromCol < toCol)) newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[i]);
                if (i == toCol) newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[fromCol]);
                if ((i != fromCol) & (fromCol > toCol)) newTS.GridColumnStyles.Add(oldTS.GridColumnStyles[i]);
                i = i + 1;
            }

            DataGrid1.TableStyles.Remove(oldTS);
            DataGrid1.TableStyles.Add(newTS);
        }


        public void HideColumn(string columnName)
        {
            HideColumn(Columns.GetColumnOrdinal(columnName));
        }


        public void ShowColumn(string columnName)
        {
            ShowColumn(Columns.GetColumnOrdinal(columnName));
        }


        public void HideColumn(int column)
        {
            Columns[column].Hidden = true;
            try
            {
                dgTableStyle.GridColumnStyles[Columns[column].FieldDB].MappingName =
                    "hidden_" + Columns[column].FieldDB;
            }
            catch
            {
            }
        }


        public void ShowColumn(int column)
        {
            Columns[column].Hidden = false;
            try
            {
                dgTableStyle.GridColumnStyles["hidden_" + Columns[column].FieldDB].MappingName =
                    Columns[column].FieldDB;
            }
            catch
            {
            }
        }


        public void ColumnHeaderText(int column, string text)
        {
            dgTableStyle.GridColumnStyles[column].HeaderText = text;
        }


        public void SetColumnError(int row, int col, string msg)
        {
            DataControl.DataTable.Rows[row].SetColumnError(Columns[col].FieldDB, msg);
        }


        public void Clear()
        {
            Columns.Clear();
            DataGrid1.TableStyles.Clear();
            dgTableStyle.GridColumnStyles.Clear();
        }


        private void DataControl_OnReConnect()
        {
            SetAutoIncrement();
            FillDescriptionColumns();
        }


        private void SetAutoIncrement()
        {
            var f = 0;

            try
            {
                for (f = 0; f <= Columns.Count - 1; f++)
                    if (Columns[f].ColumnType == DBColumn.ColumnTypes.AutoNumericColumn)
                        DataControl.DataTable.Columns[Columns[f].FieldDB].AutoIncrementSeed =
                            Convert.ToInt64(Utils.MaxColumn(DataControl.DataTable, Columns[f].FieldDB) + 1);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        public long Rows()
        {
            return DataControl.RecordCount();
        }


        public long Cols()
        {
            return Columns.Count;
        }


        private void cmdPageFirst_Click(object sender, EventArgs e)
        {
            DataControl.MoveFirstPage();
            UpdatePage();
        }


        private void cmdPageLast_Click(object sender, EventArgs e)
        {
            DataControl.MoveLastPage();
            UpdatePage();
        }


        private void cmdPagePrevious_Click(object sender, EventArgs e)
        {
            DataControl.MovePreviousPage();
            UpdatePage();
        }


        private void cmdPageNext_Click(object sender, EventArgs e)
        {
            DataControl.MoveNextPage();
            UpdatePage();
        }


        private void DataGrid1_DragEnter(object sender, DragEventArgs e)
        {
            var clientPoint = DataGrid1.PointToClient(new Point(e.X, e.Y));
            if (e.Data.GetDataPresent(DragColumn.DataFormat))
            {
                e.Effect = DragDropEffects.Copy;
                Win32API.ImageList_DragEnter(DataGrid1.Handle, clientPoint.X, clientPoint.Y);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        private void DataGrid1_DragLeave(object sender, EventArgs e)
        {
            Win32API.ImageList_DragLeave(DataGrid1.Handle);
        }


        private void DataGrid1_DragOver(object sender, DragEventArgs e)
        {
            var clientPoint = DataGrid1.PointToClient(new Point(e.X, e.Y));

            Win32API.ImageList_DragMove(clientPoint.X, clientPoint.Y);
            e.Effect = DragDropEffects.All;
        }


        private void DataGrid1_DragDrop(object sender, DragEventArgs e)
        {
            var clientPoint = PointToClient(new Point(e.X, e.Y));
            var hti = DataGrid1.HitTest(clientPoint.X, clientPoint.Y);
            var columns = DataGrid1.TableStyles[0].GridColumnStyles;
            if (!e.Data.GetDataPresent(DragColumn.DataFormat)) return;

            var c = e.Data.GetData(DragColumn.DataFormat);

            if (c == null) return;

            Win32API.ImageList_DragLeave(DataGrid1.Handle);

            var dragColIdx = 9999; // mal falta: c.Column; 
            var dropColIdx = hti.Column;
            if ((dragColIdx == dropColIdx) | (-1 == hti.Column)) return;

            if (dragColIdx == dropColIdx - 1)
            {
                dragColIdx = dragColIdx + 1;
                dropColIdx = dropColIdx - 1;
            }

            var col = new ArrayList();
            var dragCol = columns[dragColIdx];
            var newColumns = new ArrayList();
            var i = 0;
            for (i = 0; i <= columns.Count - 1; i++)
            {
                if (i == dropColIdx) newColumns.Add(dragCol);

                if (i != dragColIdx) newColumns.Add(columns[i]);
            }

            DataGrid1.TableStyles[0].GridColumnStyles.Clear();

            foreach (DataGridColumnStyle newCol in newColumns) DataGrid1.TableStyles[0].GridColumnStyles.Add(newCol);
        }


        private void DataGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            }

            var hti = DataGrid1.HitTest(e.X, e.Y);

            if (DataGrid.HitTestType.ColumnHeader == hti.Type)
                m_mouseDownColumn = hti.Column;
            else
                m_mouseDownColumn = -1;
            base.OnMouseDown(e);
        }


        private void DataGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            var hti = DataGrid1.HitTest(pt);

            m_columnMove = hti.Column;

            if (hti.Type == DataGrid.HitTestType.ColumnHeader)
                if (ToolTip1.GetToolTip(DataGrid1) != Columns[m_columnMove].ToolTip)
                    ToolTip1.SetToolTip(DataGrid1, Columns[m_columnMove].ToolTip);

            if ((hti.Type == DataGrid.HitTestType.ColumnHeader) & (m_mouseDownColumn == m_columnMove))
            {
                var dragObject = new DataObject(DragColumn.DataFormat, new DragColumn(m_columnMove));


                var colSyle = DataGrid1.TableStyles[0].GridColumnStyles[m_columnMove];
                var tableStyle = DataGrid1.TableStyles[0];

                var im = colSyle.Width;
                if (im > 256) im = 256;

                var rectF = new RectangleF(0, 0, im, 20);
                var colHeaderImage = new Bitmap(Convert.ToInt32(rectF.Width), Convert.ToInt32(rectF.Height),
                    PixelFormat.Format24bppRgb);
                var g = Graphics.FromImage(colHeaderImage);
                var stringSize = g.MeasureString(colSyle.HeaderText, DataGrid1.Font);
                if (stringSize.Width > rectF.Width)
                {
                    colHeaderImage.Dispose();
                    rectF.Width = stringSize.Width;
                    colHeaderImage = new Bitmap(Convert.ToInt32(rectF.Width), Convert.ToInt32(rectF.Height),
                        PixelFormat.Format24bppRgb);
                    g = Graphics.FromImage(colHeaderImage);
                }

                g.SmoothingMode = SmoothingMode.AntiAlias;
                var fgBrush = new SolidBrush(tableStyle.HeaderForeColor);
                var bgBrush = new SolidBrush(tableStyle.HeaderBackColor);
                g.FillRectangle(bgBrush, rectF);
                g.DrawString(colSyle.HeaderText, DataGrid1.Font, fgBrush, rectF.X,
                    (rectF.Height - stringSize.Height) / 2);

                imageList_DragDrop.Images.Clear();

                imageList_DragDrop.ImageSize = colHeaderImage.Size;
                imageList_DragDrop.Images.Add((Image) colHeaderImage.Clone());
                colHeaderImage.Dispose();

                if (Win32API.ImageList_BeginDrag(imageList_DragDrop.Handle, 0, 0,
                    Convert.ToInt32(-Cursors.Default.Size.Height / 2)))
                {
                    var de = DataGrid1.DoDragDrop(dragObject, DragDropEffects.Move);
                    Win32API.ImageList_EndDrag();
                }
            }
        }


        private void ModeRecord(object sender, EventArgs e)
        {
            FillRecord();

            if (RecordMode)
            {
                DbRecord1.Visible = false;
                DataGrid1.Visible = true;
                if (ShowTotals)
                    DataGridTotal.Visible = true;
                else
                    DataGridTotal.Visible = false;
                RecordMode = false;
            }
            else
            {
                DbRecord1.Visible = true;
                DataGrid1.Visible = false;
                DataGridTotal.Visible = false;
                RecordMode = true;
            }
        }


        private void FillRecord()
        {
            if (FilledRecord) return;

            DbRecord1 = new DBRecord();
            Controls.Add(DbRecord1);

            DbRecord1.ShowScrollBar = ShowRecordScrollBar;

            DbRecord1.Columns = Columns;
            DbRecord1.DataControl = DataControl;

            DbRecord1.Left = 0;
            DbRecord1.Top = 0;
            DbRecord1.Width = Width;
            DbRecord1.Height = Height;

            DbRecord1.Fill();

            DbRecord1.DataControl.UpdateControls(DbRecord1.Controls);
            DbRecord1.DataControl.FillComboControls(DbRecord1.Controls);

            var cmRecord = new ContextMenu();
            cmRecord.MenuItems.Add("&Modo Grid", ModeRecord);
            DbRecord1.ContextMenu = cmRecord;

            FilledRecord = true;
        }


        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var more = dataGridPrinter1.DrawDataGrid(g);
            if (more)
            {
                e.HasMorePages = true;
                dataGridPrinter1.PageNumber = dataGridPrinter1.PageNumber + 1;
            }
        }


        private void PrintPreviewDialog1_Load(object sender, EventArgs e)
        {
            PrintPreviewDialog1.Bounds = ClientRectangle;
        }


        public new void Resize()
        {
            if (ShowTotals)
            {
                DataGridTotal.Left = 0;
                DataGridTotal.Height = 55;
                DataGridTotal.Top = Height - DataGridTotal.Height;
                DataGridTotal.Width = Width;

                DataGrid1.Top = 0;
                DataGrid1.Left = 0;
                DataGrid1.Height = Height - DataGridTotal.Height;
                DataGrid1.Width = Width;
            }
            else
            {
                DataGrid1.Top = 0;
                DataGrid1.Left = 0;
                DataGrid1.Height = Height;
                DataGrid1.Width = Width;
            }
        }


        private void DBGrid_Resize(object sender, EventArgs e)
        {
            try
            {
                Resize();
            }
            catch (Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        private void DataGrid1_MouseLeave(object sender, EventArgs e)
        {
            m_mouseDownColumn = -1;
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (!(DataControl == null))
            {
                DataControl.ReConnect();
                DataControl.Go(0);
            }
        }

        #region Nested type: DBDataGridTableStylesCollectionEditor

        private class DBDataGridTableStylesCollectionEditor : CollectionEditor
        {
            public DBDataGridTableStylesCollectionEditor(Type type) : base(type)
            {
            }


            protected override Type[] CreateNewItemTypes()
            {
                return new[] {typeof(DBTableStyle)};
            }
        }

        #endregion

        #region Delegates

        public delegate void CellKeyPressEventHandler(long Column, long Row);

        public delegate void ClickButtonEventHandler(int col, int row);

        public delegate void ClickEventHandler(object sender, EventArgs e);

        public delegate void ColumnChangedEventHandler(object sender, DataColumnChangeEventArgs e);

        public delegate void ColumnChangingEventHandler(object sender, DataColumnChangeEventArgs e);

        public delegate void CurrentCellChangedEventHandler(object sender, EventArgs e);

        public delegate void DoubleClickEventHandler(object sender, EventArgs e);

        public delegate void RowChangedEventHandler(object sender, DataRowChangeEventArgs e);

        public delegate void RowChangingEventHandler(object sender, DataRowChangeEventArgs e);

        #endregion

        #region '" Código generado por el Diseñador de Windows Forms "' 

        internal DataGrid DataGrid1;
        internal DataGrid DataGridTotal;
        internal PrintDialog PrintDialog1;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal PrintPreviewDialog PrintPreviewDialog1;
        internal ToolTip ToolTip1;
        private Button cmdPageFirst;
        private Button cmdPageLast;
        private Button cmdPageNext;
        private Button cmdPagePrevious;
        private IContainer components;
        private Label lblPage;

        public DBGrid()
        {
            InitializeComponent();

            if (Columns == null) Columns = new DBColumnCollection();

            SplitContainer1.Panel2Collapsed = true;

            DataGrid1.Click += DataGrid1_Click;
            DataGrid1.Leave += DataGrid1_Leave;
            DataGrid1.Paint += DataGrid1_Paint;
            DataGrid1.Scroll += DataGrid1_Scroll;
            DataGrid1.DoubleClick += DataGrid1_DoubleClick;
            DataGrid1.CurrentCellChanged += DataGrid1_CurrentCellChanged;
            DataGrid1.MouseUp += DataGrid1_MouseUp;
            cmdPageFirst.Click += cmdPageFirst_Click;
            cmdPageLast.Click += cmdPageLast_Click;
            cmdPagePrevious.Click += cmdPagePrevious_Click;
            cmdPageNext.Click += cmdPageNext_Click;
            DataGrid1.DragEnter += DataGrid1_DragEnter;
            DataGrid1.DragLeave += DataGrid1_DragLeave;
            DataGrid1.DragOver += DataGrid1_DragOver;
            DataGrid1.DragDrop += DataGrid1_DragDrop;
            DataGrid1.MouseDown += DataGrid1_MouseDown;
            DataGrid1.MouseMove += DataGrid1_MouseMove;
            PrintDocument1.PrintPage += PrintDocument1_PrintPage;
            PrintPreviewDialog1.Load += PrintPreviewDialog1_Load;
            base.Resize += DBGrid_Resize;
            DataGrid1.MouseLeave += DataGrid1_MouseLeave;
            PictureBox1.Click += PictureBox1_Click;

            DataGrid1.MouseDown += DataGridMouseDownEventHandler;

            Win32API.InitCommonControls();
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
            components = new Container();
            var resources = new ComponentResourceManager(typeof(DBGrid));
            cmdPageLast = new Button();
            cmdPageFirst = new Button();
            cmdPagePrevious = new Button();
            cmdPageNext = new Button();
            lblPage = new Label();
            DataGrid1 = new DataGrid();
            PrintDialog1 = new PrintDialog();
            PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            PrintPreviewDialog1 = new PrintPreviewDialog();
            DataGridTotal = new DataGrid();
            ToolTip1 = new ToolTip(components);
            PictureBox1 = new PictureBox();
            SplitContainer1 = new SplitContainer();
            ((ISupportInitialize) DataGrid1).BeginInit();
            ((ISupportInitialize) DataGridTotal).BeginInit();
            ((ISupportInitialize) PictureBox1).BeginInit();
            ((ISupportInitialize) SplitContainer1).BeginInit();
            SplitContainer1.Panel1.SuspendLayout();
            SplitContainer1.Panel2.SuspendLayout();
            SplitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // cmdPageLast
            // 
            cmdPageLast.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdPageLast.BackColor = Color.LightGray;
            cmdPageLast.Image = (Image) resources.GetObject("cmdPageLast.Image");
            cmdPageLast.Location = new Point(224, 3);
            cmdPageLast.Name = "cmdPageLast";
            cmdPageLast.Size = new Size(16, 16);
            cmdPageLast.TabIndex = 4;
            cmdPageLast.UseVisualStyleBackColor = false;
            cmdPageLast.Visible = false;
            // 
            // cmdPageFirst
            // 
            cmdPageFirst.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdPageFirst.BackColor = Color.LightGray;
            cmdPageFirst.Image = (Image) resources.GetObject("cmdPageFirst.Image");
            cmdPageFirst.Location = new Point(176, 3);
            cmdPageFirst.Name = "cmdPageFirst";
            cmdPageFirst.Size = new Size(16, 16);
            cmdPageFirst.TabIndex = 5;
            cmdPageFirst.UseVisualStyleBackColor = false;
            cmdPageFirst.Visible = false;
            // 
            // cmdPagePrevious
            // 
            cmdPagePrevious.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdPagePrevious.BackColor = Color.LightGray;
            cmdPagePrevious.Image = (Image) resources.GetObject("cmdPagePrevious.Image");
            cmdPagePrevious.Location = new Point(192, 3);
            cmdPagePrevious.Name = "cmdPagePrevious";
            cmdPagePrevious.Size = new Size(16, 16);
            cmdPagePrevious.TabIndex = 6;
            cmdPagePrevious.UseVisualStyleBackColor = false;
            cmdPagePrevious.Visible = false;
            // 
            // cmdPageNext
            // 
            cmdPageNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdPageNext.BackColor = Color.LightGray;
            cmdPageNext.Image = (Image) resources.GetObject("cmdPageNext.Image");
            cmdPageNext.Location = new Point(208, 3);
            cmdPageNext.Name = "cmdPageNext";
            cmdPageNext.Size = new Size(16, 16);
            cmdPageNext.TabIndex = 7;
            cmdPageNext.UseVisualStyleBackColor = false;
            cmdPageNext.Visible = false;
            // 
            // lblPage
            // 
            lblPage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPage.BackColor = SystemColors.ActiveCaption;
            lblPage.ForeColor = SystemColors.ControlLightLight;
            lblPage.Location = new Point(88, 4);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(80, 12);
            lblPage.TabIndex = 8;
            lblPage.Text = "1/1";
            lblPage.TextAlign = ContentAlignment.MiddleCenter;
            lblPage.Visible = false;
            // 
            // DataGrid1
            // 
            DataGrid1.AllowDrop = true;
            DataGrid1.BackgroundColor = Color.LightGray;
            DataGrid1.DataMember = "";
            DataGrid1.Dock = DockStyle.Fill;
            DataGrid1.HeaderForeColor = SystemColors.ControlText;
            DataGrid1.Location = new Point(0, 0);
            DataGrid1.Name = "DataGrid1";
            DataGrid1.PreferredRowHeight = 32;
            DataGrid1.ReadOnly = true;
            DataGrid1.Size = new Size(264, 155);
            DataGrid1.TabIndex = 11;
            // 
            // PrintDialog1
            // 
            PrintDialog1.Document = PrintDocument1;
            // 
            // PrintPreviewDialog1
            // 
            PrintPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            PrintPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            PrintPreviewDialog1.ClientSize = new Size(400, 300);
            PrintPreviewDialog1.Document = PrintDocument1;
            PrintPreviewDialog1.Enabled = true;
            PrintPreviewDialog1.Icon = (Icon) resources.GetObject("PrintPreviewDialog1.Icon");
            PrintPreviewDialog1.Name = "PrintPreviewDialog1";
            PrintPreviewDialog1.Visible = false;
            // 
            // DataGridTotal
            // 
            DataGridTotal.CaptionVisible = false;
            DataGridTotal.DataMember = "";
            DataGridTotal.Dock = DockStyle.Fill;
            DataGridTotal.HeaderForeColor = SystemColors.ControlText;
            DataGridTotal.Location = new Point(0, 0);
            DataGridTotal.Name = "DataGridTotal";
            DataGridTotal.ReadOnly = true;
            DataGridTotal.Size = new Size(264, 49);
            DataGridTotal.TabIndex = 13;
            DataGridTotal.Visible = false;
            // 
            // PictureBox1
            // 
            PictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PictureBox1.Cursor = Cursors.Hand;
            PictureBox1.Image = (Image) resources.GetObject("PictureBox1.Image");
            PictureBox1.Location = new Point(243, 3);
            PictureBox1.Name = "PictureBox1";
            PictureBox1.Size = new Size(16, 16);
            PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox1.TabIndex = 12;
            PictureBox1.TabStop = false;
            ToolTip1.SetToolTip(PictureBox1, "Actualizar");
            // 
            // SplitContainer1
            // 
            SplitContainer1.Dock = DockStyle.Fill;
            SplitContainer1.Location = new Point(0, 0);
            SplitContainer1.Name = "SplitContainer1";
            SplitContainer1.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            SplitContainer1.Panel1.Controls.Add(PictureBox1);
            SplitContainer1.Panel1.Controls.Add(DataGrid1);
            // 
            // SplitContainer1.Panel2
            // 
            SplitContainer1.Panel2.Controls.Add(DataGridTotal);
            SplitContainer1.Size = new Size(264, 208);
            SplitContainer1.SplitterDistance = 155;
            SplitContainer1.TabIndex = 14;
            // 
            // DBGrid
            // 
            AllowDrop = true;
            Controls.Add(lblPage);
            Controls.Add(cmdPageNext);
            Controls.Add(cmdPagePrevious);
            Controls.Add(cmdPageFirst);
            Controls.Add(cmdPageLast);
            Controls.Add(SplitContainer1);
            Name = "DBGrid";
            Size = new Size(264, 208);
            ((ISupportInitialize) DataGrid1).EndInit();
            ((ISupportInitialize) DataGridTotal).EndInit();
            ((ISupportInitialize) PictureBox1).EndInit();
            SplitContainer1.Panel1.ResumeLayout(false);
            SplitContainer1.Panel1.PerformLayout();
            SplitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize) SplitContainer1).EndInit();
            SplitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }


    [Serializable]
    public class DragColumn
    {
        public int m_Column;

        public DragColumn(int col)
        {
            m_Column = col;
        }

        public int Column => m_Column;

        public static string DataFormat => "Dragged column object";
    }
}