

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


Dim intForumID


'If the user is user is using a banned IP redirect to an error page
If bannedIP() Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("insufficient_permission.aspx?M=IP")
End If



'If the person is not a moderator or admin then send them away
If portal.variablesForum.blnAdmin = False Then
	'Reset Server Objects
	Set rsCommon = Nothing
	Set adoCon = Nothing
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If



'Read in the forum ID
portal.variablesForum.intForumID = CInt(Request.QueryString("FID"))



'Update topic and post count
common.updateTopicPostCount(portal.variablesForum.intForumID)
	


'Reset server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<script language="javascript">
window.opener.location.href = window.opener.location.href; 
window.close();
</script>