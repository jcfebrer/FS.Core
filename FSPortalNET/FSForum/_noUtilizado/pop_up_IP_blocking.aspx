

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/admin_language_file_inc.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim rsSelectForum	'Holds the db recordset
Dim lngTopicID 		'Holds the topic ID number to return to
Dim intForumID		'Holds the forum ID number
Dim strBlockIP		'Holds the IP address to block
Dim strBlockedIPList	'Holds the IP addresses in the blocked list
Dim lngBlockedIPID	'Holds the ID number of the blcoked db record
Dim laryCheckedIPAddrID	'Holds the array of IP addresses to be ditched




'Read in the message ID number to be deleted
lngTopicID = CLng(Request("TID"))


'If the person is not an admin or a moderator then send them away
If lngTopicID = "" Then
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing

	'Redirect
	Response.Redirect("default.aspx")
End If




'Initliase the SQL query to get the topic details from the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.* "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID = " & lngTopicID & ";"

'Query the database
rsCommon=db.execute(strSQL)

'If there is a record returened read in the forum ID
If NOT rsCommon.EOF Then
	portal.variablesForum.intForumID = CInt(rsCommon("Forum_ID"))
End If

'Close rs
rsCommon.Close



'Call the moderator function and see if the user is a moderator (if not logged in as an admin)
If portal.variablesForum.blnAdmin = False Then portal.variablesForum.blnModerator = isModerator(portal.variablesForum.intForumID, portal.variablesForum.intGroupID)


'Only run the following lines if this is a moderator or an admin
If portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator Then

	'Run through till all checked IP addresses are deleted
	For each laryCheckedIPAddrID in Request.Form("chkDelete")
	
	
		'Here we use the less effiecient ADO to delete from the database this way we can throw in a requery while we wait for slow old MS Access to catch up
	
		'Delete the IP address from the database	
		strSQL = "SELECT * FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.Ban_ID="  & CInt(laryCheckedIPAddrID) & ";"
		
		With rsCommon		
			'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
			.CursorType = 2
			
			'Set the Lock Type for the records so that the record set is only locked when it is updated
			.LockType = 3
			
			'Query the database
			=db.execute(strSQL)
			
			'Delete from the db
			If NOT .EOF Then .Delete
			
			'Requery
			.Requery
			
			'Close the recordset
			.Close
		End With
		
	Next
	
	
	
	'Read in all the blocked IP address from the database
	
	'Initalise the strSQL variable with an SQL statement to query the database to count the number of topics in the forums
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "BanList.Ban_ID, " & portal.variablesForum.strDbTable & "BanList.IP FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.IP Is Not Null;"
	
	'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
	rsCommon.CursorType = 2
	
	'Set the Lock Type for the records so that the record set is only locked when it is updated
	rsCommon.LockType = 3
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	
	
	'If this is a post back then  update the database
	If Request.Form("IP") <> "" Then
	
		'Read in the IP address to block
		strBlockIP = Trim(Mid(Request.Form("IP"), 1, 30))
	
		'Update the recordset
		With rsCommon
		
			.AddNew
	
			'Update	the recorset
			.Fields("IP") = strBlockIP
	
			'Update db
			.Update
	
			'Re-run the query as access needs time to catch up
			.ReQuery
	
		End With
	End If
End If
%>
<html>
<head>

<title>IP Blocking</title>

<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a subject
	if (document.frmIPadd.IP.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtErrorIPEmpty %>";
	}

	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine1 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine2 %>\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine %>\n\n";
		msg += "<% = portal.variablesForum.strTxtErrorDisplayLine3 %>\n";

		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}

	return true;
}
</script>

<!--#include file="includes/skin_file.aspx" -->

</head>
<body bgcolor="<% = strBgColour %>" text="<% = strTextColour %>" background="<% = strBgImage %>" marginheight="0" marginwidth="0" topmargin="0" leftmargin="0" OnLoad="self.focus();">
<div align="center" class="heading"><% = portal.variablesForum.strTxtIPBlocking %></div>
    <br /><%

'If there is no topic info returned by the rs then display an error message
If portal.variablesForum.blnAdmin = False AND portal.variablesForum.blnModerator = False Then

	'Close the rs
	rsCommon.Close

	Response.Write("<div align=""center"">")
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtAccessDenied & "</span><br /><br /><br />")
	Response.Write("</div>")

'Else display a form to allow updating of the topic
Else

'Display the IP blocked list	
%>
<form name="frmIPList" method="post" action="pop_up_IP_blocking.aspx">
 <table width="350" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="100%">
     <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>">
      <td colspan="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtBlockedIPList %></td>
     </tr><%
	'Display the IP blcok list
	If rsCommon.EOF Then 
		
		'Disply no entires forun
		Response.Write("<td colspan=""2"" align=""center"" bgcolor=""" & portal.variablesForum.strTableColour & """ background=""" & portal.variablesForum.strTableBgImage & """ class=""bold"">" & portal.variablesForum.strTxtYouHaveNoBlockedIpAddesses & "</td>")
	
	'Else disply the IP block list
	Else
	
		'Loop through the recordset
		Do While NOT rsCommon.EOF
	
     			'Read in the topic details
     			lngBlockedIPID = CLng(rsCommon("Ban_ID"))
			strBlockedIPList = rsCommon("IP")
     
     %>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td width="3%" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='checkbox' name="chkDelete" value="<% = lngBlockedIPID %>"></td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = strBlockedIPList %></td>
     </tr><%
     

			'Move to the next record in the recordset
			rsCommon.MoveNext
		Loop
	End If

'Reset Server Objects
rsCommon.Close
%>
          </select></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" align="center">
      <td valign="top" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>">
        <input type="hidden" name="TID" value="<% = lngTopicID %>">
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtRemoveIP %>">
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
 </td>
  </tr>
 </table>
 </td>
  </tr>
 </table>
</form>
<form name="frmIPadd" method="post" action="pop_up_IP_blocking.aspx" onSubmit="return CheckForm();">
   <table width="350" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="350">
     <tr align="left" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>">
      <td colspan="2" bgcolor="<% = portal.variablesForum.strTableTitleColour %>" background="<% = portal.variablesForum.strTableTitleBgImage %>" class="tHeading"><% = portal.variablesForum.strTxtBlockIPAddressOrRange %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td colspan="2" align="center" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="smText"><% = portal.variablesForum.strTxtBlockIPRangeWhildcardDescription %></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>">
      <td align="right" bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"><% = portal.variablesForum.strTxtIpAddressRange %>:</td>
      <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"><input type='text' name="IP" size="20" maxlength="30" value="<% = Server.HTMLEncode(Request.QueryString("IP")) %>"/></td>
     </tr>
     <tr bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" align="center">
      <td valign="top" colspan="2" background="<% = portal.variablesForum.strTableBgImage %>">
        <input type="hidden" name="TID" value="<% = lngTopicID %>">
        <input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtBlockIPAddressRange %>">
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
 </td>
  </tr>
 </table>
 </td>
  </tr>
 </table>
   </form><%

End If

%>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" align="center">
    <tr>
      <td align="center">
        <a href="JavaScript:onClick=window.close();"><% = portal.variablesForum.strTxtCloseWindow %></a>
      </td>
    </tr>
  </table>
<br /><br />   
<div align="center">
<%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


%>
</div>
</body>
</html>