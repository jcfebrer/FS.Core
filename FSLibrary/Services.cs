using FSException;
using FSTrace;
using System;
using System.Management;
using System.ServiceProcess;

namespace FSLibrary
{
    /// <summary>
    /// Fucniones para el uso de los servicios de windows
    /// </summary>
    public class Services
    {
        /// <summary>
        /// Stops the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// or
        /// El servicio no se encuentra ejecutandose.
        /// </exception>
        public static void Stop(string serviceName)
        {
            var sc2 = new ServiceController(serviceName, ".");
            if (sc2 == null) throw new ExceptionUtil("Servicio inexistente.");
            if (sc2.Status == ServiceControllerStatus.Running)
            {
                sc2.Stop();
                sc2.WaitForStatus(ServiceControllerStatus.Stopped); //, TimeSpan.FromSeconds(30));

                Log.TraceInfo("Servicio detenido.");
            }
            else
            {
                throw new ExceptionUtil("El servicio no se encuentra ejecutandose.");
            }
        }

        /// <summary>
        /// Starts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// or
        /// or
        /// El servicio ya se encuentra iniciado.
        /// </exception>
        public static void Start(string serviceName)
        {
            var sc2 = new ServiceController("FSVncService", ".");
            if (sc2 == null) throw new ExceptionUtil("Servicio inexistente.");
            if (sc2.Status == ServiceControllerStatus.Stopped)
            {
                // Start service
                var wmiService = new ManagementObject("Win32_Service.Name='" + serviceName + "'");
                wmiService.Get();
                var outParams = wmiService.InvokeMethod("StartService", null, null);
                var ret = (uint) outParams.Properties["ReturnValue"].Value;
                if (ret != 0)
                    throw new ExceptionUtil(string.Format("Error al iniciar el servicio con el código de error: {0}", ret));
                Log.TraceInfo("Servicio iniciado.");
            }
            else
            {
                throw new ExceptionUtil("El servicio ya se encuentra iniciado.");
            }
        }
    }
}