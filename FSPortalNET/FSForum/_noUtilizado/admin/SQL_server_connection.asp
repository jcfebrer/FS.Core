<%
Dim strSQLServerName		'Holds the name of the SQL Server
Dim strSQLDBusuario		'Holds the user name (for SQL Server Authentication)
Dim strSQLDBclave		'Holds the clave (for SQL Server Authentication)
Dim strSQLDBName		'Holds name of a database on the server


'------------- The Driver Below is if you are using SQL Server (Do Not Use Unless you know and have an SQL Server) ---------------------------

'Enter the details of your SQL server below
strSQLServerName = "" 'Holds the name of the SQL Server
strSQLDBusuario = "" 'Holds the user name (for SQL Server Authentication)
strSQLDBclave = "" 'Holds the clave (for SQL Server Authentication)
strSQLDBName = ""     'Holds name of a database on the server


'Please note the forum has been optimised for the SQL OLE DB Driver using another driver 
'or system DSN to session("conn")ect to the SQL Server database will course errors in the forum and
'drastically reduce the performance of the forum!


'The SQLOLEDB driver offers the highest performance at this time for session("conn")ecting to SQL Server databases from within ASP.


'MS SQL Server OLE Driver (If you change this string make sure you also change it in the msSQL_server_setup.aspx file when creating the database)
strCon = "Provider=SQLOLEDB;Server=" & strSQLServerName & ";User ID=" & strSQLDBusuario & ";clave=" & strSQLDBclave & ";Database=" & strSQLDBName & ";"

'---------------------------------------------------------------------------------------------------------------------------------------------

%>