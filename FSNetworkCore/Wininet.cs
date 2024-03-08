#region

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using FSLibraryCore;

#endregion

namespace FSNetworkCore
{
    public class WinInet : IDisposable


    {
        private const int INTERNET_ACCESS_TYPE_DIRECT = 1;

        private const string USER_AGENT = "IE";
        private const string HEADER = "Accept: */*" + "\r\r";
        private const int CONTEXT = 0;
        private const int FLAGS = 0;
        private bool _disposed;

        private IntPtr _handle;

        public WinInet()
        {
            _handle = InternetOpen(USER_AGENT, INTERNET_ACCESS_TYPE_DIRECT, "", "", FLAGS);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion

        [DllImport("WinInet.dll", EntryPoint = "InternetOpenA", CharSet = CharSet.Ansi, ExactSpelling = true,
            SetLastError = true)]
        private static extern IntPtr InternetOpen(string agent, Int32 accessType, string proxyName, string proxyBypass,
                                                  Int32 flags);

        [DllImport("WinInet.dll", EntryPoint = "InternetOpenUrlA", CharSet = CharSet.Ansi, ExactSpelling = true,
            SetLastError = true)]
        private static extern Int32 InternetOpenUrl(IntPtr session, string url, string header, Int32 headerLength,
                                                    Int32 flags, Int32 context);

        [DllImport("WinInet.dll", EntryPoint = "InternetReadFile", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Int32 InternetReadFile(Int32 handle, [MarshalAs(UnmanagedType.LPArray)] byte[] newBuffer,
                                                     Int32 bufferLength, ref Int32 bytesRead);

        [DllImport("WinInet.dll", EntryPoint = "InternetCloseHandle", CharSet = CharSet.Ansi, ExactSpelling = true,
            SetLastError = true)]
        private static extern Int32 InternetCloseHandle(Int32 hInternet);

        public string GetHttpFile(string url)
        {
            Int32 bufferLength = 1024;
            MemoryStream bufferStream = new MemoryStream();
            BinaryWriter bufferStreamWriter = new BinaryWriter(bufferStream);

            Int32 session = 0;

            session = InternetOpenUrl(_handle, url, HEADER, HEADER.Length, 0, CONTEXT);

            if (session <= 0)
            {
                throw new WebException(Marshal.GetLastWin32Error().ToString());
            }
            else
            {
                byte[] newBuffer = null;
                Int32 bytesRead = 0;
                bool response = false;

                do
                {
                    newBuffer = new byte[bufferLength - 1];
                    response = Convert.ToBoolean(InternetReadFile(session, newBuffer, bufferLength, ref bytesRead));
                    if (!(response))
                    {
                        throw new WebException(Marshal.GetLastWin32Error().ToString());
                    }
                    else
                    {
                        bufferStreamWriter.Write(newBuffer, 0, bytesRead);
                    }
                } while (response & bytesRead > 0);
            }
            InternetCloseHandle(session);

            byte[] bufferBytes = bufferStream.GetBuffer();

            bufferStreamWriter.Close();
            bufferStream.Close();

			string str = FSLibraryCore.NumberUtils.BytesToString(bufferBytes);
            return str;
        }


        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            InternetCloseHandle(_handle.ToInt32());
            _disposed = true;
        }

        // interface methods implemented by Dispose


        ~WinInet()
        {
            Dispose();
        }
    }
}