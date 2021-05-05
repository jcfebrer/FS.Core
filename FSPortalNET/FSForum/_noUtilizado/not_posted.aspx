<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" CodeFile="not_posted.aspx.vb" Inherits="not_posted" %>
<%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %>
<%@ Register TagPrefix="navigation" TagName="navigation" Src="~/forum/includes/navigation_buttons_inc.ascx" %>


<common:common ID="common" runat="server" />
<%
'Response.Buffer = True 

'Dimension variables
Dim strErrorCode as string		'Holds the error code of the page

'Read in the error code
strErrorCode = Request.QueryString("mode")
%>  

<navigation:navigation ID="common1" runat="server" />
  <table width="<% = portal.variablesForum.strTableVariableWidth %>" border="0" cellspacing="0" cellpadding="3" align="center">
 <tr> 
  <td align="left" class="heading"><% = portal.variablesForum.strTxtMessageNotPosted %></td>
</tr>
 <tr> 
  <td align="left" width="71%" class="bold"><img src="<% = portal.variablesForum.strImagePath %>open_folder_icon.gif" border="0" align="middle">&nbsp;<a href="default.aspx" target="_self" class="boldLink"><% = portal.variablesForum.strMainForumName %></a><% = portal.variablesForum.strNavSpacer %><% = portal.variablesForum.strTxtMessageNotPosted %><br /></td>
  </tr>
</table>
<div align="center">
  <br /><br /><br />
  <span class="text"><%
'Reset Server Objects
'Set rsCommon = Nothing
'adoCon.Close
'Set adoCon = Nothing
 
'Write the error message
If strErrorCode = "posted" Then
	Response.Write(portal.variablesForum.strTxtDoublePostingIsNotPermitted) 
ElseIf strErrorCode = "noSubject" Then
	Response.Write(portal.variablesForum.strTxtYourMessageNoValidSubjectHeading) 
ElseIf strErrorCode = "maxS" OR strErrorCode = "maxM" Then    
	Response.Write("<span class=""lgText"">" & portal.variablesForum.strTxtSpammingIsNotPermitted & "</span><br />" & portal.variablesForum.strTxtYouHaveExceededNumOfPostAllowed) 
ElseIf strErrorCode = "noPoll" Then
	Response.Write(portal.variablesForum.strTxtYourNoValidPoll) 
ElseIf strErrorCode = "FLocked" Then
	Response.Write(portal.variablesForum.strTxtThisForumIsLocked) 
ElseIf strErrorCode = "TClosed" Then
	Response.Write(portal.variablesForum.strTxtThisTopicIsLocked) 
End If        
        %></span><br />
  <br /><a href="javascript:history.back(1)" target="_self"><% = portal.variablesForum.strTxtReturnToDiscussionForum %></a>
   <br /><br /><br /><br /><br />
</div>
<div align="center">
<% 

'Display the process time
If portal.variablesForum.blnShowProcessTime Then response.write("<span class=""smText""><br /><br />" & portal.variablesForum.strTxtThisPageWasGeneratedIn & " " & FormatNumber(Timer() - portal.variablesForum.dblStartTime, 4) & " " & portal.variablesForum.strTxtSeconds & "</span>")
%>
</div> 

