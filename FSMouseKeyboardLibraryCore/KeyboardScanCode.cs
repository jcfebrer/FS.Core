using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMouseKeyboardLibraryCore
{
    public class KeyboardScanCode
    {
        private int[] _virtualKeyScanCodes = new int[255];

        public KeyboardScanCode()
        {
            Initialize();
        }

        public int VirtualKeyToScanCode(int virtualKey)
        {
            return _virtualKeyScanCodes[virtualKey];
        }
        private void Initialize()
        {
            IntPtr keybHandle = FSLibraryCore.Win32API.GetKeyboardLayout(0);

            // Scroll through the Scan Code (SC) values and get the Virtual Key (VK)
            // values in it. Then, store the SC in each valid VK so it can act as both a 
            // flag that the VK is valid, and it can store the SC value.
            for (int scanCode = 0x01; scanCode <= 0xff; scanCode++)
            {
                int virtualKeyCode = FSLibraryCore.Win32API.MapVirtualKeyEx(
                    scanCode,
                    (int)FSLibraryCore.Win32API.MapType.MAPVK_VSC_TO_VK,
                    keybHandle);
                if (virtualKeyCode != 0)
                {
                    this._virtualKeyScanCodes[virtualKeyCode] = scanCode;
                }
            }

            // Add the special keys that do not get added from the code above
            for (int ke = FSLibraryCore.Win32API.VK_NUMPAD0; ke <= FSLibraryCore.Win32API.VK_NUMPAD9; ke++)
            {
                this._virtualKeyScanCodes[ke] = FSLibraryCore.Win32API.MapVirtualKeyEx(
                    ke,
                    (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC,
                    keybHandle);
            }

            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_DECIMAL] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_DECIMAL, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_DIVIDE] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_DIVIDE, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_TAB] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_TAB, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_CANCEL] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_CANCEL, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_SNAPSHOT] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_SNAPSHOT, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_APPS] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_APPS, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_OEM_102] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_OEM_102, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_LSHIFT] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_LSHIFT, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_RSHIFT] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_RSHIFT, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_LCONTROL] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_LCONTROL, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_RCONTROL] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_RCONTROL, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_LMENU] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_LMENU, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_RMENU] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_RMENU, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_LWIN] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_LWIN, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_RWIN] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_RWIN, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_PAUSE] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_PAUSE, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_VOLUME_UP] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_VOLUME_UP, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_VOLUME_DOWN] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_VOLUME_DOWN, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_VOLUME_MUTE] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_VOLUME_MUTE, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_MEDIA_NEXT_TRACK] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_MEDIA_NEXT_TRACK, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_MEDIA_PREV_TRACK] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_MEDIA_PREV_TRACK, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_MEDIA_PLAY_PAUSE] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_MEDIA_PLAY_PAUSE, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
            this._virtualKeyScanCodes[FSLibraryCore.Win32API.VK_MEDIA_STOP] =
                FSLibraryCore.Win32API.MapVirtualKeyEx(
                    FSLibraryCore.Win32API.VK_MEDIA_STOP, (int)FSLibraryCore.Win32API.MapType.MAPVK_VK_TO_VSC_EX, keybHandle);
        }
    }
}
