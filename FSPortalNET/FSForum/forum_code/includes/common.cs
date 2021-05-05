// <fileheader>
// <copyright file="common.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\common.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using FSPortal;
using FSLibrary;
using FSForum;
using FSNetwork;

namespace FSForum.Includes
{
    public class common
    {
        public static void Inicio()
        {
            string strSQL = "";
            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");


            // Initialise variables
            Variables.Forum.strLoggedInusuario = Variables.Forum.strTxtGuest;
            Variables.Forum.blnActiveMember = true;
            Variables.Forum.blnLoggedInUserEmail = false;
            Variables.Forum.blnLoggedInUserSignature = false;
            Variables.Forum.blnModerator = false;
            Variables.Forum.blnGuest = true;
            Variables.Forum.intTimeOffSet = 0;
            Variables.Forum.strTimeOffSet = "+";
            Variables.Forum.blnWYSIWYGEditor = true;
            Variables.Forum.blnLongRegForm = true;
            Variables.Forum.blnLongSecurityCode = false;


            // Database Type
            // Variables.Forum.strDatabaseType = "Access"
            // Variables.Forum.strDatabaseType = "SQLServer"




            // Set if application variables are used for forum configuration
            // This will make your forum run faster as there are less hits on the database, but if you are using free web hosting or
            // are on a server where you share your application oject with others then you will need to set this to false
            //Variables.Forum.blnUseApplicationVariables = false;


            // Create database session("conn")ection
            // Create a session("conn")ection odject
            // Set adoCon = Server.CreateObject("ADODB.Connection")

            // If this is access set the access driver
            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.Access2000 | FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.Access97 | FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.Oledb)
            {


                // --------------------- Set the path and name of the database --------------------------------------------------------------------------------

                // Virtual path to database
                Variables.Forum.strDbPathAndName = @"D:\Domains\portal.com\db\portal.mdb";  // This is the path of the database from this files location on the server

                // Physical path to database
                // strDbPathAndName = "" 'Use this if you use the physical server path, eg:- C:\Inetpub\private\wwForum.mdb


                // BRINKSTER USEdt(forum only works with free Brinkster accounts, not for the paid accounts)
                // Brinkster users remove the ' single quote mark from infront of the line below and replace usuario with your Brinkster uersname

                // strDbPathAndName =  Server.MapPath("/usuario/db/wwForum.mdb")

                // PLEASE NOTE: - For extra security it is highly recommended you change the name of the database, wwForum.mdb, to another name and then
                // replace the wwForum.mdb found above with the name you changed the forum database to.

                // ---------------------------------------------------------------------------------------------------------------------------------------------


                // ------------- If you are having problems with the script then try using a diffrent driver or DSN by editing the lines below --------------

                // Generic MS Access Database session("conn")ection info and driver (if this driver does not work then comment it out and use one of the alternative faster JET OLE DB drivers)
                // strCon = "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & strDbPathAndName

                // Alternative drivers faster than the generic one above
                // strCon = "Provider=Microsoft.Jet.OLEDB.3.51; Data Source=" & strDbPathAndName 'This one is if you convert the database to Access 97
                // strCon = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & strDbPathAndName  'This one is for Access 2000/2002
                Variables.Forum.strCon = db.ConnString;

                // If you wish to use DSN then comment out the driver above and uncomment the line below (DSN is slower than the above drivers)
                // strCon = "DSN=DSN_NAME" 'Place the DSN where you see DSN_NAME

                // ---------------------------------------------------------------------------------------------------------------------------------------------


                // The now() function is used in Access for dates
                Variables.Forum.strDatabaseDateFunction = "Now()";
            }
            else

                // The GetDate() function is used in SQL Server
                Variables.Forum.strDatabaseDateFunction = "GetDate()";


            // Set the session("conn")ection string to the database
            // adoCon.connectionstring = strCon

            // Set an active session("conn")ection to the session("conn")ection object
            // adoCon.Open

            // Intialise the main ADO recordset object
            // Set rsCommon = Server.CreateObject("ADODB.Recordset")

            // Read in the Forum configuration


