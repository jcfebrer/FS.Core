#region

using System;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using DateTimeUtil = FSLibrary.DateTimeUtil;
using FSException;

#endregion

namespace FSFormControls
{
    //public class DataGridDBTextBoxColumn : DataGridTextBoxColumn
    //{
    //    public Color BackColor { get; set; }

    //    public Color ForeColor { get; set; }

    //    public DataGridDBTextBoxColumn()
    //    {
    //    }
    //    public DataGridDBTextBoxColumn(int col, DBColumn dbcolumn, DBGrid dbgrid)
    //    {
    //    }
    //}
    public class DataGridDBTextBoxColumn : DataGridTextBoxColumn
    {
        private readonly int m_col;
        private readonly DBColumn m_dbcolumn;
        private readonly DBGrid m_dbgrid;
        private int m_row;
        private CurrencyManager m_source;


        public DataGridDBTextBoxColumn()
        {
        }

        public DataGridDBTextBoxColumn(int col, DBColumn dbcolumn, DBGrid dbgrid)
        {
            m_col = col;
            m_dbcolumn = dbcolumn;
            m_dbgrid = dbgrid;
            TextBox.KeyDown += HandleKeyDown;
            TextBox.KeyPress += HandleKeyPress;
        }


        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            object value = null;
            switch (e.KeyCode)
            {
                case Keys.F6:
                    if ((m_dbcolumn.ColumnType == DBColumn.ColumnTypes.ButtonColumn) |
                        (m_dbcolumn.ColumnType == DBColumn.ColumnTypes.DescriptionColumn))
                    {
                        m_dbgrid.ButtonColumnClick(this, new DataGridCellButtonClickEventArgs(m_col, m_row), ref value);
                        m_dbgrid.set_RowValue(m_col, Convert.ToInt32(value), null);
                    }

                    break;
                case Keys.F8:
                    m_dbgrid.set_RowValue(m_dbgrid.ColSel,
                        Convert.ToInt32(m_dbgrid.get_RowValue(m_dbgrid.ColSel, m_dbgrid.RowSel - 1)),
                        null);
                    break;
            }
        }


        private void HandleKeyPress(object sender, KeyPressEventArgs e)
        {
            string validkeys = null;
            var t = (Keys) e.KeyChar;

            if ((m_dbcolumn.ColumnType == DBColumn.ColumnTypes.NumberColumn) |
                (m_dbcolumn.ColumnType == DBColumn.ColumnTypes.MoneyColumn) |
                (m_dbcolumn.ColumnType == DBColumn.ColumnTypes.PercentColumn))
            {
                validkeys = "1234567890.,-";

                if ((t == Keys.Delete) | (t == Keys.Back)) return;
                if ((TextBox.SelectionLength != 0) & (validkeys.IndexOf(e.KeyChar) + 1 != 0)) return;

                if (validkeys.IndexOf(e.KeyChar) + 1 == 0) e.Handled = true;
            }
        }

