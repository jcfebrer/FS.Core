// // <fileheader>
// // <copyright file="FSException.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using FSTraceCore;
using System;

#endregion

namespace FSExceptionCore
{
    public class ExceptionUtil : Exception
    {
        public enum ExceptionType
        {
            Error,
            Information
        }

        private ExceptionType eType = ExceptionType.Error;

        public ExceptionUtil()
        {
        }

        public ExceptionUtil(string message)
            : base(message)
        {
            WriteError(message);
        }


        public ExceptionUtil(Exception e)
            : base(e.Message, e)
        {
            WriteError(e);
        }

        public ExceptionUtil(string message, ExceptionType type) : base(message)
        {
            eType = type;

            if (eType == ExceptionType.Error) 
                WriteError(message);
            if (eType == ExceptionType.Information)
                WriteInfo(message);
        }


        public ExceptionUtil(string message, Exception e)
            : base(message, e)
        {
            WriteError(message, e);
        }


        public void Type(ExceptionType type)
        {
            eType = type;
        }

        public static void WriteError(string message, Exception e)
        {
            WriteError(message + " : " + e.ToString());
        }

        public static void WriteError(Exception e)
        {
            WriteError(e.ToString());
        }

        public static void WriteError(string message)
        {
            string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            Log.TraceError(processName + " : " + message);
        }

        public static void WriteInfo(string message)
        {
            string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            Log.TraceInfo(processName + " : " + message);
        }

        /// <summary>
        /// Determines whether the specified ex is critical.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>
        ///   <c>true</c> if the specified ex is critical; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCritical(Exception ex)
        {
            if (ex is OutOfMemoryException)
                return true;
            if (ex is AppDomainUnloadedException)
                return true;
            if (ex is BadImageFormatException)
                return true;
            if (ex is CannotUnloadAppDomainException)
                return true;
            //if (ex is ExecutionEngineException) return true;
            if (ex is InvalidProgramException)
                return true;
            if (ex is System.Threading.ThreadAbortException)
                return true;
            return false;
        }
    }
}