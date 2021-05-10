using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ProductionFacilityMap : ClassMap<ProductionFacilityEntity>
    {
        public ProductionFacilityMap()
        {
            Table("[db_scales].[ProductionFacility]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Length(150).Nullable();
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
