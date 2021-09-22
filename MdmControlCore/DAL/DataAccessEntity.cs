// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using MdmControlCore.DAL.TableModels;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MdmControlCore.DAL
{
    public class DataAccessEntity
    {
        #region Public and private fields and properties
        
        public AppSettingsEntity AppSettings { get; set; }
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
                
                if (AppSettings.Trusted)
                {
                    FluentConfiguration configuration = Fluently.Configure()
                        .Database(
                            MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(AppSettings.Server)
                                .Database(AppSettings.Db)
                                .TrustedConnection()
                            )
                        .ShowSql()
                        .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()
                        )
                        .Mappings(m => m.FluentMappings.Add<BrandMap>())
                        .Mappings(m => m.FluentMappings.Add<InformationSystemMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureGroupMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureTypeMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureLightMap>())
                        .Mappings(m => m.FluentMappings.Add<StatusMap>());
                    configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                else
                {
                    if (string.IsNullOrEmpty(AppSettings.Username) || string.IsNullOrEmpty(AppSettings.Password))
                        throw new ArgumentException("AppSettings.Username or AppSettings.Password is null!");
                    FluentConfiguration configuration = Fluently.Configure()
                    .Database(
                            MsSqlConfiguration.MsSql2012.ConnectionString(x => x
                                .Server(AppSettings.Server)
                                .Database(AppSettings.Db)
                                .Username(AppSettings.Username)
                                .Password(AppSettings.Password)
                            )
                        .ShowSql()
                        .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()
                        )
                        .Mappings(m => m.FluentMappings.Add<BrandMap>())
                        .Mappings(m => m.FluentMappings.Add<InformationSystemMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureGroupMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureTypeMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureMap>())
                        .Mappings(m => m.FluentMappings.Add<NomenclatureLightMap>())
                        .Mappings(m => m.FluentMappings.Add<StatusMap>());
                    configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    _sessionFactory = configuration.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        // Tables CRUD.
        public BaseCrud<BrandEntity> BrandCrud;
        public BaseCrud<InformationSystemEntity> InformationSystemCrud;
        public BaseCrud<NomenclatureGroupEntity> NomenclatureGroupCrud;
        public BaseCrud<NomenclatureTypeEntity> NomenclatureTypeCrud;
        public BaseCrud<NomenclatureEntity> NomenclatureCrud;
        public BaseCrud<NomenclatureLightEntity> NomenclatureLightCrud;
        public BaseCrud<StatusEntity> StatusCrud;

        public bool IsDisabled => !GetSession().IsConnected;
        public bool IsOpen => GetSession().IsOpen;
        public bool IsConnected => GetSession().IsConnected;
        public bool IsDirty => GetSession().IsDirty();

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(AppSettingsEntity appSettings)
        {
            Setup(appSettings);
        }

        public void Setup(AppSettingsEntity appSettings)
        {
            AppSettings = appSettings;
            DataConfig = new DataConfigurationEntity();
            // Tables CRUD.
            BrandCrud = new BaseCrud<BrandEntity>(this);
            InformationSystemCrud = new BaseCrud<InformationSystemEntity>(this);
            NomenclatureGroupCrud = new BaseCrud<NomenclatureGroupEntity>(this);
            NomenclatureTypeCrud = new BaseCrud<NomenclatureTypeEntity>(this);
            NomenclatureCrud = new BaseCrud<NomenclatureEntity>(this);
            NomenclatureLightCrud = new BaseCrud<NomenclatureLightEntity>(this);
            StatusCrud = new BaseCrud<StatusEntity>(this);
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

        private ICriteria GetCriteria<T>(ISession session, FieldListEntity fieldList, FieldOrderEntity order)
        {
            var criteria = session.CreateCriteria(typeof(T));
            if (fieldList != null && fieldList.Use && fieldList.Fields != null)
            {
                var fieldsWhere = Restrictions.AllEq(fieldList.Fields);
                criteria.Add(fieldsWhere);
            }
            if (order != null && order.Use)
            {
                var fieldOrder = order.Direction == EnumOrderDirection.Asc ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        public T GetEntity<T>(FieldListEntity fieldList, FieldOrderEntity order, string filePath, int lineNumber, string memberName) 
            where T : BaseEntity, new()
        {
            var result = new T();
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = GetCriteria<T>(session, fieldList, order).SetMaxResults(1).List<T>().FirstOrDefault() ?? new T();
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

        public T[] GetEntities<T>(FieldListEntity fieldList, FieldOrderEntity order, int count, string filePath, int lineNumber, string memberName)
        {
            var result = new T[0];
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = count == 0
                        ? GetCriteria<T>(session, fieldList, order).List<T>().ToArray()
                        : GetCriteria<T>(session, fieldList, order).SetMaxResults(count).List<T>().ToArray();
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

        public IEnumerable<T> GetEntitiesAsList<T>(FieldListEntity fieldList, FieldOrderEntity order, int count, string filePath, int lineNumber, string memberName)
        {
            IEnumerable<T> result = null;
            using var session = GetSession();
            if (session != null)
            {
                using var transaction = session.BeginTransaction();
                try
                {
                    result = count == 0
                        ? GetCriteria<T>(session, fieldList, order).List<T>()
                        : GetCriteria<T>(session, fieldList, order).SetMaxResults(count).List<T>();
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
            return result?.ToList();
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

        public IEnumerable<T> GetEntitiesNativeMappingAsList<T>(string query, string filePath, int lineNumber, string memberName)
        {
            var result = new List<T>();
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
                        result.AddRange(sqlQuery.List().Cast<T>());
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
                            {
                                sqlQuery.SetParameter(parameter.Key, parameter.Value);
                            }
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
            return;
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
                    result = GetCriteria<T>(session, fieldList, order).SetMaxResults(1).List<T>().Count > 0;
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

        public T ActionGetEntity<T>(BaseEntity entity, EnumTableAction tableAction) where T : BaseEntity, new()
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
                var nextId = 0;
                if (typeof(T) == typeof(NomenclatureEntity))
                {
                    nextId = NomenclatureCrud.GetEntity(null, new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
                }
                result.Id = nextId + 1;
            }
            return result;
        }

        public void ActionDeleteEntity<T>(T entity) where T : BaseEntity
        {
            if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclatureCrud.DeleteEntity(nomenclatureEntity);
        }

        public void ActionMarkedEntity<T>(T entity) where T : BaseEntity
        {
            if (entity is NomenclatureEntity nomenclatureEntity)
                NomenclatureCrud.MarkedEntity(nomenclatureEntity);
        }

        #endregion
    }
}
