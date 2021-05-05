

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


'Dimension variables				
Dim strWebSiteName		'Holds the web site name
Dim strForumPath 		'Holds the forum path
Dim strMode			'holds the mode of the page, set to true if changes are to be made to the database
Dim strBgColour			'Holds the background colour of the forum
Dim strTextColour		'Holds the text colours of the forum
Dim strTextType			'Holds the font type of the forum
Dim intTextSize			'Holds the font size of the forum
Dim strTableColour		'Holds the table colour of the forum
Dim strTableBorderColour	'Holds the border colour of the forum
Dim strTableTitleColour		'Holds the table title and status ba rof the forum
Dim strLinkColour		'Holds the Hperlink colour
Dim strVLinkColour		'Holds the visited link colour
Dim strALinkColour		'Holds the active link colour
Dim blnTextLinks		'Set to true if you want text links instead of the powered by logo
Dim blnLCode			'Holds the LCode value
Dim blnRTEEditor			'Set to true if the HTML editor for IE 5+ is turned on
Dim intTopicPerPage		'Holds the number of topics to show on each page
Dim strTitleImage		'Holds the path and name for the title image for the forum
Dim blnEmoticons		'Set to true if emoticons are turned on
Dim strThreadOrder		'Holds the order the threads are in
Dim blnAvatarImages		'Set to true if avatar images are on
Dim intRepliesPerPage		'Holds the number of replies per page
Dim intHotTopicViews		'Holds the number of views before a topic becomes hot
Dim intHotTopicReplies		'Holds the number of replies before a topic becomes hot
Dim blnPrivateMessenger		'Set to true if the private messenger is on
Dim intPrivateMessages		'Holds the number of private msg's a user can have in there inbox
Dim strForumName 		'Holds the forum name
Dim blnActiveUsers		'Set to true if the active users list is enabled
Dim blnProcessTime		'Set to true if the user wants the page genration time displayed
Dim blnUsuariosEdited   		'Set to true if the user wants the name of a post editor displayed
Dim blnFlashFiles		'Set to true if Flash support is enabled
Dim intPollChoice		'Holds the numebr of Poll Choices
Dim strWebsiteURL 		'Holds the URL to the sites homepage
Dim blnShowMod			'Set to true if mod groups are shown on the main forum page
      

'Read in the users details for the forum
strForumName = Request.Form("forumName")
strWebSiteName = Request.Form("siteName")
strWebsiteURL = Request.Form("siteURL")
strForumPath = Request.Form("forumPath")
strTitleImage = Request.Form("titleImage")
blnTextLinks = Request.Form("textLinks")
blnRTEEditor = CBool(Request.Form("IEEditor"))
blnLCode = CBool(Request.Form("LCode"))
intTopicPerPage	= CInt(Request.Form("topic"))
blnEmoticons = CBool(Request.Form("emoticons"))	
strThreadOrder = Request.Form("threadOrder")	
intPollChoice = CInt(Request.Form("pollChoice"))	
blnAvatarImages = CBool(Request.Form("avatar"))
intRepliesPerPage = CInt(Request.Form("threads"))
intHotTopicViews = CInt(Request.Form("hotViews"))
intHotTopicReplies = CInt(Request.Form("hotReplies"))
blnPrivateMessenger = CBool(Request.Form("privateMsg"))
intPrivateMessages = CInt(Request.Form("pmNo"))
portal.variablesForum.blnActiveUsers = CBool(Request.Form("activeUsers"))
blnProcessTime = CBool(Request.Form("processTime"))
blnUsuariosEdited = CBool(Request.Form("edited"))
blnFlashFiles = CBool(Request.Form("flash"))
portal.variablesForum.blnShowMod = CBool(Request.Form("showMod"))
strMode = Request.Form("mode")

	
'Initialise the SQL variable with an SQL statement to get the configuration details from the database
If portal.variablesForum.strDatabaseType = "SQLServer" Then
	strSQL = "EXECUTE " & portal.variablesForum.strDbProc & "SelectConfiguration"
Else
	strSQL = "SELECT TOP 1 " & portal.variablesForum.strDbTable & "Configuration.* From " & portal.variablesForum.strDbTable & "Configuration;"
