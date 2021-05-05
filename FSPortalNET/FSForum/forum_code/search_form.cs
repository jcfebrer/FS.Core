// <fileheader>
// <copyright file="search_form.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\search_form.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSForum.Includes;
using FSNetwork;
using FSLibrary;
using FSPortal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    public class search_form : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            contenido = Inicio();
        }
        public string Inicio()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(@"<common:common ID=""common"" runat=""server"" />");
            common.Inicio();
            // Set the response buffer to true as we maybe redirecting
            // Response.Buffer = True
            // Dimension variables
            string strForumName;
            // Holds the forum name
            int intReadPermission;
            // holds the forums read permisisons
            int intOrderBy;
            // Holds the order by cluase
            string strSearchKeywords;
            // Holds the keywords to search for
            string strSearchIn;
            // Holds where the serach is to be done
            string strSearchMode;
            // Holds the search mode
            int intForumID2;
            // Read in values passed to this form
            intOrderBy = int.Parse(Request.QueryString["OB"]);
            strSearchKeywords = Request.QueryString["KW"].Substring(0, 35).Trim();
            strSearchIn = Request.QueryString["SI"].Substring(0, 3).Trim();
            intForumID2 = int.Parse(Request.QueryString["FM"]);
            strSearchMode = Request.QueryString["SM"].Substring(0, 3).Trim();
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            //sb.AppendLine("<title>");
            tituloPagina = Variables.Forum.strMainForumName + " Search";
            sb.AppendLine("<!-- Check the from is filled in correctly before submitting -->");
            sb.AppendLine(@"<script  language=""javascript"">");
            sb.AppendLine("<!-- Hide from older browsedt...");
            sb.AppendLine("//Function to check form is filled in correctly before submitting");
            sb.AppendLine("function CheckForm () {");
            sb.AppendLine("//Check for a somthing to search for");
            sb.AppendLine(@"if (document.frmSearch.KW.value==""""){");
            sb.AppendLine(@"msg = """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine);
            sb.AppendLine(@"\n\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine1);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine2);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine);
            sb.AppendLine(@"\n\n"";");
            sb.AppendLine(@"msg += """);
            sb.AppendLine(Variables.Forum.strTxtErrorDisplayLine3);
            sb.AppendLine(@"\n"";");
            sb.AppendLine(@"alert(msg + ""\n\t");
            sb.AppendLine(Variables.Forum.strTxtSearchFormError);
            sb.AppendLine(@"\n\n"");");
            sb.AppendLine("document.frmSearch.KW.focus();");
            sb.AppendLine("return false;");
            sb.AppendLine("}");
            if (FuncionesForum.RTEenabled() != "false")
                sb.AppendLine("\n" + "	frmSearch.Submit.disabled=true;");
            sb.AppendLine("return true;");
            sb.AppendLine("}");
            sb.AppendLine("// -->");
            sb.AppendLine("</script>");
            sb.AppendLine(@"<navigation:navigation ID=""common1"" runat=""server"" />");
            sb.AppendLine(@"<!--<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""3"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" class=""heading"">");
            sb.AppendLine(Variables.Forum.strTxtSearchTheForum);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" width=""71%"" class=""bold""><img src=""");
            sb.AppendLine(Variables.Forum.strImagePath);
            sb.AppendLine(@"open_folder_icon.gif"" border=""0"" align=""middle""> <a href=""default.aspx"" target=""_self"" class=""boldLink"">");
            sb.AppendLine(Variables.Forum.strMainForumName);
            sb.AppendLine("</a>");
            sb.AppendLine(Variables.Forum.strTxtSearchTheForum);
            sb.AppendLine("<br /></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table> <br />-->");
            sb.AppendLine(@"<form method=""get"" name=""frmSearch"" action=""search.aspx"" onSubmit=""return CheckForm();"" onReset=""return confirm('");
            sb.AppendLine(Variables.Forum.strResetFormConfirm);
            sb.AppendLine(@"');"">");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"" align=""center"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBorderColour);
            sb.AppendLine(@""" height=""8"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td height=""2"" width=""100%""><table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@"""><table width=""100%"" border=""0"" align=""center""  height=""8"" cellpadding=""4"" cellspacing=""1"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" width=""57%"" height=""2""  bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtSearchFor);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td height=""2"" width=""43%"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtSearchIn);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine(@"<tr bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableColour);
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(@"<td align=""left"" width=""57%""  height=""2"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableColour);
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@"""> <input type='text' name=""KW"" maxlength=""35"" value=""");
            sb.AppendLine(Server.HtmlEncode(strSearchKeywords));
            sb.AppendLine(@""">");
            sb.AppendLine(@"<br /> <span class=""text"">");
            sb.AppendLine(Variables.Forum.strTxtSearchOn);
            sb.AppendLine(":");
            sb.AppendLine(@"<input type=""radio"" name=""SM"" value=""1""");
            if (((strSearchMode == "1")
                                || (strSearchMode == "")))
            {
                sb.AppendLine("CHECKED");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtAllWords);
            sb.AppendLine(@"<input type=""radio"" name=""SM"" value=""2""");
            if ((strSearchMode == "2"))
            {
                sb.AppendLine("CHECKED");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtAnyWords);
            sb.AppendLine(@"<input type=""radio"" name=""SM"" value=""3""");
            if ((strSearchMode == "3"))
            {
                sb.AppendLine("CHECKED");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtPhrase);
            sb.AppendLine("</span></td>");
            sb.AppendLine(@"<td height=""2"" width=""43%"" valign=""top"" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@"""> <select name=""SI"">");
            sb.AppendLine(@"<option value=""PT""");
            if (((strSearchIn == "PT")
                                || (strSearchIn == "")))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtMessageBody);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""TC""");
            if ((strSearchIn == "TC"))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtTopicSubject);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""AR""");
            if ((strSearchIn == "AR"))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtusuario);
            sb.AppendLine("</option>");
            sb.AppendLine("</select> </td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br />");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"" align=""center"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBorderColour);
            sb.AppendLine(@""" height=""8"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td height=""24"" width=""100%""><table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@""">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableBgColour);
            sb.AppendLine(@"""><table width=""100%"" border=""0"" align=""center""  height=""8"" cellpadding=""4"" cellspacing=""1"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""left"" width=""57%"" height=""2""  bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtSearchForum);
            sb.AppendLine("</td>");
            sb.AppendLine(@"<td height=""2"" width=""43%"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableTitleColour);
            sb.AppendLine(@""" class=""tHeading"" background=""");
            sb.AppendLine(Variables.Forum.strTableTitleBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(Variables.Forum.strTxtSortResultsBy);
            sb.AppendLine("</td>");
            sb.AppendLine("</tr>");
            sb.AppendLine(@"<tr bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableColour);
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@""">");
            sb.AppendLine(@"<td align=""left"" width=""57%""  height=""12"" bgcolor=""");
            sb.AppendLine(Variables.Forum.strTableColour);
            sb.AppendLine(@""" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@"""> <select name=""FM"">");
            sb.AppendLine(@"<option value=""0"">");
            sb.AppendLine(Variables.Forum.strTxtAllForums);
            sb.AppendLine("</option>");
            // Read in the forum name from the database
            // Initalise the strSQL variable with an SQL statement to query the database
            string strSQL = ("SELECT "
                        + (Variables.Forum.strDbTable + ("Forum.* FROM "
                        + (Variables.Forum.strDbTable + ("Forum ORDER BY "
                        + (Variables.Forum.strDbTable + ("Forum.Cat_ID ASC, "
                        + (Variables.Forum.strDbTable + "Forum.Forum_Order ASC;"))))))));
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable rsCommon = db.Execute(strSQL);
            // Loop through all the froum in the database
            foreach (DataRow row in rsCommon.Rows)
            {
                // Read in the forum details from the recordset
                strForumName = row["Forum_name"].ToString();
                Variables.Forum.intForumID = int.Parse(row["Forum_ID"].ToString());
                intReadPermission = int.Parse(row["Read"].ToString());
                FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, intReadPermission, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                // Display the fourms to search as long as they are not private or they have logged in if they are and they have permission to use forum
                if ((((Functions.Valor(row["clave"]) == "")
                            || (Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) == row["Forum_code"].ToString()))
                            && ((Variables.Forum.blnRead == true)
                            || ((FSPortal.Variables.User.Administrador == true)
                            || (Variables.Forum.blnModerator == true)))))
                {
                    // Display a link in the link list to the forum
                    sb.AppendLine(("\r\n" + ("<option value="
                                    + (Variables.Forum.intForumID + " "))));
                    if (((int.Parse(Request.QueryString["FID"]) == Variables.Forum.intForumID)
                                || (int.Parse(Request.QueryString["forum"]) == Variables.Forum.intForumID)))
                    {
                        sb.AppendLine("selected");
                    }

                    sb.AppendLine((">"
                                    + (strForumName + "</option>")));
                }

            }
            sb.AppendLine("</select> </td>");
            sb.AppendLine(@"<td height=""12"" width=""43%"" valign=""top"" background=""");
            sb.AppendLine(Variables.Forum.strTableBgImage);
            sb.AppendLine(@"""> <select name=""OB"">");
            sb.AppendLine(@"<option value=""1""");
            if (((intOrderBy == 1) || (intOrderBy == 0)))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtLastPostTime);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""2""");
            if ((intOrderBy == 2))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtTopicStartDate);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""3""");
            if ((intOrderBy == 3))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtSubjectAlphabetically);
            sb.AppendLine("</option>");
            sb.AppendLine(@"<option value=""4""");
            if ((intOrderBy == 4))
            {
                sb.AppendLine("selected");
            }
            sb.AppendLine(">");
            sb.AppendLine(Variables.Forum.strTxtNumberViews);
            sb.AppendLine("</option></select> </td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("<br /><script>document.frmSearch.KW.focus()</script>");
            sb.AppendLine(@"<table width=""");
            sb.AppendLine(Variables.Forum.strTableVariableWidth);
            sb.AppendLine(@""" border=""0"" cellspacing=""0"" cellpadding=""1"" align=""center"">");
            sb.AppendLine("<tr>");
            sb.AppendLine(@"<td align=""center""> <input type='submit' name=""Submit"" value=""");
            sb.AppendLine(Variables.Forum.strTxtStartSearch);
            sb.AppendLine(@"""> <input type=""reset"" name=""Reset"" value=""");
            sb.AppendLine(Variables.Forum.strTxtResetForm);
            sb.AppendLine(@"""> </td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");
            sb.AppendLine("</form>");
            sb.AppendLine(@"<div align=""center"">");
            // Display the process time
            // If Variables.Forum.blnShowProcessTime Then sb.AppendLine("<span class=""smText""><br /><br />" & Variables.Forum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - Variables.Forum.dblStartTime, 4) & " " & Variables.Forum.strTxtSeconds & "</span>")}
            sb.AppendLine("</div>");
            return sb.ToString();
        }

    }
}