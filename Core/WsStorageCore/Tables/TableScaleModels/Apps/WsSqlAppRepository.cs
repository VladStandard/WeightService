using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Apps;

/// <summary>
/// Barcode helper.
/// </summary>
public sealed class WsSqlAppRepository : WsSqlTableRepositoryBase<WsSqlAppModel>
{
    #region Items

    public WsSqlAppModel GetItemByName(string appName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlTableBase.Name), appName));
        return SqlCore.GetItemByCrud<WsSqlAppModel>(sqlCrudConfig);
    }

    public WsSqlAppModel GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlAppModel>(uid);

    public WsSqlAppModel GetItemByNameOrCreate(string appName)
    {
        WsSqlAppModel access = GetItemByName(appName);
        if (access.IsNew) 
            access.Name = appName;
        return SaveOrUpdate(access);
    }
    
    public WsSqlAppModel SaveOrUpdate (WsSqlAppModel accessModel)
    {
        // TODO: add Access validator
        if (!accessModel.IsNew)
            SqlCore.Update(accessModel);
        else 
            SqlCore.Save(accessModel);
        return accessModel;
    }
   
    public WsSqlAppModel GetNewItem()
    {
        WsSqlAppModel app = SqlCore.GetItemNewEmpty<WsSqlAppModel>();
        //app.Name = app.DisplayName;
        return app;
    }

    #endregion

    #region List

    public List<WsSqlAppModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlAppModel>(sqlCrudConfig).ToList();
    }

    #endregion
}