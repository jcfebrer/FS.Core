

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 


'Dimension variables
Dim rsForum			'Holds the recorset of the forums
Dim strReturnPage		'Holds the page to return to 
Dim strForumName 		'Holds the forum name
Dim intForumID 			'Holds the fourum ID

'Get the forum ID
portal.variablesForum.intForumID = Request.QueryString("FID")


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If

%>
<html> 
<head>

<title>Register New User Rules and Policies</title>

<!-- The Web Wiz Guide ASP forum is written by Bruce Corkhill ©2001
    	 If you want your forum then goto http://www.webwizforums.com --> 

<!-- #include file="includes/header.aspx" -->
   <navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtRegisterNewUser %><br /></td>
  </tr>
</table><%
'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<div align="center">
  <br /><br /><br />
  <span class="text"><% = portal.variablesForum.strTxtNewRegSuspendedCheckBackLater %></span><br />
 <br /><br /><br /><br /><br />
</div>
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

