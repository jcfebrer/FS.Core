// <fileheader>
// <copyright file="FuncionesFecha.cs" company="Febrer Software">
//     Fecha: 30/11/2007
//     Path: clsFuncionesFecha.cs
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
using FSPortal;

namespace FSForum
{
    public static class FuncionesFecha
    {
        public static string[] saryDateTimeData;

        public static void Init()
        {
            int intLoopCounter = 0;
            string strSQL = null;

            FSDatabase.BdUtils db = new FSDatabase.BdUtils("FSForum");


            if (FSDatabase.Utils.BDType == FSDatabase.Utils.TypeBd.SQLServer)
            {
                strSQL = "Execute " + FSForum.Variables.Forum.strDbProc + "TimeAndDateSettings";
            }
            else
            {
                strSQL = "SELECT " + FSForum.Variables.Forum.strDbTable + "DateTimeFormat.* FROM " + FSForum.Variables.Forum.strDbTable + "DateTimeFormat;";
            }

            DataTable dtDateTimeFormat = db.Execute(strSQL);

            if (dtDateTimeFormat.Rows.Count > 0)
            {
                saryDateTimeData = new string[18];

                if (FSForum.Variables.Forum.strDateFormat != null)
                {
                    saryDateTimeData[0] = FSForum.Variables.Forum.strDateFormat;
                }
                else
                {
                    saryDateTimeData[0] = Functions.Valor(dtDateTimeFormat.Rows[0]["Date_format"]);
                }
                saryDateTimeData[1] = Functions.Valor(dtDateTimeFormat.Rows[0]["Year_format"]);
                saryDateTimeData[2] = Functions.Valor(dtDateTimeFormat.Rows[0]["Seporator"]);
                for (intLoopCounter = 1; intLoopCounter <= 12; intLoopCounter++)
                {
                    saryDateTimeData[(intLoopCounter + 2)] = Functions.Valor(dtDateTimeFormat.Rows[0]["Month" + (intLoopCounter)]);
                }
                saryDateTimeData[15] = Functions.Valor(dtDateTimeFormat.Rows[0]["Time_format"]);
                saryDateTimeData[16] = Functions.Valor(dtDateTimeFormat.Rows[0]["am"]);
                saryDateTimeData[17] = Functions.Valor(dtDateTimeFormat.Rows[0]["pm"]);
            }
        }




        public static string DateFormat(System.DateTime dtmDate, string[] saryDateTimeData)
        {

            int intDay = 0;
            int intMonth = 0;
            string strMonth = null;
            int intYear = 0;
            System.DateTime dtmTempDate = System.DateTime.MinValue;
            string df = "";


            if (saryDateTimeData == null)
            {

                df = dtmDate.ToShortDateString();

            }
            else
            {
                if (FSForum.Variables.Forum.strTimeOffSet == "+")
                {
                    dtmTempDate = dtmDate.AddHours(FSForum.Variables.Forum.intTimeOffSet);
                }
                else if (FSForum.Variables.Forum.strTimeOffSet == "-")
                {
                    dtmTempDate = dtmDate.AddHours(-FSForum.Variables.Forum.intTimeOffSet);
                }

                intDay = dtmTempDate.Day;
                intMonth = dtmTempDate.Month;
                intYear = dtmTempDate.Year;

                if (intDay < 10)
                {
                    intDay = NumberUtils.NumberInt(double.Parse("0" + intDay));
                }

                if (saryDateTimeData[1] == "short")
                {
                    intYear = Convert.ToInt32(TextUtil.Right(intYear.ToString(), 2));
                }

                strMonth = saryDateTimeData[(intMonth + 2)];

                if (!(FSForum.Variables.Forum.strDateFormat == ""))
                {
                    saryDateTimeData[0] = FSForum.Variables.Forum.strDateFormat;
                }

                switch (saryDateTimeData[0])
                {
                    case "dd/mm/yy":
                        df = intDay + saryDateTimeData[2] + strMonth + saryDateTimeData[2] + intYear;

                        break;
                    case "mm/dd/yy":
                        df = strMonth + saryDateTimeData[2] + intDay + saryDateTimeData[2] + intYear;

                        break;
                    case "yy/dd/mm":
                        df = intYear + saryDateTimeData[2] + intDay + saryDateTimeData[2] + strMonth;

                        break;
                    case "yy/mm/dd":
                        df = intYear + saryDateTimeData[2] + strMonth + saryDateTimeData[2] + intDay;
                        break;
                }

            }

            return df;

        }








