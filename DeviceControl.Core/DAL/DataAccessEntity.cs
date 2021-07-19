using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.DAL.TableModels;
using DeviceControl.Core.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceControl.Core.DAL
{
    public class DataAccessEntity
    {
        #region Public and private fields and properties
        
        public CoreSettingsEntity AppSettings { get; set; }
        public DataConfigurationEntity DataConfig { get; set; }

        private ISessionFactory _sessionFactory;
        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                    return _sessionFactory;
                if (AppSettings == null)
                    throw new ArgumentException("AppSettings is null!");
                if (_sessionFactory == null)
                {
                    if (AppSettings.Trusted)
                    {
                        FluentConfiguration configuration = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(AppSettings.Server)
                                .Database(AppSettings.Db)
                                .TrustedConnection()
                            ))
                            .Mappings(m => m.FluentMappings.Add<AppMap>())
                            .Mappings(m => m.FluentMappings.Add<BarCodeTypesMap>())
                            .Mappings(m => m.FluentMappings.Add<ContragentsMap>())
                            .Mappings(m => m.FluentMappings.Add<ErrorsMap>())
                            .Mappings(m => m.FluentMappings.Add<HostsMap>())
                            .Mappings(m => m.FluentMappings.Add<LabelsMap>())
                            .Mappings(m => m.FluentMappings.Add<LogMap>())
                            .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                            .Mappings(m => m.FluentMappings.Add<OrdersMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderTypesMap>())
                            .Mappings(m => m.FluentMappings.Add<PluMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductSeriesMap>())
                            .Mappings(m => m.FluentMappings.Add<ScalesMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterResourceRefMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateResourcesMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplatesMap>())
                            .Mappings(m => m.FluentMappings.Add<WeithingFactMap>())
                            .Mappings(m => m.FluentMappings.Add<WorkshopMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterResourceRefMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterTypeMap>());
                        configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(AppSettings.Username) || string.IsNullOrEmpty(AppSettings.Password))
                            throw new ArgumentException("AppSettings.Username or AppSettings.Password is null!");
                        FluentConfiguration configuration = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(AppSettings.Server)
                                .Database(AppSettings.Db)
                                .Username(AppSettings.Username)
                                .Password(AppSettings.Password)
                            ))
                            .Mappings(m => m.FluentMappings.Add<AppMap>())
                            .Mappings(m => m.FluentMappings.Add<BarCodeTypesMap>())
                            .Mappings(m => m.FluentMappings.Add<ContragentsMap>())
                            .Mappings(m => m.FluentMappings.Add<ErrorsMap>())
                            .Mappings(m => m.FluentMappings.Add<HostsMap>())
                            .Mappings(m => m.FluentMappings.Add<LabelsMap>())
                            .Mappings(m => m.FluentMappings.Add<LogMap>())
                            .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                            .Mappings(m => m.FluentMappings.Add<OrdersMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderTypesMap>())
                            .Mappings(m => m.FluentMappings.Add<PluMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductSeriesMap>())
                            .Mappings(m => m.FluentMappings.Add<ScalesMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterResourceRefMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateResourcesMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplatesMap>())
                            .Mappings(m => m.FluentMappings.Add<WeithingFactMap>())
                            .Mappings(m => m.FluentMappings.Add<WorkshopMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<ZebraPrinterResourceRefMap>());
                        configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                }
                return _sessionFactory;
            }
        }

        // Tables CRUD.
        public BaseCrud<AppEntity> AppCrud;
        public BaseCrud<BarCodeTypesEntity> BarCodeTypesCrud;
        public BaseCrud<ContragentsEntity> ContragentsCrud;
        public BaseCrud<ErrorsEntity> ErrorsCrud;
        public HostsCrud HostsCrud;
        public BaseCrud<LabelsEntity> LabelsCrud;
        public BaseCrud<LogEntity> LogCrud;
        public BaseCrud<NomenclatureEntity> NomenclatureCrud;
        public BaseCrud<ScalesEntity> ScalesCrud;
        public BaseCrud<OrdersEntity> OrdersCrud;
        public BaseCrud<OrderStatusEntity> OrderStatusCrud;
        public BaseCrud<OrderTypesEntity> OrderTypesCrud;
        public BaseCrud<PluEntity> PluCrud;
        public BaseCrud<ProductionFacilityEntity> ProductionFacilityCrud;
        public BaseCrud<ProductSeriesEntity> ProductSeriesCrud;
        public BaseCrud<TemplatesEntity> TemplatesCrud;
        public TemplateResourcesCrud TemplateResourcesCrud;
        public BaseCrud<ZebraPrinterResourceRefEntity> ZebraPrinterResourceRefCrud;
        public BaseCrud<WeithingFactEntity> WeithingFactCrud;
        public BaseCrud<WorkshopEntity> WorkshopCrud;
        public BaseCrud<ZebraPrinterTypeEntity> ZebraPrinterTypeCrud;
        public BaseCrud<ZebraPrinterEntity> ZebraPrinterCrud;
        // Datas CRUD.
        public BaseCrud<DeviceEntity> DeviceCrud;
        public BaseCrud<LogSummaryEntity> LogSummaryCrud;
        public BaseCrud<WeithingFactSummaryEntity> WeithingFactSummaryCrud;

        public bool IsDisabled => !GetSession().IsConnected;
        public bool IsOpen => GetSession().IsOpen;
        public bool IsConnected => GetSession().IsConnected;
        public bool IsDirty => GetSession().IsDirty();

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(CoreSettingsEntity appSettings)
        {
            Setup(appSettings);
        }

        public void Setup(CoreSettingsEntity appSettings)
        {
            AppSettings = appSettings;
            DataConfig = new DataConfigurationEntity();
            // Tables CRUD.
            AppCrud = new BaseCrud<AppEntity>(this);
            BarCodeTypesCrud = new BaseCrud<BarCodeTypesEntity>(this);
            ContragentsCrud = new BaseCrud<ContragentsEntity>(this);
            ErrorsCrud = new BaseCrud<ErrorsEntity>(this);
            HostsCrud = new HostsCrud(this);
            LabelsCrud = new BaseCrud<LabelsEntity>(this);
            LogCrud = new BaseCrud<LogEntity>(this);
            NomenclatureCrud = new BaseCrud<NomenclatureEntity>(this);
            ScalesCrud = new BaseCrud<ScalesEntity>(this);
            OrdersCrud = new BaseCrud<OrdersEntity>(this);
            OrderStatusCrud = new BaseCrud<OrderStatusEntity>(this);
            OrderTypesCrud = new BaseCrud<OrderTypesEntity>(this);
            PluCrud = new BaseCrud<PluEntity>(this);
            ProductionFacilityCrud = new BaseCrud<ProductionFacilityEntity>(this);
            ProductSeriesCrud = new BaseCrud<ProductSeriesEntity>(this);
            TemplatesCrud = new BaseCrud<TemplatesEntity>(this);
            TemplateResourcesCrud = new TemplateResourcesCrud(this);
            ZebraPrinterResourceRefCrud = new BaseCrud<ZebraPrinterResourceRefEntity>(this);
            WeithingFactCrud = new BaseCrud<WeithingFactEntity>(this);
            WorkshopCrud = new BaseCrud<WorkshopEntity>(this);
            ZebraPrinterTypeCrud = new BaseCrud<ZebraPrinterTypeEntity>(this);
            ZebraPrinterCrud = new BaseCrud<ZebraPrinterEntity>(this);
            // Datas CRUD.
            DeviceCrud = new BaseCrud<DeviceEntity>(this);
            LogSummaryCrud = new BaseCrud<LogSummaryEntity>(this);
            WeithingFactSummaryCrud = new BaseCrud<WeithingFactSummaryEntity>(this);
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
            var idLast = ErrorsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
            var error = new ErrorsEntity
            {
                Id = idLast + 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                FilePath = filePath,
                LineNumber = lineNumber,
                MemberName = memberName,
                Exception = ex.Message,
                InnerException = ex.InnerException?.Message,
            };
            ErrorsCrud.SaveEntity(error);
        }

        #endregion

        #region Public and private methods - CRUD share

        public T[] GetEntitiesWithConfig<T>(string filePath, int lineNumber, string memberName) where T : BaseEntity
        {
            var result = new T[0];
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
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
            return result;
        }

        private ICriteria GetCriteria<T>(ISession session, FieldListEntity fieldList, FieldOrderEntity order, int maxResults)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }
            if (fieldList is {Use: true, Fields: { }})
            {
                AbstractCriterion fieldsWhere = Restrictions.AllEq(fieldList.Fields);
                criteria.Add(fieldsWhere);
            }
            //if (order != null && order.Use)
            if (order is { Use: true })
            {
                Order fieldOrder = order.Direction == EnumOrderDirection.Asc ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        public T GetEntity<T>(FieldListEntity fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            var result = new T();
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    ICriteria criteria = GetCriteria<T>(session, fieldList, order, 1);
                    var list = criteria?.List<T>();
                    result = list?.FirstOrDefault() ?? new T();
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
            return result;
        }

        public string GetSqlStringFieldsSelect(string[] fieldsSelect)
        {
            var result = string.Empty;
            foreach (var field in fieldsSelect)
            {
                result += $"[{field}], ";
            }
            return result.Substring(0, result.Length - 2);
        }

        public string GetSqlStringValuesParams(object[] valuesParams)
        {
            var result = string.Empty;
            foreach (var value in valuesParams)
            {
                switch (value)
                {
                    case int _:
                    case decimal _:
                        result += $"{value}, ";
                        break;
                    default:
                        result += $"'{value}', ";
                        break;
                }
            }
            return result.Substring(0, result.Length - 2);
        }

        public ISQLQuery GetSqlQuery<T>(ISession session, string from, string[] fieldsSelect, object[] valuesParams)
        {
            if (string.IsNullOrEmpty(from) || fieldsSelect == null || fieldsSelect.Length == 0 ||
                valuesParams == null || valuesParams.Length == 0)
                return null;

            var sqlQuery = $"select {GetSqlStringFieldsSelect(fieldsSelect)} from {from} ({GetSqlStringValuesParams(valuesParams)})";
            var result = session.CreateSQLQuery(sqlQuery).AddEntity(typeof(T));
            return result;
        }

        public ISQLQuery GetSqlQuery(ISession session, string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            return session.CreateSQLQuery(query);
        }

        public T[] GetEntities<T>(FieldListEntity fieldList, FieldOrderEntity order, int maxResults, string filePath, int lineNumber, string memberName)
        {
            var result = new T[0];
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = GetCriteria<T>(session, fieldList, order, maxResults).List<T>().ToArray();
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
            var result = new T[0];
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    var sqlQuery = GetSqlQuery(session, query);
                    if (sqlQuery != null)
                    {
                        sqlQuery.AddEntity(typeof(T));
                        var listEntities = sqlQuery.List();
                        result = new T[listEntities.Count];
                        for (var i = 0; i < result.Length; i++)
                        {
                            result[i] = (T)listEntities[i];
                        }
                    }

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
            return result;
        }

        public object[] GetEntitiesNativeObject(string query, string filePath, int lineNumber, string memberName)
        {
            var result = new object[0];
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    var sqlQuery = GetSqlQuery(session, query);
                    if (sqlQuery != null)
                    {
                        var listEntities = sqlQuery.List();
                        result = new object[listEntities.Count];
                        for (var i = 0; i < result.Length; i++)
                        {
                            if (listEntities[i] is object[] records)
                                result[i] = records;
                            else 
                                result[i] = listEntities[i];
                        }
                    }

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
            return result;
        }

        public int ExecQueryNative(string query, Dictionary<string, object> parameters, string filePath, int lineNumber, string memberName)
        {
            var result = 0;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    var sqlQuery = GetSqlQuery(session, query);
                    if (sqlQuery != null && parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter.Value is byte[] imagedata)
                                sqlQuery.SetParameter(parameter.Key, imagedata);
                            else
                                sqlQuery.SetParameter(parameter.Key, parameter.Value);
                        }
                        result = sqlQuery.ExecuteUpdate();
                    }
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
            return result;
        }

        public void SaveEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : BaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    session.Save(entity);
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

        public void UpdateEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : BaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    session.SaveOrUpdate(entity);
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

        public void DeleteEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : BaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    session.Delete(entity);
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

        public bool ExistsEntity<T>(T entity, string filePath, int lineNumber, string memberName)
        {
            var result = false;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = session.Query<T>().Any(x => x.IsAny(entity));
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
            return result;
        }

        public bool ExistsEntity<T>(FieldListEntity fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName)
        {
            var result = false;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = GetCriteria<T>(session, fieldList, order, 1).List<T>().Count > 0;
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
            return result;
        }

        public T ActionGetIdEntity<T>(BaseIdEntity entity, EnumTableAction tableAction) where T : BaseIdEntity, new()
        {
            T result = tableAction switch
            {
                EnumTableAction.Add => new T(),
                EnumTableAction.Edit => (T)entity,
                EnumTableAction.Copy => (T)((T)entity).Clone(),
                EnumTableAction.Delete => (T)entity,
                EnumTableAction.Marked => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == EnumTableAction.Add || tableAction == EnumTableAction.Copy)
            {
                var nextId = 0;
                if (typeof(T) == typeof(BarCodeTypesEntity))
                {
                    nextId = BarCodeTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ContragentsEntity))
                {
                    nextId = ContragentsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(HostsEntity))
                {
                    nextId = HostsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(LabelsEntity))
                {
                    nextId = LabelsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(NomenclatureEntity))
                {
                    nextId = NomenclatureCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrdersEntity))
                {
                    nextId = OrdersCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrderStatusEntity))
                {
                    nextId = OrderStatusCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrderTypesEntity))
                {
                    nextId = OrderTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(PluEntity))
                {
                    nextId = PluCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ProductionFacilityEntity))
                {
                    nextId = ProductionFacilityCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ProductSeriesEntity))
                {
                    nextId = ProductSeriesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ScalesEntity))
                {
                    nextId = ScalesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(TemplateResourcesEntity))
                {
                    nextId = TemplateResourcesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(TemplatesEntity))
                {
                    nextId = TemplatesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(WeithingFactEntity))
                {
                    nextId = WeithingFactCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(WorkshopEntity))
                {
                    nextId = WorkshopCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ZebraPrinterEntity))
                {
                    nextId = ZebraPrinterCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ZebraPrinterResourceRefEntity))
                {
                    nextId = ZebraPrinterResourceRefCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ZebraPrinterTypeEntity))
                {
                    nextId = ZebraPrinterTypeCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                result.Id = nextId + 1;
            }
            return (T) result;
        }

        public T ActionGetUidEntity<T>(BaseUidEntity entity, EnumTableAction tableAction) where T : BaseUidEntity, new()
        {
            var result = tableAction switch
            {
                EnumTableAction.Add => new T(),
                EnumTableAction.Edit => (T)entity,
                EnumTableAction.Copy => (T)((T)entity).Clone(),
                EnumTableAction.Delete => (T)entity,
                EnumTableAction.Marked => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == EnumTableAction.Add || tableAction == EnumTableAction.Copy)
            {
                if (typeof(T) == typeof(AppEntity))
                {
                    _ = AppCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(LogEntity))
                {
                    _ = LogCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(LogSummaryEntity))
                {
                    _ = LogSummaryCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(WeithingFactSummaryEntity))
                {
                    //_ = WeithingFactSummaryCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                result.Uid = Guid.NewGuid();
            }
            return result;
        }

        public void ActionDeleteEntity<T>(T entity) where T : BaseEntity
        {
            if (entity is AppEntity appEntity)
                AppCrud.DeleteEntity(appEntity);
            else if (entity is BarCodeTypesEntity barCodeTypesEntity)
                BarCodeTypesCrud.DeleteEntity(barCodeTypesEntity);
            else if (entity is ContragentsEntity contragentsEntity)
                ContragentsCrud.DeleteEntity(contragentsEntity);
            else if (entity is HostsEntity hostsEntity)
                HostsCrud.DeleteEntity(hostsEntity);
            else if (entity is LabelsEntity labelsEntity)
                LabelsCrud.DeleteEntity(labelsEntity);
            else if (entity is LogEntity logEntity)
                LogCrud.DeleteEntity(logEntity);
            else if (entity is LogSummaryEntity logSummaryEntity)
                LogSummaryCrud.DeleteEntity(logSummaryEntity);
            else if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclatureCrud.DeleteEntity(nomenclatureEntity);
            else if (entity is OrdersEntity ordersEntity)
                OrdersCrud.DeleteEntity(ordersEntity);
            else if (entity is OrderStatusEntity orderStatusEntity)
                OrderStatusCrud.DeleteEntity(orderStatusEntity);
            else if (entity is OrderTypesEntity orderTypesEntity)
                OrderTypesCrud.DeleteEntity(orderTypesEntity);
            else if (entity is PluEntity pluEntity)
                PluCrud.DeleteEntity(pluEntity);
            else if (entity is ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilityCrud.DeleteEntity(productionFacilityEntity);
            else if (entity is ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud.DeleteEntity(productSeriesEntity);
            else if (entity is ScalesEntity scalesEntity)
                ScalesCrud.DeleteEntity(scalesEntity);
            else if (entity is TemplatesEntity templatesEntity)
                TemplatesCrud.DeleteEntity(templatesEntity);
            else if (entity is TemplateResourcesEntity templateResourcesEntity)
                TemplateResourcesCrud.DeleteEntity(templateResourcesEntity);
            else if (entity is WeithingFactEntity weithingFactEntity)
                WeithingFactCrud.DeleteEntity(weithingFactEntity);
            else if (entity is WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud.DeleteEntity(weithingFactSummaryEntity);
            else if (entity is WorkshopEntity workshopEntity)
                WorkshopCrud.DeleteEntity(workshopEntity);
            else if (entity is ZebraPrinterEntity zebraPrinterEntity)
                ZebraPrinterCrud.DeleteEntity(zebraPrinterEntity);
            else if (entity is ZebraPrinterTypeEntity zebraPrinterTypeEntity)
                ZebraPrinterTypeCrud.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                ZebraPrinterResourceRefCrud.DeleteEntity(zebraPrinterResourceRefEntity);
        }

        public void ActionMarkedEntity<T>(T entity) where T : BaseEntity
        {
            if (entity is AppEntity appEntity)
                AppCrud.MarkedEntity(appEntity);
            else if (entity is BarCodeTypesEntity barCodeTypesEntity)
                BarCodeTypesCrud.MarkedEntity(barCodeTypesEntity);
            else if (entity is ContragentsEntity contragentsEntity)
                ContragentsCrud.MarkedEntity(contragentsEntity);
            else if (entity is HostsEntity hostsEntity)
                HostsCrud.MarkedEntity(hostsEntity);
            else if (entity is LabelsEntity labelsEntity)
                LabelsCrud.MarkedEntity(labelsEntity);
            else if (entity is LogEntity logEntity)
                LogCrud.MarkedEntity(logEntity);
            else if (entity is LogSummaryEntity logSummaryEntity)
                LogSummaryCrud.MarkedEntity(logSummaryEntity);
            else if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclatureCrud.MarkedEntity(nomenclatureEntity);
            else if (entity is OrdersEntity ordersEntity)
                OrdersCrud.MarkedEntity(ordersEntity);
            else if (entity is OrderStatusEntity orderStatusEntity)
                OrderStatusCrud.MarkedEntity(orderStatusEntity);
            else if (entity is OrderTypesEntity orderTypesEntity)
                OrderTypesCrud.MarkedEntity(orderTypesEntity);
            else if (entity is PluEntity pluEntity)
                PluCrud.MarkedEntity(pluEntity);
            else if (entity is ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilityCrud.MarkedEntity(productionFacilityEntity);
            else if (entity is ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud.MarkedEntity(productSeriesEntity);
            else if (entity is ScalesEntity scalesEntity)
                ScalesCrud.MarkedEntity(scalesEntity);
            else if (entity is TemplatesEntity templatesEntity)
                TemplatesCrud.MarkedEntity(templatesEntity);
            else if (entity is TemplateResourcesEntity templateResourcesEntity)
                TemplateResourcesCrud.MarkedEntity(templateResourcesEntity);
            else if (entity is WeithingFactEntity weithingFactEntity)
                WeithingFactCrud.MarkedEntity(weithingFactEntity);
            else if (entity is WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud.MarkedEntity(weithingFactSummaryEntity);
            else if (entity is WorkshopEntity workshopEntity)
                WorkshopCrud.MarkedEntity(workshopEntity);
            else if (entity is ZebraPrinterEntity zebraPrinterEntity)
                ZebraPrinterCrud.MarkedEntity(zebraPrinterEntity);
            else if (entity is ZebraPrinterTypeEntity zebraPrinterTypeEntity)
                ZebraPrinterTypeCrud.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is ZebraPrinterResourceRefEntity zebraPrinterResourceRefEntity)
                ZebraPrinterResourceRefCrud.MarkedEntity(zebraPrinterResourceRefEntity);
        }

        #endregion
    }
}
