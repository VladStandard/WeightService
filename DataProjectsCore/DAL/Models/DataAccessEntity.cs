// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProjectsCore.DAL.Models
{
    public class DataAccessEntity
    {
        #region Public and private fields and properties

        public CoreSettingsEntity CoreSettings { get; set; }
        public DataConfigurationEntity DataConfig { get; set; }
        private delegate void ExecCallback(ISession session);
        private readonly object _locker = new();

        // https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
        private ISessionFactory? _sessionFactory;
        private ISessionFactory SessionFactory
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
                    // This code have exception: 
                    // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
                    // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
                    //MsSqlConfiguration config = CoreSettings.Trusted
                    //    ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                    //        .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
                    //    : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                    //        .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));
                    MsSqlConfiguration config = MsSqlConfiguration.MsSql2012.ConnectionString(GetConnectionString());
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema).ShowSql();
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema);
                    config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                    FluentConfiguration configuration = Fluently.Configure().Database(config);
                    AddConfigurationMappings(configuration, CoreSettings);
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                    configuration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    _sessionFactory = configuration.BuildSessionFactory();
                    return _sessionFactory;
                }
            }
        }

        // SYSTEM Tables CRUD.
        public BaseCrud<TableSystemModels.AccessEntity>? AccessesCrud = null;
        public BaseCrud<TableSystemModels.AppEntity>? AppsCrud = null;
        public BaseCrud<TableSystemModels.LogEntity>? LogsCrud = null;
        public BaseCrud<TableSystemModels.LogTypeEntity>? LogTypesCrud = null;
        public TableSystemModels.HostCrud? HostsCrud = null;

        // SCALES Tables CRUD.
        public BaseCrud<TableScaleModels.BarcodeTypeEntity>? BarcodeTypesCrud = null;
        public BaseCrud<TableScaleModels.ContragentEntity>? ContragentsCrud = null;
        public BaseCrud<TableScaleModels.ErrorEntity>? ErrorsCrud = null;
        public BaseCrud<TableScaleModels.LabelEntity>? LabelsCrud = null;
        public BaseCrud<TableScaleModels.NomenclatureEntity>? NomenclaturesCrud = null;
        public BaseCrud<TableScaleModels.OrderEntity>? OrdersCrud = null;
        public BaseCrud<TableScaleModels.OrderStatusEntity>? OrderStatusesCrud = null;
        public BaseCrud<TableScaleModels.OrderTypeEntity>? OrderTypesCrud = null;
        public BaseCrud<TableScaleModels.PluEntity>? PlusCrud = null;
        public BaseCrud<TableScaleModels.ProductSeriesEntity>? ProductSeriesCrud = null;
        public BaseCrud<TableScaleModels.ProductionFacilityEntity>? ProductionFacilitiesCrud = null;
        public BaseCrud<TableScaleModels.ScaleEntity>? ScalesCrud = null;
        public BaseCrud<TableScaleModels.TaskEntity>? TaskCrud = null;
        public BaseCrud<TableScaleModels.TaskTypeEntity>? TaskTypeCrud = null;
        public BaseCrud<TableScaleModels.TemplateEntity>? TemplatesCrud = null;
        public BaseCrud<TableScaleModels.WeithingFactEntity>? WeithingFactsCrud = null;
        public BaseCrud<TableScaleModels.WorkshopEntity>? WorkshopsCrud = null;
        public BaseCrud<TableScaleModels.PrinterEntity>? PrintersCrud = null;
        public BaseCrud<TableScaleModels.PrinterResourceEntity>? PrinterResourcesCrud = null;
        public BaseCrud<TableScaleModels.PrinterTypeEntity>? PrinterTypesCrud = null;
        public TableScaleModels.TemplateResourceCrud? TemplateResourcesCrud = null;
        // Datas CRUD.
        public BaseCrud<DataModels.DeviceEntity>? DeviceCrud = null;
        public BaseCrud<DataShareCore.DAL.DataModels.LogSummaryEntity>? LogSummaryCrud = null;
        public BaseCrud<DataModels.WeithingFactSummaryEntity>? WeithingFactSummaryCrud = null;

        // DWH Tables CRUD.
        public BaseCrud<TableDwhModels.BrandEntity>? BrandCrud = null;
        public BaseCrud<TableDwhModels.InformationSystemEntity>? InformationSystemCrud = null;
        public BaseCrud<TableDwhModels.NomenclatureEntity>? NomenclatureCrud = null;
        public BaseCrud<TableDwhModels.NomenclatureGroupEntity>? NomenclatureGroupCrud = null;
        public BaseCrud<TableDwhModels.NomenclatureLightEntity>? NomenclatureLightCrud = null;
        public BaseCrud<TableDwhModels.NomenclatureTypeEntity>? NomenclatureTypeCrud = null;
        public BaseCrud<TableDwhModels.StatusEntity>? StatusCrud = null;

        public bool IsDisabled => !GetSession().IsConnected;
        public bool IsOpen => GetSession().IsOpen;
        public bool IsConnected => GetSession().IsConnected;
        public bool IsDirty => GetSession().IsDirty();

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(CoreSettingsEntity appSettings)
        {
            CoreSettings = appSettings;
            DataConfig = new DataConfigurationEntity();

            if (string.Equals(CoreSettings.Db, "ScalesDB", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(CoreSettings.Db, "SCALES", StringComparison.InvariantCultureIgnoreCase))
            {
                // SYSTEM tables CRUD.
                AccessesCrud = new BaseCrud<TableSystemModels.AccessEntity>(this);
                AppsCrud = new BaseCrud<TableSystemModels.AppEntity>(this);
                HostsCrud = new TableSystemModels.HostCrud(this);
                LogsCrud = new BaseCrud<TableSystemModels.LogEntity>(this);
                LogTypesCrud = new BaseCrud<TableSystemModels.LogTypeEntity>(this);
                // SCALES tables CRUD.
                BarcodeTypesCrud = new BaseCrud<TableScaleModels.BarcodeTypeEntity>(this);
                ContragentsCrud = new BaseCrud<TableScaleModels.ContragentEntity>(this);
                ErrorsCrud = new BaseCrud<TableScaleModels.ErrorEntity>(this);
                LabelsCrud = new BaseCrud<TableScaleModels.LabelEntity>(this);
                NomenclaturesCrud = new BaseCrud<TableScaleModels.NomenclatureEntity>(this);
                ScalesCrud = new BaseCrud<TableScaleModels.ScaleEntity>(this);
                OrdersCrud = new BaseCrud<TableScaleModels.OrderEntity>(this);
                OrderStatusesCrud = new BaseCrud<TableScaleModels.OrderStatusEntity>(this);
                OrderTypesCrud = new BaseCrud<TableScaleModels.OrderTypeEntity>(this);
                PlusCrud = new BaseCrud<TableScaleModels.PluEntity>(this);
                ProductionFacilitiesCrud = new BaseCrud<TableScaleModels.ProductionFacilityEntity>(this);
                ProductSeriesCrud = new BaseCrud<TableScaleModels.ProductSeriesEntity>(this);
                TaskCrud = new BaseCrud<TableScaleModels.TaskEntity>(this);
                TaskTypeCrud = new BaseCrud<TableScaleModels.TaskTypeEntity>(this);
                TemplatesCrud = new BaseCrud<TableScaleModels.TemplateEntity>(this);
                TemplateResourcesCrud = new TableScaleModels.TemplateResourceCrud(this);
                PrinterResourcesCrud = new BaseCrud<TableScaleModels.PrinterResourceEntity>(this);
                WeithingFactsCrud = new BaseCrud<TableScaleModels.WeithingFactEntity>(this);
                WorkshopsCrud = new BaseCrud<TableScaleModels.WorkshopEntity>(this);
                PrinterTypesCrud = new BaseCrud<TableScaleModels.PrinterTypeEntity>(this);
                PrintersCrud = new BaseCrud<TableScaleModels.PrinterEntity>(this);
                // Datas CRUD.
                DeviceCrud = new BaseCrud<DataModels.DeviceEntity>(this);
                LogSummaryCrud = new BaseCrud<DataShareCore.DAL.DataModels.LogSummaryEntity>(this);
                WeithingFactSummaryCrud = new BaseCrud<DataModels.WeithingFactSummaryEntity>(this);
            }
            else if (string.Equals(CoreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                // DWH tables CRUD.
                BrandCrud = new BaseCrud<TableDwhModels.BrandEntity>(this);
                InformationSystemCrud = new BaseCrud<TableDwhModels.InformationSystemEntity>(this);
                NomenclatureGroupCrud = new BaseCrud<TableDwhModels.NomenclatureGroupEntity>(this);
                NomenclatureTypeCrud = new BaseCrud<TableDwhModels.NomenclatureTypeEntity>(this);
                NomenclatureCrud = new BaseCrud<TableDwhModels.NomenclatureEntity>(this);
                NomenclatureLightCrud = new BaseCrud<TableDwhModels.NomenclatureLightEntity>(this);
                StatusCrud = new BaseCrud<TableDwhModels.StatusEntity>(this);
            }
        }

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
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PluMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductionFacilityMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductSeriesMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ScaleMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WeithingFactMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WorkshopMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterTypeMap>());
        }

        private void AddConfigurationMappingsForDwh(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.BrandMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.InformationSystemMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureGroupMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureLightMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.StatusMap>());
        }

        #endregion

        #region Public and private methods - Share

        public override string ToString()
        {
            return $"{nameof(DataConfig)}: {DataConfig}. " +
                   $"{nameof(GetSession)}: {GetSession()}.";
        }

        public ISession GetSession() => SessionFactory.OpenSession();

        public void LogException(Exception ex, string filePath, int lineNumber, string memberName)
        {
            Console.WriteLine("Catch exception.");
            Console.WriteLine($"{nameof(filePath)}: {filePath}");
            Console.WriteLine($"{nameof(lineNumber)}: {lineNumber}");
            Console.WriteLine($"{nameof(memberName)}: {memberName}");
            Console.WriteLine($"{memberName}. {ex.Message}");
            if (ex.InnerException != null)
                Console.WriteLine($"{memberName}. {ex.InnerException.Message}");

            LogExceptionToSql(ex, filePath, lineNumber, memberName);
        }

        public void LogExceptionToSql(Exception ex, string filePath, int lineNumber, string memberName)
        {
            if (ErrorsCrud == null)
                return;
            int idLast = ErrorsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            TableScaleModels.ErrorEntity? error = new()
            {
                Id = idLast + 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FilePath = filePath,
                LineNumber = lineNumber,
                MemberName = memberName,
                Exception = ex.Message,
                InnerException = ex.InnerException.Message,
            };
            ErrorsCrud.SaveEntity(error);
        }

        #endregion

        #region Public and private methods - CRUD share

        public T[] GetEntitiesWithConfig<T>(string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            T[]? result = new T[0];
            ExecTransaction((session) => {
                if (DataConfig != null)
                {
                    result = DataConfig.OrderAsc
                        ? session.Query<T>()
                        .OrderBy(ent => ent)
                        .Skip(DataConfig.PageNo * DataConfig.PageSize)
                        .Take(DataConfig.PageSize)
                        .ToArray()
                        : session.Query<T>()
                            .OrderByDescending(ent => ent)
                            .Skip(DataConfig.PageNo * DataConfig.PageSize)
                            .Take(DataConfig.PageSize)
                            .ToArray()
                        ;
                }
            }, filePath, lineNumber, memberName);
            return result;
        }

        private ICriteria GetCriteria<T>(ISession session, FieldListEntity? fieldList, FieldOrderEntity order, int maxResults)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }
            if (fieldList is { Use: true, Fields: { } })
            {
                AbstractCriterion fieldsWhere = Restrictions.AllEq(fieldList.Fields);
                criteria.Add(fieldsWhere);
            }
            if (order is { Use: true })
            {
                Order fieldOrder = order.Direction == ShareEnums.DbOrderDirection.Asc ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        private void ExecTransaction(ExecCallback callback, string filePath, int lineNumber, string memberName)
        {
            lock (_locker)
            {
                using ISession? session = GetSession();
                if (session != null)
                {
                    using ITransaction? transaction = session.BeginTransaction();
                    try
                    {
                        callback?.Invoke(session);
                        session.Flush();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        LogException(ex, filePath, lineNumber, memberName);
                        throw;
                    }
                    finally
                    {
                        session.Disconnect();
                    }
                }
            }
        }

        public T GetEntity<T>(FieldListEntity? fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName) where T : IBaseEntity, new()
        {
            T? result = new();
            ExecTransaction((session) => {
                ICriteria criteria = GetCriteria<T>(session, fieldList, order, 1);
                IList<T>? list = criteria?.List<T>();
                result = list.FirstOrDefault() ?? new T();
            }, filePath, lineNumber, memberName);
            return result;
        }

        //public string GetSqlStringFieldsSelect(string[] fieldsSelect)
        //{
        //    var result = string.Empty;
        //    foreach (var field in fieldsSelect)
        //    {
        //        result += $"[{field}], ";
        //    }
        //    return result[0..^2];
        //}

        //public string GetSqlStringValuesParams(object[] valuesParams)
        //{
        //    var result = string.Empty;
        //    foreach (var value in valuesParams)
        //    {
        //        result += value switch
        //        {
        //            int _ or decimal _ => $"{value}, ",
        //            _ => $"'{value}', ",
        //        };
        //    }
        //    return result[0..^2];
        //}

        //public ISQLQuery GetSqlQuery<T>(ISession session, string from, string[] fieldsSelect, object[] valuesParams)
        //{
        //    if (string.IsNullOrEmpty(from) || fieldsSelect == null || fieldsSelect.Length == 0 ||
        //        valuesParams == null || valuesParams.Length == 0)
        //        return null;

        //    var sqlQuery = $"select {GetSqlStringFieldsSelect(fieldsSelect)} from {from} ({GetSqlStringValuesParams(valuesParams)})";
        //    var result = session.CreateSQLQuery(sqlQuery).AddEntity(typeof(T));
        //    return result;
        //}

        public ISQLQuery? GetSqlQuery(ISession session, string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            return session.CreateSQLQuery(query);
        }

        public T[] GetEntities<T>(FieldListEntity fieldList, FieldOrderEntity order, int maxResults, string filePath, int lineNumber, string memberName)
        {
            T[]? result = new T[0];
            ExecTransaction((session) => {
                result = GetCriteria<T>(session, fieldList, order, maxResults).List<T>().ToArray();
            }, filePath, lineNumber, memberName);
            return result;
        }

        //public T[] GetEntitiesNative<T>(string[] fieldsSelect, string from, object[] valuesParams,
        //    string filePath, int lineNumber, string memberName) where T : class
        //{
        //    var result = new T[0];
        //    using var session = GetSession();
        //    if (session != null)
        //    {
        //        using var transaction = session.BeginTransaction();
        //        try
        //        {
        //            var query = GetSqlQuery<T>(session, from, fieldsSelect, valuesParams);
        //            query.AddEntity(typeof(TU));
        //            if (entities != null)
        //            {
        //                var listEntities = entities.List<T>();
        //                result = new T[listEntities.Count];
        //                for (int i = 0; i < result.Length; i++)
        //                {
        //                    result[i] = (T)listEntities[i];
        //                }
        //            }

        //            session.Flush();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            LogException(ex, filePath, lineNumber, memberName);
        //            throw;
        //        }
        //        finally
        //        {
        //            session.Disconnect();
        //        }
        //    }
        //    return result;
        //}

        public T[] GetEntitiesNativeMapping<T>(string query, string filePath, int lineNumber, string memberName)
        {
            T[]? result = new T[0];
            ExecTransaction((session) => {
                ISQLQuery? sqlQuery = GetSqlQuery(session, query);
                if (sqlQuery != null)
                {
                    sqlQuery.AddEntity(typeof(T));
                    System.Collections.IList? listEntities = sqlQuery.List();
                    result = new T[listEntities.Count];
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = (T)listEntities[i];
                    }
                }
            }, filePath, lineNumber, memberName);
            return result;
        }

        public object[] GetEntitiesNativeObject(string query, string filePath, int lineNumber, string memberName)
        {
            object[]? result = new object[0];
            ExecTransaction((session) => {
                ISQLQuery? sqlQuery = GetSqlQuery(session, query);
                if (sqlQuery != null)
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
            }, filePath, lineNumber, memberName);
            return result;
        }

        public int ExecQueryNative(string query, Dictionary<string, object> parameters, string filePath, int lineNumber, string memberName)
        {
            int result = 0;
            ExecTransaction((session) => {
                ISQLQuery? sqlQuery = GetSqlQuery(session, query);
                if (sqlQuery != null && parameters != null)
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        if (parameter.Value is byte[] imagedata)
                            sqlQuery.SetParameter(parameter.Key, imagedata);
                        else
                            sqlQuery.SetParameter(parameter.Key, parameter.Value);
                    }
                    result = sqlQuery.ExecuteUpdate();
                }
            }, filePath, lineNumber, memberName);
            return result;
        }

        public void SaveEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.Save(entity);
            }, filePath, lineNumber, memberName);
        }

        public void UpdateEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.SaveOrUpdate(entity);
            }, filePath, lineNumber, memberName);
        }

        public void DeleteEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.Delete(entity);
            }, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity<T>(T entity, string filePath, int lineNumber, string memberName)
        {
            bool result = false;
            ExecTransaction((session) => {
                result = session.Query<T>().Any(x => x.IsAny(entity));
            }, filePath, lineNumber, memberName);
            return result;
        }

        public bool ExistsEntity<T>(FieldListEntity fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName)
        {
            bool result = false;
            ExecTransaction((session) => {
                result = GetCriteria<T>(session, fieldList, order, 1).List<T>().Count > 0;
            }, filePath, lineNumber, memberName);
            return result;
        }

        public T ActionGetIdEntity<T>(BaseIdEntity entity, ShareEnums.DbTableAction tableAction) where T : BaseIdEntity, new()
        {
            T result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => (T)entity,
                ShareEnums.DbTableAction.Copy => (T)((T)entity).Clone(),
                ShareEnums.DbTableAction.Delete => (T)entity,
                ShareEnums.DbTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };

            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                int nextId = 0;
                nextId = ActionGetIdEntityForScales<T>(nextId);
                if (nextId == 0)
                    nextId = ActionGetIdEntityForDwh<T>(nextId);
                result.Id = nextId + 1;
            }
            return result;
        }

        private int ActionGetIdEntityForScales<T>(int nextId) where T : BaseIdEntity, new()
        {
            if (typeof(T) == typeof(TableSystemModels.HostEntity))
            {
                if (HostsCrud != null)
                    nextId = HostsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.BarcodeTypeEntity))
            {
                if (BarcodeTypesCrud != null)
                    nextId = BarcodeTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
            {
                if (ContragentsCrud != null)
                    nextId = ContragentsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                if (LabelsCrud != null)
                    nextId = LabelsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
            {
                if (NomenclaturesCrud != null)
                    nextId = NomenclaturesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                if (OrdersCrud != null)
                    nextId = OrdersCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                if (OrderStatusesCrud != null)
                    nextId = OrderStatusesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                if (OrderTypesCrud != null)
                    nextId = OrderTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                if (PlusCrud != null)
                    nextId = PlusCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                if (ProductionFacilitiesCrud != null)
                    nextId = ProductionFacilitiesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                if (ProductSeriesCrud != null)
                    nextId = ProductSeriesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                if (ScalesCrud != null)
                    nextId = ScalesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                if (TemplateResourcesCrud != null)
                    nextId = TemplateResourcesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                if (TemplatesCrud != null)
                    nextId = TemplatesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                if (WeithingFactsCrud != null)
                    nextId = WeithingFactsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                if (WorkshopsCrud != null)
                    nextId = WorkshopsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                if (PrintersCrud != null)
                    nextId = PrintersCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                if (PrinterResourcesCrud != null)
                    nextId = PrinterResourcesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                if (PrinterTypesCrud != null)
                    nextId = PrinterTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            return nextId;
        }

        private int ActionGetIdEntityForDwh<T>(int nextId) where T : BaseIdEntity, new()
        {
            if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                if (BrandCrud != null)
                    nextId = BrandCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.InformationSystemEntity))
            {
                if (InformationSystemCrud != null)
                    nextId = InformationSystemCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                if (NomenclatureCrud != null)
                    nextId = NomenclatureCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                if (NomenclatureGroupCrud != null)
                    nextId = NomenclatureGroupCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                if (NomenclatureLightCrud != null)
                    nextId = NomenclatureLightCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                if (NomenclatureTypeCrud != null)
                    nextId = NomenclatureTypeCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.StatusEntity))
            {
                if (StatusCrud != null)
                    nextId = StatusCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }

            return nextId;
        }

        public T ActionGetUidEntity<T>(BaseUidEntity entity, ShareEnums.DbTableAction tableAction) where T : BaseUidEntity, new()
        {
            T? result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => (T)entity,
                ShareEnums.DbTableAction.Copy => (T)((T)entity).Clone(),
                ShareEnums.DbTableAction.Delete => (T)entity,
                ShareEnums.DbTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                if (typeof(T) == typeof(TableSystemModels.AccessEntity))
                {
                    _ = AccessesCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.AppEntity))
                {
                    _ = AppsCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.LogEntity))
                {
                    _ = LogsCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.LogTypeEntity))
                {
                    _ = LogTypesCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(DataShareCore.DAL.DataModels.LogSummaryEntity))
                {
                    _ = LogSummaryCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(DataModels.WeithingFactSummaryEntity))
                {
                    //_ = WeithingFactSummaryCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                result.Uid = Guid.NewGuid();
            }
            return result;
        }

        public void ActionDeleteEntity<T>(T entity) where T : IBaseEntity
        {
            // SYSTEM.
            if (entity is TableSystemModels.AccessEntity accessEntity)
                AccessesCrud?.DeleteEntity(accessEntity);
            else if (entity is TableSystemModels.AppEntity appEntity)
                AppsCrud?.DeleteEntity(appEntity);
            else if (entity is TableSystemModels.HostEntity hostsEntity)
                HostsCrud?.DeleteEntity(hostsEntity);
            else if (entity is TableSystemModels.LogEntity logEntity)
                LogsCrud?.DeleteEntity(logEntity);
            else if (entity is TableSystemModels.LogTypeEntity logTypeEntity)
                LogTypesCrud?.DeleteEntity(logTypeEntity);
            // SCALES.
            else if (entity is TableScaleModels.LabelEntity labelsEntity)
                LabelsCrud?.DeleteEntity(labelsEntity);
            else if (entity is TableScaleModels.BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud?.DeleteEntity(barCodeTypesEntity);
            else if (entity is TableScaleModels.ContragentEntity contragentsEntity)
                ContragentsCrud?.DeleteEntity(contragentsEntity);
            else if (entity is DataShareCore.DAL.DataModels.LogSummaryEntity logSummaryEntity)
                LogSummaryCrud?.DeleteEntity(logSummaryEntity);
            else if (entity is TableScaleModels.NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud?.DeleteEntity(nomenclatureEntity);
            else if (entity is TableScaleModels.OrderEntity ordersEntity)
                OrdersCrud?.DeleteEntity(ordersEntity);
            else if (entity is TableScaleModels.OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud?.DeleteEntity(orderStatusEntity);
            else if (entity is TableScaleModels.OrderTypeEntity orderTypesEntity)
                OrderTypesCrud?.DeleteEntity(orderTypesEntity);
            else if (entity is TableScaleModels.PluEntity pluEntity)
                PlusCrud?.DeleteEntity(pluEntity);
            else if (entity is TableScaleModels.ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud?.DeleteEntity(productionFacilityEntity);
            else if (entity is TableScaleModels.ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud?.DeleteEntity(productSeriesEntity);
            else if (entity is TableScaleModels.ScaleEntity scalesEntity)
                ScalesCrud?.DeleteEntity(scalesEntity);
            else if (entity is TableScaleModels.TaskEntity taskEntity)
                TaskCrud?.DeleteEntity(taskEntity);
            else if (entity is TableScaleModels.TaskTypeEntity taskTypeEntity)
                TaskTypeCrud?.DeleteEntity(taskTypeEntity);
            else if (entity is TableScaleModels.TemplateEntity templatesEntity)
                TemplatesCrud?.DeleteEntity(templatesEntity);
            else if (entity is TableScaleModels.TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud?.DeleteEntity(templateResourcesEntity);
            else if (entity is TableScaleModels.WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud?.DeleteEntity(weithingFactEntity);
            else if (entity is DataModels.WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud?.DeleteEntity(weithingFactSummaryEntity);
            else if (entity is TableScaleModels.WorkshopEntity workshopEntity)
                WorkshopsCrud?.DeleteEntity(workshopEntity);
            else if (entity is TableScaleModels.PrinterEntity zebraPrinterEntity)
                PrintersCrud?.DeleteEntity(zebraPrinterEntity);
            else if (entity is TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud?.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud?.DeleteEntity(zebraPrinterResourceRefEntity);
            // DWH.
            else if (entity is TableDwhModels.BrandEntity brandEntity)
                BrandCrud?.DeleteEntity(brandEntity);
            else if (entity is TableDwhModels.InformationSystemEntity informationSystemEntity)
                InformationSystemCrud?.DeleteEntity(informationSystemEntity);
            else if (entity is TableDwhModels.NomenclatureEntity dwhNomenclatureEntity)
                NomenclatureCrud?.DeleteEntity(dwhNomenclatureEntity);
            else if (entity is TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity)
                NomenclatureGroupCrud?.DeleteEntity(nomenclatureGroupEntity);
            else if (entity is TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity)
                NomenclatureLightCrud?.DeleteEntity(nomenclatureLightEntity);
            else if (entity is TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity)
                NomenclatureTypeCrud?.DeleteEntity(nomenclatureTypeEntity);
            else if (entity is TableDwhModels.StatusEntity statusEntity)
                StatusCrud?.DeleteEntity(statusEntity);
        }

        public void ActionMarkedEntity<T>(T entity) where T : IBaseEntity
        {
            // SYSTEM.
            if (entity is TableSystemModels.AccessEntity accessEntity)
                AccessesCrud?.MarkedEntity(accessEntity);
            else if (entity is TableSystemModels.AppEntity appEntity)
                AppsCrud?.MarkedEntity(appEntity);
            else if (entity is TableSystemModels.HostEntity hostsEntity)
                HostsCrud?.MarkedEntity(hostsEntity);
            else if (entity is TableSystemModels.LogEntity logEntity)
                LogsCrud?.MarkedEntity(logEntity);
            else if (entity is TableSystemModels.LogTypeEntity logTypeEntity)
                LogTypesCrud?.MarkedEntity(logTypeEntity);
            // SCALES.
            else if (entity is TableScaleModels.BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud?.MarkedEntity(barCodeTypesEntity);
            else if (entity is TableScaleModels.ContragentEntity contragentsEntity)
                ContragentsCrud?.MarkedEntity(contragentsEntity);
            else if (entity is TableScaleModels.LabelEntity labelsEntity)
                LabelsCrud?.MarkedEntity(labelsEntity);
            else if (entity is DataShareCore.DAL.DataModels.LogSummaryEntity logSummaryEntity)
                LogSummaryCrud?.MarkedEntity(logSummaryEntity);
            else if (entity is TableScaleModels.NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud?.MarkedEntity(nomenclatureEntity);
            else if (entity is TableScaleModels.OrderEntity ordersEntity)
                OrdersCrud?.MarkedEntity(ordersEntity);
            else if (entity is TableScaleModels.OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud?.MarkedEntity(orderStatusEntity);
            else if (entity is TableScaleModels.OrderTypeEntity orderTypesEntity)
                OrderTypesCrud?.MarkedEntity(orderTypesEntity);
            else if (entity is TableScaleModels.PluEntity pluEntity)
                PlusCrud?.MarkedEntity(pluEntity);
            else if (entity is TableScaleModels.ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud?.MarkedEntity(productionFacilityEntity);
            else if (entity is TableScaleModels.ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud?.MarkedEntity(productSeriesEntity);
            else if (entity is TableScaleModels.ScaleEntity scalesEntity)
                ScalesCrud?.MarkedEntity(scalesEntity);
            else if (entity is TableScaleModels.TaskEntity taskEntity)
                TaskCrud?.MarkedEntity(taskEntity);
            else if (entity is TableScaleModels.TaskTypeEntity taskTypeEntity)
                TaskTypeCrud?.MarkedEntity(taskTypeEntity);
            else if (entity is TableScaleModels.TemplateEntity templatesEntity)
                TemplatesCrud?.MarkedEntity(templatesEntity);
            else if (entity is TableScaleModels.TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud?.MarkedEntity(templateResourcesEntity);
            else if (entity is TableScaleModels.WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud?.MarkedEntity(weithingFactEntity);
            else if (entity is DataModels.WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud?.MarkedEntity(weithingFactSummaryEntity);
            else if (entity is TableScaleModels.WorkshopEntity workshopEntity)
                WorkshopsCrud?.MarkedEntity(workshopEntity);
            else if (entity is TableScaleModels.PrinterEntity zebraPrinterEntity)
                PrintersCrud?.MarkedEntity(zebraPrinterEntity);
            else if (entity is TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud?.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud?.MarkedEntity(zebraPrinterResourceRefEntity);
            // DWH.
            else if (entity is TableDwhModels.BrandEntity brandEntity)
                BrandCrud?.MarkedEntity(brandEntity);
            else if (entity is TableDwhModels.InformationSystemEntity informationSystemEntity)
                InformationSystemCrud?.MarkedEntity(informationSystemEntity);
            else if (entity is TableDwhModels.NomenclatureEntity dwhNomenclatureEntity)
                NomenclatureCrud?.MarkedEntity(dwhNomenclatureEntity);
            else if (entity is TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity)
                NomenclatureGroupCrud?.MarkedEntity(nomenclatureGroupEntity);
            else if (entity is TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity)
                NomenclatureLightCrud?.MarkedEntity(nomenclatureLightEntity);
            else if (entity is TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity)
                NomenclatureTypeCrud?.MarkedEntity(nomenclatureTypeEntity);
            else if (entity is TableDwhModels.StatusEntity statusEntity)
                StatusCrud?.MarkedEntity(statusEntity);
        }

        #endregion
    }
}
