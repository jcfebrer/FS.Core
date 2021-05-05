// <fileheader>
// <copyright file="openforum.ascx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: foro\openforum.ascx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using System.Text;
using System.Web;
using FSPortal;
using FSLibrary;
using FSNetwork;
using FSException;

namespace FSOpenForum
{
    public class OpenForum
    {
        private readonly Foro foro = new Foro();
        public DataTable aMessages;
        public int cOfSeperatorHeight;
        public int iAction;
        public int iMode;
        public int iMsgID;
        public int iOfAction;
        public int iOfDisplayMode;
        public int iOfEnd;
        public int iOfForumID;
        public int iOfMsgID;
        public int iOfPage;
        public int iOfStart;
        public int iOfTotalPages;
        public int iPageNum;
        public int iRes;

        public Foro ofo;


        public string ShowForum(int iForumID)
        {
            StringBuilder sb = new StringBuilder("");

            iOfAction = Web.RequestInt("ofact");

            sb.Append("\r\n" + "\r\n" + "<!-- Begin OpenForum -->" + "\r\n" + "\r\n");

            InitForum(iForumID);

            switch (iOfAction)
            {
                case Foro.ofActDisplay:
                    sb.Append(DisplayForum(iForumID));

                    break;
                case Foro.ofActFocus:
                    sb.Append(DisplayForum(iForumID));

                    break;
                case Foro.ofActNewMsg:
                case Foro.ofActReply:
                    sb.Append(DisplayPostForm(iForumID));

                    break;
                case Foro.ofActPost:
                    SavePost(iForumID);
                    sb.Append(DisplayForum(iForumID));

                    break;
            }


            sb.Append("\r\n" + "\r\n" + "<!-- End OpenForum -->" + "\r\n" + "\r\n");

            return sb.ToString();
        }


        public void SavePost(int iForumID)
        {
            int iParentID = 0;
            string sAuthor = null;
            string sTitle = null;
            string sBody = null;
			System.DateTime dDate = System.DateTime.MinValue;

            iParentID = Web.RequestInt("ofmsgid");

            sAuthor = Web.Request("txtUser");

            if (sAuthor == "")
            {
                sAuthor = Variables.User.Usuario;
            }


            if (sAuthor == "")
            {
                sAuthor = "(anónimo)";
            }

            sTitle = Web.Request("txtTitle");
            sBody = Web.Request("txtBody");
			dDate = System.DateTime.Now;

            if (!((sTitle == "" | sBody == "")))
            {
                foro.SaveMessage(iForumID, iParentID, sAuthor, sTitle, sBody, dDate);
            }

            InitForum(iForumID);
        }


