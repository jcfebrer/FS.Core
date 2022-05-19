using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace FSLibrary
{
    /// <summary>
    /// Clase con utilidades relacionadas con los procesos
    /// </summary>
    public class ProcessUtil
    {
        /// <summary>
        /// Determines whether [is already running].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is already running]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlreadyRunning()
        {
            var strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            var sExeName = fileInfo.Name;
            bool bCreatedNew;

            var mutex = new Mutex(true, "Global\\" + sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }

        /// <summary>
        /// Shows the open with dialog.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void ShowOpenWithDialog(string path)
        {
            var args = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "shell32.dll");
            args += ",OpenAs_RunDLL " + path;
            OpenDocument("rundll32.exe", args);
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="DocName">Name of the document.</param>
        /// <returns></returns>
        public static bool OpenDocument(string DocName)
        {
            return OpenDocument(DocName, "");
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        /// <param name="documentName">Name of the document.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static bool OpenDocument(string documentName, string args)
        {
            try
            {
                if (!documentName.Contains("http"))
                {
                    if (!documentName.Contains(@"\"))
                        documentName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                  @"\" + documentName;
                }

                if (args != "")
                    Process.Start(documentName, args);
                else
                    Process.Start(documentName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ejecuta el fichero o url
        /// </summary>
        /// <param name="file">Fichero o Url</param>
        /// <returns></returns>
        public static Process Execute(string file)
        {
            return Process.Start(file);
        }

        /// <summary>
        /// Hides the active window.
        /// </summary>
        public static void HideActiveWindow()
        {
            //ocultamos la ventana selecionada
            //leemos el caption de la ventana
            var handle = Win32API.FindWindow(null, Win32API.GetActiveWindowTitle());

            if (handle != IntPtr.Zero)
            {
                int style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);

                if ((style & Win32API.WS_VISIBLE) != 0)
                {
                    Win32API.ShowWindow(handle, Win32API.SW_HIDE);
                }
            }
        }

        /// <summary>
        /// Shows all process with tittle.
        /// </summary>
        public static void ShowAllProcessWithTittle()
        {
            foreach (Process pr in Process.GetProcesses())
            {
                if (pr.MainWindowTitle != "")
                    ShowProcessByTitle(pr.MainWindowTitle);
            }
        }

        /// <summary>
        /// Shows the process by title.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void ShowProcessByTitle(string title)
        {
            var handle = Win32API.FindWindow(null, title);

            if (handle != IntPtr.Zero)
            {
                Win32API.ShowWindow(handle, Win32API.SW_SHOW);
            }
        }

        /// <summary>
        /// Hides the process by title.
        /// </summary>
        /// <param name="title">The title.</param>
        public static void HideProcessByTitle(string title)
        {
            var handle = Win32API.FindWindow(null, title);

            if (handle != IntPtr.Zero)
            {
                int style = Win32API.GetWindowLong(handle, Win32API.GWL_STYLE);

                if ((style & Win32API.WS_VISIBLE) != 0)
                {
                    Win32API.ShowWindow(handle, Win32API.SW_HIDE);
                }
            }
        }

        /// <summary>
        /// Envia el proceso actual al frente.
        /// </summary>
        public static void ActivateCurrentProcess()
        {
            Process process = Process.GetCurrentProcess();
            if (process == null)
                return;

            ActivateByProcess(process);
        }

        /// <summary>
        /// Envia el proceso 'processName' al frente.
        /// </summary>
        /// <param name="processName"></param>
        public static void ActivateByProcessName(string processName)
        {
            Process process = null;
            Process[] procesess = Process.GetProcessesByName(processName);
            foreach (Process p in procesess)
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    process = p;

            if(process != null)
                ActivateByProcess(process);
        }

        /// <summary>
        /// Activamos el proceso indicado en "windowName".
        /// </summary>
        /// <param name="windowName"></param>
        public static void ActivateByWindowName(string windowName)
        {
            IntPtr hWnd = Win32API.FindWindow(null, windowName);
            if (Win32API.IsIconic(hWnd))
                Win32API.ShowWindow(hWnd, Win32API.WindowShowStyle.Restore);
            Win32API.SetForegroundWindow(hWnd);
        }


        /// <summary>
        /// Activamos el proceso indicado en "process".
        /// </summary>
        /// <param name="process"></param>
        public static void ActivateByProcess(Process process)
        {
            IntPtr hWnd = process.MainWindowHandle;
            if (Win32API.IsIconic(hWnd))
                Win32API.ShowWindow(hWnd, Win32API.WindowShowStyle.Restore);
            Win32API.SetForegroundWindow(hWnd);
        }
    }
}