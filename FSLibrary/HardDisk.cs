using System;
using System.IO;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    /// Librería para la gestión del disco duro.
    /// </summary>
    public class HardDisk
    {
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
        /// Información de disco
        /// </summary>
        /// <returns></returns>
        public static string DriveInformation()
        {
            StringBuilder sb = new StringBuilder();

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                double freeSpace = drive.TotalFreeSpace;
                double totalSpace = drive.TotalSize;
                double percentFree = (freeSpace / totalSpace) * 100;
                float num = (float)percentFree;
                
                sb.Append(String.Format("Drive:{0} With {1} % free", drive.Name, num));
                sb.Append(String.Format("Space Remaining:{0}", drive.AvailableFreeSpace));
                sb.Append(String.Format("Percent Free Space:{0}", percentFree));
                sb.Append(String.Format("Space used:{0}", drive.TotalSize));
                sb.Append(String.Format("Type: {0}", drive.DriveType));
            }

            return sb.ToString();
        }
    }
}
