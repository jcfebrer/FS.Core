<%@ Page Language="VB" AutoEventWireup="true" Inherits="FSPaginas.Servicios.Conectar" %>
<%@ Import Namespace="FSPortal" %>
    
<br />
<form action="conectar.aspx" method="post" runat="server"> 
                
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr>
            <td style="height: 300px; width: 20;"></td>
            <td style="height: 300px; width: 50%;" valign="top">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Label ID="sMessage" runat="server" ForeColor="Red"></asp:Label>
                            <%
    If Variables.paginaRegistro <> "" Then%>
                                <p>Si es la primera vez que accede a <%=Variables.nombreWeb%> y todav&iacute;a no se ha registrado como cliente, <a href="<%=Variables.paginaRegistro%>?comebackto=<%=Request("comebackto")%>"><strong>reg&iacute;strese aqu&iacute; </strong></a>.<br />
                                    <br />
                                </p></td>
                    </tr>
                    <tr>
                        <td>Si ya es cliente, introduzca su informaci&oacute;n de acceso:
                            <br />
                            <% End If%>
                            <br /></td>
                    </tr>
                </table>
                <table border="0">
                    <tr>
                        <td class="cabemaspeque">Nombre de usuario / E-Mail</td>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="cabemaspeque">Clave de acceso</td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="cabemaspeque">Recordar mis datos</td>
                        <td>
                            <asp:CheckBox ID="chkRemember" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="cmdLogin" runat="server" Text="Conectar !" /></td>
                    </tr>
                </table></td>
            <td style="height: 300px; width: 1;"></td>
            <td style="height: 300px; width: 10;"></td>
            <td valign="top" style="height: 300px">
                <p class="accionpeque" style="font-weight: bold">¿Has olvidado la clave?</p>
                <p> Si no recuerda sus datos de cliente, póngase en contacto con nuestro Departamento de Atención al Cliente y <a href="<%=Variables.paginaRecordar%>">solicite su contrase&ntilde;a</a>.</p>
            </td>
        </tr>
    </table>

</form>