using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSForum.Includes
{
    class functions_js
    {
        public static string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"<script language=""javascript"">");

            sb.AppendLine("<!--Hide from older browsedt...");

            sb.AppendLine("//Function to choose how many topics are show");
            sb.AppendLine("function ShowTopicsAT(Show)");
            sb.AppendLine("{");

            sb.AppendLine("strShow = escape(Show.options[Show.selectedIndex].value);");
            sb.AppendLine(@"document.cookie=""AT="" + strShow;");

            sb.AppendLine(@"if (Show != """") self.location.href=""active_topics.aspx?PN=1"";");
            sb.AppendLine("return true;");
            sb.AppendLine("}");

            sb.AppendLine("function ForumJump(URL)");
            sb.AppendLine("{");
            sb.AppendLine(@"if (URL.options[URL.selectedIndex].value!="""") self.location.href = URL.options[URL.selectedIndex].value;");
            sb.AppendLine("return true;");
            sb.AppendLine("}");


            sb.AppendLine("//Function to choose how many topics are show");
            sb.AppendLine("function ShowTopicsFT(Show){");
            sb.AppendLine("strShow = escape(Show.options[Show.selectedIndex].value);");
            sb.AppendLine(@"document.cookie=""TS="" + strShow;");
            sb.AppendLine(@"if (Show != """") self.location.href = ""forum_topics.aspx?FID=" + Variables.Forum.intForumID.ToString() + @"&PN=1"";");
            sb.AppendLine("return true;");
            sb.AppendLine("}");
            sb.AppendLine("// -->");
            sb.AppendLine("</script>");

            return sb.ToString();
        }
    }
}
