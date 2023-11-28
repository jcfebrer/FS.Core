using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FSLibrary;

namespace FSMouseKeyboardLibrary
{

    /// <summary>
    /// Captures global mouse events
    /// </summary>
    public class MouseHook : GlobalHook
    {

        #region MouseEventType Enum

        private enum MouseEventType
        {
            None,
            MouseDown,
            MouseUp,
            DoubleClick,
            MouseWheel,
            MouseMove
        }

        #endregion

        #region Events

        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;

        public event EventHandler Click;
        public event EventHandler DoubleClick;

        #endregion

        #region Constructor

        public MouseHook()
        {

            hookType = Win32API.WH_MOUSE_LL;

        }

        #endregion

        #region Methods

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            
            if (nCode > -1 && (MouseDown != null || MouseUp != null || MouseMove != null))
            {

                Win32API.MouseLLHookStruct mouseHookStruct =
                    (Win32API.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32API.MouseLLHookStruct));

                MouseButtons button = GetButton(wParam);
                MouseEventType eventType = GetEventType(wParam);

                MouseEventArgs e = new MouseEventArgs(
                    button,
                    (eventType == MouseEventType.DoubleClick ? 2 : 1),
                    mouseHookStruct.pt.x,
                    mouseHookStruct.pt.y,
                    (eventType == MouseEventType.MouseWheel ? (short)((mouseHookStruct.mouseData >> 16) & 0xffff) : 0));

                // Prevent multiple Right Click events (this probably happens for popup menus)
                if (button == MouseButtons.Right && mouseHookStruct.flags != 0)
                {
                    eventType = MouseEventType.None;
                }

                switch (eventType)
                {
                    case MouseEventType.MouseDown:
                        if (MouseDown != null)
                        {
                            MouseDown(this, e);
                        }
                        break;
                    case MouseEventType.MouseUp:
                        if (Click != null)
                        {
                            Click(this, new EventArgs());
                        }
                        if (MouseUp != null)
                        {
                            MouseUp(this, e);
                        }
                        break;
                    case MouseEventType.DoubleClick:
                        if (DoubleClick != null)
                        {
                            DoubleClick(this, new EventArgs());
                        }
                        break;
                    case MouseEventType.MouseWheel:
                        if (MouseWheel != null)
                        {
                            MouseWheel(this, e);
                        }
                        break;
                    case MouseEventType.MouseMove:
                        if (MouseMove != null)
                        {
                            MouseMove(this, e);
                        }
                        break;
                    default:
                        break;
                }
                
            }

            return Win32API.CallNextHookEx(handleToHook, nCode, wParam, lParam);

        }

        private MouseButtons GetButton(Int32 wParam)
        {

            switch (wParam)
            {

                case Win32API.WM_LBUTTONDOWN:
                case Win32API.WM_LBUTTONUP:
                case Win32API.WM_LBUTTONDBLCLK:
                    return MouseButtons.Left;
                case Win32API.WM_RBUTTONDOWN:
                case Win32API.WM_RBUTTONUP:
                case Win32API.WM_RBUTTONDBLCLK:
                    return MouseButtons.Right;
                case Win32API.WM_MBUTTONDOWN:
                case Win32API.WM_MBUTTONUP:
                case Win32API.WM_MBUTTONDBLCLK:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;

            }

        }

        private MouseEventType GetEventType(Int32 wParam)
        {

            switch (wParam)
            {

                case Win32API.WM_LBUTTONDOWN:
                case Win32API.WM_RBUTTONDOWN:
                case Win32API.WM_MBUTTONDOWN:
                    return MouseEventType.MouseDown;
                case Win32API.WM_LBUTTONUP:
                case Win32API.WM_RBUTTONUP:
                case Win32API.WM_MBUTTONUP:
                    return MouseEventType.MouseUp;
                case Win32API.WM_LBUTTONDBLCLK:
                case Win32API.WM_RBUTTONDBLCLK:
                case Win32API.WM_MBUTTONDBLCLK:
                    return MouseEventType.DoubleClick;
                case Win32API.WM_MOUSEWHEEL:
                    return MouseEventType.MouseWheel;
                case Win32API.WM_MOUSEMOVE:
                    return MouseEventType.MouseMove;
                default:
                    return MouseEventType.None;

            }
        }

        #endregion
        
    }

}
