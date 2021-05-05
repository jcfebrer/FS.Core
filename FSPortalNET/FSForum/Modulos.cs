using FSLibrary;
using FSPortal;
using System;
using System.Data;

namespace FSForum
{
    public class Modulos
    {

        private string ModTopPosters(long id)
        {
            string modTopPostersReturn = null;
            modTopPostersReturn = @"<table width=""100%"" border=""0"" cellspacing=""1"" cellpadding=""4"">" + "\r\n";
            modTopPostersReturn = modTopPostersReturn + "<tr>" + "\r\n";
            modTopPostersReturn = modTopPostersReturn + @"<td width=""50%"" align=""center"">";
            modTopPostersReturn = modTopPostersReturn + "Usuario" + "</td>" + "\r\n";
            modTopPostersReturn = modTopPostersReturn + @"<td width=""50%"" align=""center"">";
            modTopPostersReturn = modTopPostersReturn + "Posts" + "</td>" + "\r\n";
            modTopPostersReturn = modTopPostersReturn + "</tr>" + "\r\n";

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string ssql = null;

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.MySQL)
            {
                ssql = "SELECT usuario,usuarioId,no_of_posts FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios WHERE no_of_posts>0 ORDER BY No_of_posts DESC LIMIT 5";
            }
            else
            {
                ssql = "SELECT Top 5 usuario,usuarioId,no_of_posts FROM " + FSPortal.Variables.App.prefijoTablas + "Usuarios WHERE no_of_posts>0 ORDER BY No_of_posts DESC";
            }

            DataTable dtPosters = db.Execute(ssql);
            if (dtPosters.Rows.Count > 0)
            {
                foreach (DataRow row in dtPosters.Rows)
                {
                    modTopPostersReturn = modTopPostersReturn + "<tr>" + "\r\n";
                    modTopPostersReturn = modTopPostersReturn + @"<td width=""50%"">";
                    modTopPostersReturn = modTopPostersReturn + @"<a href='#' onclick=""javascript:window.open('" + FSPortal.Variables.App.directorioPortal + "forum/pop_up_profile.aspx?PF=" + row["usuarioID"].ToString() + @"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"">";
                    modTopPostersReturn = modTopPostersReturn + row["usuario"].ToString() + "</a></td>" + "\r\n";
                    modTopPostersReturn = modTopPostersReturn + @"<td width=""50%"" align=""center"">";
                    modTopPostersReturn = modTopPostersReturn + row["no_of_posts"].ToString() + "</td>" + "\r\n";
                    modTopPostersReturn = modTopPostersReturn + "</tr>" + "\r\n";
                }
            }
            else
            {
                modTopPostersReturn = modTopPostersReturn + "<tr>" + "\r\n";
                modTopPostersReturn = modTopPostersReturn + "<td colspan='2'>";
                modTopPostersReturn = modTopPostersReturn + "No se encontraron usuarios.</td>" + "\r\n";
                modTopPostersReturn = modTopPostersReturn + "</tr>" + "\r\n";
            }
            modTopPostersReturn = modTopPostersReturn + "</table>" + "\r\n";

            return modTopPostersReturn;
        }



        private string ModTopPosts(long id)
        {
            string modTopPostsReturn = null;
            modTopPostsReturn = @"<table width=""100%"" style=""height:50"" border=""0"" cellspacing=""1"" cellpadding=""4"">" + "\r\n";

            int DiffDate = 0;
            string DateDf = null;
            string ssql = null;
            string Topic_Html = "";
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.MySQL)
            {
                ssql = "SELECT ForumTopic.Topic_ID,ForumTopic.Subject,ForumTopic.Last_entry_date, " + "ForumForum.Hide,ForumForum.Forum_ID,ForumForum.Forum_name FROM ForumTopic " + "INNER JOIN ForumForum ON ForumTopic.Forum_ID = ForumForum.Forum_ID " + "WHERE (((ForumForum.Clave Is Null OR Len(ForumForum.Clave)=0) AND ForumForum.Hide=0)) " + "ORDER BY ForumTopic.Last_entry_date DESC LIMIT 5;";

            }
            else
            {
                ssql = "SELECT TOP 5 ForumTopic.Topic_ID,ForumTopic.Subject,ForumTopic.Last_entry_date, " + "ForumForum.Hide,ForumForum.Forum_ID,ForumForum.Forum_name FROM ForumTopic " + "INNER JOIN ForumForum ON ForumTopic.Forum_ID = ForumForum.Forum_ID " + "WHERE (((ForumForum.Clave Is Null OR Len(ForumForum.Clave)=0) AND ForumForum.Hide=0)) " + "ORDER BY ForumTopic.Last_entry_date DESC;";
            }

