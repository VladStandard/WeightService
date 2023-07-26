// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewRefModels.PluLines;

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
        return SqlCore.GetItemNotNullableByUid<WsSqlPluScaleModel>(viewPluScale.Identity.Uid);
    }

    public List<WsSqlPluScaleModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlPluScaleModel> list = SqlCore.GetListNotNullable<WsSqlPluScaleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Plu.Number).ToList();
        return list;
    }

    public List<WsSqlPluScaleModel> GetListByLine(WsSqlScaleModel line, WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddFilters(nameof(WsSqlPluScaleModel.Line), line);
        return GetList(sqlCrudConfig);
    }
    #endregion
}