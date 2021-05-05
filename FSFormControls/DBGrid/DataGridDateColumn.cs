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
    public class DataGridDateColumn_Borrar : DataGridTextBoxColumn
    {
        private readonly int m_col;
        public DBTextBox ColumnDate;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;


        public DataGridDateColumn_Borrar(int col)
        {
            m_source = null;
            m_isEditing = false;
            m_col = col;

            ColumnDate = new DBTextBox();

            ColumnDate.BorderStyle = 0;
            ColumnDate.DataType = DBTextBox.TypeData.Date;
            ColumnDate.Leave += LeaveDate;
            ColumnDate.Enter += DateStartEditing;
        }

        private void HandleScroll(object sender, EventArgs e)
        {
            if (ColumnDate.Visible) ColumnDate.Visible = false;
        }


        private void HandleMouse(object sender, MouseEventArgs e)
        {
            DataGrid.HitTestInfo hti = null;
            hti = ((DataGrid) TextBox.Parent).HitTest(e.X, e.Y);
            if (hti.Type == DataGrid.HitTestType.ColumnResize) HandleScroll(sender, e);
        }


        private void DateStartEditing(object sender, EventArgs e)
        {
            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveDate(object sender, EventArgs e)
        {
            string v = null;
            v = ColumnDate.Text + "";

            if (m_isEditing)
            {
                if (DateTimeUtil.IsDate(v))
                {
                    SetColumnValueAtRow(m_source, m_rowNum, v);
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "");
                }
                else
                {
                    if (v == "")
                    {
                        SetColumnValueAtRow(m_source, m_rowNum, "");
                        ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "");
                    }
                    else
                    {
                        ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "Fecha Incorrecta!");
                    }
                }

                m_isEditing = false;
                Invalidate();
            }

            ColumnDate.Visible = false;
            DataGridTableStyle.DataGrid.Scroll += HandleScroll;
            DataGridTableStyle.DataGrid.Leave += HandleScroll;

            ColumnDate.Parent = null;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;

            try
            {
                base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);

                m_rowNum = rowNum;
                m_source = source;

                ColumnDate.Parent = TextBox.Parent;
                ColumnDate.Location = TextBox.Location;
                ColumnDate.Size = new Size(TextBox.Size.Width, ColumnDate.Size.Height);
                ColumnDate.Text = TextBox.Text;
                TextBox.Visible = false;
                ColumnDate.Visible = true;
                DataGridTableStyle.DataGrid.Scroll += HandleScroll;
                DataGridTableStyle.DataGrid.Leave += HandleScroll;
                DataGridTableStyle.DataGrid.CurrentCellChanged += HandleScroll;
                DataGridTableStyle.DataGrid.MouseDown += HandleMouse;

                ColumnDate.BringToFront();
                ColumnDate.Focus();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            string v = null;

            v = ColumnDate.Text + "";
            if (m_isEditing)
            {
                m_isEditing = false;
                if (DateTimeUtil.IsDate(v))
                {
                    SetColumnValueAtRow(dataSource, rowNum, v);
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "");
                }
                else
                {
                    if (v == "")
                    {
                        SetColumnValueAtRow(m_source, m_rowNum, "");
                        ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "");
                    }
                    else
                    {
                        ((DBGrid) TextBox.Parent.Parent).SetColumnError(m_rowNum, m_col, "Fecha Incorrecta!");
                    }
                }
            }

            return true;
        }


        protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
        {
            var s = base.GetColumnValueAtRow(source, rowNum);
            if (s is DBNull) s = Global.SINDEFINIR;

            return s;
        }


        protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
        {
            if (DateTimeUtil.IsDate(ColumnDate.Text))
            {
                source.Position = rowNum;
                base.SetColumnValueAtRow(source, rowNum, ColumnDate.Text);
            }
            else
            {
                if (ColumnDate.Text == "")
                {
                    source.Position = rowNum;
                    base.SetColumnValueAtRow(source, rowNum, DBNull.Value);
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(rowNum, m_col, "");
                }
                else
                {
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(rowNum, m_col, "Fecha Incorrecta!");
                }
            }
        }
    }


    public class DataGridDateColumn_borrar2 : DataGridTextBoxColumn
    {
        public DBTextBox ColumnDate = new DBTextBox();
        private readonly int m_Col = 0;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;

        public DataGridDateColumn_borrar2(int col)
        {
            m_source = null;
            m_isEditing = false;

            ReadOnly = true;
            ColumnDate.DataType = DBTextBox.TypeData.Date;
            ColumnDate.BorderStyle = BorderStyle.FixedSingle;

            ColumnDate.Leave += LeaveDate;
            ColumnDate.KeyPress += DateStartEditing;

            ColumnDate.Visible = false;
        }

        private void DateStartEditing(object sender, KeyPressEventArgs e)
        {
            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveDate(object sender, EventArgs e)
        {
            var dbg = (DBGrid) TextBox.Parent.Parent;
            if (dbg.Mode == DBUserControlBase.AccessMode.ReadMode)
            {
                ColumnDate.Visible = false;
                return;
            }

            if (m_isEditing)
            {
                var v = ColumnDate.Text + "";
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

            ColumnDate.Visible = false;

            ColumnDate.Parent = null;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;


            m_rowNum = rowNum;
            m_source = source;


            ColumnDate.Parent = TextBox.Parent;
            ColumnDate.Font = TextBox.Font;

            var anObj = GetColumnValueAtRow(source, rowNum);

            ColumnDate.Bounds = bounds;
            ColumnDate.Visible = true;

            if (!(anObj is DBNull))
                ColumnDate.Text = Convert.ToString(anObj);
            else
                ColumnDate.Text = Global.SINDEFINIR;

            ColumnDate.Focus();
        }


        protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
        {
            var s = base.GetColumnValueAtRow(source, rowNum);
            if (s is DBNull) s = Global.SINDEFINIR;

            return s;
        }


        protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
        {
            if (DateTimeUtil.IsDate(ColumnDate.Text))
            {
                source.Position = rowNum;
                base.SetColumnValueAtRow(source, rowNum, ColumnDate.Text);
                ((DBGrid) TextBox.Parent.Parent).SetColumnError(rowNum, m_Col, "");
            }
            else
            {
                if (ColumnDate.Text == "")
                {
                    source.Position = rowNum;
                    base.SetColumnValueAtRow(source, rowNum, DBNull.Value);
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(rowNum, m_Col, "");
                }
                else
                {
                    ((DBGrid) TextBox.Parent.Parent).SetColumnError(rowNum, m_Col, "Fecha Incorrecta!");
                }
            }
        }
    }
}