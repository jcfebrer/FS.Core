using FSLibrary;
using System;
using System.Security.Principal;

namespace FSSecurity
{
    /// <summary>
    /// // WindowsIdentity user = (WindowsIdentity)context.User.Identity;
    /// // using (WindowsIdentity user = WindowsIdentity.GetCurrent())
    ///
    /// using (WindowsLogin wi = new WindowsLogin("Administrator", System.Environment.MachineName, "TOP_SECRET"))
    /// {
    /// #if NETFRAMEWORK
    ///     using (user.Impersonate())
    /// #else
    ///     WindowsIdentity.RunImpersonated(wi.Identity.AccessToken, () =>
    /// #endif
    /// {
    ///     WindowsIdentity useri = WindowsIdentity.GetCurrent();
    ///     System.Console.WriteLine(useri.Name);
    /// }
    /// #if NETCOREAPP
    ///     );
    /// #endif
    /// }
    /// </summary>
    public class WindowsLogin : System.IDisposable
    {
        public WindowsIdentity Identity = null;
        private System.IntPtr m_accessToken;

#if NETFRAMEWORK
        private WindowsImpersonationContext impersonationContext = null;
#endif

        // AccessToken ==> this.Identity.AccessToken
        //public Microsoft.Win32.SafeHandles.SafeAccessTokenHandle AT
        //{
        //    get
        //    {
        //        var at = new Microsoft.Win32.SafeHandles.SafeAccessTokenHandle(this.m_accessToken);
        //        return at;
        //    }
        //}


        public WindowsLogin()
        {
            this.Identity = WindowsIdentity.GetCurrent();
        }


        public string UserName()
        {
            return WindowsIdentity.GetCurrent().Name;
        }


        public WindowsLogin(string username, string domain, string password)
        {
            Login(username, domain, password);
        }


        public void Login(string username, string domain, string password)
        {
            // if domain name was blank, assume local machine
            if (String.IsNullOrEmpty(domain))
                domain = System.Environment.MachineName;

            if (this.Identity != null)
            {
                this.Identity.Dispose();
                this.Identity = null;
            }

            try
            {
                this.m_accessToken = new System.IntPtr(0);
                Logout();

                this.m_accessToken = System.IntPtr.Zero;
                bool logonSuccessfull = Win32API.LogonUser(
                   username,
                   domain,
                   password,
                   Win32APIEnums.LogonSessionType.Interactive,
                   Win32APIEnums.LogonProvider.Default,
                   out this.m_accessToken);

                if (!logonSuccessfull)
                {
                    int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(error);
                }

                // Create a new WindowsIdentity object using the access token
                Identity = new WindowsIdentity(this.m_accessToken);

#if NETFRAMEWORK
                // Begin impersonating the user
                impersonationContext = WindowsIdentity.Impersonate(this.m_accessToken);
#endif
            }
            catch
            {
                throw;
            }
        }


        public void Logout()
        {
            if (this.m_accessToken != System.IntPtr.Zero)
                Win32API.CloseHandle(m_accessToken);

            this.m_accessToken = System.IntPtr.Zero;

            if (this.Identity != null)
            {
                this.Identity.Dispose();
                this.Identity = null;
            }

#if NETFRAMEWORK
            if (impersonationContext != null)
                impersonationContext.Undo();
#endif
        }


        void System.IDisposable.Dispose()
        {
            Logout();
        }
    }
}