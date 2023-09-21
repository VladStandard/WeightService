namespace WsStorageCore.Tables.TableScaleModels.PlusScales;

/// <summary>
/// SQL-контроллер таблицы PLUS_SCALES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLineRepository : WsSqlTableRepositoryBase<WsSqlPluScaleModel>
{
    #region Public and private methods

    public WsSqlPluScaleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluScaleModel>();

    public WsSqlPluScaleModel GetItem(long scaleId, ushort pluNumber)
    {
        WsSqlViewPluLineModel viewPluScale = ContextCache.LocalViewPlusLines.Find(
            item => Equals(item.ScaleId, (ushort)scaleId) && Equals(item.PluNumber, pluNumber));
        return SqlCore.GetItemByUid<WsSqlPluScaleModel>(viewPluScale.Identity.Uid);
    }
    
    public WsSqlPluScaleModel GetItemByLinePlu(WsSqlScaleModel line, WsSqlPluModel plu)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new()
        {
            SqlRestrictions.EqualFk(nameof(WsSqlPluScaleModel.Line), line),
            SqlRestrictions.EqualFk(nameof(WsSqlPluScaleModel.Plu), plu)
        });
        return SqlCore.GetItemByCrud<WsSqlPluScaleModel>(sqlCrudConfig);
    }
    
    public List<WsSqlPluScaleModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluScaleModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }

    public List<WsSqlPluScaleModel> GetListByLine(WsSqlScaleModel line, WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlPluScaleModel.Line), line));
        return GetList(sqlCrudConfig);
    }
    #endregion
}