

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim iaryBadWordID	'Array to hold the ID number for each comment to be deleted

'Run through till all checked bad words are deleted
For each iaryBadWordID in Request.Form("chkWordID")
	
	'Initalise the strSQL variable with an SQL statement
	strSQL = "DELETE FROM " & portal.variablesForum.strDbTable & "Smut WHERE (ID_no ="  & CInt(iaryBadWordID) & ");"
		
	'Write the updated date of last post to the database
	db.execute(strSQL)	
Next
	 
'Reset server variable
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Return to the bad word admin page
response.redirect("bad_word_filter_configure.aspx"
%>
