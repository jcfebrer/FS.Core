

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<!--#include file="functions/functions_format_post.aspx" -->
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim strEmail                    'Holds the new users e-mail address
Dim intUsersGroupID             'Holds the users group ID
Dim blnShowEmail                'Boolean set to true if the user wishes there e-mail address to be shown
Dim strLocation                 'Holds the new users location
Dim strHomepage                 'Holds the new users homepage if they have one
Dim strAvatar                   'Holds the avatar image
Dim strCheckusuario            'Holds the usuarios from the database recordset to check against the new users requested usuario
Dim blnAutoLogin                'Boolean set to true if the user wants auto login trured on
Dim strImageFileExtension       'holds the file extension
Dim blnAccountReactivate        'Set to true if the users account needs to be reactivated
Dim blnSentEmail                'Set to true if the e-mail has been sent
Dim strEmailBody                'Holds the body of the welcome message e-mail
Dim strSubject                  'Holds the subject of the e-mail
Dim strSignature                'Holds the signature
Dim strICQNum                   'Holds the users ICQ Number
Dim strAIMAddress               'Holds the users AIM address
Dim strMSNAddress               'Holds the users MSN address
Dim strYahooAddress             'Holds the users Yahoo Address
Dim strOccupation               'Holds the users Occupation
Dim strInterests                'Holds the users Interests
Dim dtmDateOfBirth              'Holds the users Date Of Birth
Dim blnPMNotify                 'Set to true if the user want email notification of PM's
Dim strSmutWord                 'Holds the smut word to give better performance so we don't need to keep grabbing it form the recordset
Dim strSmutWordReplace          'Holds the smut word to be replaced with
Dim strMode                     'Holds the mode of the page
Dim blnEmailOK                  'Set to true if e-mail is not already in the database
Dim blnusuarioOK               'Set to true if the usuario requested does not already exsist
Dim intForumStartingGroup       'Holds the forum starting group ID number
Dim strEncyptedclave         'Holds the encrypted clave
Dim blnclaveChange           'Holds if the clave is changed or not
Dim blnEmailBlocked             'set to true if the email address is blocked
Dim strCheckEmailAddress        'Holds the email address to be checked
Dim lngUserProfileID            'Holds the users ID of the profile to get
Dim blnAdminMode                'Set to true if admin mode is enabled to update other members profiles
Dim blnUserActive               'Set to true if the users membership is active
Dim lngPosts                    'Holds the number of posts the user has made
Dim intDOBYear			'Holds the year of birth
Dim intDOBMonth			'Holds the month of birth
Dim intDOBDay			'Holds the day of birth
Dim strRealName			'Holds the persons real name
Dim strMemberTitle		'Holds the members title
Dim dtmServerTime		'Holds the current server time
Dim lngLoopCounter		'Holds the generic loop counter for page
Dim intUpdatePartNumber		'If an update holds which part to update
Dim strTimeOffSet		'Holds the server time off set
Dim blnAttachSignature
Dim strDateFormat
Dim intTimeOffSet
Dim blnReplyNotify
Dim blnWYSIWYGEditor
Dim strImagePath
Dim blnEmoticons


'Initlise variables
blnusuarioOK = True


