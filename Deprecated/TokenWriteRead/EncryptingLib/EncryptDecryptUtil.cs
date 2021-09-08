using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

public static class EncryptDecryptUtil
{
    public static byte[] ByteCipher(int keySize = 128)
    {
        return keySize == 256 ? Encoding.UTF8.GetBytes("SSljsdkkdlo4454Maakikjhsd55GaRTP") : Encoding.UTF8.GetBytes("SSljsdkkdlo4454M");
    }

    public static string Encrypt(string source)
    {
        byte[] key = ByteCipher();
        using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
        {
            using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
            {
                byte[] byteHash = hashMD5Provider.ComputeHash(key);
                tripleDESCryptoService.Key = byteHash;
                tripleDESCryptoService.Mode = CipherMode.ECB;
                byte[] data = Encoding.UTF8.GetBytes(source);
                return Convert.ToBase64String(tripleDESCryptoService.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
            }
        }
    }

    public static string Decrypt(string encrypt)
    {
        byte[] key = ByteCipher();

        using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
        {
            using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
            {
                byte[] byteHash = hashMD5Provider.ComputeHash(key);
                tripleDESCryptoService.Key = byteHash;
                tripleDESCryptoService.Mode = CipherMode.ECB;
                byte[] data = Convert.FromBase64String(encrypt);
                return Encoding.UTF8.GetString(tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
            }
        }
    }

}
