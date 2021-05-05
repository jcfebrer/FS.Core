

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim intUserGroupID	'Holds the group ID
Dim strGroupName	'Holds the name of the group
Dim lngMinimumPosts	'Holds the minimum amount of posts to be in that group
Dim blnSpecialGroup	'Set to true if a special group
Dim intStars		'Holds the number of stars for the group
Dim strCustomStars	'Holds the custom stars image if there is one fo0r this group
Dim strMode		'Holds the mode of the page


'Initlise variables
lngMinimumPosts = 0
blnSpecialGroup = False
intStars = 1


'Read in the details
intUserGroupID = CInt(Request.QueryString("GID"))

'Read in the page mode
strMode = Request("mode")


'Read the various groups from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.* FROM " & portal.variablesForum.strDbTable & "Group WHERE " & portal.variablesForum.strDbTable & "Group.Group_ID = " & intUserGroupID & ";"

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

'Query the database
rsCommon=db.execute(strSQL)

'If this is a post back update the database
If (strMode = "edit" OR strMode = "new") AND Request.Form("postBack") Then


	'Read in the group details
	strGroupName = Request.Form("GroupName")
	lngMinimumPosts = CLng(Request.Form("posts"))
	blnSpecialGroup = CBool(Request.Form("rank"))
	intStars = CInt(Request.Form("stars"))
	strCustomStars = Request.Form("custStars")


	'If this is a non ladder group place -1 into the minimum posts variable
	If blnSpecialGroup Then
		lngMinimumPosts = CInt("-1")
	End If


	With rsCommon
		'If this is a new one add new
		If strMode = "new" Then .AddNew

		'Update the recordset
		.Fields("Name") = strGroupName
		.Fields("Stars") = intStam_rs.Fields("Custom_stars") = strCustomStars
		If intUserGroupID <> 1 AND intUserGroupID <> 2 Then 
			.Fields("Minimum_posts") = lngMinimumPosts
			.Fields("Special_rank") = blnSpecialGroup
		End If

		'Update the database with the group details
		.Update
	End With

	'If this is a new forum go back to the main forums page
	If strMode = "new" Then

		'Release server varaibles
		rsCommon.Close
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing

		Response.Redirect("view_groups.aspx")
	End If

	'Re-run the query to read in the updated recordset from the database
	rsCommon.Requery
End If


'Read in the forum details from the recordset
If NOT rsCommon.EOF Then

	'Get the category name from the database
	strGroupName = rsCommon("Name")
	lngMinimumPosts = CLng(rsCommon("Minimum_posts"))
	blnSpecialGroup = CBool(rsCommon("Special_rank"))
	intStars = CInt(rsCommon("Stars"))
	strCustomStars = rsCommon("Custom_stars")
End If


'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

