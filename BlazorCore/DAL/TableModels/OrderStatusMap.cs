using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class OrderStatusMap : ClassMap<OrderStatusEntity>
    {
        public OrderStatusMap()
        {
            Table("[db_scales].[OrderStatus]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            CompositeId().KeyProperty(x => x.OrderId, "OrderId")
                .KeyProperty(x => x.CurrentDate, "CurrentDate")
                .KeyProperty(x => x.Id, "Id");
            Map(x => x.OrderId).CustomSqlType("INT").Column("OrderId").Not.Nullable();
            Map(x => x.CurrentDate).CustomSqlType("DATETIME").Column("CurrentDate").Not.Nullable();
            Map(x => x.CurrentStatus).CustomSqlType("TINYINT").Column("CurrentStatus").Nullable();
        }
    }
}
