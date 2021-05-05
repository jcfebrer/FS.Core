

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting and setting a cookie
Response.Buffer = True

Dim intForumID
Dim strMode

'read in the mode of the page
strMode = Request.QueryString("TP")

%>
<html>
<head>

<title>Registration Confirmation</title>


     	

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% If strMode = "UPD" OR strMode = "DEL" Then Response.Write(portal.variablesForum.strTxtEditProfile) Else Response.Write(portal.variablesForum.strTxtRegisterNewUser) %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% If strMode = "UPD" OR strMode = "DEL" Then Response.Write(portal.variablesForum.strTxtEditProfile) Else Response.Write(portal.variablesForum.strTxtRegisterNewUser) %><br /></td>
  <%
'Release server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%></tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="center">
  <br /><br />
  <span class="text"><%

'If this is a re-activation then tell the member
If strMode = "REACT" Then

	Response.Write(portal.variablesForum.strTxtYourEmailAddressHasBeenChanged)

'Else member updating profile
ElseIf strMode = "UPD" Then

	Response.Write("<span class=""bold"">" & portal.variablesForum.strTxtYourProfileHasBeenUpdated & "</span>" & _
	vbCrLf & "<br /><br />" & portal.variablesForum.strTxtYouCanAccessCP & "<a href=""member_control_panel.aspx"" target=""_self"">" & portal.variablesForum.strTxtMemberCPMenu & "</a>") 

'Else if the admin has deleted the member disply the delete msg
ElseIf strMode = "DEL" Then

	Response.Write(portal.variablesForum.strTxtTheMemberHasBeenDleted)

'Else welcome the new member
ElseIf strMode = "ACT" Then
	
	Response.Write("<span class=""bold"">" & portal.variablesForum.strTxtThankYouForRegistering & " " & strMainForumName & "</span>")

'Else welcome new user
Else

	Response.Write("<span class=""bold"">" & portal.variablesForum.strTxtThankYouForRegistering & " " & strMainForumName & "</span>" & _
	vbCrLf & "<br /><br />" & portal.variablesForum.strTxtYouCanAccessCP & "<a href=""member_control_panel.aspx"" target=""_self"">" & portal.variablesForum.strTxtMemberCPMenu & "</a>") 
	

End If 


'If this is a re-activation then tell the member
If strMode = "REACT" Then

 	Response.Write(vbCrLf & "<br /><br /><span class=""lgText"">" & portal.variablesForum.strTxtYouShouldReceiveAReactivateEmail & "</span>")

'Else welcome the new member
ElseIf strMode = "ACT" Then

 	Response.Write(vbCrLf & "<br /><br /><span class=""lgText"">" & portal.variablesForum.strTxtYouShouldReceiveAnEmail & "</span>")
End If
%>
<br /><br /><a href="forum_topics.aspx?FID=<% = portal.variablesForum.intForumID %>" target="_self"><% = portal.variablesForum.strTxtReturnToDiscussionForum %></a><br /><br />
<%

'If this person needs to activate the account
If strMode = "REACT" OR strMode = "ACT" Then
 %>
 <br /><br />
 <% = portal.variablesForum.strTxtIfErrorActvatingMembership & " " & strMainForumName & " " & " <a href=""mailto:" & strForumEmailAddress & """>" & portal.variablesForum.strTxtForumAdministrator & "</a>." %><br /><%
End If
%>
<br />
</span></div>
<br /><br />
   </td>
  </tr>
</table>
<div align="center">
<%


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

