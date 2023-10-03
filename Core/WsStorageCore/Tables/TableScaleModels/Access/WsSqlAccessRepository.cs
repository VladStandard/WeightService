using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Access;

/// <summary>
/// SQL-контроллер таблицы ACCESS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessRepository : WsSqlTableRepositoryBase<WsSqlAccessModel>
{
    #region Item
    
    public WsSqlAccessModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlAccessModel>();

    public WsSqlAccessModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlAccessModel>(uid);

    public WsSqlAccessModel GetItemByUsername(string userName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.Name), userName));
        return SqlCore.GetItemByCrud<WsSqlAccessModel>(sqlCrudConfig);
    }
    
    public WsSqlAccessModel GetItemByNameOrCreate(string username)
    {
        WsSqlAccessModel access = GetItemByUsername(username);
        if (access.IsNew)
        {
            access.Name = username;
            access.Rights = (byte)WsEnumAccessRights.None;
        }
        return SaveOrUpdate(access);
    }
    
    public WsSqlAccessModel SaveOrUpdate (WsSqlAccessModel accessModel)
    {
        // TODO: add Access validator
        accessModel.LoginDt = DateTime.Now;
        if (!accessModel.IsNew)
            SqlCore.Update(accessModel);
        else 
            SqlCore.Save(accessModel);
        return accessModel;
    }
    
    #endregion

    #region List

    public List<WsSqlAccessModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlAccessModel>(sqlCrudConfig).ToList();
    }

    #endregion
}