'If the Profile has already been edited then update the Profile
If Request.Form("postBack") Then

	'******************************************
	'***  Read in member details from form	***
	'******************************************

        'Read in the users details from the form
        strusuario = Trim(Mid(Request.Form("name1"), 1, 15))
        strclave = LCase(Trim(Mid(Request.Form("clave1"), 1, 15)))
	strEmail = Trim(Mid(Request.Form("email"), 1, 60))
	strRealName = Trim(Mid(Request.Form("realName"), 1, 27))
	strLocation = Trim(Mid(Request.Form("location"), 1, 40))
	strHomepage = Trim(Mid(Request.Form("homepage"), 1, 48))
	strSignature = Mid(Request.Form("signature"), 1, 200)
	blnAttachSignature = CBool(Request.Form("attachSig")) 
	'Check that the ICQ number is a number before reading it in
	If isNumeric(Request.Form("ICQ")) Then strICQNum = Trim(Mid(Request.Form("ICQ"), 1, 15))
	strAIMAddress = Trim(Mid(Request.Form("AIM"), 1, 60))
	strMSNAddress = Trim(Mid(Request.Form("MSN"), 1, 60))
	strYahooAddress = Trim(Mid(Request.Form("Yahoo"), 1, 60))
	strOccupation = Mid(Request.Form("occupation"), 1, 40)
	strInterests = Mid(Request.Form("interests"), 1, 130)
	'Check the date of birth is a date before entering it
	If Request.Form("DOBday") <> 0 AND Request.Form("DOBmonth") <> 0 AND Request.Form("DOByear") <> 0 Then
	       dtmDateOfBirth = CDate(DateSerial(Request.Form("DOByear"), Request.Form("DOBmonth"), Request.Form("DOBday")))
	End If
	blnShowEmail = CBool(Request.Form("emailShow"))
	blnPMNotify = CBool(Request.Form("pmNotify"))
	blnAutoLogin = CBool(Request.Form("Login"))
	portal.variablesForum.strDateFormat = Trim(Mid(Request.Form("funcFecha.DateFormat"), 1, 10))
	portal.variablesForum.strTimeOffSet = Trim(Mid(Request.Form("serverOffSet"), 1, 1))
	portal.variablesForum.intTimeOffSet = CInt(Request.Form("serverOffSetHours"))
	portal.variablesForum.blnReplyNotify = CBool(Request.Form("replyNotify"))
	blnWYSIWYGEditor = CBool(Request.Form("ieEditor"))
	blnUserActive = CBool(Request.Form("active"))
        intUsersGroupID = CInt(Request.Form("group"))
        lngPosts = CLng(Request.Form("posts"))
        strMemberTitle = Trim(Mid(Request.Form("memTitle"), 1, 40))



        '******************************************
	'***     Read in the avatar		***
	'******************************************

       strAvatar = Trim(Mid(Request.Form("txtAvatar"), 1, 95))

       'If the avatar text box is empty then read in the avatar from the list box
       If strAvatar = "http://" OR strAvatar = "" Then strAvatar = Trim(Request.Form("SelectAvatar"))

       'If there is no new avatar selected then get the old one if there is one
       If strAvatar = "" Then strAvatar = Request.Form("oldAvatar")

       'If the avatar is the blank image then the user doesn't want one
       If strAvatar = portal.variablesForum.strImagePath & "blank.gif" Then strAvatar = ""
        


        '******************************************
	'***     Clean up member details	***
	'******************************************

        'Clean up user input
        
        
	strRealName = removeAllTags(strRealName)
	strRealName = func.formatInput(strRealName)
	strHomepage = formatLink(strHomepage)
	strHomepage = func.formatInput(strHomepage)
	strLocation = removeAllTags(strLocation)
	strLocation = func.formatInput(strLocation)
	strAIMAddress = formatLink(strAIMAddress)
	strAIMAddress = func.formatInput(strAIMAddress)
	strMSNAddress = formatLink(strMSNAddress)
	strMSNAddress = func.formatInput(strMSNAddress)
	strYahooAddress = formatLink(strYahooAddress)
	strYahooAddress = func.formatInput(strYahooAddress)
	strOccupation = removeAllTags(strOccupation)
	strOccupation = func.formatInput(strOccupation)
	strInterests = removeAllTags(strInterests)
	strInterests = func.formatInput(strInterests)
	        
	'Call the function to format the signature
	strSignature = FormatPost(strSignature)
	
	'Call the function to format forum codes
	strSignature = FormatForumCodes(strSignature)
	
	 'Call the filters to remove malcious HTML code
	 strSignature = checkHTML(strSignature)
	        
	'Strip long text strings from signature
	strSignature = removeLongText(strSignature)
	
	
	'If the user has not entered a hoempage then make sure the homepage variable is blank
	If strHomepage = "http://" Then strHomepage = ""
	
            
	portal.variablesForum.strDateFormat = removeAllTags(portal.variablesForum.strDateFormat)
        portal.variablesForum.strDateFormat = func.formatInput(portal.variablesForum.strDateFormat)
	
	strMemberTitle = removeAllTags(strMemberTitle) 
	strMemberTitle = func.formatInput(strMemberTitle)
	
	'SQL safe format call
        strEmail = formatSQLInput(strEmail)
        
        'Remove any single quotes as they should not be in email addresses
        strEmail = Replace(strEmail, "'", "", 1, -1, 1)


	'******************************************
	'***     Check the avatar is OK		***
	'******************************************
	'If there is no . in the link then there is no extenison and so can't be an image
        If inStr(1, strAvatar, ".", 1) = 0 Then
                  strAvatar = ""
               
         'Else remove malicious code and check the extension is an image extension
         Else
                'Call the filter for the image
                strAvatar = checkImages(strAvatar)
                strAvatar = func.formatInput(strAvatar)
         End If
        

	'******************************************
	'***     Check the usuario is OK	***
	'******************************************


        'Check there is a usuario
      	If strusuario = "" Then blnusuarioOK = False

        'Make sure the user has not entered disallowed usuarios
        If InStr(1, strusuario, "admin", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "clave", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "salt", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "Usuarios", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "code", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "usuario", vbTextCompare) Then blnusuarioOK = False
        If InStr(1, strusuario, "N0act", vbTextCompare) Then blnusuarioOK = False

        'Clean up user input
        strusuario = formatSQLInput(strusuario)


        '******************************************
	'***   Check the usuario is availabe	***
	'******************************************

        'If the usuario is not already written off then check it's not already gone
        If blnusuarioOK Then

         	'Read in the the usuarios from the database to check that the usuario does not already exsist

                'Initalise the strSQL variable with an SQL statement to query the database
                strSQL = "SELECT " & "Usuarios.* FROM " & "Usuarios WHERE " & "Usuarios.usuario = '" & strusuario & "';"

                'Set the cursor type property of the record set to Dynamic so we can navigate through the record set
	        rsCommon.CursorType = 2
	
	        'Set the Lock Type for the records so that the record set is only locked when it is updated
	        rsCommon.LockType = 3
	
	        'Open the Usuarios table
	        rsCommon=db.execute(strSQL)

                'If there is a record returned from the database then the usuario is already used
                If NOT rsCommon.EOF Then 
                	blnusuarioOK = False
                	rsCommon.Close
                End If


                'Remove SQL safe single quote double up set in the format SQL function
                strusuario = Replace(strusuario, "''", "'", 1, -1, 1)
 	End If
		

	'******************************************
	'*** 	     Create a usercode 		***
	'******************************************

        'Calculate a code for the user
        strUserCode = func.userCode(strusuario)

	

	'******************************************
	'*** 		Encrypt clave	***
	'******************************************

        'Encrypt clave
	If strclave <> "" Then
	
		'Genrate a slat value
	       	strSalt = getSalt(Len(strclave))
	
	       'Concatenate salt value to the clave
	       strEncyptedclave = strclave & strSalt
	
	       'Encrypt the clave
	       strEncyptedclave = HashEncode(strEncyptedclave)
	 End If



	'******************************************
	'*** 	  	Update datbase		***
	'******************************************

        'If this is new reg and the usuario and email is OK or this is an update then register the new user or update the rs
        If blnusuarioOK Then

            
                'Insert the user's details into the rs
                With rsCommon
                
                	.AddNew
                             
                        .Fields("usuario") = strusuario
                        .Fields("Group_ID") = intUsersGroupID
                        .Fields("clave") = strEncyptedclave
	                .Fields("Salt") = strSalt
	                .Fields("User_code") = strUserCode
	                .Fields("email") = strEmail
                        .Fields("nombre") = strRealName
		       	.Fields("Location") = strLocation
		       	.Fields("Avatar") = strAvatar
		        .Fields("Homepage") = strHomepage
		        .Fields("ICQ") = strICQNum
		        .Fields("AIM") = strAIMAddress
		        .Fields("MSN") = strMSNAddress
		        .Fields("Yahoo") = strYahooAddress
		        .Fields("Occupation") = strOccupation
		        .Fields("Interests") = strInterests
		        .Fields("DOB") = dtmDateOfBirth
		        .Fields("Signature") = strSignature
		        .Fields("Attach_signature") = blnAttachSignature
	             	.Fields("Date_format") = portal.variablesForum.strDateFormat
			.Fields("Time_offset") = portal.variablesForum.strTimeOffSet
 			.Fields("Time_offset_hours") = portal.variablesForum.intTimeOffSet
	    		.Fields("Reply_notify") = portal.variablesForum.blnReplyNotify
	          	.Fields("Rich_editor") = blnWYSIWYGEditor
	          	.Fields("PM_notify") = blnPMNotify
	       		.Fields("Show_email") = blnShowEmail 
                        .Fields("Active") = blnUserActive	
                        .Fields("Avatar_title") = strMemberTitle	
			.Fields("No_of_posts") = lngPosts
			.Fields("FechaCreacion") = Now
                	

                        'Update the database with the new user's details (needed for MS Access which can be slow updating)
                        .Update

                        'Re-run the query to read in the updated recordset from the database
                        .Requery
                End With


		'******************************************
		'*** 	 	 Clean up   		***
		'******************************************

                'Reset server Object
                rsCommon.Close
                Set rsCommon = Nothing
		adoCon.Close
		Set adoCon = Nothing


		'******************************************
		'*** 	 Redirect to message page	***
		'******************************************

                'Redirect the welcome new user page
                Response.Redirect("added_member.aspx")
        End If
