#region

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    public class DBTableStyle : DataGridTableStyle
    {
        [Editor(typeof(DBGridColumnStylesCollectionEditor), typeof(UITypeEditor))]
        public new GridColumnStylesCollection GridColumnStyles => base.GridColumnStyles;

        protected override DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
        {
            return base.CreateGridColumn(prop, isDefault);
        }

        #region Nested type: DBGridColumnStylesCollectionEditor

        private class DBGridColumnStylesCollectionEditor : CollectionEditor
        {
            public DBGridColumnStylesCollectionEditor(Type type) : base(type)
            {
            }


            protected override Type[] CreateNewItemTypes()
            {
                return new[]
                {
                    typeof(DataGridDBTextBoxColumn), typeof(DataGridFileColumn),
                    typeof(DataGridComboBoxColumn), typeof(DataGridButtonColumn),
                    typeof(DataGridTextBoxColumn), typeof(DataGridBoolColumn)
                };
            }
        }

        #endregion
    }
}