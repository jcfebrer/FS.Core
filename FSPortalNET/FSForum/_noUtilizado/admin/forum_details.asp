

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True


'******************************************
'***  		Create Usercode	      *****
'******************************************

Private Function func.userCode(ByVal strusuario)

	'Randomise the system timer
	Randomize Timer

	'Calculate a code for the user
	strUserCode = strusuario & hexValue(15)

	'Make the usercode SQL safe
	strUserCode = formatSQLInput(strUserCode)

	'Replace double quote with single in this intance
	strUserCode = Replace(strUserCode, "''", "'", 1, -1, 1)

	'Return the function
	userCode = strUserCode
End Function




'Dimension variables
Dim strMode		'holds the mode of the page, set to true if changes are to be made to the database
Dim strForumName	'Holds the name of the forum
Dim strForumDescription	'Holds the discription of the forum
Dim strForumclave	'Holds the forum clave
Dim intForumID		'Holds the forum ID number
Dim strForumCode	'Holds a security code for the forum if it is clave protected
Dim strCatName		'Holds the name of the category
Dim intCatID		'Holds the ID number of the category
Dim intSelCatID		'Holds the selected cat id
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
Dim blnLocked
Dim blnHide
Dim intShowTopicsFrom	'Holds the amount of time to show topics in




'Initilise variables
intCatID = 0
intShowTopicsFrom = 31
intRead = 1
intPost = 2
intReply = 2
intEdit = 2
intDelete = 2
intPriority = 4
intPollCreate = 0
intVote = 0
intAttachments = 0
intImageUpload = 0
blnLocked = False
blnHide = False



'Read in the details
portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))
strForumclave = LCase(Request.Form("clave"))


strMode = Request("mode")


'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* From " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID=" & portal.variablesForum.intForumID & ";"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If this is a post back update the database
If (strMode = "edit" OR strMode = "new") AND Request.Form("postBack") Then

	With rsCommon
		'If this is a new one add new
		If strMode = "new" Then .AddNew


		'Update the recordset
		.Fields("Cat_ID") = CInt(Request.Form("cat"))
		.Fields("Forum_name") = Request.Form("forumName")
		.Fields("Forum_description") = Request.Form("description")
		.Fields("Read") = CInt(Request.Form("read"))
		.Fields("Post") = CInt(Request.Form("post"))
		.Fields("Reply_posts") = CInt(Request.Form("reply"))
		.Fields("Edit_posts") = CInt(Request.Form("edit"))
		.Fields("Delete_posts") = CInt(Request.Form("delete"))
		.Fields("Priority_posts") = CInt(Request.Form("priority"))
		.Fields("Poll_create") = CInt(Request.Form("polls"))
		.Fields("Vote") = CInt(Request.Form("vote"))
		.Fields("Attachments") = CInt(Request.Form("files"))
		.Fields("Image_upload") = CInt(Request.Form("images"))
		.Fields("Locked") = CBool(Request.Form("locked"))
		.Fields("Hide") = CBool(Request.Form("hide"))
		.Fields("Show_topics") = CInt(Request.Form("showTopics"))
		'See if there is a clave if not the filed must be null
		If Request.Form("remove") Then
			.Fields("clave") = null
			.Fields("Forum_code") = null
		'Add the new or updated clave and usercode to the database
		ElseIf strForumclave <> "" Then

			'Encrypt the forum clave
			strForumclave = HashEncode(strForumclave)

			'Calculate a code for the forum
			strForumCode = func.userCode(strForumName)

			'Place in recordset
			.Fields("clave") = strForumclave
			.Fields("Forum_code") = strForumCode
		End If

		'Update the database with the new user's details
		.Update
	End With

	'If this is a new forum go back to the main forums page
	If strMode = "new" Then

		'Release server varaibles
		rsCommon.Close
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

		Response.Redirect("view_forums.aspx")
	End If

	'Re-run the query to read in the updated recordset from the database
	rsCommon.Requery
End If

'Read in the forum details from the recordset
If NOT rsCommon.EOF Then

	'Read in the forums from the recordset
	intCatID = CInt(rsCommon("Cat_ID"))
	strForumName = rsCommon("Forum_name")
	strForumDescription = rsCommon("Forum_description")
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
	strForumclave = rsCommon("clave")
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
	blnLocked = CBool(rsCommon("Locked"))
	blnHide = CBool(rsCommon("Hide"))
	intShowTopicsFrom = CInt(rsCommon("Show_topics"))
