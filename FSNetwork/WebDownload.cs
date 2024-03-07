#region

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading;

#endregion

namespace FSNetwork
{
    public class WebDownload
    {
        #region Delegates

        public delegate void CompleteCallbackEventHandler(byte[] dataDownloaded);

        public delegate void ProgressCallbackEventHandler(int bytesRead, int totalBytes);

        #endregion

        private const int BUFFER_SIZE = 1024;
        public ManualResetEvent allDone = new ManualResetEvent(false);

        public string m_DownloadUrl = "";

        public string DownloadUrl
        {
            get { return m_DownloadUrl; }
            set { m_DownloadUrl = value; }
        }

        public event CompleteCallbackEventHandler CompleteCallback;
        public event ProgressCallbackEventHandler ProgressCallback;

        public void Download()
        {
            if ((DownloadUrl != ""))
            {
                byte[] downloadedData = Download(DownloadUrl);
                if (null != CompleteCallback) CompleteCallback(downloadedData);
            }
        }


        public byte[] Download(string url)
        {
            allDone.Reset();

            Uri httpSite = new Uri(url);

            WebRequest req = WebRequest.Create(httpSite);

            DownloadInfo info = new DownloadInfo();

            info.Request = req;

            IAsyncResult r = req.BeginGetResponse(ResponseCallback, info);

            allDone.WaitOne();


            if ((info.useFastBuffers))
            {
                return info.dataBufferFast;
            }
            else
            {
                byte[] data = null;
                int b = 0;
                data = new byte[info.dataBufferSlow.Count];
                for (b = 0; b <= info.dataBufferSlow.Count - 1; b++)
                {
                    data[b] = (byte) info.dataBufferSlow[b];
                }
                return data;
            }
        }


        private void ResponseCallback(IAsyncResult ar)
        {
            DownloadInfo info = ((DownloadInfo) (ar.AsyncState));

            WebRequest req = info.Request;

            WebResponse resp = req.EndGetResponse(ar);

            string strContentLength = resp.Headers["Content-Length"];
            if ((strContentLength != null))
            {
                info.dataLength = Convert.ToInt32(strContentLength);
                info.dataBufferFast = new byte[info.dataLength];
            }
            else
            {
                info.useFastBuffers = false;
                info.dataBufferSlow = new ArrayList(BUFFER_SIZE);
            }

            Stream ResponseStream = resp.GetResponseStream();

            info.ResponseStream = ResponseStream;

            IAsyncResult iarRead = ResponseStream.BeginRead(info.BufferRead, 0, BUFFER_SIZE, ReadCallBack, info);
        }


        private void ReadCallBack(IAsyncResult asyncResult)
        {
            DownloadInfo info = ((DownloadInfo) (asyncResult.AsyncState));

            Stream responseStream = info.ResponseStream;

            int bytesRead = responseStream.EndRead(asyncResult);
            if ((bytesRead > 0))
            {
                if ((info.useFastBuffers))
                {
                    Array.Copy(info.BufferRead, 0, info.dataBufferFast, info.bytesProcessed, bytesRead);
                }
                else
                {
                    int b = 0;
                    for (b = 0; b <= bytesRead - 1; b++)
                    {
                        info.dataBufferSlow.Add(info.BufferRead[b]);
                    }
                }
                info.bytesProcessed = info.bytesProcessed + bytesRead;

                if (null != ProgressCallback) ProgressCallback(info.bytesProcessed, info.dataLength);

                IAsyncResult ar = responseStream.BeginRead(info.BufferRead, 0, BUFFER_SIZE, ReadCallBack, info);
            }
            else
            {
                responseStream.Close();
                allDone.Set();
            }
        }
        
        
        public static void UploadFile(string fileSource, string urlDestFile)
        {
            new WebClient().UploadFile(fileSource, urlDestFile);
        }


		public static void DownloadFile(string urlSourceFile, string destFile)
        {
            new WebClient().DownloadFile(urlSourceFile, destFile);
        }
    }


    public class DownloadInfo
    {
        private const int BufferSize = 1024;
        public byte[] BufferRead;
        public WebRequest Request;
        public Stream ResponseStream;
        public int bytesProcessed;

        public byte[] dataBufferFast;
        public ArrayList dataBufferSlow;

        public int dataLength;
        public bool useFastBuffers;

        public DownloadInfo()
        {
            BufferRead = new byte[BufferSize];
            Request = null;
            dataLength = -1;
            bytesProcessed = 0;
            useFastBuffers = true;
        }
    }
}