
<% response.redirect portal.variables.directorioPortal & "../servicios/conectar.aspx"
%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Response.Buffer = True 


'Make sure this page is not cached
Response.Expires = -1
Response.ExpiresAbsolute = Now() - 2
Response.AddHeader "pragma","no-cache"
Response.AddHeader "cache-control","private"
Response.CacheControl = "No-Store"


'Dimension variables
Dim intForumID
Dim strReturnPage
Dim blnLoggedInOK
Dim strReferer			'Holds the page to return to


'Initilise variables
blnLoggedInOK = True

'read in the forum ID number
If isNumeric(Request.QueryString("FID")) Then
	portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
Else
	portal.variablesForum.intForumID = 0
End If


'Logged in cookie test
If strLoggedInUserCode = "" Then blnLoggedInOK = False
If InStr(1, strLoggedInUserCode, "LOGGED-OFF", 1) Then blnLoggedInOK = False



'Get the forum page to return to
If blnLoggedInOK = False Then
	strReturnPage = "login_user.aspx?" & removeAllTags(Request.QueryString)
ElseIf Request.QueryString("M") = "Unsubscribe" Then
	strReturnPage = "email_notify.aspx?" & removeAllTags(Request.QueryString)
'Redirect the user back to the forum they have just come from
ElseIf portal.variablesForum.intForumID > 0 Then
	strReturnPage = "forum_topics.aspx?" & removeAllTags(Request.QueryString)
'Return to forum homepage
Else
	strReturnPage = "default.aspx"
End If

%>  
<html>
<head>

<title>Login User</title>

<%
If blnLoggedInOK AND blnActiveMember = True Then Response.Write("<meta http-equiv=""refresh"" content=""3;URL=" & strReturnPage & """>")	
%>
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtLoginUser %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtLoginUser %><br /></td>
  </tr>
</table>
<div align="center">
  <br /><br />
 <span class="lgText"><% 
'Display heading text
If blnLoggedInOK = False Then Response.Write(portal.variablesForum.strTxtUn) 
 
Response.Write(portal.variablesForum.strTxtSuccessfulLogin) %></span><br />
  <br /><%
  
  
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing



'If the account has not been activated have a link to resend activation email	
If InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) = False AND portal.variablesForum.blnEmailActivation AND portal.variablesForum.blnLoggedInUserEmail AND blnActiveMember = False Then

	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtForumMembershipNotAct & "</span><br /><br /><span class=""text"">" & portal.variablesForum.strTxtToActivateYourForumMem & "<br /><br /><a href=""JavaScript:openWin('resend_email_activation.aspx','actMail','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200')"">" & portal.variablesForum.strTxtResendActivationEmail & "</a><br /><br /><br /><a href=""" & strReturnPage & """ target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a></span>")

'If the member is suspened then tell them so
ElseIf InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) AND blnActiveMember = False Then

	Response.Write("<span class=""text"">" & portal.variablesForum.strTxtForumMemberSuspended & "<br /><br /><a href=""" & strReturnPage & """ target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a></span>")

'If this is a successful login then display some text
ElseIf blnLoggedInOK = True Then
	
	Response.Write("<span class=""text"">" & portal.variablesForum.strTxtSuccessfulLoginReturnToForum & "<br /><br /><a href=""" & strReturnPage & """ target=""_self"">" & portal.variablesForum.strTxtReturnToDiscussionForum & "</a></span>")


'Display that the login was not successful
Else
	Response.Write("<span class=""text"">" & portal.variablesForum.strTxtUnSuccessfulLoginText & "</span>")
	Response.Write vbCrLf & "<br /><br /><a href=""login_user.aspx?" &  removeAllTags(Request.QueryString) & """ target=""_self"">" & portal.variablesForum.strTxtUnSuccessfulLoginReTry & "</a>"
End If

%>
<br /><br /><br /><br /><br /><br />
</div>
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div> 

