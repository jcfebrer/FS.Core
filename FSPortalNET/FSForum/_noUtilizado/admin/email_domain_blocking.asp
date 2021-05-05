

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True


'Dimension variables
Dim rsSelectForum	'Holds the db recordset
Dim strBlockEmail		'Holds the Email address to block
Dim strBlockedEmailList	'Holds the Email addresses in the blocked list
Dim lngBlockedEmailID	'Holds the ID number of the blcoked db record
Dim laryCheckedEmailAddrID	'Holds the array of Email addresses to be ditched




'Run through till all checked Email addresses are deleted
For each laryCheckedEmailAddrID in Request.Form("chkDelete")


	'Here we use the less effiecient ADO to delete from the database this way we can throw in a requery while we wait for slow old MS Access to catch up

	'Delete the Email address from the database	
	strSQL = "SELECT * FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.Ban_ID="  & laryCheckedEmailAddrID & ";"
	
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



'Read in all the blocked Email address from the database

'Initalise the strSQL variable with an SQL statement to query the database to count the number of topics in the forums
strSQL = "SELECT " & portal.variablesForum.strDbTable & "BanList.Ban_ID, " & portal.variablesForum.strDbTable & "BanList.Email FROM " & portal.variablesForum.strDbTable & "BanList WHERE " & portal.variablesForum.strDbTable & "BanList.Email Is Not Null;"

'Set the cursor	type property of the record set	to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)



'If this is a post back then  update the database
If Request.Form("Email") <> "" Then

	'Read in the Email address to block
	strBlockEmail = Trim(Mid(Request.Form("Email"), 1, 30))

	'Update the recordset
	With rsCommon
	
		.AddNew

		'Update	the recorset
		.Fields("Email") = strBlockEmail

		'Update db
		.Update

		'Re-run the query as access needs time to catch up
		.ReQuery

	End With
End If

%>
<html>
<head>

<title>Email Address Blocking</title>

<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for an Email address
	if (document.frmEmailadd.Email.value==""){
		alert("Please enter an Email address or domain");
		document.frmEmailadd.Email.focus();
		return false;
	}

	return true;
}
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Email Address Blocking</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">From here you can Block email addresses or email domains.<br />
 <br />
 This function is only really usefuil if you have email activation enabled as it will prevent anyopne with a blocked email address registering on the forum with that email address.<br />
 <br />
 Email domains can be blocked, eg. *@hotmail.com would stop anyone with a hotmail email address from registering on the board with that email address.</span><br />
</div>
<br />
<form name="frmEmailList" method="post" action="email_domain_blocking.aspx">
 <table width="450" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
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
      <td colspan="2" bgcolor="#CCCEE6" class="tHeading">Blocked Email Address List</td>
     </tr><%
'Display the Email blcok list
If rsCommon.EOF Then 
		
	'Disply no entires forun
	Response.Write("<td colspan=""2"" align=""center"" bgcolor=""#FFFFFF"" class=""bold"">You have no blocked email address</td>")
	
'Else disply the Email block list
Else
	
	'Loop through the recordset
	Do While NOT rsCommon.EOF
	
     		'Read in the topic details
     		lngBlockedEmailID = CLng(rsCommon("Ban_ID"))
		strBlockedEmailList = rsCommon("Email")
     
     %>
     <tr bgcolor="#FFFFFF">
      <td width="3%" bgcolor="#FFFFFF"><input type='checkbox' name="chkDelete" value="<% = lngBlockedEmailID %>"></td>
      <td bgcolor="#FFFFFF" class="text"><% = strBlockedEmailList %></td>
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
      <td valign="top" colspan="2" >
        <input type='submit' name="Submit" value="Remove Email Address or Range">
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
<form name="frmEmailadd" method="post" action="email_domain_blocking.aspx" onSubmit="return CheckForm();">
 <table width="450" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000" align="center">
  <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor=""#FFFFFF">
    <tr>
     <td bgcolor=""#FFFFFF">
   <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
   <td>
    <table border="0" align="center" cellpadding="4" cellspacing="1" width="450">
           <tr align="left" bgcolor="#CCCEE6"> 
            <td colspan="2" class="tHeading"> Block Email Address or Domain</td>
     </tr>
           <tr bgcolor="#F5F5FA"> 
            <td colspan="2" align="center" class="smText"> The * wildcard character can be used to block email domains. <br />
             eg. To block users with a yahoo.com email address you would use. eg. *@yahoo.com</td>
     </tr>
           <tr bgcolor="#F5F5FA"> 
            <td width="191" align="right" class="text"> Email Address or domain:</td>
            <td width="240"> 
             <input name="Email" type='text' id="Email" size="25" maxlength="50"/></td>
     </tr>
           <tr bgcolor="#F5F5FA" align="center"> 
            <td colspan="2" valign="top"> 
             <input type='submit' name="Submit" value="Block Email Address or Range">
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