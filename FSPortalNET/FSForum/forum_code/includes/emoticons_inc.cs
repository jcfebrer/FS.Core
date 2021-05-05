// <fileheader>
// <copyright file="emoticons_inc.ascx.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: forum\includes\emoticons_inc.ascx.cs
//     Copyright [c] 2003-2007 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace FSForum
{
    namespace Includes
    {
        public class emoticons_inc
        {
            public static void Load()
            {
                //Dimension variables

                Variables.Forum.saryEmoticons[1, 1] = "Smile";            //Emoticon Name
                Variables.Forum.saryEmoticons[1, 2] = "[:)]";        //Forum code
                Variables.Forum.saryEmoticons[1, 3] = "smileys/smiley1.gif";  //URL/path to smiley

                Variables.Forum.saryEmoticons[2, 1] = "Tongue";
                Variables.Forum.saryEmoticons[2, 2] = "[:P]";
                Variables.Forum.saryEmoticons[2, 3] = "smileys/smiley17.gif";

                Variables.Forum.saryEmoticons[3, 1] = "Wink";
                Variables.Forum.saryEmoticons[3, 2] = "[;)]";
                Variables.Forum.saryEmoticons[3, 3] = "smileys/smiley2.gif";

                Variables.Forum.saryEmoticons[4, 1] = "Cry";
                Variables.Forum.saryEmoticons[4, 2] = "[:^)]";
                Variables.Forum.saryEmoticons[4, 3] = "smileys/smiley19.gif";

                Variables.Forum.saryEmoticons[5, 1] = "Big smile";
                Variables.Forum.saryEmoticons[5, 2] = "[:D]";
                Variables.Forum.saryEmoticons[5, 3] = "smileys/smiley4.gif";

                Variables.Forum.saryEmoticons[6, 1] = "LOL";
                Variables.Forum.saryEmoticons[6, 2] = "[LOL]";
                Variables.Forum.saryEmoticons[6, 3] = "smileys/smiley36.gif";

                Variables.Forum.saryEmoticons[7, 1] = "Dead";
                Variables.Forum.saryEmoticons[7, 2] = "[xx)]";
                Variables.Forum.saryEmoticons[7, 3] = "smileys/smiley11.gif";

                Variables.Forum.saryEmoticons[8, 1] = "Embarrassed";
                Variables.Forum.saryEmoticons[8, 2] = "[:$]";
                Variables.Forum.saryEmoticons[8, 3] = "smileys/smiley9.gif";

                Variables.Forum.saryEmoticons[9, 1] = "Confused";
                Variables.Forum.saryEmoticons[9, 2] = "[:s]";
                Variables.Forum.saryEmoticons[9, 3] = "smileys/smiley5.gif";

                Variables.Forum.saryEmoticons[10, 1] = "Clap";
                Variables.Forum.saryEmoticons[10, 2] = "[=D>]";
                Variables.Forum.saryEmoticons[10, 3] = "smileys/smiley32.gif";

                Variables.Forum.saryEmoticons[11, 1] = "Angry";
                Variables.Forum.saryEmoticons[11, 2] = "[:x]";
                Variables.Forum.saryEmoticons[11, 3] = "smileys/smiley7.gif";

                Variables.Forum.saryEmoticons[12, 1] = "Ouch";
                Variables.Forum.saryEmoticons[12, 2] = "[8[]";
                Variables.Forum.saryEmoticons[12, 3] = "smileys/smiley18.gif";

                Variables.Forum.saryEmoticons[13, 1] = "Star";
                Variables.Forum.saryEmoticons[13, 2] = "[:*:]";
                Variables.Forum.saryEmoticons[13, 3] = "smileys/smiley10.gif";

                Variables.Forum.saryEmoticons[14, 1] = "Shocked";
                Variables.Forum.saryEmoticons[14, 2] = "[:o]";
                Variables.Forum.saryEmoticons[14, 3] = "smileys/smiley3.gif";

                Variables.Forum.saryEmoticons[15, 1] = "Sleepy";
                Variables.Forum.saryEmoticons[15, 2] = "[|)]";
                Variables.Forum.saryEmoticons[15, 3] = "smileys/smiley12.gif";

                Variables.Forum.saryEmoticons[16, 1] = "Unhappy";
                Variables.Forum.saryEmoticons[16, 2] = "[:(]";
                Variables.Forum.saryEmoticons[16, 3] = "smileys/smiley6.gif";

                Variables.Forum.saryEmoticons[17, 1] = "Approve";
                Variables.Forum.saryEmoticons[17, 2] = "[:^:]";
                Variables.Forum.saryEmoticons[17, 3] = "smileys/smiley14.gif";

                Variables.Forum.saryEmoticons[18, 1] = "Cool";
                Variables.Forum.saryEmoticons[18, 2] = "[8D]";
                Variables.Forum.saryEmoticons[18, 3] = "smileys/smiley16.gif";

                Variables.Forum.saryEmoticons[19, 1] = "Clown";
                Variables.Forum.saryEmoticons[19, 2] = "[:o)]";
                Variables.Forum.saryEmoticons[19, 3] = "smileys/smiley8.gif";

                Variables.Forum.saryEmoticons[20, 1] = "Evil Smile";
                Variables.Forum.saryEmoticons[20, 2] = "[}:)]";
                Variables.Forum.saryEmoticons[20, 3] = "smileys/smiley15.gif";

                Variables.Forum.saryEmoticons[21, 1] = "Disapprove";
                Variables.Forum.saryEmoticons[21, 2] = "[:V:]";
                Variables.Forum.saryEmoticons[21, 3] = "smileys/smiley13.gif";

                Variables.Forum.saryEmoticons[22, 1] = "Stern Smile";
                Variables.Forum.saryEmoticons[22, 2] = "[:|]";
                Variables.Forum.saryEmoticons[22, 3] = "smileys/smiley22.gif";

                Variables.Forum.saryEmoticons[23, 1] = "Thumbs Up";
                Variables.Forum.saryEmoticons[23, 2] = "[:Y:]";
                Variables.Forum.saryEmoticons[23, 3] = "smileys/smiley20.gif";

                Variables.Forum.saryEmoticons[24, 1] = "Thumbs Down";
                Variables.Forum.saryEmoticons[24, 2] = "[:N:]";
                Variables.Forum.saryEmoticons[24, 3] = "smileys/smiley21.gif";

                Variables.Forum.saryEmoticons[25, 1] = "Geek";
                Variables.Forum.saryEmoticons[25, 2] = "[:-B]";
                Variables.Forum.saryEmoticons[25, 3] = "smileys/smiley23.gif";

                Variables.Forum.saryEmoticons[26, 1] = "Ermm";
                Variables.Forum.saryEmoticons[26, 2] = "[:)]";
                Variables.Forum.saryEmoticons[26, 3] = "smileys/smiley24.gif";

                Variables.Forum.saryEmoticons[27, 1] = "Question";
                Variables.Forum.saryEmoticons[27, 2] = "[:?:]";
                Variables.Forum.saryEmoticons[27, 3] = "smileys/smiley25.gif";

                Variables.Forum.saryEmoticons[28, 1] = "Pinch";
                Variables.Forum.saryEmoticons[28, 2] = "[><]";
                Variables.Forum.saryEmoticons[28, 3] = "smileys/smiley26.gif";

                Variables.Forum.saryEmoticons[29, 1] = "Heart";
                Variables.Forum.saryEmoticons[29, 2] = "[L]";
                Variables.Forum.saryEmoticons[29, 3] = "smileys/smiley27.gif";

                Variables.Forum.saryEmoticons[30, 1] = "Broken Heart";
                Variables.Forum.saryEmoticons[30, 2] = "[%(]";
                Variables.Forum.saryEmoticons[30, 3] = "smileys/smiley28.gif";

                Variables.Forum.saryEmoticons[31, 1] = "Wacko";
                Variables.Forum.saryEmoticons[31, 2] = "[8-}]";
                Variables.Forum.saryEmoticons[31, 3] = "smileys/smiley29.gif";

                Variables.Forum.saryEmoticons[32, 1] = "Pig";
                Variables.Forum.saryEmoticons[32, 2] = "[:@)]";
                Variables.Forum.saryEmoticons[32, 3] = "smileys/smiley30.gif";

                Variables.Forum.saryEmoticons[33, 1] = "Hug";
                Variables.Forum.saryEmoticons[33, 2] = "[>:D<]";
                Variables.Forum.saryEmoticons[33, 3] = "smileys/smiley31.gif";

                Variables.Forum.saryEmoticons[34, 1] = "Censored";
                Variables.Forum.saryEmoticons[34, 2] = "[XXX]";
                Variables.Forum.saryEmoticons[34, 3] = "smileys/smiley35.gif";

                Variables.Forum.saryEmoticons[35, 1] = "Ying Yang";
                Variables.Forum.saryEmoticons[35, 2] = "[%]";
                Variables.Forum.saryEmoticons[35, 3] = "smileys/smiley33.gif";

                Variables.Forum.saryEmoticons[36, 1] = "Nuke";
                Variables.Forum.saryEmoticons[36, 2] = "[!]";
                Variables.Forum.saryEmoticons[36, 3] = "smileys/smiley34.gif";

                //If you add more emoticons don't forget to increase the number in the Dim statement at the top!
            }
        }
    }
}