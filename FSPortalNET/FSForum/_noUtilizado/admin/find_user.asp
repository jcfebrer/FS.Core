

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_common.aspx" -->
<%
'Set the response buffer to true
Response.Buffer = True



'Dimension variables
Dim strMemberName	'Holds the member name to lookup
Dim blnMemNotFound	'Holds the error code if user not found
Dim lngMemberID		'Holds the ID number of the member




'If this is a postback check for the user exsisting in the db before redirecting
If Request.Form("postBack") Then
	
	'Initliase varaibles
	blnMemNotFound = false
	
	'Read in the members name to lookup
	strMemberName = Request.Form("member")
	
	'Take out parts of the usuario that are not permitted
	strMemberName = disallowedMemberNames(strMemberName)
	
	'Get rid of milisous code
	strMemberName = formatSQLInput(strMemberName)

	'Initalise the strSQL variable with an SQL statement to query the database
	strSQL = "SELECT " & "Usuarios.UsuarioID From " & "Usuarios WHERE " & "Usuarios.usuario='" & strMemberName & "';"
	
	'Query the database
	rsCommon=db.execute(strSQL)
	
	'See if a user with that name is returned by the database
	If NOT rsCommon.EOF Then
		
		'Read in the user ID
		lngMemberID = CLng(rsCommon("UsuarioID"))
		
		'Reset Server Objects
		rsCommon.Close
		Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing
	
		'Redirct to next page
		Response.Redirect("forum_user_permissions.aspx?UID=" & lngMemberID)
	
	'Else there is no user with that name returned so set an error code
	Else
	
		blnMemNotFound = true	
		
	End If


End If



'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
<html>
<head>

<title>Create Member Permissions</title>


     	

<!-- Check the from is filled in correctly before submitting -->
<script  language="javascript">
<!-- Hide from older browsem_rs...

//Function to check form is filled in correctly before submitting
function CheckForm () {

	//Check for a group
	if (document.frmAddMessage.member.value==""){
		alert("Please enter a members usuario");
		return false;
	}
	
	return true
}


//Function to open pop up window
function openWin(theURL,winName,features) {
  	window.open(theURL,winName,features);
}
// -->
</script>
<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Member Permissions</span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <span class="text">When you created a forum you created Generic Permissions for that forum to allow users to do things like, View, Post, Reply, etc. in that forum. </span> 
 <p class="text">From here you can overwrite these Generic Forum Permissions and User Group Permissions for different Members allowing different Members to have different permissions on forums, you can 
  also select Members to be able to have moderator privileges on forums.</p>
 <p class="text">Select the Forum Member that you would like to Create, Edit, or Remove Member Permissions for.</p>
 <p></p>
</div>
<form action="find_user.aspx" method="post" name="frmAddMessage" target="_self" id="frmAddMessage" onSubmit="return CheckForm();">
 <tr>
  <td width="500"><br /> 
   <table width="500" border="0" cellspacing="0" cellpadding="0" align="center" bgcolor="#000000">
    <tr>
   <td width="500"> <table width="100%" border="0" align="center" class="normal" cellpadding="4" cellspacing="1">
       <tr bgcolor="#CCCEE6"> 
        <td class="tHeading"><b> Select a Member</b></td>
       </tr>
       <tr bgcolor="#FFFFFF"> 
        <td bgcolor="#F5F5FA" class="text">usuario 
         <input name="member" type='text' id="member" size="20" maxlength="25" value="<% = strMemberName %>">
         <input type='submit' name="Submit" value="Next &gt;&gt;">
         <input type="button" name="Button" value="Search for Member" onClick="openWin('../pop_up_member_search.aspx','profile','toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=1,width=440,height=255')">
         <input type="hidden" name="postBack" value="true" />
        </td>
       </tr>
      </table></td>
  </tr>
 </table>
 <div align="center"><br />
    <br />
 </div>
</form><%

'If the usuario is already gone display an error message pop-up
If blnMemNotFound  Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('The usuario entered could not be found.\n\nPlease check your spelling and try again.');")
        Response.Write("</script>")

End If 

%>
</body>
</html>