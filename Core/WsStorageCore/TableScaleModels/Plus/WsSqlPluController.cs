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
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Uid1C), Value = uid } },
            true, false, false, false, false);
        return AccessItem.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetItemByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            true, false, false, false, false);
        return AccessItem.GetItemNotNullable<WsSqlPluModel>(sqlCrudConfig);
    }

    public WsSqlPluModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPluModel>();

    public List<WsSqlPluModel> GetList() => ContextList.GetListNotNullablePlus(new());

    public List<WsSqlPluModel> GetListByNumber(short number)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Number), Value = number } },
            true, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(List<short> numbers)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.In,
                Values = numbers.Cast<object>().ToList() } },
            true, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByNumbers(short minNumber, short maxNumber)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
            { new() { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.MoreOrEqual, Value = minNumber } },
            true, false, false, false, false);
        sqlCrudConfig.AddFilters(new SqlFieldFilterModel()
        { Name = nameof(WsSqlPluModel.Number), Comparer = WsSqlFieldComparer.LessOrEqual, Value = maxNumber });
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public List<WsSqlPluModel> GetListByUid1c(Guid uid)
    {
        SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>()
                { new() { Name = nameof(WsSqlPluModel.Uid1C), Value = uid } },
            true, false, false, false, false);
        sqlCrudConfig.IsResultOrder = true;
        return ContextList.GetListNotNullablePlus(sqlCrudConfig);
    }

    public bool IsFullValid(WsSqlPluModel pluModel)
    {
        if (pluModel.Gtin == "" || pluModel.Ean13 == "" || pluModel.Itf14 == "")
            return false;

        List<SqlFieldFilterModel> sqlFilters = SqlCrudConfigModel.GetFiltersIdentity(nameof(WsSqlPluTemplateFkModel.Plu), pluModel.IdentityValueUid);
        SqlCrudConfigModel sqlCrudConfig = new(sqlFilters,
            true, false, false, true, false);

        List<WsSqlPluTemplateFkModel> pluTemplateFks = ContextList.GetListNotNullablePlusTemplatesFks(sqlCrudConfig);

        return pluTemplateFks.Count != 0;
    }

    #endregion
}