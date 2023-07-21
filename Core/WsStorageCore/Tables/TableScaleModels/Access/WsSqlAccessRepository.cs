// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Access;

/// <summary>
/// SQL-контроллер таблицы ACCESS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessRepository : WsSqlTableRepositoryBase<WsSqlAccessModel>
{
    #region Item
    
    public WsSqlAccessModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlAccessModel>();

    public WsSqlAccessModel GetItemByUid(Guid uid) => SqlCore.GetItemNotNullable<WsSqlAccessModel>(uid);

    public WsSqlAccessModel GetItemByUsername(string userName)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            nameof(WsSqlTableBase.Name), userName, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlAccessModel>(sqlCrudConfig);
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
    
    public List<WsSqlAccessModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableAccesses(sqlCrudConfig);
    
    public List<WsSqlAccessModel> GetList(WsSqlEnumIsMarked isMarked) => 
        ContextList.GetListNotNullableAccesses(new() { IsMarked = isMarked, IsResultOrder = true });

    #endregion
}