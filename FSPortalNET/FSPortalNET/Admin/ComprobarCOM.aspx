<%@ Page Language="VB" AutoEventWireup="false" AspCompat="true" Inherits="FSPaginas.Admin.ComprobarCom" %>
<%@ Import Namespace="FSPaginas.Admin" %>
<%@ Import Namespace="FSPortal.Funciones" %>

<%
    Inicio()
%>
<HTML>
    <HEAD>
        <TITLE>ASP Component Test  - http://www.pensaworks.com</TITLE>
        <SCRIPT type="text/javascript" language="javascript">
            <!--
            function BringUpWindow(webpage) {
                var url = webpage;
                var hWnd = window.open(url, "Mailer_Popup", "width=425,height=325,resizable=yes,scrollbars=yes,status=yes");
                if (window.focus) {
                    hWnd.focus();
                }
                if (hWnd != null) {
                    if (hWnd.opener == null) {
                        hWnd.opener = self;
                        window.name = "home";
                        hWnd.location.href = url;
                    }
                } else {
                }
            }
            // -->
        </SCRIPT>
    </HEAD>
    <body bgcolor="#ffffff" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginwidth="0" marginheight="0">
        <%
    Dim comID As String
    Dim comDetails() As String
    Dim comCreate As String
    Dim comURL As String
    Dim comName As String
    Dim comCat As Integer
    Dim comCat2 As Integer

    if request("comID") <> "" then
        comID = request("comID")
        comDetails = Split(comA(CInt(comID)), "|")
        comCreate = comDetails(0)
        comURL = comDetails(1)
        comName = comDetails(2)
        comCat = Functions.numeroEntero(comDetails(3))
        comCat2 = Functions.numeroEntero(comDetails(4))
%>
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td bgcolor="#000080"><b>
                                              Detalles del componente</b></td>
                </tr>
            </table>
            <%
        Dim b As ProgIDInfo
        Dim a As Program
        b = New ProgIDInfo
        a = CType(b.LoadProgID(comCreate), Program)
        If a.Description.ToString <> "" Then
%>
                <b>Componente:</b> <%            Response.Write(comName)%><br />
                <b>Website:</b> <%            if comURL <> "" then%><a href="<%                response.write(comURL)%>" target="_blank"><%                response.write(comURL)%></a><%            end if%><br />
                <b>Categoria(s):</b> <%            Response.Write(catA(comcat))%><%            If Functions.NumeroEntero(comCat2) <> 0 Then Response.Write("| " & catA(CInt(comCat2)))%><br />
                <b>Descripción:</b> <%            Response.Write(a.Description)%><br />
                <b>Nombre DLL:</b> <%            Response.Write(a.DLLName)%><br />
                <b>ProgID:</b> <%            Response.Write(a.ProgID)%><br />
                <b>ClsID:</b> <%            Response.Write(a.ClsID)%><br />
                <b>Path:</b> <%            Response.Write(a.ProgID)%><br />
                <b>TypeLib:</b> <%            Response.Write(a.TypeLib)%><br />
                <b>Versión:</b> <%            Response.Write(a.Version)%><br />
            <%        else%>
                <b>No se ha encontrado ninguna
                    información para:</b> <%            Response.Write(a.TypeLib)%></font>
            <%        end if%>
            <p align="center"><a href=# onclick=" self.close(); "><b>Cerrar ventana</b></a></p>

        <% else%>
            <p></p>
    
            <p align="center"><b>Espera mientras se comprueban los componentes. Esto puede llevar unos minutos.</b></p>
    
            <%     response.flush()%>
            <table border="0" align="center" cellspacing="2" cellpadding="4">
                <tr>
                    <td colspan="5">
                        <form name="ShowCOMs" method="post" action="<%     response.write(Mid(request.servervariables("SCRIPT_NAME"), _
                        InstrRev(request.servervariables("SCRIPT_NAME"), "/") + 1))%>">
                            <div align="center"><b>Mostrar:</b>
                                <select name="show">
                                    <option value="1"<%     if (show = 1) then response.write(" SELECTED")%>>Show All COMs</option>
                                    <option value="2"<%     if (show = 2) then response.write(" SELECTED")%>>Installed COMs</option>
                                    <option value="3"<%     if (show = 3) then response.write(" SELECTED")%>>Not Installed COMs</option>
                                </select>
                                <b>Categorias:</b>
                                <select name="showCat">
                                    <option value="0"<%     if (showCat = 0) then response.write(" SELECTED")%>>All Categories</option>
                                    <%     Dim i As Integer
     For i = 0 To UBound(catA)%>
                                        <option value="<%         response.write(i)%>"<%         if (showCat = i) then response.write(" SELECTED")%>><%         Response.Write(catA(i))%></option>
                                    <%     next%>
                                </select>
                                <input type="submit" name="Submit" value="Enviar">
                            </div>
                        </form>
                    </td>
                </tr>
                <tr bgcolor="#000080"> 
                    <td><b>#</b></td>
                    <td><b>Categoria</b></td>
                    <td><b>Estado</b></td>
                    <td><b>Detalles</b></td>
                    <td><b>Com</b></td>
                </tr>
                <%
     Dim display As Boolean
     Dim display2 As Boolean
     Dim installed As Boolean

     For i = 0 To UBound(comA)
         comDetails = Split(comA(i), "|")
         display = False
         display2 = False
         comCreate = comDetails(0)
         comURL = comDetails(1)
         comName = comDetails(2)
         comCat = Functions.NumeroEntero(comDetails(3))
         comCat2 = Functions.NumeroEntero(comDetails(4))
         installed = IsObjInstalled(comCreate)
         If show = 2 Then
             If (Not Installed) Then display = False Else display = True
         ElseIf show = 3 Then
             If (Not installed) Then display = True Else display = False
         Else
             display = True
         End If
         If IsNumeric(showCat) Then
             If (comCat = CDbl(showCat) Or comCat2 = CDbl(showCat)) Then display2 = True Else display2 = False
         Else
             display2 = True
         End If
