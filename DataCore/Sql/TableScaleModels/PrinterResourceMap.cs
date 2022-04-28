// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.Sql.TableScaleModels
{
    public class PrinterResourceMap : ClassMap<PrinterResourceEntity>
    {
        public PrinterResourceMap()
        {
            Table("[db_scales].[ZebraPrinterResourceRef]");
            LazyLoad();
            Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            References(x => x.Printer).Column("PrinterID").Not.Nullable();
            References(x => x.Resource).Column("ResourceID").Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.Description).CustomSqlType("NVARCHAR").Column("Description").Length(150).Nullable();
        }
    }
}
