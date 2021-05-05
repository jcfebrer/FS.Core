#region

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

namespace FSFormControls
{
    [DefaultProperty("Value")]
    [ToolboxItem(false)]
    public class DataGridPropertyEditor : UserControl, IWindowsFormsEditorService, IServiceProvider,
        ITypeDescriptorContext, ISupportInitialize


    {
        private const int defaultHeight = 95;
        private IContainer components;
        private Button DropDown;
        internal ImageList imageList1;
        private TypeConverter mConverter;
        private object mDelayedValue;
        private Form mDropDownFrm;
        private ListBox mDropDownList;
        private UITypeEditor mEditor;
        public bool mHasPreviousBackColor;
        private int mHeight;
        private bool mInitializing;
        private object mObject;
        private Rectangle mPaintRectangle;
        private bool mPaintValueSupported;
        public Color mPreviousBackColor;
        private TypeConverter.StandardValuesCollection mStandardValues;
        private bool mStandardValuesExclusive;
        private Type mType;
        private bool mUseStringAsUnderlyingType;
        private TextBox textBox1;

        public DataGridPropertyEditor()
        {
            SetStyle(ControlStyles.FixedHeight | ControlStyles.Selectable, true);
            InitializeComponent();
            DropDown.Visible = false;

            DropDown.Click += DropDown_Click;
            textBox1.KeyPress += textBox1_KeyPress;
            textBox1.DoubleClick += textBox1_DoubleClick;
            textBox1.KeyDown += textBox1_KeyDown;
            textBox1.Leave += textBox1_Leave;
        }

        [Browsable(false)] public int PreferredHeight => textBox1.PreferredHeight + 4;

        [TypeConverter(typeof(TypeTypeConverter))]
        [Category("Appearance")]
        [Description("The type of the property that is being editing.")]
        public Type PropertyType
        {
            get { return mType; }
            set
            {
                if (!(mType == value))
                {
                    mType = value;
                    if (!(mType == null))
                    {
                        mEditor = (UITypeEditor) TypeDescriptor.GetEditor(mType, typeof(UITypeEditor));
                        mPaintValueSupported = !(mEditor == null) & mEditor.GetPaintValueSupported(this);
                        mConverter = TypeDescriptor.GetConverter(mType);
                        mStandardValues = null;
                        if (!(mConverter == null) & mConverter.GetStandardValuesSupported())
                        {
                            mStandardValues = mConverter.GetStandardValues(this);
                            mStandardValuesExclusive = mConverter.GetStandardValuesExclusive();
                        }

                        var editStyle = UITypeEditorEditStyle.None;
                        if (!(mEditor == null))
                            editStyle = mEditor.GetEditStyle(this);
                        else if (!(mConverter == null))
                            if (mConverter.GetStandardValuesSupported())
                            {
                                editStyle = UITypeEditorEditStyle.DropDown;
                                if (editStyle == UITypeEditorEditStyle.None)
                                {
                                    DropDown.Visible = false;
                                }
                                else
                                {
                                    DropDown.Visible = true;
                                    if (editStyle == UITypeEditorEditStyle.Modal)
                                        DropDown.ImageIndex = 1;
                                    else
                                        DropDown.ImageIndex = 0;
                                }

                                if (!(mDropDownList == null))
                                {
                                    mDropDownList.Dispose();
                                    mDropDownList = null;
                                }

                                object v = value;
                                if (!(v == null) & !(v.GetType() == mType)) Value = null;
                                PerformLayout();
                            }
                    }

                    OnPropertyTypeChanged();
                }
            }
        }

        [Category("Data")]
        [Description("Should the object be converted to a string before being written back to the data source?")]
        public bool UseStringAsUnderlyingType
        {
            get { return mUseStringAsUnderlyingType; }
            set
            {
                mUseStringAsUnderlyingType = value;
                OnUseStringAsUnderlyingTypeChanged();
            }
        }

        [Category("Data")]
        [Description("The value of the data that is being edited")]
        public object Value
        {
            get
            {
                if (mUseStringAsUnderlyingType)
                {
                    if (mObject == null)
                        return string.Empty;
                    return mConverter.ConvertToInvariantString(mObject);
                }

                return mObject == null && !DesignMode ? Convert.DBNull : mObject;
            }
            set
            {
                if (mInitializing)
                {
                    mDelayedValue = value;
                    return;
                }

                object NewObject = null;
                if (value == Convert.DBNull)
                {
                    NewObject = null;
                }
                else if (mUseStringAsUnderlyingType)
                {
                    var s = Convert.ToString(value);
                    if (!(s == null))
                        try
                        {
                            NewObject = mConverter.ConvertFromInvariantString(s);
                        }
                        catch
                        {
                        }
                }
                else
                {
                    NewObject = value;
                    if (!(NewObject == mObject)) mObject = NewObject;

                    if (!(mObject == null) & !(mConverter == null))
                    {
                        textBox1.Text = mConverter.ConvertToString(mObject);
                    }
                    else
                    {
                        textBox1.Text = "";
                        OnValueChanged();
                        Invalidate();
                    }
                }
            }
        }

        protected new IContainer Container => null;

        protected object Instance => null;

        protected PropertyDescriptor PropertyDescriptor => null;

        private ListBox dropDownList
        {
            get
            {
                if ((mDropDownList == null) & !(mStandardValues == null))
                {
                    mDropDownList = new ListBox();
                    foreach (var obj in mStandardValues) mDropDownList.Items.Add(obj);
                    mDropDownList.MouseUp += dropDownList_MouseUp;
                    mDropDownList.KeyPress += dropDownList_KeyPress;
                    mDropDownList.Font = Font;
                    mDropDownList.ForeColor = ForeColor;
                    mDropDownList.BackColor = BackColor;
                    mDropDownList.Size = new Size(Size.Width, 95);
                    mDropDownList.IntegralHeight = true;
                }

                return mDropDownList;
            }
        }

        #region IServiceProvider Members

        object IServiceProvider.GetService(Type serviceType)
        {
            return GetService(serviceType);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            components = new Container();
            var resources = new ComponentResourceManager(typeof(DataGridPropertyEditor));
            DropDown = new Button();
            imageList1 = new ImageList(components);
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // DropDown
            // 
            DropDown.BackColor = SystemColors.Control;
            DropDown.ImageIndex = 0;
            DropDown.ImageList = imageList1;
            DropDown.Location = new Point(133, 1);
            DropDown.Name = "DropDown";
            DropDown.Size = new Size(74, 23);
            DropDown.TabIndex = 0;
            DropDown.TabStop = false;
            DropDown.UseVisualStyleBackColor = false;
            // 
            // imageList1
            // 
            imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "");
            imageList1.Images.SetKeyName(1, "");
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(132, 13);
            textBox1.TabIndex = 1;
            // 
            // PropertyEditor
            // 
            BackColor = SystemColors.Window;
            Controls.Add(textBox1);
            Controls.Add(DropDown);
            Name = "PropertyEditor";
            Padding = new System.Windows.Forms.Padding(1);
            Size = new Size(150, 16);
            ResumeLayout(false);
            PerformLayout();
        }


