// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

/// <summary>
/// Table map "LOG_TYPES".
/// </summary>
public sealed class WsSqlLogTypeMap : ClassMap<WsSqlLogTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.LogsTypes);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Number).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("NUMBER").Not.Nullable();
        Map(item => item.Icon).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ICON").Length(32).Not.Nullable();
    }
}
