// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Apps;

/// <summary>
/// Barcode helper.
/// </summary>
public sealed class WsSqlAppRepository : WsSqlTableRepositoryBase<WsSqlAppModel>
{
    #region Items

    public WsSqlAppModel GetItemByName(string appName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudConfig(
            nameof(WsSqlTableBase.Name), appName, WsSqlEnumIsMarked.ShowAll, false);
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
   
    public WsSqlAppModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlAppModel>();
    
    #endregion

    #region List

    public List<WsSqlAppModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name))); 
        return SqlCore.GetListNotNullable<WsSqlAppModel>(sqlCrudConfig);
    }
    
    #endregion
}