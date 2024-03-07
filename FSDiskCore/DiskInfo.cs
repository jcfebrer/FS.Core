#region

using FSLibraryCore;
using System;
using System.Runtime.InteropServices;

#endregion

namespace FSDiskCore
{
    public class DiskInfo
    {
        private const int FILE_SHARE_READ = 0X1;
        private const int FILE_SHARE_WRITE = 0X2;
        private const int GENERIC_READ = unchecked((int)0X80000000);
        private const int GENERIC_WRITE = 0X40000000;
        private const int CREATE_NEW = 1;
        private const int OPEN_EXISTING = 3;
        private const int DFP_RECEIVE_DRIVE_DATA = 0X7C088;
        private const int INVALID_HANDLE_VALUE = -1;

        private string SwapChars(char[] sChars)
        {
            string swapCharsReturn = null;
            var i = 0;

            for (i = 0; i <= sChars.Length - 2; i += 2) Array.Reverse(sChars, i, 2);

            swapCharsReturn = new string(sChars).Trim();
            return swapCharsReturn;
        }

        public bool GetHDData(int lDriveNumber, ref string sSerial, ref string sModel, ref string sFirmware)
        {
            return GetHDData(@"\\.\PhysicalDrive" + Convert.ToString(lDriveNumber), ref sSerial, ref sModel, ref sFirmware);
        }

        public bool GetHDData(string physicalDriveName, ref string sSerial, ref string sModel, ref string sFirmware)
        {
            int lDriveNumber = Convert.ToInt32(physicalDriveName.Substring(physicalDriveName.Length - 1, 1));
            var getHDDataReturn = false;
            var hFile = 0;
            var lReturnSize = 0;
            var sci = new Win32API.SENDCMDINPARAMS();
            var sco = new Win32API.SENDCMDOUTPARAMS();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                hFile = Win32API.CreateFile(physicalDriveName, GENERIC_READ | GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
            else
                hFile = Win32API.CreateFile(@"\\.\Smartvsd", 0, 0, 0, CREATE_NEW, 0, 0);

            if (hFile != INVALID_HANDLE_VALUE)
            {
                sci.bDriveNumber = Convert.ToByte(lDriveNumber);
                sci.cBufferSize = Marshal.SizeOf(sco);
                sci.irDriveRegs.bDriveHeadReg = Convert.ToByte(0XA0 | (lDriveNumber << 4));
                sci.irDriveRegs.bCommandReg = Convert.ToByte(0XEC);
                sci.irDriveRegs.bSectorCountReg = 1;
                sci.irDriveRegs.bSectorNumberReg = 1;

                if (
                    Win32API.DeviceIoControl(hFile, DFP_RECEIVE_DRIVE_DATA, out sci, Marshal.SizeOf(sci), out sco,
                        Marshal.SizeOf(sco), ref lReturnSize, 0) != 0)
                {
                    sSerial = SwapChars(sco.ids.sSerialNumber);
                    sModel = SwapChars(sco.ids.sModelNumber);
                    sFirmware = SwapChars(sco.ids.sFirmwareRev);
                    getHDDataReturn = true;
                }

                Win32API.CloseHandle(hFile);
            }

            return getHDDataReturn;
        }
    }
}