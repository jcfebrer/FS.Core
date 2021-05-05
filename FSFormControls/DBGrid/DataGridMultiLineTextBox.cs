#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DataGridMultiLineTextBox : DataGridTextBoxColumn
    {
        private bool _isEditing;
        private int _rowNumber;
        private CurrencyManager _source;

        public DataGridMultiLineTextBox()
        {
            _source = null;
            _isEditing = false;

            TextBox.ScrollBars = ScrollBars.Vertical;

            base.TextBox.TextChanged += TextBoxStartEditing;
            base.TextBox.Leave += LeaveTextBox;
        }

        private void TextBoxStartEditing(object sender, EventArgs e)
        {
            _isEditing = true;

            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveTextBox(object sender, EventArgs e)
        {
            if (_isEditing)
            {
                SetColumnValueAtRow(_source, _rowNumber, base.TextBox.Text);
                _isEditing = false;
            }

            Invalidate();
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly1,
            string instantText, bool cellIsVisible)
        {
            base.Edit(source, rowNum, bounds, readOnly1, instantText, cellIsVisible);

            _rowNumber = rowNum;
            _source = source;
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            if (_isEditing)
            {
                _isEditing = false;
                SetColumnValueAtRow(dataSource, rowNum, base.TextBox.Text);
            }

            TextBox.Visible = false;
            return true;
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);

            g.FillRectangle(backBrush, bounds);
            var vText = Convert.ToString(base.GetColumnValueAtRow(source, rowNum));
            g.DrawString(vText, TextBox.Font, foreBrush,
                new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height));
        }
    }
}