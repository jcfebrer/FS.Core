<%@ Page Language="VB" AutoEventWireup="false" Inherits="FSPaginas.Servicios.Default" %>
<%@ Import Namespace="FSPortal" %>

<br />
<p class='cabepeque'>Servicios</p>
<%

%>
<img src="../imagenes/bullet.gif" align="middle" /> <a href="<%=Variables.paginaPerfil%>"><%=FuncionesWeb.Idioma(307)%></a><br /><br />
<!--<%  If Variables.paginaRegistro <> "" Then%>-->
<img src="../imagenes/bullet.gif" align="middle" /> <a href="<%=Variables.paginaRegistro%>"><%=FuncionesWeb.Idioma(308)%></a><br /><br />
<!--<%      End If%>-->
<img src="../imagenes/bullet.gif" align="middle" /> <a href="<%=Variables.paginaRecordar%>"><%=FuncionesWeb.Idioma(309)%></a><br /><br />
<img src="../imagenes/bullet.gif" align="middle" /> <a href="<%=Variables.paginaLogin%>"><%=FuncionesWeb.Idioma(310)%></a><br /><br />
<img src="../imagenes/bullet.gif" align="middle" /> <a href="publicidad.aspx"><%=FuncionesWeb.Idioma(311)%></a><br /><br />
<img src="../imagenes/bullet.gif" align="middle" /> <a href="../correo/default.aspx"><%=FuncionesWeb.Idioma(115)%></a><br /><br />
<br /><hr />
<img src="../imagenes/bullet.gif" align="middle" /> <a href="desconectar.aspx">Cerrar sesión</a><br /><br />

</p>