namespace Ws.Database.Core.Models;

[DebuggerDisplay("{ToString()}")]
public class SqlCrudConfigModel
{
    #region private var
    
    private int _selectTopRowsCount;
    
    #endregion
    
    #region Public and private fields, properties, constructor
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

    public SqlCrudConfigModel()
    {
        Orders = [];
        Filters = [];
    }
    
    public SqlCrudConfigModel(SqlCrudConfigModel sqlCrudConfig)
    {
        Orders = [..sqlCrudConfig.Orders];
        Filters = [..sqlCrudConfig.Filters];
    }

    #endregion
    

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
}