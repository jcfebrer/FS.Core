using System;
using System.Security.Cryptography;
using System.Text;

namespace FSCryptoCore
{
    public class Md5
    {
        public static string Calc(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var md5Sp = MD5.Create();
            var hashbyte = md5Sp.ComputeHash(data, 0, data.Length);
            var strHash = BitConverter.ToString(hashbyte);
            strHash = strHash.Replace("-", "");
            return strHash;
        }


        public static string Calc(string value, string key)
        {
            var mac3Des = TripleDES.Create();
            var md5 = MD5.Create();
            mac3Des.Key = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            string valueBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            var encryptor = mac3Des.CreateEncryptor();
            string valueTripleDes = Convert.ToBase64String(encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(value), 0, value.Length));
            return valueBase64 + "-" + valueTripleDes;
        }

    }
}
