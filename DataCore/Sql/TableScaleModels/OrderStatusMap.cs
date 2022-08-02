// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "OrderStatus".
/// </summary>
public class OrderStatusMap : ClassMap<OrderStatusEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderStatusMap()
    {
        Schema("db_scales");
        Table("OrderStatus");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        CompositeId().KeyProperty(x => x.OrderId, "OrderId")
            .KeyProperty(x => x.CurrentDate, "CurrentDate")
            .KeyProperty(x => x.IdentityId, "Id");
        Map(x => x.OrderId).CustomSqlType("INT").Column("OrderId").Not.Nullable();
        Map(x => x.CurrentDate).CustomSqlType("DATETIME").Column("CurrentDate").Not.Nullable();
        Map(x => x.CurrentStatus).CustomSqlType("TINYINT").Column("CurrentStatus").Nullable();
    }
}
