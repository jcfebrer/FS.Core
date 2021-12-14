// // <fileheader>
// // <copyright file="Log.cs" company="Febrer Software">
// //     Fecha: 03/07/2010
// //     Project: FSLibrary
// //     Solution: FSLibraryNET2008
// //     Copyright (c) 2010 Febrer Software. Todos los derechos reservados.
// //     http://www.febrersoftware.com
// // </copyright>
// // </fileheader>

#region

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

#endregion

namespace FSTrace
{
    /// <summary>Implementa funcionalidad de traceo</summary>
    public static class Log
    {
        public delegate void MessageLogEventHandler(object source, Log.LogMessage e);
        public delegate void MessageLogTextEventHandler(object source, string e);
        public static event MessageLogTextEventHandler OnMessageLogText;
        public static event MessageLogEventHandler OnMessageLog;
        public static TraceLevel m_LogTraceLevel = TraceLevel.Verbose;

        private const string ExceptionMessageFormat = "The following exception happened: {0}";
        private static System.DateTime LastTime = System.DateTime.Now;
        private static TraceListener m_traceListener;
        private static bool m_firstSection = true;
        private static TraceSwitch m_logLevelSwitch;

        public static TraceLevel LogTraceLevel
        {
            get { return m_LogTraceLevel; }
            set
            {
                m_LogTraceLevel = value;
                LogLevelSwitch.Level = value;
            }
        }

        /// <summary>
        ///     Optiene el nivel de logeo
        /// </summary>
        private static TraceSwitch LogLevelSwitch
        {
            get
            {
                if (m_logLevelSwitch != null)
                    return m_logLevelSwitch;
                m_logLevelSwitch = new TraceSwitch("LogLevel", "Log trace switch", LogTraceLevel.ToString());

                return m_logLevelSwitch;
            }
        }

        private static void IntializeTraceListener()
        {
            //Si el trace no esta activado no necesitamos el trace listener
            if (LogLevelSwitch.Level == TraceLevel.Off)
            {
                //Cerramos el trace listener si esta abierto
                CloseTraceListener();
                return;
            }


            //Inicializamos el listener
            if (m_traceListener == null)
            {
                //Establecemos el archivo de logeo, si no esta especificado salimos
                var logFile = GetLogFile();

                if (string.IsNullOrEmpty(logFile))
                {
                    CloseTraceListener();
                    return;
                }

                m_traceListener = new TextWriterTraceListener(logFile, "TraceListener");
                EnableTraceListener();
            }
        }

