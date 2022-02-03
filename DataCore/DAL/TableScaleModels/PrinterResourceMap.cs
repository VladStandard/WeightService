// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class PrinterResourceMap : ClassMap<PrinterResourceEntity>
    {
        public PrinterResourceMap()
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
