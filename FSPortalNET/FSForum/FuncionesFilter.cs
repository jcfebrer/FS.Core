// <fileheader>
// <copyright file="FuncionesFilter.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: clsFuncionesFilter.cs
//     Copyright (c) 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using FSLibrary;

namespace FSForum
{
    public static class FuncionesFilter
    {


        public static string checkHTML(string strMessageInput)
        {
            string checkHTMLReturn = null;

            string strTempHTMLMessage = null;
            long lngMessagePosition = 0;
            int intHTMLTagLength = 0;
            string strHTMLMessage = null;
            string strTempMessageInput = null;
            long lngLoopCounter = 0;

            string[] saryHTMLtags = new string[88];

            saryHTMLtags[0] = "html";
            saryHTMLtags[1] = "body";
            saryHTMLtags[2] = "head";
            saryHTMLtags[3] = "meta";
            saryHTMLtags[4] = "button";
            saryHTMLtags[5] = "input";
            saryHTMLtags[6] = "type";
            saryHTMLtags[7] = "select";
            saryHTMLtags[8] = "radio";
            saryHTMLtags[9] = "file";
            saryHTMLtags[10] = "hidden";
            saryHTMLtags[11] = "checkbox";
            saryHTMLtags[12] = "clave";
            saryHTMLtags[13] = "checked";
            saryHTMLtags[14] = "fieldset";
            saryHTMLtags[15] = "language";
            saryHTMLtags[16] = "javascript";
            saryHTMLtags[17] = "vbscript";
            saryHTMLtags[18] = "script";
            saryHTMLtags[19] = "object";
            saryHTMLtags[20] = "applet";
            saryHTMLtags[21] = "embed";
            saryHTMLtags[22] = "event";
            saryHTMLtags[23] = "server";
            saryHTMLtags[24] = "function";
            saryHTMLtags[25] = "document";
            saryHTMLtags[26] = "cookie";
            saryHTMLtags[27] = "onclick";
            saryHTMLtags[28] = "ondblclick";
            saryHTMLtags[29] = "onkeyup";
            saryHTMLtags[30] = "onkeydown";
            saryHTMLtags[31] = "onkeypress";
            saryHTMLtags[32] = "onkey";
            saryHTMLtags[33] = "onmouseenter";
            saryHTMLtags[34] = "onmouseleave";
            saryHTMLtags[35] = "onmousemove";
            saryHTMLtags[36] = "onmouseout";
            saryHTMLtags[37] = "onmouseover";
            saryHTMLtags[38] = "onrollover";
            saryHTMLtags[39] = "onmouse";
            saryHTMLtags[40] = "onchange";
            saryHTMLtags[41] = "onunloadhave";
            saryHTMLtags[42] = "onunload";
            saryHTMLtags[43] = "onsubmit";
            saryHTMLtags[44] = "onselect";
            saryHTMLtags[45] = "accesskey";
            saryHTMLtags[46] = "tabindex";
            saryHTMLtags[47] = "onfocus";
            saryHTMLtags[48] = "onblur";
            saryHTMLtags[49] = "onsubmit";
            saryHTMLtags[50] = "onreset";
            saryHTMLtags[51] = "form";
            saryHTMLtags[52] = "iframe";
            saryHTMLtags[53] = "ilayer";
            saryHTMLtags[54] = "textarea";
            saryHTMLtags[55] = "action";
            saryHTMLtags[56] = "enctype";
            saryHTMLtags[57] = "layer";
            saryHTMLtags[58] = "multicol";
            saryHTMLtags[59] = "frameset";
            saryHTMLtags[60] = "marquee";
            saryHTMLtags[61] = "blink";
            saryHTMLtags[62] = "filter";
            saryHTMLtags[63] = "overlay";
            saryHTMLtags[64] = "param";
            saryHTMLtags[65] = "bgsound";
            saryHTMLtags[66] = "behavior";
            saryHTMLtags[67] = "ismap";
            saryHTMLtags[68] = "sound";
            saryHTMLtags[69] = "disabled";
            saryHTMLtags[70] = "ENCTYPE";
            saryHTMLtags[71] = "!DOCTYPE";
            saryHTMLtags[72] = "BACKGROUND-COLOR";
            saryHTMLtags[73] = "base";
            saryHTMLtags[74] = "position";
            saryHTMLtags[75] = "absolute";
            saryHTMLtags[76] = "z-index";
            saryHTMLtags[77] = "isindex";
            saryHTMLtags[78] = "xhtml";
            saryHTMLtags[79] = "xml";
            saryHTMLtags[80] = "class";
            saryHTMLtags[81] = "map";
            saryHTMLtags[82] = "option";
            saryHTMLtags[83] = "box";
            saryHTMLtags[84] = "SAMP";
            saryHTMLtags[85] = "data";
            saryHTMLtags[86] = "frame";





            strMessageInput = strMessageInput.Replace(@"<script language=""javascript"">", "");
            strMessageInput = strMessageInput.Replace(@"<script language=""vbscript"">", "");
            strMessageInput = strMessageInput.Replace("<script language=vbscript>", "");
            strMessageInput = strMessageInput.Replace("</script>", "");


            strTempMessageInput = strMessageInput;

            for (lngMessagePosition = 1; lngMessagePosition <= ((long)(strMessageInput.Length)); lngMessagePosition++)
            {

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "")
                {
                    break;
                }

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "<")
                {

                    intHTMLTagLength = NumberUtils.NumberInt((TextUtil.IndexOf(strMessageInput, (int)lngMessagePosition, ">") - lngMessagePosition));

                    strHTMLMessage = TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), intHTMLTagLength + 1);

