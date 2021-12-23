// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataProjectsCore.DAL.Models
{
    //public class CrudBase<T> where T : BaseEntity, new()
    public class CrudController
    {
        #region Public and private fields and properties

        public DataAccessEntity DataAccess { get; private set; }
        public DataConfigurationEntity DataConfig { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }
        private delegate void ExecCallback(ISession session);

        #endregion

        #region Constructor and destructor

        public CrudController(DataAccessEntity dataAccess, ISessionFactory sessionFactory)
        {
            DataAccess = dataAccess;
            DataConfig = new DataConfigurationEntity();
            SessionFactory = sessionFactory;
        }

        #endregion

        #region Public and private methods

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
            //if (ErrorsCrud == null)
            //    return;
            int idLast = GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            TableScaleModels.ErrorEntity error = new()
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
            //ErrorsCrud.SaveEntity(error);
            SaveEntityInside(error, filePath, lineNumber, memberName);
        }


        public IBaseEntity[] GetEntitiesWithConfig(string filePath, int lineNumber, string memberName)
        {
            IBaseEntity[]? result = new IBaseEntity[0];
            ExecTransaction((session) => {
                if (DataConfig != null)
                {
                    result = DataConfig.OrderAsc
                        ? session.Query<IBaseEntity>()
                        .OrderBy(ent => ent)
                        .Skip(DataConfig.PageNo * DataConfig.PageSize)
                        .Take(DataConfig.PageSize)
                        .ToArray()
                        : session.Query<IBaseEntity>()
                            .OrderByDescending(ent => ent)
                            .Skip(DataConfig.PageNo * DataConfig.PageSize)
                            .Take(DataConfig.PageSize)
                            .ToArray()
                        ;
                }
            }, filePath, lineNumber, memberName);
            return result;
        }

        private ICriteria GetCriteria(ISession session, FieldListEntity? fieldList, FieldOrderEntity order, int maxResults)
        {
            ICriteria criteria = session.CreateCriteria(typeof(IBaseEntity));
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
                Order fieldOrder = order.Direction == ShareEnums.DbOrderDirection.Asc
                    ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
                criteria.AddOrder(fieldOrder);
            }
            return criteria;
        }

        private void ExecTransaction(ExecCallback callback, string filePath, int lineNumber, string memberName)
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

        public IBaseEntity[]? GetEntitiesWithoutReferences(FieldListEntity fieldList, FieldOrderEntity order, int maxResults, string filePath, int lineNumber, string memberName)
        {
            IBaseEntity[]? result = new IBaseEntity[0];
            ExecTransaction((session) => {
                result = GetCriteria(session, fieldList, order, maxResults).List<IBaseEntity>().ToArray();
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

        public void DeleteEntity<T>(T item, string filePath, int lineNumber, string memberName) where T : IBaseEntity
        {
            if (item.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.Delete(item);
            }, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity<T>(T item, string filePath, int lineNumber, string memberName)
        {
            bool result = false;
            ExecTransaction((session) => {
                result = session.Query<T>().Any(x => x.IsAny(item));
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

        #endregion

        #region Public and private methods

        public void FillReferences(IBaseEntity item)
        {
            FillReferencesSystem(item);
            FillReferencesDatas(item);
            FillReferencesScales(item);
            FillReferencesDwh(item);
        }

        private void FillReferencesSystem(IBaseEntity item)
        {
            if (item is TableSystemModels.AppEntity)
            {
                //
            }
            else if (item is TableSystemModels.LogEntity logEntity)
            {
                if (!logEntity.EqualsEmpty())
                {
                    if (logEntity.App != null && DataAccess.AppsCrud != null)
                        logEntity.App = DataAccess.AppsCrud.GetEntity(logEntity.App.Uid);
                    if (logEntity.Host != null && DataAccess.HostsCrud != null)
                        logEntity.Host = DataAccess.HostsCrud.GetEntity(logEntity.Host.Id);
                }
            }
        }

        private void FillReferencesDatas(IBaseEntity item)
        {
            if (item is DataModels.DeviceEntity)
            {
                DataModels.DeviceEntity? deviceEntity = (DataModels.DeviceEntity)(object)item;
                if (!deviceEntity.EqualsEmpty())
                {
                    if (deviceEntity.Scales != null && DataAccess.ScalesCrud != null)
                        deviceEntity.Scales = DataAccess.ScalesCrud.GetEntity(deviceEntity.Scales.Id);
                }
            }
        }

        private void FillReferencesScales(IBaseEntity item)
        {
            if (item is TableScaleModels.BarcodeTypeEntity)
            {
                TableScaleModels.BarcodeTypeEntity? barCodeTypesEntity = (TableScaleModels.BarcodeTypeEntity)(object)item;
                if (!barCodeTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (item is TableScaleModels.ContragentEntity)
            {
                TableScaleModels.ContragentEntity? contragentsEntity = (TableScaleModels.ContragentEntity)(object)item;
                if (!contragentsEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                TableScaleModels.LabelEntity? labelsEntity = (TableScaleModels.LabelEntity)(object)item;
                if (!labelsEntity.EqualsEmpty())
                {
                    if (labelsEntity.WeithingFact != null && DataAccess.WeithingFactsCrud != null)
                        labelsEntity.WeithingFact = DataAccess.WeithingFactsCrud.GetEntity(labelsEntity.WeithingFact.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
            {
                TableScaleModels.NomenclatureEntity? nomenclatureEntity = (TableScaleModels.NomenclatureEntity)(object)item;
                if (!nomenclatureEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrganizationEntity))
            {
                TableScaleModels.OrganizationEntity? organizationEntity = (TableScaleModels.OrganizationEntity)(object)item;
                if (!organizationEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                TableScaleModels.OrderEntity? ordersEntity = (TableScaleModels.OrderEntity)(object)item;
                if (!ordersEntity.EqualsEmpty())
                {
                    if (ordersEntity.OrderTypes != null && DataAccess.OrderTypesCrud != null)
                        ordersEntity.OrderTypes = DataAccess.OrderTypesCrud.GetEntity(ordersEntity.OrderTypes.Id);
                    if (ordersEntity.Scales != null && DataAccess.ScalesCrud != null)
                        ordersEntity.Scales = DataAccess.ScalesCrud.GetEntity(ordersEntity.Scales.Id);
                    if (ordersEntity.Plu != null && DataAccess.PlusCrud != null)
                        ordersEntity.Plu = DataAccess.PlusCrud.GetEntity(ordersEntity.Plu.Id);
                    if (ordersEntity.Templates != null && DataAccess.TemplatesCrud != null)
                        ordersEntity.Templates = DataAccess.TemplatesCrud.GetEntity(ordersEntity.Templates.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                TableScaleModels.OrderStatusEntity? orderStatusEntity = (TableScaleModels.OrderStatusEntity)(object)item;
                if (!orderStatusEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                TableScaleModels.OrderTypeEntity? orderTypesEntity = (TableScaleModels.OrderTypeEntity)(object)item;
                if (!orderTypesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                TableScaleModels.PluEntity? pluEntity = (TableScaleModels.PluEntity)(object)item;
                if (!pluEntity.EqualsEmpty())
                {
                    if (pluEntity.Templates != null && DataAccess.TemplatesCrud != null)
                        pluEntity.Templates = DataAccess.TemplatesCrud.GetEntity(pluEntity.Templates.Id);
                    if (pluEntity.Scale != null && DataAccess.ScalesCrud != null)
                        pluEntity.Scale = DataAccess.ScalesCrud.GetEntity(pluEntity.Scale.Id);
                    if (pluEntity.Nomenclature != null && DataAccess.NomenclaturesCrud != null)
                        pluEntity.Nomenclature = DataAccess.NomenclaturesCrud.GetEntity(pluEntity.Nomenclature.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                TableScaleModels.ProductionFacilityEntity? productionFacilityEntity = (TableScaleModels.ProductionFacilityEntity)(object)item;
                if (!productionFacilityEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                TableScaleModels.ProductSeriesEntity? productSeriesEntity = (TableScaleModels.ProductSeriesEntity)(object)item;
                if (!productSeriesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                TableScaleModels.ScaleEntity? scalesEntity = (TableScaleModels.ScaleEntity)(object)item;
                if (!scalesEntity.EqualsEmpty())
                {
                    if (scalesEntity.TemplateDefault != null && DataAccess.TemplatesCrud != null)
                        scalesEntity.TemplateDefault = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateDefault.Id);
                    if (scalesEntity.TemplateSeries != null && DataAccess.TemplatesCrud != null)
                        scalesEntity.TemplateSeries = DataAccess.TemplatesCrud.GetEntity(scalesEntity.TemplateSeries.Id);
                    if (scalesEntity.WorkShop != null && DataAccess.WorkshopsCrud != null)
                        scalesEntity.WorkShop = DataAccess.WorkshopsCrud.GetEntity(scalesEntity.WorkShop.Id);
                    if (scalesEntity.Printer != null && DataAccess.PrintersCrud != null)
                        scalesEntity.Printer = DataAccess.PrintersCrud.GetEntity(scalesEntity.Printer.Id);
                    if (scalesEntity.Host != null && DataAccess.HostsCrud != null)
                        scalesEntity.Host = DataAccess.HostsCrud.GetEntity(scalesEntity.Host.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TaskEntity))
            {
                TableScaleModels.TaskEntity? taskEntity = (TableScaleModels.TaskEntity)(object)item;
                if (!taskEntity.EqualsEmpty())
                {
                    if (taskEntity.TaskType != null && DataAccess.TaskTypeCrud != null)
                        taskEntity.TaskType = DataAccess.TaskTypeCrud.GetEntity(taskEntity.TaskType.Uid);
                    if (taskEntity.Scale != null && DataAccess.ScalesCrud != null)
                        taskEntity.Scale = DataAccess.ScalesCrud.GetEntity(taskEntity.Scale.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TaskTypeEntity))
            {
                TableScaleModels.TaskTypeEntity? taskTypeEntity = (TableScaleModels.TaskTypeEntity)(object)item;
                if (!taskTypeEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                TableScaleModels.TemplateResourceEntity? templateResourcesEntity = (TableScaleModels.TemplateResourceEntity)(object)item;
                if (!templateResourcesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                TableScaleModels.TemplateEntity? templatesEntity = (TableScaleModels.TemplateEntity)(object)item;
                if (!templatesEntity.EqualsEmpty())
                {
                    //
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                TableScaleModels.WeithingFactEntity? weithingFactEntity = (TableScaleModels.WeithingFactEntity)(object)item;
                if (!weithingFactEntity.EqualsEmpty())
                {
                    if (weithingFactEntity.Plu != null && DataAccess.PlusCrud != null)
                        weithingFactEntity.Plu = DataAccess.PlusCrud.GetEntity(weithingFactEntity.Plu.Id);
                    if (weithingFactEntity.Scales != null && DataAccess.ScalesCrud != null)
                        weithingFactEntity.Scales = DataAccess.ScalesCrud.GetEntity(weithingFactEntity.Scales.Id);
                    if (weithingFactEntity.Series != null && DataAccess.ProductSeriesCrud != null)
                        weithingFactEntity.Series = DataAccess.ProductSeriesCrud.GetEntity(weithingFactEntity.Series.Id);
                    if (weithingFactEntity.Orders != null && DataAccess.OrdersCrud != null)
                        weithingFactEntity.Orders = DataAccess.OrdersCrud.GetEntity(weithingFactEntity.Orders.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                TableScaleModels.WorkshopEntity? workshopEntity = (TableScaleModels.WorkshopEntity)(object)item;
                if (!workshopEntity.EqualsEmpty())
                {
                    if (workshopEntity.ProductionFacility != null && DataAccess.ProductionFacilitiesCrud != null)
                        workshopEntity.ProductionFacility = DataAccess.ProductionFacilitiesCrud.GetEntity(workshopEntity.ProductionFacility.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                TableScaleModels.PrinterEntity? zebraPrinterEntity = (TableScaleModels.PrinterEntity)(object)item;
                if (!zebraPrinterEntity.EqualsEmpty())
                {
                    if (zebraPrinterEntity.PrinterType != null && DataAccess.PrinterTypesCrud != null)
                        zebraPrinterEntity.PrinterType = DataAccess.PrinterTypesCrud.GetEntity(zebraPrinterEntity.PrinterType.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                TableScaleModels.PrinterResourceEntity? zebraPrinterResourceRefEntity = (TableScaleModels.PrinterResourceEntity)(object)item;
                if (!zebraPrinterResourceRefEntity.EqualsEmpty())
                {
                    if (zebraPrinterResourceRefEntity.Printer != null && DataAccess.PrintersCrud != null)
                        zebraPrinterResourceRefEntity.Printer = DataAccess.PrintersCrud.GetEntity(zebraPrinterResourceRefEntity.Printer.Id);
                    if (zebraPrinterResourceRefEntity.Resource != null && DataAccess.TemplateResourcesCrud != null)
                        zebraPrinterResourceRefEntity.Resource = DataAccess.TemplateResourcesCrud.GetEntity(zebraPrinterResourceRefEntity.Resource.Id);
                }
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                TableScaleModels.PrinterTypeEntity? zebraPrinterTypeEntity = (TableScaleModels.PrinterTypeEntity)(object)item;
                if (!zebraPrinterTypeEntity.EqualsEmpty())
                {
                    //
                }
            }
        }

        private void FillReferencesDwh(IBaseEntity item)
        {
            if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                TableDwhModels.BrandEntity brandEntity = (TableDwhModels.BrandEntity)(object)item;
                if (!brandEntity.EqualsEmpty())
                {
                    if (brandEntity.InformationSystem != null && DataAccess.InformationSystemCrud != null)
                        brandEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(brandEntity.InformationSystem.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                TableDwhModels.NomenclatureEntity? nomenclatureEntity = (TableDwhModels.NomenclatureEntity)(object)item;
                if (!nomenclatureEntity.EqualsEmpty())
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
                    if (nomenclatureEntity.Status != null && DataAccess.StatusCrud != null)
                        nomenclatureEntity.Status = DataAccess.StatusCrud.GetEntity(nomenclatureEntity.Status.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity = (TableDwhModels.NomenclatureLightEntity)(object)item;
                if (!nomenclatureLightEntity.EqualsEmpty())
                {
                    if (nomenclatureLightEntity.InformationSystem != null && DataAccess.InformationSystemCrud != null)
                        nomenclatureLightEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureLightEntity.InformationSystem.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity = (TableDwhModels.NomenclatureGroupEntity)(object)item;
                if (!nomenclatureGroupEntity.EqualsEmpty())
                {
                    if (nomenclatureGroupEntity.InformationSystem != null && DataAccess.InformationSystemCrud != null)
                        nomenclatureGroupEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureGroupEntity.InformationSystem.Id);
                }
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity = (TableDwhModels.NomenclatureTypeEntity)(object)item;
                if (!nomenclatureTypeEntity.EqualsEmpty())
                {
                    if (nomenclatureTypeEntity.InformationSystem != null && DataAccess.InformationSystemCrud != null)
                        nomenclatureTypeEntity.InformationSystem = DataAccess.InformationSystemCrud.GetEntity(nomenclatureTypeEntity.InformationSystem.Id);
                }
            }
        }

        public T GetEntity(FieldListEntity? fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IBaseEntity item = GetEntity<T>(fieldList, order, filePath, lineNumber, memberName);
            FillReferences(item);
            return item;
        }

        public IBaseEntity GetEntity(int id)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), id } }),
                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
        }

        public IBaseEntity GetEntity(Guid uid)
        {
            return GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Uid.ToString(), uid } }),
                new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc));
        }

        public IBaseEntity[]? GetEntities(FieldListEntity fieldList, FieldOrderEntity order, int maxResults = 0,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            IBaseEntity[]? items = GetEntitiesWithoutReferences(fieldList, order, maxResults, filePath, lineNumber, memberName);
            if (items != null)
            {
                foreach (IBaseEntity? item in items)
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

        public IBaseEntity[] GetEntitiesNativeMappingInside(string query, string filePath, int lineNumber, string memberName)
        {
            IBaseEntity[]? result = new IBaseEntity[0];
            ExecTransaction((session) => {
                ISQLQuery? sqlQuery = GetSqlQuery(session, query);
                if (sqlQuery != null)
                {
                    sqlQuery.AddEntity(typeof(IBaseEntity));
                    System.Collections.IList? listEntities = sqlQuery.List();
                    result = new IBaseEntity[listEntities.Count];
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = (IBaseEntity)listEntities[i];
                    }
                }
            }, filePath, lineNumber, memberName);
            return result;
        }

        public IBaseEntity[] GetEntitiesNativeMapping(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            //return DataAccess.GetEntitiesNativeMapping<T>(query, filePath, lineNumber, memberName);
            return GetEntitiesNativeMappingInside(query, filePath, lineNumber, memberName);
        }

        public object[] GetEntitiesNativeObjectInside(string query, string filePath, int lineNumber, string memberName)
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

        public object[] GetEntitiesNativeObject(string query,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            //return DataAccess.GetEntitiesNativeObject(query, filePath, lineNumber, memberName);
            return GetEntitiesNativeObjectInside(query, filePath, lineNumber, memberName);
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
        {
            //return DataAccess.ExecQueryNative(query, parameters, filePath, lineNumber, memberName);
            return ExecQueryNativeInside(query, parameters, filePath, lineNumber, memberName);
        }

        public void SaveEntityInside(IBaseEntity item, string filePath, int lineNumber, string memberName)
        {
            if (item.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.Save(item);
            }, filePath, lineNumber, memberName);
        }

        public void SaveEntity(IBaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (item.EqualsEmpty()) return;
            if (item is BaseIdEntity idEntity)
            {
                if (!item.Equals(GetEntity(idEntity.Id)))
                {
                    if (item is TableScaleModels.ContragentEntity)
                    {
                        throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                    }
                    if (item is TableScaleModels.NomenclatureEntity)
                    {
                        throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                    }
                    if (item is TableScaleModels.PrinterTypeEntity)
                    {
                        Console.WriteLine($"SaveEntity: {item}");
                    }
                    //DataAccess.SaveEntity(item, filePath, lineNumber, memberName);
                    SaveEntityInside(item, filePath, lineNumber, memberName);
                }
            }
            else
            {
                if (item is BaseUidEntity uidEntity)
                {
                    if (!item.Equals(GetEntity(uidEntity.Uid)))
                    {
                        if (item is TableScaleModels.ContragentEntity)
                        {
                            throw new Exception("SaveEntity for [ContragentsEntity] is deny!");
                        }
                        if (item is TableScaleModels.NomenclatureEntity)
                        {
                            throw new Exception("SaveEntity for [NomenclatureEntity] is deny!");
                        }
                        if (item is TableScaleModels.PrinterTypeEntity)
                        {
                            Console.WriteLine($"SaveEntity: {item}");
                        }
                        //DataAccess.SaveEntity(item, filePath, lineNumber, memberName);
                        SaveEntityInside(item, filePath, lineNumber, memberName);
                    }
                }
            }
        }

        public void UpdateEntityInside(IBaseEntity item, string filePath, int lineNumber, string memberName)
        {
            if (item.EqualsEmpty()) return;
            ExecTransaction((session) => {
                session.SaveOrUpdate(item);
            }, filePath, lineNumber, memberName);
        }

        public void UpdateEntity(IBaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (item.EqualsEmpty()) return;

            // SYSTEM.
            if (item is TableSystemModels.AppEntity)
            {
                //
            }
            else if (item is TableSystemModels.LogEntity)
            {
                //
            }
            else if (item is TableSystemModels.HostEntity host)
            {
                host.ModifiedDate = DateTime.Now;
            }
            // SCALES.
            else if (item is TableScaleModels.BarcodeTypeEntity)
            {
                //
            }
            else if (item is TableScaleModels.ContragentEntity contragent)
            {
                contragent.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.LabelEntity)
            {
                //
            }
            else if (item is TableScaleModels.OrderEntity order)
            {
                order.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.OrderStatusEntity)
            {
                //
            }
            else if (item is TableScaleModels.OrderTypeEntity)
            {
                //
            }
            else if (item is TableScaleModels.PluEntity)
            {
                //
            }
            else if (item is TableScaleModels.ProductionFacilityEntity)
            {
                //
            }
            else if (item is TableScaleModels.ProductSeriesEntity)
            {
                //
            }
            else if (item is TableScaleModels.ScaleEntity scale)
            {
                scale.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.TemplateEntity template)
            {
                template.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.TemplateResourceEntity templateResource)
            {
                templateResource.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.WeithingFactEntity)
            {
                //
            }
            else if (item is TableScaleModels.WorkshopEntity workshop)
            {
                workshop.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.PrinterEntity printer)
            {
                printer.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.PrinterResourceEntity printerResource)
            {
                printerResource.ModifiedDate = DateTime.Now;
            }
            else if (item is TableScaleModels.PrinterTypeEntity)
            {
                //
            }
            // DWH.
            else if (item is TableDwhModels.BrandEntity)
            {
                //
            }
            else if (item is TableDwhModels.InformationSystemEntity)
            {
                //
            }
            else if (item is TableDwhModels.NomenclatureEntity)
            {
                //
            }
            else if (item is TableDwhModels.NomenclatureGroupEntity)
            {
                //
            }
            else if (item is TableDwhModels.NomenclatureLightEntity)
            {
                //
            }
            else if (item is TableDwhModels.NomenclatureTypeEntity)
            {
                //
            }
            else if (item is TableDwhModels.StatusEntity)
            {
                //
            }

            //DataAccess.UpdateEntity(item, filePath, lineNumber, memberName);
            UpdateEntityInside(item, filePath, lineNumber, memberName);
        }

        public void DeleteEntity(IBaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (item.EqualsEmpty()) return;

            //DataAccess.DeleteEntity(item, filePath, lineNumber, memberName);
            DeleteEntity(item, filePath, lineNumber, memberName);
        }

        public void MarkedEntity(IBaseEntity item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (item.EqualsEmpty()) return;

            // SYSTEM.
            if (item is TableSystemModels.AppEntity)
            {
                //
            }
            else if (item is TableSystemModels.LogEntity)
            {
                //
            }
            else if (item is TableSystemModels.HostEntity host)
            {
                host.Marked = true;
            }

            // SCALES.
            else if (item is TableScaleModels.BarcodeTypeEntity)
            {
                //
            }
            else if (item is TableScaleModels.ContragentEntity contragent)
            {
                contragent.Marked = true;
            }
            else if (item is TableScaleModels.LabelEntity)
            {
                //
            }
            else if (item is TableScaleModels.OrderEntity)
            {
                //
            }
            else if (item is TableScaleModels.OrderStatusEntity)
            {
                //
            }
            else if (item is TableScaleModels.OrderTypeEntity)
            {
                //
            }
            else if (item is TableScaleModels.PluEntity plu)
            {
                plu.Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                ((TableScaleModels.ProductionFacilityEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                ((TableScaleModels.ScaleEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                ((TableScaleModels.TemplateResourceEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                ((TableScaleModels.TemplateEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                ((TableScaleModels.WorkshopEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                ((TableScaleModels.PrinterEntity)(object)item).Marked = true;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                ((TableScaleModels.PrinterEntity)(object)item).Marked = true;
            }
            
            // DWH.
            else if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.InformationSystemEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                //
            }
            else if (typeof(T) == typeof(TableDwhModels.StatusEntity))
            {
                //
            }

            //DataAccess.UpdateEntity(item, filePath, lineNumber, memberName);
            UpdateEntityInside(item, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity(T item,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (item.EqualsEmpty()) return false;
            return DataAccess.ExistsEntity(item, filePath, lineNumber, memberName);
        }

        public bool ExistsEntity(FieldListEntity fieldList, FieldOrderEntity order,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            return DataAccess.ExistsEntity<T>(fieldList, order, filePath, lineNumber, memberName);
        }

        #endregion
    }
}
