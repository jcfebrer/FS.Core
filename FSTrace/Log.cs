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

#if NETCOREAPP
    using Microsoft.Extensions.Configuration;
#endif

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

#endregion

namespace FSTrace
{
    public class LogMessage
    {
        public DateTime Time { get; set; }
        public double Millisegundos { get; set; }
        public int PID { get; set; }
        public int ThreadId { get; set; }
        public TraceLevel TraceLevel { get; set; }
        public string ModuleName { get; set; }
        public string TypeName { get; set; }
        public string ProcessName { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }

        public LogMessage()
        {
        }

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

        public LogMessage(DateTime Time, TraceLevel TraceLevel, string Message)
        {
            this.Time = Time;
            this.TraceLevel = TraceLevel;
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

        public bool IsTraceLevel(TraceLevel level, bool state)
        {
            if (this.TraceLevel == level)
                return state;
            return false;
        }
    }

    /// <summary>Implementa funcionalidad de traceo</summary>
    public static class Log
    {
        private static List<LogMessage> logMessages = new List<LogMessage>();
        public static List<LogMessage> LogMessages
        {
            get { return logMessages; }
            set { logMessages = value; }
        }

        public delegate void MessageLogEventHandler(object source, LogMessage e);
        public delegate void MessageLogTextEventHandler(object source, string e);
        public static event MessageLogTextEventHandler OnMessageLogText;
        public static event MessageLogEventHandler OnMessageLog;
        public static TraceLevel m_LogTraceLevel = TraceLevel.Verbose;

        private const string ExceptionMessageFormat = "The following exception happened: {0}";
        private static System.DateTime LastTime = System.DateTime.Now;
        private static TraceListener m_traceListener;
        private static bool m_firstSection = true;
        private static TraceSwitch m_logLevelSwitch;
        private static bool m_saveLogData = true;
        private static bool m_groupData = false;

        public static TraceLevel LogTraceLevel
        {
            get { return m_LogTraceLevel; }
            set
            {
                m_LogTraceLevel = value;
                LogLevelSwitch.Level = value;
            }
        }

        public static bool SaveLogData
        {
            get { return m_saveLogData; }
            set { m_saveLogData = value; }
        }

