

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_hash1way.aspx" -->
<%
Response.Buffer = True

'Dimension variables
Dim strclave			'Holds the forum clave
Dim blnAutoLogin		'Holds whether the user wnats to be automactically logged in
Dim intForumID			'Holds the forum ID
Dim strForumCode		'Holds the users ID code
Dim strReturnPage		'Holds the page to return to
Dim strReturnPageProperties	'Holds the properties of the return page


'Get the forum page to return to
Select Case Request.QueryString("RP")
	'Read in the thread and forum to return to
	Case "PT"
		strReturnPage = "forum_posts.aspx"
		strReturnPageProperties = "?RP=PT&amp;tID=" & CLng(Request.QueryString("TID")) & "&amp;fID=" & CInt(Request.QueryString("FID"))

	'Else return to the forum main page
	Case Else
		'Read in the forum and topic to return to
		strReturnPage = "forum_topics.aspx"
		strReturnPageProperties = "?RP=TC&amp;fID=" & CInt(Request.QueryString("FID"))
End Select


'Read in the forum id number
portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))

'Read in the users details from the form
strclave = LCase(Trim(Mid(Request.Form("clave"), 1, 15)))
blnAutoLogin = CBool(Request.Form("AutoLogin"))

'If user has eneterd a clave make sure it is correct
If NOT strclave = "" Then

	'Read in the forum name from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.clave, " & portal.variablesForum.strDbTable & "Forum.Forum_code FROM " & portal.variablesForum.strDbTable & "Forum WHERE Forum_ID = " & portal.variablesForum.intForumID

	'Query the database
	rsCommon=db.execute(strSQL)


	'If the query has returned a value to the recordset then check the clave is correct
	If NOT rsCommon.EOF Then

		'Encrypt the entered clave
		strclave = HashEncode(strclave)

		'Check the clave is correct, if it is get the user ID and set a cookie
		If strclave = rsCommon("clave") Then

			'Read in the users ID number and whether they want to be automactically logged in when they return to the forum
			strForumCode = rsCommon("Forum_code")

			'Write a cookie with the Forum ID number so the user logged in throughout the forum
			'Write the cookie with the name Forum containing the value Forum Code number
			Response.Cookies(portal.variables.strCookieName)("Forum" & portal.variablesForum.intForumID) = strForumCode

			'If the user has selected to be remebered when they next login then set the expiry date for the cookie for 1 year
			If blnAutoLogin = True Then

				'Set the expiry date for 1 year (365 days)
				'If no expiry date is set the cookie is deleted from the users system 20 minutes after they leave the forum
				Response.Cookies(portal.variables.strCookieName).Expires = Now() + 365
			End If

			'Reset Server Objects
			rsCommon.Close
			Set rsCommon = Nothing
			adoCon.Close
			Set adoCon = Nothing


			'Redirect the user back to the forum page they have just come from
			Response.Redirect strReturnPage & strReturnPageProperties
		End If
	End If

	'Clean up
	rsCommon.Close
End If
%>
<html>
<head>

<title>Forum Login</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a clave
	if (document.frmLogin.clave.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorEnterclave %>";
	}

	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}

	return true;
}

// -->
</script>
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
  <tr>
  <td align="left" class="heading"><% = portal.variablesForum.strTxtLoginUser %></td>
</tr>
 <tr>
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtForumLogin %><br /></td>
  </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" class="bold"><br /><% = portal.variablesForum.strTxtclaveRequiredForForum %></td>
  </tr>
</table>
<br /><%


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'If the user has unsuccesfully tried logging in before then display a clave incorrect error
If strclave <> "" Then
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td align="center" class="error"><% = portal.variablesForum.strTxtForumclaveIncorrect %><br />
      <% = portal.variablesForum.strTxtPleaseTryAgain %></td>
  </tr>
</table><br /><%

End If
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr><form method="post" name="frmLogin" action="forum_clave_form.aspx<% = strReturnPageProperties %>" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr bgcolor="<% = portal.variablesForum.strTableTitleColour %>">
      <td colspan="2" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtLoginUser %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td colspan="2" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%"  bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtclave %>*</td>
         <td width="50%" valign="top" background="<% = portal.variablesForum.strTableBgImage %>"><input type="clave" name="clave" id="clave" size="15" maxlength="15" value="<% = strclave %>" />
     </td>
     </tr>   
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
         <td width="50%" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtAutoLogin %></td>
         <td width="50%" valign="top" class="text" background="<% = portal.variablesForum.strTableBgImage %>"><% = portal.variablesForum.strTxtYes %><input type="radio" name="AutoLogin" value="true" checked />&nbsp;&nbsp;<% = portal.variablesForum.strTxtNo %><input type="radio" name="AutoLogin" value="false" /></td>
     </tr>
     <tr bgcolor="<% = strTableBottomRowColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
       <td valign="top" height="2" colspan="2" align="center" background="<% = portal.variablesForum.strTableBgImage %>">
        <input type="hidden" name="sessionID" value="<% = Session.SessionID %>" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtLoginToForum %>" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
      </td>
     </tr>
    </table>
      </td>
    </tr>
  </table>
  </td>
 </form></tr>
</table>
<br /><br />
<script>document.frmLogin.clave.focus()</script>
<div align="center">
<%


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

