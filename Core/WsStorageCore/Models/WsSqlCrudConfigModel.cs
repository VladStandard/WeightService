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
            RemoveFilters(GetFiltersIsMarked(true));
            RemoveFilters(GetFiltersIsMarked(false));
            switch (_isMarked)
            {
                case WsSqlEnumIsMarked.ShowOnlyActual:
                    AddFilters(GetFiltersIsMarked(false));
                    break;
                case WsSqlEnumIsMarked.ShowOnlyHide:
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
        IsFillReferences = true;
        
        Orders = new();
        Filters = new();
        NativeParameters = new();
        
        IsResultOrder = false;
        IsReadUncommitted = false;
        
        NativeQuery = string.Empty;
        IsMarked = WsSqlEnumIsMarked.ShowAll;
    }

    public WsSqlCrudConfigModel(List<WsSqlFieldFilterModel> filters, List<WsSqlFieldOrderModel> orders,
        WsSqlEnumIsMarked isMarked, bool isShowOnlyTop, bool isOrder, bool isReadUncommitted) : this()
    {
        IsFillReferences = true;
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

    #endregion
    
    #region Filters

    public static List<WsSqlFieldFilterModel> GetFilters(string className, object? value) =>
        new() { new() { Name = className, Value = value } };

    private List<WsSqlFieldFilterModel> GetFiltersIsMarked(bool isMarked) =>
        new() { new() { Name = nameof(WsSqlTableBase.IsMarked), Value = isMarked } };

    public void AddFkIdentityFilter(string fkPropertyName, WsSqlTableBase item)
    {
        switch (item.Identity)
        {
            case { Name: WsSqlEnumFieldIdentity.Uid }:
                Filters.Add( new() { Name = $"{fkPropertyName}.{nameof(WsSqlTableBase.IdentityValueUid)}", Value = item.Identity.Uid });
                break;
            case { Name: WsSqlEnumFieldIdentity.Id }:
                Filters.Add( new() { Name = $"{fkPropertyName}.{nameof(WsSqlTableBase.IdentityValueId)}", Value = item.Identity.Id });
                break;
        }
    }
    public void AddFilter(WsSqlFieldFilterModel filter) => AddFilters(new() { filter });
    public void AddFilters(List<WsSqlFieldFilterModel> filters)
    {
        if (!Filters.Any())
        {
            Filters = filters;
            return;
        }
        
        Filters = Filters.Concat(filters.Where(filter =>
            !Filters.Any(item =>
                item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer)
            )
        )).ToList();
    }
    public void RemoveFilter(WsSqlFieldFilterModel filter) => RemoveFilters(new() { filter });
    public void RemoveFilters(List<WsSqlFieldFilterModel> filters)
    {
        if (!Filters.Any() || !filters.Any())
            return;
        
        Filters.RemoveAll(filter =>
            filters.Any(item =>
                item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer) &&
                ((item.Value is not null && item.Value.Equals(filter.Value)) || item.Values.Equals(filter.Values))
            )
        );
    }
    public void ClearFilters() => Filters.Clear();
    
    #endregion

    #region Orders
    
    public void AddOrder(WsSqlFieldOrderModel order) => AddOrders(new() { order });
    
    public void AddOrders(List<WsSqlFieldOrderModel> orders)
    {
        if (!Orders.Any())
        {
            Orders = orders;
            return;
        }
        
        Orders = Orders.Concat(orders.Where(order =>
            !Orders.Any(item =>
                item.Name.Equals(order.Name)
            )
        )).ToList();
    }
    
    public void RemoveOrders(List<WsSqlFieldOrderModel> orders)
    {
        if (!Orders.Any() || !orders.Any())
            return;
        
        Orders.RemoveAll(order =>
            orders.Any(item => item.Name.Equals(order.Name) && item.Direction.Equals(order.Direction))
        );
    }
    
    public void RemoveOrder(WsSqlFieldOrderModel order) => RemoveOrders(new() { order });
    
    public void ClearOrders() => Orders.Clear();
    
    #endregion
    
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
    
}