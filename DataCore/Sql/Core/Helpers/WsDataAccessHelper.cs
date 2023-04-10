// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
// https://docs.microsoft.com/ru-ru/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring

using System.Security.Claims;
using System.Threading.Tasks;
using DataCore.Files;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Settings.Helpers;
using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Models;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.TableDiagModels.Logs;
using DataCore.Sql.TableDiagModels.LogsMemories;
using DataCore.Sql.TableDiagModels.LogsTypes;
using DataCore.Sql.TableDiagModels.LogsWebs;
using DataCore.Sql.TableDiagModels.LogsWebsFks;
using DataCore.Sql.TableDiagModels.ScalesScreenshots;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusLabels;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleFkModels.PlusWeighingsFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
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
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions;
using NHibernate;

namespace DataCore.Sql.Core.Helpers;

public class WsDataAccessHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsDataAccessHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsDataAccessHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public JsonSettingsHelper JsonSettings => JsonSettingsHelper.Instance;
    private ISessionFactory? _sessionFactory;
    public ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory is null)
                throw new ArgumentNullException(nameof(SessionFactory));
            return _sessionFactory;
        }
        set => _sessionFactory = value;
    }
    protected AppVersionHelper AppVersion => AppVersionHelper.Instance;
    public AppModel App { get; set; } = new();
    public DeviceModel Device { get; set; } = new();
    private readonly object _locker = new();

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

    ~WsDataAccessHelper()
    {
        SessionFactory.Close();
        SessionFactory.Dispose();
    }

    #endregion

    #region Public and private methods - Base

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

    public static void AddConfigurationMappings(FluentConfiguration fluentConfiguration)
    {
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AccessMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<AppMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BarCodeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BoxMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BrandMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<BundleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ClipMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<ContragentMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceScaleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<DeviceTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogMemoryMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogTypeMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<LogWebMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrderWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<OrganizationMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBrandFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluBundleFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluCharacteristicsFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluClipFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluGroupMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluLabelMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluNestingFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluScaleMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluStorageMethodFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluStorageMethodMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluTemplateFkMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PluWeighingMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterMap>());
        fluentConfiguration.Mappings(m => m.FluentMappings.Add<PrinterResourceFkMap>());
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

    public void SetupLog(string deviceName, string appName)
    {
        if (Device.IsNew)
        {
            if (string.IsNullOrEmpty(deviceName))
                deviceName = NetUtils.GetLocalDeviceName(false);
            Device = GetItemDeviceOrCreateNew(deviceName);
        }

        if (App.IsNew)
        {
            if (string.IsNullOrEmpty(appName))
                appName = nameof(DataCore);
            App = GetItemAppOrCreateNew(appName);
        }
    }

    public void SetupLog(string appName) => SetupLog("", appName);

    #endregion

    #region Public and private methods - Base

    protected ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig) where T : class, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if ((JsonSettings.Local.MaxCount > 0 && sqlCrudConfig.IsResultShowOnlyTop) || JsonSettings.Local.MaxCount == 1)
            criteria.SetMaxResults(JsonSettings.Local.MaxCount);
        if (sqlCrudConfig.Filters.Any())
            criteria.SetCriteriaFilters(sqlCrudConfig.Filters);
        if (sqlCrudConfig.Orders.Any())
        {
            List<SqlFieldOrderModel> orders = sqlCrudConfig.Orders.Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            if (orders.Any())
                criteria.SetCriteriaOrder(orders);
        }
        return criteria;
    }

    protected SqlCrudResultModel ExecuteCore(Action<ISession> action, bool isTransaction)
    {
        ISession? session = null;
        Exception? exception = null;
        ITransaction? transaction = null;

        try
        {
            session = SessionFactory.OpenSession();
            if (isTransaction)
                transaction = session.BeginTransaction();
            session.FlushMode = isTransaction ? FlushMode.Commit : FlushMode.Manual;
            action.Invoke(session);
            if (isTransaction)
                session.Flush();
            if (isTransaction)
                transaction?.Commit();
            session.Clear();
        }
        catch (Exception ex)
        {
            if (isTransaction)
                transaction?.Rollback();
            exception = ex;
        }
        finally
        {
            if (isTransaction)
                transaction?.Dispose();
            if (session is not null)
            {
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }

        if (exception is not null)
        {
            SaveLogError(exception);
            return new() { IsOk = false, Exception = exception };
        }
        return new() { IsOk = true, Exception = null };
    }

    public bool IsConnected()
    {
        bool result = false;
        ExecuteCore(session =>
        {
            result = session.IsConnected;
        }, false);
        return result;
    }

    protected ISQLQuery? GetSqlQuery(ISession session, string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return null;

        ISQLQuery sqlQuery = session.CreateSQLQuery(query);
        foreach (SqlParameter parameter in parameters)
        {
            if (parameter.Value is byte[] imagedata)
                sqlQuery.SetParameter(parameter.ParameterName, imagedata);
            else
                sqlQuery.SetParameter(parameter.ParameterName, parameter.Value);
        }
        return sqlQuery;
    }

    public SqlCrudResultModel ExecQueryNative(string query, List<SqlParameter> parameters)
    {
        if (string.IsNullOrEmpty(query)) return new() { IsOk = false, Exception = null };
        return ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                _ = sqlQuery.ExecuteUpdate();
            }
        }, true);
    }

    public SqlCrudResultModel ExecQueryNative(string query, SqlParameter parameter) =>
        ExecQueryNative(query, new List<SqlParameter> { parameter });

    public SqlCrudResultModel Save<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Save(item), true);
    }

    protected async Task<SqlCrudResultModel> SaveAsync<T>(T? item) where T : WsSqlTableBase
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        return Save(item);
    }

    public SqlCrudResultModel Save<T>(T? item, SqlFieldIdentityModel? identity) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.CreateDt = DateTime.Now;
        item.ChangeDt = DateTime.Now;
        object? id = identity?.GetValueAsObjectNullable();
        if (Equals(identity?.Name, WsSqlFieldIdentity.Uid) && Equals(id, Guid.Empty))
            id = Guid.NewGuid();
        return id is null
            ? ExecuteCore(session => session.Save(item), true)
            : ExecuteCore(session => session.Save(item, id), true);
    }

    protected SqlCrudResultModel SaveOrUpdate<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    public SqlCrudResultModel UpdateForce<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.ClearNullProperties();
        item.ChangeDt = DateTime.Now;
        return ExecuteCore(session => session.Update(item), true);
    }

    public SqlCrudResultModel Delete<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        return ExecuteCore(session => session.Delete(item), true);
    }

    public SqlCrudResultModel Mark<T>(T? item) where T : WsSqlTableBase
    {
        if (item is null) return new() { IsOk = false, Exception = null };

        item.IsMarked = !item.IsMarked;
        return ExecuteCore(session => session.SaveOrUpdate(item), true);
    }

    #endregion

    #region Public and public methods - Item

    public AccessModel? GetItemAccessNullable(string? userName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), userName, false, false);
        return GetItemNullable<AccessModel>(sqlCrudConfig);
    }

    public ProductSeriesModel? GetItemProductSeriesNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new List<SqlFieldFilterModel>
            {
                new() { Name = nameof(ProductSeriesModel.IsClose), Value = false },
                new() { Name = $"{nameof(ProductSeriesModel.Scale)}.{nameof(ScaleModel.IdentityValueId)}", Value = scale.IdentityValueId }
            }, false, false);
        return GetItemNullable<ProductSeriesModel>(sqlCrudConfig);
    }

    public ProductSeriesModel GetItemProductSeriesNotNullable(ScaleModel scale) =>
        GetItemProductSeriesNullable(scale) ?? GetItemNewEmpty<ProductSeriesModel>();

    private PluModel? GetItemPluNullable(PluScaleModel pluScale)
    {
        if (!pluScale.IsNotNew || !pluScale.Plu.IsNotNew) return null;

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueUid), pluScale.Plu.IdentityValueUid, false, false);
        return GetItemNullable<PluModel>(sqlCrudConfig);
    }

    public PluModel GetItemPluNotNullable(PluScaleModel pluScale) =>
        GetItemPluNullable(pluScale) ?? new();

    public PluTemplateFkModel? GetItemPluTemplateFkNullable(PluModel plu)
    {
        if (plu.IsNew) return null;
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluTemplateFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid,
            false, false);
        return GetItemNullable<PluTemplateFkModel>(sqlCrudConfig);
    }

    public PluTemplateFkModel GetItemPluTemplateFkNotNullable(PluModel plu) =>
        GetItemPluTemplateFkNullable(plu) ?? new();

    public PluBundleFkModel? GetItemPluBundleFkNullable(PluModel plu, BundleModel bundle)
    {
        if (plu.IsNew) return null;
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, false, false);
        SqlCrudConfigModel sqlCrudConfigBundle = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(PluBundleFkModel.Bundle)}.{nameof(WsSqlTableBase.IdentityValueUid)}", bundle.IdentityValueUid, false, false);
        sqlCrudConfig.Filters.Add(sqlCrudConfigBundle.Filters.First());
        return GetItemNullable<PluBundleFkModel>(sqlCrudConfig);
    }

    public PluBundleFkModel GetItemPluBundleFkNotNullable(PluModel plu, BundleModel bundle) =>
        GetItemPluBundleFkNullable(plu, bundle) ?? new();

    private TemplateModel? GetItemTemplateNullable(PluScaleModel pluScale)
    {
        if (pluScale.IsNew || pluScale.Plu.IsNew) return null;
        PluModel plu = GetItemPluNotNullable(pluScale);
        return GetItemPluTemplateFkNullable(plu)?.Template;
    }

    public TemplateModel GetItemTemplateNotNullable(PluScaleModel pluScale) =>
        GetItemTemplateNullable(pluScale) ?? new();

    private ScaleModel? GetItemScaleNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFiltersIdentity(
            $"{nameof(DeviceScaleFkModel.Device)}", device.IdentityValueUid), false, false);
        return GetItemNotNullable<DeviceScaleFkModel>(sqlCrudConfig).Scale;
    }

    public ScaleModel GetItemScaleNotNullable(DeviceModel device) =>
        GetItemScaleNullable(device) ?? new();

    private DeviceModel? GetItemDeviceNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, false, false);
        return GetItemNullable<DeviceModel>(sqlCrudConfig);
    }

    public DeviceModel? GetItemDeviceNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig)?.Device;
    }

    public DeviceModel GetItemDeviceNotNullable(string name) => GetItemDeviceNullable(name) ?? new();

    public DeviceModel GetItemDeviceNotNullable(ScaleModel scale) => GetItemDeviceNullable(scale) ?? new();

    public DeviceTypeModel? GetItemDeviceTypeNullable(string typeName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFilters(nameof(DeviceTypeModel.Name), typeName), false, false);
        return GetItemNullable<DeviceTypeModel>(sqlCrudConfig);
    }

    public DeviceTypeModel GetItemDeviceTypeNotNullable(string typeName) =>
        GetItemDeviceTypeNullable(typeName) ?? new();

    public DeviceTypeFkModel? GetItemDeviceTypeFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceTypeFkModel.Device), device.IdentityValueUid), false, false);
        return GetItemNullable<DeviceTypeFkModel>(sqlCrudConfig);
    }

    public DeviceTypeFkModel GetItemDeviceTypeFkNotNullable(DeviceModel device) =>
        GetItemDeviceTypeFkNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(DeviceModel device)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Device), device.IdentityValueUid), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(DeviceModel device) =>
        GetItemDeviceScaleFkNullable(device) ?? new();

    public DeviceScaleFkModel? GetItemDeviceScaleFkNullable(ScaleModel scale)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(DeviceScaleFkModel.Scale), scale.IdentityValueId), false, false);
        return GetItemNullable<DeviceScaleFkModel>(sqlCrudConfig);
    }

    public DeviceScaleFkModel GetItemDeviceScaleFkNotNullable(ScaleModel scale) =>
        GetItemDeviceScaleFkNullable(scale) ?? new();

    public string GetAccessRightsDescription(AccessRightsEnum? accessRights)
    {
        return accessRights switch
        {
            AccessRightsEnum.Read => LocaleCore.Strings.AccessRightsRead,
            AccessRightsEnum.Write => LocaleCore.Strings.AccessRightsWrite,
            AccessRightsEnum.Admin => LocaleCore.Strings.AccessRightsAdmin,
            _ => LocaleCore.Strings.AccessRightsNone
        };
    }

    public string GetAccessRightsDescription(byte accessRights) =>
        GetAccessRightsDescription((AccessRightsEnum)accessRights);

    public string GetAccessRightsDescription(ClaimsPrincipal? user)
    {
        if (user == null)
            return string.Empty;
        string right = user.Claims.Where(c => c.Type == ClaimTypes.Role).
            Select(c => c.Value).OrderByDescending(int.Parse).First();
        return GetAccessRightsDescription((AccessRightsEnum)int.Parse(right));
    }

    public ScaleModel GetScaleNotNullable(long id)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.IdentityValueId), id, false, false, false, false);
        return GetItemNotNullable<ScaleModel>(sqlCrudConfig);
    }

    public ProductionFacilityModel GetProductionFacilityNotNullable(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(ProductionFacilityModel.Name), name, false, false);
        return GetItemNotNullable<ProductionFacilityModel>(sqlCrudConfig);
    }

    public PluGroupModel? GetItemNomenclatureGroupParentNullable(PluGroupModel nomenclatureGroup)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFilters(
            $"{nameof(PluGroupFkModel.PluGroup)}.{nameof(WsSqlTableBase.IdentityValueUid)}", nomenclatureGroup.IdentityValueUid),
            false, false);
        PluGroupModel? result = GetItemNullable<PluGroupFkModel>(sqlCrudConfig)?.Parent;
        return result;
    }

    public PluGroupModel GetItemNomenclatureGroupParentNotNullable(PluGroupModel nomenclatureGroup) =>
        GetItemNomenclatureGroupParentNullable(nomenclatureGroup) ?? new();

    #endregion

    #region Public and private methods - List

    [Obsolete(@"Use DataContext")]
    public List<DeviceModel> GetListDevices(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<DeviceModel>());
        List<DeviceModel> list = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeModel> GetListDevicesTypes(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<DeviceTypeModel>());
        List<DeviceTypeModel> list = GetListNotNullable<DeviceTypeModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFks(SqlCrudConfigModel sqlCrudConfig)
    {
        List<DeviceTypeFkModel> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(new() { Device = GetItemNewEmpty<DeviceModel>(), Type = GetItemNewEmpty<DeviceTypeModel>() });
        List<DeviceTypeFkModel> list = GetListNotNullable<DeviceTypeFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Type.Name).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceScaleFkModel> GetListDevicesScalesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        List<DeviceScaleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(new() { Device = GetItemNewEmpty<DeviceModel>(), Scale = GetItemNewEmpty<ScaleModel>() });
        List<DeviceScaleFkModel> list = GetListNotNullable<DeviceScaleFkModel>(sqlCrudConfig);
        result = result.OrderBy(x => x.Scale.Description).ToList();
        result = result.OrderBy(x => x.Device.Name).ToList();
        result.AddRange(list);
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeModel> GetListDevicesTypes(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeModel> deviceTypes = GetListDevicesTypes(sqlCrudConfig);
        return deviceTypes;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceModel> GetListDevices(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceModel> devices = GetListDevices(sqlCrudConfig);
        return devices;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFks(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypesFks = GetListDevicesTypesFks(sqlCrudConfig);
        return deviceTypesFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFkFree(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => !devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<DeviceTypeFkModel> GetListDevicesTypesFkBusy(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<DeviceTypeFkModel> deviceTypeFks = GetListDevicesTypesFks(sqlCrudConfig);
        List<DeviceModel> devices = GetListNotNullable<DeviceModel>(sqlCrudConfig);
        deviceTypeFks = deviceTypeFks.Where(x => devices.Contains(x.Device)).ToList();
        return deviceTypeFks;
    }

    [Obsolete(@"Use DataContext")]
    public List<PluLabelModel> GetListPluLabels(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(isShowMarked, isShowOnlyTop);
        sqlCrudConfig.Orders.Add(new() { Name = nameof(PluWeighingModel.ChangeDt), Direction = WsSqlOrderDirection.Desc });
        return GetListNotNullable<PluLabelModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use DataContext")]
    public List<ScaleScreenShotModel> GetListScalesScreenShots(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            SqlCrudConfigModel.GetFiltersIdentity(nameof(ScaleScreenShotModel.Scale), itemFilter?.IdentityValueId),
            isShowMarked, isShowOnlyTop, isAddFieldNull);
        List<ScaleScreenShotModel> result = GetListNotNullable<ScaleScreenShotModel>(sqlCrudConfig);
        result = result.OrderByDescending(x => x.CreateDt).ToList();
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<PluBundleFkModel> GetListPluBundles(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull)
    {
        List<PluBundleFkModel> result = new();
        if (isAddFieldNull)
            result.Add(GetItemNewEmpty<PluBundleFkModel>());
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluBundleFkModel.Plu), itemFilter?.IdentityValueUid);

        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(PluBundleFkModel.Plu), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        result.AddRange(GetListNotNullable<PluBundleFkModel>(sqlCrudConfig));
        result = result.OrderBy(x => x.Bundle.Name).ToList();
        result = result.OrderBy(x => x.Plu.Number).ToList();
        return result;
    }

    [Obsolete(@"Use DataContext")]
    public List<PrinterResourceFkModel> GetListPrinterResources(WsSqlTableBase? itemFilter, bool isShowMarked, bool isShowOnlyTop)
    {
        List<SqlFieldFilterModel> filters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PrinterResourceFkModel.Printer), itemFilter?.IdentityValueId);
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(filters,
            new SqlFieldOrderModel { Name = nameof(WsSqlTableBase.Description), Direction = WsSqlOrderDirection.Asc },
            isShowMarked, isShowOnlyTop);
        return GetListNotNullable<PrinterResourceFkModel>(sqlCrudConfig);
    }

    [Obsolete(@"Use DataContext")]
    public List<PrinterTypeModel> GetListPrinterTypes(bool isShowMarked, bool isShowOnlyTop)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new SqlFieldOrderModel { Name = nameof(PrinterTypeModel.Name), Direction = WsSqlOrderDirection.Asc }, isShowMarked, isShowOnlyTop);
        return GetListNotNullable<PrinterTypeModel>(sqlCrudConfig);
    }

    #endregion

    #region Public and private methods - Logs

    private void SaveLogCore(string message, LogType logType, string filePath, int lineNumber, string memberName)
    {
        StrUtils.SetStringValueTrim(ref filePath, 32, true);
        StrUtils.SetStringValueTrim(ref memberName, 32);
        StrUtils.SetStringValueTrim(ref message, 1024);
        LogTypeModel? logTypeItem = GetItemLogTypeNullable(logType);

        LogModel log = new()
        {
            CreateDt = DateTime.Now,
            ChangeDt = DateTime.Now,
            IsMarked = false,
            Device = Device,
            App = App,
            LogType = logTypeItem,
            Version = AppVersion.Version,
            File = filePath,
            Line = lineNumber,
            Member = memberName,
            Message = message
        };
        SaveAsync(log).ConfigureAwait(false);
    }

    public void SaveLogErrorWithInfo(Exception ex, string filePath, int lineNumber, string memberName)
    {
        SaveLogCore(ex.Message, LogType.Error, filePath, lineNumber, memberName);
        if (ex.InnerException is not null)
            SaveLogCore(ex.InnerException.Message, LogType.Error, filePath, lineNumber, memberName);
    }

    public void SaveLogError(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

    public void SaveLogError(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Error, filePath, lineNumber, memberName);

    public void SaveLogStop(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Stop, filePath, lineNumber, memberName);

    public void SaveLogInformation(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Information, filePath, lineNumber, memberName);

    public void SaveLogWarning(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Warning, filePath, lineNumber, memberName);

    public void SaveLogQuestion(string message,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        SaveLogCore(message, LogType.Question, filePath, lineNumber, memberName);

    #endregion

    #region Public and private methods - App & Device

    protected AppModel GetItemAppOrCreateNew(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, false, false);
        AppModel app = GetItemNotNullable<AppModel>(sqlCrudConfig);
        if (app.IsNew)
        {
            app = new()
            {
                Name = appName,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now
            };
        }
        else
        {
            app.ChangeDt = DateTime.Now;
        }
        SaveOrUpdate(app);
        return app;
    }

    public AppModel? GetItemAppNullable(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, false, false);
        return GetItemNullable<AppModel>(sqlCrudConfig);
    }

    protected DeviceModel GetItemDeviceOrCreateNew(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), name, true, false);
        DeviceModel device = GetItemNotNullable<DeviceModel>(sqlCrudConfig);
        if (device.IsNew)
        {
            device = new()
            {
                Name = name,
                PrettyName = name,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                LoginDt = DateTime.Now,
                LogoutDt = DateTime.Now,
                Ipv4 = NetUtils.GetLocalIpAddress()
            };
        }
        else
        {
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;

        }
        SaveOrUpdate(device);
        return device;
    }

    public LogTypeModel? GetItemLogTypeNullable(LogType logType)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>
                { new() { Name = nameof(LogTypeModel.Number), Value = (byte)logType } },
            true, true, false, false, false);
        return GetItemNullable<LogTypeModel>(sqlCrudConfig);
    }

    public LogTypeModel GetItemLogTypeNotNullable(LogType logType) =>
        GetItemLogTypeNullable(logType) ?? new();

    public List<LogTypeModel> GetListLogTypesNotNullable()
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(),
            false, false, false, true, false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(LogTypeModel.Number), Direction = WsSqlOrderDirection.Asc });
        return GetListNotNullable<LogTypeModel>(sqlCrudConfig);
    }

    #endregion

    #region Public and private methods - GetItem

    public T? GetItemNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = null;
        ExecuteCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            item = criteria.UniqueResult<T>();
        }, false);
        FillReferences(item, sqlCrudConfig.IsFillReferences);
        return item;
    }

    public T? GetItemNullable<T>(object? value) where T : WsSqlTableBase, new()
    {
        SqlCrudConfigModel? sqlCrudConfig = value switch
        {
            Guid uid => new(new List<SqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueUid), Value = uid } },
                true, false, false, false, false),
            long id => new(new List<SqlFieldFilterModel> { new() { Name = nameof(WsSqlTableBase.IdentityValueId), Value = id } },
                true, false, false, false, false),
            _ => null
        };
        return sqlCrudConfig is not null ? GetItemNullable<T>(sqlCrudConfig) : null;
    }

    public T GetItemNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T? item = GetItemNullable<T>(sqlCrudConfig);
        return item ?? new();
    }

    public T GetItemNotNullable<T>(object? value) where T : WsSqlTableBase, new()
    {
        T? item = value switch
        {
            Guid uid => GetItemNullable<T>(uid),
            long id => GetItemNullable<T>(id),
            _ => new()
        };
        return item ?? new();
    }

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        identity.Name switch
        {
            WsSqlFieldIdentity.Uid => GetItemNullable<T>(identity.Uid),
            WsSqlFieldIdentity.Id => GetItemNullable<T>(identity.Id),
            _ => new()
        };

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(identity) ?? new();

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        GetItemNullableByUid<T>(uid) ?? new();

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullable<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        GetItemNullableById<T>(id) ?? new();

    public bool IsItemExists<T>(T? item) where T : WsSqlTableBase, new()
    {
        if (item is null)
            return false;

        bool result = false;
        ExecuteCore(session =>
        {
            result = session.Query<T>().Any(x => x.IsAny(item));
        }, false);
        return result;
    }

    public bool IsItemExists<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        bool result = false;
        ExecuteCore(session =>
        {
            int saveCount = JsonSettings.Local.SelectTopRowsCount;
            JsonSettings.Local.SelectTopRowsCount = 1;
            result = GetCriteria<T>(session, sqlCrudConfig).List<T>().Any();
            JsonSettings.Local.SelectTopRowsCount = saveCount;
        }, false);
        return result;
    }

    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new() =>
        new() { Name = LocaleCore.Table.FieldEmpty, Description = LocaleCore.Table.FieldEmpty };

    #endregion

    #region Public and private methods - GetArray

    public T[]? GetArrayNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        T[]? items = null;
        ExecuteCore(session =>
        {
            ICriteria criteria = GetCriteria<T>(session, sqlCrudConfig);
            items = criteria.List<T>().ToArray();
            foreach (T item in items)
            {
                FillReferences(item, sqlCrudConfig.IsFillReferences);
            }
        }, false);
        return items;
    }

    public T[]? GetNativeArrayNullable<T>(string query, List<SqlParameter> parameters) where T : WsSqlTableBase, new()
    {
        T[]? result = null;
        ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                result = sqlQuery.List<T>().ToArray();
            }
        }, false);
        return result;
    }

    public T? GetNativeItemNullable<T>(string query, List<SqlParameter> parameters)
    {
        T? result = default;
        ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                //sqlQuery.AddEntity(typeof(T));
                IList<T>? list = sqlQuery.List<T>();
                result = list.First();
            }
        }, false);
        return result;
    }

    private object[]? GetNativeArrayObjectsNullable(string query, List<SqlParameter> parameters)
    {
        object[]? result = null;
        ExecuteCore(session =>
        {
            ISQLQuery? sqlQuery = GetSqlQuery(session, query, parameters);
            if (sqlQuery is not null)
            {
                System.Collections.IList? listEntities = sqlQuery.List();
                result = new object[listEntities.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    if (listEntities[i] is object[] records)
                        result[i] = records;
                    else
                        result[i] = listEntities[i];
                }
            }
        }, false);
        return result;
    }

    public object[] GetArrayObjectsNotNullable(string query) =>
        GetArrayObjectsNotNullable(query, new());

    public object[] GetArrayObjectsNotNullable(string query, List<SqlParameter> parameters) =>
        GetNativeArrayObjectsNullable(query, parameters) ?? Array.Empty<object>();

    public object[] GetArrayObjectsNotNullable(SqlCrudConfigModel sqlCrudConfig) =>
        GetNativeArrayObjectsNullable(sqlCrudConfig.NativeQuery, sqlCrudConfig.NativeParameters) ?? Array.Empty<object>();

    #endregion

    #region Public and private methods - GetList

    public List<T> GetListNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new()
    {
        List<T> result = new();
        if (sqlCrudConfig.IsResultAddFieldEmpty)
            result.Add(GetItemNewEmpty<T>());

        List<T> list = new();
        T[]? items = GetArrayNullable<T>(sqlCrudConfig);
        if (items is not null && items.Length > 0)
            list = items.ToList();

        result.AddRange(list);
        return result;
    }

    #endregion

    #region Public and private methods - Fill references

    /// <summary>
    /// Fill references for table.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    /// <param name="isFillReferences"></param>
    private void FillReferences<T>(T? item, bool isFillReferences) where T : WsSqlTableBase, new()
    {
        switch (item)
        {
            case XmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemNotNullable<ScaleModel>(xmlDevice.Scale.IdentityValueId);
                break;
            case LogModel log:
                log.App = GetItemNotNullable<AppModel>(log.App?.IdentityValueUid);
                log.Device = GetItemNullable<DeviceModel>(log.Device?.IdentityValueUid);
                log.LogType = GetItemNotNullable<LogTypeModel>(log.LogType?.IdentityValueUid);
                break;
            // Scales.
            case BarCodeModel barcode:
                barcode.PluLabel = GetItemNotNullable<PluLabelModel>(barcode.PluLabel.IdentityValueUid);
                break;
            case DeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = GetItemNotNullable<DeviceModel>(deviceTypeFk.Device.IdentityValueUid);
                deviceTypeFk.Type = GetItemNotNullable<DeviceTypeModel>(deviceTypeFk.Type.IdentityValueUid);
                break;
            case DeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = GetItemNotNullable<DeviceModel>(deviceScaleFk.Device.IdentityValueUid);
                deviceScaleFk.Scale = GetItemNotNullable<ScaleModel>(deviceScaleFk.Scale.IdentityValueId);
                break;
            case LogMemoryModel logMemory:
                logMemory.App = GetItemNotNullable<AppModel>(logMemory.App.IdentityValueUid);
                logMemory.Device = GetItemNotNullable<DeviceModel>(logMemory.Device.IdentityValueUid);
                break;
            case LogWebFkModel logWebFk:
                logWebFk.LogWebRequest = GetItemNotNullable<LogWebModel>(logWebFk.LogWebRequest.IdentityValueUid);
                logWebFk.LogWebResponse = GetItemNotNullable<LogWebModel>(logWebFk.LogWebResponse.IdentityValueUid);
                logWebFk.App = GetItemNotNullable<AppModel>(logWebFk.App.IdentityValueUid);
                logWebFk.LogType = GetItemNotNullable<LogTypeModel>(logWebFk.LogType.IdentityValueUid);
                logWebFk.Device = GetItemNotNullable<DeviceModel>(logWebFk.Device.IdentityValueUid);
                break;
            case PluFkModel pluFk:
                pluFk.Plu = GetItemNotNullable<PluModel>(pluFk.Plu.IdentityValueUid);
                pluFk.Parent = GetItemNotNullable<PluModel>(pluFk.Parent.IdentityValueUid);
                pluFk.Category = GetItemNotNullable<PluModel>(pluFk.Category?.IdentityValueUid);
                break;
            case PluBrandFkModel pluBrandFk:
                pluBrandFk.Plu = GetItemNotNullable<PluModel>(pluBrandFk.Plu.IdentityValueUid);
                pluBrandFk.Brand = GetItemNotNullable<BrandModel>(pluBrandFk.Brand.IdentityValueUid);
                break;
            case PluBundleFkModel pluBundle:
                pluBundle.Plu = GetItemNotNullable<PluModel>(pluBundle.Plu.IdentityValueUid);
                pluBundle.Bundle = GetItemNotNullable<BundleModel>(pluBundle.Bundle.IdentityValueUid);
                break;
            case PluClipFkModel pluClip:
                pluClip.Clip = GetItemNotNullable<ClipModel>(pluClip.Clip.IdentityValueUid);
                pluClip.Plu = GetItemNotNullable<PluModel>(pluClip.Plu.IdentityValueUid);
                break;
            case PluLabelModel pluLabel:
                pluLabel.PluWeighing = GetItemNullable<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNullable<PluScaleModel>(pluLabel.PluScale.IdentityValueUid);
                break;
            case PluScaleModel pluScale:
                pluScale.Plu = GetItemNotNullable<PluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Scale = GetItemNotNullable<ScaleModel>(pluScale.Scale.IdentityValueId);
                break;
            case PluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = GetItemNotNullable<PluModel>(pluTemplateFk.Plu.IdentityValueUid);
                pluTemplateFk.Template = GetItemNotNullable<TemplateModel>(pluTemplateFk.Template.IdentityValueId);
                break;
            case PluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemNotNullable<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
                pluWeighing.Series = GetItemNullable<ProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
                break;
            case PluNestingFkModel pluNestingFk:
                pluNestingFk.PluBundle = GetItemNotNullable<PluBundleFkModel>(pluNestingFk.PluBundle.IdentityValueUid);
                pluNestingFk.Box = GetItemNotNullable<BoxModel>(pluNestingFk.Box.IdentityValueUid);
                break;
            case PluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = GetItemNotNullable<PluGroupModel>(pluGroupFk.PluGroup.IdentityValueUid);
                pluGroupFk.Parent = GetItemNotNullable<PluGroupModel>(pluGroupFk.Parent.IdentityValueUid);
                break;
            case PluCharacteristicsFkModel pluCharacteristicsFk:
                pluCharacteristicsFk.Plu = GetItemNotNullable<PluModel>(pluCharacteristicsFk.Plu.IdentityValueUid);
                pluCharacteristicsFk.Characteristic = GetItemNotNullable<PluCharacteristicModel>(pluCharacteristicsFk.Characteristic.IdentityValueUid);
                break;
            case PluStorageMethodFkModel pluStorageMethod:
                if (isFillReferences)
                {
                    pluStorageMethod.Plu = GetItemNotNullable<PluModel>(pluStorageMethod.Plu.IdentityValueUid);
                    pluStorageMethod.Method = GetItemNotNullable<PluStorageMethodModel>(pluStorageMethod.Method.IdentityValueUid);
                    pluStorageMethod.Resource = GetItemNotNullable<TemplateResourceModel>(pluStorageMethod.Resource.IdentityValueUid);
                }
                else
                {
                    //pluStorageMethod.FillProperties();
                    pluStorageMethod.Plu = new();
                    pluStorageMethod.Method = new();
                    pluStorageMethod.Resource = new();
                }
                break;
            case OrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNullable<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNullable<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
                break;
            case PrinterModel printer:
                printer.PrinterType = GetItemNotNullable<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
                break;
            case PrinterResourceFkModel printerResource:
                printerResource.Printer = GetItemNotNullable<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNullable<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueUid);
                if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
                    printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
                break;
            case ProductSeriesModel product:
                product.Scale = GetItemNotNullable<ScaleModel>(product.Scale.IdentityValueId);
                break;
            case ScaleModel scale:
                scale.PrinterMain = GetItemNullable<PrinterModel>(scale.PrinterMain?.IdentityValueId);
                scale.PrinterShipping = GetItemNullable<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
                scale.WorkShop = GetItemNullable<WorkShopModel>(scale.WorkShop?.IdentityValueId);
                break;
            case ScaleScreenShotModel scaleScreenShot:
                scaleScreenShot.Scale = GetItemNotNullable<ScaleModel>(scaleScreenShot.Scale.IdentityValueId);
                break;
            case TaskModel task:
                task.TaskType = GetItemNotNullable<TaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = GetItemNotNullable<ScaleModel>(task.Scale.IdentityValueId);
                break;
            case WorkShopModel workshop:
                workshop.ProductionFacility = GetItemNotNullable<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
                break;
        }
    }

    #endregion

    #region Public and private methods - LogMemory

    /// <summary>
    /// Save log memory info.
    /// </summary>
    /// <param name="sizeAppMb"></param>
    /// <param name="sizeFreeMb"></param>
    public void SaveLogMemory(short sizeAppMb, short sizeFreeMb)
    {
        LogMemoryModel logMemory = new()
        {
            CreateDt = DateTime.Now,
            SizeAppMb = sizeAppMb,
            SizeFreeMb = sizeFreeMb,
            App = App,
            Device = Device,
        };
        SaveAsync(logMemory).ConfigureAwait(false);
    }

    /// <summary>
    /// Get list of simple log memory info.
    /// </summary>
    /// <returns></returns>
    public List<WsSqlSimpleLogMemory> GetListSimpleLogsMemories(int topRecords)
    {
        List<WsSqlSimpleLogMemory> result = new();
        string query = WsSqlQueriesDiags.Views.GetLogsMemories(topRecords);
        object[] objects = GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] { Length: 6 } item)
                break;
            result.Add(new()
            {
                CreateDt = Convert.ToDateTime(item[0]),
                AppName = item[1] as string ?? string.Empty,
                DeviceName = item[2] as string ?? string.Empty,
                ScaleName = item[3] as string ?? string.Empty,
                SizeAppMb = Convert.ToInt16(item[4]),
                SizeFreeMb = Convert.ToInt16(item[5]),
            });
        }
        return result;
    }

    #endregion

    #region Public and private methods - LogWeb

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers, FormatType formatType, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)formatType, countAll, countSuccess, countErrors);

    public void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers, string format, int countAll, int countSuccess, int countErrors) =>
        SaveLogWebService(requestStampDt, requestDataString, responseStampDt, responseDataString, logType,
            url, parameters, headers, (byte)DataFormatUtils.GetFormatType(format), countAll, countSuccess, countErrors);

    private void SaveLogWebService(DateTime requestStampDt, string requestDataString,
        DateTime responseStampDt, string responseDataString, LogType logType,
        string url, string parameters, string headers,
        byte formatType, int countAll, int countSuccess, int countErrors)
    {
        LogWebModel logWebRequest = new()
        {
            CreateDt = DateTime.Now,
            StampDt = requestStampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            Direction = (byte)ServiceLogDirection.Request,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = requestDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        Save(logWebRequest);

        LogWebModel logWebResponse = new()
        {
            CreateDt = DateTime.Now,
            StampDt = responseStampDt,
            IsMarked = false,
            Version = AppVersion.Version,
            Direction = (byte)ServiceLogDirection.Response,
            Url = url,
            Params = parameters,
            Headers = headers,
            DataType = formatType,
            DataString = responseDataString,
            CountAll = countAll,
            CountSuccess = countSuccess,
            CountErrors = countErrors,
        };
        Save(logWebResponse);

        LogTypeModel logTypeItem = GetItemLogTypeNotNullable(logType);
        LogWebFkModel logWebFk = new()
        {
            LogWebRequest = logWebRequest,
            LogWebResponse = logWebResponse,
            App = App,
            LogType = logTypeItem,
            Device = Device,
        };
        SaveAsync(logWebFk).ConfigureAwait(false);
    }

    #endregion
}