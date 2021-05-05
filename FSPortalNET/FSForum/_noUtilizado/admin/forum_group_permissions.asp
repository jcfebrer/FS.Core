

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim strForumName	'Holds the name of the forum
Dim strGroupName	'Holds the name of the forum group
Dim intUserGroupID	'Holds the ID number of the group
Dim strForumclave	'Holds the forum clave
Dim intForumID		'Holds the forum ID number
Dim intRead
Dim intPost
Dim intReply
Dim intEdit
Dim intDelete
Dim intPriority
Dim intPollCreate
Dim intVote
Dim intAttachments
Dim intImageUpload
Dim rsPermissions

'These are used for forum permissions
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
intUserGroupID = CInt(Request("GID"))



'Get the group name
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Name From " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Group_ID=" & intUserGroupID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'If there is a group name returned get the name
If NOT rsCommon.EOF Then strGroupName = rsCommon("Name")

rsCommon.Close


'Get all the forums from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Category, " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Category.Cat_ID = " & portal.variablesForum.strDbTable & "Forum.Cat_ID ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_ORDER ASC, " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"

'Query the database
rsCommon=db.execute(strSQL)



'Intialise the ADO recordset object
Set rsPermissions = Server.CreateObject("ADODB.Recordset")   

%>
<html>
<head>

<title>Group Permissions</title>


     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">User Group Permissions for <% = strGroupName %></span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="group_perm_forum.aspx" target="_self">Select another Forum to Create, Edit, or Delete Forum Permissions on</a><br />
 <br />
</div>
 <table width="100%" height="58" border="0" cellpadding="0" cellspacing="0">
  <tr>
  <td align="center" class="text">Below you can view Generic Forum Permissions for each of the forums as well as Create or Edit any User Group permissions on the forums.<br />
    <br />
    <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
     <tr>
      <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
        <tr bgcolor="#CCCEE6">
         <td class="tHeading"><b>Key to Generic Forum Permissions</b></td>
        </tr>
        <tr bgcolor="#FFFFFF">
         
        <td bgcolor="#EAEAF4" class="text">
<ol>
           <li><span class="bold">All Users</span><br />
            <span class="smText">This gives permission to all users including Guests.</span></li>
           <li><span class="bold">Registered Users</span><br />
            <span class="smText">This gives permission to all users except Guests.</span></li>
           <li><span class="bold">Private Groups</span><br />
            <span class="smText">This gives permissions to Groups that you have given permission to through the setting up of Group Permissions for this forum.</span></li>
           
          <li><span class="bold">Forum Admin's</span><br />
            <span class="smText">This gives permission to Forum Administers only.</span></li>
          </ol></td>
        </tr>
       </table></td>
     </tr>
    </table> 
    <br />
   </td>
  </tr>
 </table><%
 
'Loop through forums
Do While NOT rsCommon.EOF

	'Read in the forums from the recordset
	strForumName = rsCommon("Forum_name")
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	intRead = CInt(rsCommon("Read"))
	intPost = CInt(rsCommon("Post"))
	intReply = CInt(rsCommon("Reply_posts"))
	intEdit = CInt(rsCommon("Edit_posts"))
	intDelete = CInt(rsCommon("Delete_posts"))
	intPriority = CInt(rsCommon("Priority_posts"))
	intPollCreate = CInt(rsCommon("Poll_create"))
	intVote = CInt(rsCommon("Vote"))
	intAttachments = CInt(rsCommon("Attachments"))
	intImageUpload = CInt(rsCommon("Image_upload"))



