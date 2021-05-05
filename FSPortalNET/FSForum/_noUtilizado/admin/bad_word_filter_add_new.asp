

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
'Set the response buffer to true as we maybe redirecting
Response.Buffer = True

'Dimension variables
Dim intBadWord		'Loop counter for the bad words
Dim saryNewWord(3)	'Holds the word to enter into db
Dim saryReplaceWord(3)	'Holds the word the swear word is to be replaced with

'Loop round three times to get each new bad word
For intBadWord = 1 to 3

	'Read in the words
	saryNewWord(intBadWord) = Request.Form("badWord" & intBadWord)
	saryReplaceWord(intBadWord) = Request.Form("replaceWord" & intBadWord)
	
	'Escape SQL crashing quotes
	saryNewWord(intBadWord) = Replace(saryNewWord(intBadWord), "'", "''", 1, -1, 1)
	saryReplaceWord(intBadWord) = Replace(saryReplaceWord(intBadWord), "'", "''", 1, -1, 1)

	'Check there is a new bad word and a replacement word to add to the database
	If saryNewWord(intBadWord) <> "" AND saryReplaceWord(intBadWord) <> "" Then

		'Initalise the strSQL variable with an SQL statement
		strSQL = "INSERT INTO " & portal.variablesForum.strDbTable & "Smut (Smut, Word_replace) VALUES ('" & saryNewWord(intBadWord) & "', '" & saryReplaceWord(intBadWord) & "');"
			
		'Write the updated date of last post to the database
		db.execute(strSQL)		
	End If
Next
	 
'Reset server variable
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'Return to the bad word admin page
response.redirect("bad_word_filter_configure.aspx"
%>
