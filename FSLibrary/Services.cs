using FSException;
using FSTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace FSLibrary
{
    /// <summary>
    /// Fucniones para el uso de los servicios de windows
    /// </summary>
    public class Services
    {
        /// <summary>
        /// Devuelve true/false si el servicio existe o no.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool ExistService(string serviceName)
        {
            return ExistService(serviceName, "");
        }

        /// <summary>
        /// Devuelve true/false si el servicio existe o no.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static bool ExistService(string serviceName, string machineName)
        {
            ServiceController[] ctl;
            if (String.IsNullOrEmpty(machineName))
                ctl = ServiceController.GetServices();
            else
                ctl = ServiceController.GetServices(machineName);

            ServiceController sc = ctl.FirstOrDefault(s => s.ServiceName == serviceName);
            if (sc == null)
                return false;
            else
                return true;
        }

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
            Stop(serviceName, "");
        }

        /// <summary>
        /// Stops the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// or
        /// El servicio no se encuentra ejecutandose.
        /// </exception>
        public static void Stop(string serviceName, string machineName)
        {
            if (!ExistService(serviceName)) throw new ExceptionUtil("Servicio inexistente.");

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            if (sc.Status == ServiceControllerStatus.Running)
            {
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));

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
        /// o
        /// El servicio ya se encuentra iniciado.
        /// </exception>
        public static void Start(string serviceName)
        {
            Start(serviceName, "");
        }

        /// <summary>
        /// Starts the specified service name.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <exception cref="ExceptionUtil">
        /// Servicio inexistente.
        /// o
        /// El servicio ya se encuentra iniciado.
        /// </exception>
        public static void Start(string serviceName, string machineName)
        {
            if (!ExistService(serviceName)) throw new ExceptionUtil("Servicio inexistente.");

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start();
                //new SystemInfo().StartService(serviceName);
                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));

                Log.TraceInfo("Servicio iniciado.");
            }
            else
            {
                throw new ExceptionUtil("El servicio ya se encuentra iniciado.");
            }
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <returns></returns>
        public static string ListServices()
        {
            return ListServices("");
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static string ListServices(string machineName)
        {
            StringBuilder sb = new StringBuilder();
            ServiceController[] services;

            if (String.IsNullOrEmpty(machineName))
                services = ServiceController.GetServices();
            else
                services = ServiceController.GetServices(machineName);

            foreach (ServiceController sc in services)
            {
                sb.AppendLine(sc.ServiceName + ":" + sc.Status.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <returns></returns>
        public static List<ServiceInfo> GetServices()
        {
            return GetServices("");
        }

        /// <summary>
        /// Listado de servicios
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static List<ServiceInfo> GetServices(string machineName)
        {
            List<ServiceInfo> serviceData = new List<ServiceInfo>();
            ServiceController[] services;

            if (String.IsNullOrEmpty(machineName))
                services = ServiceController.GetServices();
            else
                services = ServiceController.GetServices(machineName);

            foreach (ServiceController sc in services)
            {
                ServiceInfo sd = new ServiceInfo();
                sd.ServiceName = sc.ServiceName;
                sd.Status = (ServiceInfo.ServiceStatus)sc.Status;
                sd.DisplayName = sc.DisplayName;
                sd.MachineName = sc.MachineName;
                sd.ServiceType = sc.ServiceType.ToString();
                serviceData.Add(sd);
            }
            return serviceData;
        }

        /// <summary>
        /// Información de servicio
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static ServiceInfo GetService(string serviceName)
        {
            return GetService(serviceName, "");
        }

        /// <summary>
        /// Información de servicio
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public static ServiceInfo GetService(string serviceName, string machineName)
        {
            if (!ExistService(serviceName)) throw new ExceptionUtil("Servicio inexistente.");

            ServiceController sc;
            if (String.IsNullOrEmpty(machineName))
                sc = new ServiceController(serviceName);
            else
                sc = new ServiceController(serviceName, machineName);

            ServiceInfo serviceInfo = new ServiceInfo();
            serviceInfo.ServiceName = sc.ServiceName;
            serviceInfo.Status = (ServiceInfo.ServiceStatus)sc.Status;
            serviceInfo.DisplayName = sc.DisplayName;
            serviceInfo.MachineName = sc.MachineName;
            serviceInfo.ServiceType = sc.ServiceType.ToString();
            return serviceInfo;
        }
    }

    public class ServiceInfo
    {
        public enum ServiceStatus
        {
            ContinuePending = 5,
            Paused = 7,
            PausePending = 6,
            Running = 4,
            StartPending = 2,
            Stopped = 1,
            StopPending = 3
        }

        public string ServiceName { get; set; }
        public ServiceStatus Status { get; set; }
        public string DisplayName { get; set; }
        public string MachineName { get; set; }
        public string ServiceType { get; set; }
    }
}