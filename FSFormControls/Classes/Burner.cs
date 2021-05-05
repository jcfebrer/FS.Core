#region

using System;
using System.Runtime.InteropServices;
using System.Text;
using FSLibrary;
using FSException;

#endregion

namespace FSFormControls
{
    public class Burner
    {
        private const int CSIDL_CDBURN_AREA = 0X3B;
        private ICDBurn cdBurner;

        public Burner()
        {
            var OS = Environment.OSVersion.Version;
            var XP = new Version(5, 1);

            if (Convert.ToDouble(OS) < Convert.ToDouble(XP)) throw new ExceptionUtil("Requires XP or greater");
            CreateInterface();
        }


        [DllImport("shell32", CharSet = CharSet.Auto)]
        private static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder pszPath);


        [DllImport("shell32", CharSet = CharSet.Auto)]
        private static extern int SHGetSpecialFolderLocation(IntPtr hWndOwner, int nFolder, ref IntPtr pidl);


        private bool CreateInterface()
        {
            var CLSID_CDBurnObj = new Guid("FBEB8A05-BEEE-4442-804e-409d6c4515e9");
            var IID_ICDBurn = new Guid("3d73a659-e5d0-4d42-afc0-5121ba425c8d");

            var comType = Type.GetTypeFromCLSID(CLSID_CDBurnObj);
            if (comType == null) throw new ExceptionUtil("OS not supported");

            object objICDBurn = null;
            objICDBurn = Activator.CreateInstance(comType, null);
            cdBurner = (ICDBurn) objICDBurn;
            objICDBurn = null;

            if (cdBurner != null)
                return true;
            throw new ExceptionUtil("Created NOTHING!!");
        }


        public bool HasRecordableDrive()
        {
            if (cdBurner == null) return false;
            var hasdrive = false;
            var hresult = 0;
            hresult = cdBurner.HasRecordableDrive(out hasdrive);
            if (hresult != 0)
                throw new ExceptionUtil("hasdrive error &H" + Convert.ToString(Convert.ToByte(hresult), 16));
            return hasdrive;
        }


        public string GetDriveLetter()
        {
            if (cdBurner == null) return string.Empty;

            if (HasRecordableDrive() == false) return string.Empty;
            var driveLetter = "????";
            var stringSize = 4;
            var hresult = cdBurner.GetRecorderDriveLetter(out driveLetter, Convert.ToUInt32(stringSize));
            if (hresult != 0)
                throw new ExceptionUtil("getdriveletter error &H" + Convert.ToString(Convert.ToByte(hresult), 16));

            return driveLetter;
        }


        public void Burn(IntPtr h)
        {
            cdBurner.Burn(h);
        }


        public string GetBurnStagingAreaFolder(IntPtr handle)
        {
            var pidl = Marshal.AllocHGlobal(1024);

            SHGetSpecialFolderLocation(handle, CSIDL_CDBURN_AREA, ref pidl);

            string folder = null;

            var path = new StringBuilder(260, 260);


            var result = SHGetPathFromIDList(pidl, path);

            if (result == 0)
                folder = "";
            else
                folder = path.ToString();

            Marshal.FreeHGlobal(pidl);

            return folder;
        }

        #region Nested type: ICDBurn

        [ComImport]
        [Guid("3d73a659-e5d0-4d42-afc0-5121ba425c8d")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ICDBurn
        {
            [PreserveSig]
            int GetRecorderDriveLetter([MarshalAs(UnmanagedType.LPWStr)] out string pszPathBuffer, [In] uint cch);


            [PreserveSig]
            int Burn(IntPtr hWnd);


            [PreserveSig]
            int HasRecordableDrive(out bool pfHasRecorder);
        }

        #endregion
    }
}