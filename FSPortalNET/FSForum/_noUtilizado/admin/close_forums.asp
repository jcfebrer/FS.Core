

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
Dim blnLocked		'Set to true if the forums are locked

'Read in the details from the form
blnLocked = CBool(Request.Form("lock"))



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
If Request.Form("postBack") Then

	With rsCommon
		'Update the recordset
		.Fields("Forums_closed") = blnLocked	
	
		'Update the database
		.Update
	
		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	'Update the application variable
	Application("blnForumClosed") = blnLocked
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false
End If

'Read in the deatils from the database
If NOT rsCommon.EOF Then

	'Read in the open forum status from the db
	blnLocked = CBool(rsCommon("Forums_closed"))
End If


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>
<title>Lock Forums</title>



     	

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Lock Forums</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  From here you can Lock Down the Board, this is useful for maintenance.</p>
 <table width="97%" border="0" cellspacing="1" cellpadding="4" bgcolor="#000000">
  <tr> 
   <td align="center" bgcolor="#CCCEE6" class="lgText"> Important - Please Read First!</td>
  </tr>
  <tr> 
   <td bgcolor="#EAEAF4"> 
    <p class="text">If you Lock the Forums the entire board will be locked and anyone entering any part of the board will receive the message, <em>'Sorry, the forums are presently 
     closed for maintenance'</em>.<br />
     <br />
     This will mean no-one will be able to register, post messages, send private messages, or even login.<br />
     <br />
     This will also mean that if you logout of this admin area <span class="bold">you will NOT be able to login through the main forum</span>.<br />
     <br />
     To log back into the admin area to un-lock the forums, point your browser at this admin folder and login into the admin area through the admin area login page.</p></td>
  </tr>
 </table>
</div><br />
<form method="post" name="frmLock" action="close_forums.aspx">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr> 
   <td width="560"> <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td colspan="2" class="text">*Indicates required fields</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="59%"  height="7" align="left" class="text">Lock Forums<br />
       <span class="smText">Please read the instructions at the top of the page for further info on logging back in to un-lock the forums.</span></td>
      <td width="41%" height="7" valign="top" class="text">Locked 
       <input type="radio" name="lock" value="True" <% If blnLocked = True Then response.write("checked" %>>
       &nbsp; &nbsp;Un-Locked 
       <input type="radio" name="lock" value="False" <% If blnLocked = False Then response.write("checked" %>> </td>
     </tr>
     <tr bgcolor="#F5F5FA" align="center"> 
      <td height="2" colspan="2" valign="top" > 
       <p> 
        <input type="hidden" name="postBack" value="true">
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