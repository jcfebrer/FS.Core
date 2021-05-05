

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim rsSelectForum	'Holds the db recordset
Dim strBlockIP		'Holds the IP address to block
Dim strBlockedIPList	'Holds the IP addresses in the blocked list
Dim lngBlockedIPID	'Holds the ID number of the blcoked db record
Dim laryCheckedIPAddrID	'Holds the array of IP addresses to be ditched





'Run through till all checked IP addresses are deleted
For each laryCheckedIPAddrID in Request.Form("chkDelete")


	'Here we use the less effiecient ADO to delete from the database this way we can throw in a requery while we wait for slow old MS Access to catch up

	'Delete the IP address from the database	
	strSQL = "SELECT * FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.Ban_ID="  & laryCheckedIPAddrID & ";"
	
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

%>
<html>
<head>

<title>IP Blocking</title>

<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for an IP address
	if (document.frmIPadd.IP.value==""){
		alert("Please enter an IP address or range");
		document.frmIPadd.IP.focus();
		return false;
	}

	return true;
}
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">IP Address Blocking</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">From here you can Block individual IP Addresses or Ranges.<br />
 <br />
 Anyone falling into a block IP address or range, will find many of the functions of the board disabled, including registering, and posting.<br />
 <br />
 Be careful when blocking IP addresses as you may block legitimate usem_rs. Many people share the same IP address, like AOL users, blocking one may block another 500,000 users from using your board.</span><br />
</div>
<br />
<form name="frmIPList" method="post" action="ip_blocking.aspx">
 <table width="350" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#000000">
    <tr>
     <td bgcolor="#000000">
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="100%">
     <tr align="left" bgcolor="#FFFFFF">
      <td colspan="2" bgcolor="#CCCEE6" class="tHeading">Blocked IP Address List</td>
     </tr><%
'Display the IP blcok list
If rsCommon.EOF Then 
		
	'Disply no entires forun
	Response.Write("<td colspan=""2"" align=""center"" bgcolor=""#FFFFFF"" class=""bold"">You have no blocked IP address</td>")
	
'Else disply the IP block list
Else
	
	'Loop through the recordset
	Do While NOT rsCommon.EOF
	
     		'Read in the topic details
     		lngBlockedIPID = CLng(rsCommon("Ban_ID"))
		strBlockedIPList = rsCommon("IP")
     
     %>
     <tr bgcolor="#FFFFFF">
      <td width="3%" bgcolor="#FFFFFF"><input type='checkbox' name="chkDelete" value="<% = lngBlockedIPID %>"></td>
      <td bgcolor="#FFFFFF" class="text"><% = strBlockedIPList %></td>
     </tr><%
     

		'Move to the next record in the recordset
		rsCommon.MoveNext
	Loop
End If

'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
          </select></td>
     </tr>
     <tr bgcolor="#FFFFFF"  align="center">
      <td valign="top" colspan="2">
        <input type='submit' name="Submit" value="Remove IP Address or Range">
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
<form name="frmIPadd" method="post" action="ip_blocking.aspx" onSubmit="return CheckForm();">
 <table width="350" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
  <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor=""#FFFFFF">
    <tr>
     <td bgcolor=""#FFFFFF">
   <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="350">
           <tr align="left" bgcolor="#CCCEE6"> 
            <td colspan="2" class="tHeading"> 
             Block IP Address or Range
            </td>
     </tr>
           <tr bgcolor="#F5F5FA"> 
            <td colspan="2" align="center" class="smText"> The * wildcard character can be used to block IP ranges. <br />
             eg. To block the range '200.200.200.0 - 255' you would use '200.200.200.*' </td>
     </tr>
           <tr bgcolor="#F5F5FA"> 
            <td align="right" class="text"> IP Address/Range:</td>
            <td> 
             <input type='text' name="IP" size="20" maxlength="30"/></td>
     </tr>
           <tr bgcolor="#F5F5FA" align="center"> 
            <td colspan="2" valign="top"> 
             <input type='submit' name="Submit" value="Block IP Address or Range">
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
<br />
</body>
</html>