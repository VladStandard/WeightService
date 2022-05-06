// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers
{
    public static class HostsUtils
    {
        #region Public and private fields and properties

        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;
        public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
        public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";
        public static DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;

        #endregion

        #region Public and private methods

        public static HostDirect LoadReader(SqlDataReader reader)
        {
            HostDirect result = new();
            if (reader.Read())
            {
                result.Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
                result.Name = SqlConnect.GetValueAsNullable<string>(reader, "NAME");
                result.HostName = SqlConnect.GetValueAsNullable<string>(reader, "HOSTNAME");
                result.Ip = SqlConnect.GetValueAsNullable<string>(reader, "IP");
                result.Mac = SqlConnect.GetValueAsNullable<string>(reader, "MAC");
                result.IdRRef = SqlConnect.GetValueAsNotNullable<Guid>(reader, "IDRREF");
                result.IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "MARKED");
                string? settingFile = SqlConnect.GetValueAsNullable<string>(reader, "SETTINGSFILE");
                if (settingFile is string sf)
                    result.SettingsFile = XDocument.Parse(sf);
                result.ScaleId = SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID");
            }
            return result;
        }

        public static HostEntity GetHostEntity(string hostName)
        {
            HostEntity host = DataAccess.Crud.GetEntity<HostEntity>(
                new FieldListEntity(new Dictionary<DbField, object?> {
                    { DbField.HostName, hostName },
                    { DbField.IsMarked, false } }),
                new FieldOrderEntity(DbField.CreateDt, DbOrderDirection.Desc));
            return host;
        }

        public static ScaleEntity GetScaleEntity(long hostId)
        {
            ScaleEntity scale = DataAccess.Crud.GetEntity<ScaleEntity>(
                new FieldListEntity(new Dictionary<string, object?> {
                    { "Host.IdentityId", hostId },
                    { DbField.IsMarked.ToString(), false } }),
                new FieldOrderEntity(DbField.CreateDt, DbOrderDirection.Desc));
            return scale;
        }

        public static HostDirect Load(Guid uid)
        {
            HostDirect result = SqlConnect.ExecuteReaderForEntity(SqlQueries.DbScales.Tables.Hosts.GetHostByUid,
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = uid }, LoadReader);
            if (result == null)
                result = new HostDirect();
            return result;
        }

        public static HostDirect Load(string hostName)
        {
            HostDirect result = SqlConnect.ExecuteReaderForEntity(SqlQueries.DbScales.Tables.Hosts.GetHostByHostName,
                new SqlParameter("@HOST_NAME", System.Data.SqlDbType.NVarChar, 255) { Value = hostName }, LoadReader);
            if (result == null)
                result = new HostDirect();
            return result;
        }

        public static HostDirect GetHostDirect()
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

        public static HostDirect GetHostDirect(string hostName) => Load(hostName);

        public static bool CheckHostUidInFile()
        {
            if (!File.Exists(FilePathToken))
                return false;

            XDocument doc = XDocument.Load(FilePathToken);
            Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
            bool result = default;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostIdByIdRRef,
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idrref }, (reader) =>
            {
                result = reader.Read();
            });
            return result;
        }

        #endregion
    }
}
