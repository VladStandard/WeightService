namespace WsStorageCore.OrmUtils;

public static class SqlOrder
{
    public static Order Desc(string name) => Order.Desc(name);
    
    public static Order Asc(string name) => Order.Asc(name);
    
    public static Order CreateDtDesc() => Order.Desc(nameof(WsSqlTableBase.CreateDt));
    
    public static Order NameAsc() => Order.Asc(nameof(WsSqlTableBase.Name));
}