using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using WeightServices.Common;

namespace EntitiesLib
{
    [Serializable]
    public class HostEntity
    {
        private static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";

        public int Id { get; set; }
        public int CurrentScaleId { get; set; }
        public String Name { get; set; }
        public String IP { get; set; }
        public String MAC { get; set; }
        public Guid IdRRef { get; set; }
        public bool Marked { get; set; }

        public override string ToString()
        {
            return $"{Name} ({IP})({MAC})" ;
        }

        [XmlIgnoreAttribute]
        public XDocument SettingsFile { get; set; }

        public string SerializeObject()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HostEntity));
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }

        public void Load()
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = @"SELECT TOP(1) h.[Id],h.[Name],h.[IP],h.[MAC],h.[IdRRef],h.[Marked],h.[SettingsFile],s.[Id] CurrentScaleId
                    FROM [db_scales].[Hosts] h
                      LEFT JOIN [db_scales].[Scales] s
                      ON h.ID = s.[HostId]
                      WHERE h.[Marked] = 0 AND h.[IdRRef] = @ID ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", this.IdRRef);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Name = SqlConnectFactory.GetValue<string>(reader, "Name");
                        IP = SqlConnectFactory.GetValue<string>(reader, "IP");
                        MAC = SqlConnectFactory.GetValue<string>(reader, "MAC");
                        Marked = SqlConnectFactory.GetValue<bool>(reader, "Marked");
                        SettingsFile = XDocument.Parse(SqlConnectFactory.GetValue<string>(reader, "SettingsFile"));
                        Id = SqlConnectFactory.GetValue<int>(reader, "Id");
                        CurrentScaleId = SqlConnectFactory.GetValue<int>(reader, "CurrentScaleId");
                    }

                    reader.Close();
                    con.Close();

                }
            }

        }
 
        public void TokenRead()
        {
            if (!File.Exists(FilePathToken))
            {
                return;
            }
            XDocument doc = XDocument.Load(FilePathToken);
            IdRRef = Guid.Parse( doc.Root.Elements("ID").First().Value );
            var EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
            Load();
        }

        public static Guid TokenWrite(string connectionString)
        {

            Guid tokenSalt = Guid.NewGuid();
            XDocument doc = new XDocument();

            XElement root = new XElement("root");
            root.Add(
                new XElement("ID", tokenSalt),
                new XElement("EncryptConnectionString", new XCData(EncryptDecryptUtil.Encrypt(connectionString)))
                );
            doc.Add(root);

            string name = Environment.MachineName;
            string uuid = tokenSalt.ToString();
            string mac = GetMacAddress();
            string ip = GetLocalIPAddress();

            string sqlExpression = $"INSERT INTO [db_scales].[HOSTS](IdRRef, NAME, MAC, IP,SettingsFile) VALUES ( '{uuid}','{name}', '{mac}', '{ip}','{doc.ToString()}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int n = command.ExecuteNonQuery();
            }

            // записать токен на диск если в БД отметилось без ошибки
            doc.Save(FilePathToken);
            return tokenSalt;
        }


        public static bool TokenExist()
        {
            if (!File.Exists(FilePathToken))
            {
                return false;
            }

            XDocument doc = XDocument.Load(FilePathToken);
            Guid IdRRef = Guid.Parse(doc.Root.Elements("ID").First().Value);
            var EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
            int countRow = 0;

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                string query = "SELECT COUNT(*) as CNT FROM [db_scales].[Hosts] WHERE [IdRRef] = @ID ";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", IdRRef);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        countRow = SqlConnectFactory.GetValue<int>(reader, "CNT");
                    }

                    reader.Close();
                    con.Close();

                }
            }

            return countRow == 1;
        }

        #region net utils

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string GetMacAddress()
        {
            string macAddresses = string.Empty;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        #endregion


    }


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
}
