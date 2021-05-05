

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
Dim strReturnPage		'Holds the page to return to 
Dim strReturnPageProperties	'Holds the properties of the return page
Dim intForumID


'Get the forum page to return to
Select Case Request.QueryString("RP")
	'Read in the search to return to
	Case "Search"
		strReturnPage = "search.aspx"
		strReturnPageProperties = "?RP=" & Trim(Mid(Request.Form("RP"), 1, 8)) & "&amp;sPN=" & CInt(Request.QueryString("SPN")) & "&amp;search=" & Server.URLEncode(Request.QueryString("search")) & "&amp;searchMode=" & Trim(Mid(Request.Form("searchMode"), 1, 3)) & "&amp;searchIn=" & Trim(Mid(Request.Form("searchIn"), 1, 3)) & "&amp;forum=" & CInt(Request.QueryString("forum")) & "&amp;searchSort=" & Trim(Mid(Request.Form("searchSort"), 1, 3))
	
	'Read in the active topic page to return to
	Case "Active"
		strReturnPage = "active_topics.aspx"
		strReturnPageProperties = "?PN=" & CInt(Request.QueryString("PN"))
	
	'Else return to the forum main page
	Case Else
		strReturnPage = "default.aspx"
		strReturnPageProperties = "?FID=0"
End Select


%>  
<html>
<head>

<title>Access Denied</title>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtAccessDenied %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtAccessDenied %><br /></td>
  </tr>
</table>
<div align="center">
  <br /><br /><br />
 <span class="lgText"><% = portal.variablesForum.strTxtInsufficientPermison %></span><br />
  <br />
  <%
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'If this is a banned IP then display an error message
If Request.QueryString("M") = "IP" Then
	
	Response.Write("<span class=""text"">" & portal.variablesForum.strTxtSorryFunctionNotPermiitedIPBanned & "</span>")

'If the session ID's don't match then make sure the user has cookies enabled on there system
ElseIf Request.QueryString("M") = "sID" Then

	Response.Write("<span class=""text"">" & portal.variablesForum.strTxtSessionIDErrorCheckCookiesAreEnabled & "</span>")

'If the users account is suspended then let them know
ElseIf Request.QueryString("M") = "ACT" AND blnActiveMember = False Then
		
	Response.Write (vbCrLf & "<br /><span class=""text"">")
	
	'If mem suspended display message
	If  InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) Then
		Response.Write(portal.variablesForum.strTxtForumMemberSuspended)
	'Else account not yet active
	Else
		Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtForumMembershipNotAct & "</span><br /><br />" & portal.variablesForum.strTxtToActivateYourForumMem)
	End If
	'If email is on then place a re-send activation email link
	If InStr(1, strLoggedInUserCode, "N0act", vbTextCompare) = False AND portal.variablesForum.blnEmailActivation AND portal.variablesForum.blnLoggedInUserEmail Then Response.Write("<br /><br /><a href=""JavaScript:openWin('resend_email_activation.aspx','actMail','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=475,height=200')"">" & portal.variablesForum.strTxtResendActivationEmail & "</a>")
	
	Response.Write("</span><br /><br />")
	
'Display a login or registration button
ElseIf portal.variablesForum.intGroupID = 2 Then
	Response.Write vbCrLf & "	<a href=""registration_rules.aspx" & strReturnPageProperties & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "register.gif""  alt=""" & portal.variablesForum.strTxtRegister & """ border=""0"" align=""middle"" /></a>&nbsp;&nbsp;<a href=""login_user.aspx" &  strReturnPageProperties & """ target=""_self""><img src=""" & portal.variablesForum.strImagePath & "login.gif""  alt=""" & portal.variablesForum.strTxtLogin & """ border=""0"" align=""middle"" /></a><br />"
End If

%>
<br />
<br />
<br />
</div>
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div> 

