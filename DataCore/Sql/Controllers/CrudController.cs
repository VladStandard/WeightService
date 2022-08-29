// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;
using DataCore.Sql.Tables;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Loader;
using NHibernate.Loader.Criteria;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
    #region Public and private fields, properties, constructor

    private DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    private DataConfigurationModel DataConfig { get; }

    private delegate void ExecCallback(ISession session);

    #endregion

    #region Constructor and destructor

    public CrudController()
    {
        DataConfig = new();
    }

    #endregion

    #region Public and private methods

    public T[]? GetItemsWithConfig<T>() where T : TableModel, new()
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
        });
        return result;
    }

    private ICriteria GetCriteria<T>(ISession session, SqlCrudConfigModel sqlCrudConfig)
        where T : TableModel, new()
    {
        ICriteria criteria = session.CreateCriteria(typeof(T));
        if (maxResults > 0)
            criteria.SetMaxResults(maxResults);
        criteria.SetCriteriaFilters(filters);
        criteria.SetCriteriaOrder(order);
        return criteria;
    }

    private ICriteria GetCriteria<T>(ISession session, FieldOrderModel? order, int maxResults)
	    where T : TableModel, new() =>
	    GetCriteria<T>(session, null, order, maxResults);

    private void ExecuteTransaction(ExecCallback callback)
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

    public int ExecQueryNativeInside(string query, Dictionary<string, object> parameters)
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
        });
        return result;
    }

    public int ExecQueryNative(string query, Dictionary<string, object> parameters,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        => ExecQueryNativeInside(query, parameters, filePath, lineNumber, memberName);

    public void Save<T>(T? item) where T : TableModel, new()
    {
        if (item == null)
            return;

        switch (item)
        {
            case ContragentEntity contragent:
                throw new($"{nameof(Save)} for {nameof(ContragentEntity)} is deny!");
            case TableScaleModels.NomenclatureEntity nomenclature:
                throw new($"{nameof(Save)} for {nameof(TableScaleModels.NomenclatureEntity)} is deny!");
            default:
                ExecuteTransaction((session) => { session.Save(item); });
                break;
        }
    }

    public void Update<T>(T? item) where T : TableModel, new()
    {
        if (item == null)
            return;

        item.ChangeDt = DateTime.Now;
        ExecuteTransaction((session) => { session.SaveOrUpdate(item); });
    }

    public void Delete<T>(T? item) where T : TableModel
    {
        //if (item == null || item.EqualsEmpty()) return;
        if (item == null) return;
        ExecuteTransaction((session) => { session.Delete(item); });
    }

    public void Mark<T>(T? item) where T : TableModel
    {
        if (item == null)
            return;

        item.IsMarked = true;
        ExecuteTransaction((session) => { session.SaveOrUpdate(item); });
    }

    private bool ExistsEntityInside<T>(T? item) where T : TableModel, new()
    {
        bool result = false;
        if (item == null)
            return result;

        ExecuteTransaction((session) =>
        {
            result = session.Query<T>().Any(x => x.IsAny(item));
        });
        return result;
    }

    public bool ExistsEntity<T>(T? item) where T : TableModel, new()
    {
        if (item == null)
            return false;

        return ExistsEntityInside(item);
    }

    private bool ExistsEntityInside<T>(List<FieldFilterModel>? filters, FieldOrderModel? order) where T : TableModel, new()
    {
        bool result = false;
        ExecuteTransaction((session) =>
        {
            result = GetCriteria<T>(session, filters, order, 1).List<T>().Count > 0;
        });
        return result;
    }

    public bool ExistsEntity<T>(List<FieldFilterModel>? filters, FieldOrderModel? order) where T : TableModel, new()
    {
        return ExistsEntityInside<T>(filters, order);
    }

    /// <summary>
    /// Get column identity.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public ColumnName GetColumnIdentity(TableModel? item)
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
