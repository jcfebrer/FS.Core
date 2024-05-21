/*
 * Please leave this Copyright notice in your code if you use it
 * Written by Decebal Mihailescu [http://www.codeproject.com/script/articles/list_articles.asp?userid=634640]
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using static FSLibraryCore.Win32APIEnums;

namespace FSLibraryCore
{
    /// <summary>
    /// Clase con funciones para el uso de la API32 de windows
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static class Win32API
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, Int32 dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGUIThreadInfo(Int32 idThread, GUITHREADINFO lpgui);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int16 GetAsyncKeyState(Int32 vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, UInt32 gaFlags);


        public delegate IntPtr LowLevelKeyboardProcDelegate(Int32 nCode, IntPtr wParam, KBDLLHOOKSTRUCT lParam);


        /// <summary>
        /// Logons the user.
        /// Declare signatures for Win32 LogonUser and CloseHandle APIs
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="authority">The authority.</param>
        /// <param name="password">The password.</param>
        /// <param name="logonType">Type of the logon.</param>
        /// <param name="logonProvider">The logon provider.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(
            string principal,
            string authority,
            string password,
            LogonSessionType logonType,
            LogonProvider logonProvider,
            out IntPtr token);


        /// <summary>
        /// Waits for single object.
        /// </summary>
        /// <param name="hHandle">The h handle.</param>
        /// <param name="dwMilliseconds">The dw milliseconds.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int WaitForSingleObject
        (
            IntPtr hHandle,
            int dwMilliseconds
        );


        /// <summary>
        /// Devuelve el path largo dado un path 8.3
        /// </summary>
        /// <param name="shortPath"></param>
        /// <param name="longPath"></param>
        /// <param name="bufSize"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetLongPathName", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetLongPathName(string shortPath, StringBuilder longPath, int bufSize);

        /// <summary>
        /// Devuelve el path corto 8.2 dado un path largo.
        /// </summary>
        /// <param name="longPath"></param>
        /// <param name="shortPath"></param>
        /// <param name="bufSize"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "GetShortPathName", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetShortPathName(string longPath, StringBuilder shortPath, int bufSize);


        /// <summary>
        /// Tipo WindowsProc para enumerar las ventanas
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate int EnumWindowsProc(IntPtr hwnd, int lParam);

        /// <summary>
        /// Función que llama a lpEnumFunc con los parametros lParam para enumerar las ventanas activas.
        /// </summary>
        /// <param name="lpEnumFunc"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.Dll")]
        public static extern int EnumWindows(EnumWindowsProc lpEnumFunc, int lParam);

        /// <summary>
        /// Devuelve los hijos de una ventana
        /// </summary>
        /// <param name="window"></param>
        /// <param name="callback"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowsProc callback, int lParam);


        /// <summary>
        /// Enums the desktop windows.
        /// </summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <param name="lpEnumCallbackFunction">The lp enum callback function.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto,
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
            IntPtr lParam);

        /// <summary>
        /// Sonido con fecuencia y duración
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);


        /// <summary>
        /// Función que genera eventos de teclado.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scan"></param>
        /// <param name="flags"></param>
        /// <param name="extraInfo"></param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte key, byte scan, int flags, int extraInfo);


        /// <summary>
        /// Gets the focus.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        /// <summary>
        /// Sets the focus.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// Closes the handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll",
            EntryPoint = "CloseHandle", SetLastError = true,
            CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        [SuppressUnmanagedCodeSecurityAttribute]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr handle);

        /// <summary>
        /// Creates the process as user.
        /// </summary>
        /// <param name="hToken">The h token.</param>
        /// <param name="lpApplicationName">Name of the lp application.</param>
        /// <param name="lpCommandLine">The lp command line.</param>
        /// <param name="lpProcessAttributes">The lp process attributes.</param>
        /// <param name="lpThreadAttributes">The lp thread attributes.</param>
        /// <param name="bInheritHandle">if set to <c>true</c> [b inherit handle].</param>
        /// <param name="dwCreationFlags">The dw creation flags.</param>
        /// <param name="lpEnvrionment">The lp envrionment.</param>
        /// <param name="lpCurrentDirectory">The lp current directory.</param>
        /// <param name="lpStartupInfo">The lp startup information.</param>
        /// <param name="lpProcessInformation">The lp process information.</param>
        /// <returns></returns>
        [
            DllImport("advapi32.dll",
                EntryPoint = "CreateProcessAsUser", SetLastError = true,
                CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)
        ]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool
            CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine,
                ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes,
                bool bInheritHandle, int dwCreationFlags, IntPtr lpEnvrionment,
                string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo,
                ref PROCESS_INFORMATION lpProcessInformation);

        /// <summary>
        /// Duplicates the token ex.
        /// </summary>
        /// <param name="hExistingToken">The h existing token.</param>
        /// <param name="dwDesiredAccess">The dw desired access.</param>
        /// <param name="lpThreadAttributes">The lp thread attributes.</param>
        /// <param name="ImpersonationLevel">The impersonation level.</param>
        /// <param name="dwTokenType">Type of the dw token.</param>
        /// <param name="phNewToken">The ph new token.</param>
        /// <returns></returns>
        [
            DllImport("advapi32.dll",
                EntryPoint = "DuplicateTokenEx")
        ]
        public static extern bool
            DuplicateTokenEx(IntPtr hExistingToken, int dwDesiredAccess,
                ref SECURITY_ATTRIBUTES lpThreadAttributes,
                int ImpersonationLevel, int dwTokenType,
                ref IntPtr phNewToken);

        /// <summary>
        /// Systems the parameters information.
        /// </summary>
        /// <param name="uiAction">The UI action.</param>
        /// <param name="uiParam">The UI parameter.</param>
        /// <param name="pvParam">The pv parameter.</param>
        /// <param name="fWinIni">The f win ini.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(SPI uiAction, int uiParam, ref ANIMATIONINFO pvParam,
            SPIF fWinIni);

        /// <summary>
        ///     Enable/Disable MinAnimate
        /// </summary>
        /// <param name="status"></param>
        public static void SetMinimizeMaximizeAnimation(bool status)
        {
            var animationInfo = new ANIMATIONINFO(status);

            //System.Threading.Thread.Sleep(500);
            if (!SystemParametersInfo(SPI.SPI_GETANIMATION, ANIMATIONINFO.GetSize(), ref animationInfo, SPIF.None))
            {
                var ex = new Win32Exception(Marshal.GetLastWin32Error());
                throw new ApplicationException("SystemParametersInfo get failed: " + ex.Message, ex);
            }
            //else
            //    EventLog.WriteEntry("Screen Monitor", "SystemParametersInfo get succeeded ", EventLogEntryType.Information, 1, 1);

            if (animationInfo.IMinAnimate != status)
            {
                animationInfo.IMinAnimate = status;
                //System.Threading.Thread.Sleep(500);
                if (!SystemParametersInfo(SPI.SPI_SETANIMATION, ANIMATIONINFO.GetSize(), ref animationInfo,
                    SPIF.SPIF_SENDCHANGE))
                {
                    var ex = new Win32Exception(Marshal.GetLastWin32Error());
                    throw new ApplicationException("SystemParametersInfo set failed: " + ex.Message, ex);
                }

                //else
                //    EventLog.WriteEntry("Screen Monitor", "SystemParametersInfo set succeeded ", EventLogEntryType.Information, 1, 1);
            }
        }

        /// <summary>
        /// API para ejecutar un fichero o aplicación.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lpOperation">The lp operation.</param>
        /// <param name="lpFile">The lp file.</param>
        /// <param name="lpParameters">The lp parameters.</param>
        /// <param name="lpDirectory">The lp directory.</param>
        /// <param name="nShowCmd">The n show command.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", EntryPoint = "ShellExecuteA", CharSet = CharSet.Ansi, SetLastError = true,
            ExactSpelling = true)]
        public static extern int ShellExecute(int hwnd, string lpOperation, string lpFile, string lpParameters,
            string lpDirectory, int nShowCmd);

        /// <summary>
        /// Opens the window station.
        /// </summary>
        /// <param name="lpszWinSta">The LPSZ win sta.</param>
        /// <param name="fInherit">if set to <c>true</c> [f inherit].</param>
        /// <param name="dwDesiredAccess">The dw desired access.</param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, int dwDesiredAccess);

        /// <summary>
        /// Opens the desktop.
        /// </summary>
        /// <param name="lpszDesktop">The LPSZ desktop.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="fInherit">if set to <c>true</c> [f inherit].</param>
        /// <param name="dwDesiredAccess">The dw desired access.</param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr OpenDesktop(string lpszDesktop, int dwFlags, bool fInherit, int dwDesiredAccess);

        /// <summary>
        /// Gets the process window station.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr GetProcessWindowStation();

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="wMsg">The w MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

        /// <summary>
        /// Gets the thread desktop.
        /// </summary>
        /// <param name="dwThreadID">The dw thread identifier.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(int dwThreadID);

        /// <summary>
        /// Gets the double click time.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetDoubleClickTime();


        /// <summary>
        /// Envia un mensaje a la ventana
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="IDDlgItem"></param>
        /// <param name="uMsg"></param>
        /// <param name="nMaxCount"></param>
        /// <param name="lpString"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr SendDlgItemMessage(IntPtr hWnd, int IDDlgItem, int uMsg, int nMaxCount, StringBuilder lpString);

        /// <summary>
        /// Obtiene la ventana padre
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        /// <summary>
        /// Obtiene el rectangulo de la ventana indicada
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern long GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);


        /// <summary>
        /// Gets the current thread identifier.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        /// <summary>
        /// Shes the get known folder path.
        /// </summary>
        /// <param name="rfid">The rfid.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="hToken">The h token.</param>
        /// <param name="ppszPath">The PPSZ path.</param>
        /// <returns></returns>
        [DllImport("Shell32.dll")]
        public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, int dwFlags,
            IntPtr hToken, out IntPtr ppszPath);

        /// <summary>
        /// Sets the process window station.
        /// </summary>
        /// <param name="hWindowStation">The h window station.</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessWindowStation(IntPtr hWindowStation);

        /// <summary>
        /// Sets the thread desktop.
        /// </summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        /// <summary>
        /// Closes the window station.
        /// </summary>
        /// <param name="hWinSta">The h win sta.</param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseWindowStation(IntPtr hWinSta);

        /// <summary>
        /// Closes the desktop.
        /// </summary>
        /// <param name="hDesktop">The h desktop.</param>
        /// <returns></returns>
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        /// <summary>
        /// Gets the best interface.
        /// </summary>
        /// <param name="dwDestAddr">The dw dest addr.</param>
        /// <param name="pdwBestIfIndex">Index of the PDW best if.</param>
        /// <returns></returns>
        [DllImport("iphlpapi")]
        public static extern int GetBestInterface(int dwDestAddr, ref int pdwBestIfIndex);

        /// <summary>
        /// RPCs the impersonate client.
        /// </summary>
        /// <param name="rpcBindingHandle">The RPC binding handle.</param>
        /// <returns></returns>
        [DllImport("rpcrt4.dll")]
        public static extern int RpcImpersonateClient(IntPtr rpcBindingHandle);

        /// <summary>
        /// RPCs the revert to self.
        /// </summary>
        /// <returns></returns>
        [DllImport("rpcrt4.dll")]
        public static extern int RpcRevertToSelf();

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="length">The length.</param>
        [DllImport("User32.Dll", SetLastError = true)]
        public static extern void GetClassName(IntPtr hWnd, [MarshalAs(UnmanagedType.LPStr)] StringBuilder param,
            int length);

        /// <summary>
        /// Gets the length of the window text.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpString">The lp string.</param>
        /// <param name="nMaxCount">The n maximum count.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Initializes the common controls.
        /// </summary>
        /// <returns></returns>
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();

        /// <summary>
        /// Images the list begin drag.
        /// </summary>
        /// <param name="himlTrack">The himl track.</param>
        /// <param name="iTrack">The i track.</param>
        /// <param name="dxHotspot">The dx hotspot.</param>
        /// <param name="dyHotspot">The dy hotspot.</param>
        /// <returns></returns>
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr himlTrack, int iTrack, int dxHotspot, int dyHotspot);

        /// <summary>
        /// Images the list drag move.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragMove(int x, int y);

        /// <summary>
        /// Images the list end drag.
        /// </summary>
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern void ImageList_EndDrag();

        /// <summary>
        /// Images the list drag enter.
        /// </summary>
        /// <param name="hwndLock">The HWND lock.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr hwndLock, int x, int y);

        /// <summary>
        /// Images the list drag leave.
        /// </summary>
        /// <param name="hwndLock">The HWND lock.</param>
        /// <returns></returns>
        [DllImport("comctl32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr hwndLock);

        /// <summary>
        /// Gets the active window.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /// <summary>
        /// Enums the thread windows.
        /// </summary>
        /// <param name="dwThreadId">The dw thread identifier.</param>
        /// <param name="lpfn">The LPFN.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        /// <summary>
        /// Prints the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="dc">The dc.</param>
        /// <param name="flags">The flags.</param>
        /// <returns></returns>
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr dc, int flags);

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hDC">The h dc.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Gets the system menu.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="bRevert">if set to <c>true</c> [b revert].</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        /// <summary>
        /// Appends the menu.
        /// </summary>
        /// <param name="hMenu">The h menu.</param>
        /// <param name="wFlags">The w flags.</param>
        /// <param name="wIDNewItem">The w identifier new item.</param>
        /// <param name="lpNewItem">The lp new item.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AppendMenu(IntPtr hMenu, int wFlags, IntPtr wIDNewItem, string lpNewItem);

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nCmdShow">The n command show.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        /// <summary>
        /// Determines whether [is rect empty] [the specified LPRC].
        /// </summary>
        /// <param name="lprc">The LPRC.</param>
        /// <returns>
        ///   <c>true</c> if [is rect empty] [the specified LPRC]; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsRectEmpty([In] ref RECT lprc);

        /// <summary>
        /// Clients to screen.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lpPoint">The lp point.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ClientToScreen(IntPtr hwnd, ref POINT lpPoint);

        /// <summary>
        /// Gets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int index);

        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int index, int dwNewLong);

        /// <summary>
        /// Sets the layered window attributes.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="crKey">The cr key.</param>
        /// <param name="bAlpha">The b alpha.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns></returns>
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

        /// <summary>
        /// Impersonates the logged on user.
        /// </summary>
        /// <param name="hToken">The h token.</param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        /// <summary>
        /// Reverts to self.
        /// </summary>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RevertToSelf();

        /// <summary>
        /// Opens the process token.
        /// </summary>
        /// <param name="ProcessHandle">The process handle.</param>
        /// <param name="DesiredAccess">The desired access.</param>
        /// <param name="TokenHandle">The token handle.</param>
        /// <returns></returns>
        [DllImport("advapi32", SetLastError = true)]
        [SuppressUnmanagedCodeSecurityAttribute]
        public static extern int OpenProcessToken(
            IntPtr ProcessHandle, // handle to process
            TokenPrivilege DesiredAccess, // desired access to process
            ref IntPtr TokenHandle // handle to open access token
        );

        /// <summary>
        /// Duplicates the token.
        /// </summary>
        /// <param name="ExistingTokenHandle">The existing token handle.</param>
        /// <param name="SECURITY_IMPERSONATION_LEVEL">The security impersonation level.</param>
        /// <param name="DuplicateTokenHandle">The duplicate token handle.</param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DuplicateToken(IntPtr ExistingTokenHandle,
            int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className,
                                                 IntPtr windowTitle);

        /// <summary>
        /// Gets the dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Deletes the dc.
        /// </summary>
        /// <param name="hDc">The h dc.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(IntPtr hDc);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hDc">The h dc.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hDc);

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="hWndChild">The h WND child.</param>
        /// <param name="hWndNewParent">The h WND new parent.</param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// Bits the BLT.
        /// </summary>
        /// <param name="hdcDest">The HDC dest.</param>
        /// <param name="xDest">The x dest.</param>
        /// <param name="yDest">The y dest.</param>
        /// <param name="wDest">The w dest.</param>
        /// <param name="hDest">The h dest.</param>
        /// <param name="hdcSource">The HDC source.</param>
        /// <param name="xSrc">The x source.</param>
        /// <param name="ySrc">The y source.</param>
        /// <param name="RasterOp">The raster op.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource,
            int xSrc, int ySrc, int RasterOp);

        /// <summary>
        /// Creates the compatible bitmap.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="nWidth">Width of the n.</param>
        /// <param name="nHeight">Height of the n.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        /// <summary>
        /// Creates the compatible dc.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="bmp">The BMP.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        /// <summary>
        /// Gets the desktop window.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = false)]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// Gets the window dc.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Attaches the thread input.
        /// </summary>
        /// <param name="idAttach">The identifier attach.</param>
        /// <param name="idAttachTo">The identifier attach to.</param>
        /// <param name="fAttach">if set to <c>true</c> [f attach].</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);

        /// <summary>
        /// Gets the foreground window.
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Determines whether the specified h WND is iconic.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>
        ///   <c>true</c> if the specified h WND is iconic; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("User32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// Determines whether the specified h WND is window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>
        ///   <c>true</c> if the specified h WND is window; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        /// Gets the window rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// Determines whether [is window visible] [the specified h WND].
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>
        ///   <c>true</c> if [is window visible] [the specified h WND]; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// Gets the client rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// Gets the window thread process identifier.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpdwProcessId">The LPDW process identifier.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        /// <summary>
        /// Loads the user profile.
        /// </summary>
        /// <param name="hToken">The h token.</param>
        /// <param name="lpProfileinfo">The lp profileinfo.</param>
        /// <returns></returns>
        [DllImport("userenv", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LoadUserProfile(IntPtr hToken, IntPtr
            lpProfileinfo);

        /// <summary>
        /// Unloads the user profile.
        /// </summary>
        /// <param name="hToken">The h token.</param>
        /// <param name="hProfile">The h profile.</param>
        /// <returns></returns>
        [DllImport("userenv", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnloadUserProfile(IntPtr hToken, IntPtr hProfile);

        /// <summary>
        /// Intersects the rect.
        /// </summary>
        /// <param name="lprcDst">The LPRC DST.</param>
        /// <param name="lprcSrc1">The LPRC SRC1.</param>
        /// <param name="lprcSrc2">The LPRC SRC2.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IntersectRect(out RECT lprcDst, [In] ref RECT lprcSrc1,
            [In] ref RECT lprcSrc2);

        /// <summary>
        /// Gets the current object.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr GetCurrentObject(IntPtr hdc, short objectType);

        /// <summary>
        /// Releases the dc.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        [DllImport("user32.dll")]
        public static extern void ReleaseDC(IntPtr hdc);

        /// <summary>
        /// Updates the window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        [DllImport("user32.dll")]
        public static extern void UpdateWindow(IntPtr hwnd);

        /// <summary>
        /// Coes the task memory free.
        /// </summary>
        /// <param name="addr">The addr.</param>
        [DllImport("ole32.dll")]
        public static extern void CoTaskMemFree(IntPtr addr);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="uMsg">The u MSG.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <param name="lpData">The lp data.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int lParam, int lpData);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="uMsg">The u MSG.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <param name="lpData">The lp data.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int lParam, string lpData);

        /// <summary>
        /// Shes the browse for folder.
        /// </summary>
        /// <param name="lpBrowseInfo">The lp browse information.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpBrowseInfo);

        /// <summary>
        /// Shes the get path from identifier list.
        /// </summary>
        /// <param name="pidl">The pidl.</param>
        /// <param name="pszPath">The PSZ path.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
        public static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);

        /// <summary>
        /// Shes the get special folder location.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nFolder">The n folder.</param>
        /// <param name="pidl">The pidl.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
        public static extern int SHGetSpecialFolderLocation(IntPtr hWnd, int nFolder, ref int pidl);
        //When using the SPI_GETANIMATION or SPI_SETANIMATION actions, the uiParam value must be set to (System.int32)Marshal.SizeOf(typeof(ANIMATIONINFO)).

        /// <summary>
        /// Mouses the event.
        /// </summary>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <param name="cButtons">The c buttons.</param>
        /// <param name="dwExtraInfo">The dw extra information.</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// Muestra u oculta el cursor
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool show);


        /// <summary>
        /// Sets the cursor position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);


        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="lpString">The lp string.</param>
        /// <param name="cch">The CCH.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hwnd, string lpString, int cch);

        /// <summary>
        /// Sets the windows hook ex.
        /// </summary>
        /// <param name="idHook">The identifier hook.</param>
        /// <param name="lpfn">The LPFN.</param>
        /// <param name="hMod">The h mod.</param>
        /// <param name="dwThreadId">The dw thread identifier.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr hMod,
            int dwThreadId);

        /// <summary>
        /// Unhooks the windows hook ex.
        /// </summary>
        /// <param name="idHook">The identifier hook.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// Calls the next hook ex.
        /// </summary>
        /// <param name="idHook">The identifier hook.</param>
        /// <param name="nCode">The n code.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(
            int idHook,
            int nCode,
            int wParam,
            IntPtr lParam);

        /// <summary>
        /// HookProc
        /// </summary>
        /// <param name="nCode">The n code.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        /// <summary>
        /// Converts to ascii.
        /// </summary>
        /// <param name="uVirtKey">The u virt key.</param>
        /// <param name="uScanCode">The u scan code.</param>
        /// <param name="lpbKeyState">State of the LPB key.</param>
        /// <param name="lpwTransKey">The LPW trans key.</param>
        /// <param name="fuState">State of the fu.</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern int ToAscii(
            int uVirtKey,
            int uScanCode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState);

        /// <summary>
        /// Converts to unicode.
        /// </summary>
        /// <param name="virtualKey">The virtual key.</param>
        /// <param name="scanCode">The scan code.</param>
        /// <param name="keyStates">The key states.</param>
        /// <param name="chars">The chars.</param>
        /// <param name="charMaxCount">The character maximum count.</param>
        /// <param name="flags">The flags.</param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Unicode)]
        public static extern int ToUnicode(
            int virtualKey, int scanCode, byte[] keyStates,
            [MarshalAs(UnmanagedType.LPArray)][Out] char[] chars,
            int charMaxCount, int flags);

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <param name="lpKeyState">State of the lp key.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// Gets the state of the key.
        /// </summary>
        /// <param name="vKey">The v key.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        /// <summary>
        /// Gets the module handle.
        /// </summary>
        /// <param name="lpModuleName">Name of the lp module.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// Maps the virtual key.
        /// </summary>
        /// <param name="uCode">The u code.</param>
        /// <param name="uMapType">Type of the u map.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(int uCode, int uMapType);

        /// <summary>
        /// Converts to unicodeex.
        /// </summary>
        /// <param name="wVirtKey">The w virt key.</param>
        /// <param name="wScanCode">The w scan code.</param>
        /// <param name="lpKeyState">State of the lp key.</param>
        /// <param name="pwszBuff">The PWSZ buff.</param>
        /// <param name="cchBuff">The CCH buff.</param>
        /// <param name="wFlags">The w flags.</param>
        /// <param name="dwhkl">The DWHKL.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int ToUnicodeEx(int wVirtKey, int wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder pwszBuff, int cchBuff, int wFlags, IntPtr dwhkl);

        /// <summary>
        /// Gets the keyboard layout.
        /// </summary>
        /// <param name="dwLayout">The dw layout.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);

        /// <summary>
        /// Maps the virtual key ex.
        /// </summary>
        /// <param name="uCode">The u code.</param>
        /// <param name="uMapType">Type of the u map.</param>
        /// <param name="dwhkl">The DWHKL.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nCmdShow">The n command show.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// ws the net add connection2.
        /// </summary>
        /// <param name="netResource">The net resource.</param>
        /// <param name="password">The password.</param>
        /// <param name="username">The username.</param>
        /// <param name="flags">The flags.</param>
        /// <returns></returns>
        [DllImport("mpr.dll")]
        public static extern int WNetAddConnection2(NetResource netResource,
            string password, string username, int flags);


        /// <summary>
        /// ws the net cancel connection2.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <returns></returns>
        [DllImport("mpr.dll")]
        public static extern int WNetCancelConnection2(string name, int flags,
            bool force);

        /// <summary>
        /// SetKeyboardState
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi)]
        public static extern bool SetKeyboardState(byte[] keys);



        /// <summary>
        /// CloseHandle
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int CloseHandle(int hObject);

        /// <summary>
        /// CreateFile
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwShareMode"></param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwCreationDisposition"></param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns></returns>
        [DllImport("kernel32", EntryPoint = "CreateFileA")]
        public static extern int CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode,
            int lpSecurityAttributes, int dwCreationDisposition,
            int dwFlagsAndAttributes, int hTemplateFile);

        /// <summary>
        /// DeviceIoControl
        /// </summary>
        /// <param name="hDevice"></param>
        /// <param name="dwIoControlCode"></param>
        /// <param name="lpInBuffer"></param>
        /// <param name="nInBufferSize"></param>
        /// <param name="lpOutBuffer"></param>
        /// <param name="nOutBufferSize"></param>
        /// <param name="lpBytesReturned"></param>
        /// <param name="lpOverlapped"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        public static extern int DeviceIoControl(int hDevice, int dwIoControlCode, out SENDCMDINPARAMS lpInBuffer,
            int nInBufferSize, out SENDCMDOUTPARAMS lpOutBuffer,
            int nOutBufferSize, ref int lpBytesReturned, int lpOverlapped);

        /// <summary>
        /// Espacio en disco utilizando recurso compartido.
        /// </summary>
        /// <param name="lpDirectoryName"></param>
        /// <param name="lpFreeBytesAvailable"></param>
        /// <param name="lpTotalNumberOfBytes"></param>
        /// <param name="lpTotalNumberOfFreeBytes"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
           out long lpFreeBytesAvailable,
           out long lpTotalNumberOfBytes,
           out long lpTotalNumberOfFreeBytes);

        /// <summary>
        /// Gets the window process identifier.
        /// This Function is used to get Active process ID...
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <returns></returns>
        public static Int32 GetWindowProcessID(IntPtr hwnd)
        {
            Int32 pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        /// <summary>
        /// Gets the active window title.
        /// </summary>
        /// <returns></returns>
        public static string GetActiveWindowTitle()
        {
            var strTitle = string.Empty;
            var handle = GetForegroundWindow();
            // Obtain the length of the text   
            var intLength = GetWindowTextLength(handle) + 1;
            var stringBuilder = new StringBuilder(intLength);
            if (GetWindowText(handle, stringBuilder, intLength) > 0)
            {
                strTitle = stringBuilder.ToString();
            }
            return strTitle;
        }
    }
}