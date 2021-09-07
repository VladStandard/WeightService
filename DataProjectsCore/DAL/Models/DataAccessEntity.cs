// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.DataModels;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.DataModels;
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

        private ISessionFactory _sessionFactory;
        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                    return _sessionFactory;
                if (CoreSettings == null)
                    throw new ArgumentException("CoreSettings is null!");
                if (_sessionFactory == null)
                {
                    if (CoreSettings.Trusted)
                    {
                        FluentConfiguration configuration = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(CoreSettings.Server)
                                .Database(CoreSettings.Db)
                                .TrustedConnection()
                            ))
                            .Mappings(m => m.FluentMappings.Add<AccessMap>())
                            .Mappings(m => m.FluentMappings.Add<AppMap>())
                            .Mappings(m => m.FluentMappings.Add<BarcodeTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<ContragentMap>())
                            .Mappings(m => m.FluentMappings.Add<ErrorMap>())
                            .Mappings(m => m.FluentMappings.Add<HostMap>())
                            .Mappings(m => m.FluentMappings.Add<LabelMap>())
                            .Mappings(m => m.FluentMappings.Add<LogMap>())
                            .Mappings(m => m.FluentMappings.Add<LogTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<PluMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductSeriesMap>())
                            .Mappings(m => m.FluentMappings.Add<ScaleMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterResourceMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateResourceMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateMap>())
                            .Mappings(m => m.FluentMappings.Add<WeithingFactMap>())
                            .Mappings(m => m.FluentMappings.Add<WorkshopMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterResourceMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterTypeMap>());
                        configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(CoreSettings.Username) || string.IsNullOrEmpty(CoreSettings.Password))
                            throw new ArgumentException("CoreSettings.Username or CoreSettings.Password is null!");
                        FluentConfiguration configuration = Fluently.Configure()
                            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(CoreSettings.Server)
                                .Database(CoreSettings.Db)
                                .Username(CoreSettings.Username)
                                .Password(CoreSettings.Password)
                            ))
                            .Mappings(m => m.FluentMappings.Add<AccessMap>())
                            .Mappings(m => m.FluentMappings.Add<AppMap>())
                            .Mappings(m => m.FluentMappings.Add<BarcodeTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<ContragentMap>())
                            .Mappings(m => m.FluentMappings.Add<ErrorMap>())
                            .Mappings(m => m.FluentMappings.Add<HostMap>())
                            .Mappings(m => m.FluentMappings.Add<LabelMap>())
                            .Mappings(m => m.FluentMappings.Add<LogMap>())
                            .Mappings(m => m.FluentMappings.Add<LogTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderMap>())
                            .Mappings(m => m.FluentMappings.Add<OrderTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<PluMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductionFacilityMap>())
                            .Mappings(m => m.FluentMappings.Add<ProductSeriesMap>())
                            .Mappings(m => m.FluentMappings.Add<ScaleMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterResourceMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateResourceMap>())
                            .Mappings(m => m.FluentMappings.Add<TemplateMap>())
                            .Mappings(m => m.FluentMappings.Add<WeithingFactMap>())
                            .Mappings(m => m.FluentMappings.Add<WorkshopMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterTypeMap>())
                            .Mappings(m => m.FluentMappings.Add<PrinterResourceMap>());
                        configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                        _sessionFactory = configuration.BuildSessionFactory();
                    }
                }
                return _sessionFactory;
            }
        }

        // Tables CRUD.
        public BaseCrud<AccessEntity> AccessesCrud;
        public BaseCrud<AppEntity> AppsCrud;
        public BaseCrud<BarcodeTypeEntity> BarcodeTypesCrud;
        public BaseCrud<ContragentEntity> ContragentsCrud;
        public BaseCrud<ErrorEntity> ErrorsCrud;
        public BaseCrud<LabelEntity> LabelsCrud;
        public BaseCrud<LogEntity> LogsCrud;
        public BaseCrud<LogTypeEntity> LogTypesCrud;
        public BaseCrud<NomenclatureEntity> NomenclaturesCrud;
        public BaseCrud<OrderEntity> OrdersCrud;
        public BaseCrud<OrderStatusEntity> OrderStatusesCrud;
        public BaseCrud<OrderTypeEntity> OrderTypesCrud;
        public BaseCrud<PluEntity> PlusCrud;
        public BaseCrud<ProductSeriesEntity> ProductSeriesCrud;
        public BaseCrud<ProductionFacilityEntity> ProductionFacilitiesCrud;
        public BaseCrud<ScaleEntity> ScalesCrud;
        public BaseCrud<TemplateEntity> TemplatesCrud;
        public BaseCrud<WeithingFactEntity> WeithingFactsCrud;
        public BaseCrud<WorkshopEntity> WorkshopsCrud;
        public BaseCrud<PrinterEntity> PrintersCrud;
        public BaseCrud<PrinterResourceEntity> PrinterResourcesCrud;
        public BaseCrud<PrinterTypeEntity> PrinterTypesCrud;
        public HostCrud HostsCrud;
        public TemplateResourceCrud TemplateResourcesCrud;
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
            CoreSettings = appSettings;
            DataConfig = new DataConfigurationEntity();
            // Tables CRUD.
            AccessesCrud = new BaseCrud<AccessEntity>(this);
            AppsCrud = new BaseCrud<AppEntity>(this);
            BarcodeTypesCrud = new BaseCrud<BarcodeTypeEntity>(this);
            ContragentsCrud = new BaseCrud<ContragentEntity>(this);
            ErrorsCrud = new BaseCrud<ErrorEntity>(this);
            HostsCrud = new HostCrud(this);
            LabelsCrud = new BaseCrud<LabelEntity>(this);
            LogsCrud = new BaseCrud<LogEntity>(this);
            LogTypesCrud = new BaseCrud<LogTypeEntity>(this);
            NomenclaturesCrud = new BaseCrud<NomenclatureEntity>(this);
            ScalesCrud = new BaseCrud<ScaleEntity>(this);
            OrdersCrud = new BaseCrud<OrderEntity>(this);
            OrderStatusesCrud = new BaseCrud<OrderStatusEntity>(this);
            OrderTypesCrud = new BaseCrud<OrderTypeEntity>(this);
            PlusCrud = new BaseCrud<PluEntity>(this);
            ProductionFacilitiesCrud = new BaseCrud<ProductionFacilityEntity>(this);
            ProductSeriesCrud = new BaseCrud<ProductSeriesEntity>(this);
            TemplatesCrud = new BaseCrud<TemplateEntity>(this);
            TemplateResourcesCrud = new TemplateResourceCrud(this);
            PrinterResourcesCrud = new BaseCrud<PrinterResourceEntity>(this);
            WeithingFactsCrud = new BaseCrud<WeithingFactEntity>(this);
            WorkshopsCrud = new BaseCrud<WorkshopEntity>(this);
            PrinterTypesCrud = new BaseCrud<PrinterTypeEntity>(this);
            PrintersCrud = new BaseCrud<PrinterEntity>(this);
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
            int idLast = ErrorsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
            ErrorEntity? error = new ErrorEntity
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
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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
            //if (order != null && order.Use)
            if (order is { Use: true })
            {
                Order fieldOrder = order.Direction == EnumOrderDirection.Asc ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        public T GetEntity<T>(FieldListEntity? fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName) where T : IBaseEntity, new()
        {
            T? result = new T();
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
                try
                {
                    ICriteria criteria = GetCriteria<T>(session, fieldList, order, 1);
                    IList<T>? list = criteria?.List<T>();
                    result = list.FirstOrDefault() ?? new T();
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

        public ISQLQuery GetSqlQuery(ISession session, string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            return session.CreateSQLQuery(query);
        }

        public T[] GetEntities<T>(FieldListEntity fieldList, FieldOrderEntity order, int maxResults, string filePath, int lineNumber, string memberName)
        {
            T[]? result = new T[0];
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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
            T[]? result = new T[0];
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
                try
                {
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
            object[]? result = new object[0];
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
                try
                {
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
            int result = 0;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
                try
                {
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

        public void SaveEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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

        public void UpdateEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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

        public void DeleteEntity<T>(T entity, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (entity.EqualsEmpty()) return;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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
            bool result = false;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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
            bool result = false;
            using ISession? session = GetSession();
            if (session != null)
            {
                using ITransaction? transaction = session.BeginTransaction();
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
                EnumTableAction.New => new T(),
                EnumTableAction.Edit => (T)entity,
                EnumTableAction.Copy => (T)((T)entity).Clone(),
                EnumTableAction.Delete => (T)entity,
                EnumTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == EnumTableAction.New || tableAction == EnumTableAction.Copy)
            {
                int nextId = 0;
                if (typeof(T) == typeof(BarcodeTypeEntity))
                {
                    nextId = BarcodeTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ContragentEntity))
                {
                    nextId = ContragentsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(HostEntity))
                {
                    nextId = HostsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(LabelEntity))
                {
                    nextId = LabelsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(NomenclatureEntity))
                {
                    nextId = NomenclaturesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrderEntity))
                {
                    nextId = OrdersCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrderStatusEntity))
                {
                    nextId = OrderStatusesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(OrderTypeEntity))
                {
                    nextId = OrderTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(PluEntity))
                {
                    nextId = PlusCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ProductionFacilityEntity))
                {
                    nextId = ProductionFacilitiesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ProductSeriesEntity))
                {
                    nextId = ProductSeriesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(ScaleEntity))
                {
                    nextId = ScalesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(TemplateResourceEntity))
                {
                    nextId = TemplateResourcesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(TemplateEntity))
                {
                    nextId = TemplatesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(WeithingFactEntity))
                {
                    nextId = WeithingFactsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(WorkshopEntity))
                {
                    nextId = WorkshopsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(PrinterEntity))
                {
                    nextId = PrintersCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(PrinterResourceEntity))
                {
                    nextId = PrinterResourcesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                else if (typeof(T) == typeof(PrinterTypeEntity))
                {
                    nextId = PrinterTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                result.Id = nextId + 1;
            }
            return result;
        }

        public T ActionGetUidEntity<T>(BaseUidEntity entity, EnumTableAction tableAction) where T : BaseUidEntity, new()
        {
            T? result = tableAction switch
            {
                EnumTableAction.New => new T(),
                EnumTableAction.Edit => (T)entity,
                EnumTableAction.Copy => (T)((T)entity).Clone(),
                EnumTableAction.Delete => (T)entity,
                EnumTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == EnumTableAction.New || tableAction == EnumTableAction.Copy)
            {
                if (typeof(T) == typeof(AccessEntity))
                {
                    _ = AccessesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(AppEntity))
                {
                    _ = AppsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(LogEntity))
                {
                    _ = LogsCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(LogTypeEntity))
                {
                    _ = LogTypesCrud.GetEntity(null, new FieldOrderEntity(EnumField.Uid, EnumOrderDirection.Desc)).Uid;
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

        public void ActionDeleteEntity<T>(T entity) where T : IBaseEntity
        {
            if (entity is AccessEntity accessEntity)
                AccessesCrud.DeleteEntity(accessEntity);
            else if (entity is AppEntity appEntity)
                AppsCrud.DeleteEntity(appEntity);
            else if (entity is BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud.DeleteEntity(barCodeTypesEntity);
            else if (entity is ContragentEntity contragentsEntity)
                ContragentsCrud.DeleteEntity(contragentsEntity);
            else if (entity is HostEntity hostsEntity)
                HostsCrud.DeleteEntity(hostsEntity);
            else if (entity is LabelEntity labelsEntity)
                LabelsCrud.DeleteEntity(labelsEntity);
            else if (entity is LogEntity logEntity)
                LogsCrud.DeleteEntity(logEntity);
            else if (entity is LogTypeEntity logTypeEntity)
                LogTypesCrud.DeleteEntity(logTypeEntity);
            else if (entity is LogSummaryEntity logSummaryEntity)
                LogSummaryCrud.DeleteEntity(logSummaryEntity);
            else if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud.DeleteEntity(nomenclatureEntity);
            else if (entity is OrderEntity ordersEntity)
                OrdersCrud.DeleteEntity(ordersEntity);
            else if (entity is OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud.DeleteEntity(orderStatusEntity);
            else if (entity is OrderTypeEntity orderTypesEntity)
                OrderTypesCrud.DeleteEntity(orderTypesEntity);
            else if (entity is PluEntity pluEntity)
                PlusCrud.DeleteEntity(pluEntity);
            else if (entity is ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud.DeleteEntity(productionFacilityEntity);
            else if (entity is ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud.DeleteEntity(productSeriesEntity);
            else if (entity is ScaleEntity scalesEntity)
                ScalesCrud.DeleteEntity(scalesEntity);
            else if (entity is TemplateEntity templatesEntity)
                TemplatesCrud.DeleteEntity(templatesEntity);
            else if (entity is TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud.DeleteEntity(templateResourcesEntity);
            else if (entity is WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud.DeleteEntity(weithingFactEntity);
            else if (entity is WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud.DeleteEntity(weithingFactSummaryEntity);
            else if (entity is WorkshopEntity workshopEntity)
                WorkshopsCrud.DeleteEntity(workshopEntity);
            else if (entity is PrinterEntity zebraPrinterEntity)
                PrintersCrud.DeleteEntity(zebraPrinterEntity);
            else if (entity is PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud.DeleteEntity(zebraPrinterResourceRefEntity);
        }

        public void ActionMarkedEntity<T>(T entity) where T : IBaseEntity
        {
            if (entity is AccessEntity accessEntity)
                AccessesCrud.MarkedEntity(accessEntity);
            else if (entity is AppEntity appEntity)
                AppsCrud.MarkedEntity(appEntity);
            else if (entity is BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud.MarkedEntity(barCodeTypesEntity);
            else if (entity is ContragentEntity contragentsEntity)
                ContragentsCrud.MarkedEntity(contragentsEntity);
            else if (entity is HostEntity hostsEntity)
                HostsCrud.MarkedEntity(hostsEntity);
            else if (entity is LabelEntity labelsEntity)
                LabelsCrud.MarkedEntity(labelsEntity);
            else if (entity is LogEntity logEntity)
                LogsCrud.MarkedEntity(logEntity);
            else if (entity is LogTypeEntity logTypeEntity)
                LogTypesCrud.MarkedEntity(logTypeEntity);
            else if (entity is LogSummaryEntity logSummaryEntity)
                LogSummaryCrud.MarkedEntity(logSummaryEntity);
            else if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud.MarkedEntity(nomenclatureEntity);
            else if (entity is OrderEntity ordersEntity)
                OrdersCrud.MarkedEntity(ordersEntity);
            else if (entity is OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud.MarkedEntity(orderStatusEntity);
            else if (entity is OrderTypeEntity orderTypesEntity)
                OrderTypesCrud.MarkedEntity(orderTypesEntity);
            else if (entity is PluEntity pluEntity)
                PlusCrud.MarkedEntity(pluEntity);
            else if (entity is ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud.MarkedEntity(productionFacilityEntity);
            else if (entity is ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud.MarkedEntity(productSeriesEntity);
            else if (entity is ScaleEntity scalesEntity)
                ScalesCrud.MarkedEntity(scalesEntity);
            else if (entity is TemplateEntity templatesEntity)
                TemplatesCrud.MarkedEntity(templatesEntity);
            else if (entity is TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud.MarkedEntity(templateResourcesEntity);
            else if (entity is WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud.MarkedEntity(weithingFactEntity);
            else if (entity is WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud.MarkedEntity(weithingFactSummaryEntity);
            else if (entity is WorkshopEntity workshopEntity)
                WorkshopsCrud.MarkedEntity(workshopEntity);
            else if (entity is PrinterEntity zebraPrinterEntity)
                PrintersCrud.MarkedEntity(zebraPrinterEntity);
            else if (entity is PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud.MarkedEntity(zebraPrinterResourceRefEntity);
        }

        #endregion
    }
}
