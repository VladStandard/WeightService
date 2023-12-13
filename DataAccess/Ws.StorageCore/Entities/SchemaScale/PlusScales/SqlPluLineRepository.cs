namespace Ws.StorageCore.Entities.SchemaScale.PlusScales;

/// <summary>
/// SQL-контроллер таблицы PLUS_SCALES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluLineRepository : SqlTableRepositoryBase<SqlPluScaleEntity>
{
    #region Public and private methods
    
    public SqlPluScaleEntity GetItemByLinePlu(SqlLineEntity line, SqlPluEntity plu)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new()
        {
            SqlRestrictions.EqualFk(nameof(SqlPluScaleEntity.Line), line),
            SqlRestrictions.EqualFk(nameof(SqlPluScaleEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<SqlPluScaleEntity>(sqlCrudConfig);
    }
    
    public List<SqlPluScaleEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlPluScaleEntity> items = SqlCore.GetEnumerable<SqlPluScaleEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public List<SqlPluScaleEntity> GetListByLine(SqlLineEntity line, SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlPluScaleEntity.Line), line));
        return GetList(sqlCrudConfig);
    }
    #endregion
}