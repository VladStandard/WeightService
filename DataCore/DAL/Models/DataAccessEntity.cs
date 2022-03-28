// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableDwhModels;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

namespace DataCore.DAL.Models
{
    public class DataAccessEntity : IDisposable
    {
        #region Public and private fields and properties

        private readonly object _locker = new();
        public JsonSettingsEntity JsonSettings { get; private set; }

        // https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
        private ISessionFactory? _sessionFactory = null;
        private ISessionFactory? SessionFactory
        {
            get
            {
                lock (_locker)
                {
                    if (_sessionFactory != null)
                        return _sessionFactory;
                    if (!JsonSettings.CheckProperties(false))
                        return null;
                    if (!JsonSettings.Trusted && (string.IsNullOrEmpty(JsonSettings.Username) || string.IsNullOrEmpty(JsonSettings.Password)))
                        throw new ArgumentException("CoreSettings.Username or CoreSettings.Password is null!");
                    MsSqlConfiguration config = MsSqlConfiguration.MsSql2012.ConnectionString(GetConnectionString());
                    config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                    FluentConfiguration configuration = Fluently.Configure().Database(config);
                    AddConfigurationMappings(configuration, JsonSettings);
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                    configuration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    // Be careful. If there are errors in the mapping, this line will make an Exception!
                    _sessionFactory = configuration.BuildSessionFactory();
                    return _sessionFactory;
                }
            }
        }

        public CrudController Crud { get; private set; }

        public bool IsDisabled
        {
            get
            {
                ISession? session = GetSession();
                return session == null || !session.IsConnected;
            }
        }

        #endregion

        #region Constructor and destructor

        public DataAccessEntity() : this(new()) { }

        public DataAccessEntity(JsonSettingsEntity jsonSettings)
        {
            JsonSettings = jsonSettings;
            Crud = new(this, SessionFactory);
        }

        // This code have exception: 
        // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
        // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        //private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
        //    ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
        //    : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

        private string GetConnectionString() => JsonSettings == null ? string.Empty : JsonSettings.Trusted
            ? $"Data Source={JsonSettings.Server};Initial Catalog={JsonSettings.Db};Persist Security Info=True;" +
              $"Trusted Connection=True;TrustServerCertificate={JsonSettings.TrustServerCertificate};"
            : $"Data Source={JsonSettings.Server};Initial Catalog={JsonSettings.Db};Persist Security Info=True;" +
              $"User ID={JsonSettings.Username};Password={JsonSettings.Password};TrustServerCertificate={JsonSettings.TrustServerCertificate};";

        private void AddConfigurationMappings(FluentConfiguration configuration, JsonSettingsEntity jsonSettings)
        {
            if (configuration == null || jsonSettings == null || string.IsNullOrEmpty(jsonSettings.Db))
                return;

            switch (jsonSettings.Db.ToUpper())
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

        private void AddConfigurationMappingsForScale(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<AccessMap>());
            configuration.Mappings(m => m.FluentMappings.Add<AppMap>());
            configuration.Mappings(m => m.FluentMappings.Add<BarCodeMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<BarCodeTypeMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<ContragentMapV2>());
            configuration.Mappings(m => m.FluentMappings.Add<ErrorMap>());
            configuration.Mappings(m => m.FluentMappings.Add<HostMap>());
            configuration.Mappings(m => m.FluentMappings.Add<LabelMap>());
            configuration.Mappings(m => m.FluentMappings.Add<LogMap>());
            configuration.Mappings(m => m.FluentMappings.Add<LogTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<OrderMap>());
            configuration.Mappings(m => m.FluentMappings.Add<OrderTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<OrganizationMap>());
            configuration.Mappings(m => m.FluentMappings.Add<PluMap>());
            configuration.Mappings(m => m.FluentMappings.Add<PrinterMap>());
            configuration.Mappings(m => m.FluentMappings.Add<PrinterResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<PrinterTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>());
            configuration.Mappings(m => m.FluentMappings.Add<ProductSeriesMap>());
            configuration.Mappings(m => m.FluentMappings.Add<ScaleMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TaskMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TaskTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TemplateMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TemplateResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<WeithingFactMap>());
            configuration.Mappings(m => m.FluentMappings.Add<WorkshopMap>());
        }

        private void AddConfigurationMappingsForDwh(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<BrandMap>());
            configuration.Mappings(m => m.FluentMappings.Add<InformationSystemMap>());
            configuration.Mappings(m => m.FluentMappings.Add<NomenclatureGroupMap>());
            configuration.Mappings(m => m.FluentMappings.Add<NomenclatureLightMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<NomenclatureTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<StatusMap>());
        }

        #endregion

        #region Public and private methods - Share

        public ISession? GetSession() => SessionFactory?.OpenSession();

        public void Dispose()
        {
            _sessionFactory?.Dispose();
        }

        #endregion
    }
}
