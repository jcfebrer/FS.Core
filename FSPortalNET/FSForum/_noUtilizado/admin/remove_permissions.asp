

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim intUserGroupID	'Holds the ID number of the group
Dim intForumID		'Holds the forum ID number
Dim lngMemberID		'Holds the member ID number
Dim strMode		'Hols the page mode


'Read in the details
portal.variablesForum.intForumID = CInt(Request("FID"))
intUserGroupID = CInt(Request("GID"))
lngMemberID = CLng(Request("UID"))
strMode = Request("M")


'If UP - User Permission only dfelete the user permimissons
If strMode = "UP" Then
	'Delete the user permissions for forums form the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.UsuarioID = " & lngMemberID & ";"

'Else User Group permisions
Else
	'Delete the group permissions for forums form the database
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Permissions WHERE " & portal.variablesForum.strDbTable & "Permissions.Forum_ID=" & portal.variablesForum.intForumID & " AND " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & intUserGroupID & ";"
End If

'Write to database
db.execute(strSQL)
	



'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

If strMode = "UP" Then
	'Return to the forum user permisisons
	Response.Redirect("forum_user_permissions.aspx?UID=" & lngMemberID)
Else
	'Return to the forum group permisisons
	Response.Redirect("forum_group_permissions.aspx?GID=" & intUserGroupID)
End If
%>