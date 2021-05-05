

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="default.aspx.vb" Inherits="_Default" %><%@ Register TagPrefix="common" TagName="common" Src="common.ascx" %><common:common ID="common" runat="server" />
<%
Response.Buffer = True

'Clear the forum cookie on the users system so the user is no longer logged in
Response.Cookies(portal.variables.strCookieName) = ""
Response.Cookies(portal.variables.strCookieName)("UID") = strLoggedInusuario & "LOGGED-OFF"
Response.Cookies("TS") = ""
Response.Cookies("LPM") = ""
Session("ViRead") = ""

'Reset Server Objects
Set rsCommon = Nothing
adoCon.Close
Set adoCon = Nothing

'Return to the forum
response.redirect("default.aspx"
%>