#region

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#endregion

internal class resfinder
{
}

namespace FSFormControls
{
    [Designer(typeof(DBControlDesigner))]
    [DesignTimeVisible(true)]
    [ToolboxItem(false)]
    public class DBUserControlBase : UserControl
    {
        public enum AccessMode
        {
            ReadMode,
            WriteMode,
            ProtectedMode
        }

        private Form m_Form;
        public DBRectTracker Tracker;

        public DBUserControlBase()
        {
            MouseDown += DBUsercontrolBase_MouseDown;

            if (m_Form != null)
                m_Form.MouseUp += m_Form_MouseUp;
        }

        //[TypeConverter(typeof(FieldList))]
        //[DefaultValueAttribute("")]
        //[Description("Campo de la base de datos que debe mostrar este control.")]
        //public string DBField { get; set; } = "";

        //[Description("DBControl asociado al control.")]
        //public DBControl DataControl { get; set; } = null;

        [Editor(typeof(EditorAbout), typeof(UITypeEditor))]
        public string About { get; set; } = "";

        public bool Track { get; set; }

        private void DBUsercontrolBase_MouseDown(object sender, MouseEventArgs e)
        {
            m_Form = FindForm();
            ((Control) sender).BringToFront();
            ((Control) sender).Capture = false;
            Tracker = (DBRectTracker) FunctionsForms.FindControlType(m_Form.Controls, "FSFormControls.DBRectTracker");
            if (Tracker != null) m_Form.Controls.Remove(Tracker);

            if (Track)
            {
                Tracker = new DBRectTracker((Control) sender);
                m_Form.Controls.Add(Tracker);
                Tracker.BringToFront();
                Tracker.Draw();
            }
        }


        private void m_Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (Track) m_Form.Controls.Remove(Tracker);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DBUsercontrolBase
            // 
            Name = "DBUsercontrolBase";
            Size = new Size(151, 145);
            ResumeLayout(false);
        }
    }


    public class EditorAbout : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var about = new frmAbout();
            about.Show();
            return null;
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }


        public new virtual bool GetPaintValueSupported()
        {
            return true;
        }
    }


    public class FieldList : StringConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(DBControl.Fields);
        }


        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }


        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }


    internal class DBControlDesigner : ControlDesigner
    {
        public override DesignerVerbCollection Verbs
        {
            get
            {
                var v = new DesignerVerbCollection();

                v.Add(new DesignerVerb("&Acerca de ...", OnAcercaDe));

                return v;
            }
        }

        private void OnAcercaDe(object sender, EventArgs e)
        {
            var about = new frmAbout();
            about.Show();
        }
    }
}