        private string GetText(object Value)
        {
            if (Value is DBNull)
                return NullText;
            if (Value == null)
                return string.Empty;
            if (Value is double)
                return Value + "falta format con 2 decimales y punto";
            return Value.ToString();
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
        {
            Paint(g, bounds, source, rowNum, false);
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
            bool alignToRight)
        {
            var drw = false;

            if (m_dbgrid != null)
            {
                if (m_dbgrid.RowDraw.IndexOf(rowNum.ToString()) + 1 > 0)
                {
                    Paint(g, bounds, source, rowNum, new SolidBrush(Color.Red), new SolidBrush(Color.White),
                        alignToRight);
                    drw = true;
                }
                else
                {
                    drw = false;
                }
            }
            else
            {
                drw = true;
            }

            if (drw)
            {
                if (BackColor.IsEmpty & ForeColor.IsEmpty)
                {
                    base.Paint(g, bounds, source, rowNum, alignToRight);
                }
                else
                {
                    Brush backBrush = new SolidBrush(BackColor);
                    Brush foreBrush = new SolidBrush(ForeColor);

                    Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
                }
            }
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var drw = false;

            if (m_dbgrid != null)
            {
                if (m_dbgrid.RowDraw.IndexOf(rowNum.ToString()) + 1 > 0)
                {
                    base.Paint(g, bounds, source, rowNum, new SolidBrush(Color.Red), new SolidBrush(Color.White),
                        alignToRight);
                    drw = false;
                }
                else
                {
                    drw = true;
                }
            }
            else
            {
                drw = true;
            }

            if (drw)
            {
                if (BackColor.IsEmpty & ForeColor.IsEmpty)
                {
                    base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
                }
                else
                {
                    string text = null;
                    text = GetText(GetColumnValueAtRow(source, rowNum));
                    backBrush = new SolidBrush(BackColor);
                    foreBrush = new SolidBrush(ForeColor);
                    PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);
                }
            }
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;
            base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);

