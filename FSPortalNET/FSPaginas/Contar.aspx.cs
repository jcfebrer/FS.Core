// <fileheader>
// <copyright file="contar.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: contar.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.Data;
using FSPortal;
using FSLibrary;
using FSDatabase;
using FSNetwork;

namespace FSPaginas
{
    public class Contar : BasePage
    {
        /// <summary>
        ///     Carga de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(Object sender, EventArgs e)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            if (db.TableExists(Variables.App.prefijoTablas + "Stats"))
            {
                Log();
            }
            Response.Redirect(Variables.App.directorioPortal + "imagenes/edit.gif", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        public long GetIdOs(string sName)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            long lIdOs;
            switch (sName)
            {
                case "Win95":
                    lIdOs = 2;
                    break;
                case "Win98":
                    lIdOs = 3;
                    break;
                case "WinNT":
                    lIdOs = 4;
                    break;
                case "Win2K":
                    lIdOs = 5;
                    break;
                case "Mac":
                    lIdOs = 6;
                    break;
                case "Linux":
                    lIdOs = 7;
                    break;
                case "WinME":
                    lIdOs = 8;
                    break;
                default:
                    lIdOs = 1;
                    break;
            }

            string sSql = "UPDATE " + Variables.App.prefijoTablas + "StatsOSes SET Total=Total+1 WHERE OsID = " + lIdOs;
            db.ExecuteNonQuery(sSql);

            return lIdOs;
        }


        public long GetIdColor(string sName)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            long lIdColor;
            switch (sName)
            {
                case "8":
                    lIdColor = 2;
                    break;
                case "16":
                    lIdColor = 3;
                    break;
                case "24":
                    lIdColor = 4;
                    break;
                case "32":
                    lIdColor = 5;
                    break;
                default:
                    lIdColor = 1;
                    break;
            }

            string sSql = "UPDATE " + Variables.App.prefijoTablas + "StatsColors SET Total=Total+1 WHERE ColorID = " + lIdColor;
            db.ExecuteNonQuery(sSql);

            return lIdColor;
        }


        public long GetIdBrowser(string sName)
        {
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            string sSql = "SELECT BrowserID, BrowserName, Total FROM " + Variables.App.prefijoTablas +
                          "StatsBrowsers WHERE BrowserName = '" + sName + "'";
            long l;

            DataTable dt = db.Execute(sSql);
            if (dt.Rows.Count == 0)
            {
                sSql = "INSERT INTO " + Variables.App.prefijoTablas + "StatsBrowsers (BrowserName,Total) VALUES ('" +
                       TextUtil.Left(sName, 100) + "',0)";
                db.ExecuteNonQuery(sSql);
                l = NumberUtils.NumberLong(db.GetIdentity());

                return l;
            }

            db.ExecuteNonQuery("UPDATE " + Variables.App.prefijoTablas + "StatsBrowsers SET Total=Total+1 WHERE BrowserName = '" +
                               sName + "'");

            l = long.Parse(dt.Rows[0]["BrowserID"].ToString());

            return l;
        }


        public long GetIdPath(string sName)
        {
            string sSql = "SELECT PathID, PathName, Total FROM " + Variables.App.prefijoTablas + "StatsPaths WHERE PathName = '" +
                          TextUtil.Substring(sName, 0, 250) + "'";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            long l;

            DataTable dt = db.Execute(sSql);
            if (dt.Rows.Count == 0)
            {
                sSql = "INSERT INTO " + Variables.App.prefijoTablas + "StatsPaths (PathName,Total) VALUES ('" +
                       TextUtil.Substring(sName, 0, 250) + "',0)";
                db.ExecuteNonQuery(sSql);
                l = NumberUtils.NumberLong(db.GetIdentity());

                return l;
            }

            db.ExecuteNonQuery("UPDATE " + Variables.App.prefijoTablas + "StatsPaths SET Total=Total+1 WHERE PathName = '" +
                               TextUtil.Substring(sName, 0, 250) + "'");

            l = long.Parse(dt.Rows[0]["PathID"].ToString());

            return l;
        }


