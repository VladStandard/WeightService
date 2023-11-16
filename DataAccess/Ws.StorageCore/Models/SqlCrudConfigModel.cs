using Ws.StorageCore.Common;
using Ws.StorageCore.OrmUtils;
namespace Ws.StorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class SqlCrudConfigModel
{
    #region private var

    private readonly ICriterion _isMarkedTrueFilter = SqlRestrictions.IsMarked();
    private readonly ICriterion _isMarkedFalseFilter = SqlRestrictions.IsActual();
    private SqlEnumIsMarked _isMarked;
    private int _selectTopRowsCount;
    
    #endregion
    
    #region Public and private fields, properties, constructor
    
    public string NativeQuery { get; set; }
    public bool IsResultOrder { get; set; }
    public bool IsReadUncommitted { get; set; }
    public List<SqlParameter> NativeParameters { get; set; }
    public List<ICriterion> Filters { get; private init; }
    public List<Order> Orders { get; private init; }
    public int OldTopRowsCount { get; set; }
    public int SelectTopRowsCount
    {
        get => _selectTopRowsCount;
        set {
            OldTopRowsCount = _selectTopRowsCount;
            _selectTopRowsCount = value;
        }
    }
    
    public SqlEnumIsMarked IsMarked
    {
        get => _isMarked; 
        set
        {
            _isMarked = value;
            switch (_isMarked)
            {
                case SqlEnumIsMarked.ShowOnlyActual:
                    RemoveFilter(_isMarkedTrueFilter);
                    AddFilter(_isMarkedFalseFilter);
                    break;
                case SqlEnumIsMarked.ShowOnlyHide:
                    RemoveFilter(_isMarkedFalseFilter);
                    AddFilter(_isMarkedTrueFilter);
                    break;
                case SqlEnumIsMarked.ShowAll:
                    RemoveFilter(_isMarkedFalseFilter);
                    RemoveFilter(_isMarkedTrueFilter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public SqlCrudConfigModel()
    {
        Orders = new();
        Filters = new();
        NativeParameters = new();
        
        IsResultOrder = false;
        IsReadUncommitted = false;
        
        NativeQuery = string.Empty;
        IsMarked = SqlEnumIsMarked.ShowAll;
    }
    
    public SqlCrudConfigModel(SqlCrudConfigModel sqlCrudConfig)
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
        RemoveFilter(filter);
        Filters.Add(filter);
    }
    
    public void AddFilters(List<ICriterion> filters)
    {
        foreach (ICriterion filter in filters)
            AddFilter(filter);
    }
    
    public void RemoveFilter(ICriterion filter)
    {
        Filters.RemoveAll(f => filter.ToString().Equals(f.ToString()));
    }
    
    public void RemoveFilters(List<ICriterion> filters)
    {
        foreach (ICriterion filter in filters)
            RemoveFilter(filter);
    }
    
    public void ClearFilters() => Filters.Clear();
    
    #endregion

    #region Orders
    
    public void AddOrder(Order order)
    {
        RemoveOrder(order);
        Orders.Add(order);
    }
    
    public void AddOrders(List<Order> orders)
    {
        foreach (Order order in orders)
            AddOrder(order);
    }

    public void RemoveOrder(Order order)
    {
        if (!Orders.Any())
            return;
        Orders.RemoveAll(item => order.ToString().Equals(item.ToString()));
    }
    
    public void RemoveOrders(List<Order> orders)
    {
        foreach (Order order in orders)
            RemoveOrder(order);
    }
    
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
            SqlEnumIsMarked.ShowAll => "Show all records",
            SqlEnumIsMarked.ShowOnlyActual => "Show only actual records",
            SqlEnumIsMarked.ShowOnlyHide => "Show only hide records",
            _ => string.Empty
        };
        if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(isMarked)) result += $" | {isMarked}";
        else if (!string.IsNullOrEmpty(isMarked)) result = isMarked;
        
        return result;
    }
    
}