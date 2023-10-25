using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Barcode helper.
/// </summary>
public sealed class WsSqlAppRepository : WsSqlTableRepositoryBase<WsSqlAppEntity>
{
    #region Items

    public WsSqlAppEntity GetItemByName(string appName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlEntityBase.Name), appName));
        return SqlCore.GetItemByCrud<WsSqlAppEntity>(sqlCrudConfig);
    }

    public WsSqlAppEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlAppEntity>(uid);

    public WsSqlAppEntity GetItemByNameOrCreate(string appName)
    {
        WsSqlAppEntity access = GetItemByName(appName);
        if (access.IsNew) 
            access.Name = appName;
        return SaveOrUpdate(access);
    }
    
    public WsSqlAppEntity SaveOrUpdate (WsSqlAppEntity accessEntity)
    {
        // TODO: add Access validator
        if (!accessEntity.IsNew)
            SqlCore.Update(accessEntity);
        else 
            SqlCore.Save(accessEntity);
        return accessEntity;
    }
   
    public WsSqlAppEntity GetNewItem()
    {
        WsSqlAppEntity app = SqlCore.GetItemNewEmpty<WsSqlAppEntity>();
        //app.Name = app.DisplayName;
        return app;
    }

    #endregion

    #region List

    public List<WsSqlAppEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlAppEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}