#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace FSFormControls
{
    public class HardDisk
    {
        private const int VER_PLATFORM_WIN32_NT = 2;
        private const int FILE_SHARE_READ = 0X1;
        private const int FILE_SHARE_WRITE = 0X2;
        private const int GENERIC_READ = unchecked((int) 0X80000000);
        private const int GENERIC_WRITE = 0X40000000;
        private const int CREATE_NEW = 1;
        private const int OPEN_EXISTING = 3;
        private const int DFP_RECEIVE_DRIVE_DATA = 0X7C088;
        private const int INVALID_HANDLE_VALUE = -1;

        [DllImport("kernel32")]
        private static extern int CloseHandle(int hObject);

        [DllImport("kernel32", EntryPoint = "CreateFileA")]
        private static extern int CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode,
            int lpSecurityAttributes, int dwCreationDisposition,
            int dwFlagsAndAttributes, int hTemplateFile);

        [DllImport("kernel32")]
        private static extern int DeviceIoControl(int hDevice, int dwIoControlCode, out SENDCMDINPARAMS lpInBuffer,
            int nInBufferSize, out SENDCMDOUTPARAMS lpOutBuffer,
            int nOutBufferSize, ref int lpBytesReturned, int lpOverlapped);

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
            var getHDDataReturn = false;
            var hFile = 0;
            var lReturnSize = 0;
            var sci = new SENDCMDINPARAMS();
            var sco = new SENDCMDOUTPARAMS();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                hFile = CreateFile(@"\\.\PhysicalDrive" + Convert.ToString(lDriveNumber), GENERIC_READ | GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
            else
                hFile = CreateFile(@"\\.\Smartvsd", 0, 0, 0, CREATE_NEW, 0, 0);

            if (hFile != INVALID_HANDLE_VALUE)
            {
                sci.bDriveNumber = Convert.ToByte(lDriveNumber);
                sci.cBufferSize = Marshal.SizeOf(sco);
                sci.irDriveRegs.bDriveHeadReg = Convert.ToByte(0XA0 | (lDriveNumber << 4));
                sci.irDriveRegs.bCommandReg = Convert.ToByte(0XEC);
                sci.irDriveRegs.bSectorCountReg = 1;
                sci.irDriveRegs.bSectorNumberReg = 1;

                if (
                    DeviceIoControl(hFile, DFP_RECEIVE_DRIVE_DATA, out sci, Marshal.SizeOf(sci), out sco,
                        Marshal.SizeOf(sco), ref lReturnSize, 0) != 0)
                {
                    sSerial = SwapChars(sco.ids.sSerialNumber);
                    sModel = SwapChars(sco.ids.sModelNumber);
                    sFirmware = SwapChars(sco.ids.sFirmwareRev);
                    getHDDataReturn = true;
                }

                CloseHandle(hFile);
            }

            return getHDDataReturn;
        }

        #region Nested type: DRIVERSTATUS

        [StructLayout(LayoutKind.Sequential, Size = 12)]
        public class DRIVERSTATUS
        {
            public byte bDriveError;
            public byte bIDEStatus;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] bReserved;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] dwReserved;

            public DRIVERSTATUS()
            {
                bReserved = new byte[2];
                dwReserved = new int[2];
            }
        }

        #endregion

        #region Nested type: IDEREGS

        [StructLayout(LayoutKind.Sequential, Size = 8)]
        public class IDEREGS
        {
            public byte bCommandReg;
            public byte bCylHighReg;
            public byte bCylLowReg;
            public byte bDriveHeadReg;
            public byte bFeaturesReg;
            public byte bReserved;
            public byte bSectorCountReg;
            public byte bSectorNumberReg;
        }

        #endregion

        #region Nested type: IDSECTOR

        [StructLayout(LayoutKind.Sequential)]
        public class IDSECTOR
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 382)]
            public byte[] bReserved;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public char[] sFirmwareRev;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] sModelNumber;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] sSerialNumber;

            public int ulCurrentSectorCapacity;
            public int ulTotalAddressableSectors;
            public short wBS;
            public short wBufferclass;
            public short wBufferSize;
            public short wBytesPerSector;
            public short wBytesPerTrack;
            public short wCapabilities;
            public short wDMATiming;
            public short wDoubleWordIO;
            public short wECCSize;
            public short wGenConfig;
            public short wMoreVendorUnique;
            public short wMultiWordDMA;
            public short wMultSectorCapacity;
            public short wMultSectorStuff;
            public short wNumCurrentCyls;
            public short wNumCurrentHeads;
            public short wNumCurrentSectorsPerTrack;
            public short wNumCyls;
            public short wNumHeads;
            public short wPIOTiming;
            public short wReserved;
            public short wReserved1;
            public short wSectorsPerTrack;
            public short wSingleWordDMA;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public short[] wVendorUnique;

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

        [StructLayout(LayoutKind.Sequential, Size = 32)]
        public class SENDCMDINPARAMS
        {
            public byte bDriveNumber;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] bReserved;

            public int cBufferSize;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;

            public IDEREGS irDriveRegs;

            public SENDCMDINPARAMS()
            {
                irDriveRegs = new IDEREGS();
                bReserved = new byte[3];
                dwReserved = new int[4];
            }
        }

        #endregion

        #region Nested type: SENDCMDOUTPARAMS

        [StructLayout(LayoutKind.Sequential)]
        public class SENDCMDOUTPARAMS
        {
            public int cBufferSize;
            public DRIVERSTATUS DStatus;
            public IDSECTOR ids;

            public SENDCMDOUTPARAMS()
            {
                DStatus = new DRIVERSTATUS();
                ids = new IDSECTOR();
            }
        }

        #endregion
    }
}