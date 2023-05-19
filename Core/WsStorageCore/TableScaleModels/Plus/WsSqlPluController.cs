// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Plus;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluModel GetItemByUid1c(Guid uid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Uid1C), Value = uid } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        return AccessItem.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetItemByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        return AccessItem.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPluModel>();

    public List<WsSqlPluModel> GetList() => ContextList.GetListNotNullablePlus(new());

    public List<WsSqlPluModel> GetListByNumber(short number)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(List<short> numbers)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.In,
                Values = numbers.Cast<object>().ToList() } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(short minNumber, short maxNumber)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.MoreOrEqual, Value = minNumber } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.AddFilters(new WsSqlFieldFilterModel()
        { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.LessOrEqual, Value = maxNumber });
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByUid1C(Guid uid)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new List<WsSqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Uid1C), Value = uid } },
            WsSqlIsMarked.ShowAll, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public bool IsFullValid(WsSqlViewPluScaleModel viewPluScale)
    {
        if (viewPluScale.PluGtin == "" || viewPluScale.PluEan13 == "" || viewPluScale.PluItf14 == "")
            return false;

        List<WsSqlFieldFilterModel> sqlFilters = WsSqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlPluTemplateFkModel.Plu), viewPluScale.PluUid);
        WsSqlCrudConfigModel sqlCrudConfig = new(sqlFilters,
            WsSqlIsMarked.ShowAll, false, false, true, false);

        List<WsSqlPluTemplateFkModel> pluTemplateFks = ContextList.GetListNotNullablePlusTemplatesFks(sqlCrudConfig);

        return pluTemplateFks.Count != 0;
    }

    #endregion
}