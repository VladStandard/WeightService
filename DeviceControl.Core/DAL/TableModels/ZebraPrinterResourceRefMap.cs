using FluentNHibernate.Mapping;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ZebraPrinterResourceRefMap : ClassMap<ZebraPrinterResourceRefEntity>
    {
        public ZebraPrinterResourceRefMap()
        {
            Table("[db_scales].[ZebraPrinterResourceRef]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            References(x => x.Printer).Column("PrinterID").Not.Nullable();
            References(x => x.Resource).Column("ResourceID").Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR(150)").Length(150).Column("Description").Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        }
    }
}
