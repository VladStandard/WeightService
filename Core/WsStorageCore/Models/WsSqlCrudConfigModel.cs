// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL config for CRUD operations.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlCrudConfigModel : ICloneable
{
    #region Public and private fields, properties, constructor

    public string NativeQuery { get; set; }
    public bool IsFillReferences { get; set; }
    public List<SqlParameter> NativeParameters { get; set; }
    public List<WsSqlFieldFilterModel> Filters { get; private set; }
    public List<WsSqlFieldOrderModel> Orders { get; private set; }
    public bool IsGuiShowFilterAdditional { get; set; }
    public bool IsGuiShowFilterMarked { get; set; }
    public bool IsGuiShowFilterOnlyTop { get; set; }
    public bool IsGuiShowItemsCount { get; set; }
    public bool IsResultAddFieldEmpty { get; }
    public bool IsResultOrder { get; set; }
    private WsSqlIsMarked _isMarked;
    /// <summary>
    /// Помеченные на удаление записи.
    /// </summary>
    public WsSqlIsMarked IsMarked
    {
        get => _isMarked; 
        set
        {
            _isMarked = value;
            switch (_isMarked)
            {
                case WsSqlIsMarked.ShowAll:
                    RemoveFilters(GetFiltersIsMarked(false));
                    RemoveFilters(GetFiltersIsMarked(true));
                    break;
                case WsSqlIsMarked.ShowOnlyActual:
                    RemoveFilters(GetFiltersIsMarked(true));
                    AddFilters(GetFiltersIsMarked(false));
                    break;
                case WsSqlIsMarked.ShowOnlyHide:
                    RemoveFilters(GetFiltersIsMarked(false));
                    AddFilters(GetFiltersIsMarked(true));
                    break;
            }
        }
    }
    public bool IsResultShowOnlyTop { get; set; }
    /// <summary>
    /// SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED.
    /// </summary>
    public bool IsReadUncommitted { get; set; }

    public WsSqlCrudConfigModel()
    {
        NativeQuery = string.Empty;
        NativeParameters = new();
        IsFillReferences = true;
        Filters = new();
        Orders = new();
        IsMarked = WsSqlIsMarked.ShowOnlyActual;

        IsGuiShowFilterAdditional = false;
        IsGuiShowFilterMarked = false;
        IsGuiShowFilterOnlyTop = true;
        IsGuiShowItemsCount = false;

        IsResultShowOnlyTop = false;
        IsResultAddFieldEmpty = false;
        IsResultOrder = false;
        IsReadUncommitted = false;
    }

    public WsSqlCrudConfigModel(string query, List<SqlParameter> parameters) : this()
    {
        NativeQuery = query;
        NativeParameters = parameters;
    }

    public WsSqlCrudConfigModel(string query, SqlParameter parameter, bool isResultAddFieldEmpty) :
        this(query, new List<SqlParameter> { parameter })
    {
        IsResultAddFieldEmpty = isResultAddFieldEmpty;
    }

    public WsSqlCrudConfigModel(string query, bool isResultAddFieldEmpty) :
        this(query, new List<SqlParameter>())
    {
        IsResultAddFieldEmpty = isResultAddFieldEmpty;
    }

    public WsSqlCrudConfigModel(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) : this()
    {
        Filters = filters;
        Orders = orders;

        IsMarked = isMarked;
        IsResultShowOnlyTop = isShowOnlyTop;
        IsResultAddFieldEmpty = isAddFieldEmpty;
        IsResultOrder = isOrder;
        IsReadUncommitted = isReadUncommitted;
    }

    public WsSqlCrudConfigModel(List<WsSqlFieldFilterModel> filters,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(filters, new(), isMarked, isShowOnlyTop, isAddFieldEmpty,
            isOrder, isReadUncommitted)
    { }

    public WsSqlCrudConfigModel(List<WsSqlFieldOrderModel> orders,
        WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(new(), orders, isMarked, isShowOnlyTop,
            isAddFieldEmpty, isOrder, isReadUncommitted)
    { }

    public WsSqlCrudConfigModel(WsSqlIsMarked isMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(new(), new(), isMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, isReadUncommitted)
    { }

    #endregion

    #region Public and private methods - Filters

    public static List<WsSqlFieldFilterModel> GetFilters(string className, WsSqlTableBase? item) =>
        item is null || string.IsNullOrEmpty(className) ? new()
            : GetFiltersIdentity(className, item.Identity.Name == WsSqlFieldIdentity.Uid ? item.IdentityValueUid : item.IdentityValueId);

    public static List<WsSqlFieldFilterModel> GetFilters(string className, object? value) =>
        new() { new() { Name = className, Value = value } };

    public static List<WsSqlFieldFilterModel> GetFiltersIdentity(string className, object? value) =>
        value switch
        {
            Guid uid => new() {
                new() { Name = $"{className}.{nameof(WsSqlTableBase.IdentityValueUid)}", Value = uid } },
            long id => new() {
                new() { Name = $"{className}.{nameof(WsSqlTableBase.IdentityValueId)}", Value = id } },
            _ => new()
        };

    private List<WsSqlFieldFilterModel> GetFiltersIsMarked(bool isMarked) =>
        new() { new() { Name = nameof(WsSqlTableBase.IsMarked), Value = isMarked } };

    public void AddFilters(List<WsSqlFieldFilterModel> filters)
    {
        if (!Filters.Any())
        {
            Filters = filters;
            return;
        }
        foreach (WsSqlFieldFilterModel filter in filters)
        {
            if (!Filters.Exists(item => item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer)))
                Filters.Add(filter);
        }
    }

    public void AddFilters(WsSqlFieldFilterModel filter) => AddFilters(new List<WsSqlFieldFilterModel> { filter });

    public void AddFilters(string className, WsSqlTableBase? item) => AddFilters(GetFilters(className, item));

    public void ClearFilters() => Filters.Clear();

    public void RemoveFilters(List<WsSqlFieldFilterModel> filters)
    {
        if (!Filters.Any()) return;
        foreach (WsSqlFieldFilterModel filter in filters)
        {
            if (Filters.Exists(item => item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer)))
            {
                Filters.Remove(filter);
                break;
            }
        }
    }

    public void RemoveFilters(WsSqlFieldFilterModel filter) => RemoveFilters(new List<WsSqlFieldFilterModel> { filter });

    public void RemoveFilters(string className, WsSqlTableBase? item) => RemoveFilters(GetFilters(className, item));

    #endregion

    #region Public and private methods - Orders

    private void AddOrders(List<WsSqlFieldOrderModel> orders)
    {
        if (!Orders.Any())
        {
            Orders = orders;
            return;
        }
        foreach (WsSqlFieldOrderModel order in orders)
        {
            if (!Orders.Exists(item => item.Name.Equals(order.Name) && item.Direction.Equals(order.Direction)))
                Orders.Add(order);
        }
    }

    public void AddOrders(WsSqlFieldOrderModel order) => AddOrders(new List<WsSqlFieldOrderModel> { order });

    private void RemoveOrders(List<WsSqlFieldOrderModel> orders)
    {
        if (!Orders.Any()) return;
        foreach (WsSqlFieldOrderModel order in orders)
        {
            if (Orders.Exists(item => item.Name.Equals(order.Name) && item.Direction.Equals(order.Direction)))
            {
                Orders.Remove(order);
                break;
            }
        }
    }

    public void RemoveOrders(WsSqlFieldOrderModel order) => RemoveOrders(new List<WsSqlFieldOrderModel> { order });

    public object Clone()
    {
        WsSqlCrudConfigModel item = new();
        item.Filters = new(Filters);
        item.Orders = new(Orders);
        item.IsGuiShowFilterAdditional = IsGuiShowFilterAdditional;
        item.IsGuiShowFilterMarked = IsGuiShowFilterMarked;
        item.IsGuiShowFilterOnlyTop = IsGuiShowFilterOnlyTop;
        item.IsGuiShowItemsCount = IsGuiShowItemsCount;
        item.IsMarked = IsMarked;
        item.IsResultShowOnlyTop = IsResultShowOnlyTop;
        return item;
    }

    public WsSqlCrudConfigModel CloneCast() => (WsSqlCrudConfigModel)Clone();

    public override string ToString()
    {
        string filters = Filters.Any() ? $"{nameof(Filters)}: {string.Join(", ", Filters)}" : string.Empty;
        string orders = Orders.Any() ? $"{nameof(Orders)}: {string.Join(", ", Orders)}" : string.Empty;
        string result = string.Empty;
        
        if (!string.IsNullOrEmpty(filters)) result = filters;
        
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(orders)) result += $" | {orders}";
        else if (!string.IsNullOrEmpty(orders)) result = orders;

        string isGuiShowFilterAdditional = IsGuiShowFilterAdditional ? "Is gui show filter sdditional" : string.Empty;
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isGuiShowFilterAdditional)) result += $" | {isGuiShowFilterAdditional}";
        else if (!string.IsNullOrEmpty(isGuiShowFilterAdditional)) result = isGuiShowFilterAdditional;
        
        string isGuiShowFilterMarked = IsGuiShowFilterMarked ? "Is gui show filterMarked" : string.Empty;
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isGuiShowFilterMarked)) result += $" | {isGuiShowFilterMarked}";
        else if (!string.IsNullOrEmpty(isGuiShowFilterMarked)) result = isGuiShowFilterMarked;

        string isGuiShowFilterOnlyTop = IsGuiShowFilterOnlyTop ? "Is gui show filter only top" : string.Empty;
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isGuiShowFilterOnlyTop)) result += $" | {isGuiShowFilterOnlyTop}";
        else if (!string.IsNullOrEmpty(isGuiShowFilterOnlyTop)) result = isGuiShowFilterOnlyTop;

        string isGuiShowItemsCount = IsGuiShowItemsCount ? "Is gui show items count" : string.Empty;
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isGuiShowItemsCount)) result += $" | {isGuiShowItemsCount}";
        else if (!string.IsNullOrEmpty(isGuiShowItemsCount)) result = isGuiShowItemsCount;

        string isMarked = IsMarked switch
        {
            WsSqlIsMarked.ShowAll => "Show all records",
            WsSqlIsMarked.ShowOnlyActual => "Show only actual records",
            WsSqlIsMarked.ShowOnlyHide => "Show only hide records",
            _ => string.Empty
        };
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isMarked)) result += $" | {isMarked}";
        else if (!string.IsNullOrEmpty(isMarked)) result = isMarked;
        
        string isResultShowOnlyTop = IsResultShowOnlyTop ? "Is result show only top" : string.Empty;
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isResultShowOnlyTop)) result += $" | {isResultShowOnlyTop}";
        else if (!string.IsNullOrEmpty(isResultShowOnlyTop)) result = isResultShowOnlyTop;

        return result;
    }

    #endregion
}