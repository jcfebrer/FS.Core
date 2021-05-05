// <fileheader>
// <copyright file="RTE_textbox.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\RTE_textbox.aspx.cs
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
    public class RTE_textbox : FSPortal.BasePage
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
            string strMode;
            // Holds the mode of the page
            string strMessage = "";
            long lngPostID;
            // Holds the post ID number
            string strQuoteusuario;
            // Holds the quoters usuario
            string strQuoteMessage;
            // Holds the message to be quoted
            long lngQuoteUserID;
            // Holds the quoters user ID
            string strSQL;
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable rsCommon;
            // Read in the message ID number to edit
            strMode = Request.QueryString["mode"];
            lngPostID = long.Parse(Request.QueryString["POID"]);
            if (((strMode == "edit")
                        || (strMode == "editTopic")))
            {
                // Initalise the strSQL variable with an SQL statement to query the database get the message details
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID " + ("FROM ("
                            + (Variables.Forum.strDbTable + ("Forum INNER JOIN "
                            + (Variables.Forum.strDbTable + ("Topic ON "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID = "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID) INNER JOIN "
                            + (Variables.Forum.strDbTable + ("Thread ON "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (Variables.Forum.strDbTable + ("Thread.Topic_ID " + ("WHERE "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID="
                            + (lngPostID + ";"))))))))))))))))))))))));
                rsCommon = db.Execute(strSQL);
                // Read in the details from the recordset
                strMessage = rsCommon.Rows[0]["Message"].ToString();
                Variables.Forum.intForumID = int.Parse(rsCommon.Rows[0]["Forum_ID"].ToString());
            }
            else if ((strMode == "quote"))
            {
                // Initialise the sql query to get the thread details to be quoted
                strSQL = ("SELECT " + ("Usuarios.UsuarioID, " + ("Usuarios.usuario, "
                            + (Variables.Forum.strDbTable + ("Thread.Message, "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID " + ("FROM " + ("Usuarios INNER JOIN (("
                            + (Variables.Forum.strDbTable + ("Forum INNER JOIN "
                            + (Variables.Forum.strDbTable + ("Topic ON "
                            + (Variables.Forum.strDbTable + ("Forum.Forum_ID = "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID) INNER JOIN "
                            + (Variables.Forum.strDbTable + ("Thread ON "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID = "
                            + (Variables.Forum.strDbTable + ("Thread.Topic_ID) ON " + ("Usuarios.UsuarioID = "
                            + (Variables.Forum.strDbTable + ("Thread.UsuarioID " + ("WHERE "
                            + (Variables.Forum.strDbTable + ("Thread.Thread_ID = " + lngPostID)))))))))))))))))))))))))))));
                // Query the database
                rsCommon = db.Execute(strSQL);
                // Read in the quoters usuario and message
                strQuoteusuario = rsCommon.Rows[0]["usuario"].ToString();
                strQuoteMessage = rsCommon.Rows[0]["Message"].ToString();
                lngQuoteUserID = long.Parse(rsCommon.Rows[0]["UsuarioID"].ToString());
                Variables.Forum.intForumID = int.Parse(rsCommon.Rows[0]["Forum_ID"].ToString());
                if ((lngQuoteUserID == 2))
                {
                    // Initalise the strSQL variable with an SQL statement to query the database
                    if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                    {
                        strSQL = ("Execute "
                                    + (Variables.Forum.strDbProc + ("GuestPoster @lngThreadID = " + lngPostID)));
                    }
                    else
                    {
                        strSQL = ("SELECT "
                                    + (Variables.Forum.strDbTable + ("GuestName.Name FROM "
                                    + (Variables.Forum.strDbTable + ("GuestName WHERE "
                                    + (Variables.Forum.strDbTable + ("GuestName.Thread_ID = "
                                    + (lngPostID + ";"))))))));
                    }

                    // Query the database
                    rsCommon = db.Execute(strSQL);
                    // Read in the quoters name    
                    if ((rsCommon.Rows.Count > 0))
                    {
                        strQuoteusuario = rsCommon.Rows[0]["Name"].ToString();
                    }

                }

                // Build up the quoted thread post
                strMessage = ("[QUOTE="
                            + (strQuoteusuario + "]"));
                strMessage = (strMessage + strQuoteMessage);
                strMessage = (strMessage + "[/QUOTE]");
            }
            else if (((strMode == "PM")
                        && (Functions.Valor(Session["PmMessage"].ToString()) != "")))
            {
                strMessage = Session["PmMessage"].ToString();
                Session["PmMessage"] = DBNull.Value;
            }

            // If we are replying to a private message then format it
            if (((strMode == "PM")
                        && !(lngPostID == 0)))
            {
                // Initlise the sql statement
                strSQL = ("SELECT "
                            + (Variables.Forum.strDbTable + ("PMMessage.*, " + ("Usuarios.usuario " + ("FROM "
                            + (Variables.Forum.strDbTable + ("PMMessage, " + ("Usuarios " + ("WHERE " + ("Usuarios.UsuarioID = "
                            + (Variables.Forum.strDbTable + ("PMMessage.From_ID AND "
                            + (Variables.Forum.strDbTable + ("PMMessage.PM_ID="
                            + (lngPostID + (" AND "
                            + (Variables.Forum.strDbTable + ("PMMessage.UsuarioID="
                            + (FSPortal.Variables.User.UsuarioId + ";")))))))))))))))))));
                rsCommon = db.Execute(strSQL);
                // Build up the reply pm post
                strMessage = ("<br /><br /><br />-- "
                            + (Variables.Forum.strTxtPreviousPrivateMessage + " --"));
                strMessage = (strMessage + ("<br /><b>"
                            + (Variables.Forum.strTxtSentBy + (" :</b> " + rsCommon.Rows[0]["usuario"].ToString()))));
                strMessage = (strMessage + ("<br /><b>"
                            + (Variables.Forum.strTxtSent + (" :</b> "
                            + (FuncionesFecha.DateFormat(System.DateTime.Parse(rsCommon.Rows[0]["PM_Message_Date"].ToString()), FuncionesFecha.saryDateTimeData) + (" "
                            + (Variables.Forum.strTxtAt + (" "
                            + (FuncionesFecha.TimeFormat(System.DateTime.Parse(rsCommon.Rows[0]["PM_Message_Date"].ToString()), FuncionesFecha.saryDateTimeData) + "<br /><br />")))))))));
                strMessage = (strMessage + rsCommon.Rows[0]["PM_Message"].ToString());
            }

            // Make the post idetical to before it was posted by removing border and target tags from the images and links
            if (!(strMessage == ""))
            {
                strMessage = strMessage.Replace("\" border=\"0\" target=\"_blank\">", "\">");
            }

            if (!(strMessage == ""))
            {
                strMessage = strMessage.Replace("\" border=\"0\">", "\">");
            }

            // If this is an edit or quote then stripout who edited the post and check permisisons
            if (((strMode == "edit")
                        || ((strMode == "editTopic")
                        || (strMode == "quote"))))
            {
                // If the message has been edited remove who edited the post
                if (((strMessage.IndexOf("<edited>") + 1)
                            > 0))
                {
                    strMessage = FuncionesForum.removeEditorUsuarios(strMessage);
                }

                // Read in the forum permissions from the database
                // Initalise the strSQL variable with an SQL statement to query the database
                if ((FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer))
                {
                    strSQL = ("Execute "
                                + (Variables.Forum.strDbProc + ("ForumsAllWhereForumIs @Variables.Forum.intForumID = " + Variables.Forum.intForumID)));
                }
                else
                {
                    strSQL = ("SELECT "
                                + (Variables.Forum.strDbTable + ("Forum.* FROM "
                                + (Variables.Forum.strDbTable + ("Forum WHERE Forum_ID = "
                                + (Variables.Forum.intForumID + ";"))))));
                }

                // Query the database
                rsCommon = db.Execute(strSQL);
                // If there is a record returned by the recordset then check to see if you need a clave to enter it
                if ((rsCommon.Rows.Count > 0))
                {
                    // Check the user is welcome in this forum
                    FuncionesForum.forumPermisisons(Variables.Forum.intForumID, FSPortal.Variables.User.GroupId, int.Parse(rsCommon.Rows[0]["Read"].ToString()), int.Parse(rsCommon.Rows[0]["Post"].ToString()), int.Parse(rsCommon.Rows[0]["Reply_posts"].ToString()), int.Parse(rsCommon.Rows[0]["Edit_posts"].ToString()), 0, 0, 0, 0, 0, 0);
                    // If the forum requires a clave and a logged in forum code is not found on the users machine then set the message to be blank
                    if ((!(rsCommon.Rows[0]["clave"].ToString() == "")
                                && !(Web.Cookie(Request.Cookies[FSPortal.Variables.App.strCookieName], ("Forum" + Variables.Forum.intForumID)) == rsCommon.Rows[0]["Forum_code"].ToString())))
                    {
                        strMessage = "";
                    }

                }

                // If the user dosn't have permisison to view/edit/post/etc. then don't let them read the post
                if (((strMode == "edit")
                            || ((strMode == "editTopic")
                            && ((FSPortal.Variables.User.Administrador == false)
                            && (Variables.Forum.blnModerator == false)))))
                {
                    if (((Variables.Forum.blnRead == false)
                                || ((Variables.Forum.blnPost == false)
                                || (Variables.Forum.blnEdit == false))))
                    {
                        strMessage = "";
                    }
                    else if ((strMode == "quote"))
                    {
                        if (((Variables.Forum.blnRead == false)
                                    || ((Variables.Forum.blnPost == false)
                                    || (Variables.Forum.blnReply == false))))
                        {
                            strMessage = "";
                        }

                    }

                }

            }
            //sb.AppendLine("<html>");
            //sb.AppendLine("<head>");
            if ((FuncionesForum.RTEenabled() == "Gecko"))
            {
                sb.AppendLine(("\r\n" + ("<script language=\"javascript\">" + ("\r\n" + ("\t<!--" + ("\r\n" + ("\tfunction enableDesignMode() {" + ("\r\n" + ("\tdocument.designMode = \"on\"" + ("\r\n" + ("\t}" + ("\r\n" + ("-->" + ("\r\n" + "</script>"))))))))))))));
            }
            //sb.AppendLine("</head>");
            sb.AppendLine(@"<body bgcolor=""");
            sb.AppendLine(Variables.Forum.strIETextBoxColour);
            sb.AppendLine(@""" class=""text"" leftmargin=""0"" topmargin=""0"" marginwidth=""0"" marginheight=""0""");
            if ((FuncionesForum.RTEenabled() == "Gecko"))
            {
                sb.AppendLine(" onLoad=\"setTimeout(\'enableDesignMode()\', 20);\"");
            }
            sb.AppendLine(">");
            sb.AppendLine(strMessage);
            sb.AppendLine("</body>");
            //sb.AppendLine("</html>");
            return sb.ToString();
        }

    }
}