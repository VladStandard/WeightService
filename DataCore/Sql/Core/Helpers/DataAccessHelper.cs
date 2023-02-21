// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

using DataCore.Files;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.LogsWebsFks;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using FluentNHibernate.Cfg;
using NHibernate;

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static DataAccessHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static DataAccessHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private readonly object _locker = new();

    public JsonSettingsHelper JsonSettings { get; } = JsonSettingsHelper.Instance;

    public FluentNHibernate.Cfg.Db.MsSqlConfiguration? SqlConfiguration { get; private set; }

	// Be careful. If setup SqlConfiguration.DefaultSchema, this line will make an Exception!
	private void SetSqlConfiguration(bool isShowSql)
    {
        string connectionString = GetConnectionString();
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        SqlConfiguration = FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012.ConnectionString(connectionString);
        if (isShowSql)
            SqlConfiguration.ShowSql();
        SqlConfiguration.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
    }

    private FluentConfiguration? _fluentConfiguration;
    private FluentConfiguration FluentConfiguration
    {
	    get
	    {
			if (_fluentConfiguration is null)
				throw new ArgumentNullException(nameof(FluentConfiguration));
            return _fluentConfiguration;
		}
	    set => _fluentConfiguration = value;
    }
    private ISessionFactory? _sessionFactory;
    public ISessionFactory SessionFactory
    {
	    get
	    {
		    if (_sessionFactory is null)
			    throw new ArgumentNullException(nameof(SessionFactory));
		    return _sessionFactory;
	    }
	    private set => _sessionFactory = value;
    }

	// Be careful. If there are errors in the mapping, this line will make an Exception!
	private void SetFluentConfiguration()
    {
        if (SqlConfiguration is null)
            throw new ArgumentNullException(nameof(SqlConfiguration));

        FluentConfiguration = Fluently.Configure().Database(SqlConfiguration);
        AddConfigurationMappings(FluentConfiguration);
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
        //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
        FluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
    }

    public void SetSessionFactory(bool isShowSql, ISessionFactory? sessionFactory = null)
    {
	    lock (_locker)
	    {
            SetSqlConfiguration(isShowSql);
            SetFluentConfiguration();
            SessionFactory = sessionFactory ?? FluentConfiguration.BuildSessionFactory();
        }
    }

    ~DataAccessHelper()
    {
	    SessionFactory.Close();
	    SessionFactory.Dispose();
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
		JsonSettings.IsRemote
        ? $"Data Source={JsonSettings.Remote.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettings.Remote.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettings.Remote.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettings.Remote.Sql.PersistSecurityInfo}; " +
          (JsonSettings.Remote.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettings.Remote.Sql.UserId}; Password={JsonSettings.Remote.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettings.Remote.Sql.TrustServerCertificate}; "
        : $"Data Source={JsonSettings.Local.Sql.DataSource}; " +
          $"Initial Catalog={JsonSettings.Local.Sql.InitialCatalog}; " +
          $"Persist Security Info={JsonSettings.Local.Sql.PersistSecurityInfo}; " +
          $"Integrated Security={JsonSettings.Local.Sql.PersistSecurityInfo}; " +
          (JsonSettings.Local.Sql.IntegratedSecurity ? "" : $"User ID={JsonSettings.Local.Sql.UserId}; Password={JsonSettings.Local.Sql.Password}; ") +
          $"TrustServerCertificate={JsonSettings.Local.Sql.TrustServerCertificate}; ";

    public void AddConfigurationMappings(FluentConfiguration fluentConfiguration)
    {
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AccessMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceScaleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AppMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BoxMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BrandMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BundleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ClipMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<NomenclatureMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBrandFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBundleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluClipFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluLabelMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluNestingFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluTemplateFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ProductSeriesMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ScaleScreenShotMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TaskTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<TemplateResourceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<VersionMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<WorkShopMap>());
    }

    #endregion
}