%>
<html>
<head>
<title>User Group Details</title>



     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a group name
	if (document.frmGroup.GroupName.value==""){
		alert("Please select the Name for this User Group");
		return false;
	}

	return true
}
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">User Group Details</span><br />
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="view_groups.aspx" target="_self">Return to the User Group Administration page</a><br />
</div>
<form method="post" name="frmGroup" action="group_details.aspx?GID=<% = intUserGroupID %>" onSubmit="return CheckForm();">
 <br />
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr>
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6"> 
      <td colspan="2" class="tHeading"><b>User Group Details</b></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td width="60%" class="text">Group Name*:</td>
      <td width="40%" valign="top"> 
       <input name="GroupName" type='text' id="GroupName" value="<% = strGroupName %>" size="20" maxlength="39"> </td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Number of Stars*:<br /> <span class="smText">This is the number of stars displayed for this user group, unless you use your own custom stars/image.</span></td>
      <td valign="top"> 
       <select name="stars" id="stars">
        <option<% If intStars = 0 Then response.write(" selected" %>>0</option>
        <option<% If intStars = 1 Then response.write(" selected" %>>1</option>
        <option<% If intStars = 2 Then response.write(" selected" %>>2</option>
        <option<% If intStars = 3 Then response.write(" selected" %>>3</option>
        <option<% If intStars = 4 Then response.write(" selected" %>>4</option>
        <option<% If intStars = 5 Then response.write(" selected" %>>5</option>
       </select></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Custom Stars Image Link:<br />
       <span class="smText">If you wish to use your own custom stars/image for this group type the path in here to the image.</span></td>
      <td valign="top"> 
       <input name="custStars" type='text' id="custStars" value="<% = strCustomStars %>" size="30" maxlength="75"></td>
     </tr><%

'If this is the admin group or guest group then don't let em change anything else
If intUserGroupID <> 1 AND intUserGroupID <> 2 Then 
	
	%>
     <tr bgcolor="#F5F5FA"> 
      <td class="text"> 
       <p>Non Ladder Group:<br />
        <span class="smText">If you check this box then this group will not be a part of the Ladder System.</span></p></td>
      <td valign="top"> 
       <input name="rank" type='checkbox' id="rank" value="true"<% If blnSpecialGroup Then response.write(" checked" %>></td>
     </tr>
     <tr bgcolor="#F5F5FA"> 
      <td class="text">Ladder Group Minimum No. of Posts:<br /> <span class="smText">This is the number of posts a user needs to post to automatically become a member of this group. This will not effect a 
       Non Ladder Group. </span></td>
      <td valign="top"> 
       <select name="posts" id="posts">
        <option<% If lngMinimumPosts = 0 Then response.write(" selected" %>>0</option>
        <option<% If lngMinimumPosts = 10 Then response.write(" selected" %>>10</option>
        <option<% If lngMinimumPosts = 20 Then response.write(" selected" %>>20</option>
        <option<% If lngMinimumPosts = 30 Then response.write(" selected" %>>30</option>
        <option<% If lngMinimumPosts = 40 Then response.write(" selected" %>>40</option>
        <option<% If lngMinimumPosts = 50 Then response.write(" selected" %>>50</option>
        <option<% If lngMinimumPosts = 60 Then response.write(" selected" %>>60</option>
        <option<% If lngMinimumPosts = 70 Then response.write(" selected" %>>70</option>
        <option<% If lngMinimumPosts = 80 Then response.write(" selected" %>>80</option>
        <option<% If lngMinimumPosts = 90 Then response.write(" selected" %>>90</option>
        <option<% If lngMinimumPosts = 100 Then response.write(" selected" %>>100</option>
        <option<% If lngMinimumPosts = 125 Then response.write(" selected" %>>125</option>
        <option<% If lngMinimumPosts = 150 Then response.write(" selected" %>>150</option>
        <option<% If lngMinimumPosts = 200 Then response.write(" selected" %>>200</option>
        <option<% If lngMinimumPosts = 250 Then response.write(" selected" %>>250</option>
        <option<% If lngMinimumPosts = 300 Then response.write(" selected" %>>300</option>
        <option<% If lngMinimumPosts = 350 Then response.write(" selected" %>>350</option>
        <option<% If lngMinimumPosts = 400 Then response.write(" selected" %>>400</option>
        <option<% If lngMinimumPosts = 450 Then response.write(" selected" %>>450</option>
        <option<% If lngMinimumPosts = 500 Then response.write(" selected" %>>500</option>
        <option<% If lngMinimumPosts = 750 Then response.write(" selected" %>>750</option>
        <option<% If lngMinimumPosts = 1000 Then response.write(" selected" %>>1000</option>
        <option<% If lngMinimumPosts = 1500 Then response.write(" selected" %>>1500</option>
        <option<% If lngMinimumPosts = 2000 Then response.write(" selected" %>>2000</option>
        <option<% If lngMinimumPosts = 2500 Then response.write(" selected" %>>2500</option>
        <option<% If lngMinimumPosts = 3000 Then response.write(" selected" %>>3000</option>
        <option<% If lngMinimumPosts = 5000 Then response.write(" selected" %>>5000</option>
        <option<% If lngMinimumPosts = 7500 Then response.write(" selected" %>>7500</option>
        <option<% If lngMinimumPosts = 10000 Then response.write(" selected" %>>10000</option>
        <option<% If lngMinimumPosts = 15000 Then response.write(" selected" %>>15000</option>
        <option<% If lngMinimumPosts = 20000 Then response.write(" selected" %>>20000</option>
        <option<% If lngMinimumPosts = 30000 Then response.write(" selected" %>>30000</option>
        <option<% If lngMinimumPosts = 40000 Then response.write(" selected" %>>40000</option>
        <option<% If lngMinimumPosts = 50000 Then response.write(" selected" %>>50000</option>
        <option<% If lngMinimumPosts = 75000 Then response.write(" selected" %>>75000</option>
        <option<% If lngMinimumPosts = 100000 Then response.write(" selected" %>>100000</option>
       </select></td>
     </tr><%

End If

%>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type="hidden" name="postBack" value="true">
  <input type="hidden" name="mode" value="<% = strMode %>">
  <input type='submit' name="Submit" value="Submit User Group Details">
  <input type="reset" name="Reset" value="Reset Form">
  <br />
 </div>
</form>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#000000">
 <tr>
  <td><table width="100%" border="0" cellspacing="1" cellpadding="2">
    <tr> 
     <td align="center" bgcolor="#CCCEE6"><span class="lgText">What is the Ladder System?</span></td>
    </tr>
    <tr> 
     <td bgcolor="#EAEAF4" class="text">The Ladder system enables your members to move up forum groups automatically depending on the number of posts they make. Once a member has made the minimum amount 
      of posts for a Ladder User Group that member will be moved up to that user group.<br /> <br />
      If you select that a user group is a Non Ladder Group, any member of the group will not be effected by the ladder system, this is useful if you wish not to use the Ladder System or for special groups 
      like moderator groups.</td>
    </tr>
   </table></td>
 </tr>
</table>
</body>
</html>