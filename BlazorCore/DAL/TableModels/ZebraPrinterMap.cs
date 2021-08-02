using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
{
    public class ZebraPrinterMap : ClassMap<ZebraPrinterEntity>
    {
        public ZebraPrinterMap()
        {
            Table("[db_scales].[ZebraPrinter]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(100)").Length(100).Column("Name").Nullable();
            Map(x => x.Ip).CustomSqlType("VARCHAR(15)").Length(15).Column("IP").Nullable();
            Map(x => x.Port).CustomSqlType("SMALLINT").Column("Port").Nullable();
            Map(x => x.Password).CustomSqlType("VARCHAR(15)").Length(15).Column("Password").Nullable();
            References(x => x.PrinterType).Column("PrinterTypeId").Not.Nullable();
            Map(x => x.Mac).CustomSqlType("VARCHAR(20)").Length(20).Column("Mac").Nullable();
            Map(x => x.PeelOffSet).CustomSqlType("BIT").Column("PeelOffSet").Nullable();
            Map(x => x.DarknessLevel).CustomSqlType("SMALLINT").Column("DarknessLevel").Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        }
    }
}
