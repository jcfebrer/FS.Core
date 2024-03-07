using System;
using System.Security.Cryptography;
using System.Text;

namespace FSCryptoCore
{
    public class Sha256
    {
        public static string Calc(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var shaSp = new SHA256CryptoServiceProvider();
            var hashbyte = shaSp.ComputeHash(data, 0, data.Length);
            var strHash = BitConverter.ToString(hashbyte);
            strHash = strHash.Replace("-", "");
            return strHash;
        }


        public static string Calc(string value, string key)
        {
            var mac3Des = TripleDES.Create();
            var sha = new SHA256CryptoServiceProvider();
            mac3Des.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(key));
            string valueBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
            var encryptor = mac3Des.CreateEncryptor();
            string valueTripleDes = Convert.ToBase64String(encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(value), 0, value.Length));
            return valueBase64 + "-" + valueTripleDes;
        }
    }
}
