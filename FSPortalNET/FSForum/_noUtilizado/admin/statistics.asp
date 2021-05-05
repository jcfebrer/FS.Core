

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
Dim intForum		'Holds the number of fourms
Dim lngTopic		'Holds the number of topics
Dim dtmTopic		'Holds the date of the last topic
Dim lngPost		'Holds the number of posts
Dim dtmPost		'Holds the date of the last post
Dim lngPm		'Holds the number of private messages
Dim dtmPm		'Holds the date of the last private message
Dim lngPoll		'Holds the number of polls
Dim intActiveUsers	'Holds the number of active users
Dim intGroups		'Holds the number of groups
Dim lngMember		'Holds the number of members
Dim dtmMember		'Holds the date of the last members signup
Dim lngUserID		'Holds the active users ID
Dim strActUser		'Holds the active users usuario
Dim strForumName 	'Holds the forum name
Dim intGuestNumber	'Holds the Guest Number
Dim intActiveGuests	'Holds the number of active guests
Dim intActiveMembers	'Holds the nunber of active members
Dim strBrowserUserType	'Holds the users browser type
Dim strOS		'Holds the users OS
Dim dtmLastActive	'Holds the last active date
Dim dtmLoggedIn		'Holds the date the user logged in
Dim blnActiveUsers	'Set to true if active users is enabled
Dim intArrayPass		'Holds array iteration possition
Dim saryActiveUsers


'Initilise variables
intActiveMembers = 0
intActiveGuests = 0
intActiveUsers = 0
intGuestNumber = 0
intForum = 0
lngTopic = 0
lngPost = 0
lngPm = 0
intActiveUsers = 0
intGroups = 0
lngMember = 0



'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If

'Query the database
rsCommon=db.execute(strSQL)


'Read in ifg active users is anbaled
If NOT rsCommon.EOF Then portal.variablesForum.blnActiveUsers = CBool(rsCommon("Active_users"))

'Clean up
rsCommon.Close




'******************************************
'***	    Read in the Counts		***
'******************************************

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.No_of_topics, " & portal.variablesForum.strDbTable & "Forum.No_of_posts FROM " & portal.variablesForum.strDbTable & "Forum;"

'Query the database
rsCommon=db.execute(strSQL)

'Get the number of topics posts and forums
Do While NOT rsCommon.EOF

 	'Count the number of forums
 	intForum = intForum + 1

 	'Count the number of topics
 	lngTopic = lngTopic + CLng(rsCommon("No_of_topics"))

 	'Count the number of posts
 	lngPost = lngPost + CLng(rsCommon("No_of_posts"))

 	'Move to the next record
 	rsCommon.MoveNext
Loop

'Clean up
rsCommon.Close



'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT Count(" & "Usuarios.UsuarioID) AS CountUsuarios FROM " & "Usuarios;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then lngMember = CLng(rsCommon("CountUsuarios"))

'Clean up
rsCommon.Close



'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT Count(" & portal.variablesForum.strDbTable & "PMMessage.PM_ID) AS CountPm FROM " & portal.variablesForum.strDbTable & "PMMessage;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then lngPm = CLng(rsCommon("CountPm"))

'Clean up
rsCommon.Close


'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT Count(" & portal.variablesForum.strDbTable & "Poll.Poll_ID) AS CountPoll FROM " & portal.variablesForum.strDbTable & "Poll;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then lngPoll = CLng(rsCommon("CountPoll"))

'Clean up
rsCommon.Close



'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT Count(" & portal.variablesForum.strDbTable & "Group.Group_ID) AS CountGroup FROM " & portal.variablesForum.strDbTable & "Group;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then intGroups = CLng(rsCommon("CountGroup"))

'Clean up
rsCommon.Close



'******************************************
'***	    	Read in Dates		***
'******************************************

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Topic.Start_date FROM " & portal.variablesForum.strDbTable & "Topic ORDER BY " & portal.variablesForum.strDbTable & "Topic.Start_date DESC;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then dtmTopic = CDate(rsCommon("Start_date"))

