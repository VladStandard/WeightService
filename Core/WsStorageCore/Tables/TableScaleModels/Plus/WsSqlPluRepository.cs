using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.Plus;

/// <summary>
/// SQL-контроллер таблицы PLUS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluRepository : WsSqlTableRepositoryBase<WsSqlPluModel>
{
    private WsSqlViewPluLineRepository PluLineRepository { get; } = new();
    
    #region Public and private methods

    /// <summary>
    /// Получить ПЛУ по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlPluModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluModel>();

    public IEnumerable<WsSqlPluModel> GetEnumerable() => GetEnumerable(WsSqlCrudConfigFactory.GetCrudAll());

    public IEnumerable<WsSqlPluModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlPluModel.Number)));
        return SqlCore.GetEnumerable<WsSqlPluModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlPluModel> GetEnumerableByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlPluModel.Number), number));
        return GetEnumerable(sqlCrudConfig);
    }

    /// <summary>
    /// Получить ПЛУ по UID_1C.
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public WsSqlPluModel GetByUid1C(Guid uid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlPluModel.Uid1C), uid));
        return SqlCore.GetItemByCrud<WsSqlPluModel>(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список валидаторов ПЛУ линии.
    /// </summary>
    /// <param name="viewPluLine"></param>
    /// <returns></returns>
    public IEnumerable<string> GetEnumerableValidatesViewPluLine(WsSqlViewPluLineModel viewPluLine)
    {
        List<string> validates = new();
        if (string.IsNullOrEmpty(viewPluLine.TemplateName)) validates.Add(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluGtin)) validates.Add(WsLocaleCore.LabelPrint.PluGtinIsNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluEan13)) validates.Add(WsLocaleCore.LabelPrint.PluEan13IsNotSet);
        //if (string.IsNullOrEmpty(viewPluLine.PluItf14)) validates.Add(LocaleCore.Scales.PluItf14IsNotSet);

        IEnumerable<WsSqlViewPluLineModel> viewPlusLines = PluLineRepository.GetEnumerable(viewPluLine.ScaleId, viewPluLine.PluNumber, 0);
        List<string> plusTemplates = viewPlusLines.Where(item => !string.IsNullOrEmpty(item.TemplateName)).
            Select(item => item.TemplateName).ToList();
        if (!plusTemplates.Any()) validates.Add(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
        return validates;
    }

    #endregion
}