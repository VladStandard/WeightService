using System;
using System.Security.Cryptography;
using System.Text;

namespace DataBaseCore.DAL.Utils
{
    public static class EncryptDecryptUtils
    {
        public static byte[] ByteCipher(int keySize = 128)
        {
            return keySize == 256 ? Encoding.UTF8.GetBytes("SSljsdkkdlo4454Maakikjhsd55GaRTP") : Encoding.UTF8.GetBytes("SSljsdkkdlo4454M");
        }

        public static string Encrypt(string source)
        {
            byte[] key = ByteCipher();
            using (TripleDESCryptoServiceProvider tripleDesCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMd5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMd5Provider.ComputeHash(key);
                    tripleDesCryptoService.Key = byteHash;
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
                    tripleDesCryptoService.Mode = CipherMode.ECB;
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
                    byte[] data = Encoding.UTF8.GetBytes(source);
                    return Convert.ToBase64String(tripleDesCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }

        public static string Decrypt(string encrypt)
        {
            byte[] key = ByteCipher();

            using (TripleDESCryptoServiceProvider tripleDesCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMd5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = hashMd5Provider.ComputeHash(key);
                    tripleDesCryptoService.Key = byteHash;
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
                    tripleDesCryptoService.Mode = CipherMode.ECB;
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
                    byte[] data = Convert.FromBase64String(encrypt);
                    return Encoding.UTF8.GetString(tripleDesCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
                }
            }
        }
    }
}