        public string DisplayForumHeader()
        {
            StringBuilder sb = new StringBuilder("");

            sb.Append("\r\n" + @"<a name=""openforum""></a>");
            sb.Append("\r\n" + @"<table border=""0"" bgcolor=""" + Foro.cForumHeaderBgColor + @""" cellspacing=""" +
                      Foro.cForumCellSpacing + @""" cellpadding=""" + Foro.cForumCellPadding + @""" width=""100%"">");
            sb.Append("\r\n" + "<tr><td>");
            sb.Append(Foro.ofStrForum + ": <strong>" + foro.ForumTitle + "</strong> (" +
                      String.Format(Foro.ofStrMessageCount, foro.MessageCount) + ")");
            sb.Append("\r\n" + "</td>");
            sb.Append("\r\n" + "</tr><tr>");
            sb.Append("\r\n" + @"<td> <img border=""0"" src='" + Variables.App.directorioPortal +
                      @"imagenes/bullet.gif' alt='' /> <a href=""" + GetNewMsgURL() + @""">" + Foro.ofStrSendNew +
                      "</a></td>");
            sb.Append("\r\n" + "</tr><tr>");
            sb.Append("\r\n" + @"<td align=""right"">Modo vista: ");
            sb.Append("\r\n" + @"<a href=""" + GetModeURL(Foro.ofModeNormal) + @""">" + Foro.ofStrModeNormal + "</a> | ");
            sb.Append("\r\n" + @"<a href=""" + GetModeURL(Foro.ofModePreview) + @""">" + Foro.ofStrModePreview +
                      "</a> | ");
            sb.Append("\r\n" + @"<a href=""" + GetModeURL(Foro.ofModeOnlyTitles) + @""">" + Foro.ofStrModeTitles +
                      "</a> | ");
            sb.Append("\r\n" + @"<a href=""" + GetRefreshURL() + @""">" + Foro.ofStrRefresh + "</a> ");
            sb.Append("\r\n" + "</td></tr>");
            sb.Append("\r\n" + "</table>");

            return sb.ToString();
        }


        public string DisplayForumFooter()
        {
            return "";
        }


        public string DisplayPostForm(int iForumID)
        {
            StringBuilder sb = new StringBuilder("");
            int iRepliedMsgID = 0;
            string sMsgTitle = "";
            string sMsgBody = "";
            string sMsgAuthor = "";
            string sMsgDate = "";
            string sTitle = "";
            string sBody = "";
            string sAuthor = "";

            sb.Append(DisplayForumHeader());

            if (iRes == Foro.ofSuccess)
            {
                iRepliedMsgID = Web.RequestInt("ofmsgid");
                iRepliedMsgID = NumberUtils.NumberInt(Convert.ToInt64(iRepliedMsgID));

                if (foro.GetMessage(iRepliedMsgID, ref sMsgTitle, ref sMsgBody, ref sMsgAuthor, ref sMsgDate) ==
                    Foro.ofSuccess)
                {
                    sTitle = Foro.ofStrRE + sMsgTitle;
                    sBody = "\r\n" + "\r\n" + "\r\n" + Foro.ofStrOriginalMsg + "\r\n" + sMsgBody;
                }

                sb.Append("\r\n" + @"<form action=""" + GetPostURL() + @""" method=""post"">");

                sb.Append("\r\n" + @"<table border=""0""><tr>");
                sb.Append("\r\n" + @"<td class=""cabemaspeque"">" + Foro.ofStrPostTitle + "</td>");
                sb.Append("\r\n" + @"<td><input type=""text"" name=""txtTitle"" class=""textboxplano"" value=""" +
                          sTitle + @""" size=""50""></td>");
                sb.Append("\r\n" + "</tr><tr>");
                sb.Append("\r\n" + @"<td class=""cabemaspeque"">" + Foro.ofStrPostBody + "</td>");
                sb.Append("\r\n" + @"<td><textarea cols=""50"" rows=""10"" name=""txtBody"" class=""textboxplano"">" +
                          sBody + "</textarea></td>");
                sb.Append("\r\n" + "</tr>");

                if (sAuthor == "")
                {
                    sAuthor = Variables.User.Usuario;
                }


                if (sAuthor == "")
                {
                    sb.Append("\r\n" + @"<tr><td class=""cabemaspeque"">" + Foro.ofStrUser + "</td>");
                    sb.Append("\r\n" + @"<td><input type=""text"" name=""txtUser"" class=""textboxplano"" value=""" +
                              sAuthor + @""" size=""50""></td></tr>");
                }

                sb.Append("\r\n" + "<tr><td>&nbsp;</td>");
                sb.Append("\r\n" + @"<td><input type=""submit"" name=""cmdPost"" value=""" + Foro.ofStrPost +
                          @""" class=""botonplano""></td>");
                sb.Append("\r\n" + "</tr></table>");

                sb.Append("\r\n" + "</form>");
            }

            sb.Append(DisplayForumFooter());

            return sb.ToString();
        }


        public void InitForum(int iForumID)
        {
            iOfPage = Web.RequestInt("ofpage");
            iOfDisplayMode = Web.RequestInt("ofdisp");
            iOfMsgID = Web.RequestInt("ofmsgid");

            if (iOfDisplayMode == 0)
            {
                iOfDisplayMode = Foro.ofModeOnlyTitles;
            }

            if (iOfPage == 0)
            {
                iOfPage = 1;
            }

            int iRes = 0;
            iRes = foro.OpenForum(iForumID);

            if (iRes != Foro.ofSuccess)
            {
                switch (iRes)
                {
                    case Foro.ofErrNoSuchForum:
                        throw new ExceptionUtil(String.Format(Foro.ofStrErrNoSuchForum, iForumID));
                    case Foro.ofErrNoMessages:
					throw new ExceptionUtil(String.Format(Foro.ofStrErrNoMessages, foro.ForumTitle));
                    default:
					throw new ExceptionUtil(Foro.ofStrErrUnknown);
                }
            }
        }


        public string DisplayForum(int iForumID)
        {
            StringBuilder sb = new StringBuilder("");

            try
            {
                sb.Append(DisplayForumHeader());

                foro.PageSize = Foro.cPageSize;
                foro.CurrentPage = iOfPage;

                aMessages = foro.GetMessages();

                iOfPage = foro.CurrentPage;
                iOfTotalPages = foro.TotalPages;

                iOfStart = foro.NumMsgStart;
                iOfEnd = foro.NumMsgEnd;

                sb.Append(DisplayNavBar());

                sb.Append(DisplayMessages());

                sb.Append(DisplayForumFooter());
            }
            catch (System.Exception e)
            {
                sb.Append(e.Message);
            }

            return sb.ToString();
        }


        public string DisplayNavBar()
        {
            StringBuilder sb = new StringBuilder("");
            string sFirst = null;
            string sPrev = null;
            string sNext = null;
            string sLast = null;
            string sNavBarHeader = null;
            string sOfNavBar = null;
            string sNavBarFooter = null;

            string sPagFirst = @"<a href=""" + GetPagingURL(1) + @""">" + Foro.ofStrFirst + "</a>";
            sFirst = ((iOfPage != 1) ? sPagFirst : Foro.ofStrFirst);
            string sPagPrev = @"<a href=""" + GetPagingURL(iOfPage - 1) + @""">" + Foro.ofStrPrev + "</a>";
            sPrev = ((iOfPage > 1) ? sPagPrev : Foro.ofStrPrev);
            string sPagNext = @"<a href=""" + GetPagingURL(iOfPage + 1) + @""">" + Foro.ofStrNext + "</a>";
            sNext = ((iOfPage < iOfTotalPages) ? sPagNext : Foro.ofStrNext);
            string sPagLast = @"<a href=""" + GetPagingURL(iOfTotalPages) + @""">" + Foro.ofStrLast + "</a>";
            sLast = ((iOfPage != iOfTotalPages) ? sPagLast : Foro.ofStrLast);
            sNavBarHeader = @"<table border=""0"" width=""100%"" bgcolor=""" + Foro.cNavBarBgColor + @"""><tr>";
            sNavBarFooter = "</tr></table>";
            sOfNavBar = @"<td align=""right"">" + sFirst + " | " + sPrev + " | " + sNext + " | " + sLast + " (" +
                        iOfPage + "/" + iOfTotalPages + ")</td>";
            sb.Append(sNavBarHeader + sOfNavBar + sNavBarFooter);
            sb.Append("\r\n" + @"<img width=""1"" height=""" + cOfSeperatorHeight + @""">");

            return sb.ToString();
        }


        public string DisplayMessages()
        {
            StringBuilder sb = new StringBuilder("");
            int i = 0;
            sb.Append("\r\n" + @"<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""" +
                      Foro.cForumBgColor + @""">");
            for (i = iOfStart; i < iOfEnd; i++)
            {
                sb.Append(DisplayMessage(i));
            }
            sb.Append("\r\n" + "</table>");

            return sb.ToString();
        }


        public string DisplayMessage(int i)
        {
            StringBuilder sb = new StringBuilder("");
            int iIndent = 0;
            string sTitle = null;
            string sSubject = null;
            string sAuthor = null;
            string ssDate = null;
            string sTitleClass = null;
            string sIndentStyle = null;
            bool bDisplaySubject = false;
            string sThreeDots = null;

            iIndent = NumberUtils.NumberInt(aMessages.Rows[i][Foro.ofFldIndent]);
            iMsgID = NumberUtils.NumberInt(aMessages.Rows[i][Foro.ofFldMsgID]);
            sTitle = Functions.Valor(aMessages.Rows[i][Foro.ofFldTitle]);
            sSubject = Functions.Valor(aMessages.Rows[i][Foro.ofFldSubject]);
            sAuthor = Functions.Valor(aMessages.Rows[i][Foro.ofFldAuthor]);
            ssDate = Convert.ToDateTime(aMessages.Rows[i][Foro.ofFldDate]).ToString();

            if (sSubject != "")
            {
                sSubject = HttpUtility.HtmlEncode(sSubject);
                sSubject = TextUtil.Replace(sSubject, "\r\n", Ui.Lf());
            }

            sTitleClass = (iIndent > 0 ? "tn" : "tb");

            sIndentStyle = "padding-left:" + (iIndent*20) + 3 + ";";

            switch (iOfDisplayMode)
            {
                case Foro.ofModeNormal:
                    bDisplaySubject = true;
                    break;
                case Foro.ofModePreview:
                    bDisplaySubject = true;
                    if (iMsgID != iOfMsgID)
                    {
                        sThreeDots = @" ... <a href=""" + GetFocusOnURL() + @""">(more)</a>";
                        string sSummary = TextUtil.Left(sSubject, Foro.cSummaryLen) + sThreeDots;
                        sSubject = (TextUtil.Length(sSubject) > Foro.cSummaryLen ? sSummary : sSubject);
                    }
                    break;
                case Foro.ofModeOnlyTitles:
                    bDisplaySubject = false;
                    if (iMsgID == iOfMsgID)
                    {
                        bDisplaySubject = true;
                    }
                    break;
            }


            string sTitleBgColor = null;
            string sSubjectBgColor = null;
            string sFocusContents = null;

            if (iMsgID == iOfMsgID)
            {
                sTitleBgColor = Foro.cTitleFocusBgColor;
                sSubjectBgColor = Foro.cSubjectFocusBgColor;
                sFocusContents = @"<table border=""0"" cellpadding=""1"" cellspacing=""1"" width=""100%"" bgcolor=""" +
                                 sTitleBgColor + @"""><tr>" + @"<td><a href=""" + GetReplyURL() + @""">" +
                                 Foro.ofStrReply + "</a></td>" + @"<td align=""right"">...</td>" + "</tr></table>";
            }
            else
            {
                sTitleBgColor = Foro.cTitleBgColor;
                sSubjectBgColor = Foro.cSubjectBgColor;
                sFocusContents = "";
            }

            string sRowSpan = null;
            string sMsg = null;

            if (bDisplaySubject)
            {
                sRowSpan = @"rowspan=""2""";
            }
            else
            {
                sRowSpan = "";
            }
            sMsg = @"<table border=""0"" cellspacing=""" + Foro.cForumCellSpacing + @""" cellpadding=""" +
                   Foro.cForumCellPadding + @""" width=""100%"" bgcolor=""" + Foro.cForumBgColor + @""">" + "<tr>" +
                   "<td " + sRowSpan + @" width=""10"" valign=""top"">" + @"<img src=""" + Variables.App.directorioPortal +
                   @"imagenes/bullet.gif""/>" + "</td>" + "<td>" + @"<table border=""0"" bgcolor=""" + sTitleBgColor +
                   @""" width=""100%"" cellspacing=""" + Foro.cForumCellSpacing
                   + @""" cellpadding=""" + Foro.cForumCellPadding + @"""><tr>" + @"<td class=""" + sTitleClass + @""">" +
                   Ui.EditPage("MensajesForo", "MsgID", iMsgID.ToString()) + @"<a class=""" + sTitleClass +
                   @""" href=""" + GetFocusOnURL() + @""">" + sTitle + "</a>" + "</td>" +
                   @"<td width=""150"" class=""tdauthor"" align=""left"">" + sAuthor + "</td>" +
                   @"<td width=""80"" class=""tddate"" align=""right"">" + ssDate + "</td>" + "</tr></table>" + "</td>" +
                   "</tr>";
            if (bDisplaySubject)
            {
                sMsg = sMsg + "<tr>" + @"<td colspan=""3"" bgcolor=""" + sSubjectBgColor + @""">" + sSubject + Ui.Lf() +
                       sFocusContents + "</td>" + "</tr>";
            }
            sMsg = sMsg + "</table>";

            sb.Append("\r\n" + "<tr>");
            sb.Append("\r\n" + @"<td style=""" + sIndentStyle + @""">" + sMsg + "</td>");
            sb.Append("\r\n" + "</tr>");

            return sb.ToString();
        }


        public Array GetQueryArray()
        {
            Array getQueryArrayReturn = null;
            Array aQuery;
            string sQueryString = null;
            sQueryString = HttpContext.Current.Request.QueryString.ToString();
            aQuery = sQueryString.Split('&');
            getQueryArrayReturn = aQuery;
            return getQueryArrayReturn;
        }


        public string GetNonOFQueryString()
        {
            Array aQuery;
            string sBuffer = "";
            int i = 0;
            string sConChar = null;
            string sScriptName = null;
            string sEndChar = null;

            aQuery = GetQueryArray();
            for (i = 0; i < aQuery.Length; i++)
            {
                switch (GetKeyPart(aQuery.GetValue(i).ToString()))
                {
                    case "ofid":
                    case "ofact":
                    case "ofdisp":
                    case "ofmsgid":
                    case "ofpage":
                    case "ofrand":
                        break;
                    default:
                        sConChar = (sBuffer == "" ? "?" : "&");
                        sBuffer = sBuffer + sConChar + aQuery.GetValue(i);
                        break;
                }
            }
            sScriptName = (TextUtil.Length(HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]) > 0
                ? HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"]
                : "default.aspx");
            sEndChar = (TextUtil.Length(sBuffer) > 0 ? "&" : "?");
            return sScriptName + sBuffer + sEndChar;
        }


        public string GetKeyPart(string sPair)
        {
            string getKeyPartReturn = null;
            int iPos = 0;
            string sKeyPart = null;
            iPos = sPair.IndexOf("=") + 1;
            if (iPos > 0)
            {
                sKeyPart = TextUtil.Substring(sPair, 0, iPos - 1);
            }
            else
            {
                sKeyPart = sPair;
            }
            getKeyPartReturn = sKeyPart;
            return getKeyPartReturn;
        }


        public string GetPagingURL(int iPageNum)
        {
            string sThis = null;
            sThis = GetNonOFQueryString();
            return sThis + GetQueryParameters(iAction, iPageNum);
        }


        public string GetReplyURL()
        {
            return GetURL(Foro.ofActReply);
        }


        public string GetNewMsgURL()
        {
            string getNewMsgURLReturn = null;
            string sThis = null;
            Random r = new Random();
            sThis = GetNonOFQueryString();
            getNewMsgURLReturn = sThis + "ofact=" + Foro.ofActNewMsg + "&amp;ofmsgid=0&amp;ofdisp=" + iMode +
                                 "&amp;ofpage=" + iPageNum + "&amp;ofrand=" + r.Next() + "#openforum";
            return getNewMsgURLReturn;
        }


        public string GetFocusOnURL()
        {
            return GetURL(Foro.ofActFocus);
        }


        public string GetRefreshURL()
        {
            return GetURL(Foro.ofActDisplay);
        }


        public string GetModeURL(int iMode)
        {
            string sThis = null;
            Random r = new Random();
            sThis = GetNonOFQueryString();
            return sThis + "ofact=0&amp;ofmsgid=" + iMsgID + "&amp;ofdisp=" + iMode + "&amp;ofpage=" + iPageNum +
                   "&amp;ofrand=" + r.Next() + "#openforum";
        }


        public string GetPostURL()
        {
            string sThis = null;
            Random r = new Random();
            sThis = GetNonOFQueryString();
            return sThis + "ofact=" + Foro.ofActPost + "&amp;ofmsgid=" + iOfMsgID + "&amp;ofdisp=" + iMode +
                   "&amp;ofpage=" + iPageNum + "&amp;ofrand=" + r.Next() + "#openforum";
        }


        public string GetURL(int iAction)
        {
            string sThis = null;
            sThis = GetNonOFQueryString();
            return sThis + GetQueryParameters(iAction, iOfPage);
        }


        public string GetQueryParameters(int iAction, int iPageNum)
        {
            Random r = new Random();
            return "ofact=" + iAction + "&amp;ofmsgid=" + iMsgID + "&amp;ofdisp=" + iOfDisplayMode + "&amp;ofpage=" +
                   iPageNum + "&amp;ofrand=" + r.Next() + "#openforum";
        }
    }
}