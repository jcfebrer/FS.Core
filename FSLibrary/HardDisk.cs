using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary
{
    class HardDisk
    {
        /// <summary>
        /// Devuelve el número de serie del disco duro.
        /// </summary>
        /// <returns></returns>
        public static string SerialNumber()
        {
            var disk = new ManagementObject(@"Win32_LogicalDisk.DeviceId=""C:""");
            return Convert.ToString(disk.Properties["VolumeSerialNumber"].Value);
        }

        /// <summary>
        /// Gets the drive.
        /// </summary>
        /// <param name="cPath">The c path.</param>
        /// <returns></returns>
        public static string GetDrive(string cPath)
        {
            var lcJustDrive = Directory.GetDirectoryRoot(cPath);
            lcJustDrive = TextUtil.Replace(lcJustDrive, @"\", "");
            return lcJustDrive;
        }

        /// <summary>
        /// Devuelve el espacio del disco o tamaño.
        /// </summary>
        /// <param name="tcDrive">Letra de la unidad.</param>
        /// <param name="tnType">Si el tipo es 0 devuelve el espacio libre, si es 1 el tamaño del disco.</param>
        /// <returns></returns>
        public static long DiskSpace(string tcDrive, int tnType = 0)
        {
            long lnRetVal = -1;
            var llFoundDrive = false;
            var lcSize = "-1";
            var lcFreeSpace = "-1";

            tcDrive = GetDrive(tcDrive.Trim()).ToUpper();

            var diskClass = new ManagementClass("Win32_LogicalDisk");
            var disks = diskClass.GetInstances();

            foreach (ManagementObject disk in disks)
            {
                llFoundDrive = (tcDrive.Trim().Length == 0) & (disk["DriveType"].ToString() == "3");

                if (!llFoundDrive) llFoundDrive = disk["Name"].ToString() == tcDrive;


                if (llFoundDrive)
                {
                    var diskProperties = disk.Properties;
                    foreach (var diskProperty in diskProperties)
                    {
                        if (diskProperty.Name == "Size") 
                            lcSize = disk[diskProperty.Name].ToString();

                        if (diskProperty.Name == "FreeSpace") 
                            lcFreeSpace = disk[diskProperty.Name].ToString();
                    }

                    if (tnType == 1)
                        lnRetVal = long.Parse(lcSize);
                    else
                        lnRetVal = long.Parse(lcFreeSpace);
                    return 0;
                }
            }

            return lnRetVal;
        }


        public static long DiskSpace()
        {
            return DiskSpace("", 2);
        }


        public static long DiskSpace(string tcDrive)
        {
            return DiskSpace(tcDrive, 2);
        }


        public static int DriveType(string tcDrive)
        {
            var nRetVal = -1;

            tcDrive = GetDrive(tcDrive.Trim()).ToUpper();

            var query =
                new SelectQuery(@"SELECT Name, DriveType, FreeSpace FROM Win32_LogicalDisk where Name = """ + tcDrive +
                                @"  """);
            var searcher = new ManagementObjectSearcher(query);

            foreach (var drive in searcher.Get()) nRetVal = int.Parse(drive["DriveType"].ToString());
            return nRetVal;
        }
    }
}