                    strTempHTMLMessage = strHTMLMessage;



                    if (TextUtil.IndexOf(strTempHTMLMessage, "href") != 0)
                    {

                        strTempHTMLMessage = strTempHTMLMessage.Replace("<", "**/**");
                        strTempHTMLMessage = strTempHTMLMessage.Replace(">", @"**\**");

                        strTempHTMLMessage = formatLink(strTempHTMLMessage);

                        strTempHTMLMessage = strTempHTMLMessage.Replace("**/**", "<");
                        strTempHTMLMessage = strTempHTMLMessage.Replace(@"**\**", ">");

                        strTempHTMLMessage = strTempHTMLMessage.Replace(">", @" target=""_blank"">");

                    }



                    if (TextUtil.IndexOf(strTempHTMLMessage, "img") != 0 & TextUtil.IndexOf(strTempHTMLMessage, "src") != 0)
                    {

                        strTempHTMLMessage = strTempHTMLMessage.Replace("<", "**/**");
                        strTempHTMLMessage = strTempHTMLMessage.Replace(">", @"**\**");

                        strTempHTMLMessage = checkImages(strTempHTMLMessage);

                        strTempHTMLMessage = strTempHTMLMessage.Replace("**/**", "<");
                        strTempHTMLMessage = strTempHTMLMessage.Replace(@"**\**", ">");

                        strTempHTMLMessage = strTempHTMLMessage.Replace(">", @" border=""0"">");

                    }



                    if (TextUtil.IndexOf(strTempHTMLMessage, "href") == 0 & TextUtil.IndexOf(strTempHTMLMessage, "img") == 0)
                    {

                        for (lngLoopCounter = saryHTMLtags.GetLowerBound(0); lngLoopCounter <= saryHTMLtags.GetUpperBound(0); lngLoopCounter++)
                        {
                            strTempHTMLMessage = strTempHTMLMessage.Replace(saryHTMLtags[NumberUtils.NumberInt(lngLoopCounter)], "");
                        }
                    }




                    strTempHTMLMessage = FSForum.FuncionesFilter.formatInput(strTempHTMLMessage);


