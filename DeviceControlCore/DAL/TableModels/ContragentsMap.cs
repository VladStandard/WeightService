using FluentNHibernate.Mapping;

namespace DeviceControlCore.DAL.TableModels
{
    public class ContragentsMap : ClassMap<ContragentsEntity>
    {
        public ContragentsMap()
        {
            Table("[db_scales].[Contragents]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate");
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate");
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Not.Nullable().Length(150);
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked");
            Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
        }
    }
}
