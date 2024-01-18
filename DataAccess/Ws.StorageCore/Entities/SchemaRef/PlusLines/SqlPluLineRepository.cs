using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.StorageCore.Entities.SchemaRef.PlusLines;

public sealed class SqlPluLineRepository : SqlTableRepositoryBase<SqlPluLineEntity>
{
    public SqlPluLineEntity GetItemByLinePlu(SqlLineEntity line, SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilters([
            SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Line), line),
            SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Plu), plu)
        ]);
        return SqlCore.GetItemByCrud<SqlPluLineEntity>(sqlCrudConfig);
    }
    
    public List<SqlPluLineEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluLineEntity> items = SqlCore.GetEnumerable<SqlPluLineEntity>(sqlCrudConfig);
        items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public IEnumerable<SqlPluLineEntity> GetListByLine(SqlLineEntity line)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluLineEntity.Line), line));
        return GetList(crud).OrderBy(i => i.Plu.Number);
    }
    
    public IEnumerable<SqlPluLineEntity> GetWeightListByLine(SqlLineEntity line)
    {
        IEnumerable<SqlPluLineEntity> items = GetListByLine(line);
        return items.Where(x => x.Plu.IsCheckWeight);
    }
    
    public IEnumerable<SqlPluLineEntity> GetPieceListByLine(SqlLineEntity line)
    {
        IEnumerable<SqlPluLineEntity> items = GetListByLine(line);
        return items.Where(x => !x.Plu.IsCheckWeight);
    }
}