%>
                    <%
         if (display AND display2) then
             onNum = onNum + 1
%>
                        <%             If (onNum Mod 2) = 0 Then%>
                        <tr>
                <%             else%>
                            <tr bgcolor="#CCCCCC">
                                <%             end If%>
                                <td><b><%             Response.Write(onNum)%></b></td>
                                <td><%             Response.Write(catA(comCat))%></td>
                                <td>
                                    <div align="center"><b>
                                                            <%             if NOT installed then%>
                                                                No Instalado
                                                            <%
             else
                 installedCOMs = installedComs + 1
%>
                                                                nstalado
                                                            <%             end if%>
                                                        </b></div>
                                </td>
                                <td>
                                    <div align="center">
                                        <%             if NOT installed then%>
                                            No disponible
                                        <%             else%>
                                            <a href="Javascript:BringUpWindow('<%                 response.write(Mid(request.servervariables("SCRIPT_NAME"), _
                                    InstrRev(request.servervariables("SCRIPT_NAME"), "/") + 1))%>?comID=<%                 response.write(i)%>')">
                                                Detalles</a>
                                        <%             end if%>
                                    </div>
                                </td>
                                <td><%             if comURL <> "" then%><a href="<%                 response.write(comURL)%>" target="_blank"><%                 response.write(comName)%></a><%             else%><%                 response.write(comName)%><%             end if%></td>
                            </tr>
                            <%
         end if
         installed = False
         comCreate = ""
         comURL = ""
         comName = ""
         comCat = 0
         comCat2 = 0
     next
     response.flush()
%>
                <%     if onNum = 0 then%>
                    <tr>
                        <td colspan="5"> 
                            <div align="center"><b>No existe ningún
                                                    componente.</b></div>
                        </td>
                    </tr>
                <%     End If%>
            </table>
            <div align="center">
                <p>&nbsp;</p>
                <p>Tienes un total de <b><%     response.write(installedCOMs)%></b> componentes instalados de <b><%     response.write(onNum)%></b> comprobados.</p>
            </div>
        <% end if%>
    </BODY>
</HTML>