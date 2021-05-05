

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True

'Dimension variables
Dim intUserGroupID	'Holds the group ID
Dim strGroupName	'Holds the name of the group
Dim lngMinimumPosts	'Holds the minimum amount of posts to be in that group
Dim blnSpecialGroup	'Set to true if a special group
Dim intStars		'Holds the number of stars for the group
Dim strCustomStars	'Holds the custom stars image if there is one fo0r this group
Dim blnStartingGroup	'Set to true if it is the starting group


'If this is a postback update the strating group
If Request.Form("postBack") Then
	
	
	'Read in the group ID to make the starting group
	intUserGroupID = CInt(Request.Form("start"))

	'Clear the old strating group

	'Initalise the SQL string with an SQL update command to update the starting group
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Group SET "
		strSQL = strSQL & "" & portal.variablesForum.strDbTable & "Group.Starting_group = 0 "
		strSQL = strSQL & " WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = 1;"
	Else
		strSQL = "UPDATE " & portal.variablesForum.strDbTable & "Group SET "
		strSQL = strSQL & "" & portal.variablesForum.strDbTable & "Group.Starting_group = False "
		strSQL = strSQL & " WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = True;"
	End If

	'Write the updated number of posts to the database
	db.execute(strSQL)
	

	'Read the various groups from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.* FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Group_ID = " & intUserGroupID & ";"
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'If a record is returned update it
	If not rsCommon.EOF Then	
		
		rsCommon.Fields("Starting_group") = True
		rsCommon.Update
		
		'Requery to let access catch up
		rsCommon.Requery
	End If
	
	
	'Close the recordset
	rsCommon.Close
End If
%>
<html>
<head>
<title>Administer User Groups</title>



     	


<link href="includes/default_style.css" rel="stylesheet" type="text/css">
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Administer User Groups</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a></div>
 <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>

  <td align="center" class="text"><br />
   From here you can create, delete, change the details, etc. of forum user groups.<br />
    <br />
   Click on User Group Name to change the details and settings for that user Group.<br />
   <br />
   To change the Starting Group, select which group you want as the starting group and click the 'Update Starting Group' button.
 </table>
 
<form action="view_groups.aspx" method="post" name="form1" target="_self">
 <table width="98%" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
  <tr>
   <td> <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center">
     <tr valign="top" class="tHeading"> 
      <td width="57%" nowrap bgcolor="#CCCEE6">User Group</td>
      <td width="10%" nowrap bgcolor="#CCCEE6">Stars</td>
      <td width="13%" height="12" nowrap bgcolor="#CCCEE6">Minimum Num of Posts<br /> <span class="smText"> (for Ladder Groups only)</span></td>
      <td width="8%" height="12" align="center" nowrap bgcolor="#CCCEE6">Starting<br />Group</td>
      <td width="4%" height="12" align="center" nowrap bgcolor="#CCCEE6">Delete</td>
     </tr>
     <%

'Read the various groups from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.* FROM " & portal.variablesForum.strDbTable & "Group ORDER BY " & portal.variablesForum.strDbTable & "Group.Group_ID ASC;"

'Query the database
rsCommon=db.execute(strSQL)

'Check there are user groups to display
If rsCommon.EOF Then

	'If there are no user groups display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""#FFFFFF"" colspan=""4""><span class=""text"">There are User Groups to display.</span></td>"

'Else there the are user groups so write the HTML to display them
Else


	'Loop round to read in all the groups in the database
	Do While NOT rsCommon.EOF

		'Get the category name from the database
		intUserGroupID = CInt(rsCommon("Group_ID"))
		strGroupName = rsCommon("Name")
		lngMinimumPosts = CLng(rsCommon("Minimum_posts"))
		blnSpecialGroup = CBool(rsCommon("Special_rank"))
		intStars = CInt(rsCommon("Stars"))
		strCustomStars = rsCommon("Custom_stars")
		blnStartingGroup = CBool(rsCommon("Starting_group"))

		'Display the groups

