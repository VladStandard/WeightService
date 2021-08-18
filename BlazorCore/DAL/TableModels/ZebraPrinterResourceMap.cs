using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class ZebraPrinterResourceMap : ClassMap<ZebraPrinterResourceEntity>
    {
        public ZebraPrinterResourceMap()
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
