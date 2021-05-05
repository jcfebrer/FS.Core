// <fileheader>
// <copyright file="RTE_message_form_js.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\RTE_message_form_js.ascx.cs
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

namespace FSForum
{
    namespace Includes
    {
        public class RTE_message_form_js
        {

            public string Render()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"<script type=""text/javascript"" language=""javascript"">");
                // Response.AddHeader("pragma", "cache")
                // Response.AddHeader "cache-control","public"
                // Response.CacheControl = "Public"}
                sb.AppendLine("var colour;");
                sb.AppendLine("var htmlOn;");
                // If this is windows IE 5.0 use different JavaScript functions
                if ((FuncionesForum.RTEenabled() == "winIE5"))
                {
                    sb.AppendLine("//Function to format text in the text box");
                    sb.AppendLine("function FormatText(command, option){");
                    sb.AppendLine("//Colour pallete");
                    sb.AppendLine(@"if ((command == ""forecolor"") || (command == ""hilitecolor"")) {");
                    sb.AppendLine("parent.command = command;");
                    sb.AppendLine("buttonElement = document.all(command);");
                    sb.AppendLine("frames.message.focus()");
                    sb.AppendLine(@"document.all.colourPalette.style.left = getOffsetLeft(buttonElement) + ""px"";");
                    sb.AppendLine(@"document.all.colourPalette.style.top = (getOffsetTop(buttonElement) + buttonElement.offsetHeight) + ""px"";");
                    sb.AppendLine(@"if (document.all.colourPalette.style.visibility==""hidden"")");
                    sb.AppendLine(@"document.all.colourPalette.style.visibility=""visible"";");
                    sb.AppendLine("else {");
                    sb.AppendLine(@"document.all.colourPalette.style.visibility=""hidden"";");
                    sb.AppendLine("}");
                    sb.AppendLine("//get current selected range");
                    sb.AppendLine("var sel = frames.message.document.selection;");
                    sb.AppendLine("if (sel != null) {");
                    sb.AppendLine("colour = sel.createRange();");
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    sb.AppendLine("//Text Format");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine("frames.message.document.execCommand(command, false, option);");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to add image");
                    sb.AppendLine("function AddImage(){");
                    sb.AppendLine(@"imagePath = prompt(""");
                    sb.AppendLine(Variables.Forum.strTxtEnterImageURL);
                    sb.AppendLine(@""", ""http://"");");
                    sb.AppendLine(@"if ((imagePath != null) && (imagePath != """")){");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine(@"frames.message.document.execCommand(""InsertImage"", false, imagePath);");
                    sb.AppendLine("}");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to switch to HTML view");
                    sb.AppendLine("function HTMLview() {");
                    sb.AppendLine("//WYSIWYG view");
                    sb.AppendLine("if (htmlOn == true) {");
                    sb.AppendLine("var html = frames.message.document.body.innerText;");
                    sb.AppendLine("frames.message.document.body.innerHTML = html;");
                    sb.AppendLine(@"ToolBar1.style.visibility=""visible"";");
                    sb.AppendLine(@"ToolBar2.style.visibility=""visible"";");
                    sb.AppendLine("htmlOn = false;");
                    sb.AppendLine("//HTML view");
                    sb.AppendLine("} else {");
                    sb.AppendLine("var html = frames.message.document.body.innerHTML;");
                    sb.AppendLine("frames.message.document.body.innerText = html;");
                    sb.AppendLine(@"ToolBar1.style.visibility=""hidden"";");
                    sb.AppendLine(@"ToolBar2.style.visibility=""hidden"";");
                    sb.AppendLine("htmlOn = true;");
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to set colour");
                    sb.AppendLine("function setColor(color) {");
                    sb.AppendLine("//retrieve selected range");
                    sb.AppendLine("var sel = frames.message.document.selection;");
                    sb.AppendLine("if (sel!=null) {");
                    sb.AppendLine("var newColour = sel.createRange();");
                    sb.AppendLine("newColour = colour;");
                    sb.AppendLine("newColour.select();");
                    sb.AppendLine("}");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine("frames.message.document.execCommand(parent.command, false, color);");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine(@"document.all.colourPalette.style.visibility=""hidden"";");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to clear form");
                    sb.AppendLine("function ResetForm(){");
                    sb.AppendLine(@"if (window.confirm(""");
                    sb.AppendLine(Variables.Forum.strResetFormConfirm);
                    sb.AppendLine(@""")){");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine(@"frames.message.document.body.innerHTML = """";");
                    sb.AppendLine("return true;");
                    sb.AppendLine("}");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to add smiley");
                    sb.AppendLine("function AddSmileyIcon(imagePath){");
                    sb.AppendLine("frames.message.focus();");
                    sb.AppendLine(@"frames.message.document.execCommand(""InsertImage"", false, imagePath);");
                    sb.AppendLine("}");
                }