End If

'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
rsCommon.CursorType = 2

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3

	
'Query the database
rsCommon=db.execute(strSQL)

'If the user is changing tthe colours then update the database
If Request.Form("postBack") Then
	
	'Update the recordset
	With rsCommon
		.Fields("forum_name") = strForumName
		.Fields("website_path") = strWebsiteURL 
		.Fields("Text_link") = blnTextLinks
		.Fields("IE_editor") = blnRTEEditor
		.Fields("L_code") = blnLCode
		.Fields("Topics_per_page") = intTopicPerPage
		.Fields("Title_image") = strTitleImage
		.Fields("website_name") = strWebSiteName
		.Fields("forum_path") = strForumPath
		.Fields("Emoticons") = blnEmoticons
		.Fields("Avatar") = blnAvatarImages
		.Fields("Threads_per_page") = intRepliesPerPage
		.Fields("Hot_views") = intHotTopicViews
		.Fields("Hot_replies") = intHotTopicReplies
		.Fields("Private_msg") = blnPrivateMessenger
		.Fields("No_of_priavte_msg") = intPrivateMessages
		.Fields("Active_users") = blnActiveUsem_rs.Fields("Process_time") = blnProcessTime
		.Fields("Show_edit") = blnUsuariosEdited
		.Fields("Flash") = blnFlashFiles
		.Fields("Vote_choices") = intPollChoice
		.Fields("Show_mod") = portal.variablesForum.blnShowMod
					
		'Update the database with the new user's coloum_rs.Update
			
		'Re-run the query to read in the updated recordset from the database
		.Requery
	End With
	
	'Update variables
	Application("strMainForumName") = strForumName
	Application("strWebsiteURL") = strWebsiteURL
	Application("blnTextLinks") = blnTextLinks
	Application("blnRTEEditor") = blnRTEEditor
	Application("blnLCode") = blnLCode
	Application("intTopicPerPage") = intTopicPerPage
	Application("strTitleImage") = strTitleImage
	Application("strWebsiteName") = strWebSiteName
	Application("strForumPath") = strForumPath
	Application("blnEmoticons") = blnEmoticons
	Application("blnAvatar") = blnAvatarImages
	Application("intThreadsPerPage") = intRepliesPerPage
	Application("intNumHotViews") = intHotTopicViews
	Application("intNumHotReplies") = intRepliesPerPage
	Application("blnPrivateMessages") = blnPrivateMessenger
	Application("intNumPrivateMessages") = intPrivateMessages
	Application("blnActiveUsers") = portal.variablesForum.blnActiveUsers
	Application("blnShowProcessTime") = blnProcessTime
	Application("blnShowEditUser") = blnUsuariosEdited
	Application("blnFlashFiles") = blnFlashFiles
	Application("intMaxPollChoices") = intPollChoice
	Application("blnShowMod") = portal.variablesForum.blnShowMod
		
	
	'Empty the application level variable so that the changes made are seen in the main forum
	Application("blnConfigurationSet") = false
End If

'Read in the forum colours from the database
If NOT rsCommon.EOF Then
	
	'Read in the colour info from the database
	strForumName = rsCommon.Fields("forum_name")
	strWebsiteURL = rsCommon.Fields("website_path")
	blnTextLinks = rsCommon.Fields("Text_link")
	blnLCode =  CBool(rsCommon.Fields("L_code"))
	blnRTEEditor =  rsCommon.Fields("IE_editor")
	intTopicPerPage = CInt(rsCommon.Fields("Topics_per_page"))
	strTitleImage = rsCommon.Fields("Title_image")
	strWebSiteName = rsCommon("website_name")
	strForumPath = rsCommon("forum_path")
	blnEmoticons = rsCommon.Fields("Emoticons")
	blnAvatarImages = rsCommon.Fields("Avatar")
	intRepliesPerPage = rsCommon.Fields("Threads_per_page")
	intHotTopicViews = rsCommon.Fields("Hot_views")
	intHotTopicReplies = rsCommon.Fields("Hot_replies")
	blnPrivateMessenger = rsCommon.Fields("Private_msg")
	intPrivateMessages = rsCommon.Fields("No_of_priavte_msg")
	portal.variablesForum.blnActiveUsers = rsCommon.Fields("Active_users")
	blnProcessTime = rsCommon.Fields("Process_time")
	blnUsuariosEdited = rsCommon.Fields("Show_edit")
	blnFlashFiles = rsCommon.Fields("Flash")
	intPollChoice = CInt(rsCommon.Fields("Vote_choices"))
	portal.variablesForum.blnShowMod = CBool(rsCommon.Fields("Show_mod"))
