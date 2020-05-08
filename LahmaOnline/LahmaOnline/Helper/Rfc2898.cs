using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace LahmaOnline.Helper
{
    public static class Rfc2898
    {
        public static string GenerateEncryptionKey()
        {
            Random Robj = new Random(Guid.NewGuid().GetHashCode());
            var minValue = DateTime.Now.Hour + DateTime.Now.Minute * DateTime.Now.Second + DateTime.Now.Millisecond;
            var maxValue = DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second * DateTime.Now.Millisecond + DateTime.Now.Hour;
            if (minValue == maxValue)
                maxValue++;
            else if (minValue > maxValue)
            {
                var temp = minValue;
                minValue = maxValue;
                maxValue = temp;
            }
            int Rnumber = Robj.Next(minValue, maxValue);
            string EncryptionKey = "PrAwoship" + Convert.ToString(Rnumber);
            return EncryptionKey;
        }
        public static (string Encrypt,string KeyUsed) Encrypt(string clearText, string Key="")
        {
            var EncryptionKey = string.IsNullOrWhiteSpace(Key)? GenerateEncryptionKey() : Key;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return (clearText, EncryptionKey);
        }
        public static string Decrypt(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
