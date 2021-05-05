using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using FSLibrary;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook
    {
        #region Private Variables

        protected int _hookType;
        protected int _handleToHook;
        protected bool _isStarted;
        protected Win32API.HookProc _hookCallback;

        #endregion

        #region Properties

        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }

        #endregion

        #region Constructor

        public GlobalHook()
        {

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

        }

        #endregion

        #region Methods

        public void Start()
        {

            if (!_isStarted &&
                _hookType != 0)
            {

                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                _hookCallback = new Win32API.HookProc(HookCallbackProcedure);

                //Metodo 1:
                //_handleToHook = SetWindowsHookEx(
                //    _hookType,
                //    _hookCallback,
                //    Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                //    0);

                //Metodo 2:
                using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
                {
                    _handleToHook = Win32API.SetWindowsHookEx(
                    _hookType,
                    _hookCallback,
                    Win32API.GetModuleHandle(curModule.ModuleName),
                    0);
                }

                // Were we able to sucessfully start hook?
                if (_handleToHook != 0)
                {
                    _isStarted = true;
                }

            }

        }

        public void Stop()
        {

            if (_isStarted)
            {

                Win32API.UnhookWindowsHookEx(_handleToHook);

                _isStarted = false;

            }

        }

        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
           
            // This method must be overriden by each extending hook
            return 0;

        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {

            if (_isStarted)
            {
                Stop();
            }

        }

        #endregion

    }

}