End If


%>
<html>
<head>

<title>Register New User</title>

<!-- Check the from is filled in correctly before submitting -->
<script language="javascript">

//Function to check form is filled in correctly before submitting
function CheckForm () {

        //Initialise variables
        var errorMsg = "";
        var errorMsgLong = "";

        //Check for a usuario
        if (document.frmRegister.name1.value.length <= 3){
                errorMsg += "\n\tusuario \t- Your usuario must be at least 4 characters";
        }

        //Check for a clave
        if (document.frmRegister.clave1.value.length <= 3){
                errorMsg += "\n\tclave \t- Your clave must be at least 4 characters";
        }

        //Check both claves are the same
        if ((document.frmRegister.clave1.value) != (document.frmRegister.clave2.value)){
                errorMsg += "\n\tclave Error\t- The claves entered do not match";
                document.frmRegister.clave1.value = ""
                document.frmRegister.clave2.value = ""
        }

        //If an e-mail is entered check that the e-mail address is valid
        if (document.frmRegister.email.value.length >0 && (document.frmRegister.email.value.indexOf("@",0) == -1||document.frmRegister.email.value.indexOf(".",0) == -1)) {
                errorMsg +="\n\tEmail\t\t- Enter your valid email address";
          errorMsgLong += "\n- If you don't want to enter your email address then leave the email field blank"; 
        }

        //Check to make sure the user is not trying to show their email if they have not entered one
        if (document.frmRegister.email.value == "" && document.frmRegister.emailShow[0].checked == true){
                errorMsgLong += "\n- You can not show your email address if you haven\'t entered one!";
                document.frmRegister.emailShow[1].checked = true
                document.frmRegister.email.focus();
        }

	
        //Check that the signature is not above 200 chracters
        if (document.frmRegister.signature.value.length > 200){
                errorMsg += "\n\tSignature \t- Your signature has to many characters";
                errorMsgLong += "\n- You have " + document.frmRegister.signature.value.length + " characters in your signature, you must shorten it to below 200";
        }

        //If there is aproblem with the form then display an error
        if ((errorMsg != "") || (errorMsgLong != "")){
                msg = "_______________________________________________________________\n\n";
                msg += "The form has not been submitted because there are problem(s) with the form.\n";
                msg += "Please correct the problem(s) and re-submit the form.\n";
                msg += "_______________________________________________________________\n\n";
                msg += "The following field(s) need to be corrected: -\n";

                errorMsg += alert(msg + errorMsg + "\n" + errorMsgLong);
                return false;
        }

        //Reset the submition action
        document.frmRegister.action = "add_member.aspx"
        document.frmRegister.target = "_self";

        return true;
}

