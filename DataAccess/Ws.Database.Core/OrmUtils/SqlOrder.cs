using Ws.Domain.Models.Common;

namespace Ws.Database.Core.OrmUtils;

public static class SqlOrder
{
    public static Order CreateDtDesc() => Order.Desc(nameof(EntityBase.CreateDt));
    
    public static Order NameAsc() => Order.Asc(nameof(EntityBase.Name));
}