            m_source = source;
            m_row = rowNum;
        }


        protected override void Dispose(bool disposing)
        {
            TextBox.KeyDown -= HandleKeyDown;
            TextBox.KeyPress -= HandleKeyPress;
        }
    }


    public class DataGridDBTextboxColumn_2 : DataGridTextBoxColumn
    {
        private readonly DBColumn m_DBColumn;
        public DBTextBox ColumnTextBox = new DBTextBox();
        private int m_Col;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;

        public DataGridDBTextboxColumn_2(int col, DBColumn dbcolumn)
        {
            m_source = null;
            m_isEditing = false;
            m_Col = col;
            m_DBColumn = dbcolumn;

            ReadOnly = true;
            ColumnTextBox.DataType = FunctionsForms.ConvertDataTypeToColumnType(dbcolumn.ColumnType);
            ColumnTextBox.ReadOnly = m_DBColumn.ReadColumn;
            ColumnTextBox.TextAlign = m_DBColumn.Alignment;
            ColumnTextBox.MaskInput = m_DBColumn.MaskInput;
            ColumnTextBox.BorderStyle = BorderStyle.FixedSingle;

            ColumnTextBox.Leave += LeaveTextBox;
            ColumnTextBox.Enter += TextBoxStartEditing;

            ColumnTextBox.Visible = false;
        }

        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        private void TextBoxStartEditing(object sender, EventArgs e)
        {
            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveTextBox(object sender, EventArgs e)
        {
            var dbg = (DBGrid) TextBox.Parent.Parent;
            if (dbg.Mode == Global.AccessMode.ReadMode)
            {
                ColumnTextBox.Visible = false;
                return;
            }

            if (m_isEditing)
            {
                var v = ColumnTextBox.Text + "";
                m_source.Position = m_rowNum;
                try
                {
                    SetColumnValueAtRow(m_source, m_rowNum, v);
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil(ex);
                }

                m_isEditing = false;
            }

            ColumnTextBox.Visible = false;

            DataGridTableStyle.DataGrid.Controls.Remove(ColumnTextBox);
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;
            if (m_DBColumn.ReadColumn) return;


            DataGridTableStyle.DataGrid.Controls.Add(ColumnTextBox);

            m_rowNum = rowNum;
            m_source = source;


            ColumnTextBox.Font = TextBox.Font;

            var anObj = GetColumnValueAtRow(source, rowNum);

            ColumnTextBox.Bounds = bounds;
            ColumnTextBox.Visible = true;

            if (!(anObj is DBNull))
                ColumnTextBox.Text = Convert.ToString(anObj);
            else
                ColumnTextBox.Text = Global.SINDEFINIR;

            ColumnTextBox.Focus();
        }


        private string GetText(object Value)
        {
            if (Value is DBNull)
                return NullText;
            if (Value == null)
                return string.Empty;
            return Value.ToString();
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
        {
            /* TRANSERROR: was MyClass */
            Paint(g, bounds, source, rowNum, false);
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
            bool alignToRight)
        {
            if (BackColor.IsEmpty & ForeColor.IsEmpty)
            {
                base.Paint(g, bounds, source, rowNum, alignToRight);
            }
            else
            {
                Brush backBrush = null;
                Brush foreBrush = null;
                backBrush = new SolidBrush(BackColor);
                foreBrush = new SolidBrush(ForeColor);
                /* TRANSERROR: was MyClass */
                Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            }
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            if (BackColor.IsEmpty & ForeColor.IsEmpty)
            {
                base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
            }
            else
            {
                string text = null;
                text = GetText(GetColumnValueAtRow(source, rowNum));
                backBrush = new SolidBrush(BackColor);
                foreBrush = new SolidBrush(ForeColor);
                PaintText(g, bounds, text, backBrush, foreBrush, alignToRight);
            }
        }


        ~DataGridDBTextboxColumn_2()
        {
        }
    }


    public class DataGridDBTextBox2Column_borrar : DataGridTextBoxColumn
    {
        public static int m_RowCount;
        public DBTextBox ColumnText;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;


        public DataGridDBTextBox2Column_borrar()
        {
            m_source = null;
            m_isEditing = false;
            m_RowCount = -1;

            ColumnText = new DBTextBox();

            ColumnText.BorderStyle = 0;
            ColumnText.Leave += LeaveDate;
            ColumnText.Enter += DateStartEditing;
        }

        private void HandleScroll(object sender, EventArgs e)
        {
            ColumnText.Hide();
        }


        private void HandleMouse(object sender, MouseEventArgs e)
        {
            DataGrid.HitTestInfo hti = null;
            hti = ((DataGrid) TextBox.Parent).HitTest(e.X, e.Y);
            if (hti.Type == DataGrid.HitTestType.ColumnResize) ColumnText.Visible = false;
            m_rowNum = hti.Row;
        }


        private void DateStartEditing(object sender, EventArgs e)
        {
            if (m_isEditing) return;
            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveDate(object sender, EventArgs e)
        {
            string v = null;
            v = ColumnText.Text + "";

            if (m_isEditing)
            {
                if (DateTimeUtil.IsDate(v))
                {
                    SetColumnValueAtRow(m_source, m_rowNum, v);
                }
                else
                {
                    if (v == "") SetColumnValueAtRow(m_source, m_rowNum, "");
                }

                m_isEditing = false;
                Invalidate();
            }

            ColumnText.Hide();
            DataGridTableStyle.DataGrid.Scroll += HandleScroll;
            DataGridTableStyle.DataGrid.Leave += HandleScroll;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;

            base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);

            m_rowNum = rowNum;
            m_source = source;

            ColumnText.Parent = TextBox.Parent;
            ColumnText.Location = TextBox.Location;
            ColumnText.Size = new Size(TextBox.Size.Width, ColumnText.Size.Height);
            ColumnText.Text = TextBox.Text;
            TextBox.Visible = false;
            ColumnText.Visible = true;
            DataGridTableStyle.DataGrid.Scroll += HandleScroll;
            DataGridTableStyle.DataGrid.Leave += HandleScroll;
            DataGridTableStyle.DataGrid.CurrentCellChanged += HandleScroll;
            DataGridTableStyle.DataGrid.MouseDown += HandleMouse;

            ColumnText.BringToFront();
            ColumnText.Focus();
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            string v = null;

            v = ColumnText.Text + "";
            if (m_isEditing)
            {
                m_isEditing = false;
                SetColumnValueAtRow(dataSource, rowNum, v);
            }

            return true;
        }


        protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
        {
            var s = base.GetColumnValueAtRow(source, rowNum);
            return s;
        }


        protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
        {
            source.Position = rowNum;
            base.SetColumnValueAtRow(source, rowNum, ColumnText.Text);
        }
    }
}