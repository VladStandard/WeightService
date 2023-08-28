namespace DeviceControl.Components.Nested.PlusLines;

public sealed partial class ItemPlusLines : ItemBase<WsSqlPluScaleModel>
{
    
    private static WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    
    #region Public and private methods

    // TODO: Fix long loading
    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        // Обновить кэш.
        ContextCache.Load(WsSqlEnumTableName.Plus);
        ContextCache.Load(WsSqlEnumTableName.Lines);
    }

    #endregion
}
