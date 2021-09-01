// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableSystemModels
{
    public class HostMap : ClassMap<HostEntity>
    {
        public HostMap()
        {
            Table("[db_scales].[Hosts]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDate).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ModifiedDate).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR(150)").Column("Name").Length(150).Nullable();
            Map(x => x.Ip).CustomSqlType("VARCHAR(15)").Column("IP").Length(15).Nullable();
            Map(x => x.Mac).CustomSqlType("VARCHAR(35)").Column("MAC").Length(35).Nullable();
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Not.Nullable();
            Map(x => x.Marked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
            Map(x => x.SettingsFile).CustomSqlType("XML").Column("SettingsFile").Nullable().Length(int.MaxValue);
        }
    }
}
