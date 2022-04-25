// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration

using DataCore.DAL.Models;
using DataCore.Files;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace DataCore.DAL
{
    public class DataAccessHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static DataAccessHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static DataAccessHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public string DirLanAppSettings => @"\\palych\Install\VSSoft\appsettings\";
        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_connectionString))
                    return _connectionString;
                return string.Empty;
            }
        }
        private readonly object _locker = new();
        
        private JsonSettingsEntity? _jsonSettings;
        public JsonSettingsEntity JsonSettings
        {
            get {
                if (_jsonSettings == null)
                    throw new ArgumentException($"{nameof(DataAccessHelper)}.{nameof(JsonSettings)} is null!");
                _jsonSettings.CheckProperties(true);
                return _jsonSettings;
            }
        }

        private FluentNHibernate.Cfg.Db.MsSqlConfiguration? _sqlConfiguration;
        /// <summary>
        /// Get MsSqlConfiguration.
        /// Be careful. If setup _sqlConfiguration.DefaultSchema, this line will make an Exception!
        /// </summary>
        private FluentNHibernate.Cfg.Db.MsSqlConfiguration SqlConfiguration
        {
            get
            {
                if (_sqlConfiguration != null)
                    return _sqlConfiguration;
                _sqlConfiguration = FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012.ConnectionString(GetConnectionString());
                _sqlConfiguration.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                //_sqlConfiguration.DefaultSchema(JsonSettings.Sql.Schema);
                return _sqlConfiguration;
            }
        }

        private FluentNHibernate.Cfg.FluentConfiguration? _fluentConfiguration;
        /// <summary>
        /// Get FluentConfiguration.
        /// Be careful. If there are errors in the mapping, this line will make an Exception!
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public FluentNHibernate.Cfg.FluentConfiguration FluentConfiguration
        {
            get
            {
                if (_fluentConfiguration != null)
                    return _fluentConfiguration;
                _fluentConfiguration = FluentNHibernate.Cfg.Fluently.Configure().Database(SqlConfiguration);
                AddConfigurationMappings(_fluentConfiguration, JsonSettings);
                //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                _fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                return _fluentConfiguration;
            }
        }

        private NHibernate.ISessionFactory? _sessionFactory = null;
        private NHibernate.ISessionFactory SessionFactory
        {
            get
            {
                lock (_locker)
                {
                    if (_sessionFactory != null)
                        return _sessionFactory;
                    _sessionFactory = FluentConfiguration.BuildSessionFactory();
                    return _sessionFactory;
                }
            }
        }

        public NHibernate.ISession? OpenSession() => SessionFactory.OpenSession();

        public bool CloseSessionFactory()
        {
            using NHibernate.ISessionFactory? session = SessionFactory;
            if (session != null)
            {
                session.Close();
                session.Dispose();
                return true;
            }
            return false;
        }

        private CrudController? _crud;
        public CrudController Crud
        {
            get {
                if (_crud != null)
                    return _crud;
                return new CrudController(SessionFactory);
            }
        }

        public bool IsConnected
        {
            get
            {
                NHibernate.ISession? session = OpenSession();
                if (session != null)
                {
                    try
                    {
                        return session.IsConnected;
                    }
                    finally
                    {
                        session.Disconnect();
                        session.Close();
                        session.Dispose();
                    }
                }
                return false;
            }
        }

        #endregion

        #region Constructor and destructor

        public DataAccessHelper()
        {
            _connectionString = string.Empty;
            _fluentConfiguration = null;
            _crud = null;
        }

        #endregion

        #region Public and private methods

        public bool SetupForBlazorApp(string rootDir)
        {
            if (Setup(rootDir))
                return true;
            string subDir =
#if DEBUG
                @"bin\x64\Debug\net6.0\";
#else
                @"bin\x64\Release\net6.0\";
#endif
            string dir = Path.Combine(rootDir, subDir);
            if (Setup(dir))
                return true;
            else
                if (DownloadAppSettings(dir) && Setup(dir))
                    return true;
            return false;
        }

        public bool SetupForTests(string rootDir)
        {
            if (Setup(rootDir))
                return true;
            else
                if (DownloadAppSettings(rootDir) && Setup(rootDir))
                    return true;
            return false;
        }

        public bool Setup(string dir)
        {
            object? jsonObject = null;
            string file =
#if DEBUG
                Path.Combine(dir, "appsettings.Debug.json");
#else
                Path.Combine(dir, "appsettings.Release.json");
#endif
            if (File.Exists(file))
            {
                using StreamReader streamReader = File.OpenText(file);
                JsonSerializer serializer = new();
                jsonObject = (JsonSettingsEntity?)serializer.Deserialize(streamReader, typeof(JsonSettingsEntity));
                if (jsonObject is JsonSettingsEntity jsonSettings)
                {
                    Microsoft.Data.SqlClient.SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                    sqlConnectionStringBuilder["Data Source"] = jsonSettings.Sql.Server;
                    sqlConnectionStringBuilder["Initial Catalog"] = jsonSettings.Sql.Db;
                    sqlConnectionStringBuilder["Persist Security Info"] = jsonSettings.Sql.Trusted;
                    sqlConnectionStringBuilder["User ID"] = jsonSettings.Sql.Username;
                    sqlConnectionStringBuilder["Password"] = jsonSettings.Sql.Password;
                    sqlConnectionStringBuilder["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate;
                    _connectionString = sqlConnectionStringBuilder.ConnectionString;
                    _jsonSettings = jsonSettings;
                    _crud = new(SessionFactory);
                }
            }
            return jsonObject != null;
        }

        public bool DownloadAppSettings(string dirLocal)
        {
            if (!Directory.Exists(DirLanAppSettings))
                return false;

            if (!DownloadFile(dirLocal, DirLanAppSettings, "appsettings.json"))
                return false;
            if (!DownloadFile(dirLocal, DirLanAppSettings, "appsettings.Debug.json"))
                return false;
            if (!DownloadFile(dirLocal, DirLanAppSettings, "appsettings.Release.json"))
                return false;

            return true;
        }

        private static bool DownloadFile(string dirLocal, string dirRemote, string file)
        {
            string filePath = Path.Combine(dirRemote, file);
            if (!File.Exists(filePath))
                return false;

            StreamReader streamReader = File.OpenText(filePath);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
            if (string.IsNullOrEmpty(content))
                return false;

            StreamWriter streamWriter = File.CreateText(Path.Combine(dirLocal, file));
            streamWriter.Write(content);
            streamWriter.Close();
            
            return true;
        }

        // This code have exception: 
        // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
        // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        //private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
        //    ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
        //    : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

        private string GetConnectionString() => JsonSettings.Sql.Trusted
            ? $"Data Source={JsonSettings.Sql.Server};Initial Catalog={JsonSettings.Sql.Db};Persist Security Info=True;" +
              $"Trusted Connection=True;TrustServerCertificate={JsonSettings.Sql.TrustServerCertificate};"
            : $"Data Source={JsonSettings.Sql.Server};Initial Catalog={JsonSettings.Sql.Db};Persist Security Info=True;" +
              $"User ID={JsonSettings.Sql.Username};Password={JsonSettings.Sql.Password};" +
              $"TrustServerCertificate={JsonSettings.Sql.TrustServerCertificate};";

        private void AddConfigurationMappings(FluentNHibernate.Cfg.FluentConfiguration fluentConfiguration, JsonSettingsEntity jsonSettings)
        {
            switch (jsonSettings.Sql.Db.ToUpper())
            {
                case "SCALESDB":
                case "SCALES":
                    AddConfigurationMappingsForScale(fluentConfiguration);
                    break;
                case "VSDWH":
                    AddConfigurationMappingsForDwh(fluentConfiguration);
                    break;
            }
        }

        private void AddConfigurationMappingsForScale(FluentNHibernate.Cfg.FluentConfiguration fluentConfiguration)
        {
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.AccessMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.AppMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarCodeMapV2>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarCodeTypeMapV2>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ContragentMapV2>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ErrorMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.HostMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LabelMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LogMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LogTypeMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.NomenclatureMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderTypeMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrganizationMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PluMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterResourceMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterTypeMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductionFacilityMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductSeriesMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ScaleMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskTypeMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateResourceMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WeithingFactMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WorkshopMap>());
        }

        private void AddConfigurationMappingsForDwh(FluentNHibernate.Cfg.FluentConfiguration fluentConfiguration)
        {
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.BrandMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.InformationSystemMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureGroupMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureLightMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureTypeMap>());
            fluentConfiguration.Mappings(m => m.FluentMappings.Add<TableDwhModels.StatusMap>());
        }

        #endregion
    }
}
