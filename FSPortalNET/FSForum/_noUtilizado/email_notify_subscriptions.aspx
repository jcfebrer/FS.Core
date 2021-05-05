

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />

<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim intForumColourNumber	'Holds the number to calculate the table row colour
Dim rsForumSelect		'Recordset for teh forum selection	
Dim strCatName			'Holds the category name
Dim intCatID			'Holds the cat ID
Dim strSelectForumName		'Holds the forum  name
Dim intSelectForumID		'Holds the forum ID
Dim intForumID			'Holds forum ID
Dim lngEmailUserID		'Holds the user ID to look at email notification for
Dim strMode			'Holds the mode of the page
Dim dtmLastEntryDate		'Holds the last entry date

'Initialise variable
intForumColourNumber = 0



'If emial notify is not on then send them away
If portal.variablesForum.blnEmail = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("default.aspx")
End If


'If the user is not allowed then send them away
If portal.variablesForum.intGroupID = 2 OR blnActiveMember = False Then 
	'Clean up
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
	
	'Redirect
	Response.Redirect("insufficient_permission.aspx")
End If



'Read in the mode of the page
strMode = Trim(Mid(Request.QueryString("M"), 1, 1))


'If this is not an admin but in admin mode then see if the user is a moderator
If portal.variablesForum.blnAdmin = False AND strMode = "A" Then
	
	'Initalise the strSQL variable with an SQL statement to query the database
        strSQL = "SELECT " & portal.variablesForum.strDbTable & "Permissions.Moderate "
        strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Permissions "
        If portal.variablesForum.strDatabaseType = "SQLServer" Then
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = 1;"
        Else
                strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Permissions.Group_ID = " & portal.variablesForum.intGroupID & " AND  " & portal.variablesForum.strDbTable & "Permissions.Moderate = True;"
        End If
               

        'Query the database
         rsCommon=db.execute(strSQL)

        'If a record is returned then the user is a moderator in one of the forums
        If NOT rsCommon.EOF Then portal.variablesForum.blnModerator = True

        'Clean up
        rsCommon.Close
End If


'Get the user ID of the email notifications to look at
If (portal.variablesForum.blnAdmin OR (portal.variablesForum.blnModerator AND CLng(Request.QueryString("PF")) > 2)) AND strMode = "A" AND CLng(Request.QueryString("PF")) <> 2 Then
	
	lngEmailUserID = CLng(Request.QueryString("PF"))

'Get the logged in ID number
Else
	lngEmailUserID = portal.variablesForum.lngLoggedInUserID
End If

%>
<html>
<head>

<title>Email Notification Subscriptions</title>


     	

<script  language="javascript">






//Funtion to check or uncheck all topic delete boxes
function checkAllTopic(){

 	if (document.frmTopicDel.chkDelete.length > 0) {
  		for (i=0; i < document.frmTopicDel.chkDelete.length; i++){
   				document.frmTopicDel.chkDelete[i].checked = document.frmTopicDel.chkAll.checked;
  		}
 	}
 	else {
  		document.frmTopicDel.chkDelete.checked = document.frmTopicDel.chkAll.checked;
 	}
}


//Funtion to check or uncheck all forum delete boxes
function checkAllForum(){

 	if (document.frmForumDel.chkDelete.length > 0) {
  		for (i=0; i < document.frmForumDel.chkDelete.length; i++){
   			document.frmForumDel.chkDelete[i].checked = document.frmForumDel.chkAll.checked;
  		}
 	}
 	else {
  		document.frmForumDel.chkDelete.checked = document.frmForumDel.chkAll.checked;
 	}
}

