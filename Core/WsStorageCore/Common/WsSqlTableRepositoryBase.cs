namespace WsStorageCore.Common;

public class WsSqlTableRepositoryBase<T> : IWsSqlTableBaseRepository<T> where T : WsSqlEntityBase, new()
{
    #region Public and private fields, properties, constructor

    protected WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion
}