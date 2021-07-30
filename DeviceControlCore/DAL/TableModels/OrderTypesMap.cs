using FluentNHibernate.Mapping;

namespace DeviceControlCore.DAL.TableModels
{
    public class OrderTypesMap : ClassMap<OrderTypesEntity>
    {
        public OrderTypesMap()
        {
            Table("[db_scales].[OrderTypes]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR(250)").Column("Description").Length(250).Nullable();
        }
    }
}
