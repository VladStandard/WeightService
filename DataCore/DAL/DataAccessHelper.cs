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

        public string ConnectionString { get; private set; }
        private readonly object _locker = new();
        public JsonSettingsEntity? JsonSettings { get; private set; }

        private NHibernate.ISessionFactory? _sessionFactory = null;
        private NHibernate.ISessionFactory? GetSessionFactory()
        {
            lock (_locker)
            {
                if (_sessionFactory != null)
                    return _sessionFactory;
                if (JsonSettings == null || JsonSettings.CheckProperties(true) == false)
                    return null;
                if (JsonSettings.Sql.Trusted == false && (string.IsNullOrEmpty(JsonSettings.Sql.Username) || string.IsNullOrEmpty(JsonSettings.Sql.Password)))
                    throw new ArgumentException("CoreSettings.Username or CoreSettings.Password is null!");
                FluentNHibernate.Cfg.Db.MsSqlConfiguration config = FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                    .ConnectionString(GetConnectionString());
                config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                FluentNHibernate.Cfg.FluentConfiguration configuration = FluentNHibernate.Cfg.Fluently.Configure().Database(config);
                AddConfigurationMappings(configuration, JsonSettings);
                //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                configuration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                // Be careful. If there are errors in the mapping, this line will make an Exception!
                _sessionFactory = configuration.BuildSessionFactory();
                return _sessionFactory;
            }
        }
        public NHibernate.ISession? OpenSession() => GetSessionFactory()?.OpenSession();

        public CrudController? Crud { get; private set; }

        public bool IsDisabled
        {
            get
            {
                NHibernate.ISession? session = OpenSession();
                return session == null || !session.IsConnected;
            }
        }

        #endregion

        #region Constructor and destructor

        public DataAccessHelper()
        {
            ConnectionString = string.Empty;
            Crud = null;
        }

        #endregion

        #region Public and private methods

        public bool SetupForBlazorApp(string contentRootPath)
        {
            if (Setup(contentRootPath))
                return true;
            string subDir =
#if DEBUG
                @"bin\x64\Debug\net6.0\";
#else
                @"bin\x64\Release\net6.0\";
#endif
            string dir = Path.Combine(contentRootPath, subDir);
            if (Setup(dir))
                return true;
            return false;
        }

        public bool Setup(string dir)
        {
            JsonSettings = null;
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
                object? jsonObject = (JsonSettingsEntity?)serializer.Deserialize(streamReader, typeof(JsonSettingsEntity));
                if (jsonObject is JsonSettingsEntity jsonSettings)
                {
                    Microsoft.Data.SqlClient.SqlConnectionStringBuilder sqlConnectionStringBuilder = new();
                    sqlConnectionStringBuilder["Data Source"] = jsonSettings.Sql.Server;
                    sqlConnectionStringBuilder["Initial Catalog"] = jsonSettings.Sql.Db;
                    sqlConnectionStringBuilder["Persist Security Info"] = jsonSettings.Sql.Trusted;
                    sqlConnectionStringBuilder["User ID"] = jsonSettings.Sql.Username;
                    sqlConnectionStringBuilder["Password"] = jsonSettings.Sql.Password;
                    sqlConnectionStringBuilder["TrustServerCertificate"] = jsonSettings.Sql.TrustServerCertificate;
                    //sqlConnectionStringBuilder["Schema"] = jsonSettings.Schema;
                    ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    JsonSettings = jsonSettings;
                    Crud = new(GetSessionFactory());
                }
            }
            return JsonSettings != null;
        }

        public bool DownloadAppSettings(string dirLocal)
        {
            string dirRemote = @"\\palych\Install\VSSoft\appsettings\";
            if (!Directory.Exists(dirRemote))
                return false;

            if (!DownloadFile(dirLocal, dirRemote, "appsettings.json"))
                return false;
            if (!DownloadFile(dirLocal, dirRemote, "appsettings.Debug.json"))
                return false;
            if (!DownloadFile(dirLocal, dirRemote, "appsettings.Release.json"))
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

        private string GetConnectionString() => JsonSettings == null ? string.Empty : JsonSettings.Sql.Trusted
            ? $"Data Source={JsonSettings.Sql.Server};Initial Catalog={JsonSettings.Sql.Db};Persist Security Info=True;" +
              $"Trusted Connection=True;TrustServerCertificate={JsonSettings.Sql.TrustServerCertificate};"
            : $"Data Source={JsonSettings.Sql.Server};Initial Catalog={JsonSettings.Sql.Db};Persist Security Info=True;" +
              $"User ID={JsonSettings.Sql.Username};Password={JsonSettings.Sql.Password};TrustServerCertificate={JsonSettings.Sql.TrustServerCertificate};";

        private void AddConfigurationMappings(FluentNHibernate.Cfg.FluentConfiguration configuration, JsonSettingsEntity? jsonSettings)
        {
            if (configuration == null || jsonSettings == null || string.IsNullOrEmpty(jsonSettings.Sql.Db))
                return;

            switch (jsonSettings.Sql.Db.ToUpper())
            {
                case "SCALESDB":
                case "SCALES":
                    AddConfigurationMappingsForScale(configuration);
                    break;
                case "VSDWH":
                    AddConfigurationMappingsForDwh(configuration);
                    break;
            }
        }

        private void AddConfigurationMappingsForScale(FluentNHibernate.Cfg.FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.AccessMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.AppMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarCodeMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarCodeTypeMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ContragentMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ErrorMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.HostMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LabelMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LogMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LogTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrganizationMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PluMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductionFacilityMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductSeriesMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ScaleMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WeithingFactMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WorkshopMap>());
        }

        private void AddConfigurationMappingsForDwh(FluentNHibernate.Cfg.FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.BrandMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.InformationSystemMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureGroupMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureLightMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.StatusMap>());
        }

        #endregion
    }
}
