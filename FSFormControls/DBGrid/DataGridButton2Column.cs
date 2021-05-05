#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public delegate void DataGridCellButton2ClickEventHandler(object sender, DataGridCellButton2ClickEventArgs e);

    public class DataGridButton2Column : DataGridTextBoxColumn
    {
        private readonly Bitmap m_buttonFace;
        private readonly Bitmap m_buttonFacePressed;
        private readonly int m_columnNum;
        private readonly DBColumn m_dbcolumn;
        private readonly DBGrid m_dbgrid;
        private int m_pressedRow;


        public DataGridButton2Column(int colNum, DBColumn dbcolumn, DBGrid dbg)
        {
            m_columnNum = colNum;
            m_pressedRow = -1;
            m_dbgrid = dbg;
            m_dbcolumn = dbcolumn;

            try
            {
                var strm = GetType().Assembly.GetManifestResourceStream("FSFormControls.DBFullbuttonface.bmp");
                m_buttonFace = new Bitmap(strm);
                strm = GetType().Assembly.GetManifestResourceStream("FSFormControls.DBFullbuttonfacepressed.bmp");
                m_buttonFacePressed = new Bitmap(strm);
            }
            catch
            {
            }

            m_dbgrid.DataGrid1.MouseDown += HandleMouseDown;
            m_dbgrid.DataGrid1.MouseUp += HandleMouseUp;
        }

        public event DataGridCellButton2ClickEventHandler CellButton2Clicked;

        private void DrawButton(Graphics g, Bitmap bm, Rectangle bounds, int row)
        {
            var dg = DataGridTableStyle.DataGrid;
            var s = dg[row, m_columnNum].ToString();
            if (string.IsNullOrEmpty(s)) s = m_dbcolumn.HeaderCaption;

            var sz = g.MeasureString(s, dg.Font, bounds.Width - 4, StringFormat.GenericTypographic);

            var x = bounds.Left + Math.Max(0, Convert.ToInt32((bounds.Width - sz.Width) / 2));
            g.DrawImage(bm, bounds, 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel);

            if (sz.Height < bounds.Height)
            {
                var y = Convert.ToInt32(bounds.Top + (bounds.Height - sz.Height) / 2);
                if (m_buttonFacePressed == bm) x = x + 1;
                g.DrawString(s, dg.Font, new SolidBrush(dg.ForeColor), x, y);
            }
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
        }


        public void HandleMouseUp(object sender, MouseEventArgs e)
        {
            var dg = DataGridTableStyle.DataGrid;
            var hti = dg.HitTest(new Point(e.X, e.Y));
            var isClickInCell = (hti.Column == m_columnNum) & (hti.Row > -1);

            m_pressedRow = -1;

            var rect = new Rectangle(0, 0, 0, 0);

            if (isClickInCell)
            {
                rect = dg.GetCellBounds(hti.Row, hti.Column);
                isClickInCell = e.X > rect.Right - m_buttonFace.Width;
            }

            if (isClickInCell)
            {
                var g = Graphics.FromHwnd(dg.Handle);
                DrawButton(g, m_buttonFace, rect, hti.Row);
                g.Dispose();
                if (null != CellButton2Clicked)
                    CellButton2Clicked(this, new DataGridCellButton2ClickEventArgs(hti.Row, hti.Column));
            }
        }


        public void HandleMouseDown(object sender, MouseEventArgs e)
        {
            var dg = DataGridTableStyle.DataGrid;
            var hti = dg.HitTest(new Point(e.X, e.Y));
            var isClickInCell = (hti.Column == m_columnNum) & (hti.Row > -1);
            var rect = new Rectangle(0, 0, 0, 0);
            if (isClickInCell)
            {
                rect = dg.GetCellBounds(hti.Row, hti.Column);
                isClickInCell = e.X > rect.Right - m_buttonFace.Width;
            }

            if (isClickInCell)
            {
                var g = Graphics.FromHwnd(dg.Handle);
                DrawButton(g, m_buttonFacePressed, rect, hti.Row);
                g.Dispose();
                m_pressedRow = hti.Row;
            }
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var parent = DataGridTableStyle.DataGrid;
            var current = parent.IsSelected(rowNum) |
                          ((parent.CurrentRowIndex == rowNum) & (parent.CurrentCell.ColumnNumber == m_columnNum));

            var BackColor = new Color();
            if (current)
                BackColor = parent.SelectionBackColor;
            else
                BackColor = parent.BackColor;
            var ForeColor = new Color();
            if (current)
                ForeColor = parent.SelectionForeColor;
            else
                ForeColor = parent.ForeColor;

            g.FillRectangle(new SolidBrush(BackColor), bounds);

            var s = GetColumnValueAtRow(source, rowNum).ToString();

            Bitmap bm = null;
            if (m_pressedRow == rowNum)
                bm = m_buttonFacePressed;
            else
                bm = m_buttonFace;
            DrawButton(g, bm, bounds, rowNum);
        }
    }


    public class DataGridCellButton2ClickEventArgs : EventArgs
    {
        public DataGridCellButton2ClickEventArgs(int row, int col)
        {
            RowIndex = row;
            ColIndex = col;
        }


        public int RowIndex { get; }

        public int ColIndex { get; }
    }
}