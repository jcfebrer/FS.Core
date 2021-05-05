<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<%
'USER CONFIGURABLE OPTIONS
Dim latest_post_num, newsforumid
latest_post_num = 10
newsforumid = 1

'NOTE: To exclude forums from the list, please go to line 227 and follow the directions.

'Skinning and Customization:
'Just set the html color codes to your desired color...or leave the defaults.

Dim sitename, tabcolor, buttonfont, buttoncolor, textcolor, welcome

Const portal.variablesForum.strDbTable = "tbl"

sitename = "Web Wiz Guide" 'Site title
tabcolor = "#5681B8" 'Tab Page Background Color
buttonfont = "Microsoft San Serif" 'Choose from Microsoft San Serif, Times New Roman, Verdana or any other common font "Microsoft San Serif" is the default.
buttoncolor = "#F5FFFA" 'Button Colors
textcolor = "#F5FFFA" 'Text color (where applicable)
welcome = "Web Wiz Forums" 'Welcome message.

'End skinning.

Private Function decodeString(ByVal strInputEntry)

 strInputEntry = Replace(strInputEntry, "&#097;", "a", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#098;", "b", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#099;", "c", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#100;", "d", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#101;", "e", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#102;", "f", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#103;", "g", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#104;", "h", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#105;", "i", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#106;", "j", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#107;", "k", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#108;", "l", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#109;", "m", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#110;", "n", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#111;", "o", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#112;", "p", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#113;", "q", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#114;", "r", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#115;", "s", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#116;", "t", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#117;", "u", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#118;", "v", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#119;", "w", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#120;", "x", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#121;", "y", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#122;", "z", 1, -1, 0)

 strInputEntry = Replace(strInputEntry, "&#065;", "A", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#066;", "B", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#067;", "C", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#068;", "D", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#069;", "E", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#070;", "F", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#071;", "G", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#072;", "H", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#073;", "I", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#074;", "J", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#075;", "K", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#076;", "L", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#077;", "M", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#078;", "N", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#079;", "O", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#080;", "P", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#081;", "Q", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#082;", "R", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#083;", "S", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#084;", "T", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#085;", "U", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#086;", "V", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#087;", "W", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#088;", "X", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#089;", "Y", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#090;", "Z", 1, -1, 0)
 
 strInputEntry = Replace(strInputEntry, "&#061;", "=", 1, -1, 0)


 strInputEntry = Replace(strInputEntry, "&#048;", "0", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#049;", "1", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#050;", "2", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#051;", "3", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#052;", "4", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#053;", "5", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#054;", "6", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#055;", "7", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#056;", "8", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#057;", "9", 1, -1, 0)
 strInputEntry = Replace(strInputEntry, "&#146;", "'", 1, -1, 0)
 strInputEntry= Replace(strInputEntry, ">","&gt;")
strInputEntry = Replace(strInputEntry, "<","&lt;")
 strInputEntry = Replace(strInputEntry, "&","&amp;")
 
 'Return
 decodeString = strInputEntry
End Function


'Write initial lines

Dim string

string = "<?xml version=""1.0"" encoding=""windows-1252""?>" & vbNewline
string = string + "<Extenshun>"
Response.Write(string)

string = _
vbNewline & "<skin>" & vbNewline & _ 
"	<sitename>" & sitename & "</sitename>" & vbNewline & _
"	<tabcolor>" & tabcolor & "</tabcolor>" & vbNewline & _
"	<buttonfont>" & buttonfont & "</buttonfont>" & vbNewline & _
"	<buttoncolor>" & buttoncolor & "</buttoncolor>" & vbNewline & _	
"	<textcolor>" & textcolor & "</textcolor>" & vbNewline & _	
"	<welcome>" & welcome & "</welcome>" & vbNewline & _	
"</skin>"
Response.Write(string)
%>
<!--#include file="admin/SQL_server_session("conn")ection.aspx" -->
<%

'Gather forum home from DB
Dim adoCon, rsCommon, rsCommon2, strSQL, base_url




'Create a session("conn")ection odject
Set adoCon = Server.CreateObject("ADODB.Connection")

'Set the session("conn")ection string to the database
adoCon.connectionstring = strCon

'Set an active session("conn")ection to the session("conn")ection object
adoCon.Open


    

'Intialise the ADO recordset object
Set rsCommon = Server.CreateObject("ADODB.Recordset")



'Query the database
strSQL = "SELECT forum_path AS url FROM tblConfiguration;" 
rsCommon=db.execute(strSQL)


base_url = rsCommon("url") & "/"

'Close it off
rsCommon.close



'Stats
'Initalise the strSQL variable with an SQL statement to query the database
strSQL = "SELECT tblForum.No_of_topics, tblForum.No_of_posts FROM tblForum;"

'Query the database
rsCommon=db.execute(strSQL)

'Get the number of topics posts and forums
Do While NOT rsCommon.EOF


 	'Count the number of topics
 	totalthreads = totalthreads + CLng(rsCommon("No_of_topics"))

 	'Count the number of posts
 	totalreplies = totalreplies + CLng(rsCommon("No_of_posts"))

 	'Move to the next record
 	rsCommon.MoveNext
Loop

totalposts = totalreplies + totalthreads

'Clean up
rsCommon.Close



'Get the total member count
strSQL = "SELECT COUNT(UsuarioID) AS members FROM tblUsuarios;"

'Query the database
rsCommon=db.execute(strSQL)

members_num = CLng(rsCommon("members"))

'Clean up
rsCommon.Close


string = _
vbNewline & "<stats>" & vbNewline & _ 
"	<total_posts>" & totalposts & "</total_posts>" & vbNewline & _
"	<total_topics>" & totalthreads & "</total_topics>" & vbNewline & _
"	<total_replies>" & totalreplies & "</total_replies>" & vbNewline & _
"	<total_members>" & members_num & "</total_members>" & vbNewline & _	
"</stats>"
Response.Write(string)

'News
Dim i, n, profile_link, member_name, post_date, topic_title, post, comments, view_all_link, topic_id, UsuarioID



'Select the top 10 from news forum
strSQL = "SELECT Top 10 tblTopic.Subject, tblTopic.Start_date, tblTopic.Topic_ID FROM tblTopic WHERE tblTopic.Forum_ID = " & newsforumid & " ORDER BY tblTopic.Start_date DESC;"

rsCommon=db.execute(strSQL)


'Need a secound rs for loop (create outside loop so it only needs to be created once for better performance)
'Intialise the ADO recordset object
Set rsCommon2 = Server.CreateObject("ADODB.Recordset")


'Loop through recordset till end of records
Do While NOT rsCommon.EOF
	
	topic_title = rsCommon("Subject")
	post_date = rsCommon("Start_date")
	topic_id = rsCommon("Topic_ID")
	
	
	strSQL = "SELECT tblThread.Message, tblThread.UsuarioID, tblUsuarios.usuario " & _
	"FROM tblThread, tblUsuarios " & _
	"WHERE tblThread.UsuarioID=tblUsuarios.UsuarioID AND tblThread.Topic_ID = " & topic_id & ";"
	
	'Set the cursor type to 1 to count the number of returned records
	rsCommon2.CursorType = 1
	
	rsCommon2=db.execute(strSQL)
	
	post = Trim(Mid(rsCommon2("Message"),1,1000)) & "......"
	UsuarioID = rsCommon2("UsuarioID")
	comments = CLng(rsCommon2.RecordCount)
	member_name = rsCommon2("usuario")
	
	'Close rs
	rsCommon2.Close
	
	
	profile_link = base_url & "pop_up_profile.aspx?PF=" & UsuarioID
	view_all_link = base_url & "forum_posts.aspx?TID=" & topic_id & "&amp;pN=1000"
	
	
	
	'Character replacement
	profile_link= decodeString(profile_link)
	member_name= decodeString(member_name)
	topic_title= decodeString(topic_title)
	post = decodeString(post)
	view_all_link= decodeString(view_all_link)
	
	
	
	
	
	string = _
	vbNewline & "<news>" & vbNewline & _ 
	"	<profile_link>" & profile_link & "</profile_link>" & vbNewline & _
	"	<member_name>" & member_name & "</member_name>" & vbNewline & _
	"	<post_date>" & post_date & "</post_date>" & vbNewline & _
	"	<topic_title>" & topic_title & "</topic_title>" & vbNewline & _	
	"	<post>" & post & "</post>" & vbNewline & _	
	"	<comments_num>" & comments & "</comments_num>" & vbNewline & _	
	"	<view_all_link>" & view_all_link & "</view_all_link>" & vbNewline & _	
	"</news>"
	Response.Write(string)
	
	rsCommon.MoveNext
Loop

'Clean up
rsCommon.Close


'Posts
Dim last_poster_name, last_post_date


	'If you wish to have ALL forums checked by Extenshun, leave the default:

	'"Topic_ID AS topic_id FROM tblTopic WHERE Forum_ID <> 0 ORDER BY Last_entry_date DESC"

	'If you have forums that you do not wish to have checked, look up the forum ID from the database and do something like this;

	'"Topic_ID AS topic_id FROM tblTopic WHERE Forum_ID <> 13 AND Forum_ID <> 7 AND Forum_ID <> 8 ORDER BY Last_entry_date DESC"

	'Where 13, 7, and 8 are forum IDs you do NOT want to include.

	'..and so on.
	
	strSQL = "SELECT TOP " & latest_post_num & " tblTopic.Subject, tblTopic.Last_entry_date, tblTopic.Start_date, tblTopic.Topic_ID " & _
	"FROM tblTopic WHERE tblTopic.Forum_ID <> 0 ORDER BY Last_entry_date DESC;"	
	
	rsCommon=db.execute(strSQL)

'Loop through recordset
Do while NOT rsCommon.EOF
	
	topic_title = rsCommon("Subject")
	post_date = rsCommon("Start_date")
	topic_id = rsCommon("Topic_ID")
	last_post_date = rsCommon("Last_entry_date")
	
	
	strSQL = "SELECT tblThread.UsuarioID, tblUsuarios.usuario FROM tblThread, tblUsuarios " & _
	"WHERE tblThread.UsuarioID=tblUsuarios.UsuarioID AND Topic_ID = " & topic_id & " ORDER BY tblThread.Message_date DESC;"
	
	rsCommon2=db.execute(strSQL)
	
	UsuarioID = rsCommon2("UsuarioID")
	last_poster_name = rsCommon2("usuario")
	
	rsCommon2.Close
	
	view_all_link = base_url & "forum_posts.aspx?TID=" & topic_id & "&amp;pN=1000"
	
	
	
	'Character replacement
	last_poster_name= decodeString(last_poster_name)
	topic_title = decodeString(topic_title)
	view_all_link = decodeString(view_all_link)
	
	string = _
	vbNewline & "<latest>" & vbNewline & _ 
	"	<last_poster_name>" & last_poster_name & "</last_poster_name>" & vbNewline & _
	"	<last_post_date>" & last_post_date & "</last_post_date>" & vbNewline & _
	"	<topic_title>" & topic_title & "</topic_title>" & vbNewline & _	
	"	<view_all_link>" & view_all_link & "</view_all_link>" & vbNewline & _	
	"</latest>"
	Response.Write(string)
	
	
	rsCommon.MoveNext
Loop

'Clean up
rsCommon.close

'Secound rs no longer needed so release server object
Set rsCommon2 = Nothing


'Active users

strSQL = "SELECT tblActiveUser.UsuarioID, tblUsuarios.usuario FROM tblActiveUser, tblUsuarios " & _
"WHERE tblActiveUser.UsuarioID=tblUsuarios.UsuarioID ORDER BY tblUsuarios.usuario ASC;"

rsCommon=db.execute(strSQL)

Do While NOT rsCommon.EOF
	
	UsuarioID = rsCommon("UsuarioID")
	member_name = rsCommon("usuario")
	profile_link = base_url & "pop_up_profile.aspx?PF=" & UsuarioID
	
	
	'Character replacement
	member_name= decodeString(member_name)
	profile_link= decodeString(profile_link)
	string = _
	vbNewline & "<active_users>" & vbNewline & _ 
	"	<user>" & member_name & "</user>" & vbNewline & _
	"	<profile_link>" & profile_link & "</profile_link>" & vbNewline & _	
	"</active_users>"
	Response.Write(string)
	
	rsCommon.MoveNext

Loop

'Clean up
rsCommon.Close
Set rsCommon = nothing
adoCon.close
set adoCon=nothing

string = vbNewline & "</Extenshun>"
Response.Write(string)

%>