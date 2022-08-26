// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDwhModels;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public class CrudController
{
    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    private DataConfigurationEntity DataConfig { get; }

    private delegate void ExecCallback(ISession session);

    #endregion

    #region Constructor and destructor

    public CrudController()
    {
        DataConfig = new();
    }

    #endregion

    #region Public and private methods

    public T[]? GetEntitiesWithConfig<T>(string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
    {
        T[]? result = null;
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

    private ICriteria GetCriteria<T>(ISession session, FilterListEntity? filterList, FieldOrderEntity? order, int maxResults)
        where T : BaseEntity, new()
    {
        Type type = typeof(T);
        ICriteria criteria = session.CreateCriteria(type);
        if (maxResults > 0)
        {
            criteria.SetMaxResults(maxResults);
        }
        if (filterList is { IsEnabled: true, Fields: { } })
        {
            foreach (FieldEntity field in filterList.Fields)
            {
                AbstractCriterion? criterion;
                switch (field.Comparer)
                {
                    case DbComparer.Equal:
                        criterion = Restrictions.Eq(field.Name, field.Value);
                        break;
                    case DbComparer.NotEqual:
                        criterion = Restrictions.Not(Restrictions.Eq(field.Name, field.Value));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (criterion != null)
                    criteria.Add(criterion);
            }
        }
        if (order is { IsEnabled: true })
        {
            Order fieldOrder = order.Direction == DbOrderDirection.Asc
                ? Order.Asc(order.Name.ToString()) : Order.Desc(order.Name.ToString());
            criteria.AddOrder(fieldOrder);
        }
        return criteria;
    }

    private void ExecuteTransaction(ExecCallback callback, string filePath, int lineNumber, string memberName, bool isException = false)
    {
        using ISession? session = DataAccess.SessionFactory.OpenSession();
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
                transaction.Dispose();
                session.Disconnect();
                session.Close();
                session.Dispose();
            }
        }
        if (!isException && exception != null)
        {
            DataAccess.Log.LogError(exception, null, null, filePath, lineNumber, memberName);
        }
    }

    public ISQLQuery? GetSqlQuery(ISession session, string query)
    {
        if (string.IsNullOrEmpty(query))
            return null;

        return session.CreateSQLQuery(query);
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
    //                List<T> listEntities = items.List<T>();
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

    private void FillReferences<T>(T? item) where T : BaseEntity, new()
    {
        FillReferencesSystem(item);
        FillReferencesDatas(item);
        FillReferencesScales(item);
        FillReferencesDwh(item);
    }

    private void FillReferencesSystem<T>(T? item) where T : BaseEntity, new()
    {
        if (item == null) return;
        switch (item)
        {
            case LogEntity log:
                log.App = log.App?.IdentityUid == null ? new() : GetEntityByUid<AppEntity>(log.App.IdentityUid);
                log.Host = log.Host?.IdentityId == null ? new() : GetEntityById<HostEntity>(log.Host.IdentityId);
                log.LogType = log.LogType?.IdentityUid == null ? new() : GetEntityByUid<LogTypeEntity>(log.LogType.IdentityUid);
                break;
        }
    }

    private void FillReferencesDatas<T>(T? item) where T : BaseEntity, new()
    {
        if (item == null) return;
        switch (item)
        {
            case DeviceEntity device:
                device.Scales = device.Scales.IdentityId == null ? new() : GetEntityById<ScaleEntity>(device.Scales.IdentityId);
                break;
        }
    }

    private void FillReferencesScales<T>(T? item) where T : BaseEntity, new()
    {
        if (item == null) return;
        switch (item)
        {
            case BarCodeEntity barcode:
                barcode.BarcodeType = barcode.BarcodeType?.IdentityUid == null ? null : GetEntityByUid<BarCodeTypeEntity>(barcode.BarcodeType.IdentityUid);
                barcode.Contragent = barcode.Contragent?.IdentityUid == null ? null : GetEntityByUid<ContragentEntity>(barcode.Contragent.IdentityUid);
                barcode.Nomenclature = barcode.Nomenclature?.IdentityId == null ? null : GetEntityById<TableScaleModels.NomenclatureEntity>(barcode.Nomenclature.IdentityId);
                break;
            case OrderWeighingEntity orderWeighing:
                orderWeighing.Order = GetEntityByUid<OrderEntity>(orderWeighing.Order.IdentityUid);
                orderWeighing.PluWeighing = GetEntityByUid<PluWeighingEntity>(orderWeighing.PluWeighing.IdentityUid);
                break;
            case PluEntity plu:
                plu.Template = GetEntityById<TemplateEntity>(plu.Template.IdentityId);
                plu.Nomenclature = GetEntityById<TableScaleModels.NomenclatureEntity>(plu.Nomenclature.IdentityId);
                break;
            case PluLabelEntity pluLabel:
                pluLabel.PluWeighing = pluLabel.PluWeighing == null ? null : GetEntityByUid<PluWeighingEntity>(pluLabel.PluWeighing.IdentityUid);
                break;
            case PluObsoleteEntity pluObsolete:
                pluObsolete.Template = GetEntityById<TemplateEntity>(pluObsolete.Template.IdentityId);
                pluObsolete.Scale = GetEntityById<ScaleEntity>(pluObsolete.Scale.IdentityId);
                pluObsolete.Nomenclature = GetEntityById<TableScaleModels.NomenclatureEntity>(pluObsolete.Nomenclature.IdentityId);
                break;
            case PluScaleEntity pluScale:
                pluScale.Plu = GetEntityByUid<PluEntity>(pluScale.Plu.IdentityUid);
                pluScale.Scale = GetEntityById<ScaleEntity>(pluScale.Scale.IdentityId);
                break;
            case PluWeighingEntity pluWeighing:
                pluWeighing.PluScale = GetEntityByUid<PluScaleEntity>(pluWeighing.PluScale.IdentityUid);
                pluWeighing.Series = GetEntityById<ProductSeriesEntity>(pluWeighing.Series.IdentityId);
                break;
            case PrinterEntity printer:
                printer.PrinterType = GetEntityById<PrinterTypeEntity>(printer.PrinterType.IdentityId);
                break;
            case PrinterResourceEntity printerResource:
                printerResource.Printer = GetEntityById<PrinterEntity>(printerResource.Printer.IdentityId);
                printerResource.Resource = GetEntityById<TemplateResourceEntity>(printerResource.Resource.IdentityId);
                if (string.IsNullOrEmpty(printerResource.Resource.Description))
                    printerResource.Resource.Description = printerResource.Resource.Name;
                break;
            case ProductSeriesEntity product:
                product.Scale = GetEntityById<ScaleEntity>(product.Scale.IdentityId);
                break;
            case ScaleEntity scale:
                scale.TemplateDefault = scale.TemplateDefault?.IdentityId == null ? null : GetEntityById<TemplateEntity>(scale.TemplateDefault.IdentityId);
                scale.TemplateSeries = scale.TemplateSeries?.IdentityId == null ? null : GetEntityById<TemplateEntity>(scale.TemplateSeries.IdentityId);
                scale.PrinterMain = scale.PrinterMain?.IdentityId == null ? null : GetEntityById<PrinterEntity>(scale.PrinterMain.IdentityId);
                scale.PrinterShipping = scale.PrinterShipping?.IdentityId == null ? null : GetEntityById<PrinterEntity>(scale.PrinterShipping.IdentityId);
                scale.Host = scale.Host?.IdentityId == null ? null : GetEntityById<HostEntity>(scale.Host.IdentityId);
                scale.WorkShop = scale.WorkShop?.IdentityId == null ? null : GetEntityById<WorkShopEntity>(scale.WorkShop.IdentityId);
                break;
            case TaskEntity task:
                task.TaskType = GetEntityByUid<TaskTypeEntity>(task.TaskType.IdentityUid);
                task.Scale = GetEntityById<ScaleEntity>(task.Scale.IdentityId);
                break;
            case WorkShopEntity workshop:
                workshop.ProductionFacility = GetEntityById<ProductionFacilityEntity>(workshop.ProductionFacility.IdentityId);
                break;
        }
    }

    private void FillReferencesDwh<T>(T? item) where T : BaseEntity, new()
    {
        if (item == null) return;
        switch (item)
        {
            case BrandEntity brand:
                //if (!brand.EqualsEmpty())
                {
                    brand.InformationSystem = brand.InformationSystem.IdentityId == null ? new() : GetEntityById<InformationSystemEntity>(brand.InformationSystem.IdentityId);
                }
                break;
            case TableDwhModels.NomenclatureEntity nomenclature:
                //if (!nomenclature.EqualsEmpty())
                {
                    //if (nomenclatureEntity.BrandBytes != null && nomenclatureEntity.BrandBytes.Length > 0)
                    //    nomenclatureEntity.Brand = GetEntity(DbField.CodeInIs, nomenclatureEntity.BrandBytes);
                    //if (nomenclatureEntity.InformationSystem.IdentityId != null)
                    //    nomenclatureEntity.InformationSystem = GetEntity(nomenclatureEntity.InformationSystem.Id);
                    //if (nomenclatureEntity.NomenclatureGroupCostBytes != null && nomenclatureEntity.NomenclatureGroupCostBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureGroupCost = GetEntity(DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupCostBytes);
                    //if (nomenclatureEntity.NomenclatureGroupBytes != null && nomenclatureEntity.NomenclatureGroupBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureGroup = GetEntity(DbField.CodeInIs, nomenclatureEntity.NomenclatureGroupBytes);
                    //if (nomenclatureEntity.NomenclatureTypeBytes != null && nomenclatureEntity.NomenclatureTypeBytes.Length > 0)
                    //    nomenclatureEntity.NomenclatureType = GetEntity(DbField.CodeInIs, nomenclatureEntity.NomenclatureTypeBytes);
                    nomenclature.Status = nomenclature.Status?.IdentityId == null ? new() : GetEntityById<StatusEntity>(nomenclature.Status.IdentityId);
                }
                break;
            case NomenclatureGroupEntity nomenclatureGroup:
                //if (!nomenclatureGroup.EqualsEmpty())
                {
                    nomenclatureGroup.InformationSystem = nomenclatureGroup.InformationSystem.IdentityId == null ? new() : GetEntityById<InformationSystemEntity>(nomenclatureGroup.InformationSystem.IdentityId);
                }
                break;
            case NomenclatureLightEntity nomenclatureLight:
                //if (!nomenclatureLight.EqualsEmpty())
                {
                    nomenclatureLight.InformationSystem = nomenclatureLight.InformationSystem.IdentityId == null ? new() : GetEntityById<InformationSystemEntity>(nomenclatureLight.InformationSystem.IdentityId);
                }
                break;
            case NomenclatureTypeEntity nomenclatureType:
                //if (!nomenclatureType.EqualsEmpty())
                {
                    nomenclatureType.InformationSystem = nomenclatureType.InformationSystem.IdentityId == null ? new() : GetEntityById<InformationSystemEntity>(nomenclatureType.InformationSystem.IdentityId);
                }
                break;
        }
    }

    public T? GetEntity<T>(FieldEntity filter, FieldOrderEntity? order = null,
	    [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
	    [CallerMemberName] string memberName = "")
	    where T : BaseEntity, new() =>
	    GetEntity<T>(new FilterListEntity(new() { filter }), order, filePath, lineNumber, memberName);

    /// <summary>
	/// Get entity.
	/// </summary>
	/// <param name="filterList"></param>
	/// <param name="order"></param>
	/// <param name="filePath"></param>
	/// <param name="lineNumber"></param>
	/// <param name="memberName"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T? GetEntity<T>(FilterListEntity? filterList = null, FieldOrderEntity? order = null,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        T? item = null;
        ExecuteTransaction((session) =>
        {
            ICriteria criteria = GetCriteria<T>(session, filterList, order, 1);
            IList<T>? list = criteria.List<T>();
            if (list is not null && list.Count > 0)
				item = list.FirstOrDefault();
        }, filePath, lineNumber, memberName);
        FillReferences(item);
        return item;
    }

    /// <summary>
    /// Get entity by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? GetEntityById<T>(long? id) where T : BaseEntity, new() =>
	    GetEntity<T>(
		    new(new() { new(DbField.IdentityId, DbComparer.Equal, id) }),
		    new(DbField.IdentityId, DbOrderDirection.Desc));

    /// <summary>
    /// Get entity by UID.
    /// </summary>
    /// <param name="uid"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? GetEntityByUid<T>(Guid? uid) where T : BaseEntity, new() =>
	    GetEntity<T>(
		    new(new() { new(DbField.IdentityUid, DbComparer.Equal, uid) }),
		    new(DbField.IdentityUid, DbOrderDirection.Desc));

    public T[]? GetEntities<T>(FilterListEntity? filterList = null, FieldOrderEntity? order = null, int maxResults = 0,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        T[]? items = null;
        
        ExecuteTransaction((session) =>
        {
	        ICriteria criteria = GetCriteria<T>(session, filterList, order, maxResults);
	        IList<T>? list = criteria.List<T>();
	        if (list is not null && list.Count > 0)
		        items = list.ToArray();
        }, filePath, lineNumber, memberName);

		if (items != null)
        {
            foreach (T? item in items)
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
        T[] result = Array.Empty<T>();
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
        object[] result = Array.Empty<object>();
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

    public void SaveEntity<T>(T? item,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        if (item == null)
            return;

        switch (item)
        {
            case ContragentEntity contragent:
                throw new($"{nameof(SaveEntity)} for {nameof(ContragentEntity)} is deny!");
            case TableScaleModels.NomenclatureEntity nomenclature:
                throw new($"{nameof(SaveEntity)} for {nameof(TableScaleModels.NomenclatureEntity)} is deny!");
            default:
                ExecuteTransaction((session) => { session.Save(item); }, filePath, lineNumber, memberName);
                break;
        }
    }

    public void UpdateEntity<T>(T? item,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        if (item == null)
            return;

        item.ChangeDt = DateTime.Now;
        ExecuteTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
    }

    public void DeleteEntity<T>(T? item,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity
    {
        //if (item == null || item.EqualsEmpty()) return;
        if (item == null) return;
        ExecuteTransaction((session) => { session.Delete(item); }, filePath, lineNumber, memberName);
    }

    public void MarkedEntity<T>(T? item,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity
    {
        if (item == null)
            return;

        item.IsMarked = true;
        ExecuteTransaction((session) => { session.SaveOrUpdate(item); }, filePath, lineNumber, memberName);
    }

    private bool ExistsEntityInside<T>(T? item, string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
    {
        bool result = false;
        if (item == null)
            return result;

        ExecuteTransaction((session) =>
        {
            result = session.Query<T>().Any(x => x.IsAny(item));
        }, filePath, lineNumber, memberName);
        return result;
    }

    public bool ExistsEntity<T>(T? item,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        if (item == null)
            return false;

        return ExistsEntityInside(item, filePath, lineNumber, memberName);
    }

    private bool ExistsEntityInside<T>(FilterListEntity filterList, FieldOrderEntity? order,
        string filePath, int lineNumber, string memberName) where T : BaseEntity, new()
    {
        bool result = false;
        ExecuteTransaction((session) =>
        {
            result = GetCriteria<T>(session, filterList, order, 1).List<T>().Count > 0;
        }, filePath, lineNumber, memberName);
        return result;
    }

    public bool ExistsEntity<T>(FilterListEntity filterList, FieldOrderEntity? order,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        where T : BaseEntity, new()
    {
        return ExistsEntityInside<T>(filterList, order, filePath, lineNumber, memberName);
    }

    /// <summary>
    /// Get column identity.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public ColumnName GetColumnIdentity(BaseEntity? item)
    {
	    return item switch
	    {
		    AccessEntity => AccessEntity.IdentityName,
		    AppEntity => AppEntity.IdentityName,
		    BarCodeTypeEntity => BarCodeTypeEntity.IdentityName,
		    BarCodeEntity => BarCodeEntity.IdentityName,
		    ContragentEntity => ContragentEntity.IdentityName,
		    HostEntity => HostEntity.IdentityName,
		    LogTypeEntity => LogTypeEntity.IdentityName,
		    LogEntity => LogEntity.IdentityName,
		    TableScaleModels.NomenclatureEntity => TableScaleModels.NomenclatureEntity.IdentityName,
		    OrderEntity => OrderEntity.IdentityName,
		    OrderWeighingEntity => OrderWeighingEntity.IdentityName,
		    OrganizationEntity => OrganizationEntity.IdentityName,
		    PluEntity => PluEntity.IdentityName,
		    PluLabelEntity => PluLabelEntity.IdentityName,
		    PluObsoleteEntity => PluObsoleteEntity.IdentityName,
		    PluScaleEntity => PluScaleEntity.IdentityName,
		    PluWeighingEntity => PluWeighingEntity.IdentityName,
		    ProductionFacilityEntity => ProductionFacilityEntity.IdentityName,
		    ProductSeriesEntity => ProductSeriesEntity.IdentityName,
		    ScaleEntity => ScaleEntity.IdentityName,
		    TaskEntity => TaskEntity.IdentityName,
		    TaskTypeEntity => TaskTypeEntity.IdentityName,
		    TemplateResourceEntity => TemplateResourceEntity.IdentityName,
		    TemplateEntity => TemplateEntity.IdentityName,
		    VersionEntity => VersionEntity.IdentityName,
		    WorkShopEntity => WorkShopEntity.IdentityName,
		    PrinterEntity => PrinterEntity.IdentityName,
		    PrinterResourceEntity => PrinterResourceEntity.IdentityName,
		    PrinterTypeEntity => PrinterTypeEntity.IdentityName,
		    _ => ColumnName.Default
	    };
    }

    #endregion

}
