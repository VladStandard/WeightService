using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class OrdersMap : ClassMap<OrdersEntity>
    {
        public OrdersMap()
        {
            Table("[db_scales].[Orders]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            References(x => x.OrderTypes).Column("OrderType");
            Map(x => x.ProductDate).CustomSqlType("DATETIME").Column("ProductDate").Nullable();
            Map(x => x.PlaneBoxCount).CustomSqlType("INT").Column("PlaneBoxCount").Nullable();
            Map(x => x.PlanePalletCount).CustomSqlType("INT").Column("PlanePalletCount").Nullable();
            Map(x => x.PlanePackingOperationBeginDate).CustomSqlType("DATETIME").Column("PlanePackingOperationBeginDate").Nullable();
            Map(x => x.PlanePackingOperationEndDate).CustomSqlType("DATETIME").Column("PlanePackingOperationEndDate").Nullable();
            References(x => x.Scales).Column("ScaleId");
            References(x => x.Plu).Column("PLU");
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
            References(x => x.Templates).Column("TemplateId");
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        }
    }
}