'Clean up
rsCommon.Close



'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Thread.Message_date FROM " & portal.variablesForum.strDbTable & "Thread ORDER BY " & portal.variablesForum.strDbTable & "Thread.Message_date DESC;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then dtmPost = CDate(rsCommon("Message_date"))

'Clean up
rsCommon.Close



'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 1 " & "Usuarios.FechaCreacion FROM " & "Usuarios ORDER BY " & "Usuarios.FechaCreacion DESC;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then dtmMember = CDate(rsCommon("FechaCreacion"))

'Clean up
rsCommon.Close




'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date FROM " & portal.variablesForum.strDbTable & "PMMessage ORDER BY " & portal.variablesForum.strDbTable & "PMMessage.PM_Message_Date DESC;"

'Query the database
rsCommon=db.execute(strSQL)

'Read in the count
If NOT rsCommon.EOF Then dtmPm = CDate(rsCommon("PM_Message_Date"))

'Clean up
rsCommon.Close


%>
<html>
<head>
<title>Forum Statistics</title>



     	
			
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
<meta http-equiv="refresh" content="60">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Forum Statistics</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  Below is a list of statistics for the board.</p>
</div>
 <table width="600" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td>
    <table width="100%" border="0" align="center" cellpadding="4" cellspacing="1">
    <tr align="left" bgcolor="#CCCEE6">
      <td colspan="4" class="tHeading">Forum Statistics</td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">Number of Forums<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = intForum %>
     </td>
      
     <td width="26%" valign="top" class="text">&nbsp;</td>
      
     <td width="32%" height="12" valign="top" class="text">&nbsp; </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">Number of Topics<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = lngTopic %>
     </td>
      
     <td width="26%" valign="top" class="bold">Last New Topic</td>
      
     <td width="32%" height="12" valign="top" class="text"> 
      <% = FormatDateTime(dtmTopic, vbLongDate) & ", " &  FormatDateTime(dtmTopic, vbShortTime) %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td width="31%"  height="12" align="left" class="bold">Number of Posts<span class="smText"></span></td>
      
     <td width="11%" valign="top" class="text"> 
      <% = lngPost %>
     </td>
      
     <td width="26%" valign="top" class="bold">Last New Post</td>
      
     <td width="32%" height="12" valign="top" class="text"> 
      <% = FormatDateTime(dtmPost, vbLongDate) & ", " &  FormatDateTime(dtmPost, vbShortTime) %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Number of Members</td>
      
     <td valign="top" class="text"> 
      <% = lngMember %>
     </td>
      
     <td valign="top" class="bold">Last New Member</td>
      
     <td height="2" valign="top" class="text"> 
      <% = FormatDateTime(dtmMember, vbLongDate) & ", " &  FormatDateTime(dtmMember, vbShortTime) %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Number of Private Messages</td>
      
     <td valign="top" class="text"> 
      <% = lngPm %>
     </td>
      
     <td valign="top" class="bold">Last Private Message</td>
      
     <td height="2" valign="top" class="text"> 
      <% = FormatDateTime(dtmPm, vbLongDate) & ", " &  FormatDateTime(dtmPm, vbShortTime) %>
     </td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Number of Polls</td>
      
     <td valign="top" class="text"> 
      <% = lngPoll %>
     </td>
      
     <td valign="top" class="text">&nbsp;</td>
      
     <td height="2" valign="top" class="text">&nbsp;</td>
     </tr>
     
    <tr  bgcolor="#F5F5FA"> 
     <td  height="2" align="left" class="bold">Number of User Groups</td>
      
     <td valign="top" class="text"> 
      <% = intGroups %>
     </td>
      
     <td valign="top" class="text">&nbsp;</td>
      
     <td height="2" valign="top" class="text">&nbsp;</td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center">
<br /><br />
<%


'******************************************
'***	    Active users list		***
'******************************************

