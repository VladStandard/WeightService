using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Access;

/// <summary>
/// SQL-контроллер таблицы ACCESS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessRepository : WsSqlTableRepositoryBase<WsSqlAccessEntity>
{
    #region Item
    
    public WsSqlAccessEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlAccessEntity>();

    public WsSqlAccessEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlAccessEntity>(uid);

    public WsSqlAccessEntity GetItemByUsername(string userName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.Name), userName));
        return SqlCore.GetItemByCrud<WsSqlAccessEntity>(sqlCrudConfig);
    }
    
    public WsSqlAccessEntity GetItemByNameOrCreate(string username)
    {
        WsSqlAccessEntity access = GetItemByUsername(username);
        if (access.IsNew)
        {
            access.Name = username;
            access.Rights = (byte)WsEnumAccessRights.None;
        }
        return SaveOrUpdate(access);
    }
    
    public WsSqlAccessEntity SaveOrUpdate (WsSqlAccessEntity accessEntity)
    {
        // TODO: add Access validator
        accessEntity.LoginDt = DateTime.Now;
        if (!accessEntity.IsNew)
            SqlCore.Update(accessEntity);
        else 
            SqlCore.Save(accessEntity);
        return accessEntity;
    }
    
    #endregion

    #region List

    public List<WsSqlAccessEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlAccessEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}