End If


'Reset Server Objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>
<title>Forum Configuration</title>


     	
		
<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a name of the forum
	if (document.frmConfiguration.forumName.value==""){
		alert("Please enter the name of your Forum");
		document.frmConfiguration.forumName.focus();
		return false;
	}
	
	//Check for a site name
	if (document.frmConfiguration.siteName.value==""){
		alert("Please enter the name of your Website");
		document.frmConfiguration.siteName.focus();
		return false;
	}
	
	//Check for a URL to homepage
	if (document.frmConfiguration.siteURL.value==""){
		alert("Please enter the URL to your websites homepage");
		document.frmConfiguration.siteURL.focus();
		return false;
	}
	
	//Check for a path to the forum
	if (document.frmConfiguration.forumPath.value==""){
		alert("Please enter the URL to your Forum");
		document.frmConfiguration.forumPath.focus();
		return false;
	}
	return true;
}
// -->
</script>
     	
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading"> Forum Configuration</span> <br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  From here you can configure general options of the forum to customise the functions and look of your forum.</p>
</div>
<form method="post" name="frmConfiguration" action="forum_configure.aspx" onSubmit="return CheckForm();">
 <table border="0" align="center" cellpadding="4" cellspacing="1" width="560" bgcolor="#000000">
  <tr bgcolor="#CCCEE6"> 
   <td colspan="2" align="left" class="tHeading"><b>General Forum Settings and Look</b></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Forum name*<br /> <span class="smText">This is the name of your forum.</span></td>
   <td valign="top"> 
    <input name="forumName" type='text' id="forumName" value="<% = strForumName %>" size="30" maxlength="50" ></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Website name*<br /> <span class="smText">The name of your website or Forum <br />
    eg. My Website Forum</span></td>
   <td valign="top"> 
    <input type='text' name="siteName" maxlength="50" value="<% = strWebsiteName %>" size="30" > </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Website Address*<br /> <span class="smText">This is the URL to your website's homepage so that members can be taken to your homepage when exiting the forum, your domain name 
    is usually best here.</span></td>
   <td valign="top"> 
    <input name="siteURL" type='text' id="siteURL" value="<% = strWebsiteURL %>" size="30" maxlength="70" ></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Web address path to forum*<br /> <span class="smText">The web URL to your forum including your domain name and any folder the forum may be in. This is the address you would 
    type into the address bar on your browser to get to the forum.<br />
    eg. http://www.mywebsite.com/forum </span></td>
   <td valign="top"> 
    <input type='text' name="forumPath" maxlength="70" value="<% = strForumPath %>" size="30" > </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Forum Title Image Location:<br /> <span class="smText">This replaces the Web Wiz Forum's logo that is shown on the top of each page. Your own web sites banner logo would 
    be good to place here.</span></td>
   <td valign="top"> 
    <input type='text' name="titleImage" maxlength="65" value="<% = strTitleImage %>" size="35"> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Powered by Web Wiz Guide Link logo:<br /> <span class="smText">If you enable this the text link to Web Wiz Forums at the bottom of the will be replaced with a graphic image.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="textLinks" value="False" <% If blnTextLinks = False Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="textLinks" value="True" <% If blnTextLinks = True Then response.write("checked" %>></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Active Users List<br />
    <span class="smText">This will display the name sof all the active users in the forum on the main homepage.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="activeUsers" value="True" <% If portal.variablesForum.blnActiveUsers = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" value="False" <% If portal.variablesForum.blnActiveUsers = False Then response.write("checked" %> name="activeUsers"></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text"> 
    <p>Display Moderator Groups on Main Page:<br />
     <span class="smText">Display the moderator user group for each forum on the main page.</span></p>
    </td>
   <td valign="top" class="text">On 
    <input type="radio" name="showMod" value="True" <% If portal.variablesForum.blnShowMod = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" value="False" <% If portal.variablesForum.blnShowMod = False Then response.write("checked" %> name="showMod"></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Display Server Processing Time for Page:<br /> <span class="smText"> Display the number of seconds it has taken the server to generate the page on the bottom of forum pages.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="processTime" value="True" <% If blnProcessTime = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="processTime" value="False" <% If blnProcessTime = False Then response.write("checked" %>> </td>
  </tr>
  <tr bgcolor="#CCCEE6"> 
   <td colspan="2" align="left" class="tHeading">User and Forum Posts Settings</td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Avatar Images:<br />
    <span class="smText">If you wish to also allow users to upload their own avatars you will need to also <a href="upload_configure.aspx" class="smLink">setup the upload component</a>.<br />
    For extra security users can only upload avatars once registered by editing their profile.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="avatar" value="True" <% If blnAvatarImages = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="avatar" value="False" <% If blnAvatarImages = False Then response.write("checked" %>> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Emoticon Smiley Images:</td>
   <td valign="top" class="text">On 
    <input type="radio" name="emoticons" value="True" <% If blnEmoticons = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="emoticons" value="False" <% If blnEmoticons = False Then response.write("checked" %>></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Display Edited Usuarios:<br /> <span class="smText">Display the name of person who edited a posts and the date the post was edited on the bottom of edited 
    posts.</span><br /> </td>
   <td valign="top" class="text">On 
    <input type="radio" name="edited" value="True" <% If blnUsuariosEdited = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="edited" value="False" <% If blnUsuariosEdited = False Then response.write("checked" %>></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Flash Files/Images:<br />
    <span class="smText">If you enable this then users will be able to display Flash Files/Images in their posts and signatures. Please be aware that allowing Flash files can cause security problems from 
    malicious Flash Files.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="flash" value="True" <% If blnFlashFiles = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="flash" value="False" <% If blnFlashFiles = False Then response.write("checked" %>></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Rich Text Editor (WYSIWYG):<br />
    <span class="smText">This is the type of editor you use to post messages if you are using a Rich Text Enabled web browser, currently Windows IE5+, Netscape 7.1+, Mozilla 1.3+, and Mozilla Firebird 0.6.1+, 
    support this feature.<br />
    If you turn this function off everyone will use the Basic message editor.<br />
    If you want greater security turn this feature off, but you will lose functionality.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="IEEditor" value="True" <% If blnRTEEditor = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" name="IEEditor" value="False" <% If blnRTEEditor = False Then response.write("checked" %>> </td>
  </tr>
  <tr bgcolor="#CCCEE6"> 
   <td colspan="2" align="left" class="tHeading">Forum Posts and Topic Page Display Settings</td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Topics Per Page:<br /> <span class="smText">This is the number of Topics shown on each page.</span></td>
   <td valign="top"> 
    <select name="topic">
     <option <% If intTopicPerPage = 10 Then Response.Write("selected") %>>10</option>
     <option <% If intTopicPerPage = 12 Then Response.Write("selected") %>>12</option>
     <option <% If intTopicPerPage = 14 Then Response.Write("selected") %>>14</option>
     <option <% If intTopicPerPage = 16 Then Response.Write("selected") %>>16</option>
     <option <% If intTopicPerPage = 18 Then Response.Write("selected") %>>18</option>
     <option <% If intTopicPerPage = 20 Then Response.Write("selected") %>>20</option>
     <option <% If intTopicPerPage = 22 Then Response.Write("selected") %>>22</option>
     <option <% If intTopicPerPage = 24 Then Response.Write("selected") %>>24</option>
     <option <% If intTopicPerPage = 26 Then Response.Write("selected") %>>26</option>
     <option <% If intTopicPerPage = 28 Then Response.Write("selected") %>>28</option>
     <option <% If intTopicPerPage = 30 Then Response.Write("selected") %>>30</option>
     <option <% If intTopicPerPage = 32 Then Response.Write("selected") %>>32</option>
     <option <% If intTopicPerPage = 34 Then Response.Write("selected") %>>34</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Posts Per Page:<br /> <span class="smText">This is the number of Posts shown on each page of a Topic.</span></td>
   <td valign="top"> 
    <select name="threads">
     <option<% If intRepliesPerPage = 10 Then Response.Write(" selected") %>>3</option>
     <option<% If intRepliesPerPage = 4 Then Response.Write(" selected") %>>4</option>
     <option<% If intRepliesPerPage = 5 Then Response.Write(" selected") %>>5</option>
     <option<% If intRepliesPerPage = 6 Then Response.Write(" selected") %>>6</option>
     <option<% If intRepliesPerPage = 7 Then Response.Write(" selected") %>>7</option>
     <option<% If intRepliesPerPage = 8 Then Response.Write(" selected") %>>8</option>
     <option<% If intRepliesPerPage = 9 Then Response.Write(" selected") %>>9</option>
     <option<% If intRepliesPerPage = 10 Then Response.Write(" selected") %>>10</option>
     <option<% If intRepliesPerPage = 12 Then Response.Write(" selected") %>>12</option>
     <option<% If intRepliesPerPage = 14 Then Response.Write(" selected") %>>14</option>
     <option<% If intRepliesPerPage = 16 Then Response.Write(" selected") %>>16</option>
     <option<% If intRepliesPerPage = 18 Then Response.Write(" selected") %>>18</option>
     <option<% If intRepliesPerPage = 20 Then Response.Write(" selected") %>>20</option>
     <option<% If intRepliesPerPage = 25 Then Response.Write(" selected") %>>25</option>
     <option<% If intRepliesPerPage = 30 Then Response.Write(" selected") %>>30</option>
     <option<% If intRepliesPerPage = 35 Then Response.Write(" selected") %>>35</option>
     <option<% If intRepliesPerPage = 40 Then Response.Write(" selected") %>>40</option>
     <option<% If intRepliesPerPage = 45 Then Response.Write(" selected") %>>45</option>
     <option<% If intRepliesPerPage = 50 Then Response.Write(" selected") %>>50</option>
     <option<% If intRepliesPerPage = 75 Then Response.Write(" selected") %>>75</option>
     <option<% If intRepliesPerPage = 100 Then Response.Write(" selected") %>>100</option>
     <option<% If intRepliesPerPage = 150 Then Response.Write(" selected") %>>150</option>
     <option<% If intRepliesPerPage = 200 Then Response.Write(" selected") %>>200</option>
     <option<% If intRepliesPerPage = 250 Then Response.Write(" selected") %>>250</option>
     <option<% If intRepliesPerPage = 300 Then Response.Write(" selected") %>>300</option>
     <option<% If intRepliesPerPage = 500 Then Response.Write(" selected") %>>500</option>
     <option<% If intRepliesPerPage = 999 Then Response.Write(" selected") %>>999</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Hot Topic Number of Views:<br /> <span class="smText">This is the number of times a Topic is viewed before it is shown as a Hot Topic.</span></td>
   <td valign="top"> 
    <select name="hotViews">
     <option<% If intHotTopicViews = 5 Then Response.Write(" selected") %>>5</option>
     <option<% If intHotTopicViews = 10 Then Response.Write(" selected") %>>10</option>
     <option<% If intHotTopicViews = 20 Then Response.Write(" selected") %>>20</option>
     <option<% If intHotTopicViews = 30 Then Response.Write(" selected") %>>30</option>
     <option<% If intHotTopicViews = 40 Then Response.Write(" selected") %>>40</option>
     <option<% If intHotTopicViews = 50 Then Response.Write(" selected") %>>50</option>
     <option<% If intHotTopicViews = 60 Then Response.Write(" selected") %>>60</option>
     <option<% If intHotTopicViews = 70 Then Response.Write(" selected") %>>70</option>
     <option<% If intHotTopicViews = 80 Then Response.Write(" selected") %>>80</option>
     <option<% If intHotTopicViews = 90 Then Response.Write(" selected") %>>90</option>
     <option<% If intHotTopicViews = 100 Then Response.Write(" selected") %>>100</option>
     <option<% If intHotTopicViews = 110 Then Response.Write(" selected") %>>110</option>
     <option<% If intHotTopicViews = 120 Then Response.Write(" selected") %>>120</option>
     <option<% If intHotTopicViews = 130 Then Response.Write(" selected") %>>130</option>
     <option<% If intHotTopicViews = 140 Then Response.Write(" selected") %>>140</option>
     <option<% If intHotTopicViews = 150 Then Response.Write(" selected") %>>150</option>
     <option<% If intHotTopicViews = 200 Then Response.Write(" selected") %>>200</option>
     <option<% If intHotTopicViews = 250 Then Response.Write(" selected") %>>250</option>
     <option<% If intHotTopicViews = 300 Then Response.Write(" selected") %>>300</option>
     <option<% If intHotTopicViews = 400 Then Response.Write(" selected") %>>400</option>
     <option<% If intHotTopicViews = 500 Then Response.Write(" selected") %>>500</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Hot Topic Number or Replies:<br /> <span class="smText">This is the number of Replies a Topic must have to be shown as a Hot Topic.</span></td>
   <td valign="top"> 
    <select name="hotReplies">
     <option<% If intHotTopicReplies = 3 Then Response.Write(" selected") %>>3</option>
     <option<% If intHotTopicReplies = 4 Then Response.Write(" selected") %>>4</option>
     <option<% If intHotTopicReplies = 5 Then Response.Write(" selected") %>>5</option>
     <option<% If intHotTopicReplies = 6 Then Response.Write(" selected") %>>6</option>
     <option<% If intHotTopicReplies = 7 Then Response.Write(" selected") %>>7</option>
     <option<% If intHotTopicReplies = 8 Then Response.Write(" selected") %>>8</option>
     <option<% If intHotTopicReplies = 9 Then Response.Write(" selected") %>>9</option>
     <option<% If intHotTopicReplies = 10 Then Response.Write(" selected") %>>10</option>
     <option<% If intHotTopicReplies = 15 Then Response.Write(" selected") %>>15</option>
     <option<% If intHotTopicReplies = 20 Then Response.Write(" selected") %>>20</option>
     <option<% If intHotTopicReplies = 25 Then Response.Write(" selected") %>>25</option>
     <option<% If intHotTopicReplies = 30 Then Response.Write(" selected") %>>30</option>
     <option<% If intHotTopicReplies = 35 Then Response.Write(" selected") %>>35</option>
     <option<% If intHotTopicReplies = 40 Then Response.Write(" selected") %>>40</option>
     <option<% If intHotTopicReplies = 45 Then Response.Write(" selected") %>>45</option>
     <option<% If intHotTopicReplies = 50 Then Response.Write(" selected") %>>50</option>
     <option<% If intHotTopicReplies = 60 Then Response.Write(" selected") %>>60</option>
     <option<% If intHotTopicReplies = 75 Then Response.Write(" selected") %>>75</option>
     <option<% If intHotTopicReplies = 100 Then Response.Write(" selected") %>>100</option>
    </select></td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Number of Poll Choices:<br /> <span class="smText">This is the maximum number of Choices allowed in a Forum Polls.</span></td>
   <td valign="top"> 
    <select name="pollChoice" id="pollChoice">
     <option<% If intPollChoice = 3 Then Response.Write(" selected") %>>3</option>
     <option<% If intPollChoice = 4 Then Response.Write(" selected") %>>4</option>
     <option<% If intPollChoice = 5 Then Response.Write(" selected") %>>5</option>
     <option<% If intPollChoice = 6 Then Response.Write(" selected") %>>6</option>
     <option<% If intPollChoice = 7 Then Response.Write(" selected") %>>7</option>
     <option<% If intPollChoice = 8 Then Response.Write(" selected") %>>8</option>
     <option<% If intPollChoice = 9 Then Response.Write(" selected") %>>9</option>
     <option<% If intPollChoice = 10 Then Response.Write(" selected") %>>10</option>
     <option<% If intPollChoice = 11 Then Response.Write(" selected") %>>11</option>
     <option<% If intPollChoice = 12 Then Response.Write(" selected") %>>12</option>
     <option<% If intPollChoice = 13 Then Response.Write(" selected") %>>13</option>
     <option<% If intPollChoice = 14 Then Response.Write(" selected") %>>14</option>
     <option<% If intPollChoice = 15 Then Response.Write(" selected") %>>15</option>
     <option<% If intPollChoice = 16 Then Response.Write(" selected") %>>16</option>
     <option<% If intPollChoice = 17 Then Response.Write(" selected") %>>17</option>
     <option<% If intPollChoice = 18 Then Response.Write(" selected") %>>18</option>
     <option<% If intPollChoice = 19 Then Response.Write(" selected") %>>19</option>
     <option<% If intPollChoice = 20 Then Response.Write(" selected") %>>20</option>
     <option<% If intPollChoice = 25 Then Response.Write(" selected") %>>25</option>
    </select> </td>
  </tr>
  <tr bgcolor="#CCCEE6"> 
   <td colspan="2" align="left" class="tHeading">Private Messenger</td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Private Messenger:<br /> <span class="smText">If you turn this off your members will no longer be able to use the Private Messenger to send and receive Private Messages.</span></td>
   <td valign="top" class="text">On 
    <input type="radio" name="privateMsg" value="True" <% If blnPrivateMessenger = True Then response.write("checked" %>> &nbsp;&nbsp;Off 
    <input type="radio" value="False" <% If blnPrivateMessenger = False Then response.write("checked" %> name="privateMsg"> </td>
  </tr>
  <tr bgcolor="#F5F5FA"> 
   <td align="left" class="text">Number of Private Messages Per Member:<br /> <span class="smText">This is the number of Private Messages a member can have in there 'inbox' at any one time.</span></td>
   <td valign="top"> 
    <select name="pmNo">
     <option<% If intPrivateMessages = 10 Then Response.Write(" selected") %>>10</option>
     <option<% If intPrivateMessages = 15 Then Response.Write(" selected") %>>15</option>
     <option<% If intPrivateMessages = 20 Then Response.Write(" selected") %>>20</option>
     <option<% If intPrivateMessages = 25 Then Response.Write(" selected") %>>25</option>
     <option<% If intPrivateMessages = 30 Then Response.Write(" selected") %>>30</option>
     <option<% If intPrivateMessages = 35 Then Response.Write(" selected") %>>35</option>
     <option<% If intPrivateMessages = 40 Then Response.Write(" selected") %>>40</option>
     <option<% If intPrivateMessages = 45 Then Response.Write(" selected") %>>45</option>
     <option<% If intPrivateMessages = 50 Then Response.Write(" selected") %>>50</option>
     <option<% If intPrivateMessages = 60 Then Response.Write(" selected") %>>60</option>
     <option<% If intPrivateMessages = 70 Then Response.Write(" selected") %>>70</option>
     <option<% If intPrivateMessages = 80 Then Response.Write(" selected") %>>80</option>
     <option<% If intPrivateMessages = 90 Then Response.Write(" selected") %>>90</option>
     <option<% If intPrivateMessages = 100 Then Response.Write(" selected") %>>100</option>
     <option<% If intPrivateMessages = 150 Then Response.Write(" selected") %>>150</option>
     <option<% If intPrivateMessages = 200 Then Response.Write(" selected") %>>200</option>
     <option<% If intPrivateMessages = 250 Then Response.Write(" selected") %>>250</option>
     <option<% If intPrivateMessages = 500 Then Response.Write(" selected") %>>500</option>
     <option<% If intPrivateMessages = 999 Then Response.Write(" selected") %>>999</option>
    </select> </td>
  </tr>
  <tr bgcolor="#F5F5FA" align="center"> 
   <td colspan="2" valign="top" class="arial"> 
    <p> 
     <input type="hidden" name="postBack" value="true">
     <input type="hidden" name="LCode" value="<% = blnLCode %>">
     <input type='submit' name="Submit" value="Update Forum Configuration">
     <input type="reset" name="Reset" value="Reset Form">
    </p></td>
  </tr>
 </table>
</form>
<br />
</body>
</html>