        public long GetIdRef(string sName)
        {
            string sSql = "SELECT RefID, RefName, Total FROM " + Variables.App.prefijoTablas + "StatsRefs WHERE RefName = '" +
                          TextUtil.Substring(sName, 0, 250) + "'";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            long l;

            DataTable dt = db.Execute(sSql);
            if (dt.Rows.Count == 0)
            {
                sSql = "INSERT INTO " + Variables.App.prefijoTablas + "StatsRefs (RefName,Total) VALUES ('" +
                       TextUtil.Substring(sName, 0, 250) + "',0)";
                db.ExecuteNonQuery(sSql);
                l = NumberUtils.NumberLong(db.GetIdentity());

                return l;
            }

            db.ExecuteNonQuery("UPDATE " + Variables.App.prefijoTablas + "StatsRefs SET Total=Total+1 WHERE RefName = '" +
                               TextUtil.Substring(sName, 0, 250) + "'");

            l = long.Parse(dt.Rows[0]["RefID"].ToString());

            return l;
        }


        public long GetIdRes(string sName)
        {
            string sSql = "SELECT ResID, ResName, Total FROM " + Variables.App.prefijoTablas +
                          "StatsResolutions WHERE ResName = '" + sName + "'";
			BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            long l;

            DataTable dt = db.Execute(sSql);
            if (dt.Rows.Count == 0)
            {
                sSql = "INSERT INTO " + Variables.App.prefijoTablas + "StatsResolutions (ResName,Total) VALUES ('" + sName +
                       "',0)";
                db.ExecuteNonQuery(sSql);
                l = NumberUtils.NumberLong(db.GetIdentity());

                return l;
            }

            db.ExecuteNonQuery("UPDATE " + Variables.App.prefijoTablas + "StatsResolutions SET Total=Total+1 WHERE ResName = '" +
                               sName + "'");

            l = long.Parse(dt.Rows[0]["ResID"].ToString());

            return l;
        }


        public string StripParameter(string sPath)
        {
            if (sPath == null) return "";

            int iPlace = sPath.IndexOf("?") + 1;
            string sBuffer = iPlace > 0 ? TextUtil.Substring(sPath, 0, iPlace - 1) : sPath;
            
            return sBuffer;
        }


        public string StripProtocol(string sPath)
        {
            int iPlace = sPath.IndexOf("://") + 1;
            string sBuffer = iPlace > 0 ? TextUtil.Substring(sPath, sPath.Length - TextUtil.Length(sPath) - (3 + iPlace - 1)) : sPath;

            if (TextUtil.Substring(sBuffer, 0, 4) == "www.")
            {
                sBuffer = TextUtil.Substring(sBuffer, sBuffer.Length - TextUtil.Length(sBuffer) - 4);
            }
            
            return sBuffer;
        }


