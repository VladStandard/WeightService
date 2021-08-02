using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class WorkshopMap : ClassMap<WorkshopEntity>
    {
        public WorkshopMap()
        {
            Table("[db_scales].[WorkShop]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Not.Nullable().Length(150);
            References(x => x.ProductionFacility).Column("ProductionFacilityID").Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
