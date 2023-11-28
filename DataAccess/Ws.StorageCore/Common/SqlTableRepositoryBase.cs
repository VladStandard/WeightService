namespace Ws.StorageCore.Common;

public class SqlTableRepositoryBase<T> where T : SqlEntityBase, new()
{
    protected SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
}