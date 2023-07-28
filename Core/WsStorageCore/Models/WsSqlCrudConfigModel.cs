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

    #endregion
    
    #region Filters

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

    public void AddFilter(WsSqlFieldFilterModel filter)
    {
        if (Filters.Any())
            Filters.RemoveAll(item => item.Name.Equals(filter.Name) && item.Comparer.Equals(filter.Comparer));
        Filters.Add(filter);
    }
    public void AddFilters(List<WsSqlFieldFilterModel> filters)
    {
        foreach (WsSqlFieldFilterModel filter in filters)
            AddFilter(filter);
    }

    public void RemoveFilter(WsSqlFieldFilterModel filter)
    {
        if (!Filters.Any())
            return;
        Filters.RemoveAll(item =>
        {
            if (!item.Name.Equals(filter.Name) || !item.Comparer.Equals(filter.Comparer))
                return false;
            if (filter.Value is not null)
                return item.Value is not null && item.Value.Equals(filter.Value);
            return filter.Values.SequenceEqual(item.Values);
        });
    }
    public void RemoveFilters(List<WsSqlFieldFilterModel> filters)
    {
        foreach (WsSqlFieldFilterModel filter in filters)
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