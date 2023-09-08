namespace WsStorageCore.Tables.TableScaleFkModels.PlusCharacteristicsFks;


public sealed class WsSqlPluCharacteristicsFkRepository : WsSqlTableRepositoryBase<WsSqlPluCharacteristicsFkModel>
{
    #region Public and private methods

    public WsSqlPluCharacteristicsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluCharacteristicsFkModel>();

    public IEnumerable<WsSqlPluCharacteristicsFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig) => 
        SqlCore.GetEnumerableNotNullable<WsSqlPluCharacteristicsFkModel>(sqlCrudConfig);
    
    // public List<WsSqlPlu1CFkModel> GetPlus1CFksByGuid1C(Guid uid1C)
    // {
    //     List<WsSqlPlu1CFkModel> plus1CFks = new();
    //     List<WsSqlPluModel> plusDb = new WsSqlPluRepository().GetEnumerableByUid1C(uid1C).ToList();
    //     foreach (WsSqlPluModel plu in plusDb)
    //     {
    //         WsSqlPlu1CFkModel? plu1CFkCache =
    //             WsServiceUtils.ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
    //         if (plu1CFkCache is not null)
    //             plus1CFks.Add(plu1CFkCache);
    //     }
    //     return plus1CFks;
    // }


    #endregion
}