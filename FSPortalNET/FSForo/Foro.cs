// <fileheader>
// <copyright file="Foro.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: Foro.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>


using System;
using System.Data;
using FSLibrary;
using FSPortal;
using FSDatabase;

namespace FSOpenForum
{
    public class Foro
    {
        public const int cPageSize = 10;

        public const int cSummaryLen = 80;

        public const string cForumBgColor = "#ffffff";
        public const string cTitleBgColor = "#fdf5e6";
        public const string cSubjectBgColor = "#fffafa";
        public const string cTitleFocusBgColor = "#e6e6fa";
        public const string cSubjectFocusBgColor = "#f5f5f5";
        public const string cForumHeaderBgColor = "#e6e6fa";
        public const string cNavBarBgColor = "#efefef";

        public const int cForumCellSpacing = 0;
        public const int cForumCellPadding = 2;
        public const string ofStrErrNoSuchForum = "No existe el foro ForumID = {0}";
        public const string ofStrErrNoMessages = "No existen mensajes en el foro '{0}'";
        public const string ofStrErrUnknown = "Error desconocido";
        public const string ofErrNoSuchMessage = "No existe un mensaje MsgID = {0}";

        public const string ofStrReply = "responder este mensaje";

        public const string ofStrFirst = "primero";
        public const string ofStrPrev = "anterior";
        public const string ofStrNext = "siguiente";
        public const string ofStrLast = "último";

        public const string ofStrForum = "foro";
        public const string ofStrSendNew = "Escribir un nuevo mensaje";
        public const string ofStrMessageCount = "Total {0} Mensajes";

        public const string ofStrRefresh = "recargar / actualizar";
        public const string ofStrModeNormal = "completo";
        public const string ofStrModePreview = "resúmen";
        public const string ofStrModeTitles = "títulos";

        public const string ofStrPost = " Grabar ";
        public const string ofStrPostTitle = "Asunto";
        public const string ofStrPostBody = "Mensaje";
        public const string ofStrUser = "Su nombre";

        public const string ofStrRE = "RE: ";
        public const string ofStrOriginalMsg = "-------- Mensaje original --------";

        public const int ofModeNormal = 0;
        public const int ofModePreview = 1;
        public const int ofModeOnlyTitles = 2;

        public const int ofActDisplay = 0;
        public const int ofActFocus = 1;
        public const int ofActNewMsg = 2;
        public const int ofActReply = 3;
        public const int ofActPost = 4;

        public const int ofSuccess = 0;
        public const int ofErrNoSuchForum = 1;
        public const int ofErrNoMessages = 2;

        public const int ofFldUMsgID = 0;
        public const int ofFldUParentID = 1;
        public const int ofFldUAuthor = 2;
        public const int ofFldUTitle = 3;
        public const int ofFldUSubject = 4;
        public const int ofFldUDate = 5;
        public const int ofFldUNotify = 6;

        public const int ofFldMsgID = 0;
        public const int ofFldIndent = 1;
        public const int ofFldAuthor = 2;
        public const int ofFldTitle = 3;
        public const int ofFldSubject = 4;
        public const int ofFldDate = 5;
        public const int ofFldNotify = 6;


        public int CurrentPage;
        public string ForumTitle;
        public int MessageCount;
        public int NumMsgEnd;
        public int NumMsgStart;
        public int PageSize;
        public int TotalPages;

        private DataTable aSorted;
        private int iForumID;
        public Array ofStrArrLongMonths;
        public Array ofStrArrShortMonths;

        public string ofStrLongMonths =
            ",Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre";

        public string ofStrShortMonths = ",Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic";

        public Foro()
        {
            ofStrArrShortMonths = ofStrShortMonths.Split(",".Split("".ToCharArray()), StringSplitOptions.None);
            ofStrArrLongMonths = ofStrLongMonths.Split(",".Split("".ToCharArray()), StringSplitOptions.None);
        }


        public int OpenForum(int ForumID)
        {
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            int iRet = 0;

            iForumID = ForumID;

            string sSQL = "SELECT Foros.Title, COUNT(MsgID) AS Total " + "FROM " + Variables.App.prefijoTablas + "Foros " +
                          "LEFT JOIN MensajesForo " + "ON (Foros.ForumID = MensajesForo.ForumID) " +
                          "WHERE Foros.ForumID = " + ForumID + " " + "GROUP BY Foros.Title";

            DataTable dtForum = db.Execute(sSQL);

            if (dtForum.Rows.Count == 0)
            {
                iRet = ofErrNoSuchForum;
            }
            else
            {
                ForumTitle = Functions.Valor(dtForum.Rows[0]["Title"]);
                MessageCount = NumberUtils.NumberInt(dtForum.Rows[0]["Total"]);
                if (MessageCount == 0)
                {
                    iRet = ofErrNoMessages;
                }
                else
                {
                    iRet = ofSuccess;
                }
            }

            return iRet;
        }