        /// <summary>
        ///     Obtiene el archivo de logs, desde el archivo de configuración
        ///     de la aplicación. Si solo se ha especificado el nombre de archivo
        ///     se logea en el directorio en que se encuentra el proceso.
        /// </summary>
        private static string GetLogFile()
        {
            string logFile = ConfigurationManager.AppSettings["LogFile"];

            if (String.IsNullOrEmpty(logFile))
                return null;

            if (logFile.Contains(@".\"))
                logFile = AppDomain.CurrentDomain.BaseDirectory +
                          logFile.Replace(@".\", "");

            if (!logFile.Contains(@"\"))
                logFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                          @"\" + logFile;

            //Si solo se ha especificado nombre del archivo de log
            //en el archivo de configuración añadimos el path del proceso
            return logFile;
        }

        /// <summary>
        ///     Cierra el listener si esta abierto
        /// </summary>
        private static void CloseTraceListener()
        {
            //Cerramos el trace listener si esta abierto
            if (m_traceListener != null)
                m_traceListener.Close();
        }

        /// <summary>
        ///     Obtiene el metodo desde el que llamamos a la función de traceo
        /// </summary>
        /// <returns>Metodo que llama a la función de traceo</returns>
        /// <remarks>Descarta los métodos que pertenece a esta misma clase</remarks>
        private static MethodBase GetCallingMethod()
        {
            //Buscamos el metodo que nos llamo por la pila, evitando los de esta clase
            var st = new StackTrace(true);
            MethodBase callingMethod = null;
            for (var i = 0; i < st.FrameCount; ++i)
            {
                callingMethod = st.GetFrame(i).GetMethod();
                if (callingMethod.DeclaringType != typeof(Log))
                    break;
            }

            return callingMethod;
        }

        /// <summary>
        ///     Comprueba si el nivel de traza especificado esta activo
        /// </summary>
        /// <param name="traceLevel">Nivel de traza para que queremos comprobar</param>
        /// <returns></returns>
        private static bool IsTraceLevelActive(TraceLevel traceLevel)
        {
            return
                traceLevel == TraceLevel.Info && LogLevelSwitch.TraceInfo ||
                traceLevel == TraceLevel.Warning && LogLevelSwitch.TraceWarning ||
                traceLevel == TraceLevel.Error && LogLevelSwitch.TraceError;
        }

        ///// <summary>Escribe una traza en el archivo de log y en trace.axd</summary>
        ///// <param name="traceLevel">Nivel de la traza según <see cref="FSLibrary.Log.Level" /></param>
        ///// <param name="message">Mensaje que se escribira en el log.</param>
        //private static void Trace(Level traceLevel, string message, System.Web.TraceContext pageTrace)
        //{
        //	Trace(traceLevel, message);

        //	//Tambien lo hacemos visible a través de trace.axd
        //	if (pageTrace != null)
        //	if (pageTrace.IsEnabled)
        //		pageTrace.Write(traceLevel.ToString(), message);
        //}

        /// <summary>Escribe una traza en el archivo de log y en trace.axd</summary>
        /// <param name="traceLevel">Nivel de la traza según <see cref="FSLibrary.Log.Level" /></param>
        /// <param name="message">Mensaje que se escribira en el log.</param>
        private static void Trace(TraceLevel traceLevel, string message)
        {
            //Si el nivel de traza especificado no esta activo no traceamos
            if (!IsTraceLevelActive(traceLevel))
                return;

            //Aseguramos que el listener esta inicializado
            IntializeTraceListener();

            if (m_firstSection)
            {
                message += FirstSection();
            }

            var msgLog = GetMessageLog(traceLevel, message);


            if (m_traceListener != null)
            {
                m_traceListener.WriteLine(msgLog.ToString());
                m_traceListener.Flush();
            }
            else
            {
                //Creamos la traza, si tenemos nuestro listener configurado
                System.Diagnostics.Trace.AutoFlush = true;
                System.Diagnostics.Trace.IndentSize = 4;
                System.Diagnostics.Trace.WriteLine(msgLog.ToString());
            }

            using (EventLog eventLog = new EventLog("Application"))
            {
                EventLogEntryType eventType = EventLogEntryType.Information;
                if (msgLog.TraceLevel == TraceLevel.Error) eventType = EventLogEntryType.Error;
                if (msgLog.TraceLevel == TraceLevel.Warning) eventType = EventLogEntryType.Warning;

                if (traceLevel == TraceLevel.Error || traceLevel == TraceLevel.Warning)
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry(msgLog.ToString(), eventType, 2117, 1);
                }
            }


            if (OnMessageLogText != null)
                OnMessageLogText.Invoke(null, msgLog.ToString());

            if (OnMessageLog != null)
                OnMessageLog.Invoke(null, msgLog);
        }

        private static string FirstSection()
        {
            string traceSectionFormat =
                    "************************ Trace Start Time {0} ************************" +
                    Environment.NewLine +
                    "Date & Time|Milliseconds|ProccesID|Thread Id|Trace Level|Module|Type|Method|Message";

            var traceSection = new StringBuilder();
            traceSection.AppendFormat(traceSectionFormat, System.DateTime.Now);

            m_firstSection = false;

            return traceSection.ToString();
        }

        /// <summary>Escribe una traza en el archivo de log y en trace.axd</summary>
        /// <param name="traceLevel">Nivel de la traza según <see cref="FSLibrary.LogUtil.Level" /></param>
        /// <param name="message">Mensaje que se escribira en el log. Puede contener marcadores de formato.</param>
        /// <param name="args">Argumentos para los marcadores de formato de la cadena espcificada en el parametro message.</param>
        private static void Trace(TraceLevel traceLevel, string message, params object[] args)
        {
            if (args.Length > 0)
            {
                //Aplicamos el formato de message a los parametros
                var sb = new StringBuilder();
                sb.AppendFormat(message, args);
                Trace(traceLevel, sb.ToString());
            }
            else
            {
                Trace(traceLevel, message);
            }
        }

        /// <summary>Escribe una traza de nivel informativo en el archivo de log y en trace.axd. </summary>
        /// <param name="message">Mensaje que se escribirá</param>
        /// <param name="args">Parametros de sustitución que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 3 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceInfo(string message, params object[] args)
        {
            Trace(TraceLevel.Info, message, args);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log. </summary>
        /// <param name="exception">Excepción que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 3 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceInfo(Exception exception)
        {
            if (exception != null)
                Trace(
                    TraceLevel.Info,
                    ExceptionMessageFormat,
                    exception.ToString()
                );
        }


        /// <summary>Escribe una traza de nivel advertencia en el archivo de log y en trace.axd. </summary>
        /// <param name="message">Mensaje que se escribirá</param>
        /// <param name="args">Parametros de sustitución que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 2 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceWarning(string message, params object[] args)
        {
            Trace(TraceLevel.Warning, message, args);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log y en trace.axd. </summary>
        /// <param name="exception">Excepción que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 2 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceWarning(Exception exception)
        {
            if (exception != null)
                Trace(
                    TraceLevel.Warning,
                    ExceptionMessageFormat,
                    exception.ToString()
                );
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log y en trace.axd. </summary>
        /// <param name="message">Mensaje que se escribirá</param>
        /// <param name="args">Parametros de sustitución que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 1 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceError(string message, params object[] args)
        {
            Trace(TraceLevel.Error, message, args);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log y en trace.axd. </summary>
        /// <param name="exception">Excepción que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta función solo se escriben si el trace switch PrismaLogSwitch tiene un valor de 1 o
        ///     más establecido en web.config o app.config
        /// </remarks>
        public static void TraceError(Exception exception)
        {
            if (exception != null)
                Trace(
                    TraceLevel.Error,
                    ExceptionMessageFormat,
                    exception.ToString()
                );
        }

        /// <summary>
        ///     Construye el mensaje de la traza
        /// </summary>
        /// <param name="traceLevel">Nivel de traza</param>
        /// <param name="message">Mensaje a mostrar en la traza</param>
        /// <returns>Mensaje completo de traza</returns>
        private static LogMessage GetMessageLog(TraceLevel traceLevel, string message)
        {
            //Construimos la cadena con el mensajes
            var cm = GetCallingMethod();

            var now = System.DateTime.Now;
            var totalMill = 0;
            totalMill = (now - LastTime).Milliseconds;
            LastTime = now;

            return new LogMessage(
                now,
                totalMill,
                Process.GetCurrentProcess().Id,
                Thread.CurrentThread.ManagedThreadId,
                traceLevel,
                cm.DeclaringType.Module.Name,
                cm.DeclaringType.Name,
                cm.Name,
                message);
        }

        /// <summary>
        ///     Habilita el listener
        /// </summary>
        private static void EnableTraceListener()
        {
            //Recuperamos el listener del log de FebrerSoftware
            if (m_traceListener != null)
            {
                System.Diagnostics.Trace.Listeners.Remove("Default");
                System.Diagnostics.Trace.Listeners.Add(m_traceListener);
            }
        }


        public class LogMessage
        {
            public DateTime Time;
            public double Millisegundos;
            public int PID;
            public int ThreadId;
            public TraceLevel TraceLevel;
            public string ModuleName;
            public string TypeName;
            public string ProcessName;
            public string Message;

            public LogMessage(DateTime Time, double Millisegundos, int PID, int ThreadId, TraceLevel TraceLevel, string ModuleName, string TypeName, string ProcessName, string Message)
            {
                this.Time = Time;
                this.Millisegundos = Millisegundos;
                this.PID = PID;
                this.ThreadId = ThreadId;
                this.TraceLevel = TraceLevel;
                this.ModuleName = ModuleName;
                this.TypeName = TypeName;
                this.ProcessName = ProcessName;
                this.Message = Message;
            }

            public override string ToString()
            {
                return string.Format(
                        "{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                        Time,
                        Millisegundos.ToString().PadLeft(5, '0'),
                        PID,
                        ThreadId,
                        TraceLevel,
                        ModuleName,
                        TypeName,
                        ProcessName,
                        Message);
            }
        }
    }
}