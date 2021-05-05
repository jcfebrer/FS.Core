
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

<title>Batch Delete Members</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center">
 <p class="text"><span class="heading">Batch Delete Members</span><br />
  <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
  <br />
  If you find the forum starts running a bit slow it maybe worth cleaning the database out by deleting Members who have never posted.<br />
  <br />
  Select the length of time it has been since the <strong>non-posting</strong> members signed up on.<br />
 </p>
</div>
<form action="batch_delete_membem_rs.aspx" method="post" name="frmDelete" id="frmDelete" onSubmit="return confirm('Are you sure you want to delete these Members?\n\nOnce the Members are deleted they will be lost forever.')">
 <table width="400" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000" height="8">
  <tr> 
   <td height="24" width="680"> <table width="100%" border="0" align="center"  height="43" cellpadding="4" cellspacing="1">
     <tr bgcolor="#CCCEE6" > 
      <td height="2" align="left" class="tHeading">Delete Members that have never posted that registered over</td>
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
      <td  height="12" align="left" bgcolor="#CCCEE6" class="tHeading">Select the type of members to delete</td>
     </tr>
     <tr bgcolor="#FFFFFF"> 
      <td  height="12" align="left" bgcolor="#F5F5FA" class="text"> 
       <input name="unactive" type="radio" value="false" checked>
       All member accounts<br />
       <input type="radio" name="unactive" value="true">
       Un-activated member accounts</td>
     </tr>
    </table></td>
  </tr>
 </table>
 <div align="center"><br />
  <input type='submit' name="Submit" value="Delete Members">
 </div>
</form>
<br />
</body>
</html>