        public event ValueChangedEventHandler ValueChanged;

        protected void OnValueChanged()
        {
            if (null != ValueChanged) ValueChanged(this, EventArgs.Empty);
        }


        public event PropertyTypeChangedEventHandler PropertyTypeChanged;

        protected void OnPropertyTypeChanged()
        {
            if (null != PropertyTypeChanged) PropertyTypeChanged(this, EventArgs.Empty);
        }


        public event UseStringAsUnderlyingTypeChangedEventHandler UseStringAsUnderlyingTypeChanged;

        protected void OnUseStringAsUnderlyingTypeChanged()
        {
            if (null != UseStringAsUnderlyingTypeChanged) UseStringAsUnderlyingTypeChanged(this, EventArgs.Empty);
        }


        public void DrawData(Graphics g, RectangleF rect, object data, Brush foreBrush, StringFormat format)
        {
            if (!(mEditor == null))
            {
                rect.Offset(0, 2);
                rect.Height -= 2;

                if ((data == null) | (data == DBNull.Value)) return;
                if (mUseStringAsUnderlyingType) data = mConverter.ConvertFromInvariantString(Convert.ToString(data));
                if (mPaintValueSupported)
                {
                    var paintRectangle = new Rectangle(Convert.ToInt32(rect.Left), Convert.ToInt32(rect.Top - 1),
                        mHeight, mHeight);
                    paintRectangle.Inflate(new Size(-2, -2));
                    paintRectangle.Height -= 1;
                    mEditor.PaintValue(data, g, paintRectangle);
                    g.DrawRectangle(Pens.Black, paintRectangle.Left, paintRectangle.Top, paintRectangle.Width,
                        paintRectangle.Height);
                    rect = new RectangleF(rect.Left + mHeight, rect.Top, rect.Width - mHeight, mHeight);
                }

                g.DrawString(mConverter.ConvertToString(data), Font, foreBrush, rect, format);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mPaintValueSupported & Enabled)
            {
                var r = mPaintRectangle;
                var g = e.Graphics;
                mEditor.PaintValue(mObject, g, r);
                g.DrawRectangle(Pens.Black, r.Left, r.Top, r.Width, r.Height);
            }
        }


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            ControlPaint.DrawBorder3D(pevent.Graphics, new Rectangle(Point.Empty, Size), Border3DStyle.Sunken);
        }


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (Height != textBox1.Height + 8) Height = textBox1.Height + 8;
            var clientRect = ClientRectangle;
            clientRect.Inflate(new Size(-1, -1));
            var left = clientRect.Left;
            var top = clientRect.Top;
            var width = clientRect.Width;
            var height = clientRect.Height;
            var buttonWidth = 0;
            if (DropDown.Visible) buttonWidth = height;
            mHeight = height;
            if (mPaintValueSupported)
            {
                mPaintRectangle = new Rectangle(left, top, height, height);
                mPaintRectangle.Inflate(new Size(-2, -2));
                mPaintRectangle.Height -= 1;
                left += height;
                width -= height;
            }

            var textWidth = width;
            if (DropDown.Visible)
                textWidth -= height;
            else
                textWidth -= 1;

            textBox1.SetBounds(left + 2, top + 2, textWidth - 2, height);

            DropDown.SetBounds(left + textWidth + 1, top + 1, height - 2, height - 2);
            Invalidate();
        }


        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (!(mDropDownList == null))
            {
                mDropDownList.Dispose();
                mDropDownList = null;
            }
        }


        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (!(mDropDownList == null))
            {
                mDropDownList.Dispose();
                mDropDownList = null;
            }
        }


        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (!(mDropDownList == null))
            {
                mDropDownList.Dispose();
                mDropDownList = null;
            }
        }


        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (Enabled)
            {
                textBox1.Visible = true;
                DropDown.Enabled = true;
                if (mHasPreviousBackColor)
                {
                    BackColor = mPreviousBackColor;
                    mHasPreviousBackColor = false;
                }
            }
            else
            {
                textBox1.Visible = false;
                DropDown.Enabled = false;
                mPreviousBackColor = BackColor;
                mHasPreviousBackColor = true;
                BackColor = SystemColors.Control;
            }
        }


        public void BeginInit()
        {
            mInitializing = true;
        }

        // interface methods implemented by BeginInit


        public void EndInit()
        {
            mInitializing = false;
            if (!(mDelayedValue == null))
            {
                Value = mDelayedValue;
                mDelayedValue = null;
            }
        }

        // interface methods implemented by EndInit


        public void DropDownControl(Control control)
        {
            var p = DropDown.Location;
            p.Y += DropDown.Height;
            p.X += DropDown.Width;

            var l = PointToScreen(p);
            var frm = new Form();

            frm.StartPosition = FormStartPosition.Manual;

            var Size = control.Size;
            frm.Size = Size;
            l.X -= frm.Width - 1;
            frm.Location = l;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowInTaskbar = false;
            frm.BackColor = SystemColors.ActiveCaption;
            control.Location = new Point(0, 0);
            control.Size = frm.Size;
            control.Layout += control_Layout;
            var panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            frm.Controls.Add(panel);
            panel.Controls.Add(control);
            mDropDownFrm = frm;
            frm.Deactivate += frm_Deactivate;
            frm.Show();
            doModalLoop();
            panel.Controls.Remove(control);
            frm.Close();
        }

        // interface methods implemented by DropDownControl

        private void control_Layout(object sender, LayoutEventArgs e)
        {
            if (!(mDropDownFrm == null))
            {
                var Size = ((Control) sender).Size;

                if (sender == mDropDownList)
                {
                    var height = defaultHeight;
                    var maxHeight = mDropDownList.ItemHeight * mDropDownList.Items.Count + 4;
                    if (height > maxHeight) height = maxHeight;
                    if (Size.Height != height)
                    {
                        Size.Height = height;
                        mDropDownList.Size = Size;
                    }
                }

                mDropDownFrm.Size = Size;
            }
        }


        public void CloseDropDown()
        {
            if (!(mDropDownFrm == null)) mDropDownFrm.Visible = false;
        }

        // interface methods implemented by CloseDropDown


        public DialogResult ShowDialog(Form dialog)
        {
            return dialog.ShowDialog(TopLevelControl);
        }

        // interface methods implemented by ShowDialog


        protected override object GetService(Type serviceType)
        {
            if (serviceType is IWindowsFormsEditorService)
                return this;
            return base.GetService(serviceType);
        }

        // interface methods implemented by GetService


        public bool OnComponentChanging()
        {
            return false;
        }

        // interface methods implemented by OnComponentChanging


        public void OnComponentChanged()
        {
        }

        // interface methods implemented by OnComponentChanged


        private void frm_Deactivate(object sender, EventArgs e)
        {
            CloseDropDown();
        }


        private void DropDown_Click(object sender, EventArgs e)
        {
            if (!(mEditor == null))
            {
                mObject = mEditor.EditValue(this, this, mObject);
                if (!(mObject == null))
                    textBox1.Text = mConverter.ConvertToString(mObject);
                else
                    textBox1.Text = "";
                OnValueChanged();
                Invalidate();
            }
            else
            {
                if (!(mStandardValues == null))
                {
                    dropDownList.SelectedItem = Value;
                    DropDownControl(dropDownList);
                }
            }
        }


        private void dropDownList_MouseUp(object sender, MouseEventArgs e)
        {
            Value = mDropDownList.SelectedItem;
            CloseDropDown();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(mStandardValues == null) & mStandardValuesExclusive)
            {
                var key = new string(e.KeyChar, 1);
                var lowerKey = key.ToLower();
                var upperKey = key.ToUpper();

                var foundCurrent = false;
                object firstMatch = null;
                object match = null;
                foreach (var obj in mStandardValues)
                    if (obj == mObject)
                    {
                        foundCurrent = true;
                    }
                    else
                    {
                        var s = obj.ToString();
                        if (s.StartsWith(lowerKey) | s.StartsWith(upperKey))
                        {
                            if (foundCurrent)
                            {
                                match = obj;
                                break;
                            }

                            if (firstMatch == null) firstMatch = obj;
                        }
                    }

                if (match == null) match = firstMatch;
                if (!(match == null))
                {
                    Value = match;
                    textBox1.SelectAll();
                }

                e.Handled = true;
            }
        }


        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (!(mStandardValues == null))
            {
                object firstObj = null;
                var wantNextObj = false;
                foreach (var obj in mStandardValues)
                {
                    if (wantNextObj)
                    {
                        wantNextObj = false;
                        Value = obj;
                        break;
                    }

                    if (firstObj == null) firstObj = obj;
                    if (obj.Equals(mObject)) wantNextObj = true;
                }

                if (wantNextObj)
                {
                    Value = firstObj;
                    textBox1.SelectAll();
                }
            }
        }


        private void dropDownList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys) e.KeyChar == Keys.Return)
            {
                Value = mDropDownList.SelectedItem;
                CloseDropDown();
                e.Handled = true;
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F4) & DropDown.Visible) DropDown.PerformClick();
        }


        private void textBox1_Leave(object sender, EventArgs e)
        {
            object NewObject = null;
            if (!(mConverter == null))
                try
                {
                    var s = textBox1.Text;
                    if (s.Length > 0)
                        NewObject = mConverter.ConvertFromString(s);
                    else
                        NewObject = null;
                }
                catch
                {
                    NewObject = mObject;
                    mObject = null;
                }

            if (!(NewObject == mObject))
            {
                mObject = NewObject;

                if (!(mObject == null) & !(mConverter == null))
                {
                    textBox1.Text = mConverter.ConvertToString(mObject);
                }
                else
                {
                    textBox1.Text = "";
                    OnValueChanged();
                    Invalidate();
                }
            }
        }


        private int MsgWaitForMultipleObjects(int nCount, IntPtr pHandles, short bWaitAll, int dwMilliseconds,
            int dwWakeMask)
        {
            return 0;
        }


        private void doModalLoop()
        {
            while (mDropDownFrm.Visible)
            {
                Application.DoEvents();
                MsgWaitForMultipleObjects(1, IntPtr.Zero, 1, 5, 255);
            }
        }

        #region Delegates

        public delegate void PropertyTypeChangedEventHandler(object sender, EventArgs e);

        public delegate void UseStringAsUnderlyingTypeChangedEventHandler(object sender, EventArgs e);

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);

        #endregion

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
            BeginInit();
        }

        void ISupportInitialize.EndInit()
        {
            EndInit();
        }

        #endregion

        #region ITypeDescriptorContext Members

        bool ITypeDescriptorContext.OnComponentChanging()
        {
            return OnComponentChanging();
        }

        void ITypeDescriptorContext.OnComponentChanged()
        {
            OnComponentChanged();
        }

        IContainer ITypeDescriptorContext.Container => Container;

        object ITypeDescriptorContext.Instance => Instance;

        PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor => PropertyDescriptor;

        #endregion

        #region IWindowsFormsEditorService Members

        void IWindowsFormsEditorService.DropDownControl(Control control)
        {
            DropDownControl(control);
        }

        void IWindowsFormsEditorService.CloseDropDown()
        {
            CloseDropDown();
        }

        DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
        {
            return ShowDialog(dialog);
        }

        #endregion
    }


    public class TypeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.ToString().ToLower() == "string") return true;
            return base.CanConvertFrom(context, sourceType);
        }


        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.ToString().ToLower() == "string") return true;
            return base.CanConvertTo(context, destinationType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var typeResolver =
                    (ITypeResolutionService) context.GetService(typeof(ITypeResolutionService));
                Type t = null;
                var typeName = Convert.ToString(value);
                if (!(typeResolver == null))
                {
                    t = typeResolver.GetType(typeName);
                }
                else
                {
                    t = Type.GetType(typeName);
                    if (t == null) t = typeof(Color).Assembly.GetType(typeName);
                    return t;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }


        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (destinationType.ToString().ToLower() == "string")
            {
                return value.ToString();
            }

            if (destinationType.ToString().ToLower() == "instancedescriptor")
                return new InstanceDescriptor(typeof(Type).GetConstructor(new Type[1] {typeof(string)}),
                    new object[] {value.ToString()});
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}