        public void Log()
        {
            bool bExit = false;

            string sResolution = Request["w"] + "x" + Request["h"];
            string sColor = Request["c"];
            string sPath = Request["u"];
            string sReferer = Request["r"];
            //string sFontSmoothing = Request["fs"];

            string sIp = Http.IpAddress();
            string sU = Request.ServerVariables["HTTP_USER_AGENT"];

            if (Variables.App.FilterIps != null)
            {
                Array aIps = Variables.App.FilterIps.Split(',');
                foreach (string sFilterIp in aIps)
                {
                    if (sFilterIp == sIp)
                    {
                        bExit = true;
                    }
                }
            }

            if (bExit)
            {
                return;
            }

            if (sResolution == "x")
            {
                sResolution = "(unknown)";
            }

            if (sReferer == "")
            {
                sReferer = Request.ServerVariables["HTTP_REFERER"];
            }
            if (sReferer == "")
            {
                sReferer = "...";
            }

            if (!Variables.App.RefThisServer)
            {
                if (StripParameter(sReferer).IndexOf(Request.ServerVariables["HTTP_HOST"]) + 1 > 0)
                {
                    sReferer = "...";
                }
            }

            if (Variables.App.StripRefFile)
            {
                int iPlace = TextUtil.LastIndexOf(sReferer, "/");
                if (Convert.ToBoolean(iPlace))
                {
                    sReferer = TextUtil.Substring(sReferer, 0, iPlace - 1);
                }
            }

            if (Variables.App.StripPathParameters)
            {
                sPath = StripParameter(sPath);
            }

            if (Variables.App.StripPathProtocol)
            {
                sPath = StripProtocol(sPath);
            }

            if (Variables.App.StripRefParameters)
            {
                sReferer = StripParameter(sReferer);
            }

            if (Variables.App.StripRefProtocol)
            {
                sReferer = StripProtocol(sReferer);
            }

            if (sPath == "")
            {
                sPath = "/";
            }

            string sOs = "";

            if (sU.IndexOf("98") + 1 > 0)
            {
                sOs = "Win98";
            }

            if (sU.IndexOf("95") + 1 > 0)
            {
                sOs = "Win95";
            }

            if (sU.IndexOf("Win 9x") + 1 > 0)
            {
                sOs = "WinME";
            }

            if (sU.IndexOf("NT") + 1 > 0)
            {
                sOs = "WinNT";
            }

            if (sU.IndexOf("NT 5") + 1 > 0)
            {
                sOs = "Win2K";
            }

            if (sU.IndexOf("Linux") + 1 > 0)
            {
                sOs = "Linux";
            }

            if (sU.IndexOf("Mac") + 1 > 0)
            {
                sOs = "Mac";
            }

            string sBrowserType = Request["b"];
            int p1;
            int p2;
            string sBrowser;

            switch (sBrowserType)
            {
                case "MSIE":
                    p1 = sU.IndexOf(";") + 1;
                    p2 = sU.IndexOf(";", p1 + 1 - 1) + 1;
                    sBrowser = TextUtil.Substring(sU, p1 + 2, (p2 - p1) - 2);
                    break;
                case "NS":
                    sBrowser = "NS " + TextUtil.Substring(sU, 9, 3);

                    if (sU.IndexOf("Netscape") + 1 > 0)
                    {
                        int i = sU.IndexOf("/", 20 - 1) + 1;
                        sBrowser = "NS " + TextUtil.Substring(sU, sU.Length - TextUtil.Length(sU) - i);
                    }
                    break;
                default:
                    if (sU.IndexOf("MSIE") + 1 > 0)
                    {
                        p1 = sU.IndexOf("MSIE") + 1;
                        p2 = sU.IndexOf(";", p1 + 1 - 1) + 1;
                        sBrowser = TextUtil.Substring(sU, p1, (p2 - p1) - 1);
                    }
                    else
                    {
                        sBrowser = sU;
                    }
                    break;
            }


            long lIdOs = GetIdOs(sOs);
            long lIdColor = GetIdColor(sColor);
            long lIdBrowser = GetIdBrowser(sBrowser);
            long lIdPath = GetIdPath(sPath);
            long lIdRef = GetIdRef(sReferer);
            long lIdRes = GetIdRes(sResolution);

            string[] sArrFields = new string[10];
            string[] sArrData = new string[10];
            Type[] sArrType = new Type[10];

            sArrFields[0] = "OsID";
            sArrFields[1] = "ColorID";
            sArrFields[2] = "BrowserID";
            sArrFields[3] = "PathID";
            sArrFields[4] = "RefID";
            sArrFields[5] = "ResID";
            sArrFields[6] = "Date";
            sArrFields[7] = "Time";
            sArrFields[8] = "IP";
            sArrFields[9] = "userID";

            sArrType[0] = typeof (int);
            sArrType[1] = typeof (int);
            sArrType[2] = typeof (int);
            sArrType[3] = typeof (int);
            sArrType[4] = typeof (int);
            sArrType[5] = typeof (int);
			sArrType[6] = typeof (System.DateTime);
            sArrType[7] = typeof (string);
            sArrType[8] = typeof (string);
            sArrType[9] = typeof (int);

            sArrData[0] = Functions.Valor(lIdOs);
            sArrData[1] = Functions.Valor(lIdColor);
            sArrData[2] = Functions.Valor(lIdBrowser);
            sArrData[3] = Functions.Valor(lIdPath);
            sArrData[4] = Functions.Valor(lIdRef);
            sArrData[5] = Functions.Valor(lIdRes);
			sArrData[6] = FSLibrary.DateTimeUtil.ShortDate(System.DateTime.Now);
			sArrData[7] = FSLibrary.DateTimeUtil.LongDate(System.DateTime.Now);
            sArrData[8] = sIp;

            if (Variables.User.UsuarioId != 0)
            {
                sArrData[9] = Variables.User.UsuarioId.ToString();
            }
            else
            {
                sArrData[9] = Functions.Valor(2);
            }

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            try
            {
                db.ExecuteNonQuery(db.InsertSql("Stats", sArrFields, sArrData, sArrType, Variables.User.UsuarioId));
            }
            catch (System.Exception e)
            {
                Response.Write(e.ToString());
            }
        }
    }
}