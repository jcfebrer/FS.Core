

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True

Dim strGroupName
Dim intSelGroupID


%>
<html>
<head>

<title>Group Permissions</title>


     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"> 
 <p class="text"><span class="heading">User Group Permissions</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  When you created a forum you created Generic Permissions for that forum to allow users to do things like, View, Post, Reply, etc. in that forum.<br />
  <br />
  From here you can overwrite these Generic Forum Permissions for different User Groups, allowing different User Groups to have different permissions on forums, you can also select User Groups to be able 
  to have moderator privileges on forums.<br />
  <br />
  Please note that User Group Permissions can be overridden by setting permissions for individual users on forums.<br />
  <br />
  Select the User Group that you would like to Create, Edit, or Remove Permissions for.<br />
 </p>
</div>
<form method="post" name="frmSelectForum" action="forum_group_permissions.aspx">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="8">
  <tr>
   <td height="24"> <table border="0" align="center"  height="8" cellpadding="4" cellspacing="1" width="100%">
     <tr bgcolor="#CCCEE6" >
      <td width="51%" height="2" align="left" valign="top" class="tHeading"><b>Select the User Group you would like to create or Edit permissions for</b></td>
     </tr>
     <tr bgcolor="#FFFFFF">
      <td width="51%"  height="12" align="left" bgcolor="#F5F5FA"> 
       <select name="GID"><%

'Read in the group name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID, " & portal.variablesForum.strDbTable & "Group.Name FROM " & portal.variablesForum.strDbTable & "Group ORDER BY " & portal.variablesForum.strDbTable & "Group.Group_ID ASC;"

'Query the database
rsCommon=db.execute(strSQL)


'Loop through all the categories in the database
Do while NOT rsCommon.EOF

	'Read in the deatils for the category
	strGroupName = rsCommon("Name")
	intSelGroupID = CInt(rsCommon("Group_ID"))

	'Display a link in the link list to the cat
	Response.Write (vbCrLf & "		<option value=""" & intSelGroupID & """")
	Response.Write(">" & strGroupName & "</option>")


	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

'Reset server objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%></select>
      </td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type='submit' name="Submit" value="Edit or Create User Group Permissions">
 </div>
</form>
<br />
</body>
</html>
