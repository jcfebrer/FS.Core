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

using FSTrace;
using System;

#endregion

namespace FSException
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
            WriteError(message, this);
        }


        public ExceptionUtil(Exception e)
            : base(e.Message, e)
        {
            WriteError(e.ToString(), e);
        }

        public ExceptionUtil(string message, ExceptionType type) : base(message)
        {
            eType = type;

            if (eType == ExceptionType.Error) 
                WriteError(message, this);
        }


        public ExceptionUtil(string message, Exception e)
            : base(message, e)
        {
            WriteError(message + ": " + e, e);
        }


        public void Type(ExceptionType type)
        {
            eType = type;
        }

        public static void WriteError(string message, Exception e)
        {
            try
            {
                string processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                Log.TraceError(processName + "\n" + message);
            }
            catch
            {
            }
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