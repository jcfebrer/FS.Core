

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'****************************************************************************************
'**  Copyright Notice
'**
'**  Web Wiz Guide - Web Wiz Forums
'**
'**  Copyright 2001-2004 Bruce Corkhill All Rights Reserved.
'**
'**  This program is free software; you can modify (at your own risk) any part of it
'**  under the terms of the License that accompanies this software and use it both
'**  privately and commercially.
'**
'**  All copyright notices must remain in tacked in the scripts and the
'**  outputted HTML.
'**
'**  You may use parts of this program in your own private work, but you may NOT
'**  redistribute, repackage, or sell the whole or any part of this program even
'**  if it is modified or reverse engineered in whole or in part without express
'**  permission from the Usuarios.
'**
'**  You may not pass the whole or any part of this application off as your own work.
'**
'**  All links to Web Wiz Guide and powered by logo's must remain unchanged and in place
'**  and must remain visible when the pages are viewed unless permission is first granted
'**  by the copyright holder.
'**
'**  This program is distributed in the hope that it will be useful,
'**  but WITHOUT ANY WARRANTY; without even the implied warranty of
'**  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR ANY OTHER
'**  WARRANTIES WHETHER EXPRESSED OR IMPLIED.
'**
'**  You should have received a copy of the License along with this program;
'**  if not, write to:- Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom.
'**
'**
'**  No official support is available for this program but you may post support questions at: -
'**  http://www.webwizguide.info/forum
'**
'**  Support questions are NOT answered by email ever!
'**
'**  For correspondence or non support questions contact: -
'**  info@webwizguide.info
'**
'**  or at: -
'**
'**  Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom
'**
'****************************************************************************************

'Set the response buffer to true
Response.Buffer = True


'Dimension variables
Dim strMode		'Holds the mode of the page, set to true if changes are to be made to the database
Dim blnSuspended	'Set to true if new registrations are suspended

'Read in the details from the form
blnSuspended = CBool(Request.Form("sus"))
strMode = Request.Form("mode")



'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If the user is changing changing the lock status then update the database
If strMode = "postBack" Then

	With rsCommon
		'Update the recordset
		.Fields("Reg_closed") = blnSuspended	
	
		'Update the database
		.Update
	
		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	'Update the application variable
	Application("blnRegistrationSuspeneded") = blnSuspended	
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false
End If

'Read in the deatils from the database
If NOT rsCommon.EOF Then

	'Read in the reg suspend status form the db
	blnSuspended = CBool(rsCommon("Reg_closed"))
End If


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>
<title>Suspend New Registrations</title>



     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Suspend New Registrations</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  From here you suspend new sign up's so no new users can register to use the forum.</p>
</div>
<form method="post" name="frmSus" action="suspend_registration.aspx">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr> 
   <td width="560"> <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td colspan="2" class="text">*Indicates required fields</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="7" align="left" class="text">Suspend New Registrations</td>
      <td width="41%" height="7" valign="top" class="text">Yes 
       <input type="radio" name="sus" value="True" <% If blnSuspended = True Then response.write("checked" %>>
       &nbsp; No 
       <input type="radio" name="sus" value="False" <% If blnSuspended = False Then response.write("checked" %>> </td>
     </tr>
     <tr bgcolor="#F5F5FA" align="center"> 
      <td height="2" colspan="2" valign="top" > 
       <p> 
        <input type="hidden" name="mode" value="postBack">
        <input type='submit' name="Submit" value="Submit">
       </p></td>
     </tr>
    </table></td>
  </tr>
 </table>
</form>
<br />
</body>
</html>