%>
     <tr bgcolor="#F5F5FA"> 
      <td class="text"><a href="group_details.aspx?mode=edit&amp;gID=<% = intUserGroupID %>" target="_self"> 
       <% = strGroupName %>
       </a></td>
      <td class="text"><img src="<% If strCustomStars <> "" Then Response.Write(strCustomStars) Else Response.Write("images/" & intStars & "_star_rating.gif") %>" alt="<% = intStars %> stars"></td>
      <td class="text"> 
       <%

     	'If this is a special group then disply a message that it is not a rank group
     	If blnSpecialGroup Then
     		Response.Write("Non Ladder Group")

     	'If this is a rank group disply the minimum number of posts to be in this group
     	Else
     		Response.Write(lngMinimumPosts)

	End If

     %>
      </td>
      <td width="8%" align="center" class="text"> 
       <%
     
     	'If this is the admin group or guest group then don't let em change the starting group
	If intUserGroupID <> 1 AND intUserGroupID <> 2 Then 
     
     %>
       <input type="radio" name="start" value="<% = intUserGroupID %>"<% If blnStartingGroup Then Response.Write(" checked") %>> 
       <%
     
	End If
       %>
      </td>
      <td width="4%" class="text"  align="center"> 
       <%

     		'If this is not the admin group, guest group, or starting group let them delete the group
     		If intUserGroupID <> 1 AND intUserGroupID <> 2 AND blnStartingGroup = False Then

     %>
       <a href="delete_group.aspx?GID=<% = intUserGroupID %>" OnClick="return confirm('Are you sure you want to Delete this User Group?\n\nWARNING: Deleting this user group will mean all members of this user group will be transfered to the Starting Group!')"><img src="images/delete_icon.gif" width="15" height="16" border="0" alt="Delete"></a> 
       <%

	End If

     %>
      </td>
     </tr>
     <%

		'Move to the next database record
		rsCommon.MoveNext
	Loop
End If

'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
    </table></td>
  </tr>
 </table>
 <table width="100%" border="0" cellspacing="0" cellpadding="4">
  <tr>
   <td align="right">
<input name="postBack" type="hidden" id="postBack" value="true">
    <input type='submit' name="Submit" value="Update Starting Group">
   </td>
 </tr>
</table>
</form>
 <table width="98%" border="0" align="center" cellpadding="3" cellspacing="0">
 <tr align="center">
   <td width="50%"><form action="group_details.aspx?mode=new" method="post" name="form2" target="_self">
    <input type='submit' name="Submit" value="Create New User Group"></form></td>
  </tr>
 </table>
 <br />
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#000000">
 <tr>
  <td><table width="100%" border="0" cellspacing="1" cellpadding="3">
    <tr> 
     <td align="center" bgcolor="#CCCEE6"><span class="lgText">Please Read the Following</span></td>
    </tr>
    <tr> 
     <td bgcolor="#CCCEE6" class="bold">Admin Group</td>
    </tr>
    <tr> 
     <td bgcolor="#EAEAF4" class="text">The Admin Group, (the first group in the list), cannot be deleted. Any member placed in this user group has admin powers over the whole board and can also use this 
      admin area, so be careful of the users you place in this group!</td>
    </tr>
    <tr> 
     <td bgcolor="#CCCEE6" class="bold">Guest Group</td>
    </tr>
    <tr> 
     <td bgcolor="#EAEAF4" class="text">The Guest Group, (the second group in the list), cannot be deleted. Any member placed in this user group has the same powers on the board as visitors that have not 
      registered.</td>
    </tr>
    <tr> 
     <td bgcolor="#CCCEE6" class="bold">Starting Group</td>
    </tr>
    <tr> 
     <td bgcolor="#EAEAF4" class="text">The Starting Group is a special group that new members automatically become part of when they first register. You can only have one starting group, if you want to 
      delete the starting group you must first select another group to be the starting group. You change the starting group by editing the details of a group and selecting it to become the starting group.<br /> 
     </td>
    </tr>
   </table></td>
 </tr>
</table>
</body>
</html>