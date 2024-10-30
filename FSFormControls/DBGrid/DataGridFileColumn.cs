#region

using System;
using System.Drawing;
using System.Windows.Forms;
using FSLibrary;
using FSException;
using System.ComponentModel;

#endregion

namespace FSFormControls
{
    public class DataGridFileColumn : DataGridTextBoxColumn
    {
        private readonly DBColumn m_dbcolumn;
        private readonly DBGrid m_dbgrid;
        public DBFile ColumnFile = new DBFile();
        private int m_Col;
        private bool m_isEditing;
        private int m_rowNum;
        private CurrencyManager m_source;

        public DataGridFileColumn(int col, DBColumn dbcol, DBGrid grd)
        {
            m_Col = col;
            m_dbcolumn = dbcol;
            m_dbgrid = grd;

            m_source = null;
            m_isEditing = false;

            ReadOnly = true;

            ColumnFile.ShowText = false;
            ColumnFile.DBField = m_dbcolumn.FieldDB;
            ColumnFile.DataControl = m_dbgrid.DataControl;
            ColumnFile.UpdateFile();

            ColumnFile.Leave += LeaveFile;
            ColumnFile.FileChanged += FileStartEditing;
            ColumnFile.ModeChanged += ModeChanged;

            ColumnFile.Visible = false;
        }


        private DBControl m_DataControl;
        /// <summary>
        /// Asignación del DBcontrol.
        /// </summary>
        [Description("Control de datos para la gestión de los registros asociados.")]
        public DBControl DataControl
        {
            get { return m_DataControl; }
            set { m_DataControl = value; }
        }

        private void ModeChanged(Global.AccessMode mode)
        {
            if (mode == Global.AccessMode.ReadMode) LeaveFile(this, new EventArgs());
        }


        private void FileStartEditing(object sender, EventArgs e)
        {
            m_isEditing = true;
            base.ColumnStartedEditing((Control) sender);
        }


        private void LeaveFile(object sender, EventArgs e)
        {
            if (m_dbgrid.Mode == Global.AccessMode.ReadMode)
            {
                ColumnFile.Visible = false;
                return;
            }

            if (m_isEditing)
            {
                object v = ColumnFile.Data;
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

            ColumnFile.Visible = false;

            ColumnFile.Parent = null;
        }


        protected override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,
            string instantText, bool cellIsVisible)
        {
            if (readOnly)
            {
                ColumnFile.Visible = false;
                return;
            }


            try
            {
                m_rowNum = rowNum;
                m_source = source;

                ColumnFile.Parent = TextBox.Parent;
                ColumnFile.Font = TextBox.Font;

                var anObj = GetColumnValueAtRow(source, rowNum);

                ColumnFile.Bounds = bounds;
                try
                {
                    ColumnFile.Visible = true;
                }
                catch
                {
                }

                ColumnFile.txtFileName.Text =
                    Functions.Valor(m_dbgrid.get_RowDataValue(ColumnFile.FieldFileName, rowNum));
                ColumnFile.txtDateTime.Text =
                    Functions.Valor(m_dbgrid.get_RowDataValue(ColumnFile.FieldDateTime, rowNum));

                if (!(anObj is DBNull))
                    ColumnFile.Data = (byte[]) anObj;
                else
                    ColumnFile.Data = null;

                ColumnFile.Focus();
            }
            catch (Exception e)
            {
                throw new ExceptionUtil(e);
            }
        }


        protected override object GetColumnValueAtRow(CurrencyManager source, int rowNum)
        {
            var s = base.GetColumnValueAtRow(source, rowNum);
            if (s is DBNull) s = null;

            return s;
        }


        protected override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
        {
            source.Position = rowNum;
            base.SetColumnValueAtRow(source, rowNum, ColumnFile.Data);
        }
    }
}