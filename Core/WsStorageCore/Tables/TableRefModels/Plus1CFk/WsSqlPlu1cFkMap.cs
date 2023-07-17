// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableRefModels.Plus1CFk;

/// <summary>
/// Маппинг полей таблицы REF.PLUS_1C_FK.
/// </summary>
public sealed class WsSqlPlu1CFkMap : ClassMap<WsSqlPlu1CFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPlu1CFkMap()
    {
        Schema(WsSqlSchemasUtils.Ref);
        Table(WsSqlTablesUtils.Plus1CFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        Map(item => item.IsEnabled).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_ENABLED").Not.Nullable().Default("0");
        Map(item => item.RequestDataString).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("REQUEST_DATA_STRING").Not.Nullable().Default("");
    }
}