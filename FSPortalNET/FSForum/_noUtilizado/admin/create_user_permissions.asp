

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim strForumName	'Holds the name of the forum
Dim strMemberName	'Holds the name of the forum member
Dim lngMemberID		'Holds the ID number of the member
Dim intForumID		'Holds the forum ID number
Dim intSelGroupID	'Holds the group ID to select
Dim iaryForumID		'Holds the forum ID array


'Read in the details
lngMemberID = CLng(Request("UID"))





'Read in the member name
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & "Usuarios.usuario From " & "Usuarios WHERE " & "Usuarios.UsuarioID=" & lngMemberID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the forum name form the recordset
If NOT rsCommon.EOF Then

	'Read in the forums from the recordset
	strMemberName = rsCommon("usuario")
End If

'Release server varaibles
rsCommon.Close




'If this is a post back update the database
If Request.Form("postBack") Then
	
	
	'Run through till all checked forums are added
	For each iaryForumID in Request.Form("chkFID")


		'Initalise the strSQL variable with an SQL statement to query the database
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.* From " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & iaryForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.UsuarioID = " & lngMemberID & ";"
		
		'Set the Lock Type for the records so that the record set is only locked when it is updated
		rsCommon.LockType = 3
		
		'Query the database
		rsCommon=db.execute(strSQL)
	
		With rsCommon
			'If this is a new one add new
			If rsCommon.EOF Then .AddNew
	
			'Update the recordset
			.Fields("Forum_ID") = iaryForumID
			.Fields("UsuarioID") = lngMemberID
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
			
			'Close recordset
			.close
		End With
	Next
	

	'Release server varaibles
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect back to permissions page
	Response.Redirect("forum_user_permissions.aspx?UID=" & lngMemberID)
End If


%>
<html>
<head>

<title>Create Member Permissions</title>


     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Create Member Permissions for <% = strMemberName %></span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="find_user.aspx" target="_self">Select another Forum to Create, Edit, or Delete Forum Permissions on</a><br />
 <br />
 <span class="text">Use the form below to create permissions on Forums for the member <b><% = strMemberName %></b>.</span></div>
<form action="create_user_permissions.aspx?UID=<% = lngMemberID %>" method="post" name="frmNewForum" target="_self">
 <br />
 <table width="500" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="500"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading"><b><% = strForumName %> Forum Permissions for <% = strMemberName %></b></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Forum Access:</td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="read" value="true" /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Post New Topics:</td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="post" value="true" /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Reply To Posts:<br /> </td>
      <td width="43%" valign="top"> 
       <input type='checkbox' name="reply" value="true" /> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Edit Posts:</td>
      <td valign="top"> 
       <input type='checkbox' name="edit" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Delete Posts:</td>
      <td valign="top"> 
       <input type='checkbox' name="delete" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Sticky Topics:</td>
      <td valign="top"> 
       <input type='checkbox' name="priority" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Create Poll's:</td>
      <td valign="top"> 
       <input type='checkbox' name="poll" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Vote in Poll's:</td>
      <td valign="top"> 
       <input type='checkbox' name="vote" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Upload Images in Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span></td>
      <td valign="top"> 
       <input type='checkbox' name="images" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Attach Files to Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span> 
      </td>
      <td valign="top"> 
       <input type='checkbox' name="files" value="true" /></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Moderate Forum:<br />
       <span class="smText">Only enable this if you wish this User Group to be able to have moderator functions on this forum.</span></td>
      <td width="43%" valign="top" class="smText"> 
       <input type='checkbox' name="moderate" value="true" /></td>
     </tr>
    </table></td>
  </tr>
 </table>
  <br /><%
 
'Display the forums


'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_Name, " & portal.variablesForum.strDbTable & "Forum.Forum_ID FROM " & portal.variablesForum.strDbTable & "Category, " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Category.Cat_ID = " & portal.variablesForum.strDbTable & "Forum.Cat_ID ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_ORDER ASC, " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"

'Query the database
rsCommon=db.execute(strSQL)
 
%>
 <table width="500" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="500"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading"><b>Select Forum to apply the Member Group Permisions to for <% = strMemberName %></b></td>
     </tr><%

'Loop round and display all the forums
Do while NOT rsCommon.EOF
     

%>
     <tr bgcolor="#F5F5FA"> 
      <td width="2%"> 
       <input type='checkbox' name="chkFID" id="chkFID" value="<% = rsCommon("Forum_ID") %>" /></td>
      <td class="text"> 
       <% = rsCommon("Forum_Name") %>
      </td>
     </tr><%
	
	'Move next record
	rsCommon.MoveNext
Loop

%>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
   <div align="center"><br />
  <input type="hidden" name="postBack" value="true" />
  <input type='submit' name="Submit" value="Create Member Permissions" />
  <input type="reset" name="Reset" value="Reset Form" />
  <br />
 </div>
</form><%


'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
</body>
</html>