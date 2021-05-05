// <fileheader>
// <copyright file="forum_jump_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\forum_jump_inc.ascx.cs
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
        public class forum_jump_inc
        {
            public static string Render()
            {
                // Declare variables
                StringBuilder sb = new StringBuilder();
                System.Data.DataTable rsForumJumpForum;      // Holds the recordset for the forum
                string strJumpCatName;        // Holds the name of the category
                int intJumpCatID;     // Holds the ID number of the category
                string strJumpForumName;      // Holds the name of the forum to jump to
                long lngJumpFID;          // Holds the forum id to jump to
                int intReadPermission;        // Holds the generic read permssions level on them forum

                // These are used as tmep store for forum permissions as this will change them
                bool blnTempRead;
                bool blnTempPost;
                bool blnTempReply;
                bool blnTempEdit;
                bool blnTempDelete;
                bool blnTempPriority;
                bool blnTempPollCreate;
                bool blnTempVote;
                bool blnTempAttachments;
                bool blnTempImageUpload;


                // Put the forum permissions into the temp store while looping through the other forums
                blnTempRead = Variables.Forum.blnRead;
                blnTempPost = Variables.Forum.blnPost;
                blnTempReply = Variables.Forum.blnReply;
                blnTempEdit = Variables.Forum.blnEdit;
                blnTempDelete = Variables.Forum.blnDelete;
                blnTempPriority = Variables.Forum.blnPriority;
                blnTempPollCreate = Variables.Forum.blnPollCreate;
                blnTempVote = Variables.Forum.blnVote;
                blnTempAttachments = Variables.Forum.blnAttachments;
                blnTempImageUpload = Variables.Forum.blnImageUpload;

                sb.AppendLine(functions_js.Render());

                sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "	<span class=\"text\">" + Variables.Forum.strTxtForumJump + "</span>");
                sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "	 <select onChange=\"ForumJump(this)\" name=\"SelectJumpForum\">");
                sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "           <option value=\"\" selected>-- " + Variables.Forum.strTxtSelectForum + " --</option>");


                // Read in the category name from the database
                // Initalise the strSQL variable with an SQL statement to query the database
                string strSQL;
                FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");

                if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                    strSQL = "Execute " + Variables.Forum.strDbProc + "CategoryAll";
                else
                    strSQL = "SELECT " + Variables.Forum.strDbTable + "Category.Cat_name, " + Variables.Forum.strDbTable + "Category.Cat_ID FROM " + Variables.Forum.strDbTable + "Category ORDER BY " + Variables.Forum.strDbTable + "Category.Cat_order ASC;";

                // Query the database
                System.Data.DataTable rsCommon;
                rsCommon = db.Execute(strSQL);

                // Loop through all the categories in the database
                foreach (System.Data.DataRow row in rsCommon.Rows)
                {

                    // Read in the deatils for the category
                    strJumpCatName = row["Cat_name"].ToString();
                    intJumpCatID = System.Convert.ToInt32(row["Cat_ID"]);

                    // Display a link in the link list to the forum
                    sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "		<option value=\"\">" + strJumpCatName + "</option>");

                    // Read in the forum name from the database
                    // Initalise the strSQL variable with an SQL statement to query the database
                    if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                        strSQL = "Execute " + Variables.Forum.strDbProc + "ForumsAllWhereCatIs @intCatID = " + intJumpCatID;
                    else
                        strSQL = "SELECT " + Variables.Forum.strDbTable + "Forum.* FROM " + Variables.Forum.strDbTable + "Forum WHERE " + Variables.Forum.strDbTable + "Forum.Cat_ID = " + intJumpCatID + " ORDER BY " + Variables.Forum.strDbTable + "Forum.Forum_Order ASC;";

                    // Query the database
                    rsForumJumpForum = db.Execute(strSQL);

                    // Loop through all the froum in the database
                    foreach (System.Data.DataRow rowJF in rsForumJumpForum.Rows)
                    {

                        // Read in the forum details from the recordset
                        strJumpForumName = rowJF["Forum_name"].ToString();
                        lngJumpFID = System.Convert.ToInt64(rowJF["Forum_ID"]);
                        intReadPermission = System.Convert.ToInt32(rowJF["Read"]);

                        // Call the function to check the users security level within this forum
                        FuncionesForum.forumPermisisons(System.Convert.ToInt32(lngJumpFID), FSPortal.Variables.User.GroupId, intReadPermission, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                        // If the user can view the forum then display it in the forum jump box
                        if (Variables.Forum.blnRead | FSPortal.Variables.User.Administrador | Variables.Forum.blnModerator)
                            // Display a link in the link list to the forum
                            sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "		<option value=\"forum_topics.aspx?FID=" + lngJumpFID + "\">&nbsp;&nbsp;-&nbsp;" + strJumpForumName + "</option>");
                    }
                }

                // Put the forum permissions back
                Variables.Forum.blnRead = blnTempRead;
                Variables.Forum.blnPost = blnTempPost;
                Variables.Forum.blnReply = blnTempReply;
                Variables.Forum.blnEdit = blnTempEdit;
                Variables.Forum.blnDelete = blnTempDelete;
                Variables.Forum.blnPriority = blnTempPriority;
                Variables.Forum.blnPollCreate = blnTempPollCreate;
                Variables.Forum.blnVote = blnTempVote;
                Variables.Forum.blnAttachments = blnTempAttachments;
                Variables.Forum.blnImageUpload = blnTempImageUpload;

                sb.AppendLine(FSLibrary.TextUtil.ControlChars.CrLf + "	</select>");

                return sb.ToString();
            }
        }
    }
}