            // Initialise the SQL variable with an SQL statement to get the configuration details from the database
            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                strSQL = "Execute " + Variables.Forum.strDbProc + "SelectConfiguration";
            else
                strSQL = "SELECT TOP 1 " + Variables.Forum.strDbTable + "Configuration.* From " + Variables.Forum.strDbTable + "Configuration;";

            // Query the database
            System.Data.DataTable rsCommon;
            rsCommon = db.Execute(strSQL);

            // If there is config deatils in the recordset then read them in
            if (rsCommon.Rows.Count > 0)
            {

                // read in the configuration details from the recordset
                Variables.Forum.strWebsiteName = rsCommon.Rows[0]["website_name"].ToString();
                Variables.Forum.strMainForumName = rsCommon.Rows[0]["forum_name"].ToString();
                Variables.Forum.strWebsiteURL = rsCommon.Rows[0]["website_path"].ToString();
                // Var.webHttp = rsCommon.Rows(0)("forum_path").ToString
                Variables.Forum.strMailComponent = rsCommon.Rows[0]["mail_component"].ToString();
                Variables.Forum.strOutgoingMailServer = rsCommon.Rows[0]["mail_server"].ToString();
                Variables.Forum.strForumEmailAddress = rsCommon.Rows[0]["forum_email_address"].ToString();
                Variables.Forum.blnLCode = System.Convert.ToBoolean(rsCommon.Rows[0]["L_Code"]);
                Variables.Forum.blnEmail = System.Convert.ToBoolean(rsCommon.Rows[0]["email_notify"]);
                Variables.Forum.blnTextLinks = System.Convert.ToBoolean(rsCommon.Rows[0]["Text_link"]);
                Variables.Forum.blnRTEEditor = System.Convert.ToBoolean(rsCommon.Rows[0]["IE_editor"]);
                Variables.Forum.intTopicPerPage = System.Convert.ToInt32(rsCommon.Rows[0]["Topics_per_page"]);
                Variables.Forum.strTitleImage = rsCommon.Rows[0]["Title_image"].ToString();
                Variables.Forum.blnEmoticons = System.Convert.ToBoolean(rsCommon.Rows[0]["Emoticons"]);
                Variables.Forum.blnAvatar = System.Convert.ToBoolean(rsCommon.Rows[0]["Avatar"]);
                Variables.Forum.blnEmailActivation = System.Convert.ToBoolean(rsCommon.Rows[0]["Email_activate"]);
                Variables.Forum.intNumHotViews = System.Convert.ToInt32(rsCommon.Rows[0]["Hot_views"]);
                Variables.Forum.intNumHotReplies = System.Convert.ToInt32(rsCommon.Rows[0]["Hot_replies"]);
                Variables.Forum.blnSendPost = System.Convert.ToBoolean(rsCommon.Rows[0]["Email_post"]);
                Variables.Forum.blnPrivateMessages = System.Convert.ToBoolean(rsCommon.Rows[0]["Private_msg"]);
                Variables.Forum.intNumPrivateMessages = System.Convert.ToInt32(rsCommon.Rows[0]["No_of_priavte_msg"]);
                Variables.Forum.intThreadsPerPage = System.Convert.ToInt32(rsCommon.Rows[0]["Threads_per_page"]);
                Variables.Forum.intSpamTimeLimitSeconds = System.Convert.ToInt32(rsCommon.Rows[0]["Spam_seconds"]);
                Variables.Forum.intSpamTimeLimitMinutes = System.Convert.ToInt32(rsCommon.Rows[0]["Spam_minutes"]);
                Variables.Forum.intMaxPollChoices = System.Convert.ToInt32(rsCommon.Rows[0]["Vote_choices"]);
                Variables.Forum.blnEmailMessenger = System.Convert.ToBoolean(rsCommon.Rows[0]["Email_sys"]);
                Variables.Forum.blnActiveUsers = System.Convert.ToBoolean(rsCommon.Rows[0]["Active_users"]);
                Variables.Forum.blnForumClosed = System.Convert.ToBoolean(rsCommon.Rows[0]["Forums_closed"]);
                Variables.Forum.blnShowEditUser = System.Convert.ToBoolean(rsCommon.Rows[0]["Show_edit"]);
                Variables.Forum.blnShowProcessTime = System.Convert.ToBoolean(rsCommon.Rows[0]["Process_time"]);
                Variables.Forum.blnFlashFiles = System.Convert.ToBoolean(rsCommon.Rows[0]["Flash"]);
                Variables.Forum.blnShowMod = System.Convert.ToBoolean(rsCommon.Rows[0]["Show_mod"]);
                Variables.Forum.blnAvatarUploadEnabled = System.Convert.ToBoolean(rsCommon.Rows[0]["Upload_avatar"]);
                Variables.Forum.blnRegistrationSuspeneded = System.Convert.ToBoolean(rsCommon.Rows[0]["Reg_closed"]);
                Variables.Forum.strImageTypes = rsCommon.Rows[0]["Upload_img_types"].ToString();

            }


