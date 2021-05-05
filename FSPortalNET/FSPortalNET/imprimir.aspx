<%

    Response.Write( _
        "<link rel=""StyleSheet"" href=""" & Variables.directorioWeb & "/estilos/estilofp.css"" type=""text/css"" />")
%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="FSPortal.BD" %>
<%@ Import Namespace="FSPortal.Variables" %>
<%@ Import Namespace="FSPortal.Funciones" %>
<html>
    <body topmargin="0" leftmargin="0" onload=" javascript:document.body.style.cursor = 'hand';window.print(); " onclick=" javascript:window.print(); return false; ">
        <head>

            <style>
                a { cursor: hand }

                .a { cursor: hand }
            </style>
        </head>
        <span style="cursor: hand">
            <%
    Dim dt As DataTable
    Dim db As New Utils

    dt = db.Execute("SELECT * FROM " & Variables.prefijoTablas & "Paginas where idPagina=" & CInt(Request("id")))

    If dt.Rows.Count > 0 Then
        If Functions.ValorBool(dt.Rows(0)("requiereLogin")) Then
            If Functions.User.Usuario = "" Then
                Response.Redirect( _
                    Variables.paginaLogin & "?Functions.User.ComeBackto=" & Request.ServerVariables("SCRIPT_NAME") & "?" & _
                    Server.UrlEncode(Request.QueryString.ToString), false)
                    Context.ApplicationInstance.CompleteRequest()
            End If
        End If

        If Functions.ValorBool(dt.Rows(0)("soloAdmin")) And Functions.User.Administrador = False Then
%>
                    El acceso a esta página, esta restringido a usuarios con derechos de administrador.
                <%
            response.end
        end if
%>
                <br />
                <p class='cabepeque'>
                    <%
        If Variables.mostrarCabeceraModulos Then
            Response.Write(Functions.formatCad(Functions.Valor(dt.Rows(0)("titulo"))))
        End If
%>
                </p>
	
            <%
        Response.Write(Functions.formatCad(Functions.Valor(dt.Rows(0)("contenido"))))
                else
                    Response.Write("Página no encontrada.")
                end if
%>
        </span>
    </body></html>