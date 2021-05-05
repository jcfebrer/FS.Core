using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System;
// End of VB project level imports

using Html2CSharp;

namespace Html2CSharp
{
	class StartupModule
	{
        [STAThread]
        public static void Main(string[] CmdArgs)
		{
            if (CmdArgs.Length != 2)
            {
                Application.Run(new frmMain());
            }
            else
            {
                try
                {

                    System.IO.StreamReader sr = default(System.IO.StreamReader);
                    sr = System.IO.File.OpenText(CmdArgs[0]);

                    string s1 = sr.ReadToEnd();
                    s1 = ConvertToCS(s1);

                    System.IO.File.WriteAllText(CmdArgs[1], s1);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
		}
		
		public static string ConvertToCS(string textHtml)
		{
			
			int pos = 1;
			int endp = 1;
			int endp2 = 1;
			bool hayCode = false;
			string p = "";
			string t = "";
			
			p = "public string Inicio() {" + "\r\n";
			p = p + "StringBuilder sb = new StringBuilder();" + "\r\n";
			
			textHtml = textHtml.Replace("<%", "\r\n" + "<%");
			textHtml = textHtml.Replace("%>", "%>" + "\r\n");
			
			textHtml = textHtml.Replace("<% =", "<%=");

            textHtml = FSLibrary.TextUtil.Replace(textHtml, "response.write", "sb.AppendLine");

            //quitamos los comandos <%@ .... %>
            textHtml = FSLibrary.TextUtil.ReplaceREG(textHtml, @"<%@[^>]*%>", String.Empty);
			
			while (endp != 0)
			{
				hayCode = false;
				endp = textHtml.IndexOf(('\r').ToString(), pos - 1) + 1;
				endp2 = textHtml.IndexOf("<%", pos - 1) + 1;
				
				if ((endp2 < endp) && endp2 != 0)
				{
					endp = textHtml.IndexOf("%>", pos - 1) + 1;
					hayCode = true;
					endp++;
				}
				
				if (endp != 0)
				{
					t = textHtml.Substring(pos - 1, endp - pos + 1);
				}
				else
				{
					t = textHtml.Substring(pos);
				}
				
				if (hayCode)
				{
					t = t.Replace("<%", "");
					t = t.Replace("%>", "");
					t = t.Trim();
					if (!string.IsNullOrEmpty(t))
					{
                        if(t.StartsWith("="))
                            t = "sb.AppendLine(" + t.Substring(1,t.Length-1) + ");\r\n";
                        else
						    t = t + "\r\n";
					}
				}
				else
				{
					t = t.Replace("\r", "");
					t = t.Replace("\n", "");
					t = t.Replace(@"""", @"""""");
					t = t.Trim();
					if (!string.IsNullOrEmpty(t))
					{
                        string arroba = "";
                        if (t.IndexOf(@"""") >= 0) arroba = "@";
						t = "sb.AppendLine(" + arroba + @"""" + t.Trim() + @""");" + "\r\n";
					}
				}
				
				pos = endp + 1;
				
				if (!string.IsNullOrEmpty(t))
				{
					p = p + t;
				}
			}
			
			p = p + "return sb.ToString();" + "\r\n";
			p = p + "}" + "\r\n";
			
			return p;
			
			
		}
		
		
	}
	
}
