#region

using FSFormControls;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class DBColumn : Component
    {
        public delegate void CellClickEventHandler(object sender, DataGridViewCellEventArgs e);

        public enum ColumnTypes
        {
            CheckColumn,
            TextColumn,
            MaskedColumn,
            DateColumn,
            ComboColumn,
            ButtonColumn,
            Button2Column,
            MoneyColumn,
            NumberColumn,
            DescriptionColumn,
            FormulaColumn,
            PercentColumn,
            ProgressColumn,
            TimeColumn,
            FileColumn,
            TimePickerColumn,
            AutoNumericColumn,
            PictureColumn
        }


        public enum DescriptionTypes
        {
            TextDescription,
            NumberDescription,
            MoneyDescription,
            DateDescription,
            CheckDescription
        }

        public enum LogicalOperatorEnum
        {
            Or,
            And
        }

        public enum OperationTypes
        {
            Sum,
            Max,
            Min,
            Average
        }

        public enum SortIndicatorEnum
        {
            Ascending,
            Descending
        }

        public DBColumn()
        {
        }

        public DBColumn(string strFieldDB, string strHeaderCaption)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, ColumnTypes tColumnType)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            ColumnType = tColumnType;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, DBControl dbcColumnDBControl)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            ColumnDBControl = dbcColumnDBControl;
        }

        public DBColumn(string strFieldDB, string strHeaderCaption, bool bolHidden)
        {
            FieldDB = strFieldDB;
            HeaderCaption = strHeaderCaption;
            Hidden = bolHidden;
        }

        /// <summary>
        ///     Valor máximo
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public decimal MaxValue { get; set; } = decimal.MaxValue;

        /// <summary>
        ///     Valor mínimo
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public decimal MinValue { get; set; } = decimal.MinValue;

        /// <summary>
        ///     Permitir líneas múltiples
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Multiline { get; set; } = false;

        /// <summary>
        ///     Permitir valores nulos
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowNull { get; set; }

        /// <summary>
        ///     Valor a mostrar cuando el contenido sea Null
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object NullValue { get; set; }

        /// <summary>
        /// Permiter cambiar el orden de las columnas
        /// </summary>
        //public int DisplayIndex { get; set; }

        /// <summary>
        ///     Tipo de letra
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Font Font { get; set; }

        /// <summary>
        ///     Carácter prompt
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public char PromptChar { get; set; }

        /// <summary>
        ///     Lista de imágenes para un combo
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ImageList ComboImageList { get; set; }

        /// <summary>
        ///     Columna encriptada
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Encrypted { get; set; }

        /// <summary>
        ///     Permitir la selección de un valor en blanco (Combo)
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ComboBlankSelection { get; set; } = true;

        /// <summary>
        ///     Color trasero de la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColumnBackColor { get; set; } = Color.Empty;

        /// <summary>
        ///     Color de las letrar en la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ColumnForeColor { get; set; } = Color.Empty;

        /// <summary>
        ///     Nombre del campo para asociar al DataTable
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string FieldDB { get; set; } = "";

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ColumnDBFieldData { get; set; } = "";

        /// <summary>
        ///     Título de la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string HeaderCaption { get; set; } = "";

        /// <summary>
        ///     DBControl asociado a la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DBControl ColumnDBControl { get; set; }

        /// <summary>
        ///     Tipo de columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ColumnTypes ColumnType { get; set; } = ColumnTypes.TextColumn;

        /// <summary>
        ///     Campo que se mostrará en el combo
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ComboListField { get; set; } = "";

        /// <summary>
        ///     Valor por defecto
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DefaultValue { get; set; } = "";

        /// <summary>
        ///     Columna asociada al botón
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int AsociatedButtonColumn { get; set; } = -1;

        /// <summary>
        ///     Columna asocidada al combo
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int AsociatedComboColumn { get; set; } = -1;

        /// <summary>
        ///     Columna de solo lectura (si/no)
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ReadColumn { get; set; }

        /// <summary>
        ///     Mostrar un formularío de selección
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowSelectForm { get; set; } = true;

        /// <summary>
        ///     Columna oculta
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Hidden { get; set; }

        /// <summary>
        ///     Longitud máxima
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxLength { get; set; }

        /// <summary>
        ///     Columna con valores únicos
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Unique { get; set; }

        /// <summary>
        ///     Columna obligatoria
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Obligatory { get; set; }

        /// <summary>
        ///     Ancho de la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Width { get; set; }

        /// <summary>
        ///     Número de decimales
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Decimals { get; set; } = 0;

        /// <summary>
        ///     Alineación de los datos
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;

        /// <summary>
        ///     Expresión o fórmula
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Expression { get; set; } = "";

        /// <summary>
        ///     Cadena de formato
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string FormatString { get; set; }

        /// <summary>
        ///     Formato
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Format { get; set; }

        /// <summary>
        ///     Tipo de descripción para la columna asociada
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DescriptionTypes DescriptionType { get; set; } = DescriptionTypes.TextDescription;

        /// <summary>
        ///     Máscara de entrada
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string MaskInput { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ActiveColumnDBButtonOnReadMode { get; set; } = true;

        /// <summary>
        ///     Último valor de la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool LastValue { get; set; }

        /// <summary>
        ///     Mostrar un tooltip en la columna
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ToolTip { get; set; } = "";

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowRowFiltering { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SortIndicatorEnum SortIndicator { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public LogicalOperatorEnum LogicalOperator { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DBGridViewFilterCollection DBGridViewFilters { get; set; }

        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Index { get; set; }

        public event CellClickEventHandler CellClick;

        public void PerformClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CellClick != null)
                CellClick(sender, e);
        }
    }
}