                    strTempMessageInput = strTempMessageInput.Replace(strHTMLMessage, strTempHTMLMessage);

                }
            }

            checkHTMLReturn = strTempMessageInput;
            return checkHTMLReturn;
        }



        public static string checkImages(string strInputEntry)
        {

            string strImageFileExtension = null;
            string[] saryImageTypes = null;
            int intExtensionLoopCounter = 0;
            bool blnImageExtOK = false;

            if (TextUtil.IndexOf(strInputEntry, ".") == 0)
            {

                strInputEntry = "";

            }
            else
            {

                blnImageExtOK = false;

                strImageFileExtension = TextUtil.Substring(strInputEntry, TextUtil.LastIndexOf(strInputEntry, "."), 4).ToLower();

                FSForum.Variables.Forum.strImageTypes = FSForum.Variables.Forum.strImageTypes + ";gif;jpg;jpe;bmp;png";

                string tt = TextUtil.Trim(FSForum.Variables.Forum.strImageTypes);
                saryImageTypes = tt.Split(";".Split("".ToCharArray()), StringSplitOptions.None);

                for (intExtensionLoopCounter = 0; intExtensionLoopCounter <= saryImageTypes.GetUpperBound(0); intExtensionLoopCounter++)
                {

                    tt = TextUtil.Substring(saryImageTypes[intExtensionLoopCounter], 1, 3);
                    saryImageTypes[intExtensionLoopCounter] = "." + tt.Trim();

                    if (saryImageTypes[intExtensionLoopCounter] == strImageFileExtension)
                    {
                        blnImageExtOK = true;
                    }
                }

                if (blnImageExtOK == false)
                {
                    strInputEntry = strInputEntry.Replace(strImageFileExtension, "");
                }

                strInputEntry = formatLink(strInputEntry);


                strInputEntry = strInputEntry.Replace("?", "");
            }

            return strInputEntry;
        }







        public static string formatLink(string strInputEntry)
        {

            strInputEntry = strInputEntry.Replace("document.cookie", ".");
            strInputEntry = strInputEntry.Replace("javascript:", "javascript ");
            strInputEntry = strInputEntry.Replace("vbscript:", "vbscript ");
            strInputEntry = strInputEntry.Replace("javascript :", "javascript ");
            strInputEntry = strInputEntry.Replace("vbscript :", "vbscript ");
            strInputEntry = strInputEntry.Replace("[", "");
            strInputEntry = strInputEntry.Replace("]", "");
            strInputEntry = strInputEntry.Replace("(", "");
            strInputEntry = strInputEntry.Replace(")", "");
            strInputEntry = strInputEntry.Replace("{", "");
            strInputEntry = strInputEntry.Replace("}", "");
            strInputEntry = strInputEntry.Replace("<", "");
            strInputEntry = strInputEntry.Replace(">", "");
            strInputEntry = strInputEntry.Replace("|", "");
            strInputEntry = strInputEntry.Replace("script", "&#115;cript");
            strInputEntry = strInputEntry.Replace("object", "&#111;bject");
            strInputEntry = strInputEntry.Replace("applet", "&#097;pplet");
            strInputEntry = strInputEntry.Replace("embed", "&#101;mbed");
            strInputEntry = strInputEntry.Replace("document", "&#100;ocument");
            strInputEntry = strInputEntry.Replace("cookie", "&#099;ookie");
            strInputEntry = strInputEntry.Replace("event", "&#101;vent");

            return strInputEntry;
        }








        public static string formatInput(string strInputEntry)
        {

            strInputEntry = strInputEntry.Replace("</script>", "");
            strInputEntry = strInputEntry.Replace(@"<script language=""javascript"">", "");
            strInputEntry = strInputEntry.Replace("<script language=javascript>", "");
            strInputEntry = strInputEntry.Replace("script", "&#115;cript");
            strInputEntry = strInputEntry.Replace("SCRIPT", "&#083;CRIPT");
            strInputEntry = strInputEntry.Replace("Script", "&#083;cript");
            strInputEntry = strInputEntry.Replace("script", "&#083;cript");
            strInputEntry = strInputEntry.Replace("object", "&#111;bject");
            strInputEntry = strInputEntry.Replace("OBJECT", "&#079;BJECT");
            strInputEntry = strInputEntry.Replace("Object", "&#079;bject");
            strInputEntry = strInputEntry.Replace("object", "&#079;bject");
            strInputEntry = strInputEntry.Replace("applet", "&#097;pplet");
            strInputEntry = strInputEntry.Replace("APPLET", "&#065;PPLET");
            strInputEntry = strInputEntry.Replace("Applet", "&#065;pplet");
            strInputEntry = strInputEntry.Replace("applet", "&#065;pplet");
            strInputEntry = strInputEntry.Replace("embed", "&#101;mbed");
            strInputEntry = strInputEntry.Replace("EMBED", "&#069;MBED");
            strInputEntry = strInputEntry.Replace("Embed", "&#069;mbed");
            strInputEntry = strInputEntry.Replace("embed", "&#069;mbed");
            strInputEntry = strInputEntry.Replace("event", "&#101;vent");
            strInputEntry = strInputEntry.Replace("EVENT", "&#069;VENT");
            strInputEntry = strInputEntry.Replace("Event", "&#069;vent");
            strInputEntry = strInputEntry.Replace("event", "&#069;vent");
            strInputEntry = strInputEntry.Replace("document", "&#100;ocument");
            strInputEntry = strInputEntry.Replace("DOCUMENT", "&#068;OCUMENT");
            strInputEntry = strInputEntry.Replace("Document", "&#068;ocument");
            strInputEntry = strInputEntry.Replace("document", "&#068;ocument");
            strInputEntry = strInputEntry.Replace("cookie", "&#099;ookie");
            strInputEntry = strInputEntry.Replace("COOKIE", "&#067;OOKIE");
            strInputEntry = strInputEntry.Replace("Cookie", "&#067;ookie");
            strInputEntry = strInputEntry.Replace("cookie", "&#067;ookie");
            strInputEntry = strInputEntry.Replace("form", "&#102;orm");
            strInputEntry = strInputEntry.Replace("FORM", "&#070;ORM");
            strInputEntry = strInputEntry.Replace("Form", "&#070;orm");
            strInputEntry = strInputEntry.Replace("form", "&#070;orm");
            strInputEntry = strInputEntry.Replace("iframe", "i&#102;rame");
            strInputEntry = strInputEntry.Replace("IFRAME", "I&#070;RAME");
            strInputEntry = strInputEntry.Replace("Iframe", "I&#102;rame");
            strInputEntry = strInputEntry.Replace("iframe", "i&#102;rame");
            strInputEntry = strInputEntry.Replace("textarea", "&#116;extarea");
            strInputEntry = strInputEntry.Replace("TEXTAREA", "&#84;EXTAREA");
            strInputEntry = strInputEntry.Replace("Textarea", "&#84;extarea");
            strInputEntry = strInputEntry.Replace("textarea", "&#84;extarea");

            strInputEntry = strInputEntry.Replace("<STR&#079;NG>", "<strong>");
            strInputEntry = strInputEntry.Replace("<str&#111;ng>", "<strong>");
            strInputEntry = strInputEntry.Replace("</STR&#079;NG>", "</strong>");
            strInputEntry = strInputEntry.Replace("</str&#111;ng>", "</strong>");
            strInputEntry = strInputEntry.Replace("f&#111;nt", "font");
            strInputEntry = strInputEntry.Replace("F&#079;NT", "FONT");
            strInputEntry = strInputEntry.Replace("F&#111;nt", "Font");
            strInputEntry = strInputEntry.Replace("f&#079;nt", "font");
            strInputEntry = strInputEntry.Replace("f&#111;nt", "font");
            strInputEntry = strInputEntry.Replace("m&#111;no", "mono");
            strInputEntry = strInputEntry.Replace("M&#079;NO", "MONO");
            strInputEntry = strInputEntry.Replace("M&#079;no", "Mono");
            strInputEntry = strInputEntry.Replace("m&#079;no", "mono");
            strInputEntry = strInputEntry.Replace("m&#111;no", "mono");

            return strInputEntry;
        }








        public static string formatSQLInput(string strInputEntry)
        {

            strInputEntry = TextUtil.Replace(strInputEntry, "<", "&lt;");
            strInputEntry = TextUtil.Replace(strInputEntry, ">", "&gt;");
            strInputEntry = TextUtil.Replace(strInputEntry, "[", "&#091;");
            strInputEntry = TextUtil.Replace(strInputEntry, "]", "&#093;");
            strInputEntry = TextUtil.Replace(strInputEntry, @"""", "");
            strInputEntry = TextUtil.Replace(strInputEntry, "=", "&#061;");
            strInputEntry = TextUtil.Replace(strInputEntry, "'", "''");
            strInputEntry = TextUtil.Replace(strInputEntry, "select", "sel&#101;ct");
            strInputEntry = TextUtil.Replace(strInputEntry, "join", "jo&#105;n");
            strInputEntry = TextUtil.Replace(strInputEntry, "union", "un&#105;on");
            strInputEntry = TextUtil.Replace(strInputEntry, "where", "wh&#101;re");
            strInputEntry = TextUtil.Replace(strInputEntry, "insert", "ins&#101;rt");
            strInputEntry = TextUtil.Replace(strInputEntry, "delete", "del&#101;te");
            strInputEntry = TextUtil.Replace(strInputEntry, "update", "up&#100;ate");
            strInputEntry = TextUtil.Replace(strInputEntry, "like", "lik&#101;");
            strInputEntry = TextUtil.Replace(strInputEntry, "drop", "dro&#112;");
            strInputEntry = TextUtil.Replace(strInputEntry, "create", "cr&#101;ate");
            strInputEntry = TextUtil.Replace(strInputEntry, "modify", "mod&#105;fy");
            strInputEntry = TextUtil.Replace(strInputEntry, "rename", "ren&#097;me");
            strInputEntry = TextUtil.Replace(strInputEntry, "alter", "alt&#101;r");
            strInputEntry = TextUtil.Replace(strInputEntry, "cast", "ca&#115;t");

            return strInputEntry;
        }







        public static string removeAllTags(string strInputEntry)
        {

            strInputEntry = TextUtil.Replace(strInputEntry, "&", "&amp;");
            strInputEntry = TextUtil.Replace(strInputEntry, "<", "&lt;");
            strInputEntry = TextUtil.Replace(strInputEntry, ">", "&gt;");
            strInputEntry = TextUtil.Replace(strInputEntry, "'", "&#146;");
            strInputEntry = TextUtil.Replace(strInputEntry, @"""", "&amp;quot;");

            return strInputEntry;
        }







        public static string characterStrip(string strTextInput)
        {

            int intLoopCounter = 0;

            for (intLoopCounter = 0; intLoopCounter <= 47; intLoopCounter++)
            {
                strTextInput = strTextInput.Replace((char)(intLoopCounter), ' ');
            }

            for (intLoopCounter = 91; intLoopCounter <= 96; intLoopCounter++)
            {
                strTextInput = strTextInput.Replace((char)(intLoopCounter), ' ');
            }

            for (intLoopCounter = 58; intLoopCounter <= 64; intLoopCounter++)
            {
                strTextInput = strTextInput.Replace((char)(intLoopCounter), ' ');
            }

            for (intLoopCounter = 123; intLoopCounter <= 255; intLoopCounter++)
            {
                strTextInput = strTextInput.Replace((char)(intLoopCounter), ' ');
            }


            return strTextInput;

        }







        public static string removeHTML(string strMessageInput)
        {

            long lngMessagePosition = 0;
            int intHTMLTagLength = 0;
            string strHTMLMessage = null;
            string strTempMessageInput = null;


            strTempMessageInput = strMessageInput;

            for (lngMessagePosition = 1; lngMessagePosition <= ((long)(strMessageInput.Length - 1)); lngMessagePosition++)
            {

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "")
                {
                    break;
                }

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "<")
                {

                    intHTMLTagLength = NumberUtils.NumberInt((TextUtil.IndexOf(strMessageInput, (int)lngMessagePosition, ">") - lngMessagePosition));

                    if (intHTMLTagLength < 0)
                    {
                        intHTMLTagLength = NumberUtils.NumberInt(((long)(strTempMessageInput.Length)));
                    }

                    strHTMLMessage = TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), intHTMLTagLength + 1);


                    strTempMessageInput = strTempMessageInput.Replace(strHTMLMessage, "");

                }
            }

            strTempMessageInput = strTempMessageInput.Replace("<", "&lt;");
            strTempMessageInput = strTempMessageInput.Replace(">", "&gt;");
            strTempMessageInput = strTempMessageInput.Replace("'", "&#039;");
            strTempMessageInput = strTempMessageInput.Replace(@"""", "&#034;");
            strTempMessageInput = strTempMessageInput.Replace("&nbsp;", "");

            return strTempMessageInput;
        }







        public static string removeLongText(string strMessageInput)
        {
            string removeLongTextReturn = null;

            long lngMessagePosition = 0;
            int intHTMLTagLength = 0;
            string strHTMLMessage = "";
            string strTempMessageText = null;
            string strTempPlainTextWord = null;
            string[] saryPlainTextWord = null;
            string[] sarySplitTextWord = null;
            long lngSplitPlainTextWordLoop = 0;
            string strTempOutputMessage = null;
            int intWordSartPos = 0;
            string[,] saryHTMLlinks = { { "" }, { "" } };
            string strHTMLlinksCode = null;
            long lngLoopCounter = 0;
            bool blnHTMLlink = false;
            string strTempFlashMsg = null;
            long lngStartPos = 0;
            long lngEndPos = 0;
            const int intMaxWordLength = 60;


            lngLoopCounter = 0;
            blnHTMLlink = false;

            strTempMessageText = strMessageInput;
            strTempOutputMessage = strMessageInput;




            while (TextUtil.IndexOf(strTempMessageText, "[FLASH") > 0 & TextUtil.IndexOf(strTempMessageText, "[/FLASH]") > 0)
            {

                lngStartPos = TextUtil.IndexOf(strTempMessageText, "[FLASH");
                lngEndPos = TextUtil.IndexOf(strTempMessageText, (int)lngStartPos, "[/FLASH]") + 8;

                if (lngEndPos < lngStartPos)
                {
                    lngEndPos = lngStartPos + 6;
                }

                string st = TextUtil.Substring(strTempMessageText, NumberUtils.NumberInt(lngStartPos), NumberUtils.NumberInt(lngEndPos - lngStartPos));
                strTempFlashMsg = st.Trim();

                lngLoopCounter = lngLoopCounter + 1;

                string[,] tt = new string[2, lngLoopCounter];
                System.Array.Copy(saryHTMLlinks, tt, System.Math.Min(saryHTMLlinks.Length, tt.Length));
                saryHTMLlinks = tt;

                strHTMLlinksCode = " **/**WWFflash00" + lngLoopCounter + @"**\** ";

                saryHTMLlinks[1, NumberUtils.NumberInt(lngLoopCounter)] = strHTMLlinksCode;
                saryHTMLlinks[2, NumberUtils.NumberInt(lngLoopCounter)] = strHTMLMessage;

                strTempMessageText = TextUtil.Replace(strTempMessageText, strTempFlashMsg, strHTMLlinksCode);

                blnHTMLlink = true;

                string reemp = TextUtil.Replace(strTempFlashMsg, "[", "&#91;");
                strTempMessageText = strTempMessageText.Replace(strTempFlashMsg, reemp);
            }





            for (lngMessagePosition = 1; lngMessagePosition <= ((long)(strMessageInput.Length)); lngMessagePosition++)
            {

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "")
                {
                    break;
                }

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "<")
                {

                    intHTMLTagLength = NumberUtils.NumberInt((TextUtil.IndexOf(strMessageInput, (int)lngMessagePosition, ">") - lngMessagePosition));

                    if (intHTMLTagLength < 0)
                    {
                        intHTMLTagLength = NumberUtils.NumberInt(((long)(strTempMessageText.Length)));
                    }

                    strHTMLMessage = TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), intHTMLTagLength + 1);



                    if (TextUtil.IndexOf(strHTMLMessage, "href") > 0)
                    {

                        lngLoopCounter = lngLoopCounter + 1;

                        string[,] tt = new string[2, lngLoopCounter];
                        System.Array.Copy(saryHTMLlinks, tt, System.Math.Min(saryHTMLlinks.Length, tt.Length));
                        saryHTMLlinks = tt;

                        strHTMLlinksCode = " **/**WWFlink00" + lngLoopCounter + @"**\** ";

                        saryHTMLlinks[1, NumberUtils.NumberInt(lngLoopCounter)] = strHTMLlinksCode;
                        saryHTMLlinks[2, NumberUtils.NumberInt(lngLoopCounter)] = strHTMLMessage;

                        strTempOutputMessage = strTempOutputMessage.Replace(strHTMLMessage, strHTMLlinksCode);

                        blnHTMLlink = true;
                    }

                    strTempMessageText = strTempMessageText.Replace(strHTMLMessage, " ");
                }
            }




            for (lngMessagePosition = 1; lngMessagePosition <= ((long)(strMessageInput.Length)); lngMessagePosition++)
            {

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "")
                {
                    break;
                }

                if (TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), 1) == "[")
                {

                    intHTMLTagLength = NumberUtils.NumberInt((TextUtil.IndexOf(strMessageInput, (int)lngMessagePosition, "]") - lngMessagePosition));

                    if (intHTMLTagLength < 0)
                    {
                        intHTMLTagLength = NumberUtils.NumberInt(((long)(strTempMessageText.Length)));
                    }

                    strHTMLMessage = TextUtil.Substring(strMessageInput, NumberUtils.NumberInt(lngMessagePosition), intHTMLTagLength + 1);

                    strTempMessageText = strTempMessageText.Replace(strHTMLMessage, " ");
                }
            }




            string tt2 = TextUtil.Trim(strTempMessageText);
            saryPlainTextWord = tt2.Split(" ".Split("".ToCharArray()), StringSplitOptions.None);

            for (lngLoopCounter = 0; lngLoopCounter <= saryPlainTextWord.GetUpperBound(0); lngLoopCounter++)
            {

                if (saryPlainTextWord[NumberUtils.NumberInt(lngLoopCounter)].Length > intMaxWordLength)
                {

                    sarySplitTextWord = new string[NumberUtils.NumberInt(saryPlainTextWord[NumberUtils.NumberInt(lngLoopCounter)].Length / intMaxWordLength + 1)];

                    intWordSartPos = 1;

                    for (lngSplitPlainTextWordLoop = 1; lngSplitPlainTextWordLoop <= sarySplitTextWord.GetUpperBound(0); lngSplitPlainTextWordLoop++)
                    {

                        sarySplitTextWord[NumberUtils.NumberInt(lngSplitPlainTextWordLoop)] = TextUtil.Substring(saryPlainTextWord[NumberUtils.NumberInt(lngLoopCounter)], intWordSartPos, intMaxWordLength);

                        intWordSartPos = intWordSartPos + intMaxWordLength;
                    }

                    strTempPlainTextWord = System.String.Join(" ", (string[])sarySplitTextWord);

                    strTempOutputMessage = strTempOutputMessage.Replace(saryPlainTextWord[NumberUtils.NumberInt(lngLoopCounter)], strTempPlainTextWord);
                }
            }




            if (blnHTMLlink)
            {
                for (lngLoopCounter = 1; lngLoopCounter <= saryHTMLlinks.GetUpperBound(1); lngLoopCounter++)
                {

                    saryHTMLlinks[2, NumberUtils.NumberInt(lngLoopCounter)] = saryHTMLlinks[2, NumberUtils.NumberInt(lngLoopCounter)].Replace("\r\n", "");

                    strTempOutputMessage = strTempOutputMessage.Replace(saryHTMLlinks[1, NumberUtils.NumberInt(lngLoopCounter)], saryHTMLlinks[2, NumberUtils.NumberInt(lngLoopCounter)]);

                }
            }


            removeLongTextReturn = strTempOutputMessage;
            return removeLongTextReturn;
        }


        public static string decodeString(string strInputEntry)
        {
            strInputEntry = strInputEntry.Replace("&#097;", "a");
            strInputEntry = strInputEntry.Replace("&#098;", "b");
            strInputEntry = strInputEntry.Replace("&#099;", "c");
            strInputEntry = strInputEntry.Replace("&#100;", "d");
            strInputEntry = strInputEntry.Replace("&#101;", "e");
            strInputEntry = strInputEntry.Replace("&#102;", "f");
            strInputEntry = strInputEntry.Replace("&#103;", "g");
            strInputEntry = strInputEntry.Replace("&#104;", "h");
            strInputEntry = strInputEntry.Replace("&#105;", "i");
            strInputEntry = strInputEntry.Replace("&#106;", "j");
            strInputEntry = strInputEntry.Replace("&#107;", "k");
            strInputEntry = strInputEntry.Replace("&#108;", "l");
            strInputEntry = strInputEntry.Replace("&#109;", "m");
            strInputEntry = strInputEntry.Replace("&#110;", "n");
            strInputEntry = strInputEntry.Replace("&#111;", "o");
            strInputEntry = strInputEntry.Replace("&#112;", "p");
            strInputEntry = strInputEntry.Replace("&#113;", "q");
            strInputEntry = strInputEntry.Replace("&#114;", "r");
            strInputEntry = strInputEntry.Replace("&#115;", "s");
            strInputEntry = strInputEntry.Replace("&#116;", "t");
            strInputEntry = strInputEntry.Replace("&#117;", "u");
            strInputEntry = strInputEntry.Replace("&#118;", "v");
            strInputEntry = strInputEntry.Replace("&#119;", "w");
            strInputEntry = strInputEntry.Replace("&#120;", "x");
            strInputEntry = strInputEntry.Replace("&#121;", "y");
            strInputEntry = strInputEntry.Replace("&#122;", "z");

            strInputEntry = strInputEntry.Replace("&#065;", "A");
            strInputEntry = strInputEntry.Replace("&#066;", "B");
            strInputEntry = strInputEntry.Replace("&#067;", "C");
            strInputEntry = strInputEntry.Replace("&#068;", "D");
            strInputEntry = strInputEntry.Replace("&#069;", "E");
            strInputEntry = strInputEntry.Replace("&#070;", "F");
            strInputEntry = strInputEntry.Replace("&#071;", "G");
            strInputEntry = strInputEntry.Replace("&#072;", "H");
            strInputEntry = strInputEntry.Replace("&#073;", "I");
            strInputEntry = strInputEntry.Replace("&#074;", "J");
            strInputEntry = strInputEntry.Replace("&#075;", "K");
            strInputEntry = strInputEntry.Replace("&#076;", "L");
            strInputEntry = strInputEntry.Replace("&#077;", "M");
            strInputEntry = strInputEntry.Replace("&#078;", "N");
            strInputEntry = strInputEntry.Replace("&#079;", "O");
            strInputEntry = strInputEntry.Replace("&#080;", "P");
            strInputEntry = strInputEntry.Replace("&#081;", "Q");
            strInputEntry = strInputEntry.Replace("&#082;", "R");
            strInputEntry = strInputEntry.Replace("&#083;", "S");
            strInputEntry = strInputEntry.Replace("&#084;", "T");
            strInputEntry = strInputEntry.Replace("&#085;", "U");
            strInputEntry = strInputEntry.Replace("&#086;", "V");
            strInputEntry = strInputEntry.Replace("&#087;", "W");
            strInputEntry = strInputEntry.Replace("&#088;", "X");
            strInputEntry = strInputEntry.Replace("&#089;", "Y");
            strInputEntry = strInputEntry.Replace("&#090;", "Z");


            strInputEntry = strInputEntry.Replace("&#048;", "0");
            strInputEntry = strInputEntry.Replace("&#049;", "1");
            strInputEntry = strInputEntry.Replace("&#050;", "2");
            strInputEntry = strInputEntry.Replace("&#051;", "3");
            strInputEntry = strInputEntry.Replace("&#052;", "4");
            strInputEntry = strInputEntry.Replace("&#053;", "5");
            strInputEntry = strInputEntry.Replace("&#054;", "6");
            strInputEntry = strInputEntry.Replace("&#055;", "7");
            strInputEntry = strInputEntry.Replace("&#056;", "8");
            strInputEntry = strInputEntry.Replace("&#057;", "9");


            strInputEntry = strInputEntry.Replace("&#061;", "=");
            strInputEntry = strInputEntry.Replace("&lt;", "<");
            strInputEntry = strInputEntry.Replace("&gt;", ">");
            strInputEntry = strInputEntry.Replace("&amp;", "&");
            strInputEntry = strInputEntry.Replace("&#146;", "'");
            strInputEntry = strInputEntry.Replace("&quot;", "'");

            return strInputEntry;
        }


        public static string searchHighlighter(string strMessage, string[] sarySearchWord)
        {
            string searchHighlighterReturn = null;

            int intHighlightLoopCounter = 0;
            string strTempMessage = "";
            long lngMessagePosition = 0;
            int intHTMLTagLength = 0;
            int intSearchWordLength = 0;
            bool blnTempUpdate = false;


            for (lngMessagePosition = 1; lngMessagePosition <= strMessage.Length; lngMessagePosition++)
            {

                blnTempUpdate = false;

                if (TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngMessagePosition), 1) == "<")
                {

                    intHTMLTagLength = NumberUtils.NumberInt((TextUtil.IndexOf(strMessage, ">") - lngMessagePosition));

                    strTempMessage = strTempMessage + TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngMessagePosition), intHTMLTagLength);

                    lngMessagePosition = lngMessagePosition + intHTMLTagLength;
                }

                for (intHighlightLoopCounter = 0; intHighlightLoopCounter <= sarySearchWord.GetUpperBound(0); intHighlightLoopCounter++)
                {

                    if (sarySearchWord[intHighlightLoopCounter] != "")
                    {

                        intSearchWordLength = sarySearchWord[intHighlightLoopCounter].Length;

                        if (TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngMessagePosition), intSearchWordLength) == sarySearchWord[intHighlightLoopCounter].ToLower())
                        {

                            strTempMessage = strTempMessage + @"<span class=""highlight"">" + TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngMessagePosition), intSearchWordLength) + "</span>";

                            lngMessagePosition = lngMessagePosition + intSearchWordLength - 1;

                            blnTempUpdate = true;
                        }
                    }
                }

                if (blnTempUpdate == false)
                {
                    strTempMessage = strTempMessage + TextUtil.Substring(strMessage, NumberUtils.NumberInt(lngMessagePosition), 1);
                }
            }

            searchHighlighterReturn = strTempMessage;
            return searchHighlighterReturn;
        }
    }
}