

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim intForumID		'Holds the forum ID number
Dim blnLockedStatus	'Holds the lock status of the topic
Dim strForumName	'Holds the name of the forum
Dim strForumDescription	'Holds the description of the forum


'Read in the forum ID number
portal.variablesForum.intForumID = CLng(Request("FID"))


'If the person is not an admin then send them away
If portal.variablesForum.intForumID = "" OR  portal.variablesForum.blnAdmin = False Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If



'Initliase the SQL query to get the topic details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereForumIs @portal.variablesForum.intForumID = " & portal.variablesForum.intForumID
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE Forum_ID = " & portal.variablesForum.intForumID & ";"
End If


'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	strForumName = rsCommon("Forum_name")
	strForumDescription = rsCommon("Forum_description")
	blnLockedStatus = CBool(rsCommon("Locked"))
	
End If


'Call the moderator function and see if the user is a moderator
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)




'If this is a post back then  update the database
If (Request.Form("postBack")) AND (portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator) Then

	strForumName = Trim(Mid(Request.Form("forumName"), 1, 70))
	strForumDescription = Trim(Mid(Request.Form("description"), 1, 190))
	blnLockedStatus = CBool(Request.Form("locked"))

	'Update the recordset
	With rsCommon

		'Update	the recorset
		.Fields("Forum_name") = strForumName
		.Fields("Forum_description") = strForumDescription
		.Fields("Locked") = blnLockedStatus

		'Update db
		.Update

		'Re-run the query as access needs time to catch up
		.ReQuery

	End With

End If

%>
<html>
<head>

<title>Forum Admin</title>

<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a forum name
	if (document.frmForumAdmin.forumName.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorForumName %>";
	}
	
	//Check for a description
	if (document.frmForumAdmin.description.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorForumDescription %>";
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
</script>

<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtForumAdmin %></div>
    <br /><%

'If there is no forum info returned by the rs then display an error message
If rsCommon.EOF OR (portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False) OR bannedIP() OR blnActiveMember = False Then

	'Close the rs
	rsCommon.Close

	Response.Write("<div align=""center"">")
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtForumNotFoundOrAccessDenied & "</span><br /><br /><br />")
	Response.Write("</div>")

'Else display a form to allow updating of the topic
Else

	'Close the rs
	rsCommon.Close
%>
<form name="frmForumAdmin" method="post" action="pop_up_forum_admin.aspx" onSubmit="return CheckForm();" onReset="return confirm('<% = strResetFormConfirm %>');">
 <table width="350" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="350">
     <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>">
      <td colspan="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="text">*<% = portal.variablesForum.strTxtRequiredFields %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtForumName %>*:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="forumName" maxlength="70" size="30" value="<% = strForumName %>"/></td>
     </tr>
     
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtForumDiscription %>*:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="description" maxlength="190" size="30" value="<% = strForumDescription %>" /></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtForumLocked %>:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='checkbox' name="locked" value="true" <% If blnLockedStatus = True Then Response.Write(" checked") %> /></td>
     </tr>     
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" colspan="2" align="center"><a href="resync_post_count.aspx?FID=<% = portal.variablesForum.intForumID %>" target="_self"><% = portal.variablesForum.strTxtResyncTopicPostCount %></a></td>
     </tr>     
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" align="center" />
      <td valign="top" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>" />
        <input type="hidden" name="FID" value="<% = portal.variablesForum.intForumID %>" />
        <input type="hidden" name="postBack" value="true" />
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtUpdateForum %>" />
        <input type="reset" name="Reset" value="<% = portal.variablesForum.strTxtResetForm %>" />
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
</form><%

End If

%>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
        <a href="JavaScript:onClick=window.opener.location.href = window.opener.location.href; window.close();"><% = portal.variablesForum.strTxtCloseWindow %></a>
      </td>
    </tr>
  </table>
<br /><br />
<div align="center">
<%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


%>
</div>
</body>
</html>