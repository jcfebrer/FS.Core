#region

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace FSFormControls
{
    [ComVisible(false)]
    public class Keyboard
    {
        #region LedKeys enum

        public enum LedKeys
        {
            NumLock = Keys.NumLock,
            CapsLock = Keys.CapsLock,
            ScrollLock = Keys.Scroll
        }

        #endregion

        private const int KEYEVENTF_KEYUP = 0X2;

        public bool CapsLock
        {
            get { return IsCapsLock(); }
            set { SetKeyState(Convert.ToInt64(Keys.CapsLock), value); }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi)]
        public static extern bool SetKeyboardState(byte[] keys);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi)]
        public static extern bool GetKeyboardState(byte[] keys);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private void SetKeyState(long key, bool state)
        {
            var keys = new byte[257];
            GetKeyboardState(keys);

            keys[Convert.ToInt32(key)] = Convert.ToByte(Math.Abs(Convert.ToInt32(state)));
            SetKeyboardState(keys);
        }


        private bool GetKeyState2(long key)
        {
            var keys = new byte[257];
            GetKeyboardState(keys);

            if (keys[Convert.ToInt32(key)] == 1)
                return true;
            return false;
        }


        public void TroggleLed(LedKeys key, bool state)
        {
            var keys = new byte[257];
            GetKeyboardState(keys);

            if (keys[Convert.ToInt32(key)] != Convert.ToInt32(!state) + 1)
            {
                keybd_event(Convert.ToByte(key), 0, 0, 0);
                keybd_event(Convert.ToByte(key), 0, KEYEVENTF_KEYUP, 0);
            }
        }


        private bool IsCapsLock()
        {
            var state = GetKeyState(Convert.ToInt32(Keys.CapsLock));

            if (state == 1)
                return true;
            return false;
        }
    }
}