//Function to check form is filled in correctly before submitting
function CheckForm () {
	
	var errorMsg = "";
	
	//Check for a forum
	if (document.frmBuddy.FID.value==""){
		errorMsg += "\n\t<% = portal.variablesForum.strTxtSelectForumErrorMsg %>";
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
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td class="heading"><% = portal.variablesForum.strTxtEmailNotificationSubscriptions %></td>
</tr>
 <tr> 
  <td width="60%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtEmailNotificationSubscriptions %>
  <td width="40%" align="right" nowrap=""nowrap""><a href="member_control_panel.aspx<% If strMode = "A" Then Response.Write("?PF=" & lngEmailUserID & "&amp;m=A") %>" target="_self"><img src="<% = portal.variablesForum.strImagePath %>cp_menu.gif" border="0" alt="<% = portal.variablesForum.strTxtMemberCPMenu %>"></a><a href="register.aspx<% If strMode = "A" Then Response.Write("?PF=" & lngEmailUserID & "&amp;m=A") %>" target="_self"><img src="<% = portal.variablesForum.strImagePath %>profile.gif" border="0" alt="<% = portal.variablesForum.strTxtEditProfile %>"></a><a href="email_notify_subscriptions.aspx<% If strMode = "A" Then Response.Write("?PF=" & lngEmailUserID & "&amp;m=A") %>" target"_self"><img src="<% = portal.variablesForum.strImagePath %>/email_notify.gif" border="0" alt="<% = portal.variablesForum.strTxtEmailNotificationSubscriptions %>"></a></td>
   </td>
  </tr>
</table>
<form name="frmForumDel" method="post" action="email_notify_remove.aspx<% If strMode = "A" Then Response.Write("?PF=" & lngEmailUserID & "&amp;m=A") %>" OnSubmit="return confirm('<% = portal.variablesForum.strTxtAreYouWantToUnsubscribe & " " & portal.variablesForum.strTxtForums %>')">
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="97%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"> <% = portal.variablesForum.strTxtForums & " " & portal.variablesForum.strTxtThatYouHaveSubscribedTo %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="3%" align="center" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><input type='checkbox' name="chkAll" onClick="checkAllForum();"></td>
       </tr><%
	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_name, " & portal.variablesForum.strDbTable & "EmailNotify.Forum_ID, " & portal.variablesForum.strDbTable & "EmailNotify.Watch_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Forum, " & portal.variablesForum.strDbTable & "EmailNotify "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Forum.Forum_ID=" & portal.variablesForum.strDbTable & "EmailNotify.Forum_ID AND " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & lngEmailUserID & " "
strSQL = strSQL & "ORDER BY " & portal.variablesForum.strDbTable & "Forum.Forum_Order;"


'Query the database
rsCommon=db.execute(strSQL)
    
'Check there are email subscriptions to show
If rsCommon.EOF Then

	'If there are no pm messages to display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""" & portal.variablesForum.strTableColour & """ background=""" & portal.variablesForum.strTableBgImage & """ colspan=""5"" class=""text"">" & portal.variablesForum.strTxtYouHaveNoSubToEmailNotify & "<input type=""hidden"" name=""chkDelete"" value=""-1""></td>"

'Else there the are email subs so show em
Else 	
	'Loop round to read in all the email notifys
	Do WHILE NOT  rsCommon.EOF 
	
		'Get the row number
		intForumColourNumber = intForumColourNumber + 1
	
	%><tr> 
          <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="97%" class="text"><a href="forum_topics.aspx?FID=<% = rsCommon("Forum_ID") %>"><% = rsCommon("Forum_name") %></a></td>
          <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="3%" class="text" align="center"><input type='checkbox' name="chkDelete" value="<% = rsCommon("Watch_ID") %>"></td>
       </tr><%
		
		'Move to the next recordset
		rsCommon.MoveNext
	Loop
End If

'clear up
rsCommon.Close
%>
      </table>
  </td>
 </tr>
</table>
</td>
 </tr>
</table>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="2" align="center"><tr>
<td width="6%" align="right"><input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtUnsusbribe %>"></td></tr>
</table>
</form>
<form name="frmTopicDel" method="post" action="email_notify_remove.aspx<% If strMode = "A" Then Response.Write("?PF=" & lngEmailUserID & "&amp;m=A") %>" OnSubmit="return confirm('<% = portal.variablesForum.strTxtAreYouWantToUnsubscribe & " " & portal.variablesForum.strTxtTopics %>')">
 <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
  <tr> 
   <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
     <tr> 
      <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
        <tr> 
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="67%" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtTopics & " " & portal.variablesForum.strTxtThatYouHaveSubscribedTo %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="30%" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtLastPost %></td>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" width="3%" align="center" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><input type='checkbox' name="chkAll" onClick="checkAllTopic();"></td>
        </tr><%
	
'Initlise the sql statement
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Topic.Subject, " & portal.variablesForum.strDbTable & "Topic.Last_entry_date, " & portal.variablesForum.strDbTable & "EmailNotify.Topic_ID, " & portal.variablesForum.strDbTable & "EmailNotify.Watch_ID "
strSQL = strSQL & "FROM " & portal.variablesForum.strDbTable & "Topic, " & portal.variablesForum.strDbTable & "EmailNotify "
strSQL = strSQL & "WHERE " & portal.variablesForum.strDbTable & "Topic.Topic_ID=" & portal.variablesForum.strDbTable & "EmailNotify.Topic_ID AND " & portal.variablesForum.strDbTable & "EmailNotify.UsuarioID=" & lngEmailUserID & " "
strSQL = strSQL & "ORDER BY " & portal.variablesForum.strDbTable & "Topic.Last_entry_date  DESC;"


'Query the database
rsCommon=db.execute(strSQL)
    
'Check there are email subscriptions to show
If rsCommon.EOF Then

	'If there are no pm messages to display then display the appropriate error message
	Response.Write vbCrLf & "<td bgcolor=""" & portal.variablesForum.strTableColour & """ background=""" & portal.variablesForum.strTableBgImage & """ colspan=""5"" class=""text"">" & portal.variablesForum.strTxtYouHaveNoSubToEmailNotify & "<input type=""hidden"" name=""chkDelete"" value=""-1""></td>"

'Else there the are email subs so show em
Else 	
	'Loop round to read in all the email notifys
	Do WHILE NOT  rsCommon.EOF 
	
		'Get the date of the last entry
		dtmLastEntryDate = CDate(rsCommon("Last_entry_date"))
	
		'Get the row number
		intForumColourNumber = intForumColourNumber + 1
	%>
        <tr> 
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="67%" class="text"><a href="forum_posts.aspx?TID=<% = rsCommon("Topic_ID") %>"><% = rsCommon("Subject") %></a></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="30%" class="text" nowrap="nowrap"><% Response.Write(funcFecha.DateFormat(dtmLastEntryDate, funcFecha.saryDateTimeData) & "&nbsp;" & portal.variablesForum.strTxtAt & "&nbsp;" & funcFecha.TimeFormat(dtmLastEntryDate, funcFecha.saryDateTimeData)) %></td>
         <td bgcolor="<% If (intForumColourNumber MOD 2 = 0 ) Then Response.Write(portal.variablesForum.strTableEvenRowColour) Else Response.Write(portal.variablesForum.strTableOddRowColour) %>" background="<% = portal.variablesForum.strTableBgImage %>" width="3%" class="text" align="center"><input type='checkbox' name="chkDelete" value="<% = rsCommon("Watch_ID") %>"></td>
        </tr><%
		
		'Move to the next recordset
		rsCommon.MoveNext
	Loop
End If

'clear up
rsCommon.Close
%>
       </table></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="2" align="center">
  <tr>
   <td width="6%" align="right"><input type='submit' name="Submit" value="<% = portal.variablesForum.strTxtUnsusbribe %>"></td>
  </tr>
 </table>
</form><%

'If this is not in admin mode then see if the user wants email notification of a forum
If strMode <> "A" Then 
%>
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr><form method="post" name="frmBuddy" action="email_notify.aspx?M=SP" onSubmit="return CheckForm();">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
         <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtSubscribeToForum %></td>
    </tr>
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>"> 
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
           <tr> 
            <td class="text"> <% = portal.variablesForum.strTxtSelectForumToSubscribeTo %> <%
            
Response.Write(vbCrLf & "	 <select name=""FID"">")
Response.Write(vbCrLf & "           <option value="""" selected>-- " & portal.variablesForum.strTxtSelectForum & " --</option>")


'Create a recordset to hold the forum name and id number
Set rsForumSelect = Server.CreateObject("ADODB.Recordset")


'Read in the category name from the database
'Initalise the strSQL variable with an SQL statement to query the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "CategoryAll"
Else
	strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.Cat_name, " & portal.variablesForum.strDbTable & "Category.Cat_ID FROM " & portal.variablesForum.strDbTable & "Category ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_order ASC;"
End If

'Query the database
rsCommon=db.execute(strSQL)




'Loop through all the categories in the database
Do while NOT rsCommon.EOF 

	'Read in the deatils for the category
	strCatName = rsCommon("Cat_name")
	intCatID = Cint(rsCommon("Cat_ID"))	
	
	'Display a link in the link list to the forum
	Response.Write vbCrLf & "		<option value="""">" & strCatName & "</option>"

	'Read in the forum name from the database
	'Initalise the strSQL variable with an SQL statement to query the database
	If portal.variablesForum.strDatabaseType = "SQLServer" Then
		strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "ForumsAllWhereCatIs @intCatID = " & intCatID
	Else
		strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.* FROM " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Forum.Cat_ID = " & intCatID & " ORDER BY " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"
	End If
	
	'Query the database
	rsForumSelect=db.execute(strSQL)
	
	'Loop through all the froum in the database
	Do while NOT rsForumSelect.EOF 
	
		'Read in the forum details from the recordset
		strSelectForumName = rsForumSelect("Forum_name")
		intSelectForumID = CLng(rsForumSelect("Forum_ID"))
		intReadPermission = CInt(rsForumSelect("Read"))

		'Call the function to check the users security level within this forum
		Call forumPermisisons(intSelectForumID, portal.variablesForum.intGroupID, intReadPermission, 0, 0, 0, 0, 0, 0, 0, 0, 0)
		
		'If the user can view the forum then display it in the select box
		If portal.variablesForum.blnRead OR portal.variablesForum.blnAdmin OR portal.variablesForum.blnModerator Then
			'Display a link in the link list to the forum
			Response.Write vbCrLf & "		<option value=""" & intSelectForumID & """>&nbsp;&nbsp;-&nbsp;" & strSelectForumName & "</option>"		
		End If
				
		'Move to the next record in the recordset
		rsForumSelect.MoveNext
	Loop
	
	'Close the forum recordset so another can be opened
	rsForumSelect.Close
	
	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

'Reset Server Objects
rsCommon.Close
Set rsForumSelect = Nothing


Response.Write(vbCrLf & "	</select>")

%>
	<input type='submit' name="Submit" value="Submit"></tr>
      </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
</td>
</form></tr>
</table><%
End If
%>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
  <tr><form>
   <td><!-- #include file="includes/forum_jump_inc.aspx" --></td>
   </form>
  </tr>
 </table>
<div align="center"><br /><%

'Clear server objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"


Response.Write("</div>")

%>