End If

'Release server varaibles
rsCommon.Close



'Read in the category name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.Cat_name, " & portal.variablesForum.strDbTable & "Category.Cat_ID FROM " & portal.variablesForum.strDbTable & "Category ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_order ASC;"

'Query the database
rsCommon=db.execute(strSQL)
%>
<html>
<head>

<title>Forum Details</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a category
	if (document.frmNewForum.cat.value==""){
		alert("Please select the Category this Forum is to be in");
		return false;
	}

	//Check for a forum name
	if (document.frmNewForum.forumName.value==""){
		alert("Please enter a Name for the Forum");
		document.frmNewForum.forumName.focus();
		return false;
	}

	//Check for a pforum description
	if (document.frmNewForum.description.value==""){
		alert("Please enter a Description for the Forum");
		document.frmNewForum.description.focus();
		return false;
	}

	return true
}
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Forum Details</span><br />
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="view_forums.aspx" target="_self">Return to the Forum Administration page</a><br />
 <br /><%

 If rsCommon.EOF Then
 	%>
 <table width="98%" border="0" cellspacing="0" cellpadding="1" height="135">
  <tr>
   <td align="center"><span class="lgText"><b>You must first enter a Forum Category to place your new Forum in.</b></span><b><br />
    <br />
    </b><a href="category_details.aspx?mode=new" target="_self">Enter a Forum Category</a></td>
  </tr>
 </table>
 <%
Else
%>
</div>
<form method="post" name="frmNewForum" action="forum_details.aspx?FID=<% = portal.variablesForum.intForumID %>" onSubmit="return CheckForm();">
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading">Select Forum Category</td>
     </tr>
     <tr bgcolor="#FFFFFF">
      <td colspan="2" bgcolor="#F5F5FA" class="text">Select the Category from the drop down list below that you would like this forum to be in.<br />
       <select name="cat">
        <option value=""<% If intCatID = 0 Then Response.Write(" selected") %>>-- Select Forum Category --</option><%


