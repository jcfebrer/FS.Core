using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FSLibraryCore;

namespace FSMouseKeyboardLibraryCore
{

    /// <summary>
    /// Captures global keyboard events
    /// </summary>
    public class KeyboardHook : GlobalHook
    {

        #region Events

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;

        #endregion

        #region Constructor

        public KeyboardHook()
        {

            hookType = Win32API.WH_KEYBOARD_LL;

        }

        #endregion

        #region Methods

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {

            bool handled = false;

            if (nCode > -1 && (KeyDown != null || KeyUp != null || KeyPress != null))
            {

                Win32API.KeyboardHookStruct keyboardHookStruct =
                    (Win32API.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32API.KeyboardHookStruct));

                // Is Control being held down?
                bool control = ((Win32API.GetKeyState(Win32API.VK_LCONTROL) & 0x80) != 0) ||
                               ((Win32API.GetKeyState(Win32API.VK_RCONTROL) & 0x80) != 0);

                // Is Shift being held down?
                bool shift = ((Win32API.GetKeyState(Win32API.VK_LSHIFT) & 0x80) != 0) ||
                             ((Win32API.GetKeyState(Win32API.VK_RSHIFT) & 0x80) != 0);

                // Is Alt being held down?
                bool alt = ((Win32API.GetKeyState(Win32API.VK_LALT) & 0x80) != 0) ||
                           ((Win32API.GetKeyState(Win32API.VK_RALT) & 0x80) != 0);

                // Is CapsLock on?
                bool capslock = (Win32API.GetKeyState(Win32API.VK_CAPITAL) != 0);

                // Create event using keycode and control/shift/alt values found above
                KeyEventArgs e = new KeyEventArgs(
                    (Keys)(
                        keyboardHookStruct.vkCode |
                        (control ? (int)Keys.Control : 0) |
                        (shift ? (int)Keys.Shift : 0) |
                        (alt ? (int)Keys.Alt : 0)
                        ));

                // Handle KeyDown and KeyUp events
                switch (wParam)
                {

                    case Win32API.WM_KEYDOWN:
                    case Win32API.WM_SYSKEYDOWN:
                        if (KeyDown != null)
                        {
                            KeyDown(this, e);
                            handled = handled || e.Handled;
                        }
                        break;
                    case Win32API.WM_KEYUP:
                    case Win32API.WM_SYSKEYUP:
                        if (KeyUp != null)
                        {
                            KeyUp(this, e);
                            handled = handled || e.Handled;
                        }
                        break;

                }

                // Handle KeyPress event
                if (wParam == Win32API.WM_KEYDOWN &&
                   !handled &&
                   !e.SuppressKeyPress &&
                    KeyPress != null)
                {

                    byte[] keyState = new byte[256];
                    byte[] inBuffer = new byte[2];
                    Win32API.GetKeyboardState(keyState);

                    if (Win32API.ToAscii(keyboardHookStruct.vkCode,
                              keyboardHookStruct.scanCode,
                              keyState,
                              inBuffer,
                              keyboardHookStruct.flags) == 1)
                    {

                        char key = (char)inBuffer[0];
                        if ((capslock ^ shift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressEventArgs e2 = new KeyPressEventArgs(key);
                        KeyPress(this, e2);
                        handled = handled || e.Handled;

                    }

                }

            }

            if (handled)
            {
                return 1;
            }
            else
            {
                return Win32API.CallNextHookEx(handleToHook, nCode, wParam, lParam);
            }

        }

        #endregion

    }

}
