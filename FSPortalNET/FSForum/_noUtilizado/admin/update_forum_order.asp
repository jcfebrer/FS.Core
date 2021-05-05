

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Update the category and forum order
If Request.Form("Submit") = "Update Order" Then
		
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.* From " & portal.variablesForum.strDbTable & "Category ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_order ASC;"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
		
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'Loop through the rs to change the cat order
	Do While NOT rsCommon.EOF
	
		rsCommon.Fields("Cat_order") = CInt(Request.Form("catOrder" & rsCommon("Cat_ID")))		
					
		'Add new forum to database
		rsCommon.Update
		
		'Move to the next record in the recordset
		rsCommon.MoveNext
	Loop
	
	'Close the recordset
	rsCommon.Close
	
	
	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* From " & portal.variablesForum.strDbTable & "Forum ORDER BY " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"
	
	'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
		
	'Query the database
	rsCommon=db.execute(strSQL)
	
	
	'Loop through rs to change the forums order
	Do While NOT rsCommon.EOF
	
		rsCommon.Fields("Forum_Order") = CInt(Request.Form("forumOrder" & rsCommon("Forum_ID")))		
					
		'Add new forum to database
		rsCommon.Update
		
		'Move to the next record in the recordset
		rsCommon.MoveNext
	Loop
	
	'Close the recordset
	rsCommon.Close	
End If
	
'Reset main server variables
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Return to the forum categories page
response.redirect("view_forums.aspx"
%>