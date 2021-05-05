#region

using System;
using System.Drawing;
using System.Windows.Forms;
using FSFormControls.Properties;

#endregion

namespace FSFormControls
{
    public delegate void DataGridCellButtonClickEventHandler(
        object sender, DataGridCellButtonClickEventArgs e, ref object value);

    public class DataGridButtonColumn : DataGridTextBoxColumn
    {
        private readonly int m_colNum;
        private readonly DBColumn m_dbcolumn;
        public Button ButtonColumn = new Button();
        private int m_rowNum;
        private CurrencyManager m_source;


        public DataGridButtonColumn(int colNum, DBColumn dbcolumn)
        {
            m_colNum = colNum;
            m_dbcolumn = dbcolumn;

            //System.IO.Stream strm = this.GetType().Assembly.GetManifestResourceStream( "DBGridThreePoints.gif" ); 
            ButtonColumn.Image = Resources.DBGridThreePoints; //Image.FromStream( strm ); 
            ButtonColumn.Size = new Size(16, 16);
            ButtonColumn.FlatStyle = FlatStyle.Standard;
            ButtonColumn.BackColor = Color.LightGray;
            ButtonColumn.Click += ButtonColumn_Click;
        }

        public event DataGridCellButtonClickEventHandler CellButtonClicked;

        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (m_dbcolumn.ActiveColumnDBButtonOnReadMode == false)
                if (readOnly)
                    return;

            base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);

            m_rowNum = rowNum;
            m_source = source;

            ButtonColumn.Parent = TextBox.Parent;
            ButtonColumn.Top = TextBox.Top - 1;
            ButtonColumn.Left = TextBox.Left + TextBox.Width - ButtonColumn.Width;
            ButtonColumn.Visible = true;
            DataGridTableStyle.DataGrid.Scroll += HandleScroll;
            DataGridTableStyle.DataGrid.Leave += HandleScroll;
            DataGridTableStyle.DataGrid.CurrentCellChanged += HandleScroll;
            DataGridTableStyle.DataGrid.MouseDown += HandleMouse;

            ButtonColumn.BringToFront();
        }


        private void HandleScroll(object sender, EventArgs e)
        {
            ButtonColumn.Hide();
        }


        private void HandleMouse(object sender, MouseEventArgs e)
        {
            DataGrid.HitTestInfo hti = null;
            hti = ((DataGrid) TextBox.Parent).HitTest(e.X, e.Y);
            if (hti.Type == DataGrid.HitTestType.ColumnResize) ButtonColumn.Visible = false;
        }


        private void ButtonColumn_Click(object sender, EventArgs e)
        {
            object value = null;
            base.ColumnStartedEditing((Control) sender);
            if (null != CellButtonClicked)
                CellButtonClicked(sender, new DataGridCellButtonClickEventArgs(m_rowNum, m_colNum), ref value);
            if (value != null) base.SetColumnValueAtRow(m_source, m_rowNum, value);
        }
    }


    public class DataGridCellButtonClickEventArgs : EventArgs
    {
        public DataGridCellButtonClickEventArgs(int row, int col)
        {
            RowIndex = row;
            ColIndex = col;
        }

        public int RowIndex { get; }

        public int ColIndex { get; }
    }
}