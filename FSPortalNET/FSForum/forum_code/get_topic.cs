// <fileheader>
// <copyright file="get_topic.aspx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\get_topic.aspx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using FSForum.Includes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum
{
    public class get_topic : FSPortal.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        void Inicio()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine(@"<common:common ID=""common"" runat=""server"" />");
            common.Inicio();
            // Set the response buffer to true as we maybe redirecting
            // Response.Buffer = True
            // Dimension variables
            long lngTopicID;
            // Holds the topic ID
            string strDirection;
            // Holds the direction to move in
            long lngNewTopicID = 0;
            // Holds the new Topic ID
            DataTable rsCommon;
            string strSQL;
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            // Read in the topic ID and directorion
            Variables.Forum.intForumID = int.Parse(Request.QueryString["FID"]);
            lngTopicID = long.Parse(Request.QueryString["TID"]);
            strDirection = Request.QueryString["DIR"];
            // ******************************************
            // ***         Get the next topic          ****
            // ******************************************
            // If the direction is next get the next topic
            if ((strDirection == "N"))
            {
                // Initliase the SQL query to get the topic and forumID from the database
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + "Topic.Topic_ID "));
                strSQL = (strSQL + ("FROM "
                            + (Variables.Forum.strDbTable + "Topic ")));
                strSQL = (strSQL + ("WHERE "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                            + (Variables.Forum.intForumID + (" AND "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID > "
                            + (lngTopicID + " ")))))))));
                strSQL = (strSQL + ("ORDER BY "
                            + (Variables.Forum.strDbTable + "Topic.Topic_ID ASC;")));
                rsCommon = db.Execute(strSQL);
                // If there is a record returned read in the topic ID to move to
                if ((rsCommon.Rows.Count > 0))
                {
                    lngNewTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                }
                else
                {
                    // Create a new query to get the first topic in the forum
                    strSQL = ("SELECT TOP 1 "
                                + (Variables.Forum.strDbTable + "Topic.Topic_ID "));
                    strSQL = (strSQL + ("FROM "
                                + (Variables.Forum.strDbTable + "Topic ")));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                                + (Variables.Forum.intForumID + " ")))));
                    strSQL = (strSQL + ("ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.Topic_ID ASC;")));
                    rsCommon = db.Execute(strSQL);
                    // Get the topic ID of the first topic in the database
                    if ((rsCommon.Rows.Count > 0))
                    {
                        lngNewTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                    }

                }

            }

            // ******************************************
            // ***         Get the previous topic      ****
            // ******************************************
            // If the direction is next get the previous topic
            if ((strDirection == "P"))
            {
                // Initliase the SQL query to get the topic and forumID from the database
                strSQL = ("SELECT TOP 1 "
                            + (Variables.Forum.strDbTable + "Topic.Topic_ID "));
                strSQL = (strSQL + ("FROM "
                            + (Variables.Forum.strDbTable + "Topic ")));
                strSQL = (strSQL + ("WHERE "
                            + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                            + (Variables.Forum.intForumID + (" AND "
                            + (Variables.Forum.strDbTable + ("Topic.Topic_ID < "
                            + (lngTopicID + " ")))))))));
                strSQL = (strSQL + ("ORDER BY "
                            + (Variables.Forum.strDbTable + "Topic.Topic_ID DESC;")));
                rsCommon = db.Execute(strSQL);
                // If there is a record returned read in the topic ID to move to
                if ((rsCommon.Rows.Count > 0))
                {
                    lngNewTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                }
                else
                {
                    // Create a new query to get the first topic in the forum
                    strSQL = ("SELECT TOP 1 "
                                + (Variables.Forum.strDbTable + "Topic.Topic_ID "));
                    strSQL = (strSQL + ("FROM "
                                + (Variables.Forum.strDbTable + "Topic ")));
                    strSQL = (strSQL + ("WHERE "
                                + (Variables.Forum.strDbTable + ("Topic.Forum_ID = "
                                + (Variables.Forum.intForumID + " ")))));
                    strSQL = (strSQL + ("ORDER BY "
                                + (Variables.Forum.strDbTable + "Topic.Topic_ID DESC;")));
                    rsCommon = db.Execute(strSQL);
                    // Get the topic ID of the last topic in the database
                    if ((rsCommon.Rows.Count > 0))
                    {
                        lngNewTopicID = long.Parse(rsCommon.Rows[0]["Topic_ID"].ToString());
                    }

                }
            }
            // Reset main server variables
            rsCommon = null;
            Response.Redirect(("forum_posts.aspx?TID=" + lngNewTopicID));
        }

    }

}