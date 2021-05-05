

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="language_files/help_language_file_inc.aspx" -->
<%
'Set the buffer to true
Response.Buffer = True

'Declare variables
Dim intForumID

%>
<html>
<head>

<title>Forum Help</title>


     	
     	
</script>
<!-- #include file="includes/header.aspx" -->
<navigation:navigation ID="common1" runat="server" />
<a name="top"></a>
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtForumHelp %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = strMainForumName %></a><% = strNavSpacer %><% = portal.variablesForum.strTxtForumHelp %><br /></td>
  </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>">
   <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr>
     <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtChooseAHelpTopic %></td>
    </tr>
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text">
      <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr>
           <td class="bold"><% = portal.variablesForum.strTxtLoginAndRegistration %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ1"><% = portal.variablesForum.strTxtWhyCantILogin %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ2"><% = portal.variablesForum.strTxtDoINeedToRegister %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ3"><% = portal.variablesForum.strTxtLostclaves %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ4"><% = portal.variablesForum.strTxtIRegisteredInThePastButCantLogin %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><% = portal.variablesForum.strTxtUserPreferencesAndForumSettings %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ5"><% = portal.variablesForum.strTxtHowDoIChangeMyForumSettings %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ6"><% = portal.variablesForum.strTxtForumTimesAndDates %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ7"><% = portal.variablesForum.strTxtWhatDoesMyRankIndicate %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ8"><% = portal.variablesForum.strTxtCanIChangeMyRank %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><% = portal.variablesForum.strTxtPostingIssues %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ9"><% = portal.variablesForum.strTxtHowPostMessageInTheForum %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ10"><% = portal.variablesForum.strTxtHowDeletePosts %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ11"><% = portal.variablesForum.strTxtHowEditPosts %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ12"><% = portal.variablesForum.strTxtHowSignaturToMyPost %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ13"><% = portal.variablesForum.strTxtHowCreatePoll %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ14"><% = portal.variablesForum.strTxtWhyNotViewForum %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ15"><% = portal.variablesForum.strTxtInternetExplorerWYSIWYGPosting %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><% = portal.variablesForum.strTxtMessageFormatting %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ16"><% = portal.variablesForum.strTxtWhatForumCodes %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ17"><% = portal.variablesForum.strTxtCanIUseHTML %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ18"><% = portal.variablesForum.strTxtWhatEmoticons %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ19"><% = portal.variablesForum.strTxtCanPostImages %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ20"><% = portal.variablesForum.strTxtWhatClosedTopics %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><% = portal.variablesForum.strTxtUsergroups %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ21"><% = portal.variablesForum.strTxtWhatForumAdministrators %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ22"><% = portal.variablesForum.strTxtWhatForumModerators %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ23"><% = portal.variablesForum.strTxtWhatUsergroups %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><% = portal.variablesForum.strTxtPrivateMessaging %></td>
          </tr>
          <tr> 
           <td><a href="#FAQ24"><% = portal.variablesForum.strTxtIPrivateMessages %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ25"><% = portal.variablesForum.strTxtIPrivateMessagesToSomeUsers %></a></td>
          </tr>
          <tr> 
           <td><a href="#FAQ26"><% = portal.variablesForum.strTxtHowCanPreventSendingPrivateMessages %></a></td>
          </tr>
         </table>
     </td>
    </tr>
   </table>
  </td>
 </tr>
</table>
</td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtLoginAndRegistration %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ1"></a><% = portal.variablesForum.strTxtWhyCantILogin %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ1 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ2"></a><% = portal.variablesForum.strTxtDoINeedToRegister %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ2 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ3"></a><% = portal.variablesForum.strTxtLostclaves %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ3 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ4"></a><% = portal.variablesForum.strTxtIRegisteredInThePastButCantLogin %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ4 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtUserPreferencesAndForumSettings %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ5"></a><% = portal.variablesForum.strTxtHowDoIChangeMyForumSettings %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ5 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ6"></a><% = portal.variablesForum.strTxtForumTimesAndDates %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ6 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ7"></a><% = portal.variablesForum.strTxtWhatDoesMyRankIndicate %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ7 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ8"></a><% = portal.variablesForum.strTxtCanIChangeMyRank %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ8 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtPostingIssues %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ9"></a><% = portal.variablesForum.strTxtHowPostMessageInTheForum %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ9 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ10"></a><% = portal.variablesForum.strTxtHowDeletePosts %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ10 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ11"></a><% = portal.variablesForum.strTxtHowEditPosts %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ11 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ12"></a><% = portal.variablesForum.strTxtHowSignaturToMyPost %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ12 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ13"></a><% = portal.variablesForum.strTxtHowCreatePoll %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ13 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ14"></a><% = portal.variablesForum.strTxtWhyNotViewForum %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ14 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ15"></a><% = portal.variablesForum.strTxtInternetExplorerWYSIWYGPosting %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ15 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtMessageFormatting %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ16"></a><% = portal.variablesForum.strTxtWhatForumCodes %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ16 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ17"></a><% = portal.variablesForum.strTxtCanIUseHTML %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ17 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ18"></a><% = portal.variablesForum.strTxtWhatEmoticons %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ18 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ19"></a><% = portal.variablesForum.strTxtCanPostImages %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ19 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ20"></a><% = portal.variablesForum.strTxtWhatClosedTopics %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ20 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtUsergroups %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ21"></a><% = portal.variablesForum.strTxtWhatForumAdministrators %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ21 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ22"></a><% = portal.variablesForum.strTxtWhatForumModerators %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ22 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ23"></a><% = portal.variablesForum.strTxtWhatUsergroups %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ23 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="1" bgcolor="<% = portal.variablesForum.strTableBorderColour %>" align="center">
 <tr> 
  <td> <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
    <tr> 
     <td bgcolor="<% = portal.variablesForum.strTableBgColour %>"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14" bgcolor="<% = portal.variablesForum.strTableBgColour %>">
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableTitleColour %>" height="2" class="tHeading" background="<% = portal.variablesForum.strTableTitleBgImage %>"><% = portal.variablesForum.strTxtPrivateMessaging %></td>
       </tr>
       <tr> 
        <td bgcolor="<% = portal.variablesForum.strTableColour %>" background="<% = portal.variablesForum.strTableBgImage %>" class="text"> <table width="100%" border="0" cellspacing="0" cellpadding="0" class="text">
          <tr> 
           <td class="bold"><a name="FAQ24"></a><% = portal.variablesForum.strTxtIPrivateMessages %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ24 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ25"></a><% = portal.variablesForum.strTxtIPrivateMessagesToSomeUsers %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ25 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
          <tr> 
           <td>&nbsp;</td>
          </tr>
          <tr> 
           <td class="bold"><a name="FAQ26"></a><% = portal.variablesForum.strTxtHowCanPreventSendingPrivateMessages %></td>
          </tr>
          <tr> 
           <td class="text"><% = portal.variablesForum.strTxtFAQ26 %></td>
          </tr>
          <tr> 
           <td><a href="#top" target="_self" class="smLink"><% = portal.variablesForum.strTxtBackToTop %></a></td>
          </tr>
         </table></td>
       </tr>
      </table></td>
    </tr>
   </table></td>
 </tr>
</table>
<br />
<table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="4" align="center">
 <tr>
  <form>
   <td>
    <!-- #include file="includes/forum_jump_inc.aspx" -->
   </td>
  </form>
 </tr>
</table>
<div align="center"><br /><%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

	


'Display the process time
If blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>"
%>
</div>

