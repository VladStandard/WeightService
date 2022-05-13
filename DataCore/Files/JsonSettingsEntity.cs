// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace DataCore.Files
{
    [Serializable]
    public class JsonSettingsEntity : ISerializable
    {
        #region Public and private fields and properties

        public JsonSettingsSqlEntity Sql { get; set; }
        public int SectionRowsCount { get; set; }
        public int ItemRowsCount { get; set; }
        public int SelectTopRowsCount { get; set; }
        public ushort Version { get; set; }
        public string AllowedHosts { get; set; }
        public string ConnectionString { get; set; }

        #endregion

        #region Constructor and destructor

        public JsonSettingsEntity(JsonSettingsSqlEntity jsonSettingsSql, bool isCheckProperties)
        {
            Sql = jsonSettingsSql;
            SectionRowsCount = 0;
            ItemRowsCount = 0;
            SelectTopRowsCount = 0;
            AllowedHosts = string.Empty;
            Version = 0;
            ConnectionString = string.Empty;
            CheckProperties(isCheckProperties);
        }

        public JsonSettingsEntity() : this(new(), false) { }

        public JsonSettingsEntity(SerializationInfo info, StreamingContext context)
        {
            Sql = (JsonSettingsSqlEntity)info.GetValue(nameof(Sql), typeof(JsonSettingsSqlEntity));
            SectionRowsCount = info.GetInt32(nameof(SectionRowsCount));
            ItemRowsCount = info.GetInt32(nameof(ItemRowsCount));
            SelectTopRowsCount = info.GetInt32(nameof(SelectTopRowsCount));
            Version = info.GetUInt16(nameof(Version));
            AllowedHosts = info.GetString(nameof(AllowedHosts));
            ConnectionString = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return Sql.ToString() +
                $"{nameof(SectionRowsCount)}: {SectionRowsCount}. " +
                $"{nameof(ItemRowsCount)}: {ItemRowsCount}. " +
                $"{nameof(SelectTopRowsCount)}: {SelectTopRowsCount}. " +
                $"{nameof(Version)}: {Version}. " +
                $"{nameof(AllowedHosts)}: {AllowedHosts}. ";
        }

        public bool CheckProperties(bool isGenerateException)
        {
            if (string.IsNullOrEmpty(Sql.DataSource))
            {
                if (isGenerateException)
                    throw new ArgumentNullException(Sql.DataSource, $"{nameof(JsonSettingsEntity)}.{nameof(Sql.DataSource)} IsNullOrEmpty!");
                return false;
            }

            if (string.IsNullOrEmpty(Sql.InitialCatalog))
            {
                if (isGenerateException)
                    throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(JsonSettingsEntity)}.{nameof(Sql.InitialCatalog)} IsNullOrEmpty!");
                return false;
            }

            if (!Sql.PersistSecurityInfo)
            {
                if (string.IsNullOrEmpty(Sql.UserId))
                {
                    if (isGenerateException)
                        throw new ArgumentNullException(Sql.UserId, $"{nameof(JsonSettingsEntity)}.{nameof(Sql.UserId)} IsNullOrEmpty!");
                    return false;
                }
                if (string.IsNullOrEmpty(Sql.Password))
                {
                    if (isGenerateException)
                        throw new ArgumentNullException(Sql.Password, $"{nameof(JsonSettingsEntity)}.{nameof(Sql.Password)} IsNullOrEmpty!");
                    return false;
                }
            }

            if (Version == 0)
            {
                if (isGenerateException)
                    throw new ArgumentNullException(Sql.InitialCatalog, $"{nameof(JsonSettingsEntity)}.{nameof(Version)} == 0!");
                return false;
            }

            return true;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sql), Sql);
            info.AddValue(nameof(SectionRowsCount), SectionRowsCount);
            info.AddValue(nameof(ItemRowsCount), ItemRowsCount);
            info.AddValue(nameof(SelectTopRowsCount), SelectTopRowsCount);
            info.AddValue(nameof(Version), Version);
            info.AddValue(nameof(AllowedHosts), AllowedHosts);
            //info.AddValue(nameof(ConnectionString), ConnectionString);
        }

        #endregion
    }
}
