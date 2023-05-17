// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Organizations;

/// <summary>
/// Table map "ORGANIZATIONS".
/// </summary>
public sealed class WsSqlOrganizationMap : ClassMap<WsSqlOrganizationModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrganizationMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Organizations);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(256).Not.Nullable();
        Map(item => item.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(256).Not.Nullable().Default("");
        Map(item => item.Gln).CustomSqlType("INT").Column("GLN").Not.Nullable();
    }
}
