// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataProjectsCore.DAL.Utils
{
    public static class HostsUtils
    {
        #region Public and private fields and properties

        private static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";

        #endregion

        #region Public and private methods

        public static HostDirect Load(Guid idrref)
        {
            HostDirect result = new();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Hosts.GetHostByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@idrref", idrref);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result.IdRRef = idrref;
                            result.Name = SqlConnectFactory.GetValue<string>(reader, "NAME");
                            result.IP = SqlConnectFactory.GetValue<string>(reader, "IP");
                            result.MAC = SqlConnectFactory.GetValue<string>(reader, "MAC");
                            result.Marked = SqlConnectFactory.GetValue<bool>(reader, "MARKED");
                            result.SettingsFile = XDocument.Parse(SqlConnectFactory.GetValue<string>(reader, "SETTINGSFILE"));
                            result.Id = SqlConnectFactory.GetValue<int>(reader, "ID");
                            result.CurrentScaleId = SqlConnectFactory.GetValue<int>(reader, "SCALE_ID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static HostDirect TokenRead()
        {
            if (!File.Exists(FilePathToken))
            {
                return new HostDirect();
            }
            XDocument doc = XDocument.Load(FilePathToken);
            Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
            //string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            //string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
            return Load(idrref);
        }

        public static Guid TokenWrite(string connectionString)
        {
            Guid tokenSalt = Guid.NewGuid();
            XDocument doc = new();
            XElement root = new("root");
            root.Add(
                new XElement("ID", tokenSalt),
                new XElement("EncryptConnectionString", new XCData(EncryptDecryptUtils.Encrypt(connectionString)))
                );
            doc.Add(root);

            string name = Environment.MachineName;
            string uuid = tokenSalt.ToString();
            string mac = NetUtils.GetMacAddress();
            string ip = NetUtils.GetLocalIpAddress();

            string sqlExpression = $"INSERT INTO [db_scales].[HOSTS](IdRRef, NAME, MAC, IP,SettingsFile) VALUES ( '{uuid}','{name}', '{mac}', '{ip}','{doc.ToString()}')";

            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new(sqlExpression, con))
                {
                    int n = cmd.ExecuteNonQuery();
                }
                con.Close();
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
            string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            string connectionString = EncryptDecryptUtils.Decrypt(EncryptConnectionString);
            int countRow = 0;

            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                string query = "SELECT COUNT(*) as CNT FROM [db_scales].[Hosts] WHERE [IdRRef] = @ID ";
                using (SqlCommand cmd = new(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@ID", IdRRef);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            countRow = SqlConnectFactory.GetValue<int>(reader, "CNT");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return countRow == 1;
        }

        #endregion
    }
}