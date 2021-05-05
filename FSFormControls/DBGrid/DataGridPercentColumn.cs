#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DataGridPercentColumn : DataGridDBTextBoxColumn
    {
        private readonly int m_columnNum;
        private DBColumn m_dbcolumn;
        private DBGrid m_dbgrid;


        public DataGridPercentColumn(int colNum, DBColumn dbcolumn, DBGrid dbg)
        {
            m_columnNum = colNum;
            m_dbgrid = dbg;
            m_dbcolumn = dbcolumn;
        }

        private void DrawValue(Graphics g, Rectangle bounds, int row)
        {
            var dg = DataGridTableStyle.DataGrid;
            var s = dg[row, m_columnNum].ToString();

            if (!string.IsNullOrEmpty(s))
            {
                s = s + " %";
            }
            else
            {
                s = "0 %";
                dg[row, m_columnNum] = 0;
            }

            var sz = g.MeasureString(s, dg.Font, bounds.Width - 4, StringFormat.GenericTypographic);

            var x = bounds.Left + Math.Max(0, Convert.ToInt32((bounds.Width - sz.Width) / 2));

            if (sz.Height < bounds.Height)
            {
                var y = Convert.ToInt32(bounds.Top + (bounds.Height - sz.Height) / 2);
                g.DrawString(s, dg.Font, new SolidBrush(dg.ForeColor), x, y);
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

            DrawValue(g, bounds, rowNum);
        }
    }
}