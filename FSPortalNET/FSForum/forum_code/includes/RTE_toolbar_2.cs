// <fileheader>
// <copyright file="RTE_toolbar_2.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\RTE_toolbar_2.ascx.cs
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
        public class RTE_toolbar_2
        {

            public static bool blnAbout;
            public static bool blnBold;
            public static bool blnUnderline;
            public static bool blnItalic;
            public static bool blnFontStyle;
            public static bool blnFontType;
            public static bool blnFontSize;
            public static bool blnTextColour;
            public static bool blnTextBackgroundColour;
            public static bool blnCut;
            public static bool blnCopy;
            public static bool blnPaste;
            public static bool blnUndo;
            public static bool blnRedo;
            public static bool blnLeftJustify;
            public static bool blnCentre;
            public static bool blnRightJustify;
            public static bool blnFullJustify;
            public static bool blnOrderList;
            public static bool blnUnOrderList;
            public static bool blnOutdent;
            public static bool blnIndent;
            public static bool blnAddHyperlink;
            public static bool blnAddImage;
            public static bool blnInsertTable;
            public static bool blnHTMLView;
            public static bool blnSpellCheck;
            public static bool blnEmoticonPopUp;

            public static string RTEenabled;

            public static string Render()
            {
                StringBuilder sb = new StringBuilder();
                // RTE Toolbox 2
                sb.AppendLine(("\r\n" + "             <span id=\"ToolBar2\">"));
                if ((blnCut
                            && (RTEenabled != "Gecko")))
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_cut.gif\" align=\"middle\" onClick=\"FormatText(\'cut\')\" style=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtCut + "\"> "))))));
                }

                if ((blnCopy
                            && (RTEenabled != "Gecko")))
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_copy.gif\" align=\"middle\" onClick=\"FormatText(\'copy\')\" style=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtCopy + "\"> "))))));
                }

                if ((blnPaste
                            && (RTEenabled != "Gecko")))
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_paste.gif\" align=\"middle\" onClick=\"FormatText(\'paste\')\" style=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtPaste + "\"> "))))));
                }

                if (blnUndo)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_undo.gif\" align=\"middle\" onClick=\"FormatText(\'undo\')\" style=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtUndo + "\">"))))));
                }

                if (blnRedo)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_redo.gif\" align=\"middle\" onClick=\"FormatText(\'redo\')\" style=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtRedo + "\"> "))))));
                }

                if (blnLeftJustify)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_left_just.gif\" align=\"middle\" onClick=\"FormatText(\'JustifyLeft\', \'\')\" style=\"cursor: poin" +
                                    "ter;\" title=\""
                                    + (Variables.Forum.strTxtLeftJustify + "\">"))))));
                }

                if (blnCentre)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_centre.gif\" align=\"middle\" border=\"0\" onClick=\"FormatText(\'JustifyCenter\', \'\')\" style=\"cu" +
                                    "rsor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtCentrejustify + "\">"))))));
                }

                if (blnRightJustify)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_right_just.gif\" align=\"middle\" onClick=\"FormatText(\'JustifyRight\', \'\')\" style=\"cursor: po" +
                                    "inter;\" title=\""
                                    + (Variables.Forum.strTxtRightJustify + "\">"))))));
                }

                if (blnFullJustify)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_justify.gif\" align=\"middle\" onClick=\"FormatText(\'JustifyFull\', \'\')\" style=\"cursor: pointe" +
                                    "r;\" title=\""
                                    + (Variables.Forum.strTxtJustify + "\"> "))))));
                }

                if (blnOrderList)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_or_list.gif\" align=\"middle\" border=\"0\" onClick=\"FormatText(\'InsertOrderedList\', \'\')\" styl" +
                                    "e=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtOrderedList + "\">"))))));
                }

                if (blnUnOrderList)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_list.gif\" align=\"middle\" border=\"0\" onClick=\"FormatText(\'InsertUnorderedList\', \'\')\" style" +
                                    "=\"cursor: pointer;\" title=\""
                                    + (Variables.Forum.strTxtUnorderedList + "\"> "))))));
                }

                if (blnOutdent)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_outdent.gif\" align=\"middle\" onClick=\"FormatText(\'Outdent\', \'\')\" style=\"cursor: pointer;\" " +
                                    "title=\""
                                    + (Variables.Forum.strTxtOutdent + "\">"))))));
                }

                if (blnIndent)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_indent.gif\" align=\"middle\" border=\"0\" onClick=\"FormatText(\'indent\', \'\')\" style=\"cursor: p" +
                                    "ointer;\" title=\""
                                    + (Variables.Forum.strTxtIndent + "\"> "))))));
                }

                if (blnAddHyperlink)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_hyperlink.gif\" align=\"middle\" border=\"0\" onClick=\"FormatText(\'createLink\')\" style=\"cursor" +
                                    ": pointer;\" title=\""
                                    + (Variables.Forum.strTxtAddHyperlink + "\">"))))));
                }

                // If this is IE then open pop up image insert window
                if ((((RTEenabled == "winIE")
                            || (RTEenabled == "winIE5"))
                            && blnAddImage))
                {
                    sb.AppendLine(("\r\n" + ("             <a href=\"javascript:openWin(\'RTE_image_window.aspx\',\'insertImg\',\'toolbar=0,location=0,st" +
                        "atus=0,menubar=0,scrollbars=0,resizable=0,width=400,height=200\')\"><img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_image.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtAddImage + "\" style=\"cursor: pointer;\"></a>"))))));
                    // If this is Gecko have a pop up JS prompt for link to image URL
                }
                else if (blnAddImage)
                {
                    sb.AppendLine(("\r\n" + ("\t     <img src=\""
                                    + (Variables.Forum.strImagePath + "post_button_image.gif\" align=\"middle\" border=\"0\" title=\"Add Image\" onClick=\"AddImage()\" style=\"cursor" +
                                    ": pointer;\">"))));
                }

                // If this is IE then open pop up table insert window
                if ((((RTEenabled == "winIE")
                            || (RTEenabled == "winIE5"))
                            && blnInsertTable))
                {
                    sb.AppendLine(("\r\n" + ("             <a href=\"javascript:openWin(\'RTE_table_window.aspx\',\'insertTable\',\'toolbar=0,location=0," +
                        "status=0,menubar=0,scrollbars=0,resizable=0,width=400,height=190\')\"><img src=\""
                                    + (Variables.Forum.strImagePath + ("post_insert_table.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtInsertTable + "\" style=\"cursor: pointer;\"></a>"))))));
                }

                // Button Pop up for emoticons 
                if (blnEmoticonPopUp)
                {
                    sb.AppendLine(("\r\n" + ("             <a href=\"javascript:openWin(\'RTE_emoticons_smilies.aspx\',\'emot\',\'toolbar=0,location=0,st" +
                        "atus=0,menubar=0,scrollbars=1,resizable=1,width=400,height=400\')\"><img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_smiley.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtEmoticons + "\" style=\"cursor: pointer;\"></a> "))))));
                }

                // If this is IE then show the spell check button
                if ((((RTEenabled == "winIE")
                            || (RTEenabled == "winIE5"))
                            && blnSpellCheck))
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_spell_check.gif\" align=\"middle\" border=\"0\" onClick=\"checkspell()\" style=\"cursor: pointer;" +
                                    "\" title=\""
                                    + (Variables.Forum.strTxtstrSpellCheck + "\">"))))));
                }

                sb.AppendLine(("\r\n" + "\t     </span>"));
                if (blnHTMLView)
                {
                    sb.AppendLine(("\r\n" + ("\t     <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_html.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtToggleHTMLView + "\" onClick=\"HTMLview()\" style=\"cursor: pointer;\">"))))));
                }

                if (blnAbout)
                {
                    sb.AppendLine(("\r\n" + ("              <a href=\"javascript:openWin(\'RTE_about.aspx\',\'about\',\'toolbar=0,location=0,status=" +
                        "0,menubar=0,scrollbars=0,resizable=0,width=400,height=200\')\"><img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_about.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtAboutRichTextEditor + "\" style=\"cursor: pointer;\"></a>"))))));
                }
                return sb.ToString();
            }

        }

    }
}