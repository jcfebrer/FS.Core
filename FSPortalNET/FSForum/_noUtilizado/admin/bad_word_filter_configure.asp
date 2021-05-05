

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True 

%>
<html>
<head>

<title>Configure Bad Word Filter</title>

<link href="includes/default_style.css" rel="stylesheet" type="text/css">
</head>
<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Configure Bad Word Filter </span><br />
 <a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a><br />
 <br />
 <form name="frmAddNewWords" method="post" action="bad_word_filter_add_new.aspx">
  <table width="574" border="0" cellspacing="0" cellpadding="1" bgcolor="#000000">
   <tr> 
    <td> <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
      <tr> 
       <td align="center" bgcolor="#F5F5FA"> <span class="lgText"><b>Add New Bad Words To List</b></span><br /> <br />
        <span class="text">Type any words you would like to add to the Bad Word filter into the boxes below.<br />
        Both the <i>Bad Word</i> and the <i>Replace With</i> boxes must be filled in for each new word.<br />
        You can add between 1 and 3 new bad words at once. </span><br /> <br /> 
        <table width="300" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000">
         <tr> 
          <td> <table width="100%" border="0" cellspacing="1" cellpadding="4">
            <tr bgcolor="#CCCEE6" align="center"> 
             <td width="50%" class="tHeading"><b>Bad Word</b></td>
             <td width="50%" class="tHeading"><b>Replace With</b></td>
            </tr>
            <tr bgcolor="#FFFFFF"> 
             <td width="50%"> <input type='text' name="badWord1" maxlength="49"> </td>
             <td width="50%"> <input type='text' name="replaceWord1" maxlength="49"> </td>
            </tr>
            <tr bgcolor="#FFFFFF"> 
             <td width="50%"> <input type='text' name="badWord2" maxlength="49"> </td>
             <td width="50%"> <input type='text' name="replaceWord2" maxlength="49"> </td>
            </tr>
            <tr bgcolor="#FFFFFF"> 
             <td width="50%"> <input type='text' name="badWord3" maxlength="49"> </td>
             <td width="50%"> <input type='text' name="replaceWord3" maxlength="49"> </td>
            </tr>
           </table></td>
         </tr>
        </table>
        <br /> <input type='submit' name="Submit" value="Add New Bad Words To List"> <br /> <br /> </td>
      </tr>
     </table></td>
   </tr>
  </table>
  <br />
  <br />
 </form>
 <table width="574" border="0" cellspacing="0" cellpadding="1" bgcolor="#000000">
  <tr> 
   <td> <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF">
     <tr> 
      <td height="255" bgcolor="#F5F5FA"> 
       <div align="center">
        <p class="text"><span class="lgText"><b>Remove Bad Words From List</b></span><b><br />
         </b><br />
         Place a tick in the checkbox for any bad words you wish to remove from the list <br />
         then click on the Delete Bad Words from List button.</p>
       </div>
       <div align="center"> 
        <form name="frmModerators" method="post" action="bad_word_filter_delete.aspx">
         <table width="300" border="0" cellspacing="0" cellpadding="0" bgcolor="#000000">
          <tr> 
           <td bgcolor="#000000"> <table width="100%" border="0" cellspacing="1" cellpadding="4" height="14">
             <tr bgcolor="#CCCEE6" class="tHeading"> 
              <td width="18%" height="2" align="center"><b>Delete</b></td>
              <td width="44%" height="2" align="center"><b>Bad Word</b></td>
              <td width="38%" height="2" align="center"><b>Replaced With</b></td>
             </tr>
             <%
						
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT " & portal.variablesForum.strDbTable & "Smut.* FROM " & portal.variablesForum.strDbTable & "Smut ORDER BY " & portal.variablesForum.strDbTable & "Smut.Smut ASC;"
				
'Query the database
rsCommon=db.execute(strSQL)

'Display the bad words       		
Do While NOT rsCommon.EOF		
			%>
             <tr bgcolor="#FFFFFF"> 
              <td width="18%" align="center" height="24"> <input type='checkbox' name="chkWordID" value="<% = rsCommon("ID_no") %>"> </td>
              <td width="44%" height="24" align="left" class="text"> 
               <% = rsCommon("Smut") %>
              </td>
              <td width="38%" height="24" align="left" class="text"> 
               <% = rsCommon("Word_replace") %>
              </td>
             </tr>
             <%
		
	'Move to the next record in the database
	rsCommon.MoveNext
	
'Loop back round   	
Loop
	
'Reset server variable
rsCommon.Close
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing
%>
            </table></td>
          </tr>
         </table>
         <br />
         <input type='submit' name="Submit" value="Delete Bad Words From List">
         <br />
        </form>
       </div></td>
     </tr>
    </table></td>
  </tr>
 </table>
 <br />
</div>
<br />
</body>
</html>