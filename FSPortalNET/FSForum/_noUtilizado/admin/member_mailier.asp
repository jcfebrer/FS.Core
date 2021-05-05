

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

'Set the response buffer to false as we may need to puase while the e-mails are being sent
Response.Buffer = True

'Dimension variables
Dim strGroupName
Dim intSelGroupID

%>
<html>
<head>
<title>Mass Mailier</title>



     	

<script  language="javascript">
//Function to check form is filled in correctly before submitting
function CheckForm () {

	var errorMsg = "";

	//Check for a subject
	if (document.frmEmailier.subject.value==""){
		errorMsg += "\n\tSubject \t\t- Enter a Subject for the Email";
	}

	//Check for message
	if (document.frmEmailier.message.value==""){
		errorMsg += "\n\tMessage \t\t- Enter a Message for the Email";
	}

	//If there is aproblem with the form then display an error
	if (errorMsg != ""){
		msg = "_______________________________________________________________\n\n";
		msg += "The form has not been submitted because there are problem(s) with the form.\n";
		msg += "Please correct the problem(s) and re-submit the form.\n";
		msg += "_______________________________________________________________\n\n";
		msg += "The following field(s) need to be corrected: -\n";

		errorMsg += alert(msg + errorMsg + "\n\n");
		return false;
	}

	return true;
}
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Member Email Messenger</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">From here you can send email's to all your forum members or those that are part of a specific User Group, that have entered valid email addresses in their profiles.</span><br />
 <br />
 <table width="97%" border="0" cellspacing="1" cellpadding="4" bgcolor="#000000">
  <tr> 
   <td align="center" bgcolor="#CCCEE6" class="lgText"> Important - Please Read First!</td>
  </tr>
  <tr> 
   <td bgcolor="#EAEAF4"> 
    <p class="text">To be able to send emails you need to have a web host that supports sending emails, first make sure that you have correctly setup your email settings given to you by your web host in 
     the <a href="email_notify_configure.aspx" target="_self">Email Setup and Configuration</a> page, or you may get a server error. </p>
    <p class="lgText">Many hosts will suspend or delete your hosting account for sending bulk email, so check with your web host first that you are allowed to send bulk email!!</p></td>
  </tr>
 </table><%

If blnMassMailier = False then
%> 
 <br />
 <br />
 <br />
 <span class="lgText">Sorry this function has been disabled!</span><%

Else

%> 
 <br />
</div>
<form method="post" name="frmEmailier" action="member_mailier_send.aspx" onSubmit="return CheckForm();" onReset="return confirm('Are you sure you want to clear the form?');">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="277">
  <tr> 
   <td height="234"> <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td colspan="2" class="text">*Indicates required fields</td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td width="23%" align="right" class="text">Recipients*:<br /> </td>
      <td width="77%" valign="top"> 
       <select name="group">
        <option value="0" selected>All Users</option>
        <%

'Read in the category name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.Group_ID, " & portal.variablesForum.strDbTable & "Group.Name FROM " & portal.variablesForum.strDbTable & "Group ORDER BY " & portal.variablesForum.strDbTable & "Group.Group_ID ASC;"

'Query the database
rsCommon=db.execute(strSQL)

'Loop through all the categories in the database
Do while NOT rsCommon.EOF

	'Read in the deatils for the category
	strGroupName = rsCommon("Name")
	intSelGroupID = CInt(rsCommon("Group_ID"))

	'Display a link in the link list to the cat
	Response.Write (vbCrLf & "		<option value=""" & intSelGroupID & """")
	Response.Write(">" & strGroupName & "</option>")


	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop


'Release Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
       </select> </td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td align="right" class="text">Email Format:</td>
      <td valign="top"> 
       <select name="mailFormat" id="mailFormat">
        <option value="false" selected>Plain Text</option>
        <option value="true">HTML</option>
       </select></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td align="right" class="text">Subject*:</td>
      <td valign="top"> 
       <input name="subject" type='text' id="subject" size="30" maxlength="45"></td>
     </tr>
     <tr  bgcolor="#F5F5FA"> 
      <td align="right" valign="top" class="text">Message*:<br /> <br />
       <span class="smText">Please note that you can not use Forum Codes.</span> </td>
      <td valign="top"> 
       <textarea name="message" cols="45" rows="10" id="message"></textarea></td>
     </tr>
     <tr align="center"  bgcolor="#F5F5FA"> 
      <td colspan="2" class="text"> 
       <input name="submit" type='submit' id="submit" value="Send Email"> &nbsp; <input type="reset" name="Reset" value="Clear Form"></td>
     </tr>
    </table></td>
  </tr>
 </table>
</form><%

End If

%>
<br />
</body>
</html>