//Function to count the number of characters in the signature text box
function DescriptionCharCount() {
        document.frmRegister.countcharactem_rs.value = document.frmRegister.signature.value.length;
}

//Function to open pop up window
function openWin(theURL,winName,features) {
  	window.open(theURL,winName,features);
}

//Function to open preview post window
function OpenPreviewWindow(targetPage, formName){
	
	now = new Date  
	
	//Open the window first 	
   	openWin('','preview','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=680,height=400')
   		
   	//Now submit form to the new window
   	formName.action = targetPage + "?ID=" + now.getTime();	
	formName.target = "preview";
	formName.submit();
}
</script>

<link href="includes/default_style.css" rel="stylesheet" type="text/css" />
</head>

<body  background="images/main_bg.gif" bgcolor="#FFFFFF" text="#000000">
<div align="center"><span class="heading">Register New User</span><br />
 <span class="text"><a href="admin_menu.aspx" target="_self">Return to the the Administration Menu</a></span><br />
 <br />
 <span class="text">From here you can register a new member on the forum <br /><br />
</span></div>
<table width="98%" border="0" cellspacing="0" cellpadding="1" bgcolor="#000000" align="center">
 <tr><form method="post" name="frmRegister" action="add_member.aspx" onReset="return confirm('Are you sure you want to reset the form?');">
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
    <tr>
     <td bgcolor="#A6AAD3">
      <table width="100%" border="0" cellspacing="1" cellpadding="3" height="14" bgcolor="#CCCCCC">
    <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading">Registration Details</td>
     </tr>
        <tr bgcolor="#F4F4FB" background="">
         <td colspan="2" bgcolor="#F4F4FB" background="" class="text">*Indicates required fields</td>
     </tr>
        <tr bgcolor="#F4F4FB" background="" >
         <td width="50%"  bgcolor="#F4F4FB" background="" class="text">usuario*<br /><span class="smText">This is the name displayed when you use the forum</span></td>
         <td width="50%" bgcolor="#F4F4FB" background="" class="text">
         <input type='text' name="name1" size="15" maxlength="15" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  bgcolor="#F4F4FB" background="" class="text">clave*</td>
         <td width="50%" valign="top" background=""><input type="clave" name="clave1" size="15" maxlength="15" /></td>
     </tr>
     <tr  bgcolor="#F4F4FB" background="">
         <td width="50%"  height="2" class="text" background="">Retype clave*</td>
         <td width="50%" valign="top" height="2" background=""><input type="clave" name="clave2" size="15" maxlength="15" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Email Address         <br /><span class="smText">Not required, but useful if you wish to be notified when someone answers one of your posts or if you lose your clave.</span><br /></td>
         <td width="50%" valign="top" background="">
          <input type='text' name="email" size="30" maxlength="60" value="" />&nbsp;</td>
     </tr>
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading">Profile Information (not required)</td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Real Name</td>
         <td width="50%" background=""><input type='text' name="realName" size="30" maxlength="27" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Location</td>
         <td width="50%" background="">
          <select name=location>
        <option value="" selected>-- Select Country --</option>
          	<option>United Kingdom</option>
                <option>United States</option>
                <option>Afghanistan</option>
                <option>Albania</option>
                <option>Algeria</option>
                <option>American Samoa</option>
                <option>Andorra</option>
                <option>Angola</option>
                <option>Anguilla</option>
                <option>Antarctica</option>
                <option>Antigua And Barbuda</option>
                <option>Argentina</option>
                <option>Armenia</option>
                <option>Aruba</option>
                <option>Australia</option>
                <option>Austria</option>
                <option>Azerbaijan</option>
                <option>Bahamas</option>
                <option>Bahrain</option>
                <option>Bangladesh</option>
                <option>Barbados</option>
                <option>Belarus</option>
                <option>Belgium</option>
                <option>Belize</option>
                <option>Benin</option>
                <option>Bermuda</option>
                <option>Bhutan</option>
                <option>Bolivia</option>
                <option>Bosnia Hercegovina</option>
                <option>Botswana</option>
                <option>Bouvet Island</option>
                <option>Brazil</option>
                <option>Brunei Darussalam</option>
                <option>Bulgaria</option>
                <option>Burkina Faso</option>
                <option>Burundi</option>
                <option>Byelorussian SSR</option>
                <option>Cambodia</option>
                <option>Cameroon</option>
                <option>Canada</option>
                <option>Cape Verde</option>
                <option>Cayman Islands</option>
                <option>Central African Republic</option>
                <option>Chad</option>
                <option>Chile</option>
                <option>China</option>
                <option>Christmas Island</option>
                <option>Cocos (Keeling) Islands</option>
                <option>Colombia</option>
                <option>Comoros</option>
                <option>Congo</option>
                <option>Cook Islands</option>
                <option>Costa Rica</option>
                <option>Cote D'Ivoire</option>
                <option>Croatia</option>
                <option>Cuba</option>
                <option>Cyprus</option>
                <option>Czech Republic</option>
                <option>Czechoslovakia</option>
                <option>Denmark</option>
                <option>Djibouti</option>
                <option>Dominica</option>
                <option>Dominican Republic</option>
                <option>East Timor</option>
                <option>Ecuador</option>
                <option>Egypt</option>
                <option>El Salvador</option>
                <option>England</option>
                <option>Equatorial Guinea</option>
                <option>Eritrea</option>
                <option>Estonia</option>
                <option>Ethiopia</option>
                <option>Falkland Islands</option>
                <option>Faroe Islands</option>
                <option>Fiji</option>
                <option>Finland</option>
                <option>France</option>
                <option>Gabon</option>
                <option>Gambia</option>
                <option>Georgia</option>
                <option>Germany</option>
                <option>Ghana</option>
                <option>Gibraltar</option>
                <option>Great Britain</option>
                <option>Greece</option>
                <option>Greenland</option>
                <option>Grenada</option>
                <option>Guadeloupe</option>
                <option>Guam</option>
                <option>Guatemela</option>
                <option>Guernsey</option>
                <option>Guiana</option>
                <option>Guinea</option>
                <option>Guinea-Bissau</option>
                <option>Guyana</option>
                <option>Haiti</option>
                <option>Heard Islands</option>
                <option>Honduras</option>
                <option>Hong Kong</option>
                <option>Hungary</option>
                <option>Iceland</option>
                <option>India</option>
                <option>Indonesia</option>
                <option>Iran</option>
                <option>Iraq</option>
                <option>Ireland</option>
                <option>Isle Of Man</option>
                <option>Israel</option>
                <option>Italy</option>
                <option>Jamaica</option>
                <option>Japan</option>
                <option>Jersey</option>
                <option>Jordan</option>
                <option>Kazakhstan</option>
                <option>Kenya</option>
                <option>Kiribati</option>
                <option>Korea, South</option>
                <option>Korea, North</option>
                <option>Kuwait</option>
                <option>Kyrgyzstan</option>
                <option>Lao People's Dem. Rep.</option>
                <option>Latvia</option>
                <option>Lebanon</option>
                <option>Lesotho</option>
                <option>Liberia</option>
                <option>Libya</option>
                <option>Liechtenstein</option>
                <option>Lithuania</option>
                <option>Luxembourg</option>
                <option>Macau</option>
                <option>Macedonia</option>
                <option>Madagascar</option>
                <option>Malawi</option>
                <option>Malaysia</option>
                <option>Maldives</option>
                <option>Mali</option>
                <option>Malta</option>
                <option>Mariana Islands</option>
                <option>Marshall Islands</option>
                <option>Martinique</option>
                <option>Mauritania</option>
                <option>Mauritius</option>
                <option>Mayotte</option>
                <option>Mexico</option>
                <option>Micronesia</option>
                <option>Moldova</option>
                <option>Monaco</option>
                <option>Mongolia</option>
                <option>Montserrat</option>
                <option>Morocco</option>
                <option>Mozambique</option>
                <option>Myanmar</option>
                <option>Namibia</option>
                <option>Nauru</option>
                <option>Nepal</option>
                <option>Netherlands</option>
                <option>Netherlands Antilles</option>
                <option>Neutral Zone</option>
                <option>New Caledonia</option>
                <option>New Zealand</option>
                <option>Nicaragua</option>
                <option>Niger</option>
                <option>Nigeria</option>
                <option>Niue</option>
                <option>Norfolk Island</option>
                <option>Northern Ireland</option>
                <option>Norway</option>
                <option>Oman</option>
                <option>Pakistan</option>
                <option>Palau</option>
                <option>Panama</option>
                <option>Papua New Guinea</option>
                <option>Paraguay</option>
                <option>Peru</option>
                <option>Philippines</option>
                <option>Pitcairn</option>
                <option>Poland</option>
                <option>Polynesia</option>
                <option>Portugal</option>
                <option>Puerto Rico</option>
                <option>Qatar</option>
                <option>Reunion</option>
                <option>Romania</option>
                <option>Russian Federation</option>
                <option>Rwanda</option>
                <option>Saint Helena</option>
                <option>Saint Kitts</option>
                <option>Saint Lucia</option>
                <option>Saint Pierre</option>
                <option>Saint Vincent</option>
                <option>Samoa</option>
                <option>San Marino</option>
                <option>Sao Tome and Principe</option>
                <option>Saudi Arabia</option>
                <option>Scotland</option>
                <option>Senegal</option>
                <option>Seychelles</option>
                <option>Sierra Leone</option>
                <option>Singapore</option>
                <option>Slovakia</option>
                <option>Slovenia</option>
                <option>Solomon Islands</option>
                <option>Somalia</option>
                <option>South Africa</option>
                <option>South Georgia</option>
                <option>Spain</option>
                <option>Sri Lanka</option>
                <option>Sudan</option>
                <option>Suriname</option>
                <option>Svalbard</option>
                <option>Swaziland</option>
                <option>Sweden</option>
                <option>Switzerland</option>
                <option>Syrian Arab Republic</option>
                <option>Taiwan</option>
                <option>Tajikista</option>
                <option>Tanzania</option>
                <option>Thailand</option>
                <option>Togo</option>
                <option>Tokelau</option>
                <option>Tonga</option>
                <option>Trinidad and Tobago</option>
                <option>Tunisia</option>
                <option>Turkey</option>
                <option>Turkmenistan</option>
                <option>Turks and Caicos Islands</option>
                <option>Tuvalu</option>
                <option>Uganda</option>
                <option>Ukraine</option>
                <option>United Arab Emirates</option>
                <option>United Kingdom</option>
                <option>United States</option>
                <option>Uruguay</option>
                <option>Uzbekistan</option>
                <option>Vanuatu</option>
                <option>Vatican City State</option>
                <option>Venezuela</option>
                <option>Vietnam</option>
                <option>Virgin Islands</option>
                <option>Wales</option>
                <option>Western Sahara</option>
                <option>Yemen</option>
                <option>Yugoslavia</option>
                <option>Zaire</option>
                <option>Zambia</option>
                <option>Zimbabwe</option>
       </select>
         </td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Homepage</td>
         <td width="50%" background=""><input type='text' name="homepage" size="30" maxlength="48" value="http://" /></td>
     </tr>

     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">ICQ Number</td>
         <td width="50%" background=""><input type='text' name="ICQ" size="15" maxlength="15" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">AIM Address</td>
         <td width="50%" background=""><input type='text' name="AIM" size="30" maxlength="60" value="" /></td>
     </tr>

     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">MSN Messenger</td>
         <td width="50%" background=""><input type='text' name="MSN" size="30" maxlength="60" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Yahoo Messenger</td>
         <td width="50%" background=""><input type='text' name="Yahoo" size="30" maxlength="60" value="" /></td>
     </tr>

     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Occupation</td>
         <td width="50%" background=""><input type='text' name="occupation" size="30" maxlength="40" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Interests</td>
         <td width="50%" background=""><input type='text' name="interests" size="30" maxlength="130" value="" /></td>
     </tr>

     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Date of Birth</td>
         <td width="50%" background="" class="text">Day
       <select name="DOBday">
        <option value="0" selected>----</option><%

		'Create lists day's for birthdays
		For lngLoopCounter = 1 to 31
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>           
       </select>
       Month <select name="DOBmonth">
       <option value="0" selected>---</option><%

		'Create lists of days of the month for birthdays
		For lngLoopCounter = 1 to 12
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>
       </select>
       Year <select name="DOByear">
       <option value="0" selected>-----</option><%

		'Create lists of years for birthdays
		For lngLoopCounter = 1910 to 2004
			Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
			Response.Write(">" & lngLoopCounter & "</option>")
		Next
        
