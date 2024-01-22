using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Common;

public class SqlTableRepositoryBase<T> where T : EntityBase, new()
{
    protected SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
}