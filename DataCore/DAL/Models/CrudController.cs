// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.DataModels;
using DataCore.DAL.TableDwhModels;
using DataCore.DAL.TableScaleModels;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataCore.DAL.Models
{
    public class CrudController
    {
        #region Public and private fields and properties

        public DataAccessEntity DataAccess { get; private set; }
        public DataConfigurationEntity DataConfig { get; private set; }
        public ISessionFactory? SessionFactory { get; private set; }
        private delegate void ExecCallback(ISession session);

        #endregion

        #region Constructor and destructor

        public CrudController(DataAccessEntity dataAccess, ISessionFactory? sessionFactory)
        {
            DataAccess = dataAccess;
            DataConfig = new DataConfigurationEntity();
            SessionFactory = sessionFactory;
        }

        #endregion

        #region Public and private methods

        public ISession? GetSession() => SessionFactory?.OpenSession();

        public void LogExceptionToSql(Exception ex, string filePath, int lineNumber, string memberName)
        {
            int idLast = GetEntity<ErrorEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            ErrorEntity error = new()
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
            ExecuteTransaction((session) => { session.Save(error); }, filePath, lineNumber, memberName, true);
        }

        public T[] GetEntitiesWithConfig<T>(string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            T[]? result = new T[0];
            ExecuteTransaction((session) =>
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
            }, filePath, lineNumber, memberName);
            return result;
        }

        private ICriteria GetCriteria<T>(ISession session, FieldListEntity? fieldList, FieldOrderEntity? order, int maxResults)
            where T : BaseEntity, new()
        {
            Type type = typeof(T);
            ICriteria criteria = session.CreateCriteria(type);
            if (maxResults > 0)
            {
                criteria.SetMaxResults(maxResults);
            }
            if (fieldList is { Use: true, Fields: { } })
            {
                AbstractCriterion fieldsWhere = Restrictions.AllEq(fieldList.Fields);
                criteria.Add(fieldsWhere);
            }
            if (order != null && order is { Use: true })
            {
                Order fieldOrder = order.Direction == ShareEnums.DbOrderDirection.Asc
                    ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        private void ExecuteTransaction(ExecCallback callback, string filePath, int lineNumber, string memberName, bool isException = false)
        {
            using ISession? session = GetSession();
            Exception? exception = null;
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
                    exception = ex;
                    //throw;
                }
                finally
                {
                    session.Disconnect();
                }
            }
            if (!isException && exception != null)
            {
                LogExceptionToSql(exception, filePath, lineNumber, memberName);
            }
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

        public T[]? GetEntitiesWithoutReferences<T>(FieldListEntity fieldList, FieldOrderEntity? order, int maxResults,
            string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            T[]? result = new T[0];
            ExecuteTransaction((session) =>
            {
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
        //            if (items != null)
        //            {
        //                var listEntities = items.List<T>();
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

        #endregion

        #region Public and private methods

        public void FillReferences<T>(T item) where T : BaseEntity, new()
        {
            FillReferencesSystem(item);
            FillReferencesDatas(item);
            FillReferencesScales(item);
            FillReferencesDwh(item);
        }

        private void FillReferencesSystem<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case AccessEntity access:
                    if (!access.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case AppEntity app:
                    if (!app.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case ErrorEntity error:
                    if (!error.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case HostEntity host:
                    if (!host.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case LogEntity log:
                    if (!log.EqualsEmpty())
                    {
                        if (log.App != null)
                            log.App = GetEntity<AppEntity>(log.App.Uid);
                        if (log.Host != null)
                            log.Host = GetEntity<HostEntity>(log.Host.Id);
                        if (log.LogType != null)
                            log.LogType = GetEntity<LogTypeEntity>(log.LogType.Uid);
                    }
                    break;
                case LogTypeEntity logType:
                    if (!logType.EqualsEmpty())
                    {
                        //
                    }
                    break;
            }
        }

        private void FillReferencesDatas<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case DeviceEntity device:
                    if (!device.EqualsEmpty())
                    {
                        if (device.Scales != null)
                            device.Scales = GetEntity<ScaleEntity>(device.Scales.Id);
                    }
                    break;
            }
        }

        private void FillReferencesScales<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case BarcodeTypeEntity barcodeType:
                    {
                        if (!barcodeType.EqualsEmpty())
                        {
                            //
                        }
                        break;
                    }
                case ContragentEntity contragent:
                    {
                        if (!contragent.EqualsEmpty())
                        {
                            //
                        }
                        break;
                    }
                case LabelEntity label:
                    if (!label.EqualsEmpty())
                    {
                        if (label.WeithingFact != null)
                            label.WeithingFact = GetEntity<WeithingFactEntity>(label.WeithingFact.Id);
                    }
                    break;
                case TableScaleModels.NomenclatureEntity nomenclature:
                    if (!nomenclature.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case OrderEntity order:
                    if (!order.EqualsEmpty())
                    {
                        if (order.OrderTypes != null)
                            order.OrderTypes = GetEntity<OrderTypeEntity>(order.OrderTypes.Id);
                        if (order.Scales != null)
                            order.Scales = GetEntity<ScaleEntity>(order.Scales.Id);
                        if (order.Plu != null)
                            order.Plu = GetEntity<PluEntity>(order.Plu.Id);
                        if (order.Templates != null)
                            order.Templates = GetEntity<TemplateEntity>(order.Templates.Id);
                    }
                    break;
                case OrderStatusEntity orderStatus:
                    if (!orderStatus.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case OrderTypeEntity orderType:
                    if (!orderType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case OrganizationEntity organization:
                    if (!organization.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case PluEntity plu:
                    if (!plu.EqualsEmpty())
                    {
                        if (plu.Templates != null)
                            plu.Templates = GetEntity<TemplateEntity>(plu.Templates.Id);
                        if (plu.Scale != null)
                            plu.Scale = GetEntity<ScaleEntity>(plu.Scale.Id);
                        if (plu.Nomenclature != null)
                            plu.Nomenclature = GetEntity<TableScaleModels.NomenclatureEntity>(plu.Nomenclature.Id);
                    }
                    break;
                case PrinterEntity printer:
                    if (!printer.EqualsEmpty())
                    {
                        if (printer.PrinterType != null)
                            printer.PrinterType = GetEntity<PrinterTypeEntity>(printer.PrinterType.Id);
                    }
                    break;
                case PrinterResourceEntity printerResource:
                    if (!printerResource.EqualsEmpty())
                    {
                        if (printerResource.Printer != null)
                            printerResource.Printer = GetEntity<PrinterEntity>(printerResource.Printer.Id);
                        if (printerResource.Resource != null)
                            printerResource.Resource = GetEntity<TemplateResourceEntity>(printerResource.Resource.Id);
                    }
                    break;
                case PrinterTypeEntity printerType:
                    if (!printerType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case ProductionFacilityEntity ProductionFacility:
                    if (!ProductionFacility.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case ProductSeriesEntity product:
                    if (!product.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case ScaleEntity scale:
                    if (!scale.EqualsEmpty())
                    {
                        if (scale.TemplateDefault != null)
                            scale.TemplateDefault = GetEntity<TemplateEntity>(scale.TemplateDefault.Id);
                        if (scale.TemplateSeries != null)
                            scale.TemplateSeries = GetEntity<TemplateEntity>(scale.TemplateSeries.Id);
                        if (scale.WorkShop != null)
                            scale.WorkShop = GetEntity<WorkshopEntity>(scale.WorkShop.Id);
                        if (scale.Printer != null)
                            scale.Printer = GetEntity<PrinterEntity>(scale.Printer.Id);
                        if (scale.Host != null)
                            scale.Host = GetEntity<HostEntity>(scale.Host.Id);
                    }
                    break;
                case TaskEntity task:
                    if (!task.EqualsEmpty())
                    {
                        if (task.TaskType != null)
                            task.TaskType = GetEntity<TaskTypeEntity>(task.TaskType.Uid);
                        if (task.Scale != null)
                            task.Scale = GetEntity<ScaleEntity>(task.Scale.Id);
                    }
                    break;
                case TaskTypeEntity taskType:
                    if (!taskType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TemplateEntity template:
                    if (!template.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TemplateResourceEntity templateResource:
                    if (!templateResource.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case WeithingFactEntity weithingFact:
                    if (!weithingFact.EqualsEmpty())
                    {
                        if (weithingFact.Plu != null)
                            weithingFact.Plu = GetEntity<PluEntity>(weithingFact.Plu.Id);
                        if (weithingFact.Scales != null)
                            weithingFact.Scales = GetEntity<ScaleEntity>(weithingFact.Scales.Id);
                        if (weithingFact.Series != null)
                            weithingFact.Series = GetEntity<ProductSeriesEntity>(weithingFact.Series.Id);
                        if (weithingFact.Orders != null)
                            weithingFact.Orders = GetEntity<OrderEntity>(weithingFact.Orders.Id);
                    }
                    break;
                case WorkshopEntity workshop:
                    if (!workshop.EqualsEmpty())
                    {
                        if (workshop.ProductionFacility != null)
                            workshop.ProductionFacility = GetEntity<ProductionFacilityEntity>(workshop.ProductionFacility.Id);
                    }
                    break;
            }
        }

        private void FillReferencesDwh<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case BrandEntity brand:
                    if (!brand.EqualsEmpty())
                    {
                        if (brand.InformationSystem != null)
                            brand.InformationSystem = GetEntity<InformationSystemEntity>(brand.InformationSystem.Id);
                    }
                    break;
                case InformationSystemEntity informationSystem:
                    if (!informationSystem.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableDwhModels.NomenclatureEntity nomenclature:
                    if (!nomenclature.EqualsEmpty())
                    {
                        //if (nomenclatureEntity.BrandBytes != null && nomenclatureEntity.BrandBytes.Length > 0)
                        //    nomenclatureEntity.Brand = DataAccess.BrandCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.BrandBytes);
                        //if (nomenclatureEntity.InformationSystem != null)
                        //    nomenclatureEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureEntity.InformationSystem.Id);
                        //if (nomenclatureEntity.NomenclatureGroupCostBytes != null && nomenclatureEntity.NomenclatureGroupCostBytes.Length > 0)
                        //    nomenclatureEntity.NomenclatureGroupCost = DataAccess.NomenclatureGroupCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupCostBytes);
                        //if (nomenclatureEntity.NomenclatureGroupBytes != null && nomenclatureEntity.NomenclatureGroupBytes.Length > 0)
                        //    nomenclatureEntity.NomenclatureGroup = DataAccess.NomenclatureGroupCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupBytes);
                        //if (nomenclatureEntity.NomenclatureTypeBytes != null && nomenclatureEntity.NomenclatureTypeBytes.Length > 0)
                        //    nomenclatureEntity.NomenclatureType = DataAccess.NomenclatureTypeCrud.GetEntity(ShareEnums.DbField.CodeInIs, nomenclatureEntity.NomenclatureTypeBytes);
                        if (nomenclature.Status != null)
                            nomenclature.Status = GetEntity<StatusEntity>(nomenclature.Status.Id);
                    }
                    break;
                case NomenclatureGroupEntity nomenclatureGroup:
                    if (!nomenclatureGroup.EqualsEmpty())
                    {
                        if (nomenclatureGroup.InformationSystem != null)
                            nomenclatureGroup.InformationSystem = GetEntity<InformationSystemEntity>(nomenclatureGroup.InformationSystem.Id);
                    }
                    break;
                case NomenclatureLightEntity nomenclatureLight:
                    if (!nomenclatureLight.EqualsEmpty())
                    {
                        if (nomenclatureLight.InformationSystem != null)
                            nomenclatureLight.InformationSystem = GetEntity<InformationSystemEntity>(nomenclatureLight.InformationSystem.Id);
                    }
                    break;
                case NomenclatureParentEntity:
                    //
                    break;
                case NomenclatureTypeEntity nomenclatureType:
                    if (!nomenclatureType.EqualsEmpty())
                    {
                        if (nomenclatureType.InformationSystem != null)
                            nomenclatureType.InformationSystem = GetEntity<InformationSystemEntity>(nomenclatureType.InformationSystem.Id);
                    }
                    break;
                case StatusEntity status:
                    if (!status.EqualsEmpty())
                    {
                        //
                    }
                    break;
            }
        }

        public T GetEntity<T>(FieldListEntity? fieldList, FieldOrderEntity? order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            T? item = new();
            ExecuteTransaction((session) =>
            {
                ICriteria criteria = GetCriteria<T>(session, fieldList, order, 1);
                IList<T>? list = criteria?.List<T>();
                item = list == null ? new T() : list.FirstOrDefault() ?? new T();
            }, filePath, lineNumber, memberName);
            FillReferences(item);
            return item;
        }

        public T GetEntity<T>(int id) where T : BaseEntity, new()
        {
            return GetEntity<T>(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }),
                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
        }

        public T GetEntity<T>(Guid uid) where T : BaseEntity, new()
        {
            return GetEntity<T>(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Uid.ToString(), uid } }),
                new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc));
        }

        public T[]? GetEntities<T>(FieldListEntity fieldList, FieldOrderEntity order, int maxResults = 0,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            T[]? items = GetEntitiesWithoutReferences<T>(fieldList, order, maxResults, filePath, lineNumber, memberName);
            if (items != null)
            {
                foreach (T item in items)
                {
                    FillReferences(item);
                }
            }
            return items;
        }

        //public T[] GetEntitiesNative(string[] fieldsSelect, string from, object[] valuesParams,
        //    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        //{
        //    return DataAccess.GetEntitiesNative<T>(fieldsSelect, from, valuesParams, filePath, lineNumber, memberName);
        //}

        public T[] GetEntitiesNativeMappingInside<T>(string query, string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            T[]? result = new T[0];
            ExecuteTransaction((session) =>
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
            }, filePath, lineNumber, memberName);
            return result;
        }

        public T[] GetEntitiesNativeMapping<T>(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
            => GetEntitiesNativeMappingInside<T>(query, filePath, lineNumber, memberName);

        public object[] GetEntitiesNativeObject(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? result = new object[0];
            ExecuteTransaction((session) =>
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
            }, filePath, lineNumber, memberName);
            return result;
        }

        public int ExecQueryNativeInside(string query, Dictionary<string, object> parameters, string filePath, int lineNumber, string memberName)
        {
            int result = 0;
            ExecuteTransaction((session) =>
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
            }, filePath, lineNumber, memberName);
            return result;
        }

        public int ExecQueryNative(string query, Dictionary<string, object> parameters,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            => ExecQueryNativeInside(query, parameters, filePath, lineNumber, memberName);

        public void SaveEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;

            //ExecuteTransaction((session) => {
            //    int idLast = GetEntity<HostEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            //    HostEntity foo = new()
            //    {
            //        Id = idLast + 1,
            //        CreateDate = DateTime.Now,
            //        ModifiedDate = DateTime.Now,
            //        Name = "Тест",
            //        Ip = "127.0.0.1",
            //        MacAddress = new MacAddressEntity(),
            //        IdRRef = Guid.NewGuid(),
            //        Marked = false,
            //        //SettingsFile = Convert.ToString(ent[8]),
            //    };
            //    Console.WriteLine(foo);
            //    session.Save(foo);
            //}, filePath, lineNumber, memberName);

            switch (item)
            {
                case BarcodeTypeEntity barcodeType:
                    ExecuteTransaction((session) =>
                    {
                        Console.WriteLine(barcodeType);
                        session.Save(barcodeType);
                    }, filePath, lineNumber, memberName);
                    break;
                case ContragentEntity contragent:
                    Console.WriteLine(contragent);
                    throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                case HostEntity host:
                    ExecuteTransaction((session) =>
                    {
                        Console.WriteLine(host);
                        session.Save(host);
                    }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.NomenclatureEntity nomenclature:
                    Console.WriteLine(nomenclature);
                    throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                case PrinterTypeEntity printerType:
                    ExecuteTransaction((session) =>
                    {
                        Console.WriteLine(printerType);
                        session.Save(printerType);
                    }, filePath, lineNumber, memberName);
                    break;
            }
        }

        public void UpdateEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;

            switch (item)
            {
                case AccessEntity access:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(access); }, filePath, lineNumber, memberName);
                    break;
                case AppEntity app:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(app); }, filePath, lineNumber, memberName);
                    break;
                case ErrorEntity error:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(error); }, filePath, lineNumber, memberName);
                    break;
                case HostEntity host:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(host); }, filePath, lineNumber, memberName);
                    host.ChangeDt = DateTime.Now;
                    break;
                case LogEntity log:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(log); }, filePath, lineNumber, memberName);
                    break;
                case LogTypeEntity logType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(logType); }, filePath, lineNumber, memberName);
                    break;
                case BarcodeTypeEntity barcodeType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(barcodeType); }, filePath, lineNumber, memberName);
                    break;
                case ContragentEntity contragent:
                    contragent.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(contragent); }, filePath, lineNumber, memberName);
                    break;
                case LabelEntity label:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(label); }, filePath, lineNumber, memberName);
                    break;
                case OrderEntity order:
                    order.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(order); }, filePath, lineNumber, memberName);
                    break;
                case OrderStatusEntity orderStatus:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(orderStatus); }, filePath, lineNumber, memberName);
                    break;
                case OrderTypeEntity orderType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(orderType); }, filePath, lineNumber, memberName);
                    break;
                case PluEntity plu:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(plu); }, filePath, lineNumber, memberName);
                    break;
                case ProductionFacilityEntity productionFacility:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(productionFacility); }, filePath, lineNumber, memberName);
                    break;
                case ProductSeriesEntity productSeries:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(productSeries); }, filePath, lineNumber, memberName);
                    break;
                case ScaleEntity scale:
                    scale.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(scale); }, filePath, lineNumber, memberName);
                    break;
                case TemplateEntity template:
                    template.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(template); }, filePath, lineNumber, memberName);
                    break;
                case TemplateResourceEntity templateResource:
                    templateResource.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(templateResource); }, filePath, lineNumber, memberName);
                    break;
                case WeithingFactEntity weithingFact:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(weithingFact); }, filePath, lineNumber, memberName);
                    break;
                case WorkshopEntity workshop:
                    workshop.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(workshop); }, filePath, lineNumber, memberName);
                    break;
                case PrinterEntity printer:
                    printer.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printer); }, filePath, lineNumber, memberName);
                    break;
                case PrinterResourceEntity printerResource:
                    printerResource.ModifiedDate = DateTime.Now;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printerResource); }, filePath, lineNumber, memberName);
                    break;
                case PrinterTypeEntity printerType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printerType); }, filePath, lineNumber, memberName);
                    break;
                case BrandEntity brand:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(brand); }, filePath, lineNumber, memberName);
                    break;
                case InformationSystemEntity informationSystem:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(informationSystem); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureEntity nomenclature:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclature); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureGroupEntity nomenclatureGroup:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureGroup); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureLightEntity nomenclatureLight:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureLight); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureTypeEntity nomenclatureType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureType); }, filePath, lineNumber, memberName);
                    break;
                case StatusEntity status:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(status); }, filePath, lineNumber, memberName);
                    break;
                default:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
                    break;
            }
        }

        public void DeleteEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;
            ExecuteTransaction((session) => { session.Delete(item); }, filePath, lineNumber, memberName);
        }

        public void MarkedEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;

            switch (item)
            {
                case AccessEntity access:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(access); }, filePath, lineNumber, memberName);
                    break;
                case AppEntity app:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(app); }, filePath, lineNumber, memberName);
                    break;
                case ErrorEntity error:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(error); }, filePath, lineNumber, memberName);
                    break;
                case HostEntity host:
                    host.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(host); }, filePath, lineNumber, memberName);
                    break;
                case LogEntity log:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(log); }, filePath, lineNumber, memberName);
                    break;
                case LogTypeEntity logType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(logType); }, filePath, lineNumber, memberName);
                    break;
                case BarcodeTypeEntity barcodeType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(barcodeType); }, filePath, lineNumber, memberName);
                    break;
                case ContragentEntity contragent:
                    contragent.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(contragent); }, filePath, lineNumber, memberName);
                    break;
                case LabelEntity label:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(label); }, filePath, lineNumber, memberName);
                    break;
                case OrderEntity order:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(order); }, filePath, lineNumber, memberName);
                    break;
                case OrderStatusEntity orderStatus:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(orderStatus); }, filePath, lineNumber, memberName);
                    break;
                case OrderTypeEntity orderType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(orderType); }, filePath, lineNumber, memberName);
                    break;
                case PluEntity plu:
                    plu.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(plu); }, filePath, lineNumber, memberName);
                    break;
                case ProductionFacilityEntity productionFacility:
                    productionFacility.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(productionFacility); }, filePath, lineNumber, memberName);
                    break;
                case ProductSeriesEntity productSeries:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(productSeries); }, filePath, lineNumber, memberName);
                    break;
                case ScaleEntity scale:
                    scale.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(scale); }, filePath, lineNumber, memberName);
                    break;
                case TemplateResourceEntity templateResource:
                    templateResource.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(templateResource); }, filePath, lineNumber, memberName);
                    break;
                case TemplateEntity template:
                    template.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(template); }, filePath, lineNumber, memberName);
                    break;
                case WeithingFactEntity weithingFact:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(weithingFact); }, filePath, lineNumber, memberName);
                    break;
                case WorkshopEntity workshop:
                    workshop.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(workshop); }, filePath, lineNumber, memberName);
                    break;
                case PrinterEntity printer:
                    printer.Marked = true;
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printer); }, filePath, lineNumber, memberName);
                    break;
                case PrinterResourceEntity printerResource:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printerResource); }, filePath, lineNumber, memberName);
                    break;
                case PrinterTypeEntity printerType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(printerType); }, filePath, lineNumber, memberName);
                    break;
                case BrandEntity brand:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(brand); }, filePath, lineNumber, memberName);
                    break;
                case InformationSystemEntity informationSystem:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(informationSystem); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureEntity nomenclature:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclature); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureGroupEntity nomenclatureGroup:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureGroup); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureLightEntity nomenclatureLight:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureLight); }, filePath, lineNumber, memberName);
                    break;
                case NomenclatureTypeEntity nomenclatureType:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(nomenclatureType); }, filePath, lineNumber, memberName);
                    break;
                case StatusEntity status:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(status); }, filePath, lineNumber, memberName);
                    break;
                default:
                    ExecuteTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
                    break;
            }
        }

        public bool ExistsEntityInside<T>(T item, string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            bool result = false;
            ExecuteTransaction((session) =>
            {
                result = session.Query<T>().Any(x => x.IsAny(item));
            }, filePath, lineNumber, memberName);
            return result;
        }

        public bool ExistsEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return false;
            //return DataAccess.ExistsEntity(item, filePath, lineNumber, memberName);
            return ExistsEntityInside(item, filePath, lineNumber, memberName);
        }

        public bool ExistsEntityInside<T>(FieldListEntity fieldList, FieldOrderEntity? order,
            string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            bool result = false;
            ExecuteTransaction((session) =>
            {
                result = GetCriteria<T>(session, fieldList, order, 1).List<T>().Count > 0;
            }, filePath, lineNumber, memberName);
            return result;
        }

        public bool ExistsEntity<T>(FieldListEntity fieldList, FieldOrderEntity? order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
            where T : BaseEntity, new()
        {
            //return DataAccess.ExistsEntity<T>(fieldList, order, filePath, lineNumber, memberName);
            return ExistsEntityInside<T>(fieldList, order, filePath, lineNumber, memberName);
        }

        #endregion

        #region Public and private methods - HostEntity

        public List<HostEntity> GetFreeHosts(int? id, bool? isMarked,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts, filePath, lineNumber, memberName);
            List<HostEntity>? items = new();
            foreach (object? entity in entities)
            {
                if (entity is object[] { Length: 10 } ent)
                {
                    HostEntity host = new()
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDt = Convert.ToDateTime(ent[1]),
                        ChangeDt = Convert.ToDateTime(ent[2]),
                        AccessDt = Convert.ToDateTime(ent[3]),
                        Name = Convert.ToString(ent[4]),
                        Ip = Convert.ToString(ent[5]),
                        MacAddress = new MacAddressEntity(Convert.ToString(ent[6])),
                        IdRRef = Guid.Parse(Convert.ToString(ent[7])),
                        Marked = Convert.ToBoolean(ent[8]),
                        SettingsFile = Convert.ToString(ent[9]),
                    };
                    if ((id == null || Equals(host.Id, id)) && (isMarked == null || Equals(host.Marked, isMarked)))
                        items.Add(host);
                }
            }
            return items;
        }

        public List<HostEntity> GetBusyHosts(int? id, bool? isMarked,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts, filePath, lineNumber, memberName);
            List<HostEntity>? items = new();
            foreach (object? entity in entities)
            {
                if (entity is object[] { Length: 12 } ent)
                {
                    HostEntity host = new()
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDt = Convert.ToDateTime(ent[1]),
                        ChangeDt = Convert.ToDateTime(ent[2]),
                        AccessDt = Convert.ToDateTime(ent[3]),
                        Name = Convert.ToString(ent[4]),
                        Ip = Convert.ToString(ent[7]),
                        MacAddress = new MacAddressEntity(Convert.ToString(ent[8])),
                        IdRRef = Guid.Parse(Convert.ToString(ent[9])),
                        Marked = Convert.ToBoolean(ent[10]),
                        SettingsFile = Convert.ToString(ent[11]),
                    };
                    if ((id == null || Equals(host.Id, id)) && (isMarked == null || Equals(host.Marked, isMarked)))
                        items.Add(host);
                }
            }
            return items;
        }

        #endregion
    }
}
