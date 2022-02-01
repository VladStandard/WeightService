// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Models;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataProjectsCore.DAL.Models
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
            int idLast = GetEntity<TableSystemModels.ErrorEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            TableSystemModels.ErrorEntity error = new()
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
            ExecTransaction((session) => { session.Save(error); }, filePath, lineNumber, memberName, true);
        }

        public T[] GetEntitiesWithConfig<T>(string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
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

        private void ExecTransaction(ExecCallback callback, string filePath, int lineNumber, string memberName, bool isException = false)
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
                case TableSystemModels.AccessEntity access:
                    if (!access.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableSystemModels.AppEntity app:
                    if (!app.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableSystemModels.ErrorEntity error:
                    if (!error.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableSystemModels.HostEntity host:
                    if (!host.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableSystemModels.LogEntity log:
                    if (!log.EqualsEmpty())
                    {
                        if (log.App != null)
                            log.App = GetEntity<TableSystemModels.AppEntity>(log.App.Uid);
                        if (log.Host != null)
                            log.Host = GetEntity<TableSystemModels.HostEntity>(log.Host.Id);
                        if (log.LogType != null)
                            log.LogType = GetEntity<TableSystemModels.LogTypeEntity>(log.LogType.Uid);
                    }
                    break;
                case TableSystemModels.LogTypeEntity logType:
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
                case DataModels.DeviceEntity device:
                    if (!device.EqualsEmpty())
                    {
                        if (device.Scales != null)
                            device.Scales = GetEntity<TableScaleModels.ScaleEntity>(device.Scales.Id);
                    }
                    break;
            }
        }

        private void FillReferencesScales<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case TableScaleModels.BarcodeTypeEntity barcodeType:
                    {
                        if (!barcodeType.EqualsEmpty())
                        {
                            //
                        }
                        break;
                    }
                case TableScaleModels.ContragentEntity contragent:
                    {
                        if (!contragent.EqualsEmpty())
                        {
                            //
                        }
                        break;
                    }
                case TableScaleModels.LabelEntity label:
                    if (!label.EqualsEmpty())
                    {
                        if (label.WeithingFact != null)
                            label.WeithingFact = GetEntity<TableScaleModels.WeithingFactEntity>(label.WeithingFact.Id);
                    }
                    break;
                case TableScaleModels.NomenclatureEntity nomenclature:
                    if (!nomenclature.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.OrderEntity order:
                    if (!order.EqualsEmpty())
                    {
                        if (order.OrderTypes != null)
                            order.OrderTypes = GetEntity<TableScaleModels.OrderTypeEntity>(order.OrderTypes.Id);
                        if (order.Scales != null)
                            order.Scales = GetEntity<TableScaleModels.ScaleEntity>(order.Scales.Id);
                        if (order.Plu != null)
                            order.Plu = GetEntity<TableScaleModels.PluEntity>(order.Plu.Id);
                        if (order.Templates != null)
                            order.Templates = GetEntity<TableScaleModels.TemplateEntity>(order.Templates.Id);
                    }
                    break;
                case TableScaleModels.OrderStatusEntity orderStatus:
                    if (!orderStatus.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.OrderTypeEntity orderType:
                    if (!orderType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.OrganizationEntity organization:
                    if (!organization.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.PluEntity plu:
                    if (!plu.EqualsEmpty())
                    {
                        if (plu.Templates != null)
                            plu.Templates = GetEntity<TableScaleModels.TemplateEntity>(plu.Templates.Id);
                        if (plu.Scale != null)
                            plu.Scale = GetEntity<TableScaleModels.ScaleEntity>(plu.Scale.Id);
                        if (plu.Nomenclature != null)
                            plu.Nomenclature = GetEntity<TableScaleModels.NomenclatureEntity>(plu.Nomenclature.Id);
                    }
                    break;
                case TableScaleModels.PrinterEntity printer:
                    if (!printer.EqualsEmpty())
                    {
                        if (printer.PrinterType != null)
                            printer.PrinterType = GetEntity<TableScaleModels.PrinterTypeEntity>(printer.PrinterType.Id);
                    }
                    break;
                case TableScaleModels.PrinterResourceEntity printerResource:
                    if (!printerResource.EqualsEmpty())
                    {
                        if (printerResource.Printer != null)
                            printerResource.Printer = GetEntity<TableScaleModels.PrinterEntity>(printerResource.Printer.Id);
                        if (printerResource.Resource != null)
                            printerResource.Resource = GetEntity<TableScaleModels.TemplateResourceEntity>(printerResource.Resource.Id);
                    }
                    break;
                case TableScaleModels.PrinterTypeEntity printerType:
                    if (!printerType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.ProductionFacilityEntity ProductionFacility:
                    if (!ProductionFacility.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.ProductSeriesEntity product:
                    if (!product.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.ScaleEntity scale:
                    if (!scale.EqualsEmpty())
                    {
                        if (scale.TemplateDefault != null)
                            scale.TemplateDefault = GetEntity<TableScaleModels.TemplateEntity>(scale.TemplateDefault.Id);
                        if (scale.TemplateSeries != null)
                            scale.TemplateSeries = GetEntity<TableScaleModels.TemplateEntity>(scale.TemplateSeries.Id);
                        if (scale.WorkShop != null)
                            scale.WorkShop = GetEntity<TableScaleModels.WorkshopEntity>(scale.WorkShop.Id);
                        if (scale.Printer != null)
                            scale.Printer = GetEntity<TableScaleModels.PrinterEntity>(scale.Printer.Id);
                        if (scale.Host != null)
                            scale.Host = GetEntity<TableSystemModels.HostEntity>(scale.Host.Id);
                    }
                    break;
                case TableScaleModels.TaskEntity task:
                    if (!task.EqualsEmpty())
                    {
                        if (task.TaskType != null)
                            task.TaskType = GetEntity<TableScaleModels.TaskTypeEntity>(task.TaskType.Uid);
                        if (task.Scale != null)
                            task.Scale = GetEntity<TableScaleModels.ScaleEntity>(task.Scale.Id);
                    }
                    break;
                case TableScaleModels.TaskTypeEntity taskType:
                    if (!taskType.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.TemplateEntity template:
                    if (!template.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.TemplateResourceEntity templateResource:
                    if (!templateResource.EqualsEmpty())
                    {
                        //
                    }
                    break;
                case TableScaleModels.WeithingFactEntity weithingFact:
                    if (!weithingFact.EqualsEmpty())
                    {
                        if (weithingFact.Plu != null)
                            weithingFact.Plu = GetEntity<TableScaleModels.PluEntity>(weithingFact.Plu.Id);
                        if (weithingFact.Scales != null)
                            weithingFact.Scales = GetEntity<TableScaleModels.ScaleEntity>(weithingFact.Scales.Id);
                        if (weithingFact.Series != null)
                            weithingFact.Series = GetEntity<TableScaleModels.ProductSeriesEntity>(weithingFact.Series.Id);
                        if (weithingFact.Orders != null)
                            weithingFact.Orders = GetEntity<TableScaleModels.OrderEntity>(weithingFact.Orders.Id);
                    }
                    break;
                case TableScaleModels.WorkshopEntity workshop:
                    if (!workshop.EqualsEmpty())
                    {
                        if (workshop.ProductionFacility != null)
                            workshop.ProductionFacility = GetEntity<TableScaleModels.ProductionFacilityEntity>(workshop.ProductionFacility.Id);
                    }
                    break;
            }
        }

        private void FillReferencesDwh<T>(T item) where T : BaseEntity, new()
        {
            switch (item)
            {
                case TableDwhModels.BrandEntity brand:
                    if (!brand.EqualsEmpty())
                    {
                        if (brand.InformationSystem != null)
                            brand.InformationSystem = GetEntity<TableDwhModels.InformationSystemEntity>(brand.InformationSystem.Id);
                    }
                    break;
                case TableDwhModels.InformationSystemEntity informationSystem:
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
                            nomenclature.Status = GetEntity<TableDwhModels.StatusEntity>(nomenclature.Status.Id);
                    }
                    break;
                case TableDwhModels.NomenclatureGroupEntity nomenclatureGroup:
                    if (!nomenclatureGroup.EqualsEmpty())
                    {
                        if (nomenclatureGroup.InformationSystem != null)
                            nomenclatureGroup.InformationSystem = GetEntity<TableDwhModels.InformationSystemEntity>(nomenclatureGroup.InformationSystem.Id);
                    }
                    break;
                case TableDwhModels.NomenclatureLightEntity nomenclatureLight:
                    if (!nomenclatureLight.EqualsEmpty())
                    {
                        if (nomenclatureLight.InformationSystem != null)
                            nomenclatureLight.InformationSystem = GetEntity<TableDwhModels.InformationSystemEntity>(nomenclatureLight.InformationSystem.Id);
                    }
                    break;
                case TableDwhModels.NomenclatureParentEntity nomenclatureParent:
                    //
                    break;
                case TableDwhModels.NomenclatureTypeEntity nomenclatureType:
                    if (!nomenclatureType.EqualsEmpty())
                    {
                        if (nomenclatureType.InformationSystem != null)
                            nomenclatureType.InformationSystem = GetEntity<TableDwhModels.InformationSystemEntity>(nomenclatureType.InformationSystem.Id);
                    }
                    break;
                case TableDwhModels.StatusEntity status:
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
            ExecTransaction((session) => {
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

        public T[] GetEntitiesNativeMapping<T>(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") 
            where T : BaseEntity, new() 
            => GetEntitiesNativeMappingInside<T>(query, filePath, lineNumber, memberName);

        public object[] GetEntitiesNativeObject(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
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

        public int ExecQueryNativeInside(string query, Dictionary<string, object> parameters, string filePath, int lineNumber, string memberName)
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

        public int ExecQueryNative(string query, Dictionary<string, object> parameters,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") 
            => ExecQueryNativeInside(query, parameters, filePath, lineNumber, memberName);

        public void SaveEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") 
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;

            //ExecTransaction((session) => {
            //    int idLast = GetEntity<TableSystemModels.HostEntity>(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            //    TableSystemModels.HostEntity foo = new()
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
                case TableScaleModels.BarcodeTypeEntity barcodeType:
                    ExecTransaction((session) => {
                        Console.WriteLine(barcodeType);
                        session.Save(barcodeType);
                    }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ContragentEntity contragent:
                    Console.WriteLine(contragent);
                    throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                case TableSystemModels.HostEntity host:
                    ExecTransaction((session) => {
                        Console.WriteLine(host);
                        session.Save(host);
                    }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.NomenclatureEntity nomenclature:
                    Console.WriteLine(nomenclature);
                    throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                case TableScaleModels.PrinterTypeEntity printerType:
                    ExecTransaction((session) => {
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
                case TableSystemModels.AccessEntity access:
                    ExecTransaction((session) => { session.SaveOrUpdate(access); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.AppEntity app:
                    ExecTransaction((session) => { session.SaveOrUpdate(app); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.ErrorEntity error:
                    ExecTransaction((session) => { session.SaveOrUpdate(error); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.HostEntity host:
                    ExecTransaction((session) => { session.SaveOrUpdate(host); }, filePath, lineNumber, memberName);
                    host.ModifiedDate = DateTime.Now;
                    break;
                case TableSystemModels.LogEntity log:
                    ExecTransaction((session) => { session.SaveOrUpdate(log); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.LogTypeEntity logType:
                    ExecTransaction((session) => { session.SaveOrUpdate(logType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.BarcodeTypeEntity barcodeType:
                    ExecTransaction((session) => { session.SaveOrUpdate(barcodeType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ContragentEntity contragent:
                    contragent.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(contragent); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.LabelEntity label:
                    ExecTransaction((session) => { session.SaveOrUpdate(label); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderEntity order:
                    order.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(order); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderStatusEntity orderStatus:
                    ExecTransaction((session) => { session.SaveOrUpdate(orderStatus); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderTypeEntity orderType:
                    ExecTransaction((session) => { session.SaveOrUpdate(orderType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PluEntity plu:
                    ExecTransaction((session) => { session.SaveOrUpdate(plu); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ProductionFacilityEntity productionFacility:
                    ExecTransaction((session) => { session.SaveOrUpdate(productionFacility); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ProductSeriesEntity productSeries:
                    ExecTransaction((session) => { session.SaveOrUpdate(productSeries); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ScaleEntity scale:
                    scale.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(scale); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.TemplateEntity template:
                    template.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(template); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.TemplateResourceEntity templateResource:
                    templateResource.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(templateResource); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.WeithingFactEntity weithingFact:
                    ExecTransaction((session) => { session.SaveOrUpdate(weithingFact); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.WorkshopEntity workshop:
                    workshop.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(workshop); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterEntity printer:
                    printer.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(printer); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterResourceEntity printerResource:
                    printerResource.ModifiedDate = DateTime.Now;
                    ExecTransaction((session) => { session.SaveOrUpdate(printerResource); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterTypeEntity printerType:
                    ExecTransaction((session) => { session.SaveOrUpdate(printerType); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.BrandEntity brand:
                    ExecTransaction((session) => { session.SaveOrUpdate(brand); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.InformationSystemEntity informationSystem:
                    ExecTransaction((session) => { session.SaveOrUpdate(informationSystem); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureEntity nomenclature:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclature); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureGroupEntity nomenclatureGroup:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureGroup); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureLightEntity nomenclatureLight:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureLight); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureTypeEntity nomenclatureType:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureType); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.StatusEntity status:
                    ExecTransaction((session) => { session.SaveOrUpdate(status); }, filePath, lineNumber, memberName);
                    break;
                default:
                    ExecTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
                    break;
            }
        }

        public void DeleteEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") 
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;
            ExecTransaction((session) => { session.Delete(item); }, filePath, lineNumber, memberName);
        }

        public void MarkedEntity<T>(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") 
            where T : BaseEntity, new()
        {
            if (item.EqualsEmpty()) return;

            switch (item)
            {
                case TableSystemModels.AccessEntity access:
                    ExecTransaction((session) => { session.SaveOrUpdate(access); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.AppEntity app:
                    ExecTransaction((session) => { session.SaveOrUpdate(app); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.ErrorEntity error:
                    ExecTransaction((session) => { session.SaveOrUpdate(error); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.HostEntity host:
                    host.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(host); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.LogEntity log:
                    ExecTransaction((session) => { session.SaveOrUpdate(log); }, filePath, lineNumber, memberName);
                    break;
                case TableSystemModels.LogTypeEntity logType:
                    ExecTransaction((session) => { session.SaveOrUpdate(logType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.BarcodeTypeEntity barcodeType:
                    ExecTransaction((session) => { session.SaveOrUpdate(barcodeType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ContragentEntity contragent:
                    contragent.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(contragent); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.LabelEntity label:
                    ExecTransaction((session) => { session.SaveOrUpdate(label); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderEntity order:
                    ExecTransaction((session) => { session.SaveOrUpdate(order); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderStatusEntity orderStatus:
                    ExecTransaction((session) => { session.SaveOrUpdate(orderStatus); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.OrderTypeEntity orderType:
                    ExecTransaction((session) => { session.SaveOrUpdate(orderType); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PluEntity plu:
                    plu.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(plu); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ProductionFacilityEntity productionFacility:
                    productionFacility.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(productionFacility); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ProductSeriesEntity productSeries:
                    ExecTransaction((session) => { session.SaveOrUpdate(productSeries); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.ScaleEntity scale:
                    scale.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(scale); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.TemplateResourceEntity templateResource:
                    templateResource.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(templateResource); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.TemplateEntity template:
                    template.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(template); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.WeithingFactEntity weithingFact:
                    ExecTransaction((session) => { session.SaveOrUpdate(weithingFact); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.WorkshopEntity workshop:
                    workshop.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(workshop); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterEntity printer:
                    printer.Marked = true;
                    ExecTransaction((session) => { session.SaveOrUpdate(printer); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterResourceEntity printerResource:
                    ExecTransaction((session) => { session.SaveOrUpdate(printerResource); }, filePath, lineNumber, memberName);
                    break;
                case TableScaleModels.PrinterTypeEntity printerType:
                    ExecTransaction((session) => { session.SaveOrUpdate(printerType); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.BrandEntity brand:
                    ExecTransaction((session) => { session.SaveOrUpdate(brand); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.InformationSystemEntity informationSystem:
                    ExecTransaction((session) => { session.SaveOrUpdate(informationSystem); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureEntity nomenclature:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclature); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureGroupEntity nomenclatureGroup:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureGroup); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureLightEntity nomenclatureLight:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureLight); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.NomenclatureTypeEntity nomenclatureType:
                    ExecTransaction((session) => { session.SaveOrUpdate(nomenclatureType); }, filePath, lineNumber, memberName);
                    break;
                case TableDwhModels.StatusEntity status:
                    ExecTransaction((session) => { session.SaveOrUpdate(status); }, filePath, lineNumber, memberName);
                    break;
                default:
                    ExecTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
                    break;
            }
        }

        public bool ExistsEntityInside<T>(T item, string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
        {
            bool result = false;
            ExecTransaction((session) => {
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
            ExecTransaction((session) => {
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

        public List<TableSystemModels.HostEntity> GetFreeHosts(int? id,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetFreeHosts, filePath, lineNumber, memberName);
            List<TableSystemModels.HostEntity>? items = new();
            foreach (object? entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new TableSystemModels.HostEntity
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDate = Convert.ToDateTime(ent[1]),
                        ModifiedDate = Convert.ToDateTime(ent[2]),
                        Name = Convert.ToString(ent[3]),
                        Ip = Convert.ToString(ent[4]),
                        MacAddress = new MacAddressEntity(Convert.ToString(ent[5])),
                        IdRRef = Guid.Parse(Convert.ToString(ent[6])),
                        Marked = Convert.ToBoolean(ent[7]),
                        SettingsFile = Convert.ToString(ent[8]),
                    });
                }
            }

            if (id > 0 && items.Select(x => x).Where(x => Equals(x.Id, id)).ToList().Count == 0)
            {
                items.Add(GetEntity<TableSystemModels.HostEntity>(
                    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }), null));
            }
            return items;
        }

        public List<TableSystemModels.HostEntity> GetBusyHosts(
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            object[]? entities = DataAccess.Crud.GetEntitiesNativeObject(SqlQueries.DbScales.Tables.Hosts.GetBusyHosts, filePath, lineNumber, memberName);
            List<TableSystemModels.HostEntity>? items = new();
            foreach (object? entity in entities)
            {
                if (entity is object[] { Length: 9 } ent)
                {
                    items.Add(new TableSystemModels.HostEntity
                    {
                        Id = Convert.ToInt32(ent[0]),
                        CreateDate = Convert.ToDateTime(ent[1]),
                        ModifiedDate = Convert.ToDateTime(ent[2]),
                        Name = Convert.ToString(ent[3]),
                        Ip = Convert.ToString(ent[4]),
                        MacAddress = new MacAddressEntity(Convert.ToString(ent[5])),
                        IdRRef = Guid.Parse(Convert.ToString(ent[6])),
                        Marked = Convert.ToBoolean(ent[7]),
                        SettingsFile = Convert.ToString(ent[8]),
                    });
                }
            }
            return items;
        }

        #endregion
    }
}