        public static string TimeFormat(System.DateTime dtmTime, string[] saryDateTimeData)
        {

            int intHour = 0;
            int intMinute = 0;
            string strPeriod = "";
            System.DateTime dtmTempTime = System.DateTime.MinValue;
            string tf = "";
            

            if (saryDateTimeData == null)
            {

                tf = dtmTime.ToShortTimeString();

            }
            else
            {

                if (FSForum.Variables.Forum.strTimeOffSet == "+")
                {
                    dtmTempTime = dtmTime.AddHours(FSForum.Variables.Forum.intTimeOffSet);
                }
                else if (FSForum.Variables.Forum.strTimeOffSet == "-")
                {
                    dtmTempTime = dtmTime.AddHours(-FSForum.Variables.Forum.intTimeOffSet);
                }

                intHour = dtmTempTime.Hour;
                intMinute = dtmTempTime.Minute;

                if (int.Parse(saryDateTimeData[15]) == 12)
                {

                    if (intHour >= 12)
                    {
                        strPeriod = saryDateTimeData[17];
                    }
                    else
                    {
                        strPeriod = saryDateTimeData[16];
                    }


                    switch (intHour)
                    {
                        case 0:
                            intHour = 12;
                            break;
                        case 1:
                            intHour = 1;
                            break;
                        case 2:
                            intHour = 2;
                            break;
                        case 3:
                            intHour = 3;
                            break;
                        case 4:
                            intHour = 4;
                            break;
                        case 5:
                            intHour = 5;
                            break;
                        case 6:
                            intHour = 6;
                            break;
                        case 7:
                            intHour = 7;
                            break;
                        case 8:
                            intHour = 8;
                            break;
                        case 9:
                            intHour = 9;
                            break;
                        case 13:
                            intHour = 1;
                            break;
                        case 14:
                            intHour = 2;
                            break;
                        case 15:
                            intHour = 3;
                            break;
                        case 16:
                            intHour = 4;
                            break;
                        case 17:
                            intHour = 5;
                            break;
                        case 18:
                            intHour = 6;
                            break;
                        case 19:
                            intHour = 7;
                            break;
                        case 20:
                            intHour = 8;
                            break;
                        case 21:
                            intHour = 9;
                            break;
                        case 22:
                            intHour = 10;
                            break;
                        case 23:
                            intHour = 11;

                            break;
                    }


                }

                tf = TextUtil.AdZero(intHour.ToString(), 2) + ":" + TextUtil.AdZero(intMinute.ToString(), 2) + strPeriod;
            }

            return tf;
        }


        public static string DateTimeNum(string strElement)
        {

            string strDateElement = "";

            switch (strElement)
            {
                case "Year":
                    strDateElement = Functions.Valor(System.DateTime.Now.Year);
                    break;
                case "Month":
                    strDateElement = Functions.Valor(System.DateTime.Now.Month);
                    break;
                case "Day":
                    strDateElement = Functions.Valor(System.DateTime.Now.Day);
                    break;
                case "Hour":
                    strDateElement = Functions.Valor(System.DateTime.Now.Hour);
                    break;
                case "Minute":
                    strDateElement = Functions.Valor(System.DateTime.Now.Minute);
                    break;
                case "Second":
                    strDateElement = Functions.Valor(((int)(NumberUtils.NumberInt(System.DateTime.Now.Second))));
                    break;
            }


            if (double.Parse(strDateElement) < 10)
            {
                strDateElement = "0" + strDateElement;
            }

            return strDateElement;
        }

    }

}
