

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
%>
<html>
<head>
<title>Forum Administration Menu</title>


<link href="includes/default_style.css" rel="stylesheet" type="text/css">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"></head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" background="images/main_bg.gif" text="#000000">
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr> 
    
  <td height="2" align="center" class="tiText"> <span class="heading">Forum Administration Menu</span><br />
   <a href="../default.aspx" target="_top">Return to the Main Forum</a>
   <%

'If this is the main forum admin let him change the admin usuario and clave
If portal.variablesForum.lngLoggedInUserID = 1 Then %>
   <br />
   <br />
   For security it is highly recommended that you <a href="change_admin_usuario.aspx" target="_self">change the Admin usuario and clave</a> to stop others messing up your Forums!<%
	 
	 End If
	 %></td>
  </tr>
</table>
<br />
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td > <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> <table width="100%" border="0" cellpadding="7" cellspacing="0">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText"><b>Forum Administration</b></span><b><br />
         </b>The following pages are to help you administer the forum</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="view_forums.aspx" target="_self">Forum Administration</a><br />
         Create, Amend, Delete any forum's and forum categories, alter forum details, set forum permissions, lock forums, clave protect forums, etc.</td>
       </tr>
       <tr>
        <td bgcolor="#F5F5FA" class="text"><a href="close_forums.aspx" target="_self">Lock Forums</a><br />
         From this option you can Lock the Forums so that no-one can post, register, login. etc. on the forum. Useful for forum maintenance.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><p><a href="http://www.febrersoftware.com/update.aspx?v=<% = Server.URLEncode(strVersion) %>" target="_blank">Check for updates</a><br />
          Check for updates to the forum application version <% = strVersion %>.
         </td>
         <tr> 
        <td bgcolor="#F5F5FA" class="text"><p><a href="http://www.febrersoftware.com" target="_blank">About</a><br />
          About FebrerSoftware.
         </td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td > <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText"><b>Forum User Group</b></span><b> Administration<br />
         </b>The following pages are to help you administer forum User Groups</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="view_groups.aspx" target="_self">Group Administration</a><br />
         Create, Amend, Delete, change the details, etc. of forum User Groups.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="group_perm_forum.aspx" target="_self">Group Permissions Administration</a><br />
         From this option you can configure permissions on forums for User Groups, set permissions for forum moderation, entry, posting, creating polls, etc.</td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td > <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText"><b>Membership</b></span><b> Administration<br />
         </b>The following pages are to help you administer Forum User Memberships</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="select_membem_rs.aspx" target="_self">Membership Administration</a><br />
         Administer members accounts, make them moderators, change status, delete members, suspend accounts, etc.</td>
       </tr>
       <tr>
        <td bgcolor="#F5F5FA" class="text"><a href="find_user.aspx" target="_self">Member Permissions Administration</a><br />
         From this option you can configure permissions on forums for Members, set permissions for forum moderation, entry, posting, creating polls, etc.</td>
       </tr>
       <tr>
        <td bgcolor="#F5F5FA" class="text"><a href="add_member.aspx" target="_self">Add New Member </a><br />
From this option you can Add a New Forum Member. </td>
       </tr>
       <tr>
        <td bgcolor="#F5F5FA" class="text"><a href="change_usuario.aspx" target="_self">Change usuario </a><br />
From this option you can change the usuario of your forum membem_rs. </td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="suspend_registration.aspx" target="_self">Suspend New Registrations</a><br />
         From this option you can Suspend New Users from Registering to use the forum.</td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td > <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText">General Forum Admin</span><br />
         The following pages are to help you setup, configure and administer your board</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="forum_configure.aspx" target="_self">Forum Configuration</a><br />
         Configure general options to customise the way the forum functions and looks.</td>
       </tr>
       <tr><%

'If this is the main forum admin let him change the admin usuario and clave
If portal.variablesForum.lngLoggedInUserID = 1 Then %>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="change_admin_usuario.aspx" target="_self">Change Admin usuario and clave</a><br />
         Definitely recommended for higher Forum security!</td>
       </tr>
       <%
End If

%>   
        <td bgcolor="#F5F5FA" class="text"><a href="date_time_configure.aspx" target="_self">Date and Time Settings</a><br />
         Change the format of dates and times in the forum or change time/date settings to your local settings if the server is in a foreign country to your own.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="email_notify_configure.aspx" target="_self"> Email Setup and Configuration </a><br />
         Configure email settings and enable email features such as email notification, email account activation, etc.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="member_mailier.aspx" target="_self">Mass Email Members</a><br />
         From this option you can send emails to all members or members of specific User Group.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="upload_configure.aspx" target="_self">File, Image and Avatar Upload Setup and Configuration </a><br />
         Allow users to upload images and files in their posts, also setup Avatar uploading.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="spam_configure.aspx" target="_self">Anti-Spam Configuration</a><br />
         Configure the Anti-Spam settings so you don't get members spamming the forum with 1,000's of unwanted and abusive posts in minutes.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="bad_word_filter_configure.aspx" target="_self">Configure the Bad Word Filter</a><br />
         Remove or add new swear words to the bad word filter.</td>
       </tr><%
       
