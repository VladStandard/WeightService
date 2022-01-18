// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;

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
        public CrudController? OrderStatusesCrud { get; private set; } = null;
        public CrudController? OrderTypesCrud { get; private set; } = null;
        public CrudController? PlusCrud { get; private set; } = null;
        public CrudController? PrintersCrud { get; private set; } = null;
        public CrudController? PrinterResourcesCrud { get; private set; } = null;
        public CrudController? ProductSeriesCrud { get; private set; } = null;
        public CrudController? ScalesCrud { get; private set; } = null;
        public CrudController? TemplatesCrud { get; private set; } = null;
        public CrudController? WeithingFactsCrud { get; private set; } = null;
        public CrudController? WorkshopsCrud { get; private set; } = null;
        // DWH tables CRUD.
        public CrudController? BrandCrud { get; private set; } = null;
        public CrudController? InformationSystemCrud { get; private set; } = null;
        public CrudController? NomenclatureCrud { get; private set; } = null;
        public CrudController? NomenclatureGroupCrud { get; private set; } = null;
        public CrudController? NomenclatureLightCrud { get; private set; } = null;
        public CrudController? NomenclatureTypeCrud { get; private set; } = null;
        public CrudController? StatusCrud { get; private set; } = null;

        public bool IsDisabled => !GetSession().IsConnected;
        public bool IsOpen => GetSession().IsOpen;
        public bool IsConnected => GetSession().IsConnected;
        public bool IsDirty => GetSession().IsDirty();

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(CoreSettingsEntity appSettings)
        {
            CoreSettings = appSettings;

            if (string.Equals(CoreSettings.Db, "ScalesDB", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(CoreSettings.Db, "SCALES", StringComparison.InvariantCultureIgnoreCase))
            {
                Crud = new CrudController(this, SessionFactory);

                // SCALES tables CRUD.
                OrderStatusesCrud = new CrudController(this, SessionFactory);
                OrderTypesCrud = new CrudController(this, SessionFactory);
                PlusCrud = new CrudController(this, SessionFactory);
                PrinterResourcesCrud = new CrudController(this, SessionFactory);
                PrintersCrud = new CrudController(this, SessionFactory);
                ProductSeriesCrud = new CrudController(this, SessionFactory);
                ScalesCrud = new CrudController(this, SessionFactory);
                TemplatesCrud = new CrudController(this, SessionFactory);
                WeithingFactsCrud = new CrudController(this, SessionFactory);
                WorkshopsCrud = new CrudController(this, SessionFactory);
                //// Datas CRUD.
                //DeviceCrud = new CrudController(this, SessionFactory);
                //LogSummaryCrud = new CrudController(this, SessionFactory);
                //WeithingFactSummaryCrud = new CrudController(this, SessionFactory);
            }
            else if (string.Equals(CoreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                // DWH tables CRUD.
                BrandCrud = new CrudController(this, SessionFactory);
                InformationSystemCrud = new CrudController(this, SessionFactory);
                NomenclatureCrud = new CrudController(this, SessionFactory);
                NomenclatureGroupCrud = new CrudController(this, SessionFactory);
                NomenclatureLightCrud = new CrudController(this, SessionFactory);
                NomenclatureTypeCrud = new CrudController(this, SessionFactory);
                StatusCrud = new CrudController(this, SessionFactory);
            }
        }

        // This code have exception: 
        // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
        // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
            ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
            : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

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
                AddConfigurationMappingsForScale(configuration);
            }
            else if (string.Equals(coreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                AddConfigurationMappingsForDwh(configuration);
            }
        }

        private void AddConfigurationMappingsForScale(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AccessMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AppMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.HostMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogTypeMap>());

            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarcodeTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ContragentMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ErrorMap>());
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

        public ISession GetSession() => SessionFactory.OpenSession();

        #endregion

        #region Public and private methods - CRUD share

        public T ActionGetIdEntity<T>(T item, ShareEnums.DbTableAction tableAction) where T : BaseEntity, new()
        {
            T result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => item,
                ShareEnums.DbTableAction.Copy => (T)item.Clone(),
                ShareEnums.DbTableAction.Delete => item,
                ShareEnums.DbTableAction.Mark => item,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                int nextId = 0;
                nextId = ActionGetIdEntityForScales(item, nextId);
                if (nextId == 0)
                    nextId = ActionGetIdEntityForDwh(item, nextId);
                result.Id = nextId + 1;
            }
            return result;
        }

        public T ActionGetUidEntity<T>(T item, ShareEnums.DbTableAction tableAction) where T : BaseEntity, new()
        {
            T result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => item,
                ShareEnums.DbTableAction.Copy => (T)item.Clone(),
                ShareEnums.DbTableAction.Delete => item,
                ShareEnums.DbTableAction.Mark => item,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                if (item is TableSystemModels.AccessEntity)
                {
                    _ = Crud.GetEntity<TableSystemModels.AccessEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (item is TableSystemModels.AppEntity)
                {
                    _ = Crud.GetEntity<TableSystemModels.AppEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (item is TableSystemModels.LogEntity)
                {
                    _ = Crud.GetEntity<TableSystemModels.LogEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (item is TableSystemModels.LogTypeEntity)
                {
                    _ = Crud.GetEntity<TableSystemModels.LogTypeEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (item is DataShareCore.DAL.DataModels.LogSummaryEntity)
                {
                    _ = Crud.GetEntity<DataShareCore.DAL.DataModels.LogSummaryEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (item is DataModels.WeithingFactSummaryEntity)
                {
                    //_ = WeithingFactSummaryCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                result.Uid = Guid.NewGuid();
            }
            return result;
        }

        private int ActionGetIdEntityForScales<T>(T item, int nextId) where T : BaseEntity, new()
        {
            if (item is TableSystemModels.HostEntity)
            {
                nextId = Crud.GetEntity<TableSystemModels.HostEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.BarcodeTypeEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.BarcodeTypeEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.ContragentEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.ContragentEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.LabelEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.LabelEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.NomenclatureEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.NomenclatureEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.OrderEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.OrderEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.OrderStatusEntity)
            {
                if (OrderStatusesCrud != null)
                    nextId = OrderStatusesCrud.GetEntity<TableScaleModels.OrderStatusEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.OrderTypeEntity)
            {
                if (OrderTypesCrud != null)
                    nextId = OrderTypesCrud.GetEntity<TableScaleModels.OrderTypeEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.PluEntity)
            {
                if (PlusCrud != null)
                    nextId = PlusCrud.GetEntity<TableScaleModels.PluEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.ProductionFacilityEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.ProductionFacilityEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.ProductSeriesEntity)
            {
                if (ProductSeriesCrud != null)
                    nextId = ProductSeriesCrud.GetEntity<TableScaleModels.ProductSeriesEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.ScaleEntity)
            {
                if (ScalesCrud != null)
                    nextId = ScalesCrud.GetEntity<TableScaleModels.ScaleEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.TemplateResourceEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.TemplateResourceEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.TemplateEntity)
            {
                if (TemplatesCrud != null)
                    nextId = TemplatesCrud.GetEntity<TableScaleModels.TemplateEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.WeithingFactEntity)
            {
                if (WeithingFactsCrud != null)
                    nextId = WeithingFactsCrud.GetEntity<TableScaleModels.WeithingFactEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.WorkshopEntity)
            {
                if (WorkshopsCrud != null)
                    nextId = WorkshopsCrud.GetEntity<TableScaleModels.WorkshopEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.PrinterEntity)
            {
                if (PrintersCrud != null)
                    nextId = PrintersCrud.GetEntity<TableScaleModels.PrinterEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.PrinterResourceEntity)
            {
                if (PrinterResourcesCrud != null)
                    nextId = PrinterResourcesCrud.GetEntity<TableScaleModels.PrinterResourceEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableScaleModels.PrinterTypeEntity)
            {
                nextId = Crud.GetEntity<TableScaleModels.PrinterTypeEntity>(null, 
                    new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            return nextId;
        }

        private int ActionGetIdEntityForDwh<T>(T item, int nextId) where T : BaseEntity, new()
        {
            if (item is TableDwhModels.BrandEntity)
            {
                if (BrandCrud != null)
                    nextId = BrandCrud.GetEntity<TableDwhModels.BrandEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.InformationSystemEntity)
            {
                if (InformationSystemCrud != null)
                    nextId = InformationSystemCrud.GetEntity<TableDwhModels.InformationSystemEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.NomenclatureEntity)
            {
                if (NomenclatureCrud != null)
                    nextId = NomenclatureCrud.GetEntity<TableDwhModels.NomenclatureEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.NomenclatureGroupEntity)
            {
                if (NomenclatureGroupCrud != null)
                    nextId = NomenclatureGroupCrud.GetEntity<TableDwhModels.NomenclatureGroupEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.NomenclatureLightEntity)
            {
                if (NomenclatureLightCrud != null)
                    nextId = NomenclatureLightCrud.GetEntity<TableDwhModels.NomenclatureLightEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.NomenclatureTypeEntity)
            {
                if (NomenclatureTypeCrud != null)
                    nextId = NomenclatureTypeCrud.GetEntity<TableDwhModels.NomenclatureTypeEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (item is TableDwhModels.StatusEntity)
            {
                if (StatusCrud != null)
                    nextId = StatusCrud.GetEntity<TableDwhModels.StatusEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }

            return nextId;
        }

        public void ActionDeleteEntity<T>(T item) where T : IBaseEntity, new()
        {
            // SYSTEM.
            if (item is TableSystemModels.AccessEntity accessEntity)
                Crud.DeleteEntity(accessEntity);
            else if (item is TableSystemModels.AppEntity appEntity)
                Crud.DeleteEntity(appEntity);
            else if (item is TableSystemModels.HostEntity hostsEntity)
                Crud.DeleteEntity(hostsEntity);
            else if (item is TableSystemModels.LogEntity logEntity)
                Crud.DeleteEntity(logEntity);
            else if (item is TableSystemModels.LogTypeEntity logTypeEntity)
                Crud.DeleteEntity(logTypeEntity);
            // SCALES.
            else if (item is TableScaleModels.LabelEntity labelsEntity)
                Crud.DeleteEntity(labelsEntity);
            else if (item is TableScaleModels.BarcodeTypeEntity barCodeTypesEntity)
                Crud.DeleteEntity(barCodeTypesEntity);
            else if (item is TableScaleModels.ContragentEntity contragentsEntity)
                Crud.DeleteEntity(contragentsEntity);
            else if (item is DataShareCore.DAL.DataModels.LogSummaryEntity logSummaryEntity)
                Crud.DeleteEntity(logSummaryEntity);
            else if (item is TableScaleModels.NomenclatureEntity nomenclatureEntity)
                Crud.DeleteEntity(nomenclatureEntity);
            else if (item is TableScaleModels.OrderEntity ordersEntity)
                Crud.DeleteEntity(ordersEntity);
            else if (item is TableScaleModels.OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud?.DeleteEntity(orderStatusEntity);
            else if (item is TableScaleModels.OrderTypeEntity orderTypesEntity)
                OrderTypesCrud?.DeleteEntity(orderTypesEntity);
            else if (item is TableScaleModels.PluEntity pluEntity)
                PlusCrud?.DeleteEntity(pluEntity);
            else if (item is TableScaleModels.ProductionFacilityEntity productionFacilityEntity)
                Crud.DeleteEntity(productionFacilityEntity);
            else if (item is TableScaleModels.ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud?.DeleteEntity(productSeriesEntity);
            else if (item is TableScaleModels.ScaleEntity scalesEntity)
                ScalesCrud?.DeleteEntity(scalesEntity);
            else if (item is TableScaleModels.TaskEntity taskEntity)
                Crud.DeleteEntity(taskEntity);
            else if (item is TableScaleModels.TaskTypeEntity taskTypeEntity)
                Crud.DeleteEntity(taskTypeEntity);
            else if (item is TableScaleModels.TemplateEntity templatesEntity)
                TemplatesCrud?.DeleteEntity(templatesEntity);
            else if (item is TableScaleModels.TemplateResourceEntity templateResourcesEntity)
                Crud.DeleteEntity(templateResourcesEntity);
            else if (item is TableScaleModels.WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud?.DeleteEntity(weithingFactEntity);
            else if (item is DataModels.WeithingFactSummaryEntity weithingFactSummaryEntity)
                Crud.DeleteEntity(weithingFactSummaryEntity);
            else if (item is TableScaleModels.WorkshopEntity workshopEntity)
                WorkshopsCrud?.DeleteEntity(workshopEntity);
            else if (item is TableScaleModels.PrinterEntity zebraPrinterEntity)
                PrintersCrud?.DeleteEntity(zebraPrinterEntity);
            else if (item is TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity)
                Crud.MarkedEntity(zebraPrinterTypeEntity);
            else if (item is TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud?.DeleteEntity(zebraPrinterResourceRefEntity);
            // DWH.
            else if (item is TableDwhModels.BrandEntity brandEntity)
                BrandCrud?.DeleteEntity(brandEntity);
            else if (item is TableDwhModels.InformationSystemEntity informationSystemEntity)
                InformationSystemCrud?.DeleteEntity(informationSystemEntity);
            else if (item is TableDwhModels.NomenclatureEntity dwhNomenclatureEntity)
                NomenclatureCrud?.DeleteEntity(dwhNomenclatureEntity);
            else if (item is TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity)
                NomenclatureGroupCrud?.DeleteEntity(nomenclatureGroupEntity);
            else if (item is TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity)
                NomenclatureLightCrud?.DeleteEntity(nomenclatureLightEntity);
            else if (item is TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity)
                NomenclatureTypeCrud?.DeleteEntity(nomenclatureTypeEntity);
            else if (item is TableDwhModels.StatusEntity statusEntity)
                StatusCrud?.DeleteEntity(statusEntity);
        }

        public void ActionMarkedEntity<T>(T item) where T : IBaseEntity, new()
        {
            // SYSTEM.
            switch (item)
            {
                case TableSystemModels.AccessEntity:
                case TableSystemModels.AppEntity:
                case TableSystemModels.HostEntity:
                case TableSystemModels.LogEntity:
                case TableSystemModels.LogTypeEntity:
                case DataShareCore.DAL.DataModels.LogSummaryEntity:
                case DataModels.WeithingFactSummaryEntity:
                case TableScaleModels.BarcodeTypeEntity:
                case TableScaleModels.TaskEntity:
                case TableScaleModels.TaskTypeEntity:
                    Crud.MarkedEntity(item);
                    break;
                case TableScaleModels.ContragentEntity contragentsEntity:
                    Crud.MarkedEntity(contragentsEntity);
                    break;
                case TableScaleModels.LabelEntity labelsEntity:
                    Crud.MarkedEntity(labelsEntity);
                    break;
                case TableScaleModels.NomenclatureEntity nomenclatureEntity:
                    Crud.MarkedEntity(nomenclatureEntity);
                    break;
                case TableScaleModels.OrderEntity ordersEntity:
                    Crud.MarkedEntity(ordersEntity);
                    break;
                case TableScaleModels.OrderStatusEntity orderStatusEntity:
                    OrderStatusesCrud?.MarkedEntity(orderStatusEntity);
                    break;
                case TableScaleModels.OrderTypeEntity orderTypesEntity:
                    OrderTypesCrud?.MarkedEntity(orderTypesEntity);
                    break;
                case TableScaleModels.PluEntity pluEntity:
                    PlusCrud?.MarkedEntity(pluEntity);
                    break;
                case TableScaleModels.ProductionFacilityEntity productionFacilityEntity:
                    Crud.MarkedEntity(productionFacilityEntity);
                    break;
                case TableScaleModels.ProductSeriesEntity productSeriesEntity:
                    ProductSeriesCrud?.MarkedEntity(productSeriesEntity);
                    break;
                case TableScaleModels.ScaleEntity scalesEntity:
                    ScalesCrud?.MarkedEntity(scalesEntity);
                    break;
                case TableScaleModels.TemplateEntity templatesEntity:
                    TemplatesCrud?.MarkedEntity(templatesEntity);
                    break;
                case TableScaleModels.TemplateResourceEntity templateResourcesEntity:
                    Crud.MarkedEntity(templateResourcesEntity);
                    break;
                case TableScaleModels.WeithingFactEntity weithingFactEntity:
                    WeithingFactsCrud?.MarkedEntity(weithingFactEntity);
                    break;
                case TableScaleModels.WorkshopEntity workshopEntity:
                    WorkshopsCrud?.MarkedEntity(workshopEntity);
                    break;
                case TableScaleModels.PrinterEntity zebraPrinterEntity:
                    PrintersCrud?.MarkedEntity(zebraPrinterEntity);
                    break;
                case TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity:
                    Crud.MarkedEntity(zebraPrinterTypeEntity);
                    break;
                case TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity:
                    PrinterResourcesCrud?.MarkedEntity(zebraPrinterResourceRefEntity);
                    break;
                case TableDwhModels.BrandEntity brandEntity:
                    BrandCrud?.MarkedEntity(brandEntity);
                    break;
                case TableDwhModels.InformationSystemEntity informationSystemEntity:
                    InformationSystemCrud?.MarkedEntity(informationSystemEntity);
                    break;
                case TableDwhModels.NomenclatureEntity dwhNomenclatureEntity:
                    NomenclatureCrud?.MarkedEntity(dwhNomenclatureEntity);
                    break;
                case TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity:
                    NomenclatureGroupCrud?.MarkedEntity(nomenclatureGroupEntity);
                    break;
                case TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity:
                    NomenclatureLightCrud?.MarkedEntity(nomenclatureLightEntity);
                    break;
                case TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity:
                    NomenclatureTypeCrud?.MarkedEntity(nomenclatureTypeEntity);
                    break;
                case TableDwhModels.StatusEntity statusEntity:
                    StatusCrud?.MarkedEntity(statusEntity);
                    break;
            }
        }

        #endregion
    }
}