        private void SortMsgs(DataTable aMsgs, int iParentID, int iLevel)
        {
            int i = 0;
            int iRec = 0;

            for (i = 0; i <= aMsgs.Rows.Count - 1; i++)
            {
                if (Convert.ToDouble(aMsgs.Rows[i][ofFldUParentID]) == iParentID)
                {
                    DataRow dt = null;
                    dt = aSorted.NewRow();

                    dt[ofFldMsgID] = aMsgs.Rows[i][ofFldUMsgID];
                    dt[ofFldIndent] = iLevel;
                    dt[ofFldAuthor] = aMsgs.Rows[i][ofFldUAuthor];
                    dt[ofFldTitle] = aMsgs.Rows[i][ofFldUTitle];
                    dt[ofFldSubject] = aMsgs.Rows[i][ofFldUSubject];
                    dt[ofFldDate] = aMsgs.Rows[i][ofFldUDate];
                    dt[ofFldNotify] = aMsgs.Rows[i][ofFldUNotify];

                    aSorted.Rows.Add(dt);

                    iRec = iRec + 1;

                    SortMsgs(aMsgs, NumberUtils.NumberInt(aMsgs.Rows[i][ofFldUMsgID]), iLevel + 1);
                }
            }
        }


        public DataTable GetMessages()
        {
            string sSQL = "SELECT MsgID, ParentID, Author, Title, Body, Date, Notify " + "FROM " + Variables.App.prefijoTablas +
                          "MensajesForo " + "WHERE ForumID = " + iForumID + " ORDER BY MsgID DESC";

            DataTable dtF = null;
            int iRecs = 0;
            int iFields = 0;
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            dtF = db.Execute(sSQL);

            iRecs = dtF.Rows.Count;

            iFields = dtF.Columns.Count;


            aSorted = new DataTable();

            for (int f = 0; f <= dtF.Columns.Count - 1; f++)
            {
                DataColumn c = new DataColumn(dtF.Columns[f].ColumnName, dtF.Columns[f].DataType);
                aSorted.Columns.Add(c);
            }
            SortMsgs(dtF, 0, 0);

            TotalPages = (iRecs/cPageSize) + 1;
            if (TotalPages < 1)
            {
                TotalPages = 1;
            }

            if (NumberUtils.IsNumeric(CurrentPage.ToString()))
            {
                if (CurrentPage < 1)
                {
                    CurrentPage = 1;
                }
                if (CurrentPage > TotalPages)
                {
                    CurrentPage = TotalPages;
                }
            }
            else
            {
                CurrentPage = 1;
            }

            NumMsgStart = (CurrentPage - 1)*cPageSize;
            NumMsgEnd = NumMsgStart + (cPageSize < MessageCount ? (NumMsgStart + cPageSize - 1) : (MessageCount - 1));

            return aSorted;
        }


        public int GetMessage(int iMsgID, ref string sMsgTitle, ref string sMsgBody, ref string sMsgAuthor,
            ref string sMsgDate)
        {
            string sSQL = "SELECT Title,body,Author,Date FROM " + Variables.App.prefijoTablas + "MensajesForo WHERE MsgID = " +
                          iMsgID;
            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);

            int iRet = 0;
            DataTable dtForum = db.Execute(sSQL);

            if (dtForum.Rows.Count == 0)
            {
                iRet = ofErrNoSuchForum;
            }
            else
            {
                sMsgTitle = Functions.Valor(dtForum.Rows[0]["Title"]);
                sMsgBody = Functions.Valor(dtForum.Rows[0]["Body"]);
                sMsgAuthor = Functions.Valor(dtForum.Rows[0]["Author"]);
                sMsgDate = Functions.Valor(dtForum.Rows[0]["Date"]);
            }

            return iRet;
        }


		public void SaveMessage(int iForumID, int iParentID, string sAuthor, string sTitle, string sBody, System.DateTime dDate)
        {
            Register r = new Register();

            r.Add(new Field("ForumID", iForumID.ToString(), typeof (int)));
            r.Add(new Field("ParentID", iParentID.ToString(), typeof (int)));
            r.Add(new Field("Author", sAuthor, typeof (string)));
            r.Add(new Field("Title", sTitle, typeof (string)));
            r.Add(new Field("Body", sBody, typeof (string)));
			r.Add(new Field("Date", dDate.ToString(), typeof (System.DateTime)));

            BdUtils db = new BdUtils(Variables.App.connectionString, Variables.App.providerName);
            db.ExecuteNonQuery(db.InsertSql("MensajesForo", r, Variables.User.UsuarioId));
        }
    }
}