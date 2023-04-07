// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Organizations;

/// <summary>
/// Table map "ORGANIZATIONS".
/// </summary>
public sealed class OrganizationMap : ClassMap<OrganizationModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrganizationMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Organizations);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(256).Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(256).Not.Nullable().Default("");
        Map(x => x.Gln).CustomSqlType("INT").Column("GLN").Not.Nullable();
    }
}
