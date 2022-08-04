// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDwhModels;

public class InformationSystemMap : ClassMap<InformationSystemEntity>
{
    public InformationSystemMap()
    {
        Table("[ETL].[InformationSystems]");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("InformationSystemID").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(255).Column("Name").Not.Nullable();
        Map(x => x.ConnectString1).CustomSqlType("NVARCHAR").Length(2048).Column("ConnectString1").Nullable();
        Map(x => x.ConnectString2).CustomSqlType("NVARCHAR").Length(2048).Column("ConnectString2").Nullable();
        Map(x => x.ConnectString3).CustomSqlType("NVARCHAR").Length(2048).Column("ConnectString3").Nullable();
        Map(x => x.StatusId).CustomSqlType("INT").Column("StatusID").Not.Nullable();
    }
}
