// <fileheader>
// <copyright file="RTE_toolbar_1.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\RTE_toolbar_1.ascx.cs
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
        public class RTE_toolbar_1
        {
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
                // RTE Toolbar 1
                sb.AppendLine(("\r\n" + "             <span id=\"ToolBar1\">"));
                // Font Style
                if (blnFontStyle)
                {
                    sb.AppendLine(("\r\n" + ("             <select id=\"selectTxtBlock\" name=\"selectTxtBlock\" onChange=\"FormatText(\'formatblock\', se" +
                        "lectTxtBlock.options[selectTxtBlock.selectedIndex].value); document.frmAddMessage.selectTxtBlock.opt" +
                        "ions[0].selected = true;\">" + ("\r\n" + ("\t      <option value=\"0\" selected>-- "
                                    + (Variables.Forum.strTxtFontStyle + (" --</option>" + ("\r\n" + ("\t      <option value=\"<p>\">Normal</option>" + ("\r\n" + ("\t      <option value=\"<p>\">Paragraph</option>" + ("\r\n" + ("\t      <option value=\"<h1>\">Heading 1 <h1></option>" + ("\r\n" + ("\t      <option value=\"<h2>\">Heading 2 <h2></option>" + ("\r\n" + ("\t      <option value=\"<h3>\">Heading 3 <h3></option>" + ("\r\n" + ("\t      <option value=\"<h4>\">Heading 4 <h4></option>" + ("\r\n" + ("\t      <option value=\"<h5>\">Heading 5 <h5></option>" + ("\r\n" + ("\t      <option value=\"<h6>\">Heading 6 <h6></option>" + ("\r\n" + ("\t      <option value=\"<address>\">Address <ADDR></option>" + ("\r\n" + ("\t      <option value=\"<pre>\">Formatted <pre></option>" + ("\r\n" + "             </select>"))))))))))))))))))))))))))));
                }

                // Font Type
                if (blnFontType)
                {
                    sb.AppendLine(("\r\n" + ("             <select id=\"selectFont\" name=\"selectFont\" onChange=\"FormatText(\'fontname\', selectFont.op" +
                        "tions[selectFont.selectedIndex].value); document.frmAddMessage.selectFont.options[0].selected = true" +
                        ";\">" + ("\r\n" + ("              <option value=\"0\" selected>-- "
                                    + (Variables.Forum.strTxtFontTypes + (" --</option>" + ("\r\n" + ("              <option value=\"Arial, Helvetica, sans-serif\">Arial</option>" + ("\r\n" + ("              <option value=\"Times New Roman, Times, serif\">Times</option>" + ("\r\n" + ("              <option value=\"Courier New, Courier, mono\">Courier New</option>" + ("\r\n" + ("              <option value=\"Verdana, Arial, Helvetica, sans-serif\">Verdana</option>" + ("\r\n" + "             </select>"))))))))))))))));
                }

                // Font Size
                if (blnFontSize)
                {
                    sb.AppendLine(("\r\n" + ("             <select id=\"selectFontSize\" name=\"selectFontSize\" onChange=\"FormatText(\'fontsize\', selec" +
                        "tFontSize.options[selectFontSize.selectedIndex].value); document.frmAddMessage.selectFontSize.option" +
                        "s[0].selected = true;\">" + ("\r\n" + ("              <option value=\"0\" selected>-- "
                                    + (Variables.Forum.strTxtFontSizes + (" --</option>" + ("\r\n" + ("              <option value=\"1\">1</option>" + ("\r\n" + ("              <option value=\"2\">2</option>" + ("\r\n" + ("              <option value=\"3\">3</option>" + ("\r\n" + ("              <option value=\"4\">4</option>" + ("\r\n" + ("              <option value=\"5\">5</option>" + ("\r\n" + ("              <option value=\"6\">6</option>" + ("\r\n" + ("              <option value=\"7\">7</option>" + ("\r\n" + "             </select>"))))))))))))))))))))));
                }

                if (blnBold)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_bold.gif\" align=\"middle\" title=\""
                                    + (Variables.Forum.strTxtBold + "\" onClick=\"FormatText(\'bold\', \'\')\" style=\"cursor: pointer;\">"))))));
                }

                if (blnItalic)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_italic.gif\"  align=\"middle\" title=\""
                                    + (Variables.Forum.strTxtItalic + "\" onClick=\"FormatText(\'italic\', \'\')\" style=\"cursor: pointer;\"> "))))));
                }

                if (blnUnderline)
                {
                    sb.AppendLine(("\r\n" + ("             <img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_underline.gif\" align=\"middle\" title=\""
                                    + (Variables.Forum.strTxtUnderline + "\" onClick=\"FormatText(\'underline\', \'\')\" style=\"cursor: pointer;\">  "))))));
                }

                if (blnTextColour)
                {
                    sb.AppendLine(("\r\n" + ("             <img id=\"forecolor\" src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_colour_pallete.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtTextColour + "\" onClick=\"FormatText(\'forecolor\', \'\')\" style=\"cursor: pointer;\">"))))));
                }

                if ((((RTEenabled == "winIE")
                            || (RTEenabled == "winIE5"))
                            && blnTextBackgroundColour))
                {
                    sb.AppendLine(("\r\n" + ("             <img id=\"backcolor\" src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_fill.gif\" align=\"middle\" border=\"0\" title=\""
                                    + (Variables.Forum.strTxtBackgroundColour + "\" onClick=\"FormatText(\'backcolor\', \'\')\" style=\"cursor: pointer;\"> "))))));
                }

                if (Variables.Forum.blnImageUpload)
                {
                    sb.AppendLine(("\r\n" + ("                <a href=\"javascript:openWin(\'upload_images.aspx?MSG=RTE&FID="
                                    + (Variables.Forum.intForumID + ("\',\'images\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=0,width=400,height=150\')\">" +
                                    "<img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_image_upload.gif\" align=\"middle\" alt=\""
                                    + (Variables.Forum.strTxtImageUpload + "\" border=\"0\" /></a>"))))))));
                }

                if (Variables.Forum.blnAttachments)
                {
                    sb.AppendLine(("\r\n" + ("                <a href=\"javascript:openWin(\'upload_files.aspx?MSG=RTE&FID="
                                    + (Variables.Forum.intForumID + ("\',\'files\',\'toolbar=0,location=0,status=0,menubar=0,scrollbars=0,resizable=0,width=400,height=150\')\"><" +
                                    "img src=\""
                                    + (Variables.Forum.strImagePath + ("post_button_file_upload.gif\" align=\"middle\" alt=\""
                                    + (Variables.Forum.strTxtFileUpload + "\" border=\"0\" /></a>"))))))));
                }

                sb.AppendLine("              </span>");
                return sb.ToString();
            }

        }
    }
}