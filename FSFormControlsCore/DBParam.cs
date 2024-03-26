#region

using System.ComponentModel;

#endregion

namespace FSFormControlsCore
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class DBParam : Component
    {
        public DBParam()
        {
        }

        public DBParam(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Nombre del parámetro
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        ///     Valor del parámetro
        /// </summary>
        public object Value { get; set; }

    }
}