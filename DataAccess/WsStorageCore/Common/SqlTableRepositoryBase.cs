namespace WsStorageCore.Common;

public class SqlTableRepositoryBase<T> : ISqlTableBaseRepository<T> where T : SqlEntityBase, new()
{
    #region Public and private fields, properties, constructor

    protected SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    protected SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;

    #endregion
}