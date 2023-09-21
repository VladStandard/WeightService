namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class WsSqlCrudConfigModel
{
    #region private var

    private readonly ICriterion _isMarkedTrueFilter = SqlRestrictions.IsMarked();
    private readonly ICriterion _isMarkedFalseFilter = SqlRestrictions.IsActual();
    private WsSqlEnumIsMarked _isMarked;
    private int _selectTopRowsCount;
    
    #endregion
    
    #region Public and private fields, properties, constructor
    
    public string NativeQuery { get; set; }
    public List<SqlParameter> NativeParameters { get; set; }
    public List<ICriterion> Filters { get; private set; }
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
    public WsSqlEnumIsMarked IsMarked
    {
        get => _isMarked; 
        set
        {
            _isMarked = value;
            RemoveFilter(_isMarkedFalseFilter);
            RemoveFilter(_isMarkedTrueFilter);
            switch (_isMarked)
            {
                case WsSqlEnumIsMarked.ShowOnlyActual:
                    RemoveFilter(_isMarkedTrueFilter);
                    AddFilter(_isMarkedFalseFilter);
                    break;
                case WsSqlEnumIsMarked.ShowOnlyHide:
                    RemoveFilter(_isMarkedFalseFilter);
                    AddFilter(_isMarkedTrueFilter);
                    break;
            }
        }
    }
    public bool IsReadUncommitted { get; set; }
    
    public WsSqlCrudConfigModel()
    {
        Orders = new();
        Filters = new();
        NativeParameters = new();
        
        IsResultOrder = false;
        IsReadUncommitted = false;
        
        NativeQuery = string.Empty;
        IsMarked = WsSqlEnumIsMarked.ShowAll;
    }
    
    public WsSqlCrudConfigModel(WsSqlCrudConfigModel sqlCrudConfig)
    {
        Orders = new(sqlCrudConfig.Orders);
        Filters = new(sqlCrudConfig.Filters);
        NativeParameters = new(sqlCrudConfig.NativeParameters);
        
        IsResultOrder = sqlCrudConfig.IsResultOrder;
        IsReadUncommitted = sqlCrudConfig.IsReadUncommitted;
        
        NativeQuery = sqlCrudConfig.NativeQuery;
        IsMarked = sqlCrudConfig.IsMarked;
    }

    #endregion
    
    #region Filters

    public void AddFilter(ICriterion filter)
    {
        Filters.Remove(filter);
        Filters.Add(filter);
    }
    public void AddFilters(List<ICriterion> filters)
    {
        foreach (ICriterion filter in filters)
            AddFilter(filter);
    }

    public void RemoveFilter(ICriterion filter)
    {
        Filters.RemoveAll(filter.Equals);
    }
    
    public void RemoveFilters(List<ICriterion> filters)
    {
        foreach (ICriterion filter in filters)
            RemoveFilter(filter);
    }
    
    public void ClearFilters() => Filters.Clear();
    
    #endregion

    #region Orders
    
    private void AddOrder(WsSqlFieldOrderModel order)
    {
        RemoveOrder(order);
        Orders.Add(order);
    }
    public void AddOrder(string key, WsSqlEnumOrder order = WsSqlEnumOrder.Asc) => AddOrder(new(key, order));
    public void AddOrders(List<WsSqlFieldOrderModel> orders)
    {
        foreach (WsSqlFieldOrderModel order in orders)
            AddOrder(order);
    }

    private void RemoveOrder(WsSqlFieldOrderModel order)
    {
        if (!Orders.Any())
            return;
        Orders.RemoveAll(item => item.Name.Equals(order.Name));
    }
    public void RemoveOrders(List<WsSqlFieldOrderModel> orders)
    {
        foreach (WsSqlFieldOrderModel order in orders)
            RemoveOrder(order);
    }
    public void RemoveOrder(string key, WsSqlEnumOrder order = WsSqlEnumOrder.Asc) => RemoveOrder(new(key, order));
    
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