#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DataGridPropertyColumn : DataGridColumnStyle
    {
        private readonly DataGridPropertyEditor mPropertyEditor = new DataGridPropertyEditor();
        private bool mIsEditing;

        public DataGridPropertyColumn()
        {
            mPropertyEditor.Visible = false;
        }

        [TypeConverter(typeof(TypeTypeConverter))]
        [Category("Appearance")]
        [Description("The type of the property that is being editing.")]
        public Type PropertyType
        {
            get { return mPropertyEditor.PropertyType; }
            set { mPropertyEditor.PropertyType = value; }
        }

        [Category("Appearance")]
        [Description("Should the data be convertered to a string before being written back to the data source?")]
        public bool UseStringAsUnderlyingType
        {
            get { return mPropertyEditor.UseStringAsUnderlyingType; }
            set { mPropertyEditor.UseStringAsUnderlyingType = value; }
        }


        protected override void SetDataGrid(DataGrid value)
        {
            base.SetDataGrid(value);
            mPropertyEditor.Font = DataGridTableStyle.DataGrid.Font;
            mPropertyEditor.ForeColor = DataGridTableStyle.ForeColor;
            mPropertyEditor.BackColor = DataGridTableStyle.BackColor;
        }


        protected override void ConcedeFocus()
        {
            mPropertyEditor.ValueChanged -= propertyEditorValueChanged;
            mPropertyEditor.Visible = false;
            base.ConcedeFocus();
        }


        protected override void Abort(int rowNum)
        {
            mIsEditing = false;
            mPropertyEditor.ValueChanged -= propertyEditorValueChanged;
            Invalidate();
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            mPropertyEditor.Bounds = Rectangle.Empty;
            mPropertyEditor.ValueChanged -= propertyEditorValueChanged;
            if (!mIsEditing) return true;

            mIsEditing = false;

            try
            {
                var value = mPropertyEditor.Value;
                SetColumnValueAtRow(dataSource, rowNum, value);
            }
            catch
            {
                Abort(rowNum);
                return false;
            }

            Invalidate();
            return true;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (!mIsEditing)
            {
                var data = GetColumnValueAtRow(source, rowNum);
                mPropertyEditor.Value = data;
            }

            if (cellIsVisible)
            {
                mPropertyEditor.Bounds = bounds;
                mPropertyEditor.Visible = true;
            }
            else
            {
                mPropertyEditor.Visible = false;
            }

            if (mPropertyEditor.Visible) DataGridTableStyle.DataGrid.Invalidate(bounds);
        }


        protected override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(100, mPropertyEditor.PreferredHeight + 4);
        }


        protected override int GetMinimumHeight()
        {
            return mPropertyEditor.PreferredHeight + 4;
        }


        protected override int GetPreferredHeight(Graphics g, object value)
        {
            return mPropertyEditor.PreferredHeight + 4;
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
        {
            Paint(g, bounds, source, rowNum, false);
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum,
            bool alignToRight)
        {
            Paint(g, bounds, source, rowNum, new SolidBrush(DataGridTableStyle.BackColor),
                new SolidBrush(DataGridTableStyle.ForeColor), alignToRight);
        }


        protected void Paint(Graphics g, RectangleF bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var data = GetColumnValueAtRow(source, rowNum);

            var rect = bounds;
            var format = new StringFormat();
            if (alignToRight) format.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            switch (Alignment)
            {
                case HorizontalAlignment.Left:
                    format.Alignment = StringAlignment.Near;
                    break;
                case HorizontalAlignment.Right:
                    format.Alignment = StringAlignment.Far;
                    break;
                case HorizontalAlignment.Center:
                    format.Alignment = StringAlignment.Center;
                    break;
            }

            format.FormatFlags = StringFormatFlags.NoWrap;
            g.FillRectangle(backBrush, rect);
            mPropertyEditor.DrawData(g, rect, data, foreBrush, format);
            format.Dispose();
        }


        protected override void SetDataGridInColumn(DataGrid value)
        {
            base.SetDataGridInColumn(value);
            if (!(mPropertyEditor.Parent == null)) mPropertyEditor.Parent.Controls.Remove(mPropertyEditor);
            if (!(value == null)) value.Controls.Add(mPropertyEditor);
        }


        private void propertyEditorValueChanged(object sender, EventArgs e)
        {
            if (mIsEditing) return;
            mIsEditing = true;
            base.ColumnStartedEditing(mPropertyEditor);
        }
    }
}