// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Security.Cryptography;
using System.Text;

namespace DataCore.DAL.Utils
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
            using TripleDESCryptoServiceProvider tripleDesCryptoService = new();
            using MD5CryptoServiceProvider hashMd5Provider = new();
            byte[] byteHash = hashMd5Provider.ComputeHash(key);
            tripleDesCryptoService.Key = byteHash;
            tripleDesCryptoService.Mode = CipherMode.ECB;
            byte[] data = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(tripleDesCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
        }

        public static string Decrypt(string encrypt)
        {
            byte[] key = ByteCipher();

            using TripleDESCryptoServiceProvider tripleDesCryptoService = new();
            using MD5CryptoServiceProvider hashMd5Provider = new();
            byte[] byteHash = hashMd5Provider.ComputeHash(key);
            tripleDesCryptoService.Key = byteHash;
            tripleDesCryptoService.Mode = CipherMode.ECB;
            byte[] data = Convert.FromBase64String(encrypt);
            return Encoding.UTF8.GetString(tripleDesCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
        }
    }
}
