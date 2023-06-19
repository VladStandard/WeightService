// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Plus;

/// <summary>
/// SQL-контроллер таблицы PLUS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Получить ПЛУ по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlPluModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
                { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    /// <summary>
    /// Получить ПЛУ по полю номер.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public WsSqlPluModel GetItemByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel> 
                { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlPluModel>();

    public List<WsSqlPluModel> GetList() => ContextList.GetListNotNullablePlus(SqlCrudConfig);

    /// <summary>
    /// Получить список ПЛУ по номеру.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public List<WsSqlPluModel> GetListByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel> 
            { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(List<short> numbers, WsSqlEnumIsMarked isMarked)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.In,
                Values = numbers.Cast<object>().ToList() } },
            isMarked, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(short minNumber, short maxNumber)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel> 
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.MoreOrEqual, Value = minNumber } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.AddFilters(new WsSqlFieldFilterModel { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlEnumFieldComparer.LessOrEqual, Value = maxNumber });
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список ПЛУ по UID_1C.
    /// </summary>
    /// <param name="uid"></param>
    /// <returns></returns>
    public List<WsSqlPluModel> GetListByUid1C(Guid uid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel> 
            { new() { Name = nameof(WsSqlPluModel.Uid1C), Value = uid } },
            WsSqlEnumIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    /// <summary>
    /// Получить список валидаторов ПЛУ линии.
    /// </summary>
    /// <param name="viewPluLine"></param>
    /// <returns></returns>
    public List<string> GetListValidatesViewPluLine(WsSqlViewPluLineModel viewPluLine)
    {
        List<string> validates = new();
        if (string.IsNullOrEmpty(viewPluLine.TemplateName)) validates.Add(WsLocaleCore.LabelPrint.PluTemplateNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluGtin)) validates.Add(WsLocaleCore.LabelPrint.PluGtinIsNotSet);
        if (string.IsNullOrEmpty(viewPluLine.PluEan13)) validates.Add(WsLocaleCore.LabelPrint.PluEan13IsNotSet);
        //if (string.IsNullOrEmpty(viewPluLine.PluItf14)) validates.Add(LocaleCore.Scales.PluItf14IsNotSet);

        List<WsSqlViewPluLineModel> viewPlusLines = WsSqlContextManagerHelper.Instance.ContextView
            .GetListViewPlusScales(viewPluLine.ScaleId, viewPluLine.PluNumber, 0);
        List<string> plusTemplates = viewPlusLines.Where(item => !string.IsNullOrEmpty(item.TemplateName)).
            Select(item => item.TemplateName).ToList();
        if (!plusTemplates.Any()) validates.Add(WsLocaleCore.LabelPrint.PluTemplateIsNotSet);
        return validates;
    }

    #endregion
}