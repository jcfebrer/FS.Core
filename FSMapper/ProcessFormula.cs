/*
 * Created by SharpDevelop.
 * User: febrer
 * Date: 07/06/2017
 * Time: 16:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace XML2XML
{
	/// <summary>
	/// Description of ProcessFormula.
	/// </summary>
	public static class ProcessFormula
	{
		public static string Start(NodeMapping node)
		{
			string formula = node.Formula;
			Regex reg = new Regex(@"\{(?<data>[^\{\}]*)\}", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Multiline);
						
			MatchCollection matches = reg.Matches(formula);
			foreach (Match match in matches) {
				string value = match.Groups["data"].Value;
				
				Regex regCmd = new Regex(@"(?<command>[a-zA-Z]*)\((?<cont>[^\(\)]*)\)", RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.Multiline);
				MatchCollection matchesCmd = regCmd.Matches(value);
				foreach (Match matchCmd in matchesCmd) {
					string command = matchCmd.Groups["command"].Value;
					string cont = matchCmd.Groups["cont"].Value;
					
					if(cont.IndexOf('#')>-1)
					{
						XmlNode findNode = XML2XMLHelper.FindXMLNode(XML2XMLHelper.SourceXML, node.Value);
						if(findNode != null)
							cont = cont.Replace("#", findNode.InnerText);
					}
					
					switch (command.ToLower()) {
						case "date":
							formula = reg.Replace(formula, Convert.ToDateTime(cont).ToShortDateString(), 1);
							break;
						case "mid":
							formula = reg.Replace(formula, Mid(cont), 1);
							break;
						default:
							throw new Exception("Comando incorrecto: " + command);
					}
				}
				
				switch (value.ToLower()) {
					case "#":
						value = value.Replace("\\", "/");
						formula = reg.Replace(formula, @"<" + XML2XMLHelper.xslPrefix + @":value-of select=""" + node.Value + @""" />", 1);
						break;
					case "date":
						formula = reg.Replace(formula, DateTime.Now.ToShortDateString(), 1);
						break;
					case "time":
						formula = reg.Replace(formula, DateTime.Now.ToShortTimeString(), 1);
						break;
					default:
						value = value.Replace("\\", "/");
						formula = reg.Replace(formula, @"<" + XML2XMLHelper.xslPrefix + @":value-of select=""" + value + @""" />", 1);
						break;
				}
			}
			return formula;
		}
		
		private static string Mid(string cont)
		{
			string[] param = cont.Split(',');
			
			if(param.Length != 3)
				throw new Exception("Parametros MID incorrectos. MID(#,1,2)");
			
			if(param[0].Length<Convert.ToInt32(param[2]))
			   throw new Exception("La longitud de la cadena es inferior a los caracteres a recuperar. MID Error.");
			
			return param[0].Substring(Convert.ToInt32(param[1]), Convert.ToInt32(param[2]));
		}
		
	}
}
