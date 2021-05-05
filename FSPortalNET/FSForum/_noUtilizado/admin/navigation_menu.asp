

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
%>
<html>
<head>
<title>Admin Navigation</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">



     	
     	
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>

<body  background="images/main_bg.gif" />
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
 <tr>
  <td align="center" class="heading">Admin Menu<br />
   <br />
   <table width="98%" border="0" cellspacing="0" cellpadding="1" align="center" bgcolor="#000000">
    <tr> 
     <td width="690"> <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
       <tr> 
        <td> <table width="100%" border="0" cellspacing="0" cellpadding="4">
          <tr> 
           <td bgcolor="#CCCEE6" class="text"><span class="bold"><b>Admin</b></span></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA"><a href="admin_menu.aspx" target="mainFrame" class="smLink">Admin Menu</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="view_forums.aspx" target="mainFrame" class="smLink">Forum Admin</a></td>
          </tr>
          <tr>
           <td bgcolor="#F5F5FA" class="text"><a href="close_forums.aspx" target="mainFrame" class="smLink">Lock Forums</a></td>
          </tr>
          <tr>
           <td bgcolor="#F5F5FA" class="text"><a href="http://www.webwizforums.com/update.aspx?v=<% = Server.URLEncode(strVersion) %>" target="_blank" class="smLink">Check for Updates </a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="http://www.webwizforums.com" target="_blank" class="smLink">About</a></td>
          </tr>
          <tr> 
           <td bgcolor="#CCCEE6" class="text"><span class="bold"><b>User Group</b></span><b> Admin</b></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="view_groups.aspx" target="mainFrame" class="smLink">Group Admin</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="group_perm_forum.aspx" target="mainFrame" class="smLink">Group Permissions Admin</a></td>
          </tr>
          <tr> 
           <td bgcolor="#CCCEE6" class="bold">Member Admin</td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="select_membem_rs.aspx" target="mainFrame" class="smLink">Membership Admin</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="find_user.aspx" target="mainFrame" class="smLink">Member Permissions Admin</a></td>
          </tr>
          <tr>
           <td bgcolor="#F5F5FA" class="text"><a href="add_member.aspx" target="mainFrame" class="smLink">Add New Member </a></td>
          </tr>
          <tr>
           <td bgcolor="#F5F5FA" class="text"><a href="change_usuario.aspx" target="mainFrame" class="smLink">Change usuario </a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="suspend_registration.aspx" target="mainFrame" class="smLink">Suspend Registration</a></td>
          </tr>
          <tr> 
           <td bgcolor="#CCCEE6" class="text"><span class="bold">General Admin</span></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="forum_configure.aspx" target="mainFrame" class="smLink">Forum Configuration</a></td>
          </tr>
          <tr><%
          
'If this is the main forum admin let him change the admin usuario and clave
If portal.variablesForum.lngLoggedInUserID = 1 Then %>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="change_admin_usuario.aspx" target="mainFrame" class="smLink">Change Admin usuario and clave</a></td>
          </tr><%
End If

%>
           <td bgcolor="#F5F5FA" class="text"><a href="date_time_configure.aspx" target="mainFrame" class="smLink">Forum Date and Time Settings</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="email_notify_configure.aspx" target="mainFrame" class="smLink">Email Configuration Setup</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="member_mailier.aspx" target="mainFrame" class="smLink">Mass Email Members</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="upload_configure.aspx" target="mainFrame" class="smLink">File and Image Upload Setup and Configuration </a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="spam_configure.aspx" target="mainFrame" class="smLink">Anti-Spam Configuration</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="bad_word_filter_configure.aspx" target="mainFrame" class="smLink">Configure the Bad Word Filter</a></td>
          </tr><%
       
'If this is an access database show the compact and repair feature
If portal.variablesForum.strDatabaseType = "Access" Then %>  
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="compact_access_db.aspx" target="mainFrame" class="smLink">Compact Database</a></td>
          </tr><%
End If %>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="statistics.aspx" target="mainFrame" class="smLink">Forum Statistics</a></td>
          </tr> <%

'If the forum is using an SQL server then display stats oabout the server link
If portal.variablesForum.strDatabaseType = "SQLServer" Then %>
           <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="sql_server_db_stats.aspx" target="mainFrame" class="smLink">SQL DB Statistics</a></td>
          </tr><%
End If %>
          <tr> 
           <td bgcolor="#CCCEE6" class="text"><span class="bold"><b>Ban Admin</b></span></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="ip_blocking.aspx" target="mainFrame" class="smLink">IP Address Banning</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="email_domain_blocking.aspx" target="mainFrame" class="smLink">Email Address Banning</a></td>
          </tr>
          <tr> 
           <td bgcolor="#CCCEE6" class="text"><span class="bold">Forum Clearout and Archive</span></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="resync_forum_post_count.aspx" target="mainFrame" class="smLink">Re-sync Topic and Post Count</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="archive_topics_form.aspx" target="mainFrame" class="smLink">Batch Close Old Topics</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_posts_form.aspx" target="mainFrame" class="smLink">Batch Delete Topics</a></td>
          </tr>
          <tr>
           <td bgcolor="#F5F5FA" class="text"><a href="batch_move_topics_form.aspx" target="mainFrame" class="smLink">Batch Move Topics</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_pm_form.aspx" target="mainFrame" class="smLink">Batch Delete Private Messages</a></td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA" class="text"><a href="batch_delete_members_form.aspx" target="mainFrame" class="smLink">Batch Delete Members</a></td>
          </tr>
          <tr> 
           <td bgcolor="#CCCEE6" class="bold">Removing links</td>
          </tr>
          <tr> 
           <td bgcolor="#F5F5FA"><a href="remove_link_buttons.aspx" target="mainFrame" class="smLink">Remove Powered By links</a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table> </td>
 </tr>
</table>
</body>
</html>
