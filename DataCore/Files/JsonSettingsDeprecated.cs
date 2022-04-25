//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using Microsoft.Extensions.Configuration;
//using System;

//namespace DataCore.Models
//{
//    /// <summary>
//    /// Data model for appsettings.json.
//    /// </summary>
//    public class JsonSettingsEntity
//    {
//        #region Public and private fields and properties

//        public IConfiguration? Configuration { get; }

//        public string Server
//        {
//            get => Configuration == null ? string.Empty : Configuration[$"Sql:{nameof(Server)}"];
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"Sql:{nameof(Server)}"] = value;
//            }
//        }
//        public string Db
//        {
//            get => Configuration == null ? string.Empty : Configuration[$"Sql:{nameof(Db)}"];
//            set
//            {
//                if (Configuration != null) 
//                    Configuration[$"Sql:{nameof(Db)}"] = value;
//            }
//        }
//        public bool Trusted
//        {
//            get => Configuration != null && Convert.ToBoolean(Configuration[$"Sql:{nameof(Trusted)}"]);
//            set
//            {
//                if (Configuration != null) 
//                    Configuration[$"Sql:{nameof(Trusted)}"] = value.ToString();
//            }
//        }
//        public string Username
//        {
//            get => Configuration == null ? string.Empty : Configuration[$"Sql:{nameof(Username)}"];
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"Sql:{nameof(Username)}"] = value;
//            }
//        }
//        public string Password
//        {
//            get => Configuration == null ? string.Empty : Configuration[$"Sql:{nameof(Password)}"];
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"Sql:{nameof(Password)}"] = value;
//            }
//        }
//        public string Schema
//        {
//            get => Configuration == null ? string.Empty : Configuration[$"Sql:{nameof(Schema)}"];
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"Sql:{nameof(Schema)}"] = value;
//            }
//        }
//        public bool TrustServerCertificate
//        {
//            get => Configuration != null && Convert.ToBoolean(Configuration[$"Sql:{nameof(TrustServerCertificate)}"]);
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"Sql:{nameof(TrustServerCertificate)}"] = value.ToString();
//            }
//        }
//        public bool IsDebug
//        {
//            get => Configuration != null && Convert.ToBoolean(Configuration[$"{nameof(IsDebug)}"]);
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"{nameof(IsDebug)}"] = value.ToString();
//            }
//        }
//        public int SectionRowsCount
//        {
//            get => Configuration == null ? 0 : Convert.ToInt32(Configuration[$"{nameof(SectionRowsCount)}"]);
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"{nameof(SectionRowsCount)}"] = value.ToString();
//            }
//        }
//        public int ItemRowsCount
//        {
//            get => Configuration == null ? 0 : Convert.ToInt32(Configuration[$"{nameof(ItemRowsCount)}"]);
//            set
//            {
//                if (Configuration != null)
//                    Configuration[$"{nameof(ItemRowsCount)}"] = value.ToString();
//            }
//        }

//        #endregion

//        #region Constructor and destructor

//        public JsonSettingsEntity() : this(null, false) { }

//        public JsonSettingsEntity(IConfiguration? configuration, bool isGenerateException)
//        {
//            Configuration = configuration;
//            CheckProperties(isGenerateException);
//        }

//        #endregion

//        #region Public and private methods

//        public override string ToString()
//        {
//            string strTrusted = Trusted ? $"{nameof(Trusted)}: true." : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}.";
//            return $"{nameof(Server)}: {Server}. " +
//                   $"{nameof(Db)}: {Db}. " +
//                   $"{strTrusted} " +
//                   $"{nameof(TrustServerCertificate)}: {TrustServerCertificate}. " +
//                   $"{nameof(IsDebug)}: {IsDebug}. " +
//                   $"{nameof(SectionRowsCount)}: {SectionRowsCount}. " +
//                   $"{nameof(ItemRowsCount)}: {ItemRowsCount}. ";
//        }

//        public bool CheckProperties(bool isGenerateException)
//        {
//            if (string.IsNullOrEmpty(Server))
//            {
//                if (isGenerateException)
//                    throw new ArgumentNullException(Server, $"{nameof(Server)} must be fill!");
//                return false;
//            }
//            if (string.IsNullOrEmpty(Db))
//            {
//                if (isGenerateException)
//                    throw new ArgumentNullException(Db, $"{nameof(Db)} must be fill!");
//                return false;
//            }
//            if (!Trusted)
//            {
//                if (string.IsNullOrEmpty(Username))
//                {
//                    if (isGenerateException)
//                        throw new ArgumentNullException(Username, $"{nameof(Username)} must be fill!");
//                    return false;
//                }
//                if (string.IsNullOrEmpty(Password))
//                {
//                    if (isGenerateException)
//                        throw new ArgumentNullException(Password, $"{nameof(Password)} must be fill!");
//                    return false;
//                }
//            }
//            return true;
//        }

//        #endregion
//    }
//}
