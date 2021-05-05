// <fileheader>
// <copyright file="show_forum_permissions_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\show_forum_permissions_inc.ascx.cs
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
        public class show_forum_permissions_inc
        {
            public static string Render()
            {
                StringBuilder sb = new StringBuilder();
                // Write what permissions the user has in the forum
                // Display the users new post permissions
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnPost == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtpostNewTopicsInThisForum + "<br />")));
                // Reply permisisons
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnReply == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtReplyToTopicsInThisForum + "<br />")));
                // Delete permssions
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnDelete == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtDeleteYourPostsInThisForum + "<br />")));
                // Edit permissions
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnEdit == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtEditYourPostsInThisForum + "<br />")));
                // Create poll permissions  
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnPollCreate == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtCreatePollsInThisForum + "<br />")));
                // Vote in poll permissions 
                sb.AppendLine((Variables.Forum.strTxtYou + " <span style=\"font-weight: bold;\">"));
                if ((Variables.Forum.blnVote == true))
                {
                    sb.AppendLine(Variables.Forum.strTxtCan);
                }
                else
                {
                    sb.AppendLine(Variables.Forum.strTxtCannot);
                }

                sb.AppendLine(("</span> "
                                + (Variables.Forum.strTxtVoteInPOllsInThisForum + "<br />")));
                return sb.ToString();
            }

        }
    }
}