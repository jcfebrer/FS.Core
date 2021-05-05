
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>

<title>Batch Delete Private Messages</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Batch Delete Private Messages</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  If you find the forum starts running a bit slow it maybe worth cleaning the database out by deleting Private Messages.<br />
  <br />
  Select the Private Messages you want deleted by the date the Private Message was sent.<br />
 </p>
</div>
<form method="post" name="frmDeletePM" action="batch_delete_pm.aspx" onSubmit="return confirm('Are you sure you want to delete these Private Messages?\n\nOnce the Private Messages are deleted they will be lost forever.')">
 <table width="300" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="8">
  <tr> 
   <td height="24" width="680"> <table width="100%" border="0" align="center"  height="8" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6" > 
      <td height="2" align="left" class="tHeading">Delete Private Messages older than</td>
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
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type='submit' name="Submit" value="Delete Private Messages">
 </div>
</form>
<br />
</body>
</html>