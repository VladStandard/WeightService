// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL config for CRUD operations.
/// </summary>
public class SqlCrudConfigModel : ICloneable
{
    #region Public and private fields, properties, constructor

    public string NativeQuery { get; set; }
    public bool IsFillReferences { get; set; }
    public List<SqlParameter> NativeParameters { get; set; }
    public List<SqlFieldFilterModel> Filters { get; private set; }
    public List<SqlFieldOrderModel> Orders { get; private set; }
    public bool IsGuiShowFilterAdditional { get; set; }
    public bool IsGuiShowFilterMarked { get; set; }
    public bool IsGuiShowFilterOnlyTop { get; set; }
    public bool IsGuiShowItemsCount { get; set; }
    public bool IsResultAddFieldEmpty { get; }
    public bool IsResultOrder { get; set; }
    private bool _isResultShowMarked;
    public bool IsResultShowMarked
    {
        get => _isResultShowMarked;
        set
        {
            _isResultShowMarked = value;
            if (!_isResultShowMarked)
                AddFilters(GetFiltersIsResultShowMarked(false));
            else
                RemoveFilters(GetFiltersIsResultShowMarked(false));
        }
    }
    public bool IsResultShowOnlyTop { get; set; }
    /// <summary>
    /// SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED.
    /// </summary>
    public bool IsReadUncommitted { get; set; }
    
    public SqlCrudConfigModel()
    {
        NativeQuery = string.Empty;
        NativeParameters = new();
        IsFillReferences = true;
        Filters = new();
        Orders = new();

        IsGuiShowFilterAdditional = false;
        IsGuiShowFilterMarked = false;
        IsGuiShowFilterOnlyTop = true;
        IsGuiShowItemsCount = false;

        IsResultShowMarked = false;
        IsResultShowOnlyTop = false;
        IsResultAddFieldEmpty = false;
        IsResultOrder = false;
        IsReadUncommitted = false;
    }

    public SqlCrudConfigModel(string query, List<SqlParameter> parameters) : this()
    {
        NativeQuery = query;
        NativeParameters = parameters;
    }

    public SqlCrudConfigModel(string query, SqlParameter parameter, bool isResultAddFieldEmpty) :
        this(query, new List<SqlParameter> { parameter })
    {
        IsResultAddFieldEmpty = isResultAddFieldEmpty;
    }

    public SqlCrudConfigModel(string query, bool isResultAddFieldEmpty) :
        this(query, new List<SqlParameter>())
    {
        IsResultAddFieldEmpty = isResultAddFieldEmpty;
    }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) : this()
    {
        Filters = filters;
        Orders = orders;

        IsResultShowMarked = isShowMarked;
        IsResultShowOnlyTop = isShowOnlyTop;
        IsResultAddFieldEmpty = isAddFieldEmpty;
        IsResultOrder = isOrder;
        IsReadUncommitted = isReadUncommitted;
    }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(filters, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, 
            isOrder, isReadUncommitted)
    { }

    public SqlCrudConfigModel(List<SqlFieldOrderModel> orders,
        bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(new(), orders, isShowMarked, isShowOnlyTop, 
            isAddFieldEmpty, isOrder, isReadUncommitted)
    { }

    public SqlCrudConfigModel(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, bool isReadUncommitted) :
        this(new(), new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, isReadUncommitted)
    { }

    #endregion

    #region Public and private methods - Filters

    public static List<SqlFieldFilterModel> GetFilters(string className, WsSqlTableBase? item) =>
        item is null || string.IsNullOrEmpty(className) ? new()
            : GetFiltersIdentity(className, item.Identity.Name == WsSqlFieldIdentity.Uid ? item.IdentityValueUid : item.IdentityValueId);

    public static List<SqlFieldFilterModel> GetFilters(string className, object? value) =>
        new() { new() { Name = className, Value = value } };

    public static List<SqlFieldFilterModel> GetFiltersIdentity(string className, object? value) =>
        value switch
        {
            Guid uid => new() { 
                new() { Name = $"{className}.{nameof(WsSqlTableBase.IdentityValueUid)}", Value = uid } },
            long id => new() {
                new() { Name = $"{className}.{nameof(WsSqlTableBase.IdentityValueId)}", Value = id } },
            _ => new()
        };

    private List<SqlFieldFilterModel> GetFiltersIsResultShowMarked(bool isShowMarked) =>
        new() { new() { Name = nameof(WsSqlTableBase.IsMarked), Value = isShowMarked } };

    public void AddFilters(List<SqlFieldFilterModel> filters)
    {
        if (!Filters.Any())
        {
            Filters = filters;
            return;
        }

        foreach (SqlFieldFilterModel filter in filters)
        {
            if (!Filters.Contains(filter))
                Filters.Add(filter);
        }
    }

    public void AddFilters(SqlFieldFilterModel filter) => AddFilters(new List<SqlFieldFilterModel> { filter });

    public void AddFilters(string className, WsSqlTableBase? item) => AddFilters(GetFilters(className, item));

    public void ClearFilters() => Filters.Clear();

    public void RemoveFilters(List<SqlFieldFilterModel> filters)
    {
        if (!Filters.Any())
            return;

        foreach (SqlFieldFilterModel filter in filters)
        {
            if (Filters.Contains(filter))
            {
                Filters.Remove(filter);
                break;
            }
        }
    }

    public void RemoveFilters(SqlFieldFilterModel filter) => RemoveFilters(new List<SqlFieldFilterModel> { filter });

    public void RemoveFilters(string className, WsSqlTableBase? item) => RemoveFilters(GetFilters(className, item));

    #endregion

    #region Public and private methods - Orders

    private void AddOrders(List<SqlFieldOrderModel> orders)
    {
        if (!Orders.Any())
            Orders = orders;
        else
            foreach (SqlFieldOrderModel order in orders.Where(order => !Orders.Contains(order)))
            {
                Orders.Add(order);
            }
    }

    public void AddOrders(SqlFieldOrderModel order) => AddOrders(new List<SqlFieldOrderModel> { order });

    private void RemoveOrders(List<SqlFieldOrderModel> orders)
    {
        if (!Orders.Any()) return;
        bool isExists = true;
        while (isExists)
        {
            isExists = false;
            foreach (SqlFieldOrderModel order in orders)
            {
                if (Orders.Contains(order))
                {
                    isExists = true;
                    Orders.Remove(order);
                    break;
                }
            }
        }
    }

    public void RemoveOrders(SqlFieldOrderModel order) => RemoveOrders(new List<SqlFieldOrderModel> { order });

    public object Clone()
    {
        SqlCrudConfigModel item = new();
        item.Filters = new(Filters);
        item.Orders = new(Orders);
        item.IsGuiShowFilterAdditional = IsGuiShowFilterAdditional;
        item.IsGuiShowFilterMarked = IsGuiShowFilterMarked;
        item.IsGuiShowFilterOnlyTop = IsGuiShowFilterOnlyTop;
        item.IsGuiShowItemsCount = IsGuiShowItemsCount;
        item.IsResultShowMarked = IsResultShowMarked;
        item.IsResultShowOnlyTop = IsResultShowOnlyTop;
        return item;
    }

    public SqlCrudConfigModel CloneCast() => (SqlCrudConfigModel)Clone();

    #endregion
}