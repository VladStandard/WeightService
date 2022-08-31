// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "Hosts".
/// </summary>
public class HostMap : ClassMap<HostModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public HostMap()
    {
        Schema("db_scales");
        Table("Hosts");
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(x => x.AccessDt).CustomSqlType("DATETIME").Column("ACCESS_DT").Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Nullable();
        Map(x => x.HostName).CustomSqlType("NVARCHAR").Column("HOSTNAME").Length(255).Nullable();
        Map(x => x.Ip).CustomSqlType("VARCHAR").Column("IP").Length(15).Nullable();
        Map(x => x.MacAddressValue).CustomSqlType("VARCHAR").Column("MAC").Length(35).Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
    }
}
