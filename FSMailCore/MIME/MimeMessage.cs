using System;
using System.Text;
using System.Collections;

namespace FSMailCore
{
    public class MimeMessage : FSMailCore.MimeBody
    {
        public MimeMessage()
        {
            SetDate();
            SetVersion();
        }

        public MimeMessage(string data)
        {
            SetDate();
            SetVersion();
            LoadBody(data);
        }

        public void SetFrom(string from)
        {
            SetFrom(from, null);
        }

        // set/get RFC 822 message header fields
        public void SetFrom(string from, string charset)
        {
            SetFieldValue("From", from, charset);
        }

        public string GetFrom()
        {
            return GetFieldValue("From");
        }

        public void SetTo(string to)
        {
            SetTo(to, null);
        }

        public void SetTo(string to, string charset)
        {
            SetFieldValue("To", to, charset);
        }
        public string GetTo()
        {
            return GetFieldValue("To");
        }

        public void SetCC(string cc)
        {
            SetCC(cc, null);
        }

        public void SetCC(string cc, string charset)
        {
            SetFieldValue("CC", cc, charset);
        }
        public string GetCC()
        {
            return GetFieldValue("CC");
        }

        public void SetBCC(string bcc, string charset)
        {
            SetFieldValue("BCC", bcc, charset);
        }
        public string GetBCC()
        {
            return GetFieldValue("BCC");
        }

        public void SetSubject(string subject)
        {
            SetSubject(subject, null);
        }

        public void SetSubject(string subject, string charset)
        {
            SetFieldValue("Subject", subject, charset);
        }
        public string GetSubject()
        {
            return GetFieldValue("Subject");
        }

        public void SetDate(string date, string charset)
        {
            SetFieldValue("Date", date, charset);
        }
        public void SetDate()
        {
            string dt = DateTime.Now.ToString("r", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            dt = dt.Replace("GMT", DateTime.Now.ToString("zz", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "00");
            SetFieldValue("Date", dt, null);
        }
        public void SetDate(DateTime date)
        {
            string dt = date.ToString("r", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            dt = dt.Replace("GMT", date.ToString("zz", System.Globalization.DateTimeFormatInfo.InvariantInfo) + "00");
            SetFieldValue("Date", dt, null);
        }
        public string GetDate()
        {
            return GetFieldValue("Date");
        }

        public void SetVersion()
        {
            SetFieldValue(MimeConst.MimeVersion, "1.0", null);
        }
        public void SetVersion(string version)
        {
            SetFieldValue(MimeConst.MimeVersion, version, null);
        }
    }
}
