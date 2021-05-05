#region

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class DataGridComboBoxColumn : DataGridTextBoxColumn
    {
        private readonly DBColumn m_dbcolumn;
        private readonly DBGrid m_dbgrid;
        public DBCombo ColumnComboBox = new DBCombo();

        private Color m_BackGroundColor;
        private Color m_ForeGroundColor;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;

        public DataGridComboBoxColumn(DBColumn dbcolumn, DBGrid dbgrid)
        {
            m_source = null;
            m_isEditing = false;

            m_dbcolumn = dbcolumn;
            m_dbgrid = dbgrid;

            ReadOnly = true;
            ColumnComboBox.Mode = DBUserControlBase.AccessMode.WriteMode;
            ColumnComboBox.GridMode = true;
            ColumnComboBox.Visible = false;

            ColumnComboBox.Leave += LeaveComboBox;
            ColumnComboBox.SelectionChangeCommitted += ComboStartEditing;
        }

        public Color BackGroundColour
        {
            get { return m_BackGroundColor; }
            set { m_BackGroundColor = value; }
        }

        public Color ForeGroundColour
        {
            get { return m_ForeGroundColor; }
            set { m_ForeGroundColor = value; }
        }

        private void ComboStartEditing(object sender, EventArgs e)
        {
            if (ColumnComboBox.BlankSelection)
                ColumnComboBox.DataControlList.Go(ColumnComboBox.SelectedIndex - 1);
            else
                ColumnComboBox.DataControlList.Go(ColumnComboBox.SelectedIndex);

            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveComboBox(object sender, EventArgs e)
        {
            if (m_dbgrid.Mode == DBUserControlBase.AccessMode.ReadMode)
            {
                ColumnComboBox.Visible = false;
                return;
            }

            if (m_isEditing)
            {
                var objValue = ColumnComboBox.SelectedValue;
                if (objValue is DBNull) objValue = DBNull.Value;
                try
                {
                    base.SetColumnValueAtRow(m_source, m_rowNum, objValue);
                }
                catch
                {
                    Abort(m_rowNum);
                }

                m_isEditing = false;
            }

            ColumnComboBox.Visible = false;

            ColumnComboBox.Parent = null;

            m_dbgrid.UpdateAsociatedColumns(m_rowNum, false, false);
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly) return;
            if (m_dbcolumn.ReadColumn) return;


            m_rowNum = rowNum;
            m_source = source;


            ColumnComboBox.Parent = TextBox.Parent;
            ColumnComboBox.Font = TextBox.Font;

            var anObj = base.GetColumnValueAtRow(source, rowNum);

            ColumnComboBox.BeginUpdate();
            ColumnComboBox.Bounds = bounds;
            ColumnComboBox.Visible = true;


            if (!(anObj is DBNull))
                ColumnComboBox.SelectedValue = anObj;
            else
                ColumnComboBox.SelectedIndex = 0;

            ColumnComboBox.EndUpdate();
            ColumnComboBox.Focus();
        }


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var strData = Global.SINDEFINIR;
            var strMember = ColumnComboBox.DisplayMember;
            var strValue = ColumnComboBox.ValueMember;
            var objSource = ColumnComboBox.DataControlList.DataTable;
            DataRow[] aRowA = null;
            var sstr = "";
            double v = 0;

            var anObj = base.GetColumnValueAtRow(source, rowNum);

            if (!(anObj is DBNull))
            {
                try
                {
                    v = NumberUtils.NumberDouble(anObj);
                    if (anObj is double)
                        sstr = strValue + " = " + TextUtil.Replace(anObj.ToString(), ",", ".");
                    else
                        sstr = strValue + " = " + anObj;
                    aRowA = objSource.Select(sstr);
                }
                catch (Exception ex)
                {
                    throw new ExceptionUtil("Expresión: " + sstr, ex);
                }

                if (aRowA.Length > 0) strData = aRowA[0][strMember].ToString();
            }
            else
            {
                strData = Global.SINDEFINIR;
                v = 0;
            }

            var rect = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            if (m_BackGroundColor.GetType() == Color.Transparent.GetType())
                g.FillRectangle(backBrush, rect);
            else
                g.FillRectangle(new SolidBrush(m_BackGroundColor), rect);

            rect.Y += 2;

            if (!(ColumnComboBox.ImageList == null))
            {
                var im = Convert.ToInt32(v);

                if (im >= ColumnComboBox.ImageList.Images.Count) im = ColumnComboBox.ImageList.Images.Count - 1;
                if (im < 0) im = 0;
                var myImage = ColumnComboBox.ImageList.Images[im];

                if (!((v == 0) & ColumnComboBox.BlankSelection))
                {
                    g.DrawImage(myImage, rect.X + 5, rect.Top + Convert.ToInt64(rect.Height - myImage.Height) / 2);
                    g.DrawString(strData, TextBox.Font, foreBrush, rect.X + myImage.Width + 5, rect.Top);
                }
            }
            else
            {
                if (m_ForeGroundColor.GetType() == Color.Transparent.GetType())
                    g.DrawString(strData, TextBox.Font, foreBrush, rect);
                else
                    g.DrawString(strData, TextBox.Font, new SolidBrush(m_ForeGroundColor), rect);
            }
        }
    }
}