'Loop through all the categories in the database
Do while NOT rsCommon.EOF

	'Read in the deatils for the category
	strCatName = rsCommon("Cat_name")
	intSelCatID = CInt(rsCommon("Cat_ID"))

	'Display a link in the link list to the cat
	Response.Write (vbCrLf & "		<option value=""" & intSelCatID & """")
	If intCatID = intSelCatID Then Response.Write(" selected")
	Response.Write(">" & strCatName & "</option>")


	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

%>
       </select>
      </td>
     </tr>
    </table></td>
  </tr>
 </table>
 <br />
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading"><b>Forum Details</b></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Forum Name*:</td>
      <td width="43%" valign="top"> 
       <input type='text' name="forumName" maxlength="60" size="30" value="<% = strForumName %>"> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Forum Description*:<br /> <span class="smText">Give a brief description of the forum.</span></td>
      <td valign="top"> 
       <input type='text' name="description" maxlength="190" size="30" value="<% = strForumDescription %>"></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Show Topics in the Last:<br />
       <span class="smText">This is the default time span in which topics containing new posts are shown in the topic list.</span></td>
      <td valign="top"> 
       <select name="showTopics">
        <option value="0" <% If intShowTopicsFrom = 0 Then response.write("selected" %>> All </option>
        <option value="7" <% If intShowTopicsFrom = 7 Then response.write("selected" %>> Last week </option>
        <option value="14" <% If intShowTopicsFrom = 14 Then response.write("selected" %>> Last two weeks </option>
        <option value="31" <% If intShowTopicsFrom = 31 Then response.write("selected" %>> Last month </option>
        <option value="62" <% If intShowTopicsFrom = 62 Then response.write("selected" %>> Last two months </option>
        <option value="182" <% If intShowTopicsFrom = 182 Then response.write("selected" %>> Last six months </option>
        <option value="365" <% If intShowTopicsFrom = 365 Then response.write("selected" %>> Last year </option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text"><p>Forum Locked:<br />
        <span class="smText">If the forum is locked posts can not be made in the forum. Useful for maintenance.</span></p>
       </td>
      <td valign="top"> 
       <input name="locked" type='checkbox' id="locked" value="true"<% If blnLocked Then response.write(" checked" %>></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Hide Forum if no access:<br />
       <span class="smText">Hide this forum on the boards main page if the user can not access the forum.</span></td>
      <td width="43%" valign="top"> 
       <input name="hide" type='checkbox' id="hide" value="true"<% If blnHide Then response.write(" checked" %>></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <br />
 <br />
 <table width="100%" height="58" border="0" cellpadding="0" cellspacing="0">
  <tr>
   <td align="center" class="text"><span class="lgText">Generic Forum Permissions</span><br />
    Use the form below to set Generic Permissions on this forum.<br />
    <br />
    Please be aware that the Generic Permissions can be over ridden by setting permissions on this forum by User Group.<br />
    <br />
    <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
     <tr>
      <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
        <tr bgcolor="#CCCEE6">
         <td class="tHeading"><b>Key to Forum Permissions</b></td>
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
    </table> <br />
   </td>
  </tr>
 </table>
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading"><b>Generic Forum Permissions</b></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Forum Access:</td>
      <td width="43%" valign="top"> 
       <select name="read">
        <option value="1"<% If intRead = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intRead = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intRead = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intRead = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Post New Topics:</td>
      <td width="43%" valign="top"> 
       <select name="post">
        <option value="1"<% If intPost = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intPost = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intPost = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intPost = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text">Reply To Posts:<br /> </td>
      <td width="43%" valign="top"> 
       <select name="reply">
        <option value="1"<% If intReply = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intReply = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intReply = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intReply = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Edit Posts:</td>
      <td valign="top"> 
       <select name="edit">
        <option value="1"<% If intEdit = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intEdit = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intEdit = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intEdit = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Delete Posts:</td>
      <td valign="top"> 
       <select name="delete">
        <option value="1"<% If intDelete = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intDelete = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intDelete = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intDelete = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Sticky Topics:</td>
      <td valign="top"> 
       <select name="priority">
        <option value="1"<% If intPriority = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intPriority = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intPriority = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intPriority = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Create Poll's:</td>
      <td valign="top"> 
       <select name="polls">
	<option value="0"<% If intPollCreate = 0 Then Response.Write(" selected") %>>Off</option>
        <option value="1"<% If intPollCreate = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intPollCreate = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intPollCreate = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intPollCreate = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Vote in Poll's:</td>
      <td valign="top"> 
       <select name="vote">
	<option value="0"<% If intVote = 0 Then Response.Write(" selected") %>>Off</option>
        <option value="1"<% If intVote = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intVote = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intVote = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intVote = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Upload Images in Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span></td>
      <td valign="top"> 
       <select name="images">
        <option value="0"<% If intImageUpload = 0 Then Response.Write(" selected") %>>Off</option>
        <option value="1"<% If intImageUpload = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intImageUpload = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intImageUpload = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intImageUpload = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Attach Files to Posts:<br />
       <span class="smText">Only enable this function if you have first setup the <a href="upload_configure.aspx" target="_self" class="smLink">upload configuration</a>, if your web space supports it.</span> 
      </td>
      <td valign="top"> 
       <select name="files">
        <option value="0"<% If intAttachments = 0 Then Response.Write(" selected") %>>Off</option>
        <option value="1"<% If intAttachments = 1 Then Response.Write(" selected") %>>All Users</option>
        <option value="2"<% If intAttachments = 2 Then Response.Write(" selected") %>>Registered Users</option>
        <option value="3"<% If intAttachments = 3 Then Response.Write(" selected") %>>Private Groups</option>
        <option value="4"<% If intAttachments = 4 Then Response.Write(" selected") %>>Forum Admin's Only</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="57%" class="text"> 
       <% If strForumclave <> "" Then Response.Write("Change ")%>
       clave:<br /> <span class="smText">If you want this forum clave protected place the clave here. Otherwise leave it blank.</span></td>
      <td width="43%" valign="top" class="smText"> 
       <input type='text' name="clave" maxlength="20" size="20"><%

      	'If there is a clave ask if they want to remove it
      	If strForumclave <> "" Then
      	%>
      <br /><input name="remove" type='checkbox' id="remove" value="true"> Remove Forum clave<%

	End If

      	%></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type="hidden" name="postBack" value="true">
  <input type="hidden" name="mode" value="<% = strMode %>">
  <input type='submit' name="Submit" value="Submit Forum Details">
  <input type="reset" name="Reset" value="Reset Form">
  <br />
 </div>
</form><%
End If


'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
</body>
</html>