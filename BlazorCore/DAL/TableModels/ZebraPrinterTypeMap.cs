// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace BlazorCore.DAL.TableModels
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