        public static bool GroupData
        {
            get { return m_groupData; }
            set { m_groupData = value; }
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
        ///     Obtiene el archivo de logs, desde el archivo de configuraci�n
        ///     de la aplicaci�n. Si solo se ha especificado el nombre de archivo
        ///     se logea en el directorio en que se encuentra el proceso.
        /// </summary>
        private static string GetLogFile()
        {
#if NETFRAMEWORK
#if NET40_OR_GREATER
            string logFile = ConfigurationManager.AppSettings["LogFile"];
#else
            string logFile = "FSLog.txt";
#endif
#else
            IConfiguration configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            string logFile = configurationBuilder.GetValue<string>("appSettings:LogFile");
#endif            

            if (String.IsNullOrEmpty(logFile))
                return null;

            if (logFile.Contains(@".\"))
                logFile = AppDomain.CurrentDomain.BaseDirectory +
                          logFile.Replace(@".\", "");

            if (!logFile.Contains(@"\"))
                logFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                          @"\" + logFile;

            //Si solo se ha especificado nombre del archivo de log
            //en el archivo de configuraci�n a�adimos el path del proceso
            return logFile;
        }

#if NET30 || NET20
        private static bool IsMessageMatch(LogMessage message, bool error, bool warning, bool info)
        {
            return message.IsTraceLevel(TraceLevel.Error, error) ||
                   message.IsTraceLevel(TraceLevel.Warning, warning) ||
                   message.IsTraceLevel(TraceLevel.Info, info);
        }

        public static List<LogMessage> GetLogData(bool error, bool warning, bool info)
        {
            List<LogMessage> result = new List<LogMessage>();

            foreach (LogMessage message in logMessages)
            {
                if (IsMessageMatch(message, error, warning, info))
                {
                    result.Add(message);
                }
            }

            return result;
        }
#else
        public static List<LogMessage> GetLogData(bool error, bool warning, bool info)
        {
            Func<LogMessage, bool> predicate1 = s => s.IsTraceLevel(TraceLevel.Error, error);
            Func<LogMessage, bool> predicate2 = s => s.IsTraceLevel(TraceLevel.Warning, warning);
            Func<LogMessage, bool> predicate3 = s => s.IsTraceLevel(TraceLevel.Info, info);
            return logMessages.FindAll(s => (predicate1(s) || predicate2(s) || predicate3(s)));
        }
#endif

        public static List<LogMessage> GetLogData(TraceLevel level)
        {
            return logMessages.FindAll(x => x.TraceLevel == level);
        }

        public static List<LogMessage> GetLogData()
        {
            return logMessages;
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
        ///     Obtiene el metodo desde el que llamamos a la funci�n de traceo
        /// </summary>
        /// <returns>Metodo que llama a la funci�n de traceo</returns>
        /// <remarks>Descarta los m�todos que pertenece a esta misma clase</remarks>
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
        ///// <param name="traceLevel">Nivel de la traza seg�n <see cref="FSLibrary.Log.Level" /></param>
        ///// <param name="message">Mensaje que se escribira en el log.</param>
        //private static void Trace(Level traceLevel, string message, System.Web.TraceContext pageTrace)
        //{
        //	Trace(traceLevel, message);

        //	//Tambien lo hacemos visible a trav�s de trace.axd
        //	if (pageTrace != null)
        //	if (pageTrace.IsEnabled)
        //		pageTrace.Write(traceLevel.ToString(), message);
        //}

        /// <summary>Escribe una traza en el archivo de log y en trace.axd</summary>
        /// <param name="traceLevel">Nivel de la traza seg�n <see cref="FSLibrary.Log.Level" /></param>
        /// <param name="message">Mensaje que se escribira en el log.</param>
        private static void Trace(TraceLevel traceLevel, string message)
        {
            //Si el nivel de traza especificado no esta activo no traceamos
            if (!IsTraceLevelActive(traceLevel))
                return;

            //Aseguramos que el listener esta inicializado
            IntializeTraceListener();

            LogMessage msgLog = GetMessageLog(traceLevel, message);


            if (m_traceListener != null)
            {
                if(m_firstSection) m_traceListener.WriteLine(FirstSection());
                m_traceListener.WriteLine(msgLog.ToString());
                m_traceListener.Flush();
            }
            else
            {
                //Creamos la traza, si tenemos nuestro listener configurado
                System.Diagnostics.Trace.AutoFlush = true;
                System.Diagnostics.Trace.IndentSize = 4;

                if (m_firstSection) System.Diagnostics.Trace.WriteLine(FirstSection());
                System.Diagnostics.Trace.WriteLine(msgLog.ToString());
            }

            if (m_saveLogData)
            {
                // Si esta la opci�n de agrupar, y existe el evento, actualizamos la informaci�n del evento.
                if (m_groupData && logMessages.Exists(e => e.Message == msgLog.Message))
                {
                    LogMessage logUpdate = logMessages.Find(e => e.Message == msgLog.Message);
                    logUpdate.Time = msgLog.Time;
                    logUpdate.Count++;
                }
                else
                {
                    // Creamos una nueva entrada
                    logMessages.Add(new LogMessage(msgLog.Time, msgLog.TraceLevel, msgLog.Message));
                }
            }

#if NETFRAMEWORK
            using (EventLog eventLog = new EventLog("Application"))
            {
                EventLogEntryType eventType = EventLogEntryType.Information;
                if (msgLog.TraceLevel == TraceLevel.Error) eventType = EventLogEntryType.Error;
                if (msgLog.TraceLevel == TraceLevel.Warning) eventType = EventLogEntryType.Warning;

                if (traceLevel == TraceLevel.Error || traceLevel == TraceLevel.Warning)
                {
                    eventLog.Source = "Application";
                    if (m_firstSection) eventLog.WriteEntry(FirstSection());
                    eventLog.WriteEntry(msgLog.ToString(), eventType, 2117, 1);
                }
            }
#endif


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
        /// <param name="traceLevel">Nivel de la traza seg�n <see cref="FSLibrary.LogUtil.Level" /></param>
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
        /// <param name="message">Mensaje que se escribir�</param>
        /// <param name="args">Parametros de sustituci�n que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 3 o
        ///     m�s establecido en web.config o app.config
        /// </remarks>
        public static void TraceInfo(string message, params object[] args)
        {
            Trace(TraceLevel.Info, message, args);
        }

        public static void Trace(string message)
        {
            Trace(TraceLevel.Info, message);
        }

        public static void Trace(Exception exception)
        {
            Trace(exception);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log. </summary>
        /// <param name="exception">Excepci�n que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 3 o
        ///     m�s establecido en web.config o app.config
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
        /// <param name="message">Mensaje que se escribir�</param>
        /// <param name="args">Parametros de sustituci�n que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 2 o
        ///     m�s establecido en web.config o app.config
        /// </remarks>
        public static void TraceWarning(string message, params object[] args)
        {
            Trace(TraceLevel.Warning, message, args);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log y en trace.axd. </summary>
        /// <param name="exception">Excepci�n que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 2 o
        ///     m�s establecido en web.config o app.config
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
        /// <param name="message">Mensaje que se escribir�</param>
        /// <param name="args">Parametros de sustituci�n que se usan si la cadena tiene indicadores de formato.</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 1 o
        ///     m�s establecido en web.config o app.config
        /// </remarks>
        public static void TraceError(string message, params object[] args)
        {
            Trace(TraceLevel.Error, message, args);
        }

        /// <summary>Escribe una traza de nivel error en el archivo de log y en trace.axd. </summary>
        /// <param name="exception">Excepci�n que queremos logear</param>
        /// <remarks>
        ///     Las trazas lanzadas con esta funci�n solo se escriben si el trace switch DefaultSwitch tiene un valor de 1 o
        ///     m�s establecido en web.config o app.config
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
    }
}