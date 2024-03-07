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

using FSExceptionCore;
using FSDiskCore;
using FSLibraryCore;
using System;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;

#endregion

namespace FSNetworkCore
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

            foreach(var file in HttpContext.Current.Request.Form.Files)
            {
                if ((file.Length > 0))
                {
                    fic = NumberUtils.RandomHexValue(3) + Path.GetFileName(file.FileName);
                    strNewFileName = strFileUploadPath + @"\" + fic;
                    blnExtensionOK = FileUtils.FileExtension(strNewFileName, saryFileUploadTypes);
                    lngErrorFileSize = Convert.ToInt64(MaxFileSize(file.Length, intMaxFileSize));

                    if (blnExtensionOK & lngErrorFileSize == 0)
                    {
                        using (var stream = System.IO.File.Create(strNewFileName))
                        {
                            file.CopyToAsync(stream);
                        }

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