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
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(64).Not.Nullable().Default("");
        Map(item => item.MinTemp).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("MIN_TEMP").Not.Nullable().Default("0");
        Map(item => item.MaxTemp).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("MAX_TEMP").Not.Nullable().Default("0");
    }
}