%>            
       </select>
         </td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td valign="top" height="2" class="text" background="">Select Avatar
          <br /><span class="smText">This is the small icon shown next to your posts. Either select one from the list or type the path in to your own Avatar (must be 64 x 64  pixels).</span></td>
         <td valign="top" height="2" background="" >
          <table width="290" border="0" cellspacing="0" cellpadding="1">
        <tr>
         <td width="168">&nbsp;
        </td>
         <td width="122" align="center"><img src="images/blank.gif" width="64" height="64" name="avatar">
          <input type="hidden" name="oldAvatar" value="" /></td>
        </tr>
        <tr>

         <td width="168">
          <input type='text' name="txtAvatar" size="30" maxlength="95" value="http://" onChange="oldAvatar.value=''" />
         </td>
         <td width="122">
          <input type="button" name="preview" value="Preview" onClick="avatar.src = txtAvatar.value" />  
         </td>
        </tr>
       </table>       
         </td>
     </tr>

     <tr bgcolor="#F4F4FB" background="">
         <td valign="top" height="2" class="text" background="">Signature<br />
          <span class="smText">Enter a signature that you would like shown at the bottom of your Forum Posts&nbsp;(max 200 characters)<br />
          <br />
          <br />
          <a href="JavaScript:openWin('../forum_codes.aspx','codes','toolbar=0,location=0,status=0,menubar=0,scrollbars=1,resizable=1,width=550,height=400')" class="smLink">Forum Codes</a> can be used in your signature</span></td>

         <td valign="top" height="2" background="">
          <textarea name="signature" cols="30" rows="3" onKeyDown="DescriptionCharCount();" onKeyUp="DescriptionCharCount();"></textarea>
         <br /><input size="3" value="0" name="countcharacters" maxlength="3" />
          <input onClick="DescriptionCharCount();" type="button" value="Character Count" name="Count" />
          &nbsp;&nbsp;<span class="smText"><a href="javascript:OpenPreviewWindow('../signature_preview.aspx', document.frmRegister)" class="smLink">Signature Preview</a>
         </td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">

         <td width="50%" class="text" background="">Always attach my signature to posts</td>
         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="attachSig" value="true" checked />&nbsp;&nbsp;No<input type="radio" name="attachSig" value="false"  /></td>
     </tr>
     <tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading">Forum Preferences</td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">

         <td width="50%"  class="text" background="">Show my Email Address
          <br /><span class="smText">Hide your email address if you want it kept private from other usem_rs.</span></td>
         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="emailShow" value="True"  />&nbsp;&nbsp;No<input type="radio" name="emailShow" value="False" checked />
         </td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Notify me of replies to posts
          <br /><span class="smText">Sends an email when someone replies to a topic you have posted in. This can be changed whenever you post.</span></td>

         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="replyNotify" value="True"  />&nbsp;&nbsp;No<input type="radio" name="replyNotify" value="False" checked />
         </td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%" class="text" background="">Notify me by email when I receive a Private Message</td>
         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="pmNotify" value="True"  />&nbsp;&nbsp;No<input type="radio" name="pmNotify" value="False" checked /></td>

     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%" class="text" background="">Enable the WYSIWYG post editor <br /><span class="smText">Only browsers that are detected as being Rich Text Enabled will have this feature available when posting.</span></td>
         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="ieEditor" value="True" checked />&nbsp;&nbsp;No<input type="radio" name="ieEditor" value="False"  /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%" class="text" background="">Automatically log me in when I return to the Forum</td>

         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="Login" value="True" checked />&nbsp;&nbsp;No<input type="radio" name="Login" value="False"  /></td>
     </tr>
      <tr bgcolor="#F4F4FB" background="">
         <td width="50%" class="text" background="">Time offset from forum time
         <br /><span class="smText">Present server date and time is: <% 
         
	'Get the current server time
	dtmServerTime = Now()
	
	'Make sure that the time and date format function isn't effected by the server time off set
	If portal.variablesForum.strTimeOffSet = "-" Then
		dtmServerTime = DateAdd("h", + portal.variablesForum.intTimeOffSet, dtmServerTime)
	ElseIf portal.variablesForum.strTimeOffSet = "+" Then
		dtmServerTime = DateAdd("h", - portal.variablesForum.intTimeOffSet, dtmServerTime)
	End If         

	'Display the current server time      
	Response.Write(FormatDateTime(dtmServerTime, vbLongDate) & " At " & FormatDateTime(dtmServerTime, VbShortTime)) 

