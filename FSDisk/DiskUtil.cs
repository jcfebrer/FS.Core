using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDisk
{
    public class DiskUtil
    {
        public static long DiskFreeSpace(string uncPath)
        {
            long FreeBytesAvailable;
            long TotalNumberOfBytes;
            long TotalNumberOfFreeBytes;

            bool success = FSLibrary.Win32API.GetDiskFreeSpaceEx(uncPath,
                                              out FreeBytesAvailable,
                                              out TotalNumberOfBytes,
                                              out TotalNumberOfFreeBytes);
            if (!success)
                throw new System.ComponentModel.Win32Exception();

            return TotalNumberOfFreeBytes;
        }

        public static long DiskSize(string uncPath)
        {
            long FreeBytesAvailable;
            long TotalNumberOfBytes;
            long TotalNumberOfFreeBytes;

            bool success = FSLibrary.Win32API.GetDiskFreeSpaceEx(uncPath,
                                              out FreeBytesAvailable,
                                              out TotalNumberOfBytes,
                                              out TotalNumberOfFreeBytes);
            if (!success)
                throw new System.ComponentModel.Win32Exception();

            return TotalNumberOfBytes;
        }
    }
}
