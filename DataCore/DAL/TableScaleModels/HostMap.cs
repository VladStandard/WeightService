// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Mapping;

namespace DataCore.DAL.TableScaleModels
{
    public class HostMap : ClassMap<HostEntity>
    {
        public HostMap()
        {
            Table("[db_scales].[Hosts]");
            LazyLoad();
            Id(x => x.Id).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
            Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
            Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
            Map(x => x.AccessDt).CustomSqlType("DATETIME").Column("ACCESS_DT").Not.Nullable();
            Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Nullable();
            Map(x => x.Ip).CustomSqlType("VARCHAR").Column("IP").Length(15).Nullable();
            Map(x => x.MacAddressValue).CustomSqlType("VARCHAR").Column("MAC").Length(35).Nullable();
            Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Not.Nullable();
            Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
            Map(x => x.SettingsFile).CustomSqlType("XML").Column("SettingsFile").Nullable().Length(int.MaxValue);
        }
    }
}
