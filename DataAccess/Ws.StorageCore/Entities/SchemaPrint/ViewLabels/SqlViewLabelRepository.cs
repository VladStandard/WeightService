using System;

namespace Ws.StorageCore.Entities.SchemaPrint.ViewLabels;

public sealed class SqlViewLabelRepository : SqlTableRepositoryBase<SqlViewLabel>
{
    public List<SqlViewLabel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlViewLabel>(sqlCrudConfig).ToList();
    }
}