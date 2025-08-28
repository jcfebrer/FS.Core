using System;
using System.Runtime.InteropServices;
using System.Threading;

#if NET40_OR_GREATER || NETCOREAPP
    using System.Threading.Tasks;
#endif

namespace FSLibrary
{
    /// <summary>
    /// Clase para el manejo de la energia, suspensión, mantener activo...
    /// </summary>
    public static class PowerEnergy
    {
        private static AutoResetEvent _event = new AutoResetEvent(false);

        /// <summary>
        /// Evitar que el equipo entre en suspensión.
        /// </summary>
        public static void PreventPowerSave()
        {
#if NET40_OR_GREATER || NETCOREAPP
            (new TaskFactory()).StartNew(() =>
            {
#endif
                Win32API.SetThreadExecutionState(
                    Win32APIEnums.EXECUTION_STATE.ES_CONTINUOUS
                    | Win32APIEnums.EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | Win32APIEnums.EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                _event.WaitOne();

#if NET40_OR_GREATER || NETCOREAPP
            }, TaskCreationOptions.LongRunning);
#endif
        }

        /// <summary>
        /// Apagado
        /// </summary>
        public static void Shutdown()
        {
            _event.Set();
            // Win32API.SetThreadExecutionState(Win32APIEnums.EXECUTION_STATE.ES_CONTINUOUS);
        }
    }
}