using FSCryptoCore;
using FSLibraryCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSDiskCore
{
    public class FileUtils
    {
        public static event EventHandler FileAdded;

        public static void Rename(string file, string newName)
        {
            string origName = newName;
            int f = 1;
            while (System.IO.File.Exists(newName))
            {
                newName = String.Format(origName + " [{0}]", f);
                f++;
            }

            System.IO.File.Move(file, newName);
        }

        public static string FileExtension(string fileName)
        {
            string[] s = fileName.Split('.');
            return s[s.Length - 1];
        }

        public static String CalcCrc32(string fileName)
        {
            Crc32CAlgorithm crc32 = new Crc32CAlgorithm();
            String hash = String.Empty;

            try
            {
                using (FileStream fs = System.IO.File.OpenRead(fileName))
                {
                    byte[] hashes = crc32.ComputeHash(fs);
                    foreach (byte b in hashes)
                        hash += b.ToString("x2").ToLower();
                }
            }
            catch
            {
            }

            return hash;
        }

        public static string CalcCrc32Async(string fileName, CancellationToken token)
        {
            string crc32 = "";
            Task.Factory.StartNew(() =>
            {
                crc32 = CalcCrc32(fileName);
            }, token);
            return crc32;
        }


        /// <summary>
        /// Obtenemos la lista de ficheros de la carpeta indicada.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="extType"></param>
        public static Files GetFiles(string folder, string extType, bool recursive, bool calcCRC32, bool calcSoundEx)
        {
            Files ficheros = new Files();
            List<FileInfo> flatList = new List<FileInfo>();

            flatList = RecurseFiles(new DirectoryInfo(folder), extType, recursive);

            foreach (FileInfo f in flatList)
            {
                File fil = new File(f.Name);

                fil.Dir = f.DirectoryName;
                fil.Nombre = f.Name;
                fil.NombreNormalizado = NormalizeFileName(f.Name);
                fil.Tamaño = f.Length;
                fil.Extension = f.Extension;
                fil.FechaArchivo = f.CreationTime;
                fil.Label = DriveLabel(f.Directory.Root.FullName);
                fil.FullName = f.FullName;
                fil.FullName83 = FSDiskCore.FileUtils.GetShortFileName(f.FullName);

                if (calcSoundEx)
                    fil.SoundEx = FSFuzzyStrings.SoundExEsp.Do(f.Name);

                if(calcCRC32)
                    fil.Crc32 = CalcCrc32(f.FullName);

                if (fil.Nombre.ToLower() == fil.NombreNormalizado.ToLower())
                    fil.ColorFondo = Color.LightGreen;

                if (fil.NombreNormalizado.IndexOf("by ") > 0 ||
                    fil.NombreNormalizado.IndexOf('-') > 0 ||
                    fil.NombreNormalizado.IndexOf('[') > 0 ||
                    fil.NombreNormalizado.IndexOf(']') > 0 ||
                    fil.NombreNormalizado.IndexOf('(') > 0 ||
                    fil.NombreNormalizado.IndexOf(')') > 0)
                    fil.ColorFondo = Color.Red;

                //if (!cmbExtension.Items.Contains(pel.Extension))
                //    cmbExtension.Items.Add(pel.Extension);

                ficheros.Add(fil);

                if (FileAdded != null)
                    FileAdded(fil, EventArgs.Empty);
            }

            return ficheros;
        }

        public static DriveInfo[] GetDrives()
        {
            return System.IO.DriveInfo.GetDrives();
        }


        /// <summary>
        /// Devuelve recursivamente los ficheros con extensión 'extType' existentes en un directorio.
        /// Las extesiones se indican separadas por ";"
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="extType"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static List<FileInfo> RecurseFiles(DirectoryInfo directory, string extType, Boolean recursive)
        {
            List<FileInfo> resultList = new List<FileInfo>();

            string[] ext = extType.Split(';');
            foreach (string s in ext)
            {
                try
                {
                    resultList.AddRange(directory.GetFiles(s));
                }
                catch
                {
                }
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    resultList.AddRange(RecurseFiles(subDirectory, extType, recursive));
                }
            }

            return resultList;
        }



        /// <summary>
        /// Devuelve recursivamente el total de los ficheros con extensión 'extType' existentes en un directorio.
        /// Las extesiones se indican separadas por ";"
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="extType"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static int TotalFiles(string folder, string extType, Boolean recursive)
        {
            DirectoryInfo directory = new DirectoryInfo(folder);
            int total = 0;
            string[] ext = extType.Split(';');

            foreach (string s in ext)
            {
                try
                {
                    total += directory.GetFiles(s).Length;
                }
                catch
                {
                }
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    total += TotalFiles(subDirectory.FullName, extType, recursive);
                }
            }

            return total;
        }


        public static string DriveLabel(string dir)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drv in drives)
            {
                if (drv.Name == dir)
                    return drv.VolumeLabel;
            }
            return "NoLabel";
        }

        public static void SaveAsCsv(string fileName, Files ficheros)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(@"""Path"",""Nombre original"",""Nombre normalizado"",""Nombre 8.3"",""Label"",""Tamaño"",""Fecha""");  //cabecera

                foreach (File pel in ficheros)
                {

                    sw.WriteLine(@"""" + pel.Dir +
                                @""",""" + pel.Nombre +
                                 @""",""" + pel.NombreNormalizado +
                                 @""",""" + pel.FullName83 +
                                 @""",""" + pel.Label +
                                 @""",""" + pel.Tamaño +
                                 @""",""" + pel.FechaArchivo
                                 + @"""");
                }

                sw.Close();
            }
        }

        public static void SaveAsCsv(string fileName, DataTable dataTable)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                int f = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    sw.WriteLine(@"" + col.ColumnName);

                    if (f != dataTable.Columns.Count)
                        sw.Write(@""",");

                    f++;
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    int g = 0;
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        sw.WriteLine(@"""" + row[g]
                                     + @"""");

                        g++;
                    }
                }

                sw.Close();
            }
        }


        public static string ApplicationPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //string path = new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            //int pos = path.LastIndexOf('\\');
            //return path.Substring(0, pos);
        }

        public static string NormalizeFileName(string fileName)
        {
            //ponemos en minusculas
            fileName = fileName.ToLower();
            //obtenemos la extensión del fichero
            string ext = System.IO.Path.GetExtension(fileName);
            //cojemos el nombre del fichero
            string name = System.IO.Path.GetFileName(fileName);

            //quitamos la extensión del nombre
            name = TextUtil.Replace(name, ext, "");

            //quitamos "." y "_" del nombre del fichero
            name = name.Replace(".", " ");
            name = name.Replace("_", " ");
            name = name.Replace("-", " ");
            name = name.Replace("  ", " ");
            //borramos desde "by ", hasta la extensión
            name = TextUtil.Remove("by ", ext, name + ext);
            name = TextUtil.Replace(name, ext, "");


            //si es mp3, quitamos carácteres ilegales
            if (ext == ".mp3")
                name = TextUtil.RemoveInitialIlegalChars(name);

            //quitamos carácteres ilegales
            name = TextUtil.RemoveIllegalChar(name);

            //quitamos cadenas no validas
            name = name.Replace("2.0", "");
            name = name.Replace("5.1", "");
            name = name.Replace("spanish", "");
            name = name.Replace("english", "");
            name = name.Replace("español", "");
            name = name.Replace("ingles", "");
            name = name.Replace("castellano", "");
            name = name.Replace("dvd-rip", "");
            name = name.Replace("dvdrip", "");
            name = name.Replace("dvd rip", "");
            name = name.Replace("hdrip", "");
            name = name.Replace("tvrip", "");
            name = name.Replace("kvcd", "");
            name = name.Replace("dvdsat", "");
            name = name.Replace("hdtv", "");
            name = name.Replace("xbytes", "");
            name = name.Replace("desde emulemola", "");
            name = name.Replace("cineparasordos com", "");
            name = name.Replace("centraldivx com", "");
            name = name.Replace("centraldivx", "");
            name = name.Replace("elitetorrent net", "");
            name = name.Replace("elitefreak net", "");
            name = name.Replace("tusseries com", "");
            name = name.Replace("newtdt com", "");
            name = name.Replace("xvid", "");
            name = name.Replace("divx", "");
            name = name.Replace("dual", "");
            name = name.Replace("satrip", "");
            name = name.Replace("mp3", "");
            name = name.Replace("ac3", "");
            name = name.Replace("cvcd", "");
            name = name.Replace("dvd", "");
            name = name.Replace("esp ", "");
            name = name.Replace("ing ", "");

            //name = name.Replace("dvdscreener", "");

            //name = TextUtil.Remove(@"\[", @"\]", name);
            name = TextUtil.Remove("www", "com", name);
            name = TextUtil.Remove("www", "cl", name);
            name = TextUtil.Remove("www", "es", name);
            name = TextUtil.Remove("www", "org", name);

            name = TextUtil.RemoveRepeatSpaces(name);

            name = name.Replace(" (", "(");
            name = name.Replace("(", " (");

            name = name.Replace("()", "");
            name = name.Replace("( )", "");
            name = name.Replace("[]", "");
            name = name.Replace("[ ]", "");


            name = TextUtil.ToUTF8(name);

            //name = ToCase(name);

            name = name.Trim();

            //name = ToTitleCase(name);
            name = TextUtil.ToCase(name);

            name = name + ext.Trim().ToLower();

            name = name.Replace("-.", ".");

            name = TextUtil.RemoveRepeatSpaces(name);

            return name;
        }


        public static bool FileExtension(string strFileName, string[] saryFileUploadTypes)
        {
            int intExtensionLoopCounter = 0;
            bool fe = false;
            fe = false;

            for (intExtensionLoopCounter = 0;
                intExtensionLoopCounter <= saryFileUploadTypes.GetUpperBound(0);
                intExtensionLoopCounter++)
            {
                if (TextUtil.Right(strFileName, TextUtil.Length(saryFileUploadTypes[intExtensionLoopCounter])).ToLower() ==
                    saryFileUploadTypes[intExtensionLoopCounter].ToLower())
                {
                    fe = true;
                }
            }

            return fe;
        }

        static public void CopyDirectory(string SourceDirectory, string TargetDirectory, bool recursive, bool overwrite, bool copyHidden)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);

            //Determine whether the source directory exists.
            if (!source.Exists)
                return;

            if (!target.Exists)
            {
                target.Create();
            }
            else
            {
                if (!overwrite)
                {
                    int number = 0;

                    while (target.Exists)
                    {
                        number++;
                        string newTarget = TargetDirectory + " (" + number + ")";
                        target = new DirectoryInfo(newTarget);
                    }
                }

            }

            //Copy files.
            FileInfo[] sourceFiles = source.GetFiles();
            for (int i = 0; i < sourceFiles.Length; ++i)
            {
                System.IO.File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + sourceFiles[i].Name, overwrite);
            }

            if (recursive)
            {
                //Copy directories.
                DirectoryInfo[] sourceDirectories = source.GetDirectories();
                for (int j = 0; j < sourceDirectories.Length; ++j)
                {
                    if (copyHidden)
                    {
                        CopyDirectory(sourceDirectories[j].FullName, target.FullName + "\\" + sourceDirectories[j].Name, recursive, overwrite, copyHidden);
                    }
                    else
                    {
                        if (!(sourceDirectories[j].FullName.StartsWith(".") || sourceDirectories[j].Attributes.HasFlag(FileAttributes.Hidden)))
                        {
                            CopyDirectory(sourceDirectories[j].FullName, target.FullName + "\\" + sourceDirectories[j].Name, recursive, overwrite, copyHidden);
                        }
                    }
                }
            }
        }

        public static string FileToString(string cFileName)
        {
            var oReader = System.IO.File.OpenText(cFileName);

            var lcString = oReader.ReadToEnd();

            oReader.Close();
            return lcString;
        }

        public static string[] DirArray(string cFileSkeleton)
        {
            string[] aFiles = null;
            var lcDrive = cFileSkeleton.Substring(1, TextUtil.LastIndexOf(cFileSkeleton, @"\") + 1);
            var lcStem = cFileSkeleton.Substring(TextUtil.LastIndexOf(cFileSkeleton, @"\") + 2);
            aFiles = Directory.GetFiles(lcDrive, lcStem);
            return aFiles;
        }

        public static string FullPath(string cFileName)
        {
            var fi = new FileInfo(cFileName);
            return fi.FullName;
        }

        public static string GetExtension(string cFileName)
        {
            var fi = new FileInfo(cFileName);
            return fi.Extension;
        }

        public static string GetFileName(string cFileName)
        {
            var fi = new FileInfo(cFileName);
            return fi.Name;
        }

        public static DateTime GetModification(string cFileName)
        {
            return GetLastWriteTime(cFileName);
        }

        public static DateTime GetLastWriteTime(string cFileName)
        {
            return System.IO.File.GetLastWriteTime(cFileName);
        }

        public static string GetPath(string cPath)
        {
            var lcPath = cPath.Trim();

            if (lcPath.IndexOf('\\') == -1)
                return string.Empty;
            return lcPath.Substring(0, lcPath.LastIndexOf('\\'));
        }

        public static string GetBaseFileName(string cPath)
        {
            var lcFileName = GetFileName(cPath.Trim());

            if (lcFileName.IndexOf(".") == -1)
                return lcFileName;
            return lcFileName.Substring(0, lcFileName.LastIndexOf('.'));
        }

        public static string SetPath(string cFileName, string cPath)
        {
            cPath = cPath.Trim();
            cFileName = GetFileName(cFileName.Trim());

            if (cPath.Length == 0)
                return cFileName;
            if (cPath[cPath.Length - 1] == '\\')
                return cPath + cFileName;
            return cPath + @"\" + cFileName;
        }

        public static void StringToFile(string cExpression, string cFileName)
        {
            if (System.IO.File.Exists(cFileName)) System.IO.File.Delete(cFileName);

            var oFs = new FileStream(cFileName, FileMode.CreateNew, FileAccess.ReadWrite);

            var oWriter = new StreamWriter(oFs);

            oWriter.Write(cExpression);
            oWriter.Flush();
            oWriter.Close();

            oFs.Close();
        }

        public static void StringToFile(string cExpression, string cFileName, bool lAdditive)
        {
            var oFs = new FileStream(cFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            var oWriter = new StreamWriter(oFs);

            oWriter.BaseStream.Seek(0, SeekOrigin.End);

            oWriter.Write(cExpression);
            oWriter.Flush();
            oWriter.Close();
            oFs.Close();
        }

        public static string SetExtension(string cFileName, string cExtension)
        {
            cFileName = cFileName.Trim();
            var nLastDot = 0;
            var nLastBackSlash = 0;
            var nLength = 0;
            nLength = cFileName.Length;

            nLastDot = cFileName.LastIndexOf('.') + 1;

            if (nLastDot < 1) return cFileName + "." + cExtension;

            nLastBackSlash = cFileName.LastIndexOf('\\') + 1;
            if (nLastDot > nLastBackSlash)
                return cFileName.Substring(0, nLastDot - 1) + "." + cExtension;
            return cFileName + "." + cExtension;
        }

        public static string AddBackSlash(string cPath)
        {
            if (cPath.Trim().EndsWith(@"\"))
                return cPath.Trim();
            return cPath.Trim() + @"\";
        }

        public static string DisplayPath(string cFileNameWithPath, int nMaxLength)
        {
            if (cFileNameWithPath.Length <= nMaxLength) return cFileNameWithPath;

            char[] cSepatator = { '\\' };

            string[] aStr = null;
            aStr = cFileNameWithPath.Split(cSepatator);

            var lcBegin = aStr[0] + @"\...";
            var lnBeginLength = aStr[0].Length + 3;
            var lcRetVal = "";
            var lnLength = lcRetVal.Length;
            var lAddHeader = false;

            var s = "";
            var n = 0;

            var i = 0;
            for (i = aStr.Length - 1; i <= 0 - 1; i += i - 1)
            {
                s = '\\' + aStr[i];
                n = s.Length;

                if (lnLength + n <= nMaxLength)
                {
                    lcRetVal = s + lcRetVal;
                    lnLength += n;
                }
                else
                {
                    break;
                }

                if ((lAddHeader == false) & (lnLength + lnBeginLength <= nMaxLength))
                {
                    lAddHeader = true;
                    lnLength += lnBeginLength;
                }
            }

            if (lAddHeader) lcRetVal = lcBegin + lcRetVal;

            if (lcRetVal.Length == 0) lcRetVal = aStr[aStr.Length - 1].Substring(0, nMaxLength);

            return lcRetVal;
        }

        public static string GetLongFileName(String shortFileName)
        {
            StringBuilder longPath = new StringBuilder(2048);
            int len = Win32API.GetLongPathName(shortFileName, longPath, longPath.Capacity);
            //if (len == 0) throw new System.ComponentModel.Win32Exception();

            return longPath.ToString();
        }

        public static string GetShortFileName(String longFileName)
        {
            StringBuilder shortPath = new StringBuilder(255);
            int len = Win32API.GetShortPathName(longFileName, shortPath, shortPath.Capacity);
            //if (len == 0) throw new System.ComponentModel.Win32Exception();

            return shortPath.ToString();
        }

        public static List<FileInfo> FindFilesMatching(string dirPath, string pattern)
        {
            var files = new List<FileInfo>();

            if (!Directory.Exists(dirPath))
            {
                throw new ArgumentException("Directory Path does not exist!");
            }

            var dirInfo = new DirectoryInfo(dirPath);
            files = dirInfo.GetFiles("*" + pattern).ToList();

            return files;
        }

        public static List<string> GetFileExtensionsInDirectory(string dirPath)
        {
            var files = new List<string>();

            if (!Directory.Exists(dirPath))
            {
                throw new ArgumentException("Directory Path does not exist!");
            }

            var dirInfo = new DirectoryInfo(dirPath);
            files = dirInfo.GetFiles().Select(f => f.Extension).Distinct().ToList();

            return files;
        }

        public static List<FileInfo> FindImageFiles(string dirPath)
        {
            string[] extensions = { ".bmp", ".jpg", ".png" };
            var imgFiles = new List<FileInfo>();

            var dirInfo = new DirectoryInfo(dirPath);
            imgFiles = dirInfo.GetFiles("*.*")
                    .Where(f => extensions.Contains(f.Extension.ToLower())).ToList();

            return imgFiles;
        }

        public static IEnumerable<FileInfo> GetFilesByExtensions(DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentNullException("extensions");
            }

            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            var fileInfoList = files.Where(f => extensions.Contains(f.Extension));

            return fileInfoList;
        }
    }
}