'If active sers is ebaled show the table
If portal.variablesForum.blnActiveUsers Then
	
	
	'Initialise  the array from the application veriable
	If IsArray(Application("saryAppActiveUsers")) Then 
		
		'Place the application level active users array into a tmporary static array
		saryActiveUsers = Application("saryAppActiveUsers")
	
	
	'Else Initialise the an empty array
	Else
		ReDim saryActiveUsers(7,1)
	End If


	'Get the number of active users
	'Get the active users online
	For intArrayPass = 1 To UBound(saryActiveUsers, 2)
		
		'If this is a guest user then increment the number of active guests veriable
		If saryActiveUsers(1, intArrayPass) = 2 Then 	
				
			intActiveGuests = intActiveGuests + 1
		End If
			
	Next 
	
	'Calculate the number of members online and total people online
	intActiveUsers = UBound(saryActiveUsers, 2)
	intActiveMembers = intActiveUsers - intActiveGuests

	Response.Write("    	<span class=""text"">There are currently " & intActiveUsers & " Active Users on-line, "  & intActiveGuests & " Guest(s) and " & intActiveMembers & " Member(s)</span><br />")
%>
    <br />
    <table width="600" border="0" cellspacing="0" cellpadding="1" bgcolor="#000000" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr>
     <td bgcolor="#FFFFFF">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="#FFFFFF">
        <tr>
         <td bgcolor="#CCCEE6" width="88" class="tHeading">usuario</td>
         <td bgcolor="#CCCEE6" width="110" class="tHeading">Logged In</td>
         <td bgcolor="#CCCEE6" width="100" class="tHeading">Last Active</td>
         <td bgcolor="#CCCEE6" width="81" class="tHeading">Active</td>
         <td bgcolor="#CCCEE6" width="88" align="left" class="tHeading">OS</td>
         <td bgcolor="#CCCEE6" width="88" align="left" class="tHeading">Browser</td>
        </tr>
        <%
        
        'Sort the active users array
	Call SortActiveUsersList(saryActiveUsers)

	'display the active users
	For intArrayPass = 1 To UBound(saryActiveUsers, 2)
	
		'Read in the details from the rs
		lngUserID = saryActiveUsers(1, intArrayPass)
		strActUser = saryActiveUsers(2, intArrayPass)
		dtmLoggedIn = saryActiveUsers(3, intArrayPass)
		dtmLastActive = saryActiveUsers(4, intArrayPass)
		strOS = saryActiveUsers(5, intArrayPass)
		strBrowserUserType = saryActiveUsers(6, intArrayPass)
	


				'Write the HTML of the Topic descriptions as hyperlinks to the Topic details and message
			%>
        <tr bgcolor="#F5F5FA"> 
         <td width="88" height="24" class="text"><%

	         'If the user is a Guest then display them as a Guest
	         If lngUserID = 2 Then

	         	'Add 1 to the Guest number
	         	intGuestNumber = intGuestNumber + 1

	         	'Display the User as Guest
	         	Response.Write("Guest "& intGuestNumber)

	         'Else display the users name
	         Else
	          	Response.Write("<font color=""red"">" & strActUser & "</font>")

	        End If
        %>
         </td>
         <td class="smText"><% Response.Write(FormatDateTime(dtmLoggedIn, vbLongDate) & " at&nbsp;" & FormatDateTime(dtmLoggedIn, vbShortTime))  %></td>
         <td class="smText"><% Response.Write(FormatDateTime(dtmLastActive, vbLongDate) & " at&nbsp;" & FormatDateTime(dtmLastActive, vbShortTime)) %></td>
         <td class="text"> <% = DateDiff("n", dtmLoggedIn, dtmLastActive) %>&nbsp;Minutes</td>
         <td class="text" nowrap="nowrap"><% = strOS %></td>
         <td class="text" nowrap="nowrap"><% = strBrowserUserType %></td>
        </tr>
        <%

	Next
%>
       </table>
     </tr>
    </table>
    </td>
 </tr>
</table>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
 <tr>
  <td align="center" class="text"><br />
   This data is based on users active over the past ten minutes</td>
 </tr>
</table>
<br />
<br />
<%
End If


'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
</div>
<br />
</body>
</html>