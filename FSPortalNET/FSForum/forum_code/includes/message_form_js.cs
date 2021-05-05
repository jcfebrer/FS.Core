// <fileheader>
// <copyright file="message_form_js.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\message_form_js.ascx.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FSForum.Includes
{
    public class message_form_js
    {
        public static string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<script type=""text/javascript"" language=""javascript"">");
            sb.AppendLine("//Have the propmt box turned on by default");
            sb.AppendLine("var promptOn = true;");
            sb.AppendLine("//Function to turn on or off the prompt box");
            sb.AppendLine("function PromptMode(selectMode){");
            sb.AppendLine("if (selectMode.options[selectMode.selectedIndex].value == 0){");
            sb.AppendLine("promptOn = false;");
            sb.AppendLine("}");
            sb.AppendLine("else{");
            sb.AppendLine("promptOn = true;");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("// Function to add the code for bold italic centre and underline, to the message");
            sb.AppendLine("function AddMessageCode(code, promptText, InsertText) {");
            sb.AppendLine(@"if (code != """") {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"insertCode = prompt(promptText + ""\n["" + code + ""]xxx[/"" + code + ""]"", InsertText);");
            sb.AppendLine(@"if ((insertCode != null) && (insertCode != """")){");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""]"" + insertCode + ""[/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else{");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""][/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("document.frmAddMessage.message.focus();");
            sb.AppendLine("}");
            sb.AppendLine("// Function to add the font colours, sizes, type to the message");
            sb.AppendLine("function FontCode(code, endCode) {");
            sb.AppendLine(@"if (code != """") {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"insertCode = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterTextYouWouldLikeIn);
            sb.AppendLine(@""" + code + ""\n["" + code + ""]xxx[/"" + endCode + ""]"", '');");
            sb.AppendLine(@"if ((insertCode != null) && (insertCode != """")){");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""]"" + insertCode + ""[/"" + endCode + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else{");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""][/"" + endCode + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("document.frmAddMessage.message.focus();");
            sb.AppendLine("}");
            sb.AppendLine("//Function to add the URL, indent, list, and Email code to the message");
            sb.AppendLine("function AddCode(code) {");
            sb.AppendLine("//For the URL code");
            sb.AppendLine(@"if ((code != """") && (code == ""URL"")) {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"insertText = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterHyperlinkText);
            sb.AppendLine(@""", """");");
            sb.AppendLine(@"if ((insertText != null) && (insertText != """") && (code == ""URL"")){");
            sb.AppendLine(@"insertCode = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterHeperlinkURL);
            sb.AppendLine(@""", ""http://"");");
            sb.AppendLine(@"if ((insertCode != null) && (insertCode != """") && (insertCode != ""http://"")){");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""="" + insertCode + ""]"" + insertText + ""[/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""= ][/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("//For the email code");
            sb.AppendLine(@"if ((code != """") && (code == ""EMAIL"")) {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"insertText = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterEmailText);
            sb.AppendLine(@""", """");");
            sb.AppendLine(@"if ((insertText != null) && (insertText != """")){");
            sb.AppendLine(@"insertCode = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterEmailMailto);
            sb.AppendLine(@""", """");");
            sb.AppendLine(@"if ((insertCode != null) && (insertCode != """")){");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""="" + insertCode + ""]"" + insertText + ""[/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""= ][/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("//For the image code");
            sb.AppendLine(@"if ((code != """") && (code == ""IMG"")) {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"insertCode = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterImageURL);
            sb.AppendLine(@""", ""http://"");");
            sb.AppendLine(@"if ((insertCode != null) && (insertCode != """")){");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""]"" + insertCode + ""[/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""][/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("//For the list code");
            sb.AppendLine(@"if ((code != """") && (code == ""LIST"")) {");
            sb.AppendLine("if (promptOn == true){");
            sb.AppendLine(@"listType = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterTypeOfList);
            sb.AppendLine("\n");
            sb.AppendLine(Variables.Forum.strTxtEnterEnter);
            sb.AppendLine("\'1\'");
            sb.AppendLine(Variables.Forum.strTxtEnterNumOrBlankList);
            sb.AppendLine(@""", """");");
            sb.AppendLine(@"while ((listType != null) && (listType != """") && (listType != ""1"")) {");
            sb.AppendLine(@"listType = prompt(""");
            sb.AppendLine(Variables.Forum.strTxtEnterListError);
            sb.AppendLine("\'1\'");
            sb.AppendLine(Variables.Forum.strTxtEnterNumOrBlankList);
            sb.AppendLine(@""","""");");
            sb.AppendLine("}");
            sb.AppendLine("if (listType != null) {");
            sb.AppendLine(@"var listItem = ""1"";");
            sb.AppendLine(@"var insertCode = """";");
            sb.AppendLine(@"while ((listItem != """") && (listItem != null)) {");
            sb.AppendLine(@"listItem = prompt(""");
            sb.AppendLine(Variables.Forum.strEnterLeaveBlankForEndList);
            sb.AppendLine(@""","""");");
            sb.AppendLine(@"if (listItem != """") {");
            sb.AppendLine(@"insertCode += ""[LI]"" + listItem + ""[/LI]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine(@"if (listType == """") {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""]"" + insertCode + ""[/"" + code + ""]"";");
            sb.AppendLine("} else {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""="" + listType + ""]"" + insertCode + ""[/"" + code + ""="" + listType + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("else{");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""["" + code + ""][LI] [/LI][LI] [/LI][LI] [/LI][/"" + code + ""]"";");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("//For the indent");
            sb.AppendLine(@"if ((code != """") && (code == ""INDENT"")) {");
            sb.AppendLine(@"document.frmAddMessage.message.value += ""      "";");
            sb.AppendLine("}");
            sb.AppendLine("document.frmAddMessage.message.focus();");
            sb.AppendLine("}");
            sb.AppendLine("//Function to add the code to the message for the smileys");
            sb.AppendLine("function AddSmileyIcon(iconCode) {");
            sb.AppendLine("var txtarea = document.frmAddMessage.message;");
            sb.AppendLine("iconCode = ' ' + iconCode + ' ';");
            sb.AppendLine("if (txtarea.createTextRange && txtarea.caretPos) {");
            sb.AppendLine("var caretPos = txtarea.caretPos;");
            sb.AppendLine("caretPos.text = caretPos.text.charAt(caretPos.text.length - 1) == ' ' ? iconCode + ' ' : iconCode;");
            sb.AppendLine("txtarea.focus();");
            sb.AppendLine("} else {");
            sb.AppendLine("txtarea.value  += iconCode;");
            sb.AppendLine("txtarea.focus();");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("//Insert at Claret position.");
            sb.AppendLine("function storeCaret(cursorPosition) {");
            sb.AppendLine("if (cursorPosition.createTextRange) cursorPosition.caretPos = document.selection.createRange().duplicate();");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            return sb.ToString();
        }

    }

}