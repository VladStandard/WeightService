using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.StorageCore.Entities.SchemaRef.PlusLines;

public sealed class SqlPluLineRepository : SqlTableRepositoryBase<SqlPluLineEntity>
{
    public SqlPluLineEntity GetItemByLinePlu(SqlLineEntity line, SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters(new()
        {
            SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Line), line),
            SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<SqlPluLineEntity>(sqlCrudConfig);
    }
    
    public List<SqlPluLineEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluLineEntity> items = SqlCore.GetEnumerable<SqlPluLineEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public List<SqlPluLineEntity> GetListByLine(SqlLineEntity line, SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Line), line));
        return GetList(sqlCrudConfig);
    }
}