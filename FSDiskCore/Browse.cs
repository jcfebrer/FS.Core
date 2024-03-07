#region

using System;
using System.Runtime.InteropServices;
using System.Text;
using FSLibraryCore;

#endregion

namespace FSDiskCore
{
    public class Browse
    {
        #region '" Local Constants "' 

        private const int BFFM_INITIALIZED = 1;
        private const int BFFM_SELCHANGED = 2;
        private const int BFFM_VALIDATEFAILED = 3;
        private const int BFFM_ENABLEOK = 0X465;
        private const int BFFM_SETSELECTIONA = 0X466;
        private const int BFFM_SETSTATUSTEXT = 0X464;

        private const short BIF_EDITBOX = 0X10;
        private const short BIF_VALIDATE = 0X20;
        private const short BIF_STATUSTEXT = 0X4;
        private const short BIF_NEWDIALOGSTYLE = 0X40;
        private const short BIF_DONTGOBELOWDOMAIN = 0X2;

        private const short BIF_RETURNONLYFSDIRS = 0X1;
        private const short BIF_RETURNFSANCESTORS = 0X8;

        private const short BIF_BROWSEFORPRINTER = 0X2000;
        private const short BIF_BROWSEFORCOMPUTER = 0X1000;

        private const short BIF_BROWSEINCLUDEFILES = 0X4000;

        private const short MAX_PATH = 260;

        #endregion

        #region '" Local Variables "'

        private bool flgInit;
        private Win32API.BROWSEINFO stcBrowseInfo;

        #endregion

        #region '" Local Functions "' 

        private string DoBrowse(string StartPath)
        {
            var iprtSelectDir = new IntPtr();
            string strDirSelect = null;

            if (NewUI) stcBrowseInfo.ulFlags += BIF_NEWDIALOGSTYLE;
            if (ShowStatus) stcBrowseInfo.ulFlags += BIF_STATUSTEXT;

            flgInit = true;

            stcBrowseInfo.lParam = Marshal.StringToHGlobalAnsi(StartPath);

            int transTemp0 = MAX_PATH;
            stcBrowseInfo.pszDisplayName = string.Empty.PadLeft(transTemp0);

            iprtSelectDir = Win32API.SHBrowseForFolder(ref stcBrowseInfo);

            if (GetFSPath(iprtSelectDir) == "")
            {
                var transTemp1 = stcBrowseInfo.pszDisplayName;
                strDirSelect = @"\\" + transTemp1.Trim();
            }
            else
            {
                strDirSelect = GetFSPath(iprtSelectDir);
            }

            Win32API.CoTaskMemFree(iprtSelectDir);

            return strDirSelect;
        }


        private int BrowseCallbackProc(IntPtr hWnd, int uMsg, int lParam, int lpData)
        {
            if (uMsg == BFFM_INITIALIZED)
            {
                Win32API.SendMessage(hWnd, BFFM_SETSELECTIONA, 1, lpData);
                flgInit = false;
            }
            else if ((uMsg == BFFM_SELCHANGED) & !flgInit)
            {
                Win32API.SendMessage(hWnd, BFFM_SETSTATUSTEXT, 0, GetFSPath(new IntPtr(lParam)));
            }

            return 0;
        }


        private string GetFSPath(IntPtr pidl)
        {
            var strPath = new StringBuilder(MAX_PATH);

            if (pidl.Equals(IntPtr.Zero)) return string.Empty;

            if (Win32API.SHGetPathFromIDList(pidl, strPath) == 1) return strPath.ToString();

            return string.Empty;
        }

        #endregion

        #region '" Public Procedures and Properties "' 

        public Browse(IntPtr Handle)
        {
            stcBrowseInfo.hOwner = Handle;
            stcBrowseInfo.lpfn = BrowseCallbackProc;
        }

        #region '" Browse for Computers "' 

        public string BrowseForComputers()
        {
            var transTemp4 = stcBrowseInfo.pidlRoot;
            Win32API.SHGetSpecialFolderLocation(stcBrowseInfo.hOwner, Convert.ToInt32(Win32API.CSIDL.NETWORK), ref transTemp4);
            stcBrowseInfo.ulFlags = BIF_BROWSEFORCOMPUTER;
            return DoBrowse("");
        }

        #endregion

        #region '" Browse for Folder "' 

        public string BrowseForFolder()
        {
            stcBrowseInfo.pidlRoot = 0;
            stcBrowseInfo.ulFlags = BIF_RETURNONLYFSDIRS;
            return DoBrowse("");
        }


        public string BrowseForFolder(string StartPath)
        {
            stcBrowseInfo.pidlRoot = 0;
            stcBrowseInfo.ulFlags = BIF_RETURNONLYFSDIRS;
            return DoBrowse(StartPath);
        }


        public string BrowseForFolder(Win32API.CSIDL StartLocation)
        {
            var transTemp3 = stcBrowseInfo.pidlRoot;
            Win32API.SHGetSpecialFolderLocation(stcBrowseInfo.hOwner, Convert.ToInt32(StartLocation), ref transTemp3);
            stcBrowseInfo.ulFlags = BIF_RETURNONLYFSDIRS;
            return DoBrowse("");
        }

        #endregion

        #region '" Browse for Files "' 

        public string BrowseForFiles()
        {
            stcBrowseInfo.pidlRoot = 0;
            stcBrowseInfo.ulFlags = BIF_BROWSEINCLUDEFILES;
            return DoBrowse("");
        }


        public string BrowseForFiles(string StartPath)
        {
            stcBrowseInfo.pidlRoot = 0;
            stcBrowseInfo.ulFlags = BIF_BROWSEINCLUDEFILES;
            return DoBrowse(StartPath);
        }


        public string BrowseForFiles(Win32API.CSIDL StartLocation)
        {
            var transTemp2 = stcBrowseInfo.pidlRoot;
            Win32API.SHGetSpecialFolderLocation(stcBrowseInfo.hOwner, Convert.ToInt32(StartLocation), ref transTemp2);
            stcBrowseInfo.ulFlags = BIF_BROWSEINCLUDEFILES;
            return DoBrowse("");
        }

        #endregion

        #region '" Public Properties "' 

        public string Title
        {
            get { return stcBrowseInfo.lpszTitle; }

            set { stcBrowseInfo.lpszTitle = value; }
        }

        public bool NewUI { get; set; }

        public bool ShowStatus { get; set; }

        #endregion

        #endregion
    }
}