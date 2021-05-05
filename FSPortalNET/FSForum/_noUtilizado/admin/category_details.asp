

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true
Response.Buffer = True 


'Dimension variables				
Dim strMode		'holds the mode of the page, set to true if changes are to be made to the database
Dim strCatName		'Holds the name of the category
Dim intCatID		'Holds the ID number of the category
 

'Read in the details
intCatID = CInt(Request.QueryString("CatID"))
strMode = Request("mode")
	

'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Category.* From " & portal.variablesForum.strDbTable & "Category WHERE " & portal.variablesForum.strDbTable & "Category.Cat_ID=" & intCatID & ";"

'Set the Lock Type for the records so that the record set is only locked when it is updated
rsCommon.LockType = 3
	
'Query the database
rsCommon=db.execute(strSQL)

'If this is a post back then save the category
If (strMode = "edit" OR strMode = "new") AND CBool(Request.Form("postBack")) Then

	'If this is a new one add new
	If strMode = "new" Then rsCommon.AddNew

	'Update the recordset
	rsCommon.Fields("Cat_name") = Request.Form("category")
					
	'Update the database with the category details
	rsCommon.Update
	
		
	'Release server varaibles
	rsCommon.Close
	Set rsCommon = Nothing
	adoCon.Close
	Set adoCon = Nothing
		
	Response.Redirect("view_forums.aspx")
		
	'Re-run the query to read in the updated recordset from the database
	rsCommon.Requery	
End If

'Read in the forum details from the recordset
If NOT rsCommon.EOF Then
	
	'Read in the forums from the recordset
	intCatID = CInt(rsCommon("Cat_ID"))
	strCatName = rsCommon("Cat_name")
End If

'Release server varaibles
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>  
<html>
<head>

<title>Category Details</title>

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a a category
	if (document.frmNewForum.category.value==""){
		alert("Please enter the Category");
		return false;
	}
	
	return true
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Category</span><br />
<a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <a href="view_forums.aspx" target="_self">Return to the Category and Forum Set up and Admin page</a><br />
</div>
<form method="post" name="frmNewForum" action="category_details.aspx?CatID=<% = intCatID %>" onSubmit="return CheckForm();">
 <br />
 <table width="450" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
  <tr> 
   <td width="450"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
     <tr align="left" bgcolor="#CCCEE6"> 
      <td colspan="2" class="lgText"><b>Category Details</b></td>
     </tr>
     <tr class="arial" bgcolor="#FFFFFF"> 
      <td width="33%" align="left" bgcolor="#F5F5FA" class="text">Category*</td>
      <td width="67%" valign="top" bgcolor="#F5F5FA"> 
       <input type='text' name="category" maxlength="50" size="30" value="<% = Server.HTMLEncode(strCatName) %>" >
      </td>
     </tr>
    </table></td>
  </tr>
 </table>
 <br />
 <div align="center"><br />
  <input type="hidden" name="postBack" value="true">
  <input type="hidden" name="mode" value="<% = strMode %>">
  <input name="Submit" type='submit' id="Submit" value="Submit Category">
  <input name="Reset" type="reset" id="Reset" value="Reset Form">
  <br />
 </div>
</form>
</body>
</html>