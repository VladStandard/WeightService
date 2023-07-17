// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

/// <summary>
/// Table map "PLUS_GROUPS".
/// </summary>
public sealed class WsSqlPluGroupMap : ClassMap<WsSqlPluGroupModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluGroupMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusGroups);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsGroup).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_GROUP").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(128).Not.Nullable();
        Map(item => item.Code).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("CODE").Length(128).Not.Nullable().Default("");
        Map(item => item.Uid1C).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}