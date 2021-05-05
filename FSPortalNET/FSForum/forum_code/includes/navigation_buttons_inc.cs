// <fileheader>
// <copyright file="navigation_buttons_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\navigation_buttons_inc.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSPortal;
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
        public class navigation_buttons_inc
        {
            public static string Render()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<br />");
                sb.AppendLine(@"<div align=""left"">");
                sb.AppendLine("<p class='cabepeque'>Foro</p>");
                sb.AppendLine(@"<img src=""../imagenes/bullet.gif"" align=""middle"" /> <a href=""default.aspx"">Lista de temas</a><br /><br />");
                sb.AppendLine(@"<img src=""../imagenes/bullet.gif"" align=""middle"" /> <a href=""active_topics.aspx"">");
                sb.AppendLine(Variables.Forum.strTxtActiveTopics);
                sb.AppendLine("</a><br /><br />");
                sb.AppendLine(@"<img src=""../imagenes/bullet.gif"" align=""middle"" /> <a href=""membedt.aspx"">");
                sb.AppendLine(Variables.Forum.strTxtMemberlist);
                sb.AppendLine("</a><br /><br />");
                sb.AppendLine(@"<img src=""../imagenes/bullet.gif"" align=""middle"" /> <a href=""search_form.aspx?FID=");
                sb.AppendLine(Variables.Forum.intForumID.ToString());
                sb.AppendLine(@""">");
                sb.AppendLine(Variables.Forum.strTxtSearch);
                sb.AppendLine("</a><br /><br />");
                sb.AppendLine("</p></div>");
                // Display the other common buttons
                // sb.Append ("  <a href=""active_topics.aspx"" target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "active_topics.gif"" align=""middle"" border=""0"" alt=""" & Variables.Forum.strTxtActiveTopics & """>" & Variables.Forum.strTxtActiveTopics & "</a>")
                // sb.Append ("  <a href=""membedt.aspx"" target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "members_list.gif"" border=""0"" align=""middle"" alt=""" & Variables.Forum.strTxtMembersList & """>" & Variables.Forum.strTxtMemberlist & "</a>")
                // sb.Append ("  <a href=""search_form.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "search.gif"" align=""middle"" border=""0"" alt=""" & Variables.Forum.strTxtSearchTheForum & """>" & Variables.Forum.strTxtSearch & "</a>")
                // sb.Append ("  <a href=""help.aspx"" target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "help_icon.gif"" align=""middle"" border=""0"" alt=""" & Variables.Forum.strTxtHelp & """>" & Variables.Forum.strTxtHelp & "</a>")
                // sb.Append ("<br />")}
                sb.AppendLine("</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr>");
                sb.AppendLine(@"<td align=""center""><!-- include file=""pm_check_inc.aspx"" -->");
                // If the user has logged in then the Logged In User ID number will not be 0 and not 2 for the guest account
                if (((FSPortal.Variables.User.UsuarioId != 0)
                            && (FSPortal.Variables.User.UsuarioId != 2)))
                {
                    // Dispaly a welcome message to the user in the top bar
                    // sb.Append ("  <a href=""member_control_panel.aspx"" target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "admin_icon.gif"" border=""0"" align=""middle"" alt=""" & Variables.Forum.strTxtMemberCPMenu & """>" & Variables.Forum.strTxtSettings & "</a>")
                    // sb.Append ("  <a href=""log_off_user.aspx"" target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "log_off_icon.gif"" alt=""" & Variables.Forum.strTxtLogOff & """ border=""0"" align=""middle"" />" & Variables.Forum.strTxtLogOff & " [" & strLoggedInusuario & "]</a>")
                    // Else the user is not logged
                }
                else
                {
                    // Display a welcome guset message with the option to login or register
                    // sb.Append ("  <a href=""registration_rules.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "register_icon.gif"" alt=""" & Variables.Forum.strTxtRegister & """ border=""0"" align=""middle"" />" & Variables.Forum.strTxtRegister & "</a>")
                    // sb.Append ("  <a href=""login_user.aspx?FID=" & Variables.Forum.intForumID & """ target=""_self"" class=""nav""><img src=""" & Variables.Forum.strImagePath & "login_icon.gif"" alt=""" & Variables.Forum.strTxtLogin & """ border=""0"" align=""middle"" />" & Variables.Forum.strTxtLogin & "</a>")
                }
                return sb.ToString();
            }

        }

    }
}