

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


'Dimension variables
Dim intSpamTimeLimitSeconds	'Holds the number of secounds between posts
Dim intSpamTimeLimitMinutes	'Holds the number of minutes the user can post five posts in
      
      

'Read in the users colours for the forum
intSpamTimeLimitSeconds = CInt(Request.Form("seconds"))
intSpamTimeLimitMinutes = CInt(Request.Form("minutes"))

	
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'we only want one record so set the maximum records to 1
rsCommon.MaxRecords = 1
	
'Query the database
rsCommon=db.execute(strSQL)

'If the user is changing tthe colours then update the database
If Request.Form("postBack") Then
	
	'Update the recordset
	rsCommon.Fields("Spam_seconds") = intSpamTimeLimitSeconds
	rsCommon.Fields("Spam_minutes") = intSpamTimeLimitMinutes
				
	'Update the database with the new user's colours
	rsCommon.Update
		
	'Re-run the query to read in the updated recordset from the database
	rsCommon.Requery
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false	
End If

'Read in the forum colours from the database
If NOT rsCommon.EOF Then
	
	'Read in the colour info from the database
	intSpamTimeLimitSeconds = CInt(rsCommon.Fields("Spam_seconds"))
	intSpamTimeLimitMinutes = CInt(rsCommon.Fields("Spam_minutes"))
End If


'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>
<title>Anti-Spam Configuration</title>



     	
		
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Anti-Spam Configuration</span><br />
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">These Anti-Spam measures are to stop a spammer going on a spree and posting thousands of useless or abusive messages on your forum in a matter of minutes. The higher you set these times 
 the harder it is for a spammer, but bewared set them to high and you may block legitimate forum usem_rs.</span></div>
<br />
<form method="post" name="frmConfiguration" action="spam_configure.aspx">
 <table border="0" align="center" cellpadding="4" cellspacing="1" width="560" bgcolor="#000000">
  <tr bgcolor="#CCCEE6"> 
   <td colspan="2" align="left" class="lgText">Anti-Spam Configuration</td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Time Interval Between Posts:<br />
    <span class="smText">This how long a forum member needs to wait before posting a new post.<br />
    If this is set to high it will stop forum members from posting another post straight after their last post if they suddenly realize they forgot to mention something.</span></td>
   <td valign="top"> 
    <select name="seconds">
     <option value="0" <% If intSpamTimeLimitSeconds = 0 Then Response.Write("selected") %>>Off</option>
     <option value="10" <% If intSpamTimeLimitSeconds = 10 Then Response.Write("selected") %>>10 Seconds</option>
     <option value="15" <% If intSpamTimeLimitSeconds = 15 Then Response.Write("selected") %>>15 Seconds</option>
     <option value="20" <% If intSpamTimeLimitSeconds = 20 Then Response.Write("selected") %>>20 Seconds</option>
     <option value="25" <% If intSpamTimeLimitSeconds = 25 Then Response.Write("selected") %>>25 Seconds</option>
     <option value="30" <% If intSpamTimeLimitSeconds = 30 Then Response.Write("selected") %>>30 Seconds</option>
     <option value="45" <% If intSpamTimeLimitSeconds = 45 Then Response.Write("selected") %>>45 Seconds</option>
     <option value="60" <% If intSpamTimeLimitSeconds = 60 Then Response.Write("selected") %>>1 minute</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">A Forum Member can Post a Maximum of 5 Posts In:<br />
    <span class="smText">This is the amount of time a Forum Member can post 5 Posts, once this is reached the forum member will have to wait till at least one of their 5 posts is is no longer in this time 
    limit.</span></td>
   <td valign="top"> 
    <select name="minutes">
     <option value="0" <% If intSpamTimeLimitMinutes = 0 Then Response.Write(" selected") %>>Off</option>
     <option value="1" <% If intSpamTimeLimitMinutes = 1 Then Response.Write(" selected") %>>1 minute</option>
     <option value="2" <% If intSpamTimeLimitMinutes = 2 Then Response.Write(" selected") %>>2 minutes</option>
     <option value="3" <% If intSpamTimeLimitMinutes = 3 Then Response.Write(" selected") %>>3 minutes</option>
     <option value="4" <% If intSpamTimeLimitMinutes = 4 Then Response.Write(" selected") %>>4 minutes</option>
     <option value="5" <% If intSpamTimeLimitMinutes = 5 Then Response.Write(" selected") %>>5 minutes</option>
     <option value="6" <% If intSpamTimeLimitMinutes = 6 Then Response.Write(" selected") %>>6 minutes</option>
     <option value="7" <% If intSpamTimeLimitMinutes = 7 Then Response.Write(" selected") %>>7 minutes</option>
     <option value="8" <% If intSpamTimeLimitMinutes = 8 Then Response.Write(" selected") %>>8 minutes</option>
     <option value="9" <% If intSpamTimeLimitMinutes = 9 Then Response.Write(" selected") %>>9 minutes</option>
     <option value="10" <% If intSpamTimeLimitMinutes = 10 Then Response.Write(" selected") %>>10 minutes</option>
     <option value="15" <% If intSpamTimeLimitMinutes = 15 Then Response.Write(" selected") %>>15 minutes</option>
     <option value="20" <% If intSpamTimeLimitMinutes = 20 Then Response.Write(" selected") %>>20 minutes</option>
     <option value="25" <% If intSpamTimeLimitMinutes = 25 Then Response.Write(" selected") %>>25 minutes</option>
     <option value="30" <% If intSpamTimeLimitMinutes = 30 Then Response.Write(" selected") %>>30 minutes</option>
     <option value="40" <% If intSpamTimeLimitMinutes = 40 Then Response.Write(" selected") %>>40 minutes</option>
     <option value="50" <% If intSpamTimeLimitMinutes = 50 Then Response.Write(" selected") %>>50 minutes</option>
     <option value="60" <% If intSpamTimeLimitMinutes = 60 Then Response.Write(" selected") %>>1 hour</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA" align="center"> 
   <td colspan="2" valign="top" class="arial"> 
    <p> 
     <input type="hidden" name="postBack" value="true">
     <input type='submit' name="Submit" value="Update Spam Configuration">
     <input type="reset" name="Reset" value="Reset Form">
    </p></td>
  </tr>
 </table>
</form>
<div align="center"><br />
 <span class="text"><b>Please Note</b><br />
 The Anti-Spam measures do not affect the admin accounts, so you can still post as many messages as you like.<br />
 <br />
 <b>Guest Account Enabled: -</b> If you have Guest posting enabled then you have no protection against a spammer.<br />
 <br />
 <br />
 <b>These measures are not fool proof but should make it harder for a spammer to attack your forum.</b></span><br />
</div>
</body>
</html>
