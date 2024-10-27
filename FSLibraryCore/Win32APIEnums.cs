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

namespace FSLibraryCore
{
    /// <summary>
    /// Clase con funciones para el uso de la API32 de windows
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static class Win32APIEnums
    {
        /// <summary>
        /// Estructura STARTUPINFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct STARTUPINFO
        {
            /// <summary>
            /// The cb
            /// </summary>
            public int cb;
            /// <summary>
            /// The lp reserved
            /// </summary>
            public string lpReserved;
            /// <summary>
            /// The lp desktop
            /// </summary>
            public string lpDesktop;
            /// <summary>
            /// The lp title
            /// </summary>
            public string lpTitle;
            /// <summary>
            /// The dw x
            /// </summary>
            public int dwX;
            /// <summary>
            /// The dw y
            /// </summary>
            public int dwY;
            /// <summary>
            /// The dw x size
            /// </summary>
            public int dwXSize;
            /// <summary>
            /// The dw x count chars
            /// </summary>
            public int dwXCountChars;
            /// <summary>
            /// The dw y count chars
            /// </summary>
            public int dwYCountChars;
            /// <summary>
            /// The dw fill attribute
            /// </summary>
            public int dwFillAttribute;
            /// <summary>
            /// The dw flags
            /// </summary>
            public int dwFlags;
            /// <summary>
            /// The w show window
            /// </summary>
            public short wShowWindow;
            /// <summary>
            /// The cb reserved2
            /// </summary>
            public short cbReserved2;
            /// <summary>
            /// The lp reserved2
            /// </summary>
            public IntPtr lpReserved2;
            /// <summary>
            /// The h standard input
            /// </summary>
            public IntPtr hStdInput;
            /// <summary>
            /// The h standard output
            /// </summary>
            public IntPtr hStdOutput;
            /// <summary>
            /// The h standard error
            /// </summary>
            public IntPtr hStdError;
        }

        /// <summary>
        /// Process information
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            /// <summary>
            /// The process
            /// </summary>
            public IntPtr hProcess;
            /// <summary>
            /// The thread
            /// </summary>
            public IntPtr hThread;
            /// <summary>
            /// The process identifier
            /// </summary>
            public int dwProcessID;
            /// <summary>
            /// The thread identifier
            /// </summary>
            public int dwThreadID;
        }

