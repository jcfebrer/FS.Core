<%@ Page Language="VB" AutoEventWireup="false" Inherits="FSPaginas.FileManager.Editor" %>
<%@ Import Namespace="FSPortal.Funciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Editor de ficheros - <%=Functions.ValorRequest(Request("file"))%></title>
    
        <script>

            function guarda() {
                var n = prompt('Introduzca el nombre del fichero: ', "Nombre del fichero ...");
                frmEditor.nombreFic.value = n;
                frmEditor.submit();
            }

        </script>
    </head>
    <body>
        <form id="frmEditor" method="post" action="editor.aspx?guardar=si">
            <input type="hidden" name="file" value="<%=Functions.ValorRequest(Request("file"))%>" />
            <input type="hidden" name="path" value="<%=Functions.ValorRequest(Request("path"))%>" />
            <input type="hidden" name="nombreFic" value="" />
            <textarea rows="28" cols="72" name="txtContenido"><% =Edita(Functions.ValorRequest(Request("file")))%></textarea>
            <input type="submit" value="Guardar" /><input type="button" onclick=" javascript:guarda(); " value="Guardar como" />
        </form>
        <%
            Dim f As String = Functions.ValorRequest(Request("nombreFic"))
            If f = "" Then
                f = Functions.ValorRequest(Request("file"))
            Else
                f = Functions.ValorRequest(Request("path")) & "/" & f
                f = Server.MapPath(f)
            End If
            If Functions.ValorRequest(Request("guardar")) = "si" Then
                If Guarda(f, Request("txtContenido")) Then
                    Response.Write("<script>alert('Guardado correctamente.');</script>")
                Else
                    Response.Write("<script>alert('Problemas al guardar el fichero.'" & f & ");</script>")
                End If
            End If
%>
    </body>
</html>