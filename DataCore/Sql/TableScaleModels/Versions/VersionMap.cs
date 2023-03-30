// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Versions;

/// <summary>
/// Table map "VERSIONS".
/// </summary>
public sealed class VersionMap : ClassMap<VersionModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public VersionMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.Versions);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.ReleaseDt).CustomSqlType("DATE").Column("RELEASE_DT").Not.Nullable();
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(256).Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(256).Not.Nullable().Default("");
        Map(x => x.Version).CustomSqlType("SMALLINT").Column("VERSION").Not.Nullable();
    }
}
