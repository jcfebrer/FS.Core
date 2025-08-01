using System;
using System.Text;

namespace FSMail
{
    public class MimeFieldCodeBase : FSMail.MimeCode
    {
        public MimeFieldCodeBase()
        {
        }

        public override string DecodeToString(string s)
        {
            string dString = "";
            int start = 0;
            while (start < s.Length)
            {
                int i = s.IndexOf("=?", start);
                if (i != -1)
                {
                    dString += s.Substring(start, i - start);
                    int j = s.IndexOf("?=", i + 2);
                    if (j != -1)
                    {
                        i += 2;
                        int k = s.IndexOf('?', i);
                        if (k != -1 && s[k + 2] == '?')
                        {
                            Charset = s.Substring(i, k - i);
                            string decString = s.Substring(k + 3, j - k - 3);
                            if (s[k + 1] == 'Q')
                            {
                                MimeCode aCode = MimeCodeManager.Instance.GetCode("quoted-printable");
                                aCode.Charset = Charset;
                                dString += aCode.DecodeToString(decString);
                            }
                            else if (s[k + 1] == 'B')
                            {
                                MimeCode aCode = MimeCodeManager.Instance.GetCode("base64");
                                aCode.Charset = Charset;
                                dString += aCode.DecodeToString(decString);
                            }
                            else
                            {
                                dString += decString;
                            }
                        }
                        else
                        {
                            dString += s.Substring(k, j - k);
                        }
                        start = j + 2;
                    }
                    else
                    {
                        dString += s.Substring(i, s.Length - i);
                        break;
                    }
                }
                else
                {
                    dString += s.Substring(start, s.Length - start);
                    break;
                }
            }
            return dString;
        }

        //folding chars
        protected virtual char[] GetFoldChars()
        {
            return null;
        }

        //need fold?
        protected virtual bool IsAutoFold()
        {
            return false;
        }

        //need delimeter?
        protected virtual bool IsNeedDelimeter()
        {
            return false;
        }

        protected virtual char[] GetDelimeterChars()
        {
            return null;
        }

        protected void EncodeDelimeter(string s, StringBuilder sb)
        {
            char[] filter = GetDelimeterChars();
            string[] strArr = s.Split(filter);
            int filterIndex = 0;
            for (int i = 0; i < strArr.Length; i++)
            {
                filterIndex += strArr[i].Length;
                if (strArr[i] != null)
                {
                    if (Charset == null)
                        Charset = System.Text.Encoding.Default.BodyName;

                    switch (SelectEncoding(strArr[i]).ToLower())
                    {
                        case "non": sb.Append(strArr[i]); break;
                        case "quoted-printable":
                            {
                                MimeCode aCode = MimeCodeManager.Instance.GetCode("quoted-printable");
                                aCode.Charset = Charset;
                                sb.AppendFormat("=?{0}?Q?{1}?=", Charset, aCode.EncodeFromString(strArr[i]));
                                break;
                            }
                        case "base64":
                            {
                                MimeCode aCode = MimeCodeManager.Instance.GetCode("base64");
                                aCode.Charset = Charset;
                                sb.AppendFormat("=?{0}?B?{1}?=", Charset, aCode.EncodeFromString(strArr[i]));
                                break;
                            }
                    }
                }
                if (filterIndex < s.Length)
                    sb.Append(s.Substring(filterIndex, 1));
                filterIndex += 1;
            }
        }

        protected void EncodeNoDelimeter(string s, StringBuilder sb)
        {
            if (Charset == null)
                Charset = System.Text.Encoding.Default.BodyName;

            switch (SelectEncoding(s).ToLower())
            {
                case "non": sb.Append(s); break;
                case "quoted-printable":
                    {
                        MimeCode aCode = MimeCodeManager.Instance.GetCode("quoted-printable");
                        aCode.Charset = Charset;
                        sb.AppendFormat("=?{0}?Q?{1}?=", Charset, aCode.EncodeFromString(s));
                        break;
                    }
                case "base64":
                    {
                        MimeCode aCode = MimeCodeManager.Instance.GetCode("base64");
                        aCode.Charset = Charset;
                        sb.AppendFormat("=?{0}?B?{1}?=", Charset, aCode.EncodeFromString(s));
                        break;
                    }
            }
        }

        public override string EncodeFromString(string s)
        {
            StringBuilder sb = new StringBuilder();

            if (IsNeedDelimeter())
                EncodeDelimeter(s, sb);
            else
                EncodeNoDelimeter(s, sb);

            if (IsAutoFold())
            {
                char[] foldChars = GetFoldChars();
                for (int i = 0; i < foldChars.Length; i++)
                {
                    string oldStr = foldChars[i].ToString();
                    string newStr = oldStr + "\r\n\t";
                    sb.Replace(oldStr, newStr);
                }
            }

            return sb.ToString();
        }

        string SelectEncoding(string s)
        {
            int nNonAscii = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNonAscii(s[i]))
                    nNonAscii += 1;
            }
            if (nNonAscii == 0)
                return "non";
            else
            {
                int nQSize = s.Length + nNonAscii * 2;
                int nBSize = (s.Length + 2) / 3 * 4;
                return (nQSize <= nBSize || nNonAscii * 5 <= s.Length) ? "quoted-printable" : "base64";
            }
        }

        bool IsNonAscii(char c)
        {
            return (int)c > 255;
        }
    }

    public class MimeFieldCodeAddress : FSMail.MimeFieldCodeBase
    {
        public MimeFieldCodeAddress()
        {
        }

        protected override char[] GetFoldChars()
        {
            char[] foldchars = { ',', ':' };
            return foldchars;
        }

        protected override bool IsAutoFold()
        {
            return true;
        }

        protected override bool IsNeedDelimeter()
        {
            return true;
        }

        protected override char[] GetDelimeterChars()
        {
            char[] d = { '(', ')', '<', '>', '"' };
            return d;
        }

    }

    public class MimeFieldCodeParameter : FSMail.MimeFieldCodeBase
    {
        public MimeFieldCodeParameter()
        {
        }

        protected override char[] GetFoldChars()
        {
            char[] foldchars = new char[1];
            foldchars[0] = ';';
            return foldchars;
        }

        protected override bool IsAutoFold()
        {
            return true;
        }

        protected override bool IsNeedDelimeter()
        {
            return true;
        }

        protected override char[] GetDelimeterChars()
        {
            char[] d = { '(', ')', '<', '>', '"' };
            return d;
        }
    }
}
