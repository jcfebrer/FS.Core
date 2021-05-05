
<% response.redirect(portal.variables.paginaRegistro)
%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 


'Dimension variables
Dim rsForum			'Holds the recorset of the forums
Dim strReturnPage		'Holds the page to return to 
Dim strForumName 		'Holds the forum name
Dim intForumID 			'Holds the fourum ID

'read in the forum ID number
If isNumeric(Request.QueryString("FID")) Then
	portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
Else
	portal.variablesForum.intForumID = 0
End If


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")

End If



'If new registrations are suspended then display a message
If blnRegistrationSuspeneded Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("registration_suspended.aspx")

End If

%>
<html> 
<head>

<title>Register New User Rules and Policies</title>


     	

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
<form method="post" name="frmregister" action="register.aspx?FID=<% = Server.HTMLEncode(portal.variablesForum.intForumID) %>">
    <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
      <td align="center" height="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"> 
       <% = portal.variablesForum.strTxtForumRulesAndPolicies %>
      </td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"> 
      <td align="left"  height="2" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> 
       <!--#include file="language_files/registration_polices_file.aspx" -->
       <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
        <tr> 
         <td align="center"> 
          <input type="button" name="Button" value="<% = portal.variablesForum.strTxtCancel %>" onClick="window.open('default.aspx', '_self')" />
          <input type="hidden" name="Reg" value="OK" />
          <input type="hidden" name="mode" value="reg" />
          <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
          <input type='submit' name="Registration" value="<% = srtTxtAccept %>" />
         </td>
        </tr>
       </table>
      </td>
     </tr>
    </table>
      </td>
    </tr>
  </table>
  </td>
    </tr>
  </table>
  <br />
 <br />
</form>
<div align="center">
<% 

'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

