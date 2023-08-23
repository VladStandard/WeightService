// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

public class WsSqlTableRepositoryBase<T> : IWsSqlTableBaseRepository<T> where T : WsSqlTableBase, new()
{
    #region Public and private fields, properties, constructor

    protected WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion
}