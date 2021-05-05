

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 



%>  
<html>
<head>

<title>Close Forum Topics</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Batch Close Forum Topics</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  Batch Close Topics that have not been posted in for sometime by locking them so they can no-longer be posted in.<br />
  <br />
  Select the Topics you want closed by Forum and when a message was last posted in them.<br />
 </p>
</div>
<form method="post" name="frmSelectForum" action="archive_topics.aspx" onSubmit="return confirm('Are you sure you want to achive these topics?')">
 <table width="560" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="8">
  <tr> 
   <td height="24"> <table border="0" align="center"  height="95" cellpadding="4" cellspacing="1" width="100%">
     <tr bgcolor="#CCCEE6" > 
      <td width="51%" height="2" align="left" valign="top" class="tHeading"><b>Close Topics in</b></td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#F5F5FA">
<select name="FID">
        <option value="0" selected>All Forums</option>
        <%

'Read in the forum name from the database
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Forum.Forum_name, " & portal.variablesForum.strDbTable & "Forum.Forum_ID FROM " & portal.variablesForum.strDbTable & "Category, " & portal.variablesForum.strDbTable & "Forum WHERE " & portal.variablesForum.strDbTable & "Category.Cat_ID = " & portal.variablesForum.strDbTable & "Forum.Cat_ID ORDER BY " & portal.variablesForum.strDbTable & "Category.Cat_ORDER ASC, " & portal.variablesForum.strDbTable & "Forum.Forum_Order ASC;"

'Query the database
rsCommon=db.execute(strSQL)

'Loop through all the froum in the database
Do while NOT rsCommon.EOF 

	'Display a link in the link list to the forum
	Response.Write vbCrLf & "<option value=" & CLng(rsCommon("Forum_ID")) & " "
	response.write(">" & rsCommon("Forum_name") & "</option>"		
			
	'Move to the next record in the recordset
	rsCommon.MoveNext
Loop

'Reset server objects
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
       </select>
      </td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#CCCEE6" class="tHeading"><b>Close Topics that haven't been posted in for</b></td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#F5F5FA">
<select name="days">
        <option value="0">Now</option>
        <option value="7">1 Week</option>
        <option value="14">2 Weeks</option>
        <option value="31">1 Month</option>
        <option value="62">2 Months</option>
        <option value="124">4 Months</option>
        <option value="182" selected>6 Months</option>
        <option value="279">9 Months</option>
        <option value="365">1 Year</option>
        <option value="730">2 Years</option>
       </select>
      </td>
     </tr>
     <tr bgcolor="#FFFFFF">
      <td  height="12" align="left" bgcolor="#CCCEE6" class="tiHeading">Select which type of topics to close</td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#F5F5FA" class="tiHeading">
<select name="priority">
        <option value="4" selected>All Topics</option>
        <option value="0">Normal Topics Only</option>
        <option value="1">Sticky Topics Only</option>
        <option value="2">Announcements Only</option>
        <option value="3">Announcements (All Forums) Only</option>
       </select>
      </td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#CCCEE6" class="tiHeading">Select below to open closed topics if you have accidentally closed the wrong ones</td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#F5F5FA" class="text"> 
       <input name="closeTopic" type="radio" value="true" checked>
       Close Topics<br /> <input type="radio" name="closeTopic" value="false">
       Open Closed Topics</td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type='submit' name="Submit" value="Close Topics">
 </div>
</form>
<br />
</body>
</html>
