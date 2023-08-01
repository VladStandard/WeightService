// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C });
        return SqlCore.GetItemByCrud<WsSqlPluModel>(sqlCrudConfig);
    }

    /// <summary>
    /// Получить ПЛУ по полю номер.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public WsSqlPluModel GetItemByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlPluModel.Number), Value = number });
        return SqlCore.GetItemByCrud<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluModel>();

    public List<WsSqlPluModel> GetList() => GetList(WsSqlCrudConfigFactory.GetCrudAll());

    public List<WsSqlPluModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlPluModel.Number));
        return SqlCore.GetListNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список ПЛУ по номеру.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public List<WsSqlPluModel> GetListByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlPluModel.Number), Value = number });
        return GetList(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(List<short> numbers, WsSqlEnumIsMarked isMarked)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new()
        {
            Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.In,
            Values = numbers.Cast<object>().ToList()
        });
        return GetList(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByRange(short minNumber, short maxNumber)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(
            new()
            {
                new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.LessOrEqual, Value = maxNumber },
                new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.MoreOrEqual, Value = minNumber }
            }
        );
        return GetList(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список ПЛУ по UID_1C.
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public List<WsSqlPluModel> GetListByUid1C(Guid uid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTableBase.IdentityValueUid), Value = uid });
        return GetList(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список валидаторов ПЛУ линии.
    /// </summary>
    /// <param name="viewPluLine"></param>
    /// <returns></returns>
    public List<string> GetListValidatesViewPluLine(WsSqlViewPluLineModel viewPluLine)
    {
        List<string> validates = new();
        if (string.IsNullOrEmpty(viewPluLine.TemplateName)) validates.Add(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluGtin)) validates.Add(WsLocaleCore.LabelPrint.PluGtinIsNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluEan13)) validates.Add(WsLocaleCore.LabelPrint.PluEan13IsNotSet);
        //if (string.IsNullOrEmpty(viewPluLine.PluItf14)) validates.Add(LocaleCore.Scales.PluItf14IsNotSet);

        List<WsSqlViewPluLineModel> viewPlusLines = PluLineRepository.GetList(viewPluLine.ScaleId, viewPluLine.PluNumber, 0);
        List<string> plusTemplates = viewPlusLines.Where(item => !string.IsNullOrEmpty(item.TemplateName)).
            Select(item => item.TemplateName).ToList();
        if (!plusTemplates.Any()) validates.Add(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
        return validates;
    }

    #endregion
}