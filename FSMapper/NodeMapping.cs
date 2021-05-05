/*
 * Created by SharpDevelop.
 * User: febrer
 * Date: 06/06/2017
 * Time: 10:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;

namespace XML2XML
{
	/// <summary>
	/// Description of NodeMapping.
	/// </summary>
	[Serializable]
	public class NodeMapping
	{
		public NodeMapping()
		{
		}
		
		public NodeMapping(string key, string value)
		{
			_Key = key;
			_Value = value;
		}
		
		private string _Key;
		[Browsable(true)]
		[ReadOnly(true)]
		[Description("Clave destino de mapeo")]
		[Category("Datos de mapeo")]
		[DisplayName("Key")]
		public string Key {
			get { return _Key; }
			set { _Key = value; }
		}
		
		private string _Value;
		[Browsable(true)]
		[ReadOnly(false)]
		[Description("Valor origen de mapeo")]
		[Category("Datos de mapeo")]
		[DisplayName("Value")]
		public string Value {
			get { return _Value; }
			set { _Value = value; }
		}
		
		private string _Formula;
		[Browsable(true)]
		[ReadOnly(false)]
		[Description("Formula a aplicar en el mapeo")]
		[Category("Datos de mapeo")]
		[DisplayName("Formula")]
		public string Formula {
			get { return _Formula; }
			set { _Formula = value; }
		}
		
		private bool _Loop;
		[Browsable(true)]
		[ReadOnly(false)]
		[Description("Activar si el campo a mapear es múltiple")]
		[Category("Datos de mapeo")]
		[DisplayName("Loop")]
		public bool Loop {
			get { return _Loop; }
			set { _Loop = value; }
		}
	}
}
