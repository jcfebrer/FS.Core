#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class DataGridDataPictureColumn : DataGridColumnStyle
    {
        private readonly DataGridPictureColumn mDataPicture = new DataGridPictureColumn();
        private bool mIsEditing;

        public DataGridDataPictureColumn()
        {
            mDataPicture.Visible = false;
        }


        protected override void ConcedeFocus()
        {
            mDataPicture.ImageChanged -= dataPictureImageChanged;
            mDataPicture.Visible = false;
            base.ConcedeFocus();
        }


        protected override void SetDataGrid(DataGrid value)
        {
            base.SetDataGrid(value);
        }


        protected override void Abort(int rowNum)
        {
            mIsEditing = false;
            mDataPicture.ImageChanged -= dataPictureImageChanged;
            Invalidate();
        }


        protected override bool Commit(CurrencyManager dataSource, int rowNum)
        {
            mDataPicture.Bounds = Rectangle.Empty;
            mDataPicture.ImageChanged -= dataPictureImageChanged;
            if (!mIsEditing) return true;

            mIsEditing = false;

            try
            {
                object value = imageToByteArray(mDataPicture.Image);
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
                if (data is byte[])
                    mDataPicture.Image = byteArrayToImage((byte[]) data);
                else
                    mDataPicture.Image = DataGridPictureContextMenu.EmptyBitmap;
            }

            if (cellIsVisible)
            {
                mDataPicture.Bounds = bounds;
                mDataPicture.SizeMode = PictureBoxSizeMode.Normal;
                mDataPicture.Visible = true;
                mDataPicture.ImageChanged += dataPictureImageChanged;
            }
            else
            {
                mDataPicture.Visible = false;
            }

            if (mDataPicture.Visible) DataGridTableStyle.DataGrid.Invalidate(bounds);
        }


        protected override Size GetPreferredSize(Graphics g, object value)
        {
            return new Size(100, 100 + 4);
        }


        protected override int GetMinimumHeight()
        {
            return 100 + 4;
        }


        protected override int GetPreferredHeight(Graphics g, object value)
        {
            return 100 + 4;
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


        protected override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush,
            Brush foreBrush, bool alignToRight)
        {
            var data = GetColumnValueAtRow(source, rowNum);

            Image img = null;
            if (data is byte[]) img = byteArrayToImage((byte[]) data);

            g.FillRectangle(backBrush, bounds);
            if (img != null)
            {
                var r = justify(bounds, img.Size, Alignment);
                g.DrawImage(img, r.X, r.Y, r.Width, r.Height);
            }
        }


        protected override void SetDataGridInColumn(DataGrid value)
        {
            base.SetDataGridInColumn(value);
            if (mDataPicture.Parent != null) mDataPicture.Parent.Controls.Remove(mDataPicture);
            if (value != null) value.Controls.Add(mDataPicture);
        }


        public void dataPictureImageChanged(object sender, EventArgs e)
        {
            if (mIsEditing) return;
            mIsEditing = true;
            base.ColumnStartedEditing(mDataPicture);
        }


        private Rectangle justify(Rectangle r, Size sz, HorizontalAlignment align)
        {
            if (sz.Width < r.Width)
            {
                switch (align)
                {
                    case HorizontalAlignment.Left:
                        break;
                    case HorizontalAlignment.Right:
                        r.X += r.Width - sz.Width;
                        break;
                    case HorizontalAlignment.Center:
                        r.X += (r.Width - sz.Width) >> 1;
                        break;
                }

                r.Width = sz.Width;
            }

            if (sz.Height < r.Height)
            {
                r.Y += (r.Height - sz.Height) >> 1;
                r.Height = sz.Height;
            }

            return r;
        }


        private Image byteArrayToImage(byte[] ba)
        {
            var ms = new MemoryStream();
            Image rc = null;
            try
            {
                ms.Write(ba, 0, ba.Length);
                rc = new Bitmap(ms);
                ms.Close();
            }
            catch (ExceptionUtil e)
            {
                throw new ExceptionUtil(e);
            }

            return rc;
        }


        private byte[] imageToByteArray(Image img)
        {
            var ms = new MemoryStream();
            var format = img.RawFormat;
            if (format == ImageFormat.MemoryBmp) format = ImageFormat.Bmp;
            img.Save(ms, ImageCodecInfo.GetImageEncoders()[0], null);
            ms.Close();
            return ms.GetBuffer();
        }
    }
}