%></span></td>
         <td width="50%" valign="top" class="text" background=""><select name="serverOffSet">
        <option value="+" selected>+</option>

        <option value="-" >-</option>
       </select>
       <select name="serverOffSetHours"><%

	'Create list of time off-set
	For lngLoopCounter = 0 to 24
		Response.Write(VbCrLf & "        <option value=""" & lngLoopCounter & """")
		Response.Write(">" & lngLoopCounter & "</option>")
	Next
        
%>              
       </select> hours</td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%" class="text" background="">Date Format</td>

         <td width="50%" valign="top" class="text" background=""><select name="funcFecha.DateFormat">
        <option value="dd/mm/yy" selected>Day/Month/Year</option>
        <option value="mm/dd/yy" >Month/Day/Year</option>
        <option value="yy/mm/dd" >Year/Month/Day</option>
        <option value="yy/dd/mm" >Year/Day/Month</option>
       </select></td>
     </tr>
<tr bgcolor="#CCCEE6">
      <td colspan="2" class="tHeading">Admin and Moderator Functions</td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">User is active</td>

         <td width="50%" valign="top" class="text" background="">Yes<input type="radio" name="active" value="True" checked>&nbsp;&nbsp;No<input type="radio" name="active" value="False"  />
         </td>
     </tr><%
     			'Get the forum groups from the database so admin can change the members group
	
	                'Initlise SQL query
	                strSQL = "SELECT " & portal.variablesForum.strDbTable & "Group.* FROM " & portal.variablesForum.strDbTable & "Group;"
	
	                'Query the database
	                rsCommon=db.execute(strSQL)
	
	                'If there are groups then disply them
	                If NOT rsCommon.Eof Then
		

     %>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Group</td>
         <td width="50%" valign="top" class="smText" background="">
          <select name="group"><%

	                        'Loop round to display all the groups
	                        Do While NOT rsCommon.EOF
	
	                                Dim intSelGroupID
	                                Dim strSelGroupName
	                                Dim blnSelSpecialGroup
	                                Dim lngSelMinimumRankPosts
	                                Dim blnStartGroup
	
	                                'Read in the recordset
	                                intSelGroupID = CInt(rsCommon("Group_ID"))
	                                strSelGroupName = rsCommon("Name")
	                                blnSelSpecialGroup = CBool(rsCommon("Special_rank"))
	                                lngSelMinimumRankPosts = CLng(rsCommon("Minimum_posts"))
	                                blnStartGroup = CBool(rsCommon("Starting_group"))
	
	                                'Display the selection
	                                Response.Write("<option value=""" & intSelGroupID & """")
	                                
	                                'If this is starting group then select it
	                                If blnStartGroup Then Response.Write(" selected")
	
	                                'Display the end of the select option
	                                If blnSelSpecialGroup Then
	                                        Response.Write(">" & strSelGroupName & " - Non Rank Group</option>" & vbCrLf)
	                                Else
	                                        Response.Write(">" & strSelGroupName & " - Ladder Group - Min. Posts " & lngSelMinimumRankPosts & "</option>" & vbCrLf)
	                                End If
	
	                                'Move to the next record
	                                rsCommon.MoveNext
	
	                        Loop
%>	</select></td>
     </tr><%
		End If
		
%>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Member Title</td>
         <td width="50%" valign="top" class="smText" background=""><input type='text' name="memTitle" size="30" maxlength="40" value="" /></td>
     </tr>
     <tr bgcolor="#F4F4FB" background="">
         <td width="50%"  class="text" background="">Number of posts</td>
         <td width="50%" valign="top" class="smText" background=""><input type='text' name="posts" size="4" maxlength="7" value="0" /></td>
     </tr>
     <tr bgcolor="#E6E7F2" background="">
      <td valign="top" height="2" colspan="2" align="center" background="">
        <input type="hidden" name="postBack" value="True" />
        <input type='submit' name="Submit" value="Add New Member" onClick="return CheckForm();" />
        <input type="reset" name="Reset" value="Reset Form" />
      </td>
     </tr>
    </table>
      </td>
    </tr>
  </table>
  </td>
 </form></tr>
</table>
<div align="center"><br />
 <span class="text">Please note: This form can only be used if encrypted claves have not been disabled. </span><br />
 <%

'Clean up
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'If the usuario is already gone diaply an error message pop-up
If blnusuarioOK = False Then
        Response.Write("<script  language=""JavaScript"">")
        Response.Write("alert('Sorry, the usuario you requested is already taken.\n\nPlease choose another usuario.');")
        Response.Write("</script>")

End If
%>
</div>
</body>
</html>
