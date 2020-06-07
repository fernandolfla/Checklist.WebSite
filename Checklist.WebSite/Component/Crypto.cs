using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Checklist.WebSite.Component
{
    public class Crypto
    {
        public static string MD5(string texto)
        {
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes("CheckJuriseg"));
            des.Mode = CipherMode.ECB;

            ICryptoTransform crypt = des.CreateEncryptor();

            byte[] buff = ASCIIEncoding.ASCII.GetBytes(texto);
            return Convert.ToBase64String(crypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        public static string Token(string texto)
        {
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes("Avelar"));
            des.Mode = CipherMode.ECB;

            ICryptoTransform crypt = des.CreateEncryptor();

            byte[] buff = ASCIIEncoding.ASCII.GetBytes(texto);
            return Convert.ToBase64String(crypt.TransformFinalBlock(buff, 0, buff.Length));
        }



        public static bool Autenticar(string key)
        {
            if (key.Equals("juriseg"))
                return true;
            return false;

        }

    }
}