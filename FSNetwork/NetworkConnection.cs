using FSLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FSNetwork
{

    public class NetworkConnection : IDisposable
    {
        string _networkName;

        public NetworkConnection(string networkName,
            NetworkCredential credentials)
        {
            _networkName = networkName;

            var netResource = new Win32APIEnums.NetResource()
            {
                Scope = Win32APIEnums.ResourceScope.GlobalNetwork,
                ResourceType = Win32APIEnums.ResourceType.Disk,
                DisplayType = Win32APIEnums.ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var userName = string.IsNullOrEmpty(credentials.Domain)
                ? credentials.UserName
                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

            var result = Win32API.WNetAddConnection2(
                netResource,
                credentials.Password,
                userName,
                0);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }
        }

        ~NetworkConnection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Win32API.WNetCancelConnection2(_networkName, 0, true);
        }
    }
}