            // If the forums are closed redirect to the forums closed page
            if (Variables.Forum.blnForumClosed & Variables.Forum.blnClosedForumPage == false)

                // Redirect to the forum closed page
                FSPortal.Variables.App.Page.Response.Redirect("forum_closed.aspx");


            // Initalise the process start time
            if (Variables.Forum.blnShowProcessTime)
                Variables.Forum.dblStartTime = System.DateTime.Now.Ticks;


            // 'FEBRER: Para marcar los nuevos mensajes.
            // Set a cookie with the last date/time the user used the forum to calculate if there any new posts
            // If the date/time the user was last here is 20 minutes since the last visit then set the session variable to the users last date they were here
            if (Functions.Valor(FSPortal.Variables.App.Page.Session["dtmLastVisit"]) == "" & Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], "LTVST") != "")
            {
                FSPortal.Variables.App.Page.Session["dtmLastVisit"] = Convert.ToDateTime(FSNetwork.Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], "LTVST"));
                FSPortal.Variables.App.Page.Response.Cookies[FSPortal.Variables.App.strCookieName]["LTVST"] = System.DateTime.Now.ToString();
                FSPortal.Variables.App.Page.Response.Cookies["LTVST"].Expires = System.DateTime.Now.AddYears(1);
            }
            else if (Functions.Valor(FSPortal.Variables.App.Page.Session["dtmLastVisit"]) == "")
                FSPortal.Variables.App.Page.Session["dtmLastVisit"] = System.DateTime.Now;


            // If the cookie is older than 5 mintues set a new one
            if (FSLibrary.DateTimeUtil.IsDate(FSNetwork.Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], "LTVST")))
            {
                if (Convert.ToDateTime(FSNetwork.Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], "LTVST")) < System.DateTime.Now.AddMinutes(-5))
                {
                    //'Response.Cookies(Var.strCookieName)("LTVST") = Now().ToString
                    //'Response.Cookies("LTVST").Expires = DateAdd("yyyy", 1, Now())
                }
            }
            else
            {
                //If there is no date in the cookie or it is empty then set the date to now()
                //'Response.Cookies(Var.strCookieName)("LTVST") = Now().ToString
                //'Response.Cookies("LTVST").Expires = DateAdd("yyyy", 1, Now())
            }




            // If someone has placed the default.aspx in the path to the forum then remove it as it's not needed
            // Var.webHttp = Replace(Var.webHttp, "default.aspx", "")



            // Read in users ID number from the cookie
            Variables.Forum.strLoggedInUserCode = FSLibrary.TextUtil.Trim(FSLibrary.TextUtil.Substring(FSNetwork.Web.Cookie(FSPortal.Variables.App.Page.Request.Cookies[FSPortal.Variables.App.strCookieName], "UID"), 1, 44));

            // If a cookie exsists on the users system then read in there usuario from the database
            if (Variables.Forum.strLoggedInUserCode != "")
            {

                // Make the usercode SQL safe
                Variables.Forum.strLoggedInUserCode = FuncionesFilter.formatSQLInput(Variables.Forum.strLoggedInUserCode);

                // Initalise the strSQL variable with an SQL statement to query the database
                if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                    strSQL = "Execute " + Variables.Forum.strDbProc + "ChkUserID @strUserID = '" + Variables.Forum.strLoggedInUserCode + "'";
                else
                {
                    strSQL = "SELECT " + "Usuarios.usuario, " + "Usuarios.UsuarioID, " + "Usuarios.Group_ID, " + "Usuarios.Active, " + "Usuarios.Signature, " + "Usuarios.email, " + "Usuarios.Date_format, " + "Usuarios.Time_offset, " + "Usuarios.Time_offset_hours, " + "Usuarios.Reply_notify, " + "Usuarios.Attach_signature, " + "Usuarios.Rich_editor, " + "Usuarios.UltimaConexion ";
                    strSQL = strSQL + "FROM " + "Usuarios ";
                    strSQL = strSQL + "WHERE " + "Usuarios.User_code = '" + Variables.Forum.strLoggedInUserCode + "';";
                }

                // Query the database
                rsCommon = db.Execute(strSQL);

                // If the database has returned a record then run next bit
                if (rsCommon.Rows.Count > 0)
                {

                    // Read in the users details from the recordset
                    Variables.Forum.strLoggedInusuario = rsCommon.Rows[0]["usuario"].ToString();
                    // FSPortal.Variables.User.GroupId = CInt(rsCommon.Rows(0)("Group_ID").ToString)
                    // FSPortal.Variables.User.UsuarioId = CInt(rsCommon.Rows(0)("UsuarioID"))
                    Variables.Forum.blnActiveMember = System.Convert.ToBoolean(rsCommon.Rows[0]["Active"]);
                    Variables.Forum.strDateFormat = rsCommon.Rows[0]["Date_format"].ToString();
                    Variables.Forum.strTimeOffSet = rsCommon.Rows[0]["Time_offset"].ToString();
                    Variables.Forum.intTimeOffSet = System.Convert.ToInt32(rsCommon.Rows[0]["Time_offset_hours"]);
                    Variables.Forum.blnReplyNotify = System.Convert.ToBoolean(rsCommon.Rows[0]["Reply_notify"]);
                    Variables.Forum.blnAttachSignature = System.Convert.ToBoolean(rsCommon.Rows[0]["Attach_signature"]);
                    Variables.Forum.blnWYSIWYGEditor = System.Convert.ToBoolean(rsCommon.Rows[0]["Rich_editor"]);
                    Variables.Forum.strLoggedInUserEmail = rsCommon.Rows[0]["email"].ToString();
                    if (rsCommon.Rows[0]["Signature"].ToString() != "")
                        Variables.Forum.blnLoggedInUserSignature = true;

                    // See if the user has entered an email address
                    if (Variables.Forum.strLoggedInUserEmail != "")
                        Variables.Forum.blnLoggedInUserEmail = true;



                    // Read in the Last Visit Date for the user from the db if we haven't already
                    if (Functions.Valor(FSPortal.Variables.App.Page.Session["ViRead"]) == "")
                    {
                        if (FSLibrary.DateTimeUtil.IsDate(rsCommon.Rows[0]["UltimaConexion"].ToString()))
                            FSPortal.Variables.App.Page.Session["dtmLastVisit"] = (System.DateTime)rsCommon.Rows[0]["UltimaConexion"];
                        FSPortal.Variables.App.Page.Session["ViRead"] = true;
                    }

                    // Check that there is a last visit date in the db or we will get an error
                    if (FSLibrary.DateTimeUtil.IsDate(rsCommon.Rows[0]["UltimaConexion"].ToString()))
                    {

                        // If the Last Visit date in the db is older than 5 minutes for the user then update it
                        if ((System.DateTime)rsCommon.Rows[0]["UltimaConexion"] < System.DateTime.Now.AddMinutes(-5))
                        {

                            // Initilse sql statement
                            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                                strSQL = "Execute " + Variables.Forum.strDbProc + "UpdateLasVisit @lngUserID = " + FSPortal.Variables.User.UsuarioId;
                            else
                                strSQL = "UPDATE " + "Usuarios SET " + "Usuarios.UltimaConexion = Now() WHERE " + "Usuarios.UsuarioID=" + FSPortal.Variables.User.UsuarioId + ";";

                            // Write to database
                            db.Execute(strSQL);
                        }
                    }
                    else
                    {

                        // Initilse sql statement
                        if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
                            strSQL = "Execute " + Variables.Forum.strDbProc + "UpdateLasVisit @lngUserID = " + FSPortal.Variables.User.UsuarioId;
                        else
                            strSQL = "UPDATE " + "Usuarios SET " + "Usuarios.UltimaConexion=Now() WHERE " + "Usuarios.UsuarioID=" + FSPortal.Variables.User.UsuarioId + ";";

                        // Write to database
                        db.Execute(strSQL);
                    }

                    // If the members account is not active then set there group to 2 (Guest Group)
                    if (Variables.Forum.blnActiveMember == false)
                        FSPortal.Variables.User.GroupId = 2;

                    // Set the Guest boolean to false
                    Variables.Forum.blnGuest = false;
                }
                else
                {
                    // Make sure the main admin account remains active and full access rights and in the admin group
                    if (FSPortal.Variables.User.UsuarioId == 1)
                    {
                        FSPortal.Variables.User.GroupId = 1;
                        Variables.Forum.blnActiveMember = true;
                    }
                }
            }

            // If in the admin group set the admin boolean to true
            // If FSPortal.Variables.User.GroupId = 1 Then FSPortal.Variables.User.Administrador = True


            //// If active users is on update the table
            //if (Variables.Forum.blnActiveUsers)
            //    s = s + FSLibrary.Text.ControlChars.CrLf + "<!--include file=\"includes/active_users_inc.aspx\" -->";
        }

 

        public string userCode(string strUserCode)
        {
            strUserCode = strUserCode + hexValue(10);
            strUserCode = FuncionesFilter.formatSQLInput(strUserCode);
            strUserCode = strUserCode.Replace("''", "'");
            return strUserCode;
        }




        public string hexValue(int intHexLength)
        {

            int intLoopCounter = 0;
            string strHexValue = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            Random r = new Random();

            for (intLoopCounter = 1; intLoopCounter <= intHexLength; intLoopCounter++)
            {

                intHexLength = NumberUtils.NumberInt(r.Next(1000)) % 16;

                switch (intHexLength)
                {
                    case 1:
                        strHexValue = "1";
                        break;
                    case 2:
                        strHexValue = "2";
                        break;
                    case 3:
                        strHexValue = "3";
                        break;
                    case 4:
                        strHexValue = "4";
                        break;
                    case 5:
                        strHexValue = "5";
                        break;
                    case 6:
                        strHexValue = "6";
                        break;
                    case 7:
                        strHexValue = "7";
                        break;
                    case 8:
                        strHexValue = "8";
                        break;
                    case 9:
                        strHexValue = "9";
                        break;
                    case 10:
                        strHexValue = "A";
                        break;
                    case 11:
                        strHexValue = "B";
                        break;
                    case 12:
                        strHexValue = "C";
                        break;
                    case 13:
                        strHexValue = "D";
                        break;
                    case 14:
                        strHexValue = "E";
                        break;
                    case 15:
                        strHexValue = "F";
                        break;
                    default:
                        strHexValue = "Z";
                        break;
                }


                sb.AppendLine(strHexValue);
            }

            return sb.ToString();
        }





        public string BrowserType()
        {
            return FSPortal.Variables.App.Page.Request.ServerVariables["HTTP_USER_AGENT"];
        }


        public string OSType()
        {
            return FSPortal.Variables.App.Page.Request.ServerVariables["HTTP_USER_AGENT"];
        }


        public static void updateTopicPostCount(int intForumID)
        {

            DataTable dtCount;
            long lngNumberOfTopics = 0;
            long lngNumberOfPosts = 0;

            lngNumberOfTopics = 0;
            lngNumberOfPosts = 0;


            string strSQL = "SELECT Count(" + Variables.Forum.strDbTable + "Topic.Forum_ID) AS Topic_Count ";
            strSQL = strSQL + "From " + Variables.Forum.strDbTable + "Topic ";
            strSQL = strSQL + "WHERE " + Variables.Forum.strDbTable + "Topic.Forum_ID = " + intForumID + " ";

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            dtCount = db.Execute(strSQL);

            if (dtCount.Rows.Count > 0)
            {
                lngNumberOfTopics = System.Convert.ToInt64(dtCount.Rows[0]["Topic_Count"]);
            }

            strSQL = "SELECT Count(" + Variables.Forum.strDbTable + "Thread.Thread_ID) AS Thread_Count ";
            strSQL = strSQL + "FROM " + Variables.Forum.strDbTable + "Topic INNER JOIN " + Variables.Forum.strDbTable + "Thread ON " + Variables.Forum.strDbTable + "Topic.Topic_ID = " + Variables.Forum.strDbTable + "Thread.Topic_ID ";
            strSQL = strSQL + "GROUP BY " + Variables.Forum.strDbTable + "Topic.Forum_ID ";
            strSQL = strSQL + "HAVING (((" + Variables.Forum.strDbTable + "Topic.Forum_ID)=" + intForumID + "));";

            dtCount = db.Execute(strSQL);

            if (dtCount.Rows.Count > 0)
            {
                lngNumberOfPosts = System.Convert.ToInt64(dtCount.Rows[0]["Thread_Count"]);
            }

            strSQL = "UPDATE " + Variables.Forum.strDbTable + "Forum SET ";
            strSQL = strSQL + "" + Variables.Forum.strDbTable + "Forum.No_of_topics = " + lngNumberOfTopics + ", " + Variables.Forum.strDbTable + "Forum.No_of_posts = " + lngNumberOfPosts;
            strSQL = strSQL + " WHERE " + Variables.Forum.strDbTable + "Forum.Forum_ID= " + intForumID + ";";

            db.ExecuteNonQuery(strSQL);
        }






        public static bool isModerator(int intForumID, int intGroupID)
        {
            bool isModeratorReturn = false;

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            DataTable dtPermissions;
            bool blnModerator = false;
            string strSQL = null;

            blnModerator = false;

            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = "Execute " + Variables.Forum.strDbProc + "ForumPermissions @VarForum.intForumID = " + Variables.Forum.intForumID + ", @intGroupID = " + FSPortal.Variables.User.GroupId + ", @intUsuariosID = " + FSPortal.Variables.User.UsuarioId;
            }
            else
            {
                strSQL = "SELECT " + Variables.Forum.strDbTable + "Permissions.* ";
                strSQL = strSQL + "FROM " + Variables.Forum.strDbTable + "Permissions ";
                strSQL = strSQL + "WHERE (" + Variables.Forum.strDbTable + "Permissions.Group_ID = " + FSPortal.Variables.User.GroupId + " OR " + Variables.Forum.strDbTable + "Permissions.UsuarioID = " + FSPortal.Variables.User.UsuarioId + ") AND " + Variables.Forum.strDbTable + "Permissions.Forum_ID = " + Variables.Forum.intForumID + " ";
                strSQL = strSQL + "ORDER BY " + Variables.Forum.strDbTable + "Permissions.UsuarioID DESC;";
            }

            dtPermissions = db.Execute(strSQL);

            if (dtPermissions.Rows.Count > 0)
            {
                blnModerator = System.Convert.ToBoolean(dtPermissions.Rows[0]["Moderate"]);
            }

            isModeratorReturn = blnModerator;

            return isModeratorReturn;
        }





        public static string disallowedMemberNames(string strusuario)
        {
            string disallowedMemberNamesReturn = null;

            strusuario = strusuario.Replace("salt", "");
            strusuario = strusuario.Replace("clave", "");
            strusuario = strusuario.Replace("Usuarios", "");
            strusuario = strusuario.Replace("code", "");
            strusuario = strusuario.Replace("usuario", "");
            strusuario = strusuario.Replace("N0act", "");

            disallowedMemberNamesReturn = strusuario;
            return disallowedMemberNamesReturn;
        }




        public static bool bannedIP()
        {
            bool bannedIPReturn = false;


            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");
            string strCheckIPAddress = null;
            string strUserIPAddress = null;
            bool blnIPMatched = false;
            string strSQL = null;

            blnIPMatched = false;

            strUserIPAddress = Http.IpAddress();


            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = "Execute " + Variables.Forum.strDbProc + "BannedIPs";
            }
            else
            {
                strSQL = "SELECT " + Variables.Forum.strDbTable + "BanList.IP FROM " + Variables.Forum.strDbTable + "BanList WHERE " + Variables.Forum.strDbTable + "BanList.IP Is Not Null;";
            }

            DataTable dtIPAddr = db.Execute(strSQL);

            foreach (DataRow row in dtIPAddr.Rows)
            {

                strCheckIPAddress = Functions.Valor(row["IP"]);

                if (TextUtil.Substring(strCheckIPAddress, strCheckIPAddress.Length - 1) == "*")
                {

                    strCheckIPAddress = strCheckIPAddress.Replace("*", "");

                    strUserIPAddress = TextUtil.Substring(strUserIPAddress, 1, strCheckIPAddress.Length);

                    if (strCheckIPAddress == strUserIPAddress)
                    {
                        blnIPMatched = true;
                    }

                }
                else
                {
                    if (strCheckIPAddress == strUserIPAddress)
                    {
                        blnIPMatched = true;
                    }

                }
            }

            bannedIPReturn = blnIPMatched;

            return bannedIPReturn;
        }









        public void checkSessionID(long lngAspSessionID)
        {

            if (lngAspSessionID != double.Parse(FSPortal.Variables.App.Page.Session.SessionID))
            {

                FSPortal.Variables.App.Page.Response.Redirect("insufficient_permission.aspx?FID=" + Variables.Forum.intForumID + "&amp;m=sID");
            }

        }



        public void SortActiveUsersList(ref string[,] saryActiveUsers)
        {

            int intIndexPosition = 0;
            int intPassNumber = 0;
            string[] saryTempStringStore = new string[8];

            for (intPassNumber = 1; intPassNumber <= saryActiveUsers.GetUpperBound(2 - 1); intPassNumber++)
            {

                for (intIndexPosition = 1; intIndexPosition <= (saryActiveUsers.GetUpperBound(2 - 1) - intPassNumber); intIndexPosition++)
                {

                    if (double.Parse(saryActiveUsers[4, intIndexPosition]) < double.Parse(saryActiveUsers[4, (intIndexPosition + 1)]))
                    {


                        saryTempStringStore[0] = saryActiveUsers[0, intIndexPosition];
                        saryTempStringStore[1] = saryActiveUsers[1, intIndexPosition];
                        saryTempStringStore[2] = saryActiveUsers[2, intIndexPosition];
                        saryTempStringStore[3] = saryActiveUsers[3, intIndexPosition];
                        saryTempStringStore[4] = saryActiveUsers[4, intIndexPosition];
                        saryTempStringStore[5] = saryActiveUsers[5, intIndexPosition];
                        saryTempStringStore[6] = saryActiveUsers[6, intIndexPosition];
                        saryTempStringStore[7] = saryActiveUsers[7, intIndexPosition];



                        saryActiveUsers[0, intIndexPosition] = saryActiveUsers[0, (intIndexPosition + 1)];
                        saryActiveUsers[1, intIndexPosition] = saryActiveUsers[1, (intIndexPosition + 1)];
                        saryActiveUsers[2, intIndexPosition] = saryActiveUsers[2, (intIndexPosition + 1)];
                        saryActiveUsers[3, intIndexPosition] = saryActiveUsers[3, (intIndexPosition + 1)];
                        saryActiveUsers[4, intIndexPosition] = saryActiveUsers[4, (intIndexPosition + 1)];
                        saryActiveUsers[5, intIndexPosition] = saryActiveUsers[5, (intIndexPosition + 1)];
                        saryActiveUsers[6, intIndexPosition] = saryActiveUsers[6, (intIndexPosition + 1)];
                        saryActiveUsers[7, intIndexPosition] = saryActiveUsers[7, (intIndexPosition + 1)];

                        saryActiveUsers[0, (intIndexPosition + 1)] = saryTempStringStore[0];
                        saryActiveUsers[1, (intIndexPosition + 1)] = saryTempStringStore[1];
                        saryActiveUsers[2, (intIndexPosition + 1)] = saryTempStringStore[2];
                        saryActiveUsers[3, (intIndexPosition + 1)] = saryTempStringStore[3];
                        saryActiveUsers[4, (intIndexPosition + 1)] = saryTempStringStore[4];
                        saryActiveUsers[5, (intIndexPosition + 1)] = saryTempStringStore[5];
                        saryActiveUsers[6, (intIndexPosition + 1)] = saryTempStringStore[6];
                        saryActiveUsers[7, (intIndexPosition + 1)] = saryTempStringStore[7];
                    }
                }
            }
        }
    }
}
