#region

using System;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using DateTime = System.DateTime;
using FSException;

#endregion

namespace FSFormControls
{
    public class DataGridTimePickerColumn : DataGridColumnStyle
    {
        private readonly DateTime nullDate = new DateTime(1980, 1, 1);
        private readonly DateTimePicker timePicker = new DateTimePicker();
        private bool isEditing;

        public DataGridTimePickerColumn()
        {
            timePicker.Visible = false;
            timePicker.Format = DateTimePickerFormat.Short;
        }

        protected override void Abort(int rowNum)
        {
            isEditing = false;
            timePicker.ValueChanged -= TimePickerValueChanged;
            Invalidate();
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            timePicker.Bounds = Rectangle.Empty;

            timePicker.ValueChanged += TimePickerValueChanged;

            if (!isEditing) return true;
            isEditing = false;

            try
            {
                var value = timePicker.Value;
                SetColumnValueAtRow(dataSource, rowNum, value);
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }

            Invalidate();
            return true;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            var value = DateTime.MinValue;
            var columnValue = GetColumnValueAtRow(source, rowNum);

            if (readOnly) return;

            if (columnValue is DBNull)
                value = nullDate;
            else
                value = Convert.ToDateTime(columnValue);

            if (cellIsVisible)
            {
                timePicker.Bounds = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4);

                timePicker.Value = value;
                timePicker.Visible = true;
                timePicker.ValueChanged += TimePickerValueChanged;
            }
            else
            {
                timePicker.Value = value;
                timePicker.Visible = false;
            }

            if (timePicker.Visible) DataGridTableStyle.DataGrid.Invalidate(bounds);
        }


        protected override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(100, timePicker.PreferredHeight + 4);
        }


        protected override int GetMinimumHeight()
        {
            return timePicker.PreferredHeight + 4;
        }


        protected override int GetPreferredHeight(Graphics g, object value)
        {
            return timePicker.PreferredHeight + 4;
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
        {
            Paint(g, bounds, source, rowNum, false);
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
            bool alignToRight)
        {
            Paint(g, bounds, source, rowNum, Brushes.Red, Brushes.Blue, alignToRight);
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var columnValue = GetColumnValueAtRow(source, rowNum);
            var date = DateTime.MinValue;
            if (columnValue is DBNull)
                date = nullDate;
            else
                date = Convert.ToDateTime(columnValue);

            var rect = bounds;
            g.FillRectangle(backBrush, rect);
            rect.Offset(0, 2);
            rect.Height -= 2;
            g.DrawString(date.ToString("d"), DataGridTableStyle.DataGrid.Font, foreBrush,
                RectangleF.FromLTRB(rect.X, rect.Y, rect.Right, rect.Bottom));
        }


        protected override void SetDataGridInColumn(DataGrid value)
        {
            base.SetDataGridInColumn(value);
            if (!(timePicker.Parent == null)) timePicker.Parent.Controls.Remove(timePicker);
            if (!(value == null)) value.Controls.Add(timePicker);
        }


        private void TimePickerValueChanged(object sender, EventArgs e)
        {
            isEditing = true;
            base.ColumnStartedEditing(timePicker);
        }
    }
}