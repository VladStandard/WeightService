// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.Models
{

    public class JsonSettingsBase
    {
        #region Public and private fields and properties

        public JsonSettingsSqlEntity Sql { get; set; }
        public int SectionRowCount { get; set; }
        public int ItemRowCount { get; set; }

        #endregion

        #region Constructor and destructor

        public JsonSettingsBase() : this(new(), false)
        {
            //
        }

        public JsonSettingsBase(JsonSettingsSqlEntity jsonSettingsSql, bool isCheckProperties)
        {
            Sql = jsonSettingsSql;
            SectionRowCount = 0;
            ItemRowCount = 0;
            CheckProperties(isCheckProperties);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strTrusted = Sql.Trusted ? $"{nameof(Sql.Trusted)}: true." : $"{nameof(Sql.Username)}: {Sql.Username}. {nameof(Sql.Password)}: {Sql.Password}.";
            return $"{nameof(Sql.Server)}: {Sql.Server}. " +
                   $"{nameof(Sql.Db)}: {Sql.Db}. " +
                   $"{strTrusted} " +
                   $"{nameof(Sql.TrustServerCertificate)}: {Sql.TrustServerCertificate}. " +
                   $"{nameof(SectionRowCount)}: {SectionRowCount}. " +
                   $"{nameof(ItemRowCount)}: {ItemRowCount}. ";
        }

        public bool CheckProperties(bool isGenerateException)
        {
            if (string.IsNullOrEmpty(Sql.Server))
            {
                if (isGenerateException)
                    throw new ArgumentNullException(Sql.Server, $"{nameof(Sql.Server)} must be fill!");
                return false;
            }
            if (string.IsNullOrEmpty(Sql.Db))
            {
                if (isGenerateException)
                    throw new ArgumentNullException(Sql.Db, $"{nameof(Sql.Db)} must be fill!");
                return false;
            }
            if (!Sql.Trusted)
            {
                if (string.IsNullOrEmpty(Sql.Username))
                {
                    if (isGenerateException)
                        throw new ArgumentNullException(Sql.Username, $"{nameof(Sql.Username)} must be fill!");
                    return false;
                }
                if (string.IsNullOrEmpty(Sql.Password))
                {
                    if (isGenerateException)
                        throw new ArgumentNullException(Sql.Password, $"{nameof(Sql.Password)} must be fill!");
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