                // ***********************************************
                // *** JavaScript for Win IE5.5+ and Mozilla *****
                // ***********************************************
                // Else use cross browsers RTE JS for all other RTE enabled browsers
                else
                {
                    sb.AppendLine("//Function to format text in the text box");
                    sb.AppendLine("function FormatText(command, option) {");
                    // If this is the Gecko engine then uncomment the following line if you don't wish to use CSS
                    // If RTEenabled = "Gecko" Then Response.Write("    document.getElementById(""message"").contentWindow.document.execCommand(""useCSS"", false, option);")}
                    sb.AppendLine("//Colour pallete");
                    sb.AppendLine(@"if ((command == ""forecolor"") || (command == ""backcolor"")) {");
                    sb.AppendLine("parent.command = command;");
                    sb.AppendLine("buttonElement = document.getElementById(command);");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.left = getOffsetLeft(buttonElement) + ""px"";");
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.top = (getOffsetTop(buttonElement) + buttonElement.offsetHeight) + ""px"";");
                    sb.AppendLine(@"if (document.getElementById(""colourPalette"").style.visibility==""hidden"")");
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.visibility=""visible"";");
                    sb.AppendLine("else {");
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.visibility=""hidden"";");
                    sb.AppendLine("}");
                    sb.AppendLine("//get current selected range");
                    sb.AppendLine(@"var sel = document.getElementById(""message"").contentWindow.document.selection;");
                    sb.AppendLine("if (sel != null) {");
                    sb.AppendLine("colour = sel.createRange();");
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    // If this is the Gecko then url links are cerated differently
                    if ((FuncionesForum.RTEenabled() == "Gecko"))
                    {
                        sb.AppendLine("//URL link for Gecko");
                        sb.AppendLine(@"else if (command == ""createLink"") {");
                        sb.AppendLine(@"insertLink = prompt(""");
                        sb.AppendLine(Variables.Forum.strTxtEnterHeperlinkURL);
                        sb.AppendLine(@""", ""http://"");");
                        sb.AppendLine(@"if ((insertLink != null) && (insertLink != """")) {");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.execCommand(""CreateLink"", false, insertLink);");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                        sb.AppendLine("}");
                        sb.AppendLine("}");
                    }
                    sb.AppendLine("//Text Format");
                    sb.AppendLine("else {");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus();");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.execCommand(command, false, option);");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus();");
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to set colour");
                    sb.AppendLine("function setColor(color) {");
                    // If this is IE then use the following
                    if ((FuncionesForum.RTEenabled() == "winIE"))
                    {

                    }
                    sb.AppendLine("//retrieve selected range");
                    sb.AppendLine(@"var sel = document.getElementById(""message"").contentWindow.document.selection;");
                    sb.AppendLine("if (sel!=null) {");
                    sb.AppendLine("var newColour = sel.createRange();");
                    sb.AppendLine("newColour = colour;");
                    sb.AppendLine("newColour.select();");
                    sb.AppendLine("}");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus();");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.execCommand(parent.command, false, color);");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus();");
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.visibility=""hidden"";");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to add image");
                    sb.AppendLine("function AddImage() {");
                    sb.AppendLine(@"imagePath = prompt(""");
                    sb.AppendLine(Variables.Forum.strTxtEnterImageURL);
                    sb.AppendLine(@""", ""http://"");");
                    sb.AppendLine(@"if ((imagePath != null) && (imagePath != """")) {");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.execCommand(""InsertImage"", false, imagePath);");
                    sb.AppendLine("}");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to switch to HTML view");
                    sb.AppendLine("function HTMLview() {");
                    // If this is IE then use the following
                    if ((FuncionesForum.RTEenabled() == "winIE"))
                    {
                        sb.AppendLine("//WYSIWYG view");
                        sb.AppendLine("if (htmlOn == true) {");
                        sb.AppendLine(@"var html = document.getElementById(""message"").contentWindow.document.body.innerText;");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.innerHTML = html;");
                        sb.AppendLine(@"document.getElementById(""ToolBar1"").style.visibility=""visible"";");
                        sb.AppendLine(@"document.getElementById(""ToolBar2"").style.visibility=""visible"";");
                        sb.AppendLine("htmlOn = false;");
                        sb.AppendLine("//HTML view");
                        sb.AppendLine("} else {");
                        sb.AppendLine(@"var html = document.getElementById(""message"").contentWindow.document.body.innerHTML;");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.innerText = html;");
                        sb.AppendLine(@"document.getElementById(""ToolBar1"").style.visibility=""hidden"";");
                        sb.AppendLine(@"document.getElementById(""ToolBar2"").style.visibility=""hidden"";");
                        sb.AppendLine("htmlOn = true;");
                        sb.AppendLine("}");
                    }
                    else
                    {
                        // Else for Midas (Geckos RTE API)
                        sb.AppendLine("//WYSIWYG view");
                        sb.AppendLine("if (htmlOn == true) {");
                        sb.AppendLine(@"var html = document.getElementById(""message"").contentWindow.document.body.ownerDocument.createRange();");
                        sb.AppendLine(@"html.selectNodeContents(document.getElementById(""message"").contentWindow.document.body);");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.innerHTML = html.toString();");
                        sb.AppendLine(@"document.getElementById(""ToolBar1"").style.visibility=""visible"";");
                        sb.AppendLine(@"document.getElementById(""ToolBar2"").style.visibility=""visible"";");
                        sb.AppendLine("htmlOn = false;");
                        sb.AppendLine("//HTML view");
                        sb.AppendLine("} else {");
                        sb.AppendLine(@"var html = document.createTextNode(document.getElementById(""message"").contentWindow.document.body.innerHTML);");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.innerHTML = """";");
                        sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.appendChild(html);");
                        sb.AppendLine(@"document.getElementById(""ToolBar1"").style.visibility=""hidden"";");
                        sb.AppendLine(@"document.getElementById(""ToolBar2"").style.visibility=""hidden"";");
                        sb.AppendLine("htmlOn = true;");
                        sb.AppendLine("}");
                    }
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to clear form");
                    sb.AppendLine("function ResetForm() {");
                    sb.AppendLine(@"if (window.confirm(""");
                    sb.AppendLine(Variables.Forum.strResetFormConfirm);
                    sb.AppendLine(@""")) {");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus()");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.body.innerHTML = """";");
                    sb.AppendLine("return true;");
                    sb.AppendLine("}");
                    sb.AppendLine("return false;");
                    sb.AppendLine("}");
                    sb.AppendLine("//Function to add smiley");
                    sb.AppendLine("function AddSmileyIcon(imagePath){");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.focus();");
                    sb.AppendLine(@"document.getElementById(""message"").contentWindow.document.execCommand(""InsertImage"", false, imagePath);");
                    sb.AppendLine("}");
                }
                sb.AppendLine("//Colour pallete top offset");
                sb.AppendLine("function getOffsetTop(elm) {");
                sb.AppendLine("var mOffsetTop = elm.offsetTop;");
                sb.AppendLine("var mOffsetParent = elm.offsetParent;");
                sb.AppendLine("while(mOffsetParent){");
                sb.AppendLine("mOffsetTop += mOffsetParent.offsetTop;");
                sb.AppendLine("mOffsetParent = mOffsetParent.offsetParent;");
                sb.AppendLine("}");
                sb.AppendLine("return mOffsetTop;");
                sb.AppendLine("}");
                sb.AppendLine("//Colour pallete left offset");
                sb.AppendLine("function getOffsetLeft(elm) {");
                sb.AppendLine("var mOffsetLeft = elm.offsetLeft;");
                sb.AppendLine("var mOffsetParent = elm.offsetParent;");
                sb.AppendLine("while(mOffsetParent) {");
                sb.AppendLine("mOffsetLeft += mOffsetParent.offsetLeft;");
                sb.AppendLine("mOffsetParent = mOffsetParent.offsetParent;");
                sb.AppendLine("}");
                sb.AppendLine("return mOffsetLeft;");
                sb.AppendLine("}");
                sb.AppendLine("//Function to hide colour pallete");
                sb.AppendLine("function hideColourPallete() {");
                // If this is win IE 5 use document.all
                if ((FuncionesForum.RTEenabled() == "winIE5"))
                {
                    sb.AppendLine(@"document.all.colourPalette.style.visibility=""hidden"";");
                }
                else       // For all other browsers use document.getElementById
                {
                    sb.AppendLine(@"document.getElementById(""colourPalette"").style.visibility=""hidden"";");
                }
                sb.AppendLine("}");
                //***********************************************
                //***        JavaScript for ieSpell        *****
                //***********************************************

                //If this is IE then write the following spel check function
                if (FuncionesForum.RTEenabled() == "winIE" || FuncionesForum.RTEenabled() == "winIE5")
                {
                    sb.AppendLine("//Function to perform spell check");
                    sb.AppendLine("function checkspell() {");
                    sb.AppendLine("try {");
                    sb.AppendLine(@"var tmpis = new ActiveXObject(""ieSpell.ieSpellExtension"");");
                    sb.AppendLine("tmpis.CheckAllLinkedDocuments(document);");
                    sb.AppendLine("}");
                    sb.AppendLine("catch(exception) {");
                    sb.AppendLine("if(exception.number==-2146827859) {");
                    sb.AppendLine(@"if (confirm(""");
                    sb.AppendLine(Variables.Forum.strTxtIeSpellNotDetected);
                    sb.AppendLine(@"""))");
                    sb.AppendLine(@"openWin(""http://www.iespell.com/download.php"",""DownLoad"", """");");
                    sb.AppendLine("}");
                    sb.AppendLine("else");
                    sb.AppendLine(@"alert(""Error Loading ieSpell: Exception "" + exception.number);");
                    sb.AppendLine("}");
                    sb.AppendLine("}");
                }
                sb.AppendLine("</script>");
                return sb.ToString();
            }

        }
    }
}