            DataTable dtTopPosts = db.Execute(ssql);

            if (dtTopPosts.Rows.Count > 0)
            {
                foreach (DataRow row in dtTopPosts.Rows)
                {
                    TimeSpan TS = new TimeSpan(System.DateTime.Now.Ticks - System.DateTime.Parse(row["last_entry_date"].ToString()).Ticks);
                    DiffDate = TS.Days;
                    switch (DiffDate)
                    {
                        case 0:
                            DateDf = "Hoy";
                            break;
                        case 1:
                            DateDf = "Ayer";
                            break;
                        default:
                            DateDf = System.DateTime.Parse(row["last_entry_date"].ToString()).ToLongDateString();
                            break;
                    }

                    Topic_Html = Topic_Html + @"<a href=""" + FSPortal.Variables.App.directorioPortal + "forum/forum_posts.aspx?TID=" + row["Topic_ID"].ToString() + "&amp;get=last#" + FSForum.FuncionesForum.getThreadID(System.Convert.ToInt64(NumberUtils.NumberDouble(row["Topic_ID"]))) + @""" class=""smLink"">";
                    Topic_Html = Topic_Html + @"<img src=""" + FSPortal.Variables.App.directorioPortal + @"imagenes/bullet.gif"" alt=""" + "Último post" + @""" border=""0"" align=""middle"" /></a>&nbsp;";
                    Topic_Html = Topic_Html + @"<a href=""" + FSPortal.Variables.App.directorioPortal + "forum/forum_posts.aspx?TID=" + row["Topic_ID"].ToString() + @""" class=""smLink"">";
                    Topic_Html = Topic_Html + row["Subject"].ToString() + "</a>";
                    Topic_Html = Topic_Html + @"<br /><span class=""smText"">" + "Último mensaje por: " + @"<a href=""#"" onclick=""javascript:window.open('" + FSPortal.Variables.App.directorioPortal + "forum/pop_up_profile.aspx?PF=" + FSForum.FuncionesForum.getUserIDbyTopicID(System.Convert.ToInt64(row["Topic_ID"]));
                    Topic_Html = Topic_Html + @"','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=590,height=425')"" class=""smLink"">" + FSForum.FuncionesForum.getUserNamebyTopicID(System.Convert.ToInt64(NumberUtils.NumberDouble(row["Topic_ID"]))) + "</a>";
                    Topic_Html = Topic_Html + "<br />" + "Forum: " + @"<a href=""" + FSPortal.Variables.App.directorioPortal + "forum/forum_topics.aspx?FID=" + row["Forum_ID"].ToString() + @""" class=""smLink"">" + row["Forum_name"].ToString() + "</a><br />";
                    Topic_Html = Topic_Html + "Enviado: " + "<b>" + DateDf + "</b>&nbsp;" + " a las:<b> ";
                    Topic_Html = Topic_Html + System.DateTime.Parse(row["last_entry_date"].ToString()).ToLongTimeString() + "</b></span><br /><br />";

                }
                modTopPostsReturn = modTopPostsReturn + "<tr>" + @"<td valign=""top"">";


                modTopPostsReturn = modTopPostsReturn + Topic_Html;

                modTopPostsReturn = modTopPostsReturn + "</td>" + "</tr>";
            }
            else
            {
                modTopPostsReturn = modTopPostsReturn + "<tr>" + @"<td valign=""top"">";
                modTopPostsReturn = modTopPostsReturn + "No hay posts recientes" + "</td>" + "</tr>";
            }
            modTopPostsReturn = modTopPostsReturn + "</table>" + "\r\n";

            return modTopPostsReturn;
        }
    }
}