%>
<hr /><br /><span class="lgText"><% = strForumName %></span><br /><br />
 <table width="680" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr> 
   <td width="680"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
    <tr bgcolor="#CCCEE6"> 
      <td colspan="10" class="tHeading"><b>Generic Forum Permissions for <% = strForumName %></b></td>
     </tr>
     
    <tr bgcolor="#F5F5FA" class="bold"> 
     <td align="center">Forum Access</td>
      
     <td align="center">New Topics</td>
      
     <td align="center">Reply to Posts</td>
      
     <td align="center">Edit Posts</td>
      
     <td align="center">Delete Posts</td>
      
     <td align="center">Sticky Topics</td>
      
     <td align="center">Create Poll's</td>
      
     <td align="center">Vote in Poll's</td>
      
     <td align="center">Upload Images</td>
      
     <td align="center">Attach Files</td>
     </tr>
     
    <tr align="center" bgcolor="#F5F5FA"> 
     <td class="text"> 
      <% If intRead = 1 Then Response.Write("All Users") %>
      <% If intRead = 2 Then Response.Write("Registered Users") %>
      <% If intRead = 3 Then Response.Write("Private Groups") %>
      <% If intRead = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intPost = 1 Then Response.Write("All Users") %>
      <% If intPost = 2 Then Response.Write("Registered Users") %>
      <% If intPost = 3 Then Response.Write("Private Groups") %>
      <% If intPost = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intReply = 1 Then Response.Write("All Users") %>
      <% If intReply = 2 Then Response.Write("Registered Users") %>
      <% If intReply = 3 Then Response.Write("Private Groups") %>
      <% If intReply = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intEdit = 1 Then Response.Write("All Users") %>
      <% If intEdit = 2 Then Response.Write("Registered Users") %>
      <% If intEdit = 3 Then Response.Write("Private Groups") %>
      <% If intEdit = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intDelete = 1 Then Response.Write("All Users") %>
      <% If intDelete = 2 Then Response.Write("Registered Users") %>
      <% If intDelete = 3 Then Response.Write("Private Groups") %>
      <% If intDelete = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intPriority = 1 Then Response.Write("All Users") %>
      <% If intPriority = 2 Then Response.Write("Registered Users") %>
      <% If intPriority = 3 Then Response.Write("Private Groups") %>
      <% If intPriority = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intPollCreate = 0 Then Response.Write("Off") %>
      <% If intPollCreate = 1 Then Response.Write("All Users") %>
      <% If intPollCreate = 2 Then Response.Write("Registered Users") %>
      <% If intPollCreate = 3 Then Response.Write("Private Groups") %>
      <% If intPollCreate = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intVote = 0 Then Response.Write("Off") %>
      <% If intVote = 1 Then Response.Write("All Users") %>
      <% If intVote = 2 Then Response.Write("Registered Users") %>
      <% If intVote = 3 Then Response.Write("Private Groups") %>
      <% If intVote = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intImageUpload = 0 Then Response.Write("Off") %>
      <% If intImageUpload = 1 Then Response.Write("All Users") %>
      <% If intImageUpload = 2 Then Response.Write("Registered Users") %>
      <% If intImageUpload = 3 Then Response.Write("Private Groups") %>
      <% If intImageUpload = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
      
     <td class="text"> 
      <% If intAttachments = 0 Then Response.Write("Off") %>
      <% If intAttachments = 1 Then Response.Write("All Users") %>
      <% If intAttachments = 2 Then Response.Write("Registered Users") %>
      <% If intAttachments = 3 Then Response.Write("Private Groups") %>
      <% If intAttachments = 4 Then Response.Write("Forum Admin's Only") %>
     </td>
     </tr>
    </table></td>
  </tr>
  </table><%

	    
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.* From " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID=" & intUserGroupID & " AND " & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & ";"
	
	'Query the database
	rsPermissions=db.execute(strSQL)
	
	'Read in the forum details from the recordset
	Do While NOT rsPermissions.EOF
		
		'Read in the permissions from the recordset
		portal.variablesForum.blnRead = CBool(rsPermissions("Read"))
		portal.variablesForum.blnPost = CBool(rsPermissions("Post"))
		portal.variablesForum.blnReply = CBool(rsPermissions("Reply_posts"))
		portal.variablesForum.blnEdit = CBool(rsPermissions("Edit_posts"))
		portal.variablesForum.blnDelete = CBool(rsPermissions("Delete_posts"))
		portal.variablesForum.blnPriority = CBool(rsPermissions("Priority_posts"))
		portal.variablesForum.blnPollCreate = CBool(rsPermissions("Poll_create"))
		portal.variablesForum.blnVote = CBool(rsPermissions("Vote"))
		portal.variablesForum.blnAttachments = CBool(rsPermissions("Attachments"))
		portal.variablesForum.blnImageUpload = CBool(rsPermissions("Image_upload"))
		blnModerateForum = CBool(rsPermissions("Moderate"))
		
	


%><br />
  <table width="680" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
   <tr> 
    <td width="680"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
    <tr bgcolor="#CCCEE6">  
     <td colspan="11"><a href="edit_group_permissions.aspx?FID=<% = portal.variablesForum.intForumID %>&amp;gID=<% = intUserGroupID %>" target="_self" class="boldLink"><b><% = strGroupName %> Permissions for <% = strForumName %></b></a></td>
     </tr>
     
    <tr bgcolor="#F5F5FA" class="bold"> 
     <td align="center">Forum Access</td>
      
     <td align="center">New Topics</td>
      
     <td align="center">Reply to Posts</td>
      
     <td align="center">Edit Posts</td>
      
     <td align="center">Delete Posts</td>
      
     <td align="center">Sticky Topics</td>
      
     <td align="center">Create Poll's</td>
      
     <td align="center">Vote in Poll's</td>
      
     <td align="center">Upload Images</td>
      
     <td align="center">Attach Files</td>
      
     <td align="center">Moderate this Forum</td>
     </tr>
     
    <tr align="center" bgcolor="#F5F5FA"> 
     <td class="text"> 
      <% If portal.variablesForum.blnRead = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnPost = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnReply = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnEdit = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnDelete = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnPriority = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnPollCreate = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnVote = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnImageUpload = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If portal.variablesForum.blnAttachments = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
      
     <td class="text"> 
      <% If blnModerateForum = True Then Response.Write("Yes") Else Response.Write("No") %>
     </td>
     </tr>
     
    <tr align="right" bgcolor="#F5F5FA"> 
     <td colspan="11" class="text">Remove this Group Permissions <a href="remove_permissions.aspx?FID=<% = portal.variablesForum.intForumID %>&amp;gID=<% = intUserGroupID %>" OnClick="return confirm('Are you sure you want to Remove this Group Permission?')"><img src="images/delete_icon.gif" width="15" height="16" border="0" alt="Remove"></a></td>
     </tr>
    </table></td>
   </tr>
  </table>
  <br /><%
 
	 	'Move to the next recordset
		rsPermissions.MoveNext
	Loop

	'Close rs
	rsPermissions.Close

	'Move next record
	rsCommon.MoveNext
Loop



'Reset Server Objects
Set rsPermissions = Nothing
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<br /><hr /><br />
<form name="frm" method="post" action="create_group_permissions.aspx?GID=<% = intUserGroupID %>">
 <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
   <td align="center"><input type='submit' name="Submit" value="Create New Forum Permissions for User Group"></td>
  </tr>
 </table>
</form>
</body>
</html>