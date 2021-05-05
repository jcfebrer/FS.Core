

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Set the timeout of the page
Server.ScriptTimeout =  1000


'Dimension variables
Dim intStartGroupID		'Holds the strating group ID
Dim intDeleteUserGroupID	'Holds the group ID to be deleted

'Get the forum ID to delete
intDeleteUserGroupID = CInt(Request.QueryString("GID"))


'If there is a group ID to delete then do teh job
If intDeleteUserGroupID <> "" Then
	
	'Initalise the strSQL variable with an SQL statement to get the topic from the database
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = 1;"
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Starting_group = True;"
	End If
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Read in the strating group ID
	intStartGroupID = CInt(rsCommon("Group_ID"))




	'Initalise the SQL string with an SQL update command to update the starting group
	strSQL = "UPDATE " & "Usuarios SET "_
	 	 & "" & "Usuarios.Group_ID = " & intStartGroupID & " "_
	         & "WHERE " & "Usuarios.Group_ID = " & intDeleteUserGroupID & ";"

	'Write the updated number of posts to the database
	db.execute(strSQL)



	'Delete the group permissions for forums form the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID ="  & intDeleteUserGroupID & ";"

	'Write to database
	db.execute(strSQL)
	


	'Delete the group form the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Group_ID ="  & intDeleteUserGroupID & ";"

	'Write to database
	db.execute(strSQL)


	'Reset Server Objects
	rsCommon.Close
End If



'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Return to the forum categories page
Response.Redirect("view_groups.aspx")
%>