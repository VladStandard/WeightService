// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL config for CRUD operations.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlCrudConfigModel
{
    #region Public and private fields, properties, constructor

    private int _selectTopRowsCount;
    private WsSqlEnumIsMarked _isMarked;
    public string NativeQuery { get; set; }
    public bool IsFillReferences { get; set; }
    public List<SqlParameter> NativeParameters { get; set; }
    public List<WsSqlFieldFilterModel> Filters { get; private set; }
    public List<WsSqlFieldOrderModel> Orders { get; private set; }
    public int OldTopRowsCount { get; set; }
    public int SelectTopRowsCount
    {
        get => _selectTopRowsCount;
        set {
            OldTopRowsCount = _selectTopRowsCount;
            _selectTopRowsCount = value;
        }
    }
    public bool IsResultOrder { get; set; }

    /// <summary>
    /// Помеченные на удаление записи.
    /// </summary>
    public WsSqlEnumIsMarked IsMarked
    {
        get => _isMarked; 
        set
        {
            _isMarked = value;
            switch (_isMarked)
            {
                case WsSqlEnumIsMarked.ShowAll:
                    RemoveFilters(GetFiltersIsMarked(false));
                    RemoveFilters(GetFiltersIsMarked(true));
                    break;
                case WsSqlEnumIsMarked.ShowOnlyActual:
                    RemoveFilters(GetFiltersIsMarked(true));
                    AddFilters(GetFiltersIsMarked(false));
                    break;
                case WsSqlEnumIsMarked.ShowOnlyHide:
                    RemoveFilters(GetFiltersIsMarked(false));
                    AddFilters(GetFiltersIsMarked(true));
                    break;
            }
        }
    }

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
        IsMarked = WsSqlEnumIsMarked.ShowAll;
        IsResultOrder = false;
        IsReadUncommitted = false;
    }

    public WsSqlCrudConfigModel(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder, bool isReadUncommitted) : this()
    {
        Filters = filters;
        Orders = orders;
        IsMarked = isMarked;
        IsResultOrder = isOrder;
        IsReadUncommitted = isReadUncommitted;
        SelectTopRowsCount = isShowOnlyTop ? 200 : 0;
    }

    public WsSqlCrudConfigModel(List<WsSqlFieldFilterModel> filters,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder, bool isReadUncommitted) :
        this(filters, new(), isMarked, isShowOnlyTop, isOrder, isReadUncommitted)
    { }
    
    public WsSqlCrudConfigModel(WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder, bool isReadUncommitted) :
        this(new(), new(), isMarked, isShowOnlyTop, isOrder, isReadUncommitted)
    { }

    #endregion

    #region Public and private methods - Filters

    public static List<WsSqlFieldFilterModel> GetFilters(string className, WsSqlTableBase? item) =>
        item is null || string.IsNullOrEmpty(className) ? new()
            : GetFiltersIdentity(className, item.Identity.Name == WsSqlEnumFieldIdentity.Uid ? item.IdentityValueUid : item.IdentityValueId);

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
            WsSqlFieldFilterModel? filterRemove = Filters.Find(
                item => item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer) &&
                    ((item.Value is not null && item.Value.Equals(filter.Value)) || item.Values.Equals(filter.Values)));
            if (filterRemove is not null) Filters.Remove(filterRemove);
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

    public override string ToString()
    {
        string filters = Filters.Any() ? $"{nameof(Filters)}: {string.Join(", ", Filters)}" : string.Empty;
        string orders = Orders.Any() ? $"{nameof(Orders)}: {string.Join(", ", Orders)}" : string.Empty;
        string result = string.Empty;
        
        if (!string.IsNullOrEmpty(filters)) result = filters;
        
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(orders)) result += $" | {orders}";
        else if (!string.IsNullOrEmpty(orders)) result = orders;
        
        string isMarked = IsMarked switch
        {
            WsSqlEnumIsMarked.ShowAll => "Show all records",
            WsSqlEnumIsMarked.ShowOnlyActual => "Show only actual records",
            WsSqlEnumIsMarked.ShowOnlyHide => "Show only hide records",
            _ => string.Empty
        };
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isMarked)) result += $" | {isMarked}";
        else if (!string.IsNullOrEmpty(isMarked)) result = isMarked;
        
        return result;
    }

    #endregion
}