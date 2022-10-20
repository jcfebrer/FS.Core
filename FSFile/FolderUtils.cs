using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;

namespace FSFile
{
    public class FolderUtils
    {
        public static void Rename(string file, string newName)
        {
            string origName = newName;
            int f = 1;
            while (System.IO.File.Exists(newName))
            {
                newName = String.Format(origName + " [{0}]", f);
                f++;
            }

            System.IO.Directory.Move(file, newName);
        }


        /// <summary>
        /// Obtenemos la lista de carpetas de la carpeta indicada.
        /// </summary>
        /// <param name="folder"></param>
        public static Folders GetFolders(string folder, Boolean recursive)
        {
            Folders carpetas = new Folders();
            List<DirectoryInfo> flatList = new List<DirectoryInfo>();

            flatList = RecurseFolders(new DirectoryInfo(folder), recursive);

            foreach (DirectoryInfo f in flatList)
            {
                Folder fol = new Folder(f.Name);

                fol.Nombre = f.Name;
                fol.Tamaño = 0;
                fol.FechaArchivo = f.CreationTime;
                fol.Path = f.FullName;

                FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
                fileIOPermission.AllLocalFiles = FileIOPermissionAccess.Read;
                try
                {
                    fileIOPermission.Demand();

                    DirectoryInfo[] dir = f.GetDirectories();
                    if (dir != null && dir.Length > 0)
                        fol.HasSubfolder = true;
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                carpetas.Add(fol);
            }

            return carpetas;
        }

        /// <summary>
        /// Devuelve recursivamente las carpetas existentes en un directorio.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static List<DirectoryInfo> RecurseFolders(DirectoryInfo directory, Boolean recursive)
        {
            List<DirectoryInfo> resultList = new List<DirectoryInfo>();

            try
            {
                resultList.AddRange(directory.GetDirectories());
            }
            catch
            {
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    resultList.AddRange(RecurseFolders(subDirectory, recursive));
                }
            }

            return resultList;
        }

        public static void SaveAsCsv(string fileName, Folders folders)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(@"""Nombre"",""Path"",""Tamaño"",""Fecha""");  //cabecera

                foreach (Folder fol in folders)
                {

                    sw.WriteLine(@"""" + fol.Nombre +
                                 @""",""" + fol.Path +
                                 @""",""" + fol.Tamaño +
                                 @""",""" + fol.FechaArchivo
                                 + @"""");
                }

                sw.Close();
            }
        }

        /// <summary>
        /// Borra las carpetas con antiguedad mayor a 'daysOld' días.
        /// </summary>
        /// <param name="folder">Carpeta a analizar</param>
        /// <param name="daysOld">Días de antiguedad</param>
        static public void DeleteOldFolder(string folder, int daysOld)
        {
            System.DateTime timeOld;
            timeOld = System.DateTime.Now.Subtract(System.TimeSpan.FromDays(daysOld));
            DirectoryInfo parentDir = Directory.GetParent(folder);
            string[] dirs = Directory.GetDirectories(parentDir.FullName);
            for (int i = 0; i < dirs.Length; i++)
            {
                System.DateTime writeTime = Directory.GetLastWriteTime(dirs[i]);
                if (writeTime < timeOld)
                    Directory.Delete(dirs[i], true);
            }
        }
    }
}
