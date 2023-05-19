// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table map "PLUS_STORAGE_METHODS".
/// </summary>
public sealed class WsSqlPluStorageMethodMap : ClassMap<WsSqlPluStorageMethodModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluStorageMethodMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusStorageMethods);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(64).Not.Nullable().Default("");
        Map(item => item.MinTemp).CustomSqlType("SMALLINT").Column("MIN_TEMP").Not.Nullable().Default("0");
        Map(item => item.MaxTemp).CustomSqlType("SMALLINT").Column("MAX_TEMP").Not.Nullable().Default("0");
    }
}