'If this is an access database show the compact and repair feature
If portal.variablesForum.strDatabaseType = "Access" Then %>      
<tr>
        <td bgcolor="#F5F5FA" class="text">
<p><a href="compact_access_db.aspx">Compact and Repair Access Database</a><br />
          Form here you can compact and repair your Forums Access database to increase performance.</p>
         </td>
       </tr><%

End If %>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="statistics.aspx" target="_self">Forum Statistics</a><br />
         Displays a list of forum statistics.</td>
       </tr><%

'If the forum is using an SQL server then display stats oabout the server link
If portal.variablesForum.strDatabaseType = "SQLServer" Then %>
       <tr>
        <td bgcolor="#F5F5FA" class="text">
<p><a href="sql_server_db_stats.aspx">MS SQL Server Database Statistics</a><br />
          Displays statistics about the MS SQL Server Database you are using for your forum.</p>
         </td>
       </tr><%
End If %>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td > <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText"><b>Ban Administration</b></span><b><br />
         </b>The following pages are to help you control who uses the forums</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="ip_blocking.aspx" target="_self">IP Address Banning</a><br />
         Use this option to Ban IP addresses and Ranges.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="email_domain_blocking.aspx" target="_self">Email Address Banning</a><br />
         Use this option to Ban Email address and Email Domains from being registered on the board.</td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td> 
   <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr> 
     <td> 
      <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="text"><span class="lgText">Forum Clearout and Archive</span><br />
         The following pages are to clear out the database if you find it is getting a little full and slowing down and to archive topics</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="resync_forum_post_count.aspx" target="_self">Re-sync Topic and Post Count</a><br />
         Re-sync the Topic and Post Count display for the forum's</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="archive_topics_form.aspx" target="_self">Batch Lock Old Topics</a><br />
         Batch lock old Topics allows you to batch lock Topics that haven't been posted in for sometime.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_posts_form.aspx" target="_self">Batch Delete Topics</a><br />
         Clean out the Forum Database by batch deleting topics that have not been posted in for sometime.</td>
       </tr>
       <tr>
        <td bgcolor="#F5F5FA" class="text"><a href="batch_move_topics_form.aspx" target="_self">Batch Move Topics</a><br />
         Batch move Topics from one forum to another.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_pm_form.aspx" target="_self">Batch Delete Private Messages</a><br />
         Clean out the Forum Database by batch deleting old Private Messages.</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_members_form.aspx" target="_self">Batch Delete Members</a><br />
         Clean out the Forum Database by batch deleting Members who have never posted.</td>
       </tr>
      </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
<br />
<br />
<table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
 <tr> 
  <td> 
   <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
    <tr> 
     <td> 
      <table width="100%" border="0" cellspacing="0" cellpadding="7">
       <tr> 
        <td bgcolor="#CCCEE6" class="lgText">Removing the Forum links</td>
       </tr>
       <tr> 
        <td bgcolor="#F5F5FA" class="text"><a href="remove_link_buttons.aspx">Remove Powered By FebrerSoftware links</a><br />
         Remove the Powered by FebrerSoftware links from the forum application pages. </td>
       </tr>
      </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
<div align="center"><br />
 <br />
 <table width="98%" border="0" cellspacing="0" cellpadding="1" bgcolor="#000000">
  <tr> 
   <td width="986"> 
    <table width="100%" border="0" cellspacing="0" cellpadding="4" bgcolor="#EFEF#F5F5FA">
     <tr> 
      <td width="100%" height="186" align="center" bgcolor="#EAEAF4" class="text">I have spent many 1000's of unpaid hours in development and support this and the other applications available for free from 
       Web Wiz Guide. 
       <p class="text">If you like using this application then please help support the development and update of this and future applications from Web Wiz Guide by purchasing a link removal key for Web 
        Wiz Forums. For more info click on the link below:-<br />
        <br />
        <a href="remove_link_buttons.aspx">Remove Powered By FebrerSoftware links</a><br />
        <br />
        The FebrerSoftware application remains free and you may still use it as much as you like both privately and commercially, the purchasing the link removal key is only a request to help me cover some 
        of the costs involved.<br />
        <br />
        For more info contact: -<br />
        <a href="mailto:purchase@webwizguide.info">purchase@webwizguide.info</a><br />
        Web Wiz Guide, PO Box 4982, Bournemouth, BH8 8XP, United Kingdom. </p>
      </td>
     </tr>
    </table>
   </td>
  </tr>
 </table>
 <br />
 <a href="http://www.webwizforums.com" target="_blank" class="boldLink">Check for updates to FebrerSoftware</a><br />
 <br />
 <a href="http://www.webwizguide.info/asp/sample_scripts/default.aspx" target="_blank">Other Free ASP Applications from Web Wiz Guide</a><br />
 </div>
</body>
</html>
<%
'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
