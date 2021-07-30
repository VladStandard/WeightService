using FluentNHibernate.Mapping;

namespace DeviceControlCore.DAL.TableModels
{
    public class ZebraPrinterTypeMap : ClassMap<ZebraPrinterTypeEntity>
    {
        public ZebraPrinterTypeMap()
        {
            Table("[db_scales].[ZebraPrinterType]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(100)").Column("Name").Nullable().Length(100);
        }
    }
}
