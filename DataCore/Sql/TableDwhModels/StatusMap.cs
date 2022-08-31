// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDwhModels;

public class StatusMap : ClassMap<StatusModel>
{
    public StatusMap()
    {
        Table("[ETL].[Statuses]");
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("StatusID").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(25).Column("Name").Not.Nullable();
    }
}
