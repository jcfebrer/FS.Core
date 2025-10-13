#if NET40_OR_GREATER || NETCOREAPP
using FSException;
using FSTrace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSDisk
{
    public class SyncFolder
    {
        public static event EventHandler OnFileProcessed;

        private string m_folder1;
        private string m_folder2;
        private ModeEnum m_mode;
        private bool m_deleteFiles;
        private bool m_compareSizes;
        private bool m_force;
        private bool m_readOnly;
        private TimeFilterMode m_lastModify;
        private bool m_deleteEmptyFolders;
        private string m_filesToProcess;
        private string m_foldersToExclude;

        public string Folder1 { get { return m_folder1; } set { m_folder1 = value; } }
        public string Folder2 { get { return m_folder2; } set { m_folder2 = value; } }
        public ModeEnum Mode { get { return m_mode; } set { m_mode = value; } }
        public bool DeleteFiles { get { return m_deleteFiles; } set { m_deleteFiles = value; } }
        public bool CompareSizes { get { return m_compareSizes; } set { m_compareSizes = value; } }
        public bool Force { get { return m_force; } set { m_force = value; } }
        public bool ReadOnly { get { return m_readOnly; } set { m_readOnly = value; } }
        public TimeFilterMode LastModify { get { return m_lastModify; } set { m_lastModify = value; } }
        public bool DeleteEmptyFolders { get { return m_deleteEmptyFolders; } set { m_deleteEmptyFolders = value; } }
        public string FilesToProcess { get { return m_filesToProcess; } set { m_filesToProcess = value; } }
        public string FoldersToExclude { get { return m_foldersToExclude; } set { m_foldersToExclude = value; } }
        
        public SyncFolder(string folder1, string folder2, ModeEnum mode, bool deleteFiles, bool compareSizes, bool force, bool readOnly, TimeFilterMode lastModify, bool deleteEmptyFolders, string filesToProcess = "*", string foldersToExclude = "")
        {
            m_folder1 = folder1;
            m_folder2 = folder2;
            m_mode = mode;
            m_deleteFiles = deleteFiles;
            m_compareSizes = compareSizes;
            m_force = force;
            m_readOnly = readOnly;
            m_lastModify = lastModify;
            m_deleteEmptyFolders = deleteEmptyFolders;
            m_filesToProcess = filesToProcess;
            m_foldersToExclude = foldersToExclude;

            Log.TraceInfo("SyncFolder inicializado.");
        }

        public long TotalFiles 
        {
            get
            {
                return (m_mode == ModeEnum.RIGHT_TO_LEFT) ?
                    FileUtils.TotalFiles(m_folder2) :
                    FileUtils.TotalFiles(m_folder1);
            }
        }

        public enum ModeEnum
        {
            LEFT_TO_RIGHT,
            RIGHT_TO_LEFT,
            TWO_WAY
        };

        // Renombrado para mayor claridad
        public enum TimeFilterMode
        {
            ALL,
            YESTERDAY,
            WEEK,
            MONTH,
            YEAR
        }

        // Estructura para almacenar información de archivos de forma eficiente
        private struct FileData
        {
            public string RelativePath { get; set; }
            public long Size { get; set; }
            public DateTime LastWriteTime { get; set; }
        }

        /// <summary>
        /// USAGE:
        ///
        ///     folderSync mode folder1 folder2 -[options]
        ///
        ///Mode:
        ///-----
        /// 1=left to right sync
        /// 2=right to left sync
        /// 3=two way sync
        ///
        ///- left to right sync mode means that only folder2 is updated based on folder1.
        ///- right to left sync mode means that only folder1 is updated based on folder2.
        ///- Two way sync mode means that both folders are updated.
        ///  This mode does not support delete and compare size options. Only files and subfolders that are missing in each folder are copied.
        ///
        ///Options:
        ///--------
        /// d=Delete files in folder2 that don't exist in folder1. (Works only in one way sync)
        /// s=Compare file sizes and overwrite files with different size (folder1 -> folder2). (Works only in one way sync)
        /// f=Force delete/overwrite (don't ask user for each file/subfolder)
        /// r=Readonly (simulation mode - no actual copy/delete/overwrite)
        ///
        ///
        /// </summary>
        /// <param name="folder1">Carpeta 1</param>
        /// <param name="folder2">Carpeta 2</param>
        /// <param name="mode">Modo de sincronización</param>
        /// <param name="deleteFiles">Borra los ficheros de la carpeta 2.</param>
        /// <param name="compareSizes">Compara tamaños para determinar si son diferentes.</param>
        /// <param name="force">No pregunta si borrar o sobreescribir.</param>
        /// <param name="readOnly">Solo lectura. No procesar nada. Para depurar.</param>
        /// <param name="lastModify">Copiar solo ficheros modificados en el último periodo de tiempo.</param>
        /// <param name="filesToProcess">Ficheros a procesar. Permite especificar los ficheros a procesar con expresiones regulares de esta forma: "\.doc|\.xls"</param>
        /// <param name="deleteEmptyFolders">Borra las carpetas vacias de la carpeta 2.</param>
        /// <param name="foldersToExclude">Carpetas a excluir de la sincronización. Separadas por '|'.</param>
        /// <returns></returns>
        public string DoSync()
        {
            // Normalizar las rutas para asegurar que terminan con el separador de directorio
            m_folder1 = FSDisk.FileUtils.TrimEndingDirectorySeparator(m_folder1) + Path.DirectorySeparatorChar;
            m_folder2 = FSDisk.FileUtils.TrimEndingDirectorySeparator(m_folder2) + Path.DirectorySeparatorChar;

            // Si el modo es de derecha a izquierda, intercambiamos los paths y dejamos el mode en LEFT_TO_RIGHT.
            if (m_mode == ModeEnum.RIGHT_TO_LEFT)
            {
                string folderTmp = m_folder1;
                m_folder1 = m_folder2;
                m_folder2 = folderTmp;
                m_mode = ModeEnum.LEFT_TO_RIGHT;
                Log.TraceInfo("Carpetas intercambiadas. Procesando como 'Left to Right'.");
            }

            try
            {
                if (!Directory.Exists(m_folder1))
                    // Uso de excepción estándar de .NET para errores de E/S
                    throw new DirectoryNotFoundException("Carpeta principal (Origen): " + m_folder1 + " no existe!");
                if (!Directory.Exists(m_folder2))
                    Directory.CreateDirectory(m_folder2);
            }
            catch (Exception ex)
            {
                Log.TraceError(ex);
                return ex.Message;
            }


            /////////////
            // DO WORK //
            /////////////
            Log.TraceInfo("INICIANDO " + (((m_mode == ModeEnum.LEFT_TO_RIGHT) ? "SINCRONIZACIÓN UNIDIRECCIONAL" : "SINCRONIZACIÓN BIDIRECCIONAL")));
            Log.TraceInfo("ENTRE");
            Log.TraceInfo("CARPETA 1: (REF) " + m_folder1);
            Log.TraceInfo("CARPETA 2:       " + m_folder2);

            Log.TraceInfo("CON OPCIONES");

            // Registro de opciones mejorado (if separados)
            if (m_deleteFiles)
                Log.TraceInfo("  BORRAR ARCHIVOS EN CARPETA 2 QUE NO EXISTEN EN CARPETA 1 (DELETE FILES)");
            if (m_compareSizes)
                Log.TraceInfo("  SOBREESCRIBIR ARCHIVOS EN CARPETA 2 CON TAMAÑO DIFERENTE (COMPARE SIZES)");
            if (m_force)
                Log.TraceInfo("  MODO FORZADO (OMITIR CONFIRMACIÓN)");
            if (m_readOnly)
                Log.TraceInfo("  SOLO LECTURA (PARA PRUEBAS)");
            if (m_lastModify != TimeFilterMode.ALL)
                Log.TraceInfo($"  FILTRAR POR ÚLTIMA MODIFICACIÓN: {m_lastModify}");

            // collect folder content
            Log.TraceInfo("RECOLECTANDO CONTENIDO...");
            List<string> _content1_folders = new List<string>();
            List<string> _content2_folders = new List<string>();
            List<FileData> _content1_files = new List<FileData>();
            List<FileData> _content2_files = new List<FileData>();

            try
            {
                // Calcular fecha de inicio para el filtro
                DateTime from_date = GetFromDate(m_lastModify);

                Log.TraceInfo("... " + m_folder1);
                _content1_folders = Directory.EnumerateDirectories(m_folder1, "*", SearchOption.AllDirectories)
                    .Select(x => x.Replace(m_folder1, ""))
                    .Where(x => !IsExcludeFolder(m_foldersToExclude, x))
                    .ToList();

                _content1_files = GetFilteredFiles(m_folder1, m_filesToProcess, m_foldersToExclude, from_date);


                Log.TraceInfo("... " + m_folder2);
                _content2_folders = Directory.EnumerateDirectories(m_folder2, "*", SearchOption.AllDirectories)
                    .Select(x => x.Replace(m_folder2, ""))
                    .Where(x => !IsExcludeFolder(m_foldersToExclude, x))
                    .ToList();

                _content2_files = GetFilteredFiles(m_folder2, m_filesToProcess, m_foldersToExclude, from_date);

            }
            catch (Exception ex)
            {
                Log.TraceInfo("Error al leer el contenido de la carpeta!");
                Log.TraceError(ex);
                return ex.Message;
            }

            // Convertir listas de FileData a listas de rutas relativas para las operaciones de conjunto (Except, Intersect)
            List<string> _content1_files_paths = _content1_files.Select(f => f.RelativePath).ToList();
            List<string> _content2_files_paths = _content2_files.Select(f => f.RelativePath).ToList();


            Log.TraceInfo("CALCULANDO DIFERENCIAS...");

            // collect missing folders and files
            Log.TraceInfo("... CARPETAS PARA COPIAR");
            List<string> _missing1_folders = new List<string>(), _missing2_folders = new List<string>();
            if (m_mode == ModeEnum.TWO_WAY)
            {
                _missing1_folders = _content2_folders.Except(_content1_folders).ToList();
            }
            _missing2_folders = _content1_folders.Except(_content2_folders).ToList();
            if (_missing1_folders.Count > 0)
                Log.TraceInfo($"      {_missing1_folders.Count} de CARPETA 2 a CARPETA 1");
            if (_missing2_folders.Count > 0)
                Log.TraceInfo($"      {_missing2_folders.Count} de CARPETA 1 a CARPETA 2");
            if (_missing1_folders.Count + _missing2_folders.Count == 0)
                Log.TraceInfo("      ¡Ninguna!");

            Log.TraceInfo("... ARCHIVOS PARA COPIAR");
            List<string> _missing1_files = new List<string>(), _missing2_files = new List<string>();
            if (m_mode == ModeEnum.TWO_WAY)
            {
                _missing1_files = _content2_files_paths.Except(_content1_files_paths).ToList();
            }
            _missing2_files = _content1_files_paths.Except(_content2_files_paths).ToList();
            if (_missing1_files.Count > 0)
                Log.TraceInfo($"      {_missing1_files.Count} de CARPETA 2 a CARPETA 1");
            if (_missing2_files.Count > 0)
                Log.TraceInfo($"      {_missing2_files.Count} de CARPETA 1 a CARPETA 2");
            if (_missing1_files.Count + _missing2_files.Count == 0)
                Log.TraceInfo("      ¡Ninguno!");

            // collect list to delete
            List<string> _delete_folders = new List<string>(), _delete_files = new List<string>();
            if (m_deleteFiles && m_mode == ModeEnum.LEFT_TO_RIGHT) // delete one way only
            {
                Log.TraceInfo("... CARPETAS PARA BORRAR");
                _delete_folders = _content2_folders.Except(_content1_folders).ToList();
                if (_delete_folders.Count > 0)
                    Log.TraceInfo($"      {_delete_folders.Count} de CARPETA 2");
                else
                    Log.TraceInfo("      ¡Ninguna!");
                Log.TraceInfo("... ARCHIVOS PARA BORRAR");
                _delete_files = _content2_files_paths.Except(_content1_files_paths).ToList();
                if (_delete_files.Count > 0)
                    Log.TraceInfo($"      {_delete_files.Count} de CARPETA 2");
                else
                    Log.TraceInfo("      ¡Ninguno!");
            }

            // collect list for overwrite
            List<string> _overwrite = new List<string>();
            if (m_compareSizes && m_mode == ModeEnum.LEFT_TO_RIGHT) // overwrite one way only
            {
                Log.TraceInfo("... ARCHIVOS PARA SOBREESCRIBIR");

                // Usar Join y Where para comparar tamaños eficientemente
                _overwrite = _content1_files.Join(_content2_files,
                                                 f1 => f1.RelativePath,
                                                 f2 => f2.RelativePath,
                                                 (f1, f2) => new { File1 = f1, File2 = f2 })
                                            .Where(pair => pair.File1.Size != pair.File2.Size)
                                            .Select(pair => pair.File1.RelativePath)
                                            .ToList();

                if (_overwrite.Count > 0)
                    Log.TraceInfo($"      {_overwrite.Count} en CARPETA 2");
                else
                    Log.TraceInfo("      ¡Ninguno!");
            }

            // check if there is anything to do
            if (_missing1_folders.Count + _missing2_folders.Count + _missing1_files.Count + _missing2_files.Count + _delete_folders.Count + _delete_files.Count + _overwrite.Count == 0)
            {
                Log.TraceInfo("NADA QUE HACER...");
                return "OK"; // nothing to do
            }

            //////////////////////
            // CHECK DISK SPACE //
            //////////////////////

            if (_missing1_files.Count + _missing2_files.Count + _overwrite.Count > 0)
            {
                try
                {
                    Log.TraceInfo("VERIFICANDO ESPACIO EN DISCO...");
                    DriveInfo _di;
                    long _size;

                    // check disk space 1 (for files from 2 to 1)
                    if (m_mode == ModeEnum.TWO_WAY && _missing1_files.Count > 0)
                    {
                        _di = new DriveInfo(Path.GetPathRoot(m_folder1));
                        _size = _missing1_files.Sum(x => _content2_files.FirstOrDefault(f => f.RelativePath == x).Size);
                        if (_di.AvailableFreeSpace <= _size)
                            throw new ExceptionUtil($"ERROR: NO HAY ESPACIO SUFICIENTE EN DISCO {Path.GetPathRoot(m_folder1)} para archivos de CARPETA 2.");
                    }

                    // check disk space 2 (for files from 1 to 2, and overwrites)
                    if (_missing2_files.Count > 0 || _overwrite.Count > 0)
                    {
                        _di = new DriveInfo(Path.GetPathRoot(m_folder2));

                        // Tamaño de archivos que faltan en folder2 (serán copiados)
                        _size = _missing2_files.Sum(x => _content1_files.FirstOrDefault(f => f.RelativePath == x).Size);

                        // Diferencia de tamaño para archivos a sobrescribir (solo si el nuevo es más grande)
                        _size += _overwrite.Sum(x => {
                            long size1 = _content1_files.FirstOrDefault(f => f.RelativePath == x).Size;
                            long size2 = _content2_files.FirstOrDefault(f => f.RelativePath == x).Size;
                            return Math.Max(0, size1 - size2);
                        });

                        if (_di.AvailableFreeSpace <= _size)
                            throw new ExceptionUtil($"ERROR: NO HAY ESPACIO SUFICIENTE EN DISCO {Path.GetPathRoot(m_folder2)} para archivos de CARPETA 1.");
                    }
                }
                catch (Exception ex)
                {
                    Log.TraceError(ex);
                    return ex.Message;
                }
                Log.TraceInfo("ESPACIO EN DISCO OK!");
            }

            //////////////////////////////
            // COPY FOLDERS AND FILES //
            ////////////////////////////

            // Preparar y registrar carpetas a crear
            if (_missing1_folders.Count + _missing2_folders.Count > 0)
            {
                _missing1_folders.Sort(); // Asegurar que los padres se creen antes
                _missing2_folders.Sort(); // Asegurar que los padres se creen antes

                if (!m_force || m_readOnly)
                {
                    Log.TraceInfo("Carpetas a ser creadas");
                    Log.TraceInfo("=>");
                    foreach (string s in _missing1_folders) Log.TraceInfo(m_folder1 + s);
                    foreach (string s in _missing2_folders) Log.TraceInfo(m_folder2 + s);
                }

                Log.TraceInfo("CREANDO CARPETAS...");
                Log.TraceInfo("===================");

                // Creación de carpetas a folder1 (desde folder2)
                foreach (string relativePath in _missing1_folders)
                {
                    string targetPath = Path.Combine(m_folder1, relativePath);
                    Log.TraceInfo($" Creando carpeta en CARPETA 1: {targetPath}", true);
                    if (!m_readOnly)
                        Directory.CreateDirectory(targetPath);
                }
                // Creación de carpetas a folder2 (desde folder1)
                foreach (string relativePath in _missing2_folders)
                {
                    string targetPath = Path.Combine(m_folder2, relativePath);
                    Log.TraceInfo($" Creando carpeta en CARPETA 2: {targetPath}", true);
                    if (!m_readOnly)
                        Directory.CreateDirectory(targetPath);
                }
            }

            // Copia de archivos (missing files)
            if (_missing1_files.Count + _missing2_files.Count > 0)
            {
                if (!m_force || m_readOnly)
                {
                    Log.TraceInfo("Archivos a ser copiados");
                    Log.TraceInfo("=>");
                    foreach (string s in _missing1_files) Log.TraceInfo(Path.Combine(m_folder2, s) + " -> " + Path.Combine(m_folder1, s));
                    foreach (string s in _missing2_files) Log.TraceInfo(Path.Combine(m_folder1, s) + " -> " + Path.Combine(m_folder2, s));
                }

                Log.TraceInfo("COPIANDO ARCHIVOS NUEVOS...");
                Log.TraceInfo("===========================");

                // Copia de FOLDER 2 a FOLDER 1 (Missing 1 files)
                foreach (string relativePath in _missing1_files)
                {
                    string sourcePath = Path.Combine(m_folder2, relativePath);
                    string destPath = Path.Combine(m_folder1, relativePath);

                    Log.TraceInfo($" Copiando (2 -> 1): {relativePath}");
                    if (!m_readOnly)
                    {
                        // Usar 'true' para permitir sobrescribir en Two-Way si existe, aunque no debería con 'Except'
                        System.IO.File.Copy(sourcePath, destPath, true);

                        if(OnFileProcessed != null)
                            OnFileProcessed(sourcePath, EventArgs.Empty);
                    }
                }

                // Copia de FOLDER 1 a FOLDER 2 (Missing 2 files)
                foreach (string relativePath in _missing2_files)
                {
                    string sourcePath = Path.Combine(m_folder1, relativePath);
                    string destPath = Path.Combine(m_folder2, relativePath);

                    Log.TraceInfo($" Copiando (1 -> 2): {relativePath}");
                    if (!m_readOnly)
                    {
                        System.IO.File.Copy(sourcePath, destPath, true);

                        if (OnFileProcessed != null)
                            OnFileProcessed(sourcePath, EventArgs.Empty);
                    }
                }
            }

            //////////////////
            // DELETE FILES //
            //////////////////
            bool doDelete;
            if (m_deleteFiles && m_mode == ModeEnum.LEFT_TO_RIGHT && _delete_files.Count > 0) // delete only one way
            {
                // print out list of files to be deleted
                if (!m_force || m_readOnly)
                {
                    Log.TraceInfo("Archivos a ser borrados");
                    Log.TraceInfo("=>");
                    foreach (string s in _delete_files)
                        Log.TraceInfo(m_folder2 + s);
                }

                // delete files
                Log.TraceInfo("BORRANDO ARCHIVOS...");
                Log.TraceInfo("====================");
                if (!m_force)
                    Log.TraceInfo("(Confirmación de usuario activa)");

                foreach (string relativePath in _delete_files)
                {
                    string fullPath = Path.Combine(m_folder2, relativePath);
                    doDelete = false;
                    Log.TraceInfo($"  {fullPath}");

                    //if (!force && !readOnly) // ask user
                    //{
                    //    DialogResult res = InputBox.ShowDialog($"¿Borrar: {fullPath}?", "Borrar Archivo", "", InputBox.Icon.Question, InputBox.Buttons.YesNo);
                    //    if (res == DialogResult.Yes)
                    //        doDelete = true;
                    //}
                    //else
                    if (m_force)
                    {
                        doDelete = true;
                    }

                    try
                    {
                        if (doDelete)
                        {
                            if (!m_readOnly)
                            {
                                System.IO.File.Delete(fullPath);
                                Log.TraceInfo("¡Borrado!");
                            }
                            else
                                Log.TraceInfo("¡Solo lectura!");
                        }
                        else
                            Log.TraceInfo("¡Omitido!");
                    }
                    catch (Exception ex)
                    {
                        Log.TraceInfo("  ¡FALLÓ!");
                        Log.TraceError(ex);
                        return ex.Message;
                    }
                }
            }

            // delete folders
            if (m_deleteFiles && m_mode == ModeEnum.LEFT_TO_RIGHT && _delete_folders.Count > 0) // delete only one-way
            {
                _delete_folders = _delete_folders.OrderByDescending(x => x).ToList(); // para asegurar que los hijos se borren antes que los padres
                // print out all the folders to be deleted
                if (!m_force || m_readOnly)
                {
                    Log.TraceInfo("Carpetas a ser borradas");
                    Log.TraceInfo("=>");
                    foreach (string s in _delete_folders)
                        Log.TraceInfo(m_folder2 + s);
                }
                Log.TraceInfo("BORRANDO CARPETAS...");
                Log.TraceInfo("====================");
                if (!m_force)
                    Log.TraceInfo("(Confirmación de usuario activa)");

                foreach (string relativePath in _delete_folders)
                {
                    string fullPath = Path.Combine(m_folder2, relativePath);
                    doDelete = false;
                    Log.TraceInfo($"  {fullPath}");

                    //if (!force && !readOnly)
                    //{
                    //    DialogResult res = InputBox.ShowDialog($"¿Borrar: {fullPath}?", "Borrar Carpeta", "", InputBox.Icon.Question, InputBox.Buttons.YesNo);
                    //    if (res == DialogResult.Yes)
                    //        doDelete = true;
                    //}
                    //else
                    if (m_force)
                    {
                        doDelete = true;
                    }

                    try
                    {
                        if (doDelete)
                        {
                            if (!m_readOnly)
                            {
                                Directory.Delete(fullPath);
                                Log.TraceInfo("¡Borrado!");
                            }
                            else
                                Log.TraceInfo("¡Solo lectura!");
                        }
                        else
                            Log.TraceInfo("¡Omitido!");
                    }
                    catch (Exception ex)
                    {
                        Log.TraceInfo("  ¡FALLÓ!");
                        Log.TraceError(ex);
                        return ex.Message;
                    }
                }
            }

            // overwrite files
            bool doOverwrite;
            if (m_compareSizes && m_mode == ModeEnum.LEFT_TO_RIGHT && _overwrite.Count > 0) // overwrite only one-way
            {
                if (!m_force || m_readOnly)
                {
                    Log.TraceInfo("Archivos a ser sobrescritos");
                    Log.TraceInfo("=>");
                    foreach (string s in _overwrite)
                        Log.TraceInfo(m_folder2 + s);
                }
                Log.TraceInfo("SOBREESCRIBIENDO ARCHIVOS...");
                Log.TraceInfo("============================");
                if (!m_force)
                    Log.TraceInfo("(Confirmación de usuario activa)");

                foreach (string relativePath in _overwrite)
                {
                    string sourcePath = Path.Combine(m_folder1, relativePath);
                    string destPath = Path.Combine(m_folder2, relativePath);

                    doOverwrite = false;
                    Log.TraceInfo($"  {destPath}");

                    //if (!force && !readOnly)
                    //{
                    //    DialogResult res = InputBox.ShowDialog($"¿Sobreescribir: {destPath}?", "Sobreescribir Archivo", "", InputBox.Icon.Question, InputBox.Buttons.YesNo);
                    //    if (res == DialogResult.Yes)
                    //        doOverwrite = true;
                    //}
                    //else
                    if (m_force)
                    {
                        doOverwrite = true;
                    }

                    try
                    {
                        if (doOverwrite)
                        {
                            if (!m_readOnly)
                            {
                                System.IO.File.Copy(sourcePath, destPath, true);
                                Log.TraceInfo("¡Sobrescrito!");
                            }
                            else
                                Log.TraceInfo("¡Solo lectura!");
                        }
                        else
                            Log.TraceInfo("¡Omitido!");
                    }
                    catch (Exception ex)
                    {
                        Log.TraceError(ex);
                        return ex.Message;
                    }
                }
            }

            if (m_deleteEmptyFolders)
            {
                DeleteEmptyFoldersFunc(m_folder2);
            }

            return "OK";
        }

        // Método auxiliar para calcular la fecha de inicio del filtro
        private static DateTime GetFromDate(TimeFilterMode lastModify)
        {
            DateTime now = DateTime.Now.Date; // Usar solo la fecha para comparación

            switch (lastModify)
            {
                case TimeFilterMode.YESTERDAY:
                    return now.AddDays(-1);
                case TimeFilterMode.WEEK:
                    return now.AddDays(-7);
                case TimeFilterMode.MONTH:
                    return now.AddMonths(-1);
                case TimeFilterMode.YEAR:
                    return now.AddYears(-1);
                default:
                    return DateTime.Now.AddYears(-50); // Valor por defecto
            }
        }

        // Método auxiliar para la recolección eficiente de archivos con filtrado
        private static List<FileData> GetFilteredFiles(string folder, string filesToProcess, string foldersToExclude, DateTime fromDate)
        {
            bool hasFileFilter = !String.IsNullOrEmpty(filesToProcess) && filesToProcess != "*" && filesToProcess != "*.*";
            bool hasFolderFilter = !String.IsNullOrEmpty(foldersToExclude);
            bool hasDateFilter = fromDate > DateTime.Now.AddYears(-49); // Asume que -50 años es "ALL"

            var files = Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories)
                .Select(path => new FileInfo(path))
                .Select(info => new FileData
                {
                    RelativePath = info.FullName.Replace(folder, ""),
                    Size = info.Length,
                    LastWriteTime = info.LastWriteTime
                });

            if (hasFolderFilter)
                files = files.Where(f => !IsExcludeFolder(foldersToExclude, f.RelativePath));

            if (hasFileFilter)
                // Se asume el uso de extensiones separadas por | ya que la implementación de IsFileToProcess es simple (no Regex)
                files = files.Where(f => IsFileToProcess(filesToProcess, f.RelativePath));

            if (hasDateFilter)
                files = files.Where(f => f.LastWriteTime >= fromDate);

            return files.ToList();
        }


        private static void DeleteEmptyFoldersFunc(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyFoldersFunc(directory);
                // Utilizar Enumerate... para evitar cargar la lista completa en memoria
                if (!Directory.EnumerateFiles(directory).Any() &&
                    !Directory.EnumerateDirectories(directory).Any())
                {
                    try
                    {
                        Directory.Delete(directory, false);
                        Log.TraceInfo($"Carpeta vacía borrada: {directory}");
                    }
                    catch (IOException ex)
                    {
                        // Manejar caso donde el directorio está en uso o tiene permisos.
                        Log.TraceError($"No se pudo borrar la carpeta vacía {directory}: {ex.Message}");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Log.TraceError($"No autorizado para borrar la carpeta vacía {directory}: {ex.Message}");
                    }
                }
            }
        }

        private static bool IsExcludeFolder(string folders, string folderName)
        {
            if (String.IsNullOrEmpty(folders)) return false;

            // Uso de StringSplitOptions.RemoveEmptyEntries para mayor robustez
            string[] folderToExclude = folders.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            // Normalizar la ruta del archivo/carpeta para tener siempre el separador al inicio para comparación
            string normalizedFolderName = folderName.Replace(Path.DirectorySeparatorChar, '/');

            foreach (string s in folderToExclude)
            {
                string normalizedExclude = s.Trim().Replace(Path.DirectorySeparatorChar, '/').ToLower();

                // Si la ruta relativa contiene el nombre de la carpeta a excluir, asumimos que debe excluirse.
                // Se busca "/nombreCarpeta/" para evitar coincidencias parciales con nombres de archivo.
                if (normalizedFolderName.ToLower().Contains($"/{normalizedExclude}/") || normalizedFolderName.ToLower().EndsWith($"/{normalizedExclude}"))
                    return true;
            }

            return false;
        }

        private static bool IsFileToProcess(string files, string fileName)
        {
            if (String.IsNullOrEmpty(files)) return true;

            // Uso de StringSplitOptions.RemoveEmptyEntries para mayor robustez
            string[] filesToProcess = files.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string fileExtension = Path.GetExtension(fileName).ToLower();

            foreach (string s in filesToProcess)
            {
                string requiredExtension = s.Trim().ToLower();

                // Asume que la entrada es "doc" o ".doc". Elimina el punto si existe.
                if (requiredExtension.StartsWith("."))
                    requiredExtension = requiredExtension.Substring(1);

                if (fileExtension == $".{requiredExtension}")
                    return true;
            }

            return false;
        }
    }
}
#endif