        /// <summary>
        /// Security attributes
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            /// <summary>
            /// The length
            /// </summary>
            public int Length;
            /// <summary>
            /// The security descriptor
            /// </summary>
            public IntPtr lpSecurityDescriptor;
            /// <summary>
            /// The inherit handle
            /// </summary>
            public bool bInheritHandle;
        }

        /// <summary>
        /// Security impersonation level
        /// </summary>
        public enum SECURITY_IMPERSONATION_LEVEL
        {
            /// <summary>
            /// The security anonymous
            /// </summary>
            SecurityAnonymous,
            /// <summary>
            /// The security identification
            /// </summary>
            SecurityIdentification,
            /// <summary>
            /// The security impersonation
            /// </summary>
            SecurityImpersonation,
            /// <summary>
            /// The security delegation
            /// </summary>
            SecurityDelegation
        }

        /// <summary>
        /// Token type
        /// </summary>
        public enum TOKEN_TYPE
        {
            /// <summary>
            /// The token primary
            /// </summary>
            TokenPrimary = 1,
            /// <summary>
            /// The token impersonation
            /// </summary>
            TokenImpersonation
        }

        /// <summary>
        /// Map type
        /// </summary>
        public enum MapType
        {
            /// <summary>
            /// The mapvk vk to VSC
            /// </summary>
            MAPVK_VK_TO_VSC = 0x0,
            /// <summary>
            /// The mapvk VSC to vk
            /// </summary>
            MAPVK_VSC_TO_VK = 0x1,
            /// <summary>
            /// The mapvk vk to character
            /// </summary>
            MAPVK_VK_TO_CHAR = 0x2,
            /// <summary>
            /// The mapvk VSC to vk ex
            /// </summary>
            MAPVK_VSC_TO_VK_EX = 0x3,
            /// <summary>
            /// The mapvk vk to VSC ex
            /// </summary>
            MAPVK_VK_TO_VSC_EX = 0x4
        }

        /// <summary>
        /// Logon session type
        /// </summary>
        public enum LogonSessionType
        {
            /// <summary>
            /// The interactive
            /// </summary>
            Interactive = 2,
            /// <summary>
            /// The network
            /// </summary>
            Network,
            /// <summary>
            /// The batch
            /// </summary>
            Batch,
            /// <summary>
            /// The service
            /// </summary>
            Service,
            /// <summary>
            /// The network cleartext
            /// </summary>
            NetworkCleartext = 8,
            /// <summary>
            /// Creates new credentials.
            /// </summary>
            NewCredentials
        }

        /// <summary>
        /// CSIDL
        /// </summary>
        public enum CSIDL
        {
            /// <summary>
            /// The admintools
            /// </summary>
            ADMINTOOLS = 0X30,
            /// <summary>
            /// The altstartup
            /// </summary>
            ALTSTARTUP = 0X1D,
            /// <summary>
            /// The appdata
            /// </summary>
            APPDATA = 0X1A,
            /// <summary>
            /// The bitbucket
            /// </summary>
            BITBUCKET = 0XA,
            /// <summary>
            /// The cdburn area
            /// </summary>
            CDBURN_AREA = 0X3B,
            /// <summary>
            /// The common admintools
            /// </summary>
            COMMON_ADMINTOOLS = 0X2F,
            /// <summary>
            /// The common altstartup
            /// </summary>
            COMMON_ALTSTARTUP = 0X1E,
            /// <summary>
            /// The common appdata
            /// </summary>
            COMMON_APPDATA = 0X23,
            /// <summary>
            /// The common desktopdirectory
            /// </summary>
            COMMON_DESKTOPDIRECTORY = 0X19,
            /// <summary>
            /// The common documents
            /// </summary>
            COMMON_DOCUMENTS = 0X2E,
            /// <summary>
            /// The common favorites
            /// </summary>
            COMMON_FAVORITES = 0X1F,
            /// <summary>
            /// The common music
            /// </summary>
            COMMON_MUSIC = 0X35,
            /// <summary>
            /// The common oem links
            /// </summary>
            COMMON_OEM_LINKS = 0X3A,
            /// <summary>
            /// The common pictures
            /// </summary>
            COMMON_PICTURES = 0X36,
            /// <summary>
            /// The common programs
            /// </summary>
            COMMON_PROGRAMS = 0X17,
            /// <summary>
            /// The common startmenu
            /// </summary>
            COMMON_STARTMENU = 0X16,
            /// <summary>
            /// The common startup
            /// </summary>
            COMMON_STARTUP = 0X18,
            /// <summary>
            /// The common templates
            /// </summary>
            COMMON_TEMPLATES = 0X2D,
            /// <summary>
            /// The common video
            /// </summary>
            COMMON_VIDEO = 0X37,
            /// <summary>
            /// The computers near me
            /// </summary>
            COMPUTERSNEARME = 0X3D,
            /// <summary>
            /// The connections
            /// </summary>
            CONNECTIONS = 0X31,
            /// <summary>
            /// The controls
            /// </summary>
            CONTROLS = 0X3,
            /// <summary>
            /// The cookies
            /// </summary>
            COOKIES = 0X21,
            /// <summary>
            /// The desktop
            /// </summary>
            DESKTOP = 0X0,
            /// <summary>
            /// The desktopdirectory
            /// </summary>
            DESKTOPDIRECTORY = 0X10,
            /// <summary>
            /// The drives
            /// </summary>
            DRIVES = 0X11,
            /// <summary>
            /// The favorites
            /// </summary>
            FAVORITES = 0X6,
            /// <summary>
            /// The flag create
            /// </summary>
            FLAG_CREATE = 0X8000,
            /// <summary>
            /// The flag dont verify
            /// </summary>
            FLAG_DONT_VERIFY = 0X4000,
            /// <summary>
            /// The flag mask
            /// </summary>
            FLAG_MASK = 0XFF00,
            /// <summary>
            /// The flag no alias
            /// </summary>
            FLAG_NO_ALIAS = 0X1000,
            /// <summary>
            /// The flag per user initialize
            /// </summary>
            FLAG_PER_USER_INIT = 0X800,
            /// <summary>
            /// The fonts
            /// </summary>
            FONTS = 0X14,
            /// <summary>
            /// The history
            /// </summary>
            HISTORY = 0X22,
            /// <summary>
            /// The internet
            /// </summary>
            INTERNET = 0X1,
            /// <summary>
            /// The internet cache
            /// </summary>
            INTERNET_CACHE = 0X20,
            /// <summary>
            /// The local appdata
            /// </summary>
            LOCAL_APPDATA = 0X1C,
            /// <summary>
            /// The mydocuments
            /// </summary>
            MYDOCUMENTS = 0XC,
            /// <summary>
            /// The mymusic
            /// </summary>
            MYMUSIC = 0XD,
            /// <summary>
            /// The mypictures
            /// </summary>
            MYPICTURES = 0X27,
            /// <summary>
            /// The myvideo
            /// </summary>
            MYVIDEO = 0XE,
            /// <summary>
            /// The nethood
            /// </summary>
            NETHOOD = 0X13,
            /// <summary>
            /// The network
            /// </summary>
            NETWORK = 0X12,
            /// <summary>
            /// The personal
            /// </summary>
            PERSONAL = 0X5,
            /// <summary>
            /// The printers
            /// </summary>
            PRINTERS = 0X4,
            /// <summary>
            /// The printhood
            /// </summary>
            PRINTHOOD = 0X1B,
            /// <summary>
            /// The profile
            /// </summary>
            PROFILE = 0X28,
            /// <summary>
            /// The program files
            /// </summary>
            PROGRAM_FILES = 0X26,
            /// <summary>
            /// The program files common
            /// </summary>
            PROGRAM_FILES_COMMON = 0X2B,
            /// <summary>
            /// The program files common X86
            /// </summary>
            PROGRAM_FILES_COMMONX86 = 0X2C,
            /// <summary>
            /// The program files X86
            /// </summary>
            PROGRAM_FILESX86 = 0X2A,
            /// <summary>
            /// The programs
            /// </summary>
            PROGRAMS = 0X2,
            /// <summary>
            /// The recent
            /// </summary>
            RECENT = 0X8,
            /// <summary>
            /// The resources
            /// </summary>
            RESOURCES = 0X38,
            /// <summary>
            /// The resources localized
            /// </summary>
            RESOURCES_LOCALIZED = 0X39,
            /// <summary>
            /// The sendto
            /// </summary>
            SENDTO = 0X9,
            /// <summary>
            /// The startmenu
            /// </summary>
            STARTMENU = 0XB,
            /// <summary>
            /// The startup
            /// </summary>
            STARTUP = 0X7,
            /// <summary>
            /// The system
            /// </summary>
            SYSTEM = 0X25,
            /// <summary>
            /// The system X86
            /// </summary>
            SYSTEMX86 = 0X29,
            /// <summary>
            /// The templates
            /// </summary>
            TEMPLATES = 0X15,
            /// <summary>
            /// The windows
            /// </summary>
            WINDOWS = 0X24
        }

        /// <summary>
        /// Logon Provider
        /// </summary>
        public enum LogonProvider
        {
            /// <summary>
            /// The default
            /// </summary>
            Default = 0, // default for platform (use this!)            
            /// <summary>
            /// The win NT35
            /// </summary>
            WinNT35, // sends smoke signals to authority            
            /// <summary>
            /// The win NT40
            /// </summary>
            WinNT40, // uses NTLM            
            /// <summary>
            /// The win NT50
            /// </summary>
            WinNT50 // negotiates Kerb or NTLM
        }

        /// <summary>
        /// Browse info
        /// </summary>
        public struct BROWSEINFO
        {
            /// <summary>
            /// The owner
            /// </summary>
            public IntPtr hOwner;
            /// <summary>
            /// The image
            /// </summary>
            public int iImage;
            /// <summary>
            /// The parameter
            /// </summary>
            public IntPtr lParam;
            /// <summary>
            /// The LPFN
            /// </summary>
            public fbCallBack lpfn;
            /// <summary>
            /// The LPSZ title
            /// </summary>
            public string lpszTitle;
            /// <summary>
            /// The pidl root
            /// </summary>
            public int pidlRoot;
            /// <summary>
            /// The PSZ display name
            /// </summary>
            public string pszDisplayName;
            /// <summary>
            /// The ul flags
            /// </summary>
            public int ulFlags;
        }


        /// <summary>
        /// Token Privilege
        /// </summary>
        [Flags]
        public enum TokenPrivilege
        {
            /// <summary>
            /// The standard rights required
            /// </summary>
            STANDARD_RIGHTS_REQUIRED = 0x000F0000,
            /// <summary>
            /// The standard rights read
            /// </summary>
            STANDARD_RIGHTS_READ = 0x00020000,
            /// <summary>
            /// The token assign primary
            /// </summary>
            TOKEN_ASSIGN_PRIMARY = 0x0001,
            /// <summary>
            /// The token duplicate
            /// </summary>
            TOKEN_DUPLICATE = 0x0002,
            /// <summary>
            /// The token impersonate
            /// </summary>
            TOKEN_IMPERSONATE = 0x0004,
            /// <summary>
            /// The token query
            /// </summary>
            TOKEN_QUERY = 0x0008,
            /// <summary>
            /// The token query source
            /// </summary>
            TOKEN_QUERY_SOURCE = 0x0010,
            /// <summary>
            /// The token adjust privileges
            /// </summary>
            TOKEN_ADJUST_PRIVILEGES = 0x0020,
            /// <summary>
            /// The token adjust groups
            /// </summary>
            TOKEN_ADJUST_GROUPS = 0x0040,
            /// <summary>
            /// The token adjust default
            /// </summary>
            TOKEN_ADJUST_DEFAULT = 0x0080,
            /// <summary>
            /// The token adjust sessionid
            /// </summary>
            TOKEN_ADJUST_SESSIONID = 0x0100,
            /// <summary>
            /// The token read
            /// </summary>
            TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY,
            /// <summary>
            /// The token all access
            /// </summary>
            TOKEN_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
                               TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
                               TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
                               TOKEN_ADJUST_SESSIONID
        }

        // The SPI enum code borrowed from www.pinvoke.net

        #region SPI

        /// <summary>
        ///     SPI_ System-wide parameter - Used in SystemParametersInfo function
        /// </summary>
        public enum SPI : int
        {
            /// <summary>
            ///     Determines whether the warning beeper is on.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE if the beeper is on, or FALSE if it is off.
            /// </summary>
            SPI_GETBEEP = 0x0001,

            /// <summary>
            ///     Turns the warning beeper on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
            /// </summary>
            SPI_SETBEEP = 0x0002,

            /// <summary>
            ///     Retrieves the two mouse threshold values and the mouse speed.
            /// </summary>
            SPI_GETMOUSE = 0x0003,

            /// <summary>
            ///     Sets the two mouse threshold values and the mouse speed.
            /// </summary>
            SPI_SETMOUSE = 0x0004,

            /// <summary>
            ///     Retrieves the border multiplier factor that determines the width of a window's sizing border.
            ///     The pvParam parameter must point to an integer variable that receives this value.
            /// </summary>
            SPI_GETBORDER = 0x0005,

            /// <summary>
            ///     Sets the border multiplier factor that determines the width of a window's sizing border.
            ///     The uiParam parameter specifies the new value.
            /// </summary>
            SPI_SETBORDER = 0x0006,

            /// <summary>
            ///     Retrieves the keyboard repeat-speed setting, which is a value in the range from 0 (approximately 2.5 repetitions
            ///     per second)
            ///     through 31 (approximately 30 repetitions per second). The actual repeat rates are hardware-dependent and may vary
            ///     from
            ///     a linear scale by as much as 20%. The pvParam parameter must point to a DWORD variable that receives the setting
            /// </summary>
            SPI_GETKEYBOARDSPEED = 0x000A,

            /// <summary>
            ///     Sets the keyboard repeat-speed setting. The uiParam parameter must specify a value in the range from 0
            ///     (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).
            ///     The actual repeat rates are hardware-dependent and may vary from a linear scale by as much as 20%.
            ///     If uiParam is greater than 31, the parameter is set to 31.
            /// </summary>
            SPI_SETKEYBOARDSPEED = 0x000B,

            /// <summary>
            ///     Not implemented.
            /// </summary>
            SPI_LANGDRIVER = 0x000C,

            /// <summary>
            ///     Sets or retrieves the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large
            ///     icon view.
            ///     To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than
            ///     SM_CXICON.
            ///     To retrieve this value, pvParam must point to an integer that receives the current value.
            /// </summary>
            SPI_ICONHORIZONTALSPACING = 0x000D,

            /// <summary>
            ///     Retrieves the screen saver time-out value, in seconds. The pvParam parameter must point to an integer variable that
            ///     receives the value.
            /// </summary>
            SPI_GETSCREENSAVETIMEOUT = 0x000E,

            /// <summary>
            ///     Sets the screen saver time-out value to the value of the uiParam parameter. This value is the amount of time, in
            ///     seconds,
            ///     that the system must be idle before the screen saver activates.
            /// </summary>
            SPI_SETSCREENSAVETIMEOUT = 0x000F,

            /// <summary>
            ///     Determines whether screen saving is enabled. The pvParam parameter must point to a bool variable that receives TRUE
            ///     if screen saving is enabled, or FALSE otherwise.
            /// </summary>
            SPI_GETSCREENSAVEACTIVE = 0x0010,

            /// <summary>
            ///     Sets the state of the screen saver. The uiParam parameter specifies TRUE to activate screen saving, or FALSE to
            ///     deactivate it.
            /// </summary>
            SPI_SETSCREENSAVEACTIVE = 0x0011,

            /// <summary>
            ///     Retrieves the current granularity value of the desktop sizing grid. The pvParam parameter must point to an integer
            ///     variable
            ///     that receives the granularity.
            /// </summary>
            SPI_GETGRIDGRANULARITY = 0x0012,

            /// <summary>
            ///     Sets the granularity of the desktop sizing grid to the value of the uiParam parameter.
            /// </summary>
            SPI_SETGRIDGRANULARITY = 0x0013,

            /// <summary>
            ///     Sets the desktop wallpaper. The value of the pvParam parameter determines the new wallpaper. To specify a wallpaper
            ///     bitmap,
            ///     set pvParam to point to a null-terminated string containing the name of a bitmap file. Setting pvParam to ""
            ///     removes the wallpaper.
            ///     Setting pvParam to SETWALLPAPER_DEFAULT or null reverts to the default wallpaper.
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,

            /// <summary>
            ///     Sets the current desktop pattern by causing Windows to read the Pattern= setting from the WIN.INI file.
            /// </summary>
            SPI_SETDESKPATTERN = 0x0015,

            /// <summary>
            ///     Retrieves the keyboard repeat-delay setting, which is a value in the range from 0 (approximately 250 ms delay)
            ///     through 3
            ///     (approximately 1 second delay). The actual delay associated with each value may vary depending on the hardware. The
            ///     pvParam parameter must point to an integer variable that receives the setting.
            /// </summary>
            SPI_GETKEYBOARDDELAY = 0x0016,

            /// <summary>
            ///     Sets the keyboard repeat-delay setting. The uiParam parameter must specify 0, 1, 2, or 3, where zero sets the
            ///     shortest delay
            ///     (approximately 250 ms) and 3 sets the longest delay (approximately 1 second). The actual delay associated with each
            ///     value may
            ///     vary depending on the hardware.
            /// </summary>
            SPI_SETKEYBOARDDELAY = 0x0017,

            /// <summary>
            ///     Sets or retrieves the height, in pixels, of an icon cell.
            ///     To set this value, set uiParam to the new value and set pvParam to null. You cannot set this value to less than
            ///     SM_CYICON.
            ///     To retrieve this value, pvParam must point to an integer that receives the current value.
            /// </summary>
            SPI_ICONVERTICALSPACING = 0x0018,

            /// <summary>
            ///     Determines whether icon-title wrapping is enabled. The pvParam parameter must point to a bool variable that
            ///     receives TRUE
            ///     if enabled, or FALSE otherwise.
            /// </summary>
            SPI_GETICONTITLEWRAP = 0x0019,

            /// <summary>
            ///     Turns icon-title wrapping on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
            /// </summary>
            SPI_SETICONTITLEWRAP = 0x001A,

            /// <summary>
            ///     Determines whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.
            ///     The pvParam parameter must point to a bool variable that receives TRUE if left-aligned, or FALSE otherwise.
            /// </summary>
            SPI_GETMENUDROPALIGNMENT = 0x001B,

            /// <summary>
            ///     Sets the alignment value of pop-up menus. The uiParam parameter specifies TRUE for right alignment, or FALSE for
            ///     left alignment.
            /// </summary>
            SPI_SETMENUDROPALIGNMENT = 0x001C,

            /// <summary>
            ///     Sets the width of the double-click rectangle to the value of the uiParam parameter.
            ///     The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be
            ///     registered
            ///     as a double-click.
            ///     To retrieve the width of the double-click rectangle, call GetSystemMetrics with the SM_CXDOUBLECLK flag.
            /// </summary>
            SPI_SETDOUBLECLKWIDTH = 0x001D,

            /// <summary>
            ///     Sets the height of the double-click rectangle to the value of the uiParam parameter.
            ///     The double-click rectangle is the rectangle within which the second click of a double-click must fall for it to be
            ///     registered
            ///     as a double-click.
            ///     To retrieve the height of the double-click rectangle, call GetSystemMetrics with the SM_CYDOUBLECLK flag.
            /// </summary>
            SPI_SETDOUBLECLKHEIGHT = 0x001E,

            /// <summary>
            ///     Retrieves the logical font information for the current icon-title font. The uiParam parameter specifies the size of
            ///     a LOGFONT structure,
            ///     and the pvParam parameter must point to the LOGFONT structure to fill in.
            /// </summary>
            SPI_GETICONTITLELOGFONT = 0x001F,

            /// <summary>
            ///     Sets the double-click time for the mouse to the value of the uiParam parameter. The double-click time is the
            ///     maximum number
            ///     of milliseconds that can occur between the first and second clicks of a double-click. You can also call the
            ///     SetDoubleClickTime
            ///     function to set the double-click time. To get the current double-click time, call the GetDoubleClickTime function.
            /// </summary>
            SPI_SETDOUBLECLICKTIME = 0x0020,

            /// <summary>
            ///     Swaps or restores the meaning of the left and right mouse buttons. The uiParam parameter specifies TRUE to swap the
            ///     meanings
            ///     of the buttons, or FALSE to restore their original meanings.
            /// </summary>
            SPI_SETMOUSEBUTTONSWAP = 0x0021,

            /// <summary>
            ///     Sets the font that is used for icon titles. The uiParam parameter specifies the size of a LOGFONT structure,
            ///     and the pvParam parameter must point to a LOGFONT structure.
            /// </summary>
            SPI_SETICONTITLELOGFONT = 0x0022,

            /// <summary>
            ///     This flag is obsolete. Previous versions of the system use this flag to determine whether ALT+TAB fast task
            ///     switching is enabled.
            ///     For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_GETFASTTASKSWITCH = 0x0023,

            /// <summary>
            ///     This flag is obsolete. Previous versions of the system use this flag to enable or disable ALT+TAB fast task
            ///     switching.
            ///     For Windows 95, Windows 98, and Windows NT version 4.0 and later, fast task switching is always enabled.
            /// </summary>
            SPI_SETFASTTASKSWITCH = 0x0024,

            //#if(WINVER >= 0x0400)
            /// <summary>
            ///     Sets dragging of full windows either on or off. The uiParam parameter specifies TRUE for on, or FALSE for off.
            ///     Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_SETDRAGFULLWINDOWS = 0x0025,

            /// <summary>
            ///     Determines whether dragging of full windows is enabled. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if enabled, or FALSE otherwise.
            ///     Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_GETDRAGFULLWINDOWS = 0x0026,

            /// <summary>
            ///     Retrieves the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point
            ///     to a NONCLIENTMETRICS structure that receives the information. Set the cbSize member of this structure and the
            ///     uiParam parameter
            ///     to sizeof(NONCLIENTMETRICS).
            /// </summary>
            SPI_GETNONCLIENTMETRICS = 0x0029,

            /// <summary>
            ///     Sets the metrics associated with the nonclient area of nonminimized windows. The pvParam parameter must point
            ///     to a NONCLIENTMETRICS structure that contains the new parameters. Set the cbSize member of this structure
            ///     and the uiParam parameter to sizeof(NONCLIENTMETRICS). Also, the lfHeight member of the LOGFONT structure must be a
            ///     negative value.
            /// </summary>
            SPI_SETNONCLIENTMETRICS = 0x002A,

            /// <summary>
            ///     Retrieves the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(MINIMIZEDMETRICS).
            /// </summary>
            SPI_GETMINIMIZEDMETRICS = 0x002B,

            /// <summary>
            ///     Sets the metrics associated with minimized windows. The pvParam parameter must point to a MINIMIZEDMETRICS
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(MINIMIZEDMETRICS).
            /// </summary>
            SPI_SETMINIMIZEDMETRICS = 0x002C,

            /// <summary>
            ///     Retrieves the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that
            ///     receives
            ///     the information. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
            /// </summary>
            SPI_GETICONMETRICS = 0x002D,

            /// <summary>
            ///     Sets the metrics associated with icons. The pvParam parameter must point to an ICONMETRICS structure that contains
            ///     the new parameters. Set the cbSize member of this structure and the uiParam parameter to sizeof(ICONMETRICS).
            /// </summary>
            SPI_SETICONMETRICS = 0x002E,

            /// <summary>
            ///     Sets the size of the work area. The work area is the portion of the screen not obscured by the system taskbar
            ///     or by application desktop toolbars. The pvParam parameter is a pointer to a RECT structure that specifies the new
            ///     work area rectangle,
            ///     expressed in virtual screen coordinates. In a system with multiple display monitors, the function sets the work
            ///     area
            ///     of the monitor that contains the specified rectangle.
            /// </summary>
            SPI_SETWORKAREA = 0x002F,

            /// <summary>
            ///     Retrieves the size of the work area on the primary display monitor. The work area is the portion of the screen not
            ///     obscured
            ///     by the system taskbar or by application desktop toolbars. The pvParam parameter must point to a RECT structure that
            ///     receives
            ///     the coordinates of the work area, expressed in virtual screen coordinates.
            ///     To get the work area of a monitor other than the primary display monitor, call the GetMonitorInfo function.
            /// </summary>
            SPI_GETWORKAREA = 0x0030,

            /// <summary>
            ///     Windows Me/98/95:  Pen windows is being loaded or unloaded. The uiParam parameter is TRUE when loading and FALSE
            ///     when unloading pen windows. The pvParam parameter is null.
            /// </summary>
            SPI_SETPENWINDOWS = 0x0031,

            /// <summary>
            ///     Retrieves information about the HighContrast accessibility feature. The pvParam parameter must point to a
            ///     HIGHCONTRAST structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(HIGHCONTRAST).
            ///     For a general discussion, see remarks.
            ///     Windows NT:  This value is not supported.
            /// </summary>
            /// <remarks>
            ///     There is a difference between the High Contrast color scheme and the High Contrast Mode. The High Contrast color
            ///     scheme changes
            ///     the system colors to colors that have obvious contrast; you switch to this color scheme by using the Display
            ///     Options in the control panel.
            ///     The High Contrast Mode, which uses SPI_GETHIGHCONTRAST and SPI_SETHIGHCONTRAST, advises applications to modify
            ///     their appearance
            ///     for visually-impaired users. It involves such things as audible warning to users and customized color scheme
            ///     (using the Accessibility Options in the control panel). For more information, see HIGHCONTRAST on MSDN.
            ///     For more information on general accessibility features, see Accessibility on MSDN.
            /// </remarks>
            SPI_GETHIGHCONTRAST = 0x0042,

            /// <summary>
            ///     Sets the parameters of the HighContrast accessibility feature. The pvParam parameter must point to a HIGHCONTRAST
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(HIGHCONTRAST).
            ///     Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETHIGHCONTRAST = 0x0043,

            /// <summary>
            ///     Determines whether the user relies on the keyboard instead of the mouse, and wants applications to display keyboard
            ///     interfaces
            ///     that would otherwise be hidden. The pvParam parameter must point to a BOOL variable that receives TRUE
            ///     if the user relies on the keyboard; or FALSE otherwise.
            ///     Windows NT:  This value is not supported.
            /// </summary>
            SPI_GETKEYBOARDPREF = 0x0044,

            /// <summary>
            ///     Sets the keyboard preference. The uiParam parameter specifies TRUE if the user relies on the keyboard instead of
            ///     the mouse,
            ///     and wants applications to display keyboard interfaces that would otherwise be hidden; uiParam is FALSE otherwise.
            ///     Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETKEYBOARDPREF = 0x0045,

            /// <summary>
            ///     Determines whether a screen reviewer utility is running. A screen reviewer utility directs textual information to
            ///     an output device,
            ///     such as a speech synthesizer or Braille display. When this flag is set, an application should provide textual
            ///     information
            ///     in situations where it would otherwise present the information graphically.
            ///     The pvParam parameter is a pointer to a BOOL variable that receives TRUE if a screen reviewer utility is running,
            ///     or FALSE otherwise.
            ///     Windows NT:  This value is not supported.
            /// </summary>
            SPI_GETSCREENREADER = 0x0046,

            /// <summary>
            ///     Determines whether a screen review utility is running. The uiParam parameter specifies TRUE for on, or FALSE for
            ///     off.
            ///     Windows NT:  This value is not supported.
            /// </summary>
            SPI_SETSCREENREADER = 0x0047,

            /// <summary>
            ///     Retrieves the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(ANIMATIONINFO).
            /// </summary>
            SPI_GETANIMATION = 0x0048,

            /// <summary>
            ///     Sets the animation effects associated with user actions. The pvParam parameter must point to an ANIMATIONINFO
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(ANIMATIONINFO).
            /// </summary>
            SPI_SETANIMATION = 0x0049,

            /// <summary>
            ///     Determines whether the font smoothing feature is enabled. This feature uses font antialiasing to make font curves
            ///     appear smoother
            ///     by painting pixels at different gray levels.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is enabled, or FALSE if it is
            ///     not.
            ///     Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_GETFONTSMOOTHING = 0x004A,

            /// <summary>
            ///     Enables or disables the font smoothing feature, which uses font antialiasing to make font curves appear smoother
            ///     by painting pixels at different gray levels.
            ///     To enable the feature, set the uiParam parameter to TRUE. To disable the feature, set uiParam to FALSE.
            ///     Windows 95:  This flag is supported only if Windows Plus! is installed. See SPI_GETWINDOWSEXTENSION.
            /// </summary>
            SPI_SETFONTSMOOTHING = 0x004B,

            /// <summary>
            ///     Sets the width, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new
            ///     value.
            ///     To retrieve the drag width, call GetSystemMetrics with the SM_CXDRAG flag.
            /// </summary>
            SPI_SETDRAGWIDTH = 0x004C,

            /// <summary>
            ///     Sets the height, in pixels, of the rectangle used to detect the start of a drag operation. Set uiParam to the new
            ///     value.
            ///     To retrieve the drag height, call GetSystemMetrics with the SM_CYDRAG flag.
            /// </summary>
            SPI_SETDRAGHEIGHT = 0x004D,

            /// <summary>
            ///     Used internally; applications should not use this value.
            /// </summary>
            SPI_SETHANDHELD = 0x004E,

            /// <summary>
            ///     Retrieves the time-out value for the low-power phase of screen saving. The pvParam parameter must point to an
            ///     integer variable
            ///     that receives the value. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETLOWPOWERTIMEOUT = 0x004F,

            /// <summary>
            ///     Retrieves the time-out value for the power-off phase of screen saving. The pvParam parameter must point to an
            ///     integer variable
            ///     that receives the value. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETPOWEROFFTIMEOUT = 0x0050,

            /// <summary>
            ///     Sets the time-out value, in seconds, for the low-power phase of screen saving. The uiParam parameter specifies the
            ///     new value.
            ///     The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETLOWPOWERTIMEOUT = 0x0051,

            /// <summary>
            ///     Sets the time-out value, in seconds, for the power-off phase of screen saving. The uiParam parameter specifies the
            ///     new value.
            ///     The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETPOWEROFFTIMEOUT = 0x0052,

            /// <summary>
            ///     Determines whether the low-power phase of screen saving is enabled. The pvParam parameter must point to a BOOL
            ///     variable
            ///     that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETLOWPOWERACTIVE = 0x0053,

            /// <summary>
            ///     Determines whether the power-off phase of screen saving is enabled. The pvParam parameter must point to a BOOL
            ///     variable
            ///     that receives TRUE if enabled, or FALSE if disabled. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_GETPOWEROFFACTIVE = 0x0054,

            /// <summary>
            ///     Activates or deactivates the low-power phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate.
            ///     The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETLOWPOWERACTIVE = 0x0055,

            /// <summary>
            ///     Activates or deactivates the power-off phase of screen saving. Set uiParam to 1 to activate, or zero to deactivate.
            ///     The pvParam parameter must be null. This flag is supported for 32-bit applications only.
            ///     Windows NT, Windows Me/98:  This flag is supported for 16-bit and 32-bit applications.
            ///     Windows 95:  This flag is supported for 16-bit applications only.
            /// </summary>
            SPI_SETPOWEROFFACTIVE = 0x0056,

            /// <summary>
            ///     Reloads the system cursors. Set the uiParam parameter to zero and the pvParam parameter to null.
            /// </summary>
            SPI_SETCURSORS = 0x0057,

            /// <summary>
            ///     Reloads the system icons. Set the uiParam parameter to zero and the pvParam parameter to null.
            /// </summary>
            SPI_SETICONS = 0x0058,

            /// <summary>
            ///     Retrieves the input locale identifier for the system default input language. The pvParam parameter must point
            ///     to an HKL variable that receives this value. For more information, see Languages, Locales, and Keyboard Layouts on
            ///     MSDN.
            /// </summary>
            SPI_GETDEFAULTINPUTLANG = 0x0059,

            /// <summary>
            ///     Sets the default input language for the system shell and applications. The specified language must be displayable
            ///     using the current system character set. The pvParam parameter must point to an HKL variable that contains
            ///     the input locale identifier for the default language. For more information, see Languages, Locales, and Keyboard
            ///     Layouts on MSDN.
            /// </summary>
            SPI_SETDEFAULTINPUTLANG = 0x005A,

            /// <summary>
            ///     Sets the hot key set for switching between input languages. The uiParam and pvParam parameters are not used.
            ///     The value sets the shortcut keys in the keyboard property sheets by reading the registry again. The registry must
            ///     be set before this flag is used. the path in the registry is \HKEY_CURRENT_USER\keyboard layout\toggle. Valid
            ///     values are "1" = ALT+SHIFT, "2" = CTRL+SHIFT, and "3" = none.
            /// </summary>
            SPI_SETLANGTOGGLE = 0x005B,

            /// <summary>
            ///     Windows 95:  Determines whether the Windows extension, Windows Plus!, is installed. Set the uiParam parameter to 1.
            ///     The pvParam parameter is not used. The function returns TRUE if the extension is installed, or FALSE if it is not.
            /// </summary>
            SPI_GETWINDOWSEXTENSION = 0x005C,

            /// <summary>
            ///     Enables or disables the Mouse Trails feature, which improves the visibility of mouse cursor movements by briefly
            ///     showing
            ///     a trail of cursors and quickly erasing them.
            ///     To disable the feature, set the uiParam parameter to zero or 1. To enable the feature, set uiParam to a value
            ///     greater than 1
            ///     to indicate the number of cursors drawn in the trail.
            ///     Windows 2000/NT:  This value is not supported.
            /// </summary>
            SPI_SETMOUSETRAILS = 0x005D,

            /// <summary>
            ///     Determines whether the Mouse Trails feature is enabled. This feature improves the visibility of mouse cursor
            ///     movements
            ///     by briefly showing a trail of cursors and quickly erasing them.
            ///     The pvParam parameter must point to an integer variable that receives a value. If the value is zero or 1, the
            ///     feature is disabled.
            ///     If the value is greater than 1, the feature is enabled and the value indicates the number of cursors drawn in the
            ///     trail.
            ///     The uiParam parameter is not used.
            ///     Windows 2000/NT:  This value is not supported.
            /// </summary>
            SPI_GETMOUSETRAILS = 0x005E,

            /// <summary>
            ///     Windows Me/98:  Used internally; applications should not use this flag.
            /// </summary>
            SPI_SETSCREENSAVERRUNNING = 0x0061,

            /// <summary>
            ///     Same as SPI_SETSCREENSAVERRUNNING.
            /// </summary>
            SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING,
            //#endif /* WINVER >= 0x0400 */

            /// <summary>
            ///     Retrieves information about the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(FILTERKEYS).
            /// </summary>
            SPI_GETFILTERKEYS = 0x0032,

            /// <summary>
            ///     Sets the parameters of the FilterKeys accessibility feature. The pvParam parameter must point to a FILTERKEYS
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(FILTERKEYS).
            /// </summary>
            SPI_SETFILTERKEYS = 0x0033,

            /// <summary>
            ///     Retrieves information about the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(TOGGLEKEYS).
            /// </summary>
            SPI_GETTOGGLEKEYS = 0x0034,

            /// <summary>
            ///     Sets the parameters of the ToggleKeys accessibility feature. The pvParam parameter must point to a TOGGLEKEYS
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(TOGGLEKEYS).
            /// </summary>
            SPI_SETTOGGLEKEYS = 0x0035,

            /// <summary>
            ///     Retrieves information about the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(MOUSEKEYS).
            /// </summary>
            SPI_GETMOUSEKEYS = 0x0036,

            /// <summary>
            ///     Sets the parameters of the MouseKeys accessibility feature. The pvParam parameter must point to a MOUSEKEYS
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(MOUSEKEYS).
            /// </summary>
            SPI_SETMOUSEKEYS = 0x0037,

            /// <summary>
            ///     Determines whether the Show Sounds accessibility flag is on or off. If it is on, the user requires an application
            ///     to present information visually in situations where it would otherwise present the information only in audible
            ///     form.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE if the feature is on, or FALSE if it is off.
            ///     Using this value is equivalent to calling GetSystemMetrics (SM_SHOWSOUNDS). That is the recommended call.
            /// </summary>
            SPI_GETSHOWSOUNDS = 0x0038,

            /// <summary>
            ///     Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_SETSHOWSOUNDS = 0x0039,

            /// <summary>
            ///     Retrieves information about the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS
            ///     structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(STICKYKEYS).
            /// </summary>
            SPI_GETSTICKYKEYS = 0x003A,

            /// <summary>
            ///     Sets the parameters of the StickyKeys accessibility feature. The pvParam parameter must point to a STICKYKEYS
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(STICKYKEYS).
            /// </summary>
            SPI_SETSTICKYKEYS = 0x003B,

            /// <summary>
            ///     Retrieves information about the time-out period associated with the accessibility features. The pvParam parameter
            ///     must point
            ///     to an ACCESSTIMEOUT structure that receives the information. Set the cbSize member of this structure and the
            ///     uiParam parameter
            ///     to sizeof(ACCESSTIMEOUT).
            /// </summary>
            SPI_GETACCESSTIMEOUT = 0x003C,

            /// <summary>
            ///     Sets the time-out period associated with the accessibility features. The pvParam parameter must point to an
            ///     ACCESSTIMEOUT
            ///     structure that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(ACCESSTIMEOUT).
            /// </summary>
            SPI_SETACCESSTIMEOUT = 0x003D,

            //#if(WINVER >= 0x0400)
            /// <summary>
            ///     Windows Me/98/95:  Retrieves information about the SerialKeys accessibility feature. The pvParam parameter must
            ///     point
            ///     to a SERIALKEYS structure that receives the information. Set the cbSize member of this structure and the uiParam
            ///     parameter
            ///     to sizeof(SERIALKEYS).
            ///     Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
            /// </summary>
            SPI_GETSERIALKEYS = 0x003E,

            /// <summary>
            ///     Windows Me/98/95:  Sets the parameters of the SerialKeys accessibility feature. The pvParam parameter must point
            ///     to a SERIALKEYS structure that contains the new parameters. Set the cbSize member of this structure and the uiParam
            ///     parameter
            ///     to sizeof(SERIALKEYS).
            ///     Windows Server 2003, Windows XP/2000/NT:  Not supported. The user controls this feature through the control panel.
            /// </summary>
            SPI_SETSERIALKEYS = 0x003F,
            //#endif /* WINVER >= 0x0400 */ 

            /// <summary>
            ///     Retrieves information about the SoundSentry accessibility feature. The pvParam parameter must point to a
            ///     SOUNDSENTRY structure
            ///     that receives the information. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_GETSOUNDSENTRY = 0x0040,

            /// <summary>
            ///     Sets the parameters of the SoundSentry accessibility feature. The pvParam parameter must point to a SOUNDSENTRY
            ///     structure
            ///     that contains the new parameters. Set the cbSize member of this structure and the uiParam parameter to
            ///     sizeof(SOUNDSENTRY).
            /// </summary>
            SPI_SETSOUNDSENTRY = 0x0041,

            //#if(_WIN32_WINNT >= 0x0400)
            /// <summary>
            ///     Determines whether the snap-to-default-button feature is enabled. If enabled, the mouse cursor automatically moves
            ///     to the default button, such as OK or Apply, of a dialog box. The pvParam parameter must point to a BOOL variable
            ///     that receives TRUE if the feature is on, or FALSE if it is off.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETSNAPTODEFBUTTON = 0x005F,

            /// <summary>
            ///     Enables or disables the snap-to-default-button feature. If enabled, the mouse cursor automatically moves to the
            ///     default button,
            ///     such as OK or Apply, of a dialog box. Set the uiParam parameter to TRUE to enable the feature, or FALSE to disable
            ///     it.
            ///     Applications should use the ShowWindow function when displaying a dialog box so the dialog manager can position the
            ///     mouse cursor.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETSNAPTODEFBUTTON = 0x0060,
            //#endif /* _WIN32_WINNT >= 0x0400 */

            //#if (_WIN32_WINNT >= 0x0400) || (_WIN32_WINDOWS > 0x0400)
            /// <summary>
            ///     Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a int variable that receives the width.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERWIDTH = 0x0062,

            /// <summary>
            ///     Retrieves the width, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a int variable that receives the width.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERWIDTH = 0x0063,

            /// <summary>
            ///     Retrieves the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a int variable that receives the height.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERHEIGHT = 0x0064,

            /// <summary>
            ///     Sets the height, in pixels, of the rectangle within which the mouse pointer has to stay for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. Set the uiParam parameter to the new height.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERHEIGHT = 0x0065,

            /// <summary>
            ///     Retrieves the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. The pvParam parameter must point to a int variable that receives the time.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETMOUSEHOVERTIME = 0x0066,

            /// <summary>
            ///     Sets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle for TrackMouseEvent
            ///     to generate a WM_MOUSEHOVER message. This is used only if you pass HOVER_DEFAULT in the dwHoverTime parameter in
            ///     the call to TrackMouseEvent. Set the uiParam parameter to the new time.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETMOUSEHOVERTIME = 0x0067,

            /// <summary>
            ///     Retrieves the number of lines to scroll when the mouse wheel is rotated. The pvParam parameter must point
            ///     to a int variable that receives the number of lines. The default value is 3.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETWHEELSCROLLLINES = 0x0068,

            /// <summary>
            ///     Sets the number of lines to scroll when the mouse wheel is rotated. The number of lines is set from the uiParam
            ///     parameter.
            ///     The number of lines is the suggested number of lines to scroll when the mouse wheel is rolled without using
            ///     modifier keys.
            ///     If the number is 0, then no scrolling should occur. If the number of lines to scroll is greater than the number of
            ///     lines viewable,
            ///     and in particular if it is WHEEL_PAGESCROLL (#defined as int_MAX), the scroll operation should be interpreted
            ///     as clicking once in the page down or page up regions of the scroll bar.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETWHEELSCROLLLINES = 0x0069,

            /// <summary>
            ///     Retrieves the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor
            ///     is
            ///     over a submenu item. The pvParam parameter must point to a DWORD variable that receives the time of the delay.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_GETMENUSHOWDELAY = 0x006A,

            /// <summary>
            ///     Sets uiParam to the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse
            ///     cursor is
            ///     over a submenu item.
            ///     Windows 95:  Not supported.
            /// </summary>
            SPI_SETMENUSHOWDELAY = 0x006B,

            /// <summary>
            ///     Determines whether the IME status window is visible (on a per-user basis). The pvParam parameter must point to a
            ///     BOOL variable
            ///     that receives TRUE if the status window is visible, or FALSE if it is not.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETSHOWIMEUI = 0x006E,

            /// <summary>
            ///     Sets whether the IME status window is visible or not on a per-user basis. The uiParam parameter specifies TRUE for
            ///     on or FALSE for off.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETSHOWIMEUI = 0x006F,
            //#endif

            //#if(WINVER >= 0x0500)
            /// <summary>
            ///     Retrieves the current mouse speed. The mouse speed determines how far the pointer will move based on the distance
            ///     the mouse moves.
            ///     The pvParam parameter must point to an integer that receives a value which ranges between 1 (slowest) and 20
            ///     (fastest).
            ///     A value of 10 is the default. The value can be set by an end user using the mouse control panel application or
            ///     by an application using SPI_SETMOUSESPEED.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSESPEED = 0x0070,

            /// <summary>
            ///     Sets the current mouse speed. The pvParam parameter is an integer between 1 (slowest) and 20 (fastest). A value of
            ///     10 is the default.
            ///     This value is typically set using the mouse control panel application.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSESPEED = 0x0071,

            /// <summary>
            ///     Determines whether a screen saver is currently running on the window station of the calling process.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE if a screen saver is currently running, or
            ///     FALSE otherwise.
            ///     Note that only the interactive window station, "WinSta0", can have a screen saver running.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETSCREENSAVERRUNNING = 0x0072,

            /// <summary>
            ///     Retrieves the full path of the bitmap file for the desktop wallpaper. The pvParam parameter must point to a buffer
            ///     that receives a null-terminated path string. Set the uiParam parameter to the size, in characters, of the pvParam
            ///     buffer. The returned string will not exceed MAX_PATH characters. If there is no desktop wallpaper, the returned
            ///     string is empty.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,
            //#endif /* WINVER >= 0x0500 */

            //#if(WINVER >= 0x0500)
            /// <summary>
            ///     Determines whether active window tracking (activating the window the mouse is on) is on or off. The pvParam
            ///     parameter must point
            ///     to a BOOL variable that receives TRUE for on, or FALSE for off.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWINDOWTRACKING = 0x1000,

            /// <summary>
            ///     Sets active window tracking (activating the window the mouse is on) either on or off. Set pvParam to TRUE for on or
            ///     FALSE for off.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWINDOWTRACKING = 0x1001,

            /// <summary>
            ///     Determines whether the menu animation feature is enabled. This master switch must be on to enable menu animation
            ///     effects.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE if animation is enabled and FALSE if it is
            ///     disabled.
            ///     If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETMENUANIMATION = 0x1002,

            /// <summary>
            ///     Enables or disables menu animation. This master switch must be on for any menu animation to occur.
            ///     The pvParam parameter is a BOOL variable; set pvParam to TRUE to enable animation and FALSE to disable animation.
            ///     If animation is enabled, SPI_GETMENUFADE indicates whether menus use fade or slide animation.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETMENUANIMATION = 0x1003,

            /// <summary>
            ///     Determines whether the slide-open effect for combo boxes is enabled. The pvParam parameter must point to a BOOL
            ///     variable
            ///     that receives TRUE for enabled, or FALSE for disabled.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETCOMBOBOXANIMATION = 0x1004,

            /// <summary>
            ///     Enables or disables the slide-open effect for combo boxes. Set the pvParam parameter to TRUE to enable the gradient
            ///     effect,
            ///     or FALSE to disable it.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETCOMBOBOXANIMATION = 0x1005,

            /// <summary>
            ///     Determines whether the smooth-scrolling effect for list boxes is enabled. The pvParam parameter must point to a
            ///     BOOL variable
            ///     that receives TRUE for enabled, or FALSE for disabled.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,

            /// <summary>
            ///     Enables or disables the smooth-scrolling effect for list boxes. Set the pvParam parameter to TRUE to enable the
            ///     smooth-scrolling effect,
            ///     or FALSE to disable it.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,

            /// <summary>
            ///     Determines whether the gradient effect for window title bars is enabled. The pvParam parameter must point to a BOOL
            ///     variable
            ///     that receives TRUE for enabled, or FALSE for disabled. For more information about the gradient effect, see the
            ///     GetSysColor function.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETGRADIENTCAPTIONS = 0x1008,

            /// <summary>
            ///     Enables or disables the gradient effect for window title bars. Set the pvParam parameter to TRUE to enable it, or
            ///     FALSE to disable it.
            ///     The gradient effect is possible only if the system has a color depth of more than 256 colors. For more information
            ///     about
            ///     the gradient effect, see the GetSysColor function.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETGRADIENTCAPTIONS = 0x1009,

            /// <summary>
            ///     Determines whether menu access keys are always underlined. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if menu access keys are always underlined, and FALSE if they are underlined only when the menu is activated by the
            ///     keyboard.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETKEYBOARDCUES = 0x100A,

            /// <summary>
            ///     Sets the underlining of menu access key letters. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to
            ///     always underline menu
            ///     access keys, or FALSE to underline menu access keys only when the menu is activated from the keyboard.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETKEYBOARDCUES = 0x100B,

            /// <summary>
            ///     Same as SPI_GETKEYBOARDCUES.
            /// </summary>
            SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES,

            /// <summary>
            ///     Same as SPI_SETKEYBOARDCUES.
            /// </summary>
            SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES,

            /// <summary>
            ///     Determines whether windows activated through active window tracking will be brought to the top. The pvParam
            ///     parameter must point
            ///     to a BOOL variable that receives TRUE for on, or FALSE for off.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWNDTRKZORDER = 0x100C,

            /// <summary>
            ///     Determines whether or not windows activated through active window tracking should be brought to the top. Set
            ///     pvParam to TRUE
            ///     for on or FALSE for off.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWNDTRKZORDER = 0x100D,

            /// <summary>
            ///     Determines whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. The
            ///     pvParam parameter
            ///     must point to a BOOL variable that receives TRUE for enabled, or FALSE for disabled.
            ///     Hot tracking means that when the cursor moves over an item, it is highlighted but not selected. You can query this
            ///     value to decide
            ///     whether to use hot tracking in the user interface of your application.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETHOTTRACKING = 0x100E,

            /// <summary>
            ///     Enables or disables hot tracking of user-interface elements such as menu names on menu bars. Set the pvParam
            ///     parameter to TRUE
            ///     to enable it, or FALSE to disable it.
            ///     Hot-tracking means that when the cursor moves over an item, it is highlighted but not selected.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETHOTTRACKING = 0x100F,

            /// <summary>
            ///     Determines whether menu fade animation is enabled. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     when fade animation is enabled and FALSE when it is disabled. If fade animation is disabled, menus use slide
            ///     animation.
            ///     This flag is ignored unless menu animation is enabled, which you can do using the SPI_SETMENUANIMATION flag.
            ///     For more information, see AnimateWindow.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETMENUFADE = 0x1012,

            /// <summary>
            ///     Enables or disables menu fade animation. Set pvParam to TRUE to enable the menu fade effect or FALSE to disable it.
            ///     If fade animation is disabled, menus use slide animation. he The menu fade effect is possible only if the system
            ///     has a color depth of more than 256 colors. This flag is ignored unless SPI_MENUANIMATION is also set. For more
            ///     information,
            ///     see AnimateWindow.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETMENUFADE = 0x1013,

            /// <summary>
            ///     Determines whether the selection fade effect is enabled. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if enabled or FALSE if disabled.
            ///     The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading
            ///     out
            ///     after the menu is dismissed.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETSELECTIONFADE = 0x1014,

            /// <summary>
            ///     Set pvParam to TRUE to enable the selection fade effect or FALSE to disable it.
            ///     The selection fade effect causes the menu item selected by the user to remain on the screen briefly while fading
            ///     out
            ///     after the menu is dismissed. The selection fade effect is possible only if the system has a color depth of more
            ///     than 256 colors.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETSELECTIONFADE = 0x1015,

            /// <summary>
            ///     Determines whether ToolTip animation is enabled. The pvParam parameter must point to a BOOL variable that receives
            ///     TRUE
            ///     if enabled or FALSE if disabled. If ToolTip animation is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTips use
            ///     fade or slide animation.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETTOOLTIPANIMATION = 0x1016,

            /// <summary>
            ///     Set pvParam to TRUE to enable ToolTip animation or FALSE to disable it. If enabled, you can use SPI_SETTOOLTIPFADE
            ///     to specify fade or slide animation.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETTOOLTIPANIMATION = 0x1017,

            /// <summary>
            ///     If SPI_SETTOOLTIPANIMATION is enabled, SPI_GETTOOLTIPFADE indicates whether ToolTip animation uses a fade effect or
            ///     a slide effect.
            ///     The pvParam parameter must point to a BOOL variable that receives TRUE for fade animation or FALSE for slide
            ///     animation.
            ///     For more information on slide and fade effects, see AnimateWindow.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETTOOLTIPFADE = 0x1018,

            /// <summary>
            ///     If the SPI_SETTOOLTIPANIMATION flag is enabled, use SPI_SETTOOLTIPFADE to indicate whether ToolTip animation uses a
            ///     fade effect
            ///     or a slide effect. Set pvParam to TRUE for fade animation or FALSE for slide animation. The tooltip fade effect is
            ///     possible only
            ///     if the system has a color depth of more than 256 colors. For more information on the slide and fade effects,
            ///     see the AnimateWindow function.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETTOOLTIPFADE = 0x1019,

            /// <summary>
            ///     Determines whether the cursor has a shadow around it. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if the shadow is enabled, FALSE if it is disabled. This effect appears only if the system has a color depth of more
            ///     than 256 colors.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETCURSORSHADOW = 0x101A,

            /// <summary>
            ///     Enables or disables a shadow around the cursor. The pvParam parameter is a BOOL variable. Set pvParam to TRUE to
            ///     enable the shadow
            ///     or FALSE to disable the shadow. This effect appears only if the system has a color depth of more than 256 colors.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETCURSORSHADOW = 0x101B,

            //#if(_WIN32_WINNT >= 0x0501)
            /// <summary>
            ///     Retrieves the state of the Mouse Sonar feature. The pvParam parameter must point to a BOOL variable that receives
            ///     TRUE
            ///     if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSESONAR = 0x101C,

            /// <summary>
            ///     Turns the Sonar accessibility feature on or off. This feature briefly shows several concentric circles around the
            ///     mouse pointer
            ///     when the user presses and releases the CTRL key. The pvParam parameter specifies TRUE for on and FALSE for off. The
            ///     default is off.
            ///     For more information, see About Mouse Input.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSESONAR = 0x101D,

            /// <summary>
            ///     Retrieves the state of the Mouse ClickLock feature. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if enabled, or FALSE otherwise. For more information, see About Mouse Input.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSECLICKLOCK = 0x101E,

            /// <summary>
            ///     Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse
            ///     button
            ///     when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam
            ///     parameter specifies
            ///     TRUE for on,
            ///     or FALSE for off. The default is off. For more information, see Remarks and About Mouse Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSECLICKLOCK = 0x101F,

            /// <summary>
            ///     Retrieves the state of the Mouse Vanish feature. The pvParam parameter must point to a BOOL variable that receives
            ///     TRUE
            ///     if enabled or FALSE otherwise. For more information, see About Mouse Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSEVANISH = 0x1020,

            /// <summary>
            ///     Turns the Vanish feature on or off. This feature hides the mouse pointer when the user types; the pointer reappears
            ///     when the user moves the mouse. The pvParam parameter specifies TRUE for on and FALSE for off. The default is off.
            ///     For more information, see About Mouse Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSEVANISH = 0x1021,

            /// <summary>
            ///     Determines whether native User menus have flat menu appearance. The pvParam parameter must point to a BOOL variable
            ///     that returns TRUE if the flat menu appearance is set, or FALSE otherwise.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFLATMENU = 0x1022,

            /// <summary>
            ///     Enables or disables flat menu appearance for native User menus. Set pvParam to TRUE to enable flat menu appearance
            ///     or FALSE to disable it.
            ///     When enabled, the menu bar uses COLOR_MENUBAR for the menubar background, COLOR_MENU for the menu-popup background,
            ///     COLOR_MENUHILIGHT
            ///     for the fill of the current menu selection, and COLOR_HILIGHT for the outline of the current menu selection.
            ///     If disabled, menus are drawn using the same metrics and colors as in Windows 2000 and earlier.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFLATMENU = 0x1023,

            /// <summary>
            ///     Determines whether the drop shadow effect is enabled. The pvParam parameter must point to a BOOL variable that
            ///     returns TRUE
            ///     if enabled or FALSE if disabled.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETDROPSHADOW = 0x1024,

            /// <summary>
            ///     Enables or disables the drop shadow effect. Set pvParam to TRUE to enable the drop shadow effect or FALSE to
            ///     disable it.
            ///     You must also have CS_DROPSHADOW in the window class style.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETDROPSHADOW = 0x1025,

            /// <summary>
            ///     Retrieves a BOOL indicating whether an application can reset the screensaver's timer by calling the SendInput
            ///     function
            ///     to simulate keyboard or mouse input. The pvParam parameter must point to a BOOL variable that receives TRUE
            ///     if the simulated input will be blocked, or FALSE otherwise.
            /// </summary>
            SPI_GETBLOCKSENDINPUTRESETS = 0x1026,

            /// <summary>
            ///     Determines whether an application can reset the screensaver's timer by calling the SendInput function to simulate
            ///     keyboard
            ///     or mouse input. The uiParam parameter specifies TRUE if the screensaver will not be deactivated by simulated input,
            ///     or FALSE if the screensaver will be deactivated by simulated input.
            /// </summary>
            SPI_SETBLOCKSENDINPUTRESETS = 0x1027,
            //#endif /* _WIN32_WINNT >= 0x0501 */

            /// <summary>
            ///     Determines whether UI effects are enabled or disabled. The pvParam parameter must point to a BOOL variable that
            ///     receives TRUE
            ///     if all UI effects are enabled, or FALSE if they are disabled.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETUIEFFECTS = 0x103E,

            /// <summary>
            ///     Enables or disables UI effects. Set the pvParam parameter to TRUE to enable all UI effects or FALSE to disable all
            ///     UI effects.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETUIEFFECTS = 0x103F,

            /// <summary>
            ///     Retrieves the amount of time following user input, in milliseconds, during which the system will not allow
            ///     applications
            ///     to force themselves into the foreground. The pvParam parameter must point to a DWORD variable that receives the
            ///     time.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,

            /// <summary>
            ///     Sets the amount of time following user input, in milliseconds, during which the system does not allow applications
            ///     to force themselves into the foreground. Set pvParam to the new timeout value.
            ///     The calling thread must be able to change the foreground window, otherwise the call fails.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,

            /// <summary>
            ///     Retrieves the active window tracking delay, in milliseconds. The pvParam parameter must point to a DWORD variable
            ///     that receives the time.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,

            /// <summary>
            ///     Sets the active window tracking delay. Set pvParam to the number of milliseconds to delay before activating the
            ///     window
            ///     under the mouse pointer.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,

            /// <summary>
            ///     Retrieves the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch
            ///     request.
            ///     The pvParam parameter must point to a DWORD variable that receives the value.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,

            /// <summary>
            ///     Sets the number of times SetForegroundWindow will flash the taskbar button when rejecting a foreground switch
            ///     request.
            ///     Set pvParam to the number of times to flash.
            ///     Windows NT, Windows 95:  This value is not supported.
            /// </summary>
            SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,

            /// <summary>
            ///     Retrieves the caret width in edit controls, in pixels. The pvParam parameter must point to a DWORD that receives
            ///     this value.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETCARETWIDTH = 0x2006,

            /// <summary>
            ///     Sets the caret width in edit controls. Set pvParam to the desired width, in pixels. The default and minimum value
            ///     is 1.
            ///     Windows NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETCARETWIDTH = 0x2007,

            //#if(_WIN32_WINNT >= 0x0501)
            /// <summary>
            ///     Retrieves the time delay before the primary mouse button is locked. The pvParam parameter must point to DWORD that
            ///     receives
            ///     the time delay. This is only enabled if SPI_SETMOUSECLICKLOCK is set to TRUE. For more information, see About Mouse
            ///     Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_GETMOUSECLICKLOCKTIME = 0x2008,

            /// <summary>
            ///     Turns the Mouse ClickLock accessibility feature on or off. This feature temporarily locks down the primary mouse
            ///     button
            ///     when that button is clicked and held down for the time specified by SPI_SETMOUSECLICKLOCKTIME. The uiParam
            ///     parameter
            ///     specifies TRUE for on, or FALSE for off. The default is off. For more information, see Remarks and About Mouse
            ///     Input on MSDN.
            ///     Windows 2000/NT, Windows 98/95:  This value is not supported.
            /// </summary>
            SPI_SETMOUSECLICKLOCKTIME = 0x2009,

            /// <summary>
            ///     Retrieves the type of font smoothing. The pvParam parameter must point to a int that receives the information.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFONTSMOOTHINGTYPE = 0x200A,

            /// <summary>
            ///     Sets the font smoothing type. The pvParam parameter points to a int that contains either FE_FONTSMOOTHINGSTANDARD,
            ///     if standard anti-aliasing is used, or FE_FONTSMOOTHINGCLEARTYPE, if ClearType is used. The default is
            ///     FE_FONTSMOOTHINGSTANDARD.
            ///     When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise,
            ///     SystemParametersInfo fails.
            /// </summary>
            SPI_SETFONTSMOOTHINGTYPE = 0x200B,

            /// <summary>
            ///     Retrieves a contrast value that is used in ClearType™ smoothing. The pvParam parameter must point to a int
            ///     that receives the information.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,

            /// <summary>
            ///     Sets the contrast value used in ClearType smoothing. The pvParam parameter points to a int that holds the contrast
            ///     value.
            ///     Valid contrast values are from 1000 to 2200. The default value is 1400.
            ///     When using this option, the fWinIni parameter must be set to SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE; otherwise,
            ///     SystemParametersInfo fails.
            ///     SPI_SETFONTSMOOTHINGTYPE must also be set to FE_FONTSMOOTHINGCLEARTYPE.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,

            /// <summary>
            ///     Retrieves the width, in pixels, of the left and right edges of the focus rectangle drawn with DrawFocusRect.
            ///     The pvParam parameter must point to a int.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFOCUSBORDERWIDTH = 0x200E,

            /// <summary>
            ///     Sets the height of the left and right edges of the focus rectangle drawn with DrawFocusRect to the value of the
            ///     pvParam parameter.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFOCUSBORDERWIDTH = 0x200F,

            /// <summary>
            ///     Retrieves the height, in pixels, of the top and bottom edges of the focus rectangle drawn with DrawFocusRect.
            ///     The pvParam parameter must point to a int.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_GETFOCUSBORDERHEIGHT = 0x2010,

            /// <summary>
            ///     Sets the height of the top and bottom edges of the focus rectangle drawn with DrawFocusRect to the value of the
            ///     pvParam parameter.
            ///     Windows 2000/NT, Windows Me/98/95:  This value is not supported.
            /// </summary>
            SPI_SETFOCUSBORDERHEIGHT = 0x2011,

            /// <summary>
            ///     Not implemented.
            /// </summary>
            SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,

            /// <summary>
            ///     Not implemented.
            /// </summary>
            SPI_SETFONTSMOOTHINGORIENTATION = 0x2013
        }

        #endregion // SPI

        /// <summary>
        /// RECT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// The left
            /// </summary>
            public int Left;
            /// <summary>
            /// The top
            /// </summary>
            public int Top;
            /// <summary>
            /// The right
            /// </summary>
            public int Right;
            /// <summary>
            /// The bottom
            /// </summary>
            public int Bottom;

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return string.Format("Left = {0}, Top = {1}, Right = {2}, Bottom ={3}",
                    Left, Top, Right, Bottom);
            }

            /// <summary>
            /// Gets the width.
            /// </summary>
            /// <value>
            /// The width.
            /// </value>
            public int Width => Math.Abs(Right - Left);

            /// <summary>
            /// Gets the height.
            /// </summary>
            /// <value>
            /// The height.
            /// </value>
            public int Height => Math.Abs(Bottom - Top);
        }

        /// <summary>
        /// EnumDelegate
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns></returns>
        public delegate bool EnumDelegate(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd">The WND.</param>
        /// <param name="lParam">The parameter.</param>
        /// <returns></returns>
        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// fbCallBack
        /// </summary>
        /// <param name="hWnd">The WND.</param>
        /// <param name="uMsg">The MSG.</param>
        /// <param name="lParam">The parameter.</param>
        /// <param name="lpData">The data.</param>
        /// <returns></returns>
        public delegate int fbCallBack(IntPtr hWnd, int uMsg, int lParam, int lpData);

        /// <summary>
        ///     Enumeration of the different ways of showing a window using
        ///     ShowWindow
        /// </summary>
        public enum WindowShowStyle
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,

            /// <summary>
            ///     Activates and displays a window. If the window is minimized
            ///     or maximized, the system restores it to its original size and
            ///     position. An application should specify this flag when displaying
            ///     the window for the first time.
            /// </summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,

            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,

            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,

            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,

            /// <summary>
            ///     Displays a window in its most recent size and position.
            ///     This value is similar to "ShowNormal", except the window is not
            ///     actived.
            /// </summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,

            /// <summary>
            ///     Activates the window and displays it in its current size
            ///     and position.
            /// </summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,

            /// <summary>
            ///     Minimizes the specified window and activates the next
            ///     top-level window in the Z order.
            /// </summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,

            /// <summary>
            ///     Displays the window as a minimized window. This value is
            ///     similar to "ShowMinimized", except the window is not activated.
            /// </summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,

            /// <summary>
            ///     Displays the window in its current size and position. This
            ///     value is similar to "Show", except the window is not activated.
            /// </summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,

            /// <summary>
            ///     Activates and displays the window. If the window is
            ///     minimized or maximized, the system restores it to its original size
            ///     and position. An application should specify this flag when restoring
            ///     a minimized window.
            /// </summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,

            /// <summary>
            ///     Sets the show state based on the SW_ value specified in the
            ///     STARTUPINFO structure passed to the CreateProcess function by the
            ///     program that started the application.
            /// </summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,

            /// <summary>
            ///     Windows 2000/XP: Minimizes a window, even if the thread
            ///     that owns the window is hung. This flag should only be used when
            ///     minimizing windows from a different thread.
            /// </summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }

        /// <summary>
        /// KBDLLHOOKSTRUCT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class KBDLLHOOKSTRUCT
        {
            /// <summary>
            /// vkCode
            /// </summary>
            public Int32 vkCode;
            /// <summary>
            /// scanCode
            /// </summary>
            public Int32 scanCode;
            /// <summary>
            /// flags
            /// </summary>
            public Int32 flags;
            /// <summary>
            /// time
            /// </summary>
            public Int32 time;
            /// <summary>
            /// dwExtraInfo
            /// </summary>
            public IntPtr dwExtraInfo;
        };

        /// <summary>
        /// GUITHREADINFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class GUITHREADINFO
        {
            /// <summary>
            /// GUITHREADINFO
            /// </summary>
            public GUITHREADINFO()
            {
                cbSize = Convert.ToInt32(Marshal.SizeOf(this));
            }

            /// <summary>
            /// cbSize
            /// </summary>
            public Int32 cbSize;
            /// <summary>
            /// flags
            /// </summary>
            public Int32 flags;
            /// <summary>
            /// hwndActive
            /// </summary>
            public IntPtr hwndActive;
            /// <summary>
            /// hwndFocus
            /// </summary>
            public IntPtr hwndFocus;
            /// <summary>
            /// hwndCapture
            /// </summary>
            public IntPtr hwndCapture;
            /// <summary>
            /// hwndMenuOwner
            /// </summary>
            public IntPtr hwndMenuOwner;
            /// <summary>
            /// hwndMoveSize
            /// </summary>
            public IntPtr hwndMoveSize;
            /// <summary>
            /// hwndCaret
            /// </summary>
            public IntPtr hwndCaret;
            /// <summary>
            /// rcCaret
            /// </summary>
            public RECT rcCaret;
        }

        /// <summary>
        /// The vk lbutton
        /// Virtual Keys, Standard Set
        /// </summary>
        public const int VK_LBUTTON = 0x01;
        /// <summary>
        /// The vk rbutton
        /// </summary>
        public const int VK_RBUTTON = 0x02;
        /// <summary>
        /// The vk cancel
        /// </summary>
        public const int VK_CANCEL = 0x03;
        /// <summary>
        /// The vk mbutton
        /// </summary>
        public const int VK_MBUTTON = 0x04;    /* NOT contiguous with L & RBUTTON */

        //#if(_WIN32_WINNT >= 0x0500)        
        /// <summary>
        /// The vk xbutto n1
        /// </summary>
        public const int VK_XBUTTON1 = 0x05;    /* NOT contiguous with L & RBUTTON */
        /// <summary>
        /// The vk xbutto n2
        /// </summary>
        public const int VK_XBUTTON2 = 0x06;    /* NOT contiguous with L & RBUTTON */
        //#endif /* _WIN32_WINNT >= 0x0500 */

        /* 0x07 : unassigned */
        /// <summary>
        /// The vk back
        /// </summary>
        public const int VK_BACK = 0x08;
        /// <summary>
        /// The vk tab
        /// </summary>
        public const int VK_TAB = 0x09;

        /* 0x0A - 0x0B : reserved */
        /// <summary>
        /// The vk clear
        /// </summary>
        public const int VK_CLEAR = 0x0C;
        /// <summary>
        /// The vk return
        /// </summary>
        public const int VK_RETURN = 0x0D;
        /// <summary>
        /// The vk shift
        /// </summary>
        public const int VK_SHIFT = 0x10;
        /// <summary>
        /// The vk control
        /// </summary>
        public const int VK_CONTROL = 0x11;
        /// <summary>
        /// The vk menu
        /// </summary>
        public const int VK_MENU = 0x12;
        /// <summary>
        /// The vk pause
        /// </summary>
        public const int VK_PAUSE = 0x13;
        /// <summary>
        /// The vk capital
        /// </summary>
        public const int VK_CAPITAL = 0x14;
        /// <summary>
        /// The vk kana
        /// </summary>
        public const int VK_KANA = 0x15;
        /// <summary>
        /// The vk hangeul
        /// </summary>
        public const int VK_HANGEUL = 0x15;  /* old name - should be here for compatibility */
        /// <summary>
        /// The vk hangul
        /// </summary>
        public const int VK_HANGUL = 0x15;
        /// <summary>
        /// The vk junja
        /// </summary>
        public const int VK_JUNJA = 0x17;
        /// <summary>
        /// The vk final
        /// </summary>
        public const int VK_FINAL = 0x18;
        /// <summary>
        /// The vk hanja
        /// </summary>
        public const int VK_HANJA = 0x19;
        /// <summary>
        /// The vk kanji
        /// </summary>
        public const int VK_KANJI = 0x19;
        /// <summary>
        /// The vk escape
        /// </summary>
        public const int VK_ESCAPE = 0x1B;
        /// <summary>
        /// The vk convert
        /// </summary>
        public const int VK_CONVERT = 0x1C;
        /// <summary>
        /// The vk nonconvert
        /// </summary>
        public const int VK_NONCONVERT = 0x1D;
        /// <summary>
        /// The vk accept
        /// </summary>
        public const int VK_ACCEPT = 0x1E;
        /// <summary>
        /// The vk modechange
        /// </summary>
        public const int VK_MODECHANGE = 0x1F;
        /// <summary>
        /// The vk space
        /// </summary>
        public const int VK_SPACE = 0x20;
        /// <summary>
        /// The vk prior
        /// </summary>
        public const int VK_PRIOR = 0x21;
        /// <summary>
        /// The vk next
        /// </summary>
        public const int VK_NEXT = 0x22;
        /// <summary>
        /// The vk end
        /// </summary>
        public const int VK_END = 0x23;
        /// <summary>
        /// The vk home
        /// </summary>
        public const int VK_HOME = 0x24;
        /// <summary>
        /// The vk left
        /// </summary>
        public const int VK_LEFT = 0x25;
        /// <summary>
        /// The vk up
        /// </summary>
        public const int VK_UP = 0x26;
        /// <summary>
        /// The vk right
        /// </summary>
        public const int VK_RIGHT = 0x27;
        /// <summary>
        /// The vk down
        /// </summary>
        public const int VK_DOWN = 0x28;
        /// <summary>
        /// The vk select
        /// </summary>
        public const int VK_SELECT = 0x29;
        /// <summary>
        /// The vk print
        /// </summary>
        public const int VK_PRINT = 0x2A;
        /// <summary>
        /// The vk execute
        /// </summary>
        public const int VK_EXECUTE = 0x2B;
        /// <summary>
        /// The vk snapshot
        /// </summary>
        public const int VK_SNAPSHOT = 0x2C;
        /// <summary>
        /// The vk insert
        /// </summary>
        public const int VK_INSERT = 0x2D;
        /// <summary>
        /// The vk delete
        /// </summary>
        public const int VK_DELETE = 0x2E;
        /// <summary>
        /// The vk help
        /// </summary>
        public const int VK_HELP = 0x2F;

        /*
        public const int VK_LWIN = 0x5B;CII '0' - '9' (0x30 - 0x39)
    * 0x40 : unassigned * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A) */
        /// <summary>
        /// The vk lwin
        /// </summary>
        public const int VK_LWIN = 0x5B;
        /// <summary>
        /// The vk rwin
        /// </summary>
        public const int VK_RWIN = 0x5C;
        /// <summary>
        /// The vk apps
        /// </summary>
        public const int VK_APPS = 0x5D;

        /* 0x5E : reserved */
        /// <summary>
        /// The vk sleep
        /// </summary>
        public const int VK_SLEEP = 0x5F;
        /// <summary>
        /// The vk numpa d0
        /// </summary>
        public const int VK_NUMPAD0 = 0x60;
        /// <summary>
        /// The vk numpa d1
        /// </summary>
        public const int VK_NUMPAD1 = 0x61;
        /// <summary>
        /// The vk numpa d2
        /// </summary>
        public const int VK_NUMPAD2 = 0x62;
        /// <summary>
        /// The vk numpa d3
        /// </summary>
        public const int VK_NUMPAD3 = 0x63;
        /// <summary>
        /// The vk numpa d4
        /// </summary>
        public const int VK_NUMPAD4 = 0x64;
        /// <summary>
        /// The vk numpa d5
        /// </summary>
        public const int VK_NUMPAD5 = 0x65;
        /// <summary>
        /// The vk numpa d6
        /// </summary>
        public const int VK_NUMPAD6 = 0x66;
        /// <summary>
        /// The vk numpa d7
        /// </summary>
        public const int VK_NUMPAD7 = 0x67;
        /// <summary>
        /// The vk numpa d8
        /// </summary>
        public const int VK_NUMPAD8 = 0x68;
        /// <summary>
        /// The vk numpa d9
        /// </summary>
        public const int VK_NUMPAD9 = 0x69;
        /// <summary>
        /// The vk multiply
        /// </summary>
        public const int VK_MULTIPLY = 0x6A;
        /// <summary>
        /// The vk add
        /// </summary>
        public const int VK_ADD = 0x6B;
        /// <summary>
        /// The vk separator
        /// </summary>
        public const int VK_SEPARATOR = 0x6C;
        /// <summary>
        /// The vk subtract
        /// </summary>
        public const int VK_SUBTRACT = 0x6D;
        /// <summary>
        /// The vk decimal
        /// </summary>
        public const int VK_DECIMAL = 0x6E;
        /// <summary>
        /// The vk divide
        /// </summary>
        public const int VK_DIVIDE = 0x6F;
        /// <summary>
        /// The vk f1
        /// </summary>
        public const int VK_F1 = 0x70;
        /// <summary>
        /// The vk f2
        /// </summary>
        public const int VK_F2 = 0x71;
        /// <summary>
        /// The vk f3
        /// </summary>
        public const int VK_F3 = 0x72;
        /// <summary>
        /// The vk f4
        /// </summary>
        public const int VK_F4 = 0x73;
        /// <summary>
        /// The vk f5
        /// </summary>
        public const int VK_F5 = 0x74;
        /// <summary>
        /// The vk f6
        /// </summary>
        public const int VK_F6 = 0x75;
        /// <summary>
        /// The vk f7
        /// </summary>
        public const int VK_F7 = 0x76;
        /// <summary>
        /// The vk f8
        /// </summary>
        public const int VK_F8 = 0x77;
        /// <summary>
        /// The vk f9
        /// </summary>
        public const int VK_F9 = 0x78;
        /// <summary>
        /// The vk F10
        /// </summary>
        public const int VK_F10 = 0x79;
        /// <summary>
        /// The vk F11
        /// </summary>
        public const int VK_F11 = 0x7A;
        /// <summary>
        /// The vk F12
        /// </summary>
        public const int VK_F12 = 0x7B;
        /// <summary>
        /// The vk F13
        /// </summary>
        public const int VK_F13 = 0x7C;
        /// <summary>
        /// The vk F14
        /// </summary>
        public const int VK_F14 = 0x7D;
        /// <summary>
        /// The vk F15
        /// </summary>
        public const int VK_F15 = 0x7E;
        /// <summary>
        /// The vk F16
        /// </summary>
        public const int VK_F16 = 0x7F;
        /// <summary>
        /// The vk F17
        /// </summary>
        public const int VK_F17 = 0x80;
        /// <summary>
        /// The vk F18
        /// </summary>
        public const int VK_F18 = 0x81;
        /// <summary>
        /// The vk F19
        /// </summary>
        public const int VK_F19 = 0x82;
        /// <summary>
        /// The vk F20
        /// </summary>
        public const int VK_F20 = 0x83;
        /// <summary>
        /// The vk F21
        /// </summary>
        public const int VK_F21 = 0x84;
        /// <summary>
        /// The vk F22
        /// </summary>
        public const int VK_F22 = 0x85;
        /// <summary>
        /// The vk F23
        /// </summary>
        public const int VK_F23 = 0x86;
        /// <summary>
        /// The vk F24
        /// </summary>
        public const int VK_F24 = 0x87;

        /* 0x88 - 0x8F : unassigned */
        /// <summary>
        /// The vk numlock
        /// </summary>
        public const int VK_NUMLOCK = 0x90;
        /// <summary>
        /// The vk scroll
        /// </summary>
        public const int VK_SCROLL = 0x91;

        /* NEC PC-9800 kbd definitions */
        /// <summary>
        /// The vk oem nec equal
        /// </summary>
        public const int VK_OEM_NEC_EQUAL = 0x92;   // '=' key on numpad

        /* Fujitsu/OASYS kbd definitions */
        /// <summary>
        /// The vk oem fj jisho
        /// </summary>
        public const int VK_OEM_FJ_JISHO = 0x92;   // 'Dictionary' key        
        /// <summary>
        /// The vk oem fj masshou
        /// </summary>
        public const int VK_OEM_FJ_MASSHOU = 0x93;   // 'Unregister word' key        
        /// <summary>
        /// The vk oem fj touroku
        /// </summary>
        public const int VK_OEM_FJ_TOUROKU = 0x94;   // 'Register word' key        
        /// <summary>
        /// The vk oem fj loya
        /// </summary>
        public const int VK_OEM_FJ_LOYA = 0x95;   // 'Left OYAYUBI' key        
        /// <summary>
        /// The vk oem fj roya
        /// </summary>
        public const int VK_OEM_FJ_ROYA = 0x96;   // 'Right OYAYUBI' key

        /* 0x97 - 0x9F : unassigned */
        /* VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys. * Used only as parameters to GetAsyncKeyState() and GetKeyState(). * No other API or message will distinguish left and right keys in this way. */
        /// <summary>
        /// The vk lshift
        /// </summary>
        public const int VK_LSHIFT = 0xA0;
        /// <summary>
        /// The vk rshift
        /// </summary>
        public const int VK_RSHIFT = 0xA1;
        /// <summary>
        /// The vk lcontrol
        /// </summary>
        public const int VK_LCONTROL = 0xA2;
        /// <summary>
        /// The vk rcontrol
        /// </summary>
        public const int VK_RCONTROL = 0xA3;
        /// <summary>
        /// The vk lmenu
        /// </summary>
        public const int VK_LMENU = 0xA4;
        /// <summary>
        /// The vk rmenu
        /// </summary>
        public const int VK_RMENU = 0xA5;
        /// <summary>
        /// The vk lalt
        /// </summary>
        public const int VK_LALT = 0xA4;
        /// <summary>
        /// The vk ralt
        /// </summary>
        public const int VK_RALT = 0xA5;

        //#if(_WIN32_WINNT >= 0x0500)        
        /// <summary>
        /// The vk browser back
        /// </summary>
        public const int VK_BROWSER_BACK = 0xA6;
        /// <summary>
        /// The vk browser forward
        /// </summary>
        public const int VK_BROWSER_FORWARD = 0xA7;
        /// <summary>
        /// The vk browser refresh
        /// </summary>
        public const int VK_BROWSER_REFRESH = 0xA8;
        /// <summary>
        /// The vk browser stop
        /// </summary>
        public const int VK_BROWSER_STOP = 0xA9;
        /// <summary>
        /// The vk browser search
        /// </summary>
        public const int VK_BROWSER_SEARCH = 0xAA;
        /// <summary>
        /// The vk browser favorites
        /// </summary>
        public const int VK_BROWSER_FAVORITES = 0xAB;
        /// <summary>
        /// The vk browser home
        /// </summary>
        public const int VK_BROWSER_HOME = 0xAC;
        /// <summary>
        /// The vk volume mute
        /// </summary>
        public const int VK_VOLUME_MUTE = 0xAD;
        /// <summary>
        /// The vk volume down
        /// </summary>
        public const int VK_VOLUME_DOWN = 0xAE;
        /// <summary>
        /// The vk volume up
        /// </summary>
        public const int VK_VOLUME_UP = 0xAF;
        /// <summary>
        /// The vk media next track
        /// </summary>
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        /// <summary>
        /// The vk media previous track
        /// </summary>
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        /// <summary>
        /// The vk media stop
        /// </summary>
        public const int VK_MEDIA_STOP = 0xB2;
        /// <summary>
        /// The vk media play pause
        /// </summary>
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        /// <summary>
        /// The vk launch mail
        /// </summary>
        public const int VK_LAUNCH_MAIL = 0xB4;
        /// <summary>
        /// The vk launch media select
        /// </summary>
        public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        /// <summary>
        /// The vk launch ap p1
        /// </summary>
        public const int VK_LAUNCH_APP1 = 0xB6;
        /// <summary>
        /// The vk launch ap p2
        /// </summary>
        public const int VK_LAUNCH_APP2 = 0xB7;

        //#endif /* _WIN32_WINNT >= 0x0500 */

        /* 0xB8 - 0xB9 : reserved */
        /// <summary>
        /// The vk oem 1
        /// </summary>
        public const int VK_OEM_1 = 0xBA;   // ';:' for US        
        /// <summary>
        /// The vk oem plus
        /// </summary>
        public const int VK_OEM_PLUS = 0xBB;   // '+' any country        
        /// <summary>
        /// The vk oem comma
        /// </summary>
        public const int VK_OEM_COMMA = 0xBC;   // ',' any country        
        /// <summary>
        /// The vk oem minus
        /// </summary>
        public const int VK_OEM_MINUS = 0xBD;   // '-' any country        
        /// <summary>
        /// The vk oem period
        /// </summary>
        public const int VK_OEM_PERIOD = 0xBE;   // '.' any country        
        /// <summary>
        /// The vk oem 2
        /// </summary>
        public const int VK_OEM_2 = 0xBF;   // '/?' for US        
        /// <summary>
        /// The vk oem 3
        /// </summary>
        public const int VK_OEM_3 = 0xC0;   // '`~' for US

        /* 0xC1 - 0xD7 : reserved */
        /* 0xD8 - 0xDA : unassigned */
        /// <summary>
        /// The vk oem 4
        /// </summary>
        public const int VK_OEM_4 = 0xDB;  //  '[{' for US        
        /// <summary>
        /// The vk oem 5
        /// </summary>
        public const int VK_OEM_5 = 0xDC;  //  '\|' for US        
        /// <summary>
        /// The vk oem 6
        /// </summary>
        public const int VK_OEM_6 = 0xDD;  //  ']}' for US        
        /// <summary>
        /// The vk oem 7
        /// </summary>
        public const int VK_OEM_7 = 0xDE;  //  ''"' for US        
        /// <summary>
        /// The vk oem 8
        /// </summary>
        public const int VK_OEM_8 = 0xDF;

        /* 0xE0 : reserved */
        /* Various extended or enhanced keyboards */
        /// <summary>
        /// The vk oem ax
        /// </summary>
        public const int VK_OEM_AX = 0xE1;  //  'AX' key on Japanese AX kbd        
        /// <summary>
        /// The vk oem 102
        /// </summary>
        public const int VK_OEM_102 = 0xE2;  //  "<>" or "\|" on RT 102-key kbd.        
        /// <summary>
        /// The vk icon help
        /// </summary>
        public const int VK_ICO_HELP = 0xE3;  //  Help key on ICO        
        /// <summary>
        /// The vk icon 00
        /// </summary>
        public const int VK_ICO_00 = 0xE4;  //  00 key on ICO

        //#if(WINVER >= 0x0400)        
        /// <summary>
        /// The vk processkey
        /// </summary>
        public const int VK_PROCESSKEY = 0xE5;
        //#endif /* WINVER >= 0x0400 */        
        /// <summary>
        /// The vk icon clear
        /// </summary>
        public const int VK_ICO_CLEAR = 0xE6;
        //#if(_WIN32_WINNT >= 0x0500)        
        /// <summary>
        /// The vk packet
        /// </summary>
        public const int VK_PACKET = 0xE7;
        //#endif /* _WIN32_WINNT >= 0x0500 */

        /* 0xE8 : unassigned */
        /* Nokia/Ericsson definitions */
        /// <summary>
        /// The vk oem reset
        /// </summary>
        public const int VK_OEM_RESET = 0xE9;
        /// <summary>
        /// The vk oem jump
        /// </summary>
        public const int VK_OEM_JUMP = 0xEA;
        /// <summary>
        /// The vk oem p a1
        /// </summary>
        public const int VK_OEM_PA1 = 0xEB;
        /// <summary>
        /// The vk oem p a2
        /// </summary>
        public const int VK_OEM_PA2 = 0xEC;
        /// <summary>
        /// The vk oem p a3
        /// </summary>
        public const int VK_OEM_PA3 = 0xED;
        /// <summary>
        /// The vk oem WSCTRL
        /// </summary>
        public const int VK_OEM_WSCTRL = 0xEE;
        /// <summary>
        /// The vk oem cusel
        /// </summary>
        public const int VK_OEM_CUSEL = 0xEF;
        /// <summary>
        /// The vk oem attn
        /// </summary>
        public const int VK_OEM_ATTN = 0xF0;
        /// <summary>
        /// The vk oem finish
        /// </summary>
        public const int VK_OEM_FINISH = 0xF1;
        /// <summary>
        /// The vk oem copy
        /// </summary>
        public const int VK_OEM_COPY = 0xF2;
        /// <summary>
        /// The vk oem automatic
        /// </summary>
        public const int VK_OEM_AUTO = 0xF3;
        /// <summary>
        /// The vk oem enlw
        /// </summary>
        public const int VK_OEM_ENLW = 0xF4;
        /// <summary>
        /// The vk oem backtab
        /// </summary>
        public const int VK_OEM_BACKTAB = 0xF5;
        /// <summary>
        /// The vk attn
        /// </summary>
        public const int VK_ATTN = 0xF6;
        /// <summary>
        /// The vk crsel
        /// </summary>
        public const int VK_CRSEL = 0xF7;
        /// <summary>
        /// The vk exsel
        /// </summary>
        public const int VK_EXSEL = 0xF8;
        /// <summary>
        /// The vk ereof
        /// </summary>
        public const int VK_EREOF = 0xF9;
        /// <summary>
        /// The vk play
        /// </summary>
        public const int VK_PLAY = 0xFA;
        /// <summary>
        /// The vk zoom
        /// </summary>
        public const int VK_ZOOM = 0xFB;
        /// <summary>
        /// The vk noname
        /// </summary>
        public const int VK_NONAME = 0xFC;
        /// <summary>
        /// The vk p a1
        /// </summary>
        public const int VK_PA1 = 0xFD;
        /// <summary>
        /// The vk oem clear
        /// </summary>
        public const int VK_OEM_CLEAR = 0xFE;

        /* 0xFF : reserved */
        /* missing letters and numbers for convenience*/
        /// <summary>
        /// The vk 0
        /// </summary>
        public static int VK_0 = 0x30;
        /// <summary>
        /// The vk 1
        /// </summary>
        public static int VK_1 = 0x31;
        /// <summary>
        /// The vk 2
        /// </summary>
        public static int VK_2 = 0x32;
        /// <summary>
        /// The vk 3
        /// </summary>
        public static int VK_3 = 0x33;
        /// <summary>
        /// The vk 4
        /// </summary>
        public static int VK_4 = 0x34;
        /// <summary>
        /// The vk 5
        /// </summary>
        public static int VK_5 = 0x35;
        /// <summary>
        /// The vk 6
        /// </summary>
        public static int VK_6 = 0x36;
        /// <summary>
        /// The vk 7
        /// </summary>
        public static int VK_7 = 0x37;
        /// <summary>
        /// The vk 8
        /// </summary>
        public static int VK_8 = 0x38;
        /// <summary>
        /// The vk 9
        /// </summary>
        public static int VK_9 = 0x39;
        /* 0x40 : unassigned*/
        /// <summary>
        /// The vk a
        /// </summary>
        public static int VK_A = 0x41;
        /// <summary>
        /// The vk b
        /// </summary>
        public static int VK_B = 0x42;
        /// <summary>
        /// The vk c
        /// </summary>
        public static int VK_C = 0x43;
        /// <summary>
        /// The vk d
        /// </summary>
        public static int VK_D = 0x44;
        /// <summary>
        /// The vk e
        /// </summary>
        public static int VK_E = 0x45;
        /// <summary>
        /// The vk f
        /// </summary>
        public static int VK_F = 0x46;
        /// <summary>
        /// The vk g
        /// </summary>
        public static int VK_G = 0x47;
        /// <summary>
        /// The vk h
        /// </summary>
        public static int VK_H = 0x48;
        /// <summary>
        /// The vk i
        /// </summary>
        public static int VK_I = 0x49;
        /// <summary>
        /// The vk j
        /// </summary>
        public static int VK_J = 0x4A;
        /// <summary>
        /// The vk k
        /// </summary>
        public static int VK_K = 0x4B;
        /// <summary>
        /// The vk l
        /// </summary>
        public static int VK_L = 0x4C;
        /// <summary>
        /// The vk m
        /// </summary>
        public static int VK_M = 0x4D;
        /// <summary>
        /// The vk n
        /// </summary>
        public static int VK_N = 0x4E;
        /// <summary>
        /// The vk o
        /// </summary>
        public static int VK_O = 0x4F;
        /// <summary>
        /// The vk p
        /// </summary>
        public static int VK_P = 0x50;
        /// <summary>
        /// The vk q
        /// </summary>
        public static int VK_Q = 0x51;
        /// <summary>
        /// The vk r
        /// </summary>
        public static int VK_R = 0x52;
        /// <summary>
        /// The vk s
        /// </summary>
        public static int VK_S = 0x53;
        /// <summary>
        /// The vk t
        /// </summary>
        public static int VK_T = 0x54;
        /// <summary>
        /// The vk u
        /// </summary>
        public static int VK_U = 0x55;
        /// <summary>
        /// The vk v
        /// </summary>
        public static int VK_V = 0x56;
        /// <summary>
        /// The vk w
        /// </summary>
        public static int VK_W = 0x57;
        /// <summary>
        /// The vk x
        /// </summary>
        public static int VK_X = 0x58;
        /// <summary>
        /// The vk y
        /// </summary>
        public static int VK_Y = 0x59;
        /// <summary>
        /// The vk z
        /// </summary>
        public static int VK_Z = 0x5A;
        /* end virtual kets */
        /// <summary>
        /// The generic all access
        /// </summary>
        public const int GENERIC_ALL_ACCESS = 0x10000000;
        /// <summary>
        /// The wm paint
        /// </summary>
        public const int WM_PAINT = 0x000F;
        /// <summary>
        /// Get text
        /// </summary>
        public const int WM_GETTEXT = 0x000D;
        /// <summary>
        /// The GWL exstyle
        /// </summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>
        /// get richedit text
        /// </summary>
        public const int GWL_ID = -12;
        /// <summary>
        /// The ws ex layered
        /// </summary>
        public const int WS_EX_LAYERED = 0x80000;
        /// <summary>
        /// The lwa alpha
        /// </summary>
        public const int LWA_ALPHA = 0x2;
        /// <summary>
        /// The pw clientonly
        /// </summary>
        public const int PW_CLIENTONLY = 0x00000001;
        /// <summary>
        /// The sw maximize
        /// </summary>
        public const int SW_MAXIMIZE = 3;
        /// <summary>
        /// The GWL style
        /// </summary>
        public const int GWL_STYLE = -16;
        /// <summary>
        /// The ws visible
        /// </summary>
        public const long WS_VISIBLE = 0x10000000;
        /// <summary>
        /// The sw hide
        /// </summary>
        public const int SW_HIDE = 0x00;
        /// <summary>
        /// The sw show
        /// </summary>
        public const int SW_SHOW = 0x06;
        /// <summary>
        /// The maximum allowed
        /// </summary>
        public const int MAXIMUM_ALLOWED = 0x02000000;
        /// <summary>
        /// The wm syscommand
        /// </summary>
        public const int WM_SYSCOMMAND = 0x112;
        /// <summary>
        /// The mf separator
        /// </summary>
        public const int MF_SEPARATOR = 0x800;
        /// <summary>
        /// The mf string
        /// </summary>
        public const int MF_STRING = 0x0;
        /// <summary>
        /// The infinite
        /// </summary>
        public const int INFINITE = 0xFFFFFFF;
        /// <summary>
        /// The srccopy
        /// </summary>
        public const int SRCCOPY = 13369376;

        /// <summary>
        /// Extended Key
        /// </summary>
        public const int KEYEVENTF_EXTENDEDKEY = 0x1;

        /// <summary>
        /// KeyUp
        /// </summary>
        public const int KEYEVENTF_KEYUP = 0x2;

        /// <summary>
        /// The mouseeventf move
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
        /// <summary>
        /// The mouseeventf leftdown
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
        /// <summary>
        /// The mouseeventf leftup
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
        /// <summary>
        /// The mouseeventf rightdown
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
        /// <summary>
        /// The mouseeventf rightup
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
        /// <summary>
        /// The mouseeventf middledown
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
        /// <summary>
        /// The mouseeventf middleup
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
        /// <summary>
        /// The mouseeventf xdown
        /// </summary>
        public const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
        /// <summary>
        /// The mouseeventf xup
        /// </summary>
        public const int MOUSEEVENTF_XUP = 0x0100; /* x button down */
        /// <summary>
        /// The mouseeventf wheel
        /// </summary>
        public const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
        /// <summary>
        /// The mouseeventf virtualdesk
        /// </summary>
        public const int MOUSEEVENTF_VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
        /// <summary>
        /// The mouseeventf absolute
        /// </summary>
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */

        /// <summary>
        /// The wh mouse ll
        /// </summary>
        public const int WH_MOUSE_LL = 14;
        /// <summary>
        /// The wh keyboard ll
        /// </summary>
        public const int WH_KEYBOARD_LL = 13;
        /// <summary>
        /// The wh mouse
        /// </summary>
        public const int WH_MOUSE = 7;
        /// <summary>
        /// The wh keyboard
        /// </summary>
        public const int WH_KEYBOARD = 2;
        /// <summary>
        /// The wm mousemove
        /// </summary>
        public const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// The wm lbuttondown
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// The wm rbuttondown
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// The wm mbuttondown
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x207;
        /// <summary>
        /// The wm lbuttonup
        /// </summary>
        public const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// The wm rbuttonup
        /// </summary>
        public const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// The wm mbuttonup
        /// </summary>
        public const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// The wm lbuttondblclk
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// The wm rbuttondblclk
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// The wm mbuttondblclk
        /// </summary>
        public const int WM_MBUTTONDBLCLK = 0x209;
        /// <summary>
        /// The wm mousewheel
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x020A;
        /// <summary>
        /// The wm keydown
        /// </summary>
        public const int WM_KEYDOWN = 0x100;
        /// <summary>
        /// The wm keyup
        /// </summary>
        public const int WM_KEYUP = 0x101;
        /// <summary>
        /// The wm syskeydown
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x104;
        /// <summary>
        /// The wm syskeyup
        /// </summary>
        public const int WM_SYSKEYUP = 0x105;
        /// <summary>
        /// The LLKHF altdown
        /// </summary>
        public const byte LLKHF_ALTDOWN = 0x20;

        /// <summary>
        /// GetAncestor
        /// </summary>
        public const int GA_ROOT = 2;

        /// <summary>
        /// LowLevelKeyboardProcDelegate
        /// </summary>
        public const int HC_ACTION = 0;

        /// <summary>
        /// GetAsyncKeyState
        /// </summary>
        public const int KEYSTATE_PRESSED = 0x8000;



        /// <summary>
        /// NetResource
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class NetResource
        {
            /// <summary>
            /// The scope
            /// </summary>
            public ResourceScope Scope;
            /// <summary>
            /// The resource type
            /// </summary>
            public ResourceType ResourceType;
            /// <summary>
            /// The display type
            /// </summary>
            public ResourceDisplaytype DisplayType;
            /// <summary>
            /// The usage
            /// </summary>
            public int Usage;
            /// <summary>
            /// The local name
            /// </summary>
            public string LocalName;
            /// <summary>
            /// The remote name
            /// </summary>
            public string RemoteName;
            /// <summary>
            /// The comment
            /// </summary>
            public string Comment;
            /// <summary>
            /// The provider
            /// </summary>
            public string Provider;
        }

        /// <summary>
        /// ResourceScope
        /// </summary>
        public enum ResourceScope : int
        {
            /// <summary>
            /// The connected
            /// </summary>
            Connected = 1,
            /// <summary>
            /// The global network
            /// </summary>
            GlobalNetwork,
            /// <summary>
            /// The remembered
            /// </summary>
            Remembered,
            /// <summary>
            /// The recent
            /// </summary>
            Recent,
            /// <summary>
            /// The context
            /// </summary>
            Context
        };

        /// <summary>
        /// ResourceType
        /// </summary>
        public enum ResourceType : int
        {
            /// <summary>
            /// Any
            /// </summary>
            Any = 0,
            /// <summary>
            /// The disk
            /// </summary>
            Disk = 1,
            /// <summary>
            /// The print
            /// </summary>
            Print = 2,
            /// <summary>
            /// The reserved
            /// </summary>
            Reserved = 8,
        }

        /// <summary>
        /// ResourceDisplaytype
        /// </summary>
        public enum ResourceDisplaytype : int
        {
            /// <summary>
            /// The generic
            /// </summary>
            Generic = 0x0,
            /// <summary>
            /// The domain
            /// </summary>
            Domain = 0x01,
            /// <summary>
            /// The server
            /// </summary>
            Server = 0x02,
            /// <summary>
            /// The share
            /// </summary>
            Share = 0x03,
            /// <summary>
            /// The file
            /// </summary>
            File = 0x04,
            /// <summary>
            /// The group
            /// </summary>
            Group = 0x05,
            /// <summary>
            /// The network
            /// </summary>
            Network = 0x06,
            /// <summary>
            /// The root
            /// </summary>
            Root = 0x07,
            /// <summary>
            /// The shareadmin
            /// </summary>
            Shareadmin = 0x08,
            /// <summary>
            /// The directory
            /// </summary>
            Directory = 0x09,
            /// <summary>
            /// The tree
            /// </summary>
            Tree = 0x0a,
            /// <summary>
            /// The ndscontainer
            /// </summary>
            Ndscontainer = 0x0b
        }


        #region Nested type: DRIVERSTATUS

        /// <summary>
        /// DRIVERSTATUS
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 12)]
        public class DRIVERSTATUS
        {
            /// <summary>
            /// bDriveError
            /// </summary>
            public byte bDriveError;
            /// <summary>
            /// bIDEStatus
            /// </summary>
            public byte bIDEStatus;

            /// <summary>
            /// bReserved
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] bReserved;

            /// <summary>
            /// dwReserved
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] dwReserved;

            /// <summary>
            /// DRIVERSTATUS
            /// </summary>
            public DRIVERSTATUS()
            {
                bReserved = new byte[2];
                dwReserved = new int[2];
            }
        }

        #endregion

        #region Nested type: IDEREGS

        /// <summary>
        /// IDEREGS
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 8)]
        public class IDEREGS
        {
            /// <summary>
            /// bCommandReg
            /// </summary>
            public byte bCommandReg;
            /// <summary>
            /// bCylHighReg
            /// </summary>
            public byte bCylHighReg;
            /// <summary>
            /// bCylLowReg
            /// </summary>
            public byte bCylLowReg;
            /// <summary>
            /// bDriveHeadReg
            /// </summary>
            public byte bDriveHeadReg;
            /// <summary>
            /// bFeaturesReg
            /// </summary>
            public byte bFeaturesReg;
            /// <summary>
            /// bReserved
            /// </summary>
            public byte bReserved;
            /// <summary>
            /// bSectorCountReg
            /// </summary>
            public byte bSectorCountReg;
            /// <summary>
            /// bSectorNumberReg
            /// </summary>
            public byte bSectorNumberReg;
        }

        #endregion

        #region Nested type: IDSECTOR

        /// <summary>
        /// IDSECTOR
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class IDSECTOR
        {
            /// <summary>
            /// bReserved
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 382)]
            public byte[] bReserved;

            /// <summary>
            /// sFirmwareRev
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] sFirmwareRev;

            /// <summary>
            /// sModelNumber
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] sModelNumber;

            /// <summary>
            /// sSerialNumber
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sSerialNumber;

            /// <summary>
            /// ulCurrentSectorCapacity
            /// </summary>
            public int ulCurrentSectorCapacity;
            /// <summary>
            /// ulTotalAddressableSectors
            /// </summary>
            public int ulTotalAddressableSectors;
            /// <summary>
            /// wBS
            /// </summary>
            public short wBS;
            /// <summary>
            /// wBufferclass
            /// </summary>
            public short wBufferclass;
            /// <summary>
            /// wBufferSize
            /// </summary>
            public short wBufferSize;
            /// <summary>
            /// wBytesPerSector
            /// </summary>
            public short wBytesPerSector;
            /// <summary>
            /// wBytesPerTrack
            /// </summary>
            public short wBytesPerTrack;
            /// <summary>
            /// wCapabilities
            /// </summary>
            public short wCapabilities;
            /// <summary>
            /// wDMATiming
            /// </summary>
            public short wDMATiming;
            /// <summary>
            /// wDoubleWordIO
            /// </summary>
            public short wDoubleWordIO;
            /// <summary>
            /// wECCSize
            /// </summary>
            public short wECCSize;
            /// <summary>
            /// wGenConfig
            /// </summary>
            public short wGenConfig;
            /// <summary>
            /// wMoreVendorUnique
            /// </summary>
            public short wMoreVendorUnique;
            /// <summary>
            /// wMultiWordDMA
            /// </summary>
            public short wMultiWordDMA;
            /// <summary>
            /// wMultSectorCapacity
            /// </summary>
            public short wMultSectorCapacity;
            /// <summary>
            /// wMultSectorStuff
            /// </summary>
            public short wMultSectorStuff;
            /// <summary>
            /// wNumCurrentCyls
            /// </summary>
            public short wNumCurrentCyls;
            /// <summary>
            /// wNumCurrentHeads
            /// </summary>
            public short wNumCurrentHeads;
            /// <summary>
            /// wNumCurrentSectorsPerTrack
            /// </summary>
            public short wNumCurrentSectorsPerTrack;
            /// <summary>
            /// wNumCyls
            /// </summary>
            public short wNumCyls;
            /// <summary>
            /// wNumHeads
            /// </summary>
            public short wNumHeads;
            /// <summary>
            /// wPIOTiming
            /// </summary>
            public short wPIOTiming;
            /// <summary>
            /// wReserved
            /// </summary>
            public short wReserved;
            /// <summary>
            /// wReserved1
            /// </summary>
            public short wReserved1;
            /// <summary>
            /// wSectorsPerTrack
            /// </summary>
            public short wSectorsPerTrack;
            /// <summary>
            /// wSingleWordDMA
            /// </summary>
            public short wSingleWordDMA;

            /// <summary>
            /// wVendorUnique
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] wVendorUnique;

            /// <summary>
            /// IDSECTOR
            /// </summary>
            public IDSECTOR()
            {
                wVendorUnique = new short[3];
                bReserved = new byte[382];
                sFirmwareRev = new char[8];
                sSerialNumber = new char[20];
                sModelNumber = new char[40];
            }
        }

        #endregion

        #region Nested type: SENDCMDINPARAMS

        /// <summary>
        /// SENDCMDINPARAMS
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Size = 32)]
        public class SENDCMDINPARAMS
        {
            /// <summary>
            /// bDriveNumber
            /// </summary>
            public byte bDriveNumber;

            /// <summary>
            /// bReserved
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] bReserved;

            /// <summary>
            /// cBufferSize
            /// </summary>
            public int cBufferSize;

            /// <summary>
            /// dwReserved
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;

            /// <summary>
            /// irDriveRegs
            /// </summary>
            public IDEREGS irDriveRegs;

            /// <summary>
            /// SENDCMDINPARAMS
            /// </summary>
            public SENDCMDINPARAMS()
            {
                irDriveRegs = new IDEREGS();
                bReserved = new byte[3];
                dwReserved = new int[4];
            }
        }

        #endregion

        #region Nested type: SENDCMDOUTPARAMS

        /// <summary>
        /// SENDCMDOUTPARAMS
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SENDCMDOUTPARAMS
        {
            /// <summary>
            /// cBufferSize
            /// </summary>
            public int cBufferSize;
            /// <summary>
            /// DStatus
            /// </summary>
            public DRIVERSTATUS DStatus;
            /// <summary>
            /// ids
            /// </summary>
            public IDSECTOR ids;

            /// <summary>
            /// SENDCMDOUTPARAMS
            /// </summary>
            public SENDCMDOUTPARAMS()
            {
                DStatus = new DRIVERSTATUS();
                ids = new IDSECTOR();
            }
        }

        #endregion


        /// <summary>
        /// SPIF
        /// </summary>
        [Flags]
        public enum SPIF
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0x00,
            /// <summary>
            /// SPIF_UPDATEINIFILE. Writes the new system-wide parameter setting to the user profile.
            /// </summary>
            SPIF_UPDATEINIFILE = 0x01,
            /// <summary>
            /// SPIF_SENDCHANGE. Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
            /// </summary>
            SPIF_SENDCHANGE = 0x02,
            /// <summary>
            /// SPIF_SENDWININICHANGE. Same as SPIF_SENDCHANGE.
            /// </summary>
            SPIF_SENDWININICHANGE = 0x02
        }

        /// <summary>
        ///     ANIMATIONINFO specifies animation effects associated with user actions.
        ///     Used with SystemParametersInfo when SPI_GETANIMATION or SPI_SETANIMATION action is specified.
        /// </summary>
        /// <remark>
        ///     The uiParam value must be set to (System.int32)Marshal.SizeOf(typeof(ANIMATIONINFO)) when using this structure.
        /// </remark>
        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            /// <summary>
            ///     Creates an AMINMATIONINFO structure.
            /// </summary>
            /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
            public ANIMATIONINFO(bool iMinAnimate)
            {
                cbSize = GetSize();

                if (iMinAnimate) this.iMinAnimate = 1;
                else this.iMinAnimate = 0;
            }

            /// <summary>
            ///     Always must be set to (System.int32)Marshal.SizeOf(typeof(ANIMATIONINFO)).
            /// </summary>
            public readonly int cbSize;

            /// <summary>
            ///     If non-zero, minimize/restore animation is enabled, otherwise disabled.
            /// </summary>
            private int iMinAnimate;

            /// <summary>
            /// IMinAnimate
            /// </summary>
            public bool IMinAnimate
            {
                get
                {
                    if (iMinAnimate == 0) return false;
                    return true;
                }
                set
                {
                    if (value) iMinAnimate = 1;
                    else iMinAnimate = 0;
                }
            }

            /// <summary>
            /// GetSize
            /// </summary>
            /// <returns></returns>
            public static int GetSize()
            {
                return (int)Marshal.SizeOf(typeof(ANIMATIONINFO));
            }
        }

        /// <summary>
        /// PROFILEINFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PROFILEINFO
        {
            /// <summary>
            /// The dw size
            /// </summary>
            public int dwSize;
            /// <summary>
            /// The dw flags
            /// </summary>
            public int dwFlags;
            /// <summary>
            /// The lp user name
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)] public string lpUserName;
            /// <summary>
            /// The lp profile path
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)] public string lpProfilePath;
            /// <summary>
            /// The lp default path
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)] public string lpDefaultPath;
            /// <summary>
            /// The lp server name
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)] public string lpServerName;
            /// <summary>
            /// The lp policy path
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)] public string lpPolicyPath;
            /// <summary>
            /// The h profile
            /// </summary>
            public IntPtr hProfile;
        }

        /// <summary>
        /// POINT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            /// <summary>
            /// The x
            /// </summary>
            public int x;
            /// <summary>
            /// The y
            /// </summary>
            public int y;
        }

        /// <summary>
        /// MouseHookStruct
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            /// <summary>
            /// The pt
            /// </summary>
            public POINT pt;
            /// <summary>
            /// The HWND
            /// </summary>
            public int hwnd;
            /// <summary>
            /// The w hit test code
            /// </summary>
            public int wHitTestCode;
            /// <summary>
            /// The dw extra information
            /// </summary>
            public int dwExtraInfo;
        }

        /// <summary>
        /// MouseLLHookStruct
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MouseLLHookStruct
        {
            /// <summary>
            /// The pt
            /// </summary>
            public POINT pt;
            /// <summary>
            /// The mouse data
            /// </summary>
            public int mouseData;
            /// <summary>
            /// The flags
            /// </summary>
            public int flags;
            /// <summary>
            /// The time
            /// </summary>
            public int time;
            /// <summary>
            /// The dw extra information
            /// </summary>
            public int dwExtraInfo;
        }

        /// <summary>
        /// KeyboardHookStruct
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            /// <summary>
            /// The vk code
            /// </summary>
            public int vkCode;
            /// <summary>
            /// The scan code
            /// </summary>
            public int scanCode;
            /// <summary>
            /// The flags
            /// </summary>
            public int flags;
            /// <summary>
            /// The time
            /// </summary>
            public int time;
            /// <summary>
            /// The dw extra information
            /// </summary>
            public int dwExtraInfo;
        }
    }
}