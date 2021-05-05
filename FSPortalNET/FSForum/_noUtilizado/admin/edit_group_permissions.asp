

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim strForumName	'Holds the name of the forum
Dim strGroupName	'Holds the name of the forum group
Dim intUserGroupID	'Holds the ID number of the group
Dim intForumID		'Holds the forum ID number
Dim blnRead
Dim blnPost
Dim blnReply
Dim blnEdit
Dim blnDelete
Dim blnPriority
Dim blnPollCreate
Dim blnVote
Dim blnAttachments
Dim blnImageUpload
Dim blnModerateForum



'Read in the details
portal.variablesForum.intForumID = CInt(Request("FID"))
intUserGroupID = CInt(Request("GID"))



'Read in the forum name
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* From " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID=" & portal.variablesForum.intForumID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the forum name form the recordset
If NOT rsCommon.EOF Then

	'Read in the forums from the recordset
	strForumName = rsCommon("Forum_name")
End If

'Release server varaibles
rsCommon.Close



'Read in the user group name
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Name FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Group_ID=" & intUserGroupID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the forum name form the recordset
If NOT rsCommon.EOF Then

	'Read in the user group name from the recordset
	strGroupName = rsCommon("Name")
End If

'Release server varaibles
rsCommon.Close




'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.* From " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & intUserGroupID & ";"
	
'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3
	
'Query the database
rsCommon=db.execute(strSQL)

'If this is a post back update the database
If Request.Form("postBack") Then

	With rsCommon

		'Update the recordset
		.Fields("Forum_ID") = portal.variablesForum.intForumID
		.Fields("Group_ID") = intUserGroupID
		.Fields("Read") = CBool(Request.Form("read"))
		.Fields("Post") = CBool(Request.Form("post"))
		.Fields("Reply_posts") = CBool(Request.Form("reply"))
		.Fields("Edit_posts") = CBool(Request.Form("edit"))
		.Fields("Delete_posts") = CBool(Request.Form("delete"))
		.Fields("Priority_posts") = CBool(Request.Form("priority"))
		.Fields("Poll_create") = CBool(Request.Form("poll"))
		.Fields("Vote") = CBool(Request.Form("vote"))
		.Fields("Attachments") = CBool(Request.Form("files"))
		.Fields("Image_upload") = CBool(Request.Form("images"))
		.Fields("Moderate") = CBool(Request.Form("moderate"))

		'Update the database with the new user's details
		.Update
		
		'Read back in detials from the db
		.Requery
		
		'Release server varaibles
		rsCommon.Close
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing
	
		'Redirect to main permisions page
		Response.Redirect("forum_group_permissions.aspx?GID=" & intUserGroupID)
	End With

End If



'Read in the forum details from the recordset
If NOT rsCommon.EOF Then

	'Read in the forums from the recordset
	portal.variablesForum.blnRead = CBool(rsCommon("Read"))
	portal.variablesForum.blnPost = CBool(rsCommon("Post"))
	portal.variablesForum.blnReply = CBool(rsCommon("Reply_posts"))
	portal.variablesForum.blnEdit = CBool(rsCommon("Edit_posts"))
	portal.variablesForum.blnDelete = CBool(rsCommon("Delete_posts"))
	portal.variablesForum.blnPriority = CBool(rsCommon("Priority_posts"))
	portal.variablesForum.blnPollCreate = CBool(rsCommon("Poll_create"))
	portal.variablesForum.blnVote = CBool(rsCommon("Vote"))
	portal.variablesForum.blnAttachments = CBool(rsCommon("Attachments"))
	portal.variablesForum.blnImageUpload = CBool(rsCommon("Image_upload"))
	blnModerateForum = CBool(rsCommon("Moderate"))
End If


'Release server varaibles
rsCommon.Close

%>
<html>
<head>

<title>Edit Group Permissions</title>


     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Edit User Group Permissions for <% = strGroupName %></span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="group_perm_forum.aspx" target="_self">Select another Forum to Create, Edit, or Delete Forum Permissions on</a><br />
 <br />
 <span class="text">Use the form below to Edit the User Group Permissions for <% = strGroupName %> on the forum <% = strForumName %></span></div>
<form method="post" name="frmNewForum" action="edit_group_permissions.aspx?FID=<% = portal.variablesForum.intForumID %>&amp;gID=<% = intUserGroupID %>">
 <br />
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading"><b><% = strForumName %> Forum<br /> <% = strGroupName %> User Group Permissions</b></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Forum Access:</td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="read" value="true"<% If portal.variablesForum.blnRead = true Then Response.Write(" checked") %> /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Post New Topics:</td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="post" value="true"<% If portal.variablesForum.blnPost = true Then Response.Write(" checked") %> /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Reply To Posts:<br /> </td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="reply" value="true"<% If portal.variablesForum.blnReply = true Then Response.Write(" checked") %> /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Edit Posts:</td>
      <td valign="top"> 
       <input type='checkbox' name="edit" value="true"<% If portal.variablesForum.blnEdit = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Delete Posts:</td>
      <td valign="top"> 
       <input type='checkbox' name="delete" value="true"<% If portal.variablesForum.blnDelete = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Sticky Topics:</td>
      <td valign="top"> 
       <input type='checkbox' name="priority" value="true"<% If portal.variablesForum.blnPriority = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Create Poll's:</td>
      <td valign="top"> 
       <input type='checkbox' name="poll" value="true"<% If portal.variablesForum.blnPollCreate = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Vote in Poll's:</td>
      <td valign="top"> 
       <input type='checkbox' name="vote" value="true"<% If portal.variablesForum.blnVote = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Upload Images in Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span></td>
      <td valign="top"> 
       <input type='checkbox' name="images" value="true"<% If portal.variablesForum.blnImageUpload = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Attach Files to Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span> 
      </td>
      <td valign="top"> 
       <input type='checkbox' name="files" value="true"<% If portal.variablesForum.blnAttachments = true Then Response.Write(" checked") %> /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Moderate Forum:<br />
       <span class="smText">Only enable this if you wish this User Group to be able to have moderator functions on this forum.</span></td>
      <td width="43%" valign="top" class="smText"> 
       <input type='checkbox' name="moderate" value="true"<% If blnModerateForum = true Then Response.Write(" checked") %> /></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type="hidden" name="postBack" value="true" />
  <input type='submit' name="Submit" value="Update User Group Permissions" />
  <input type="reset" name="Reset" value="Reset Form" />
  <br />
 </div>
</form><%


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
</body>
</html>