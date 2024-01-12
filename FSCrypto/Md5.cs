using System;
using System.Security.Cryptography;
using System.Text;

namespace FSCrypto
{
    public class Md5
    {
        public static string Calc(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var md5Sp = new MD5CryptoServiceProvider();
            var hashbyte = md5Sp.ComputeHash(data, 0, data.Length);
            var strHash = BitConverter.ToString(hashbyte);
            strHash = strHash.Replace("-", "");
            return strHash;
        }


        public static string Calc(string value, string key)
        {
            var mac3Des = new MACTripleDES();
            var md5 = new MD5CryptoServiceProvider();
            mac3Des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            string valueBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            string valueTripleDes = Convert.ToBase64String(mac3Des.ComputeHash(Encoding.UTF8.GetBytes(value)));
            return valueBase64 + "-" + valueTripleDes;
        }

    }
}
