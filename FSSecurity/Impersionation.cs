using FSException;
using FSLibrary;
using FSTrace;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace FSSecurity
{
    public class Impersonation : IDisposable
    {
#if NETFRAMEWORK    	
        private WindowsImpersonationContext _impersonatedUserContext;
#endif        


        /// <summary>
        ///     Class to allow running a segment of code under a given user login context
        /// </summary>
        /// <param name="user">domain\user</param>
        /// <param name="password">user's domain password</param>
        public Impersonation(string domain, string username, string password)
        {
#if NETFRAMEWORK
            var token = ValidateParametersAndGetFirstLoginToken(username, domain, password);

            var duplicateToken = IntPtr.Zero;
            try
            {
                if (!Win32API.DuplicateToken(token, 2, ref duplicateToken))
                    throw new InvalidOperationException(
                        "DuplicateToken call to reset permissions for this token failed");

                var identityForLoggedOnUser = new WindowsIdentity(duplicateToken);
                _impersonatedUserContext = identityForLoggedOnUser.Impersonate();
                if (_impersonatedUserContext == null)
                    throw new InvalidOperationException("WindowsIdentity.Impersonate() failed");
            }
            finally
            {
                if (token != IntPtr.Zero)
                    Win32API.CloseHandle(token);
                if (duplicateToken != IntPtr.Zero)
                    Win32API.CloseHandle(duplicateToken);
            }
#endif
        }

        public void Dispose()
        {
#if NETFRAMEWORK
            // Stop impersonation and revert to the process identity
            if (_impersonatedUserContext != null)
            {
                _impersonatedUserContext.Undo();
                _impersonatedUserContext = null;
            }
#endif
        }


        public static int LaunchCommand(string command, string domain, string account, string password)
        {
            var ProcessId = -1;
            var processInfo = new Win32APIEnums.PROCESS_INFORMATION();
            var startInfo = new Win32APIEnums.STARTUPINFO();
            var bResult = false;

            var uiResultWait = Win32APIEnums.INFINITE;

            var token = ValidateParametersAndGetFirstLoginToken(domain, account, password);

            var duplicateToken = IntPtr.Zero;
            try
            {
                startInfo.cb = Marshal.SizeOf(startInfo);
                //  startInfo.lpDesktop = "winsta0\\default";

                var sa = new Win32APIEnums.SECURITY_ATTRIBUTES();
                sa.Length = Marshal.SizeOf(sa);
                var si = new Win32APIEnums.STARTUPINFO();
                si.cb = Marshal.SizeOf(si);
                si.lpDesktop = string.Empty;

                bResult = Win32API.CreateProcessAsUser(
                    token,
                    null,
                    command,
                    ref sa,
                    ref sa,
                    false,
                    0,
                    IntPtr.Zero,
                    null,
                    ref startInfo,
                    ref processInfo
                );

                if (!bResult) throw new ExceptionUtil("CreateProcessAsUser error #" + Marshal.GetLastWin32Error());

                // Wait for process to end
                uiResultWait = Win32API.WaitForSingleObject(processInfo.hProcess, Win32APIEnums.INFINITE);

                ProcessId = processInfo.dwProcessID;

                if (uiResultWait == Win32APIEnums.INFINITE)
                    throw new ExceptionUtil("WaitForSingleObject error #" + Marshal.GetLastWin32Error());
            }
            finally
            {
                if (token != IntPtr.Zero)
                    Win32API.CloseHandle(token);
                if (duplicateToken != IntPtr.Zero)
                    Win32API.CloseHandle(duplicateToken);
                Win32API.CloseHandle(processInfo.hProcess);
                Win32API.CloseHandle(processInfo.hThread);
            }

            return ProcessId;
        }

        private static IntPtr ValidateParametersAndGetFirstLoginToken(string domain, string username, string password)
        {
            if (!Win32API.RevertToSelf())
            {
                Log.TraceError("RevertToSelf call to remove any prior impersonations failed");

                throw new InvalidOperationException("RevertToSelf call to remove any prior impersonations failed");
            }

            IntPtr token;

            var result = Win32API.LogonUser(domain, username,
                password,
                Win32APIEnums.LogonSessionType.NewCredentials,
                Win32APIEnums.LogonProvider.Default,
                out token);
            if (!result)
            {
                var errorCode = Marshal.GetLastWin32Error();
                Log.TraceError(string.Format(
                    "Could not impersonate the elevated user.  LogonUser: {2}\\{1} returned error code: {0}.",
                    errorCode, username, domain));
                throw new InvalidOperationException("Logon for user " + username + " failed.");
            }

            return token;
        }
    }
}