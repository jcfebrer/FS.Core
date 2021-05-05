// <fileheader>
// <copyright file="editor.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: filemanager\editor.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using System.IO;
using FSPortal;

namespace FSPaginas.FileManager
{
    public class Editor : BasePage
    {
        public string Edita(string strFileName)
        {
            try
            {
                return File.ReadAllText(strFileName);
            }
            catch (System.Exception ex)
            {
                return ex.ToString();
            }
        }


        public bool Guarda(string strFileName, string data)
        {
            try
            {
                File.WriteAllText(strFileName, data);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}