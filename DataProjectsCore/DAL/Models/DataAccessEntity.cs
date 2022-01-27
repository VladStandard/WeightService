// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Models;
using DataShareCore.DAL.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using static DataShareCore.ShareEnums;

namespace DataProjectsCore.DAL.Models
{
    public class DataAccessEntity
    {
        #region Public and private fields and properties

        public CoreSettingsEntity CoreSettings { get; set; }
        private readonly object _locker = new();

        // https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
        private ISessionFactory? _sessionFactory = null;
        private ISessionFactory? SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                    return _sessionFactory;
                lock (_locker)
                {
                    if (CoreSettings == null)
                        throw new ArgumentException("CoreSettings is null!");
                    if (!CoreSettings.Trusted && (string.IsNullOrEmpty(CoreSettings.Username) || string.IsNullOrEmpty(CoreSettings.Password)))
                        throw new ArgumentException("CoreSettings.Username or CoreSettings.Password is null!");
                    MsSqlConfiguration config = MsSqlConfiguration.MsSql2012.ConnectionString(GetConnectionString());
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema).ShowSql();
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema);
                    config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                    FluentConfiguration configuration = Fluently.Configure().Database(config);
                    AddConfigurationMappings(configuration, CoreSettings);
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                    configuration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    try
                    {
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
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
        public bool IsOpen
        {
            get
            {
                ISession? session = GetSession();
                return session == null || session.IsOpen;
            }
        }
        public bool IsConnected
        {
            get
            {
                ISession? session = GetSession();
                return session == null || session.IsConnected;
            }
        }
        public bool IsDirty
        {
            get
            {
                ISession? session = GetSession();
                return session == null || session.IsDirty();
            }
        }

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(CoreSettingsEntity appSettings)
        {
            CoreSettings = appSettings;
            Crud = new CrudController(this, SessionFactory);
        }

        // This code have exception: 
        // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
        // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        //private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
        //    ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
        //    : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
        //        .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

        private string GetConnectionString() => CoreSettings.Trusted
            ? $"Data Source={CoreSettings.Server};Initial Catalog={CoreSettings.Db};Persist Security Info=True;Trusted Connection=True;TrustServerCertificate=True;"
            : $"Data Source={CoreSettings.Server};Initial Catalog={CoreSettings.Db};Persist Security Info=True;User ID={CoreSettings.Username};Password={CoreSettings.Password};TrustServerCertificate=True;";

        private void AddConfigurationMappings(FluentConfiguration configuration, CoreSettingsEntity coreSettings)
        {
            if (configuration == null || coreSettings == null || string.IsNullOrEmpty(coreSettings.Db))
                return;

            if (string.Equals(coreSettings.Db, "ScalesDB", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(coreSettings.Db, "SCALES", StringComparison.InvariantCultureIgnoreCase))
            {
                AddConfigurationMappingsForSystem(configuration);
                AddConfigurationMappingsForScale(configuration);
            }
            else if (string.Equals(coreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                AddConfigurationMappingsForDwh(configuration);
            }
        }

        private void AddConfigurationMappingsForSystem(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AccessMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AppMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.ErrorMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.HostMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogTypeMap>());
        }

        private void AddConfigurationMappingsForScale(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarcodeTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ContragentMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LabelMap>());
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

        private void AddConfigurationMappingsForDwh(FluentConfiguration configuration)
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

        #region Public and private methods - Share

        public ISession? GetSession() => SessionFactory?.OpenSession();

        #endregion

        #region Public and private methods - CRUD share

        //public T ActionGetIdEntity<T>(T item, DbTableAction tableAction) where T : BaseEntity, new()
        //{
        //    T result = tableAction switch
        //    {
        //        DbTableAction.New => new T(),
        //        DbTableAction.Edit => item,
        //        DbTableAction.Copy => (T)item.Clone(),
        //        DbTableAction.Delete => item,
        //        DbTableAction.Mark => item,
        //        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
        //    };
        //    if (tableAction == DbTableAction.New || tableAction == DbTableAction.Copy)
        //    {
        //        int nextId = 0;
        //        nextId = ActionGetIdEntityForScales(item, nextId);
        //        if (nextId == 0)
        //            nextId = ActionGetIdEntityForDwh(item, nextId);
        //        result.Id = nextId + 1;
        //    }
        //    return result;
        //}

        //public T ActionGetUidEntity<T>(T item, DbTableAction tableAction) where T : BaseEntity, new()
        //{
        //    T result = tableAction switch
        //    {
        //        DbTableAction.New => new T(),
        //        DbTableAction.Edit => item,
        //        DbTableAction.Copy => (T)item.Clone(),
        //        DbTableAction.Delete => item,
        //        DbTableAction.Mark => item,
        //        _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
        //    };
        //    if (tableAction == DbTableAction.New || tableAction == DbTableAction.Copy)
        //    {
        //        switch (item)
        //        {
        //            case TableSystemModels.AccessEntity:
        //                _ = Crud.GetEntity<TableSystemModels.AccessEntity>(null, 
        //                    new FieldOrderEntity(DbField.Uid, DbOrderDirection.Desc)).Uid;
        //                break;
        //            case TableSystemModels.AppEntity:
        //                _ = Crud.GetEntity<TableSystemModels.AppEntity>(null, 
        //                    new FieldOrderEntity(DbField.Uid, DbOrderDirection.Desc)).Uid;
        //                break;
        //            case TableSystemModels.LogEntity:
        //                _ = Crud.GetEntity<TableSystemModels.LogEntity>(null, 
        //                    new FieldOrderEntity(DbField.Uid, DbOrderDirection.Desc)).Uid;
        //                break;
        //            case TableSystemModels.LogTypeEntity:
        //                _ = Crud.GetEntity<TableSystemModels.LogTypeEntity>(null, 
        //                    new FieldOrderEntity(DbField.Uid, DbOrderDirection.Desc)).Uid;
        //                break;
        //            case DataShareCore.DAL.DataModels.LogSummaryEntity:
        //                _ = Crud.GetEntity<DataShareCore.DAL.DataModels.LogSummaryEntity>(null, 
        //                    new FieldOrderEntity(DbField.Uid, DbOrderDirection.Desc)).Uid;
        //                break;
        //            case DataModels.WeithingFactSummaryEntity:
        //                break;
        //        }
        //        result.Uid = Guid.NewGuid();
        //    }
        //    return result;
        //}

        //private int ActionGetIdEntityForScales<T>(T item, int nextId) where T : BaseEntity, new()
        //{
        //    switch (item)
        //    {
        //        case TableSystemModels.HostEntity:
        //            nextId = Crud.GetEntity<TableSystemModels.HostEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.BarcodeTypeEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.BarcodeTypeEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.ContragentEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.ContragentEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.LabelEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.LabelEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.NomenclatureEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.NomenclatureEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.OrderEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.OrderEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.OrderStatusEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.OrderStatusEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.OrderTypeEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.OrderTypeEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.PluEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.PluEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.ProductionFacilityEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.ProductionFacilityEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.ProductSeriesEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.ProductSeriesEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.ScaleEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.ScaleEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.TemplateResourceEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.TemplateResourceEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.TemplateEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.TemplateEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.WeithingFactEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.WeithingFactEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.WorkshopEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.WorkshopEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.PrinterEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.PrinterEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.PrinterResourceEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.PrinterResourceEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableScaleModels.PrinterTypeEntity:
        //            nextId = Crud.GetEntity<TableScaleModels.PrinterTypeEntity>(null, 
        //                new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //    }
        //    return nextId;
        //}

        //private int ActionGetIdEntityForDwh<T>(T item, int nextId) where T : BaseEntity, new()
        //{
        //    switch (item)
        //    {
        //        case TableDwhModels.BrandEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.BrandEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.InformationSystemEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.InformationSystemEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.NomenclatureEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.NomenclatureEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.NomenclatureGroupEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.NomenclatureGroupEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.NomenclatureLightEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.NomenclatureLightEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.NomenclatureTypeEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.NomenclatureTypeEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //        case TableDwhModels.StatusEntity:
        //            nextId = Crud.GetEntity<TableDwhModels.StatusEntity>(null, new FieldOrderEntity(DbField.Id, DbOrderDirection.Desc)).Id;
        //            break;
        //    }
        //    return nextId;
        //}

        //public void ActionDeleteEntity<T>(T item) where T : BaseEntity, new() => Crud.DeleteEntity(item);

        //public void ActionMarkedEntity<T>(T item) where T : BaseEntity, new() => Crud.MarkedEntity(item);

        #endregion
    }
}
