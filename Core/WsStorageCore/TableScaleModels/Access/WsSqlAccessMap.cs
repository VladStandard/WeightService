// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Access;

/// <summary>
/// Table map "ACCESS".
/// </summary>
public sealed class WsSqlAccessMap : ClassMap<WsSqlAccessModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlAccessMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Access);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.LoginDt).CustomSqlType("DATETIME").Column("LOGIN_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(32).Not.Nullable();
        Map(x => x.Rights).CustomSqlType("TINYINT").Column("RIGHTS").Not.Nullable().Default("0");
    }
}
