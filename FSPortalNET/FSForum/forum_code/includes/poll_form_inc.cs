// <fileheader>
// <copyright file="poll_form_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\poll_form_inc.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    namespace Includes
    {
        public class poll_form_inc
        {
            public string Render()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<tr>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" height=""22"" class=""text"">");
                sb.AppendLine(Variables.Forum.strTxtPollQuestion);
                sb.AppendLine("*:</td>");
                sb.AppendLine(@"<td height=""22""><input name=""pollQuestion"" type='text' size=""30"" maxlength=""70"" /></td>");
                sb.AppendLine("</tr>");
                // Loop around to display text boxes for the maximum amount of allowed poll questions
                int intPollLoopCounter;
                for (intPollLoopCounter = 1; (intPollLoopCounter <= Variables.Forum.intMaxPollChoices); intPollLoopCounter++)
                {
                    sb.AppendLine(("\r\n" + ("        <tr>" + ("\r\n" + "\t <td align=\"right\" height=\"22\" class=\"text\">"))));
                    // Display the poll choice text
                    sb.AppendLine((Variables.Forum.strTxtPollChoice + " "));
                    // Display poll number
                    sb.AppendLine(intPollLoopCounter.ToString());
                    // If this is choice 1 or 2 display a required astrerix
                    if ((intPollLoopCounter < 3))
                    {
                        sb.AppendLine("*");
                    }

                    sb.AppendLine((":</td>" + ("\r\n" + ("\t<td height=\"22\"><input name=\"choice"
                                    + (intPollLoopCounter + ("\" type=\"text\" size=\"30\" maxlength=\"60\" /></td>" + ("\r\n" + "        </tr>")))))));
                }
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" height=""22"" class=""text""> </td>");
                sb.AppendLine(@"<td height=""22"" class=""text""> <input type='checkbox' name=""multiVote"" value=""True"" />");
                sb.AppendLine(Variables.Forum.strTxtAllowMultipleVotes);
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""right"" height=""22"" class=""text""> </td>");
                sb.AppendLine(@"<td height=""22"" class=""text""> <input type='checkbox' name=""pollReply"" value=""True"" />");
                sb.AppendLine(Variables.Forum.strTxtMakePollOnlyNoReplies);
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("<td> </td>");
                sb.AppendLine("</tr>");
                return sb.ToString();
            }

        }

    }
}