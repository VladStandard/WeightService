// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

using DataCore.Files;
using DataCore.Sql.Controllers;

namespace DataCore.Sql.Core;

public class DataAccessHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static DataAccessHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static DataAccessHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private readonly object _locker = new();
    public bool JsonSettingsIsRemote { get; private set; }

    private JsonSettingsModel? _jsonSettingsLocal;
    public JsonSettingsModel JsonSettingsLocal
    {
        get
        {
            if (_jsonSettingsLocal == null)
                throw new ArgumentException($"{nameof(DataAccessHelper)}.{nameof(JsonSettingsLocal)} is null!");
            _jsonSettingsLocal.CheckProperties(true);
            return _jsonSettingsLocal;
        }
        set => _jsonSettingsLocal = value;
    }

    private JsonSettingsModel? _jsonSettingsRemote;
    public JsonSettingsModel JsonSettingsRemote
    {
        get
        {
            if (_jsonSettingsRemote == null)
                throw new ArgumentException($"{nameof(DataAccessHelper)}.{nameof(JsonSettingsRemote)} is null!");
            _jsonSettingsRemote.CheckProperties(true);
            return _jsonSettingsRemote;
        }
        set => _jsonSettingsRemote = value;
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
            AddConfigurationMappings(_fluentConfiguration, JsonSettingsIsRemote ? JsonSettingsRemote : JsonSettingsLocal);
            //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
            //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
            _fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
            return _fluentConfiguration;
        }
    }

    private NHibernate.ISessionFactory? _sessionFactory;
    public NHibernate.ISessionFactory SessionFactory
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

    public void InitSessionFactory()
    {
        lock (_locker)
        {
            _fluentConfiguration = null;
            _sqlConfiguration = null;
            _sessionFactory = null;
            _crud = null;
        }
    }

    public void InitSessionFactoryWithJson(bool isRemote)
    {
        JsonSettingsIsRemote = isRemote;
        InitSessionFactory();
        _ = SessionFactory;
        _ = SqlConfiguration;
        _ = FluentConfiguration;
    }

    public NHibernate.ISession OpenSession() => SessionFactory.OpenSession();

    public void CloseSessionFactory()
    {
        using NHibernate.ISessionFactory session = SessionFactory;
        session.Close();
        session.Dispose();
    }

    private CrudController? _crud;
    public CrudController Crud
    {
        get
        {
            if (_crud != null)
                return _crud;
            return _crud = new();
        }
        set => _crud = value;
    }

    private JsonSettingsController? _jsonControl;
    public JsonSettingsController JsonControl
    {
        get
        {
            if (_jsonControl != null)
                return _jsonControl;
            return _jsonControl = new();
        }
        set => _jsonControl = value;
    }

    private LogController? _log;
    public LogController Log
    {
        get
        {
            if (_log != null)
                return _log;
            return _log = new();
        }
        set => _log = value;
    }

    public bool IsConnected
    {
        get
        {
            NHibernate.ISession session = OpenSession();
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
    }

    #endregion

    #region Constructor and destructor

    public DataAccessHelper()
    {
        InitSessionFactory();
    }

    #endregion

    #region Public and private methods

    // This code have exception: 
    // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
    // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
    //private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
    //    ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
    //        .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
    //    : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
    //        .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

    private string GetConnectionString() =>
        JsonSettingsIsRemote
        ? $"Data Source={JsonSettingsRemote.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettingsRemote.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettingsRemote.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettingsRemote.Sql.PersistSecurityInfo}; " +
          (JsonSettingsRemote.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettingsRemote.Sql.UserId}; Password={JsonSettingsRemote.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettingsRemote.Sql.TrustServerCertificate}; "
        : $"Data Source={JsonSettingsLocal.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettingsLocal.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettingsLocal.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettingsLocal.Sql.PersistSecurityInfo}; " +
          (JsonSettingsLocal.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettingsLocal.Sql.UserId}; Password={JsonSettingsLocal.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettingsLocal.Sql.TrustServerCertificate}; ";

    private void AddConfigurationMappings(FluentNHibernate.Cfg.FluentConfiguration fluentConfiguration, JsonSettingsModel jsonSettings)
    {
        switch (jsonSettings.Sql.InitialCatalog.ToUpper())
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
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AccessMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AppMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BarCodeTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<HostMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<NomenclatureMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluLabelMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluObsoleteMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductSeriesMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<VersionMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WorkShopMap>());
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
