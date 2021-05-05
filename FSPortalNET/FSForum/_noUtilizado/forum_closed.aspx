

<%

Response.Buffer = True 

'First we need to tell the common.aspx page to stop redirecting or we'll get in a bit of a loop
blnClosedForumPage = True

%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Dim intForumID



'If the forum is no-longer closed redirect to teh default page
If blnForumClosed = False Then
	
	'Reset server objects
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect to the forum closed page
	Response.Redirect("default.aspx")
End If
%>  
<html>
<head>

<title><% = strMainForumName %> Closed</title>

<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" /><%

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtForumClosed %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtForumClosed %><br /></td>
  </tr>
</table>
<div align="center">
  <br /><br /><br />
 <span class="heading"><% = portal.variablesForum.strTxtForumClosed %></span><br />
<br />
<span class="text"><% = portal.variablesForum.strTxtSorryTheForumsAreClosedForMaintenance %></span><br />
<br /><br />
<br /><br /><br />
</div>
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div> 

