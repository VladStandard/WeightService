using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.PlusScales;

/// <summary>
/// SQL-контроллер таблицы PLUS_SCALES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLineRepository : WsSqlTableRepositoryBase<WsSqlPluScaleEntity>
{
    #region Public and private methods

    public WsSqlPluScaleEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluScaleEntity>();

    public WsSqlPluScaleEntity GetItem(long scaleId, ushort pluNumber)
    {
        WsSqlViewPluLineModel viewPluScale = ContextCache.LocalViewPlusLines.Find(
            item => Equals(item.ScaleId, (ushort)scaleId) && Equals(item.PluNumber, pluNumber));
        return SqlCore.GetItemByUid<WsSqlPluScaleEntity>(viewPluScale.Identity.Uid);
    }
    
    public WsSqlPluScaleEntity GetItemByLinePlu(WsSqlScaleEntity line, WsSqlPluEntity plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new()
        {
            SqlRestrictions.EqualFk(nameof(WsSqlPluScaleEntity.Line), line),
            SqlRestrictions.EqualFk(nameof(WsSqlPluScaleEntity.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluScaleEntity>(sqlCrudConfig);
    }
    
    public List<WsSqlPluScaleEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluScaleEntity> items = SqlCore.GetEnumerable<WsSqlPluScaleEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public List<WsSqlPluScaleEntity> GetListByLine(WsSqlScaleEntity line, WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluScaleEntity.Line), line));
        return GetList(sqlCrudConfig);
    }
    #endregion
}