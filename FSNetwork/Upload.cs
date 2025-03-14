// // <fileheader>
// // <copyright file="FuncionesUpload.cs" company="Febrer Software">
// //     Fecha: 03/07/2015
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using FSException;
using FSDisk;
using FSLibrary;
using System;
using System.IO;
using System.Web;

#if !NETFRAMEWORK
    using Microsoft.AspNetCore.Http;
#endif

#endregion

namespace FSNetwork
{
    /// <summary>
    ///     Funciones para subir ficheros al servidor web
    /// </summary>
    public class Upload
    {
        /// <summary>
        ///     Función para subir un fichero al servidor web
        /// </summary>
        /// <param name="strFileUploadPath">Fichero a subir</param>
        /// <param name="saryFileUploadTypes">Tipos de ficheros permitidos</param>
        /// <param name="intMaxFileSize">Tamaño máximo permitido</param>
        /// <param name="strUploadComponent">Componente a utilizar</param>
        /// <param name="lngErrorFileSize">Tamaño de fichero erroneo</param>
        /// <param name="blnExtensionOK">Extensión correcta si/no</param>
        /// <returns></returns>
        public void FileUpload(string strFileUploadPath, string[] saryFileUploadTypes, int intMaxFileSize,
            ref long lngErrorFileSize, ref bool blnExtensionOK)
        {
            string strNewFileName = null;
            string fic = null;
            //Field v = null;

#if NETFRAMEWORK
            HttpFileCollection uploadedFiles = HttpContext.Current.Request.Files;
#else
            var uploadedFiles = HttpContext.Current.Request.Form.Files;
#endif


#if NETFRAMEWORK
            foreach (HttpPostedFile file in uploadedFiles)
#else
            foreach (var file in uploadedFiles)
#endif
            {
#if NETFRAMEWORK
                long len = file.ContentLength;
#else
                long len = file.Length;
#endif

                if (len > 0)
                {
                    fic = NumberUtils.RandomHexValue(3) + Path.GetFileName(file.FileName);
                    strNewFileName = strFileUploadPath + @"\" + fic;
                    blnExtensionOK = FileUtils.FileExtension(strNewFileName, saryFileUploadTypes);
                    lngErrorFileSize = Convert.ToInt64(MaxFileSize(len, intMaxFileSize));

                    if (blnExtensionOK & lngErrorFileSize == 0)
                    {
#if NETFRAMEWORK
                        file.SaveAs(strNewFileName);
#else
                        using (var stream = System.IO.File.Create(strNewFileName))
                        {
                            file.CopyToAsync(stream);
                        }
#endif

                        //if (frmCampos != null)
                        //{
                        //    v = frmCampos.Find(HttpContext.Current.Request.Files.Keys[i]);
                        //    if (v != null)
                        //    {
                        //        v.Valor = fic;
                        //    }
                        //}
                    }
                    else
                    {
                        throw new ExceptionUtil("Extensión o tamaño de fichero incorrecto. Extensiones permitidas: " +
                                              Functions.ArrayToString(saryFileUploadTypes, ",") +
                                              ". Tamaño máximo de fichero: " + intMaxFileSize + "kb");
                    }
                }
            }
        }

        private double MaxFileSize(long lngFileSize, int intMaxFileSize)
        {
            if (intMaxFileSize == 0)
            {
                return 0;
            }
            if (lngFileSize / 1024 > intMaxFileSize)
            {
                return Convert.ToDouble(lngFileSize / 1